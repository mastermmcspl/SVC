Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.Object
Partial Class Accounts_ReceiptTransactionDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts_ReceiptTransactionDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objRecp As New clsReceipt
    Dim objCOA As New clsChartOfAccounts

    Private Shared sSession As AllSession
    Private objAccSetting As New clsAccountSetting
    Private objSettings As New clsApplicationSettings

    Private Shared sTypeName As String
    Private Shared sMasterName As String
    Private Shared iMasterID As Integer
    Private Shared iPKID As Integer
    Private Shared sTableName As String
    Private Shared sGMSave As String
    Private Shared sGMFlag As String
    Private Shared sGMBackStatus As String
    Private Shared iDbOrCr As Integer = 0

    Public dtReceipt As New DataTable
    Private objclsAttachments As New clsAttachments
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Private Shared dtAttach As New DataTable
    Private Shared sWFDelete As String
    Private Shared sINWView As String
    Private Shared sINWDownload As String
    Private objclsModulePermission As New clsModulePermission
    Private Shared iInwardID As Integer
    Private Shared iStatus As Integer
    Private objIndex As New clsIndexing
    Dim dt As New DataTable
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Public Shared dtDiff As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnConfirm.ImageUrl = "~/Images/Confirm24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnSearch.ImageUrl = "~/Images/Search16.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"

        imgbtnDrOtherGL.ImageUrl = "~/Images/Add16.png"
        imgbtnDrOtherSubGL.ImageUrl = "~/Images/Add16.png"
        imgbtnCrOtherGL.ImageUrl = "~/Images/Add16.png"
        imgbtnCrOtherSGL.ImageUrl = "~/Images/Add16.png"

        btnAddDebit.ImageUrl = "~/Images/Add24.png"
        btnAddCredit.ImageUrl = "~/Images/Add24.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterType As String = ""
        Dim sMasterID As String = ""
        Dim sFormButtons As String = ""
        Dim iDefaultBranch As Integer
        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "RT")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : btnAddCredit.Visible = False : btnAddDebit.Visible = False
                btnAddAttch.Visible = False : btnScan.Visible = False : imgbtnConfirm.Visible = False
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
                        btnAddCredit.Visible = True : btnAddDebit.Visible = True
                        btnAddAttch.Visible = True : btnScan.Visible = True
                    End If
                End If
                'If sSession.YearID > 0 Then
                '    sStr = sSession.YearName
                '    sArray = sStr.Split("-")
                '    iSYear = sArray(0)
                '    iEYear = sArray(1)

                'dStartDate = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'dEndDate = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                '    ccInvoiceDate.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    ccInvoiceDate.EndDate = New DateTime(iEYear, 3, dEndDate)

                '    cclBillDate.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    cclBillDate.EndDate = New DateTime(iEYear, 3, dEndDate)
                'End If

                'rgvtxtInvoiceDate.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvtxtInvoiceDate.MinimumValue = "" & dStartDate & ""
                'rgvtxtInvoiceDate.MaximumValue = "" & dEndDate & ""

                'rgvtxtBillDate.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvtxtBillDate.MinimumValue = "" & dStartDate & ""
                'rgvtxtBillDate.MaximumValue = "" & dEndDate & ""

                'txtStartDate.Text = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtEndDate.Text = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                BindPaymentType() : LoadCurrencyType() : GetAppSettings()
                BillNoVisibleFalse() : OrderVisibleFalse()

                txtPaidAmount.Text = "0"
                DivBill.Visible = False : DivBillManual.Visible = False
                Session("dtReceipt") = Nothing
                'imgbtnAdd.Visible = False : imgbtnSave.Visible = False
                imgbtnUpdate.Visible = False
                'imgbtnAdd.Visible = True : imgbtnSave.Visible = True
                'divCollapseOthersDetails.Visible = True

                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)

                iDefaultBranch = objRecp.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                BinPartyOrCustomerORGL() : BindTransactionType() : BindHeadofAccounts()

                LoadBillType() : LoadSubGL()
                txtTransactionNo.Text = objRecp.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                Me.txtBillDate.Attributes.Add("onblur", "return checkForm()")
                lblStatus.Text = "Not Started."

                RFVInvoiceDate.ErrorMessage = "Enter Date of Receipt."
                REVInvoiceDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$" : REVInvoiceDate.ErrorMessage = "Enter Valid Date Format."

                RFVddlPaymentType.InitialValue = "Select Receipt Type" : RFVddlPaymentType.ErrorMessage = "Select Receipt Type"
                RFVCustomerParty.InitialValue = "Select Customer/Supplier/GL" : RFVddlDbOtherGL.ErrorMessage = "Select Customer/Supplier/GL."
                RFVParty.InitialValue = "Select Customer/Supplier/Party" : RFVParty.ErrorMessage = "Select Customer/Supplier/Party."
                RFVTransType.InitialValue = "Select Transaction Type" : RFVTransType.ErrorMessage = "Select Transaction Type."
                RFVBillType.InitialValue = "Select Receipt Voucher Type" : RFVBillType.ErrorMessage = "Select Receipt Voucher Type."

                'RFVBillDate.ErrorMessage = "Enter Bill Date."
                'RFVBillDate.ErrorMessage = "Enter Valid Bill Date."
                REFBillDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"

                RFVEBillAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVEBillAmount.ErrorMessage = "Enter Valid Bill Amount."

                RFVEPaidAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVEPaidAmount.ErrorMessage = "Enter Valid Bill Amount."

                RFVddlDbOtherGL.InitialValue = "Select GL Code" : RFVddlDbOtherGL.ErrorMessage = "Select General Ledger."
                RFVddlCrOtherGL.InitialValue = "Select GL Code" : RFVddlCrOtherGL.ErrorMessage = "Select General Ledger."

                RFVEChequeNo.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVEChequeNo.ErrorMessage = "Enter Valid Cheque No."

                REFChequeDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REFChequeDate.ErrorMessage = "Enter Valid cheque Date."

                REFFundDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REFFundDate.ErrorMessage = "Enter Valid Fund Transfer Date."

                REFChallenDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REFChallenDate.ErrorMessage = "Enter Valid Challen Date."

                RFVAccZone.InitialValue = "--- Select Zone ---" : RFVAccZone.ErrorMessage = "Select Zone."
                RFVAccRgn.InitialValue = "--- Select Region ---" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "--- Select Area ---" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "--- Select Branch ---" : RFVAccBrnch.ErrorMessage = "Select Branch."

                sINWView = "YES" : sINWDownload = "YES" : sWFDelete = "YES"
                iAttachID = 0
                lblSize.Text = "(Max " & sSession.FileSize & "MB)"
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                imgView.ImageUrl = "~/Images/NoImage.jpg"

                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    ddlExistReceipt.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExistReceipt_SelectedIndexChanged(sender, e)
                    divcollapseAttachments.Visible = True
                    BindAllAttachments(sSession.AccessCode, iAttachID)
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
    Public Sub BindPaymentType()
        Try
            ddlPaymentType.Items.Insert(0, "Select Receipt Type")
            ddlPaymentType.Items.Insert(1, "With Inventory Advance")
            ddlPaymentType.Items.Insert(2, "Without Inventory Advance")
            ddlPaymentType.Items.Insert(3, "General")
            ddlPaymentType.Items.Insert(4, "With Inventory Receipt")
            ddlPaymentType.Items.Insert(5, "Without Inventory Receipt")
            ddlPaymentType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingPurchaseOrder(ByVal iPayementType As Integer, ByVal iParty As Integer)
        Try
            If iPayementType = 1 Or iPayementType = 4 Then   'Inventory
                ddlOrderNo.DataSource = objRecp.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iParty)
                ddlOrderNo.DataTextField = "SPO_OrderCode"
                ddlOrderNo.DataValueField = "SPO_ID"
                ddlOrderNo.DataBind()
                ddlOrderNo.Items.Insert(0, "Existing Sales Order")
            End If
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
    Public Sub BinPartyOrCustomerORGL()
        Try
            'ddlCustomerParty.Items.Insert(0, "Select Customer/Supplier/GL")
            'ddlCustomerParty.Items.Insert(1, "Customer")
            'ddlCustomerParty.Items.Insert(2, "Supplier")
            'ddlCustomerParty.Items.Insert(3, "General Ledger")
            'ddlCustomerParty.SelectedIndex = 0

            ddlCustomerParty.Items.Add(New ListItem("Select Customer/Supplier/GL", 0))
            ddlCustomerParty.Items.Add(New ListItem("Customer", 1))
            ddlCustomerParty.Items.Add(New ListItem("Supplier", 2))
            ddlCustomerParty.Items.Add(New ListItem("General Ledger", 3))
            ddlCustomerParty.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindTransactionType()
        Try
            ddlTransType.Items.Insert(0, "Select Transaction Type")
            ddlTransType.Items.Insert(1, "Cash")
            ddlTransType.Items.Insert(2, "Bank(Cheque)")
            ddlTransType.Items.Insert(3, "Demand Draft")
            ddlTransType.Items.Insert(4, "RTGS")
            ddlTransType.Items.Insert(5, "NEFT")
            ddlTransType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindHeadofAccounts()
        Try

            ddlDBOtherHead.Items.Insert(0, "Select Head of Account")
            ddlDBOtherHead.Items.Insert(1, "Asset")
            ddlDBOtherHead.Items.Insert(2, "Income")
            ddlDBOtherHead.Items.Insert(3, "Expenditure")
            ddlDBOtherHead.Items.Insert(4, "Liabilities")
            ddlDBOtherHead.SelectedIndex = 0

            ddlCrOtherHead.Items.Insert(0, "Select Head of Account")
            ddlCrOtherHead.Items.Insert(1, "Asset")
            ddlCrOtherHead.Items.Insert(2, "Income")
            ddlCrOtherHead.Items.Insert(3, "Expenditure")
            ddlCrOtherHead.Items.Insert(4, "Liabilities")
            ddlCrOtherHead.SelectedIndex = 0


            ddlHead.Items.Insert(0, "Select Head of Account")
            ddlHead.Items.Insert(1, "Asset")
            ddlHead.Items.Insert(2, "Income")
            ddlHead.Items.Insert(3, "Expenditure")
            ddlHead.Items.Insert(4, "Liabilities")
            ddlHead.SelectedIndex = 0

            ddlHeadSgl.Items.Insert(0, "Select Head of Account")
            ddlHeadSgl.Items.Insert(1, "Asset")
            ddlHeadSgl.Items.Insert(2, "Income")
            ddlHeadSgl.Items.Insert(3, "Expenditure")
            ddlHeadSgl.Items.Insert(4, "Liabilities")
            ddlHeadSgl.SelectedIndex = 0

            ddlCRGLHead.Items.Insert(0, "Select Head of Account")
            ddlCRGLHead.Items.Insert(1, "Asset")
            ddlCRGLHead.Items.Insert(2, "Income")
            ddlCRGLHead.Items.Insert(3, "Expenditure")
            ddlCRGLHead.Items.Insert(4, "Liabilities")
            ddlCRGLHead.SelectedIndex = 0

            ddlHeadCRSgl.Items.Insert(0, "Select Head of Account")
            ddlHeadCRSgl.Items.Insert(1, "Asset")
            ddlHeadCRSgl.Items.Insert(2, "Income")
            ddlHeadCRSgl.Items.Insert(3, "Expenditure")
            ddlHeadCRSgl.Items.Insert(4, "Liabilities")
            ddlHeadCRSgl.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSubGL()
        Dim dt As New DataTable
        Try
            dt = objRecp.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)

            ddlDbOtherSubGL.DataSource = dt
            ddlDBOtherSubGL.DataTextField = "GlDesc"
            ddlDBOtherSubGL.DataValueField = "gl_Id"
            ddlDBOtherSubGL.DataBind()
            ddlDBOtherSubGL.Items.Insert(0, "Select SubGL Code")

            ddlCrOtherSubGL.DataSource = dt
            ddlCrOtherSubGL.DataTextField = "GlDesc"
            ddlCrOtherSubGL.DataValueField = "gl_Id"
            ddlCrOtherSubGL.DataBind()
            ddlCrOtherSubGL.Items.Insert(0, "Select SubGL Code")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadBillType()
        Try
            ddlExistReceipt.DataSource = objRecp.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            ddlExistReceipt.DataTextField = "Acc_RM_TransactionNo"
            ddlExistReceipt.DataValueField = "Acc_RM_ID"
            ddlExistReceipt.DataBind()
            ddlExistReceipt.Items.Insert(0, "Existing Receipt Voucher")

            ddlBillType.DataSource = objRecp.LoadBIllType(sSession.AccessCode, sSession.AccessCodeID)
            ddlBillType.DataTextField = "Mas_Desc"
            ddlBillType.DataValueField = "Mas_ID"
            ddlBillType.DataBind()
            ddlBillType.Items.Insert(0, "Select Receipt Voucher Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindBankName()
        Try
            ' If ddlCrGL.SelectedIndex > 0 Then
            ddlBankName.DataSource = objRecp.LoadBankNames(sSession.AccessCode, sSession.AccessCodeID)
            ddlBankName.DataTextField = "Mas_Desc"
            ddlBankName.DataValueField = "Mas_Id"
            ddlBankName.DataBind()
            ddlBankName.Items.Insert(0, "Select Bank Name")
            ' End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadParty(ByVal iType As Integer)
        Try
            If iType = 3 Then

                ddlParty.DataSource = objRecp.LoadAllGLCodes(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "GlDesc"
                ddlParty.DataValueField = "gl_Id"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")

            ElseIf iType = 1 Then
                'ddlParty.DataSource = objPayment.LoadParty(sSession.AccessCode, sSession.AccessCodeID, iType)
                'ddlParty.DataTextField = "Name"
                'ddlParty.DataValueField = "ACM_ID"
                'ddlParty.DataBind()
                'ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
                ddlParty.DataSource = objRecp.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "BM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")

            ElseIf iType = 2 Then
                'ddlParty.DataSource = objPayment.LoadParty(sSession.AccessCode, sSession.AccessCodeID, iType)
                'ddlParty.DataTextField = "Name"
                'ddlParty.DataValueField = "ACM_ID"
                'ddlParty.DataBind()
                'ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
                ddlParty.DataSource = objRecp.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "CSM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSupplier()
        Try
            ddlParty.DataSource = objRecp.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "Name"
            ddlParty.DataValueField = "CSM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCustomers()
        Try
            ddlParty.DataSource = objRecp.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCustomerParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerParty.SelectedIndexChanged
        Try
            If ddlCustomerParty.SelectedIndex = 1 Then
                lblParty.Text = "* Customer"
                If ddlTransType.SelectedIndex = 0 Then
                    ddlCustomerParty.SelectedIndex = 0
                End If
                If ddlTransType.SelectedIndex > 0 Then
                    LoadCustomers()
                End If
            ElseIf ddlCustomerParty.SelectedIndex = 2 Then
                lblParty.Text = "* Supplier"
                If ddlTransType.SelectedIndex = 0 Then
                    ddlCustomerParty.SelectedIndex = 0
                End If
                If ddlTransType.SelectedIndex > 0 Then
                    LoadSupplier()
                End If
            ElseIf ddlCustomerParty.SelectedIndex = 3 Then
                lblParty.Text = "* General Ledger"
                LoadParty(3)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerParty_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object
        Try
            lblError.Text = ""

            If lblStatus.Text = "Waiting for Approval" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf lblStatus.Text = "De-Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf lblStatus.Text = "Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Not Started." Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            End If
            Response.Redirect(String.Format("~/Accounts/ReceiptTransaction.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dtParty As New DataTable
        Dim chkSelectBox As New CheckBox
        Try
            lblError.Text = ""
            If ddlParty.SelectedIndex > 0 Then

                If ddlCustomerParty.SelectedIndex = 3 Then  ' General Ledger
                    ddlCrOtherHead.SelectedIndex = objRecp.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                    ddlCrOtherHead_SelectedIndexChanged(sender, e)

                    ddlCrOtherGL.SelectedValue = ddlParty.SelectedValue
                    ddlCrOtherGL_SelectedIndexChanged(sender, e)

                ElseIf ddlCustomerParty.SelectedIndex = 1 Then   'Customer

                    dt = objRecp.getCustomerLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        ddlCrOtherHead.SelectedIndex = objRecp.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("BM_GL").ToString())
                        ddlCrOtherHead_SelectedIndexChanged(sender, e)

                        ddlCrOtherGL.SelectedValue = dt.Rows(0)("BM_GL").ToString()
                        ddlCrOtherGL_SelectedIndexChanged(sender, e)

                        If dt.Rows(0)("BM_SubGL").ToString() = "0" Then
                            ddlCrOtherSubGL.SelectedIndex = -1
                        Else
                            ddlCrOtherSubGL.SelectedValue = dt.Rows(0)("BM_SubGL").ToString()
                        End If
                    End If
                ElseIf ddlCustomerParty.SelectedIndex = 2 Then   'Suppliers

                    dt = objRecp.getSuppliersLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        ddlCrOtherHead.SelectedIndex = objRecp.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CSM_GL").ToString())
                        ddlCrOtherHead_SelectedIndexChanged(sender, e)

                        ddlCrOtherGL.SelectedValue = dt.Rows(0)("CSM_GL").ToString()
                        ddlCrOtherGL_SelectedIndexChanged(sender, e)

                        If dt.Rows(0)("CSM_SubGL").ToString() = "0" Then
                            ddlCrOtherSubGL.SelectedIndex = -1
                        Else
                            ddlCrOtherSubGL.SelectedValue = dt.Rows(0)("CSM_SubGL").ToString()
                        End If
                    End If
                End If

                dtParty = objRecp.CheckBillForParty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue, ddlPaymentType.SelectedIndex)
                If dtParty.Rows.Count > 0 Then
                    'If ddlParty.SelectedIndex > 0 Then
                    '    lblError.Text = "Do you want to Adjust the Bills Manually ?."
                    '    lblBillAdjusment.Text = lblError.Text
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divBillAdjusment').addClass('alert alert-success');$('#ModalBillAdjusment').modal('show');", True)
                    'End If
                    If ddlPaymentType.SelectedIndex = 1 Then    'With Inventory Advance 
                        LoadExistingPurchaseOrder(ddlPaymentType.SelectedIndex, ddlParty.SelectedValue)

                    ElseIf ddlPaymentType.SelectedIndex = 4 Then    'With Inventory Payment
                        LoadExistingPurchaseOrder(ddlPaymentType.SelectedIndex, ddlParty.SelectedValue)
                        If lblStatus.Text = "Not Started." Then
                            ddlCurrency.Enabled = True : imgbtnConfirm.Visible = True : imgbtnSave.Visible = False
                        End If
                        'If ddlParty.SelectedIndex > 0 Then
                        '    lblError.Text = "Do you want to Adjust the Bills Manually ?."
                        '    lblBillAdjusment.Text = lblError.Text
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divBillAdjusment').addClass('alert alert-success');$('#ModalBillAdjusment').modal('show');", True)
                        'End If
                    ElseIf ddlPaymentType.SelectedIndex = 5 Then    'Without Inventory Payment
                        If lblStatus.Text = "Not Started." Then
                            ddlCurrency.Enabled = True : imgbtnConfirm.Visible = True : imgbtnSave.Visible = False
                        End If
                    End If
                Else
                    If lblStatus.Text = "Not Started." Then
                        ddlCurrency.Enabled = True : imgbtnConfirm.Visible = True : imgbtnSave.Visible = False
                    End If
                    DivBill.Visible = False
                    GVReceipt.DataSource = Nothing
                    GVReceipt.DataBind()

                    DivBillManual.Visible = False
                    GVBillManual.DataSource = Nothing
                    GVBillManual.DataBind()
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim iReceiptID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim sArray As Array, sTransArray As Array

        Dim sBillNo As String = "", sBillName As String = ""
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim lblPMID As New Label : Dim lblBillNo As New Label

        Dim dDatel, dSDateo As Date
        Dim dGridDebit As Double = 0 : Dim dGridCredit As Double = 0, dSum As Double = 0
        Dim dDate, dSDate As Date : Dim m As Integer, iGL As Integer = 0, iSubGL As Integer = 0
        Dim sTime As String = ""

        Try
            lblError.Text = ""

            If ddlCustomerParty.SelectedIndex = 1 Then
                lblError.Text = "select " & lblParty.Text & " ."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If

            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date of Receipt (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblPaymentValidataionMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date of Receipt (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblPaymentValidataionMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            'Cheque Date Comparision'
            If txtBillDate.Text <> "" Then
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblPaymentValidataionMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtBillDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblPaymentValidataionMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtBillDate.Focus()
                    Exit Sub
                End If
            End If
            'Cheque Date Comparision'

            'If ddlDbOtherSubGL.Items.Count > 1 Then
            '    If ddlDbOtherSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Debit."
            '        Exit Sub
            '    End If
            'End If

            'If ddlCrOtherSubGL.Items.Count > 1 Then
            '    If ddlCrOtherSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Credit."
            '        Exit Sub
            '    End If
            'End If

            If dgReceiptDetails.Items.Count = 0 Then
                lblError.Text = "Add Amount"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Amount','', 'success');", True)
                Exit Sub
            End If

            For i = 0 To dgReceiptDetails.Items.Count - 1
                dGridDebit = dGridDebit + Convert.ToDouble(dgReceiptDetails.Items(i).Cells(9).Text)
                dGridCredit = dGridCredit + Convert.ToDouble(dgReceiptDetails.Items(i).Cells(10).Text)
            Next

            If dGridDebit <> dGridCredit Then
                lblError.Text = "Debit And Credit Amount not matching."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit And Credit Amount not matching','', 'success');", True)
                Exit Sub
            End If
            For i = 0 To dgReceiptDetails.Items.Count - 1
                dGridDebit = Convert.ToDouble(dgReceiptDetails.Items(i).Cells(9).Text)
                dSum = dSum + dGridDebit
            Next

            'If ddlTransType.SelectedItem.Text = "Bank" Then
            '    If txtChequeNo.Text = "" Then
            '        lblError.Text = "Enter Cheque No."
            '        Exit Sub
            '    End If
            '    If txtChequeDate.Text = "" Then
            '        lblError.Text = "Enter Cheque Date."
            '        Exit Sub
            '    End If
            '    If txtIFSC.Text = "" Then
            '        lblError.Text = "Enter IFSC No."
            '        Exit Sub
            '    End If
            '    If ddlBankName.SelectedIndex = 0 Then
            '        lblError.Text = "Select Bank Name."
            '        Exit Sub
            '    End If
            '    'If txtBranchName.Text = "" Then
            '    '    lblError.Text = "Enter Branch Name."
            '    '    Exit Sub
            '    'End If
            'End If

            'Cheque Date Comparision'
            If txtBillDate.Text <> "" Then
                dDatel = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDateo = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim f As Integer
                f = DateDiff(DateInterval.Day, dDatel, dSDateo)
                If f < 0 Then
                    lblError.Text = "Receipt Date (" & txtInvoiceDate.Text & ") should be Greater than or equal to Invoice Date(" & txtBillDate.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Receipt Date (" & txtInvoiceDate.Text & ") should be Greater than or equal to Invoice Date(" & txtBillDate.Text & ").','', 'success');", True)
                    txtBillDate.Focus()
                    Exit Sub
                End If
            End If
            'Cheque Date Comparision'

            'Extra'
            If GVReceipt.Rows.Count > 0 Then
                For i = 0 To GVReceipt.Rows.Count - 1
                    chkField = GVReceipt.Rows(i).FindControl("chkSelectBox")
                    lblPMID = GVReceipt.Rows(i).FindControl("lblSMID")
                    lblBillNo = GVReceipt.Rows(i).FindControl("lblBillNo")

                    If chkField.Checked = True Then
                        sBillNo = sBillNo & "," & lblPMID.Text
                        sBillName = sBillName & "," & lblBillNo.Text
                    End If
                Next
            Else
                sBillNo = ""
            End If
            'Extra'

            If ddlExistReceipt.SelectedIndex > 0 Then
                objRecp.iAcc_RM_ID = ddlExistReceipt.SelectedValue
            Else
                objRecp.iAcc_RM_ID = 0
            End If

            objRecp.sAcc_RM_TransactionNo = txtTransactionNo.Text

            If ddlCustomerParty.SelectedIndex > 0 Then
                objRecp.iAcc_RM_Location = ddlCustomerParty.SelectedIndex
            Else
                objRecp.iAcc_RM_Location = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                objRecp.iAcc_RM_Party = ddlParty.SelectedValue
            Else
                objRecp.iAcc_RM_Party = 0
            End If

            If ddlTransType.SelectedIndex > 0 Then
                objRecp.iAcc_RM_TransactionType = ddlTransType.SelectedIndex
            Else
                objRecp.iAcc_RM_TransactionType = 0
            End If

            If ddlBillType.SelectedIndex > 0 Then
                objRecp.iAcc_RM_BillType = ddlBillType.SelectedValue
            Else
                objRecp.iAcc_RM_BillType = 0
            End If

            If txtBillNo.Text <> "" Then
                objRecp.sAcc_RM_BillNo = txtBillNo.Text
            Else
                objRecp.sAcc_RM_BillNo = ""
            End If
            If txtBillDate.Text <> "" Then
                objRecp.dAcc_RM_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objRecp.dAcc_RM_BillDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtBillAmount.Text <> "" Then
                objRecp.dAcc_RM_BillAmount = txtBillAmount.Text
            Else
                objRecp.dAcc_RM_BillAmount = 0
            End If

            objRecp.iAcc_RM_YearID = sSession.YearID
            objRecp.sAcc_RM_Status = "W"
            objRecp.iAcc_RM_CreatedBy = sSession.UserID
            objRecp.sAcc_RM_Operation = "U"
            objRecp.sAcc_RM_IPAddress = sSession.IPAddress
            objRecp.sAcc_RM_BillNarration = txtNarration.Text
            If txtInvoiceDate.Text <> "" Then
                objRecp.dAcc_RM_InvoiceDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objRecp.dAcc_RM_InvoiceDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If ddlTransType.SelectedIndex = 2 Then 'Cheque Details 

                If txtChequeNo.Text <> "" Then
                    objRecp.sAcc_RM_ChequeNo = txtChequeNo.Text

                    If txtChequeDate.Text = "" Then
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact(txtChequeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    If txtIFSC.Text <> "" Then
                        objRecp.sAcc_RM_IFSCCode = txtIFSC.Text
                    Else
                        objRecp.sAcc_RM_IFSCCode = ""
                    End If

                    If ddlBankName.SelectedIndex > 0 Then
                        objRecp.sAcc_RM_BankName = ddlBankName.SelectedValue
                    Else
                        objRecp.sAcc_RM_BankName = 0
                    End If

                    If txtBranchName.Text <> "" Then
                        objRecp.sAcc_RM_BranchName = txtBranchName.Text
                    Else
                        objRecp.sAcc_RM_BranchName = ""
                    End If
                    objRecp.iAcc_RM_TrTypeDetails = 1
                ElseIf txtFundTransferNo.Text <> "" Then
                    objRecp.sAcc_RM_ChequeNo = txtFundTransferNo.Text

                    If txtFundTransferDate.Text = "" Then
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact(txtFundTransferDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    objRecp.sAcc_RM_IFSCCode = ""
                    objRecp.sAcc_RM_BankName = 0
                    objRecp.sAcc_RM_BranchName = ""
                    objRecp.iAcc_RM_TrTypeDetails = 2
                ElseIf txtChallenNo.Text <> "" Then
                    objRecp.sAcc_RM_ChequeNo = txtChallenNo.Text

                    If txtChallenDate.Text = "" Then
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact(txtChallenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    objRecp.sAcc_RM_IFSCCode = ""
                    objRecp.sAcc_RM_BankName = 0
                    objRecp.sAcc_RM_BranchName = ""
                    objRecp.iAcc_RM_TrTypeDetails = 3
                Else
                    objRecp.sAcc_RM_ChequeNo = ""
                    objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    objRecp.sAcc_RM_IFSCCode = ""
                    objRecp.sAcc_RM_BankName = 0
                    objRecp.sAcc_RM_BranchName = ""
                    objRecp.iAcc_RM_TrTypeDetails = 0
                End If
            Else
                objRecp.sAcc_RM_ChequeNo = ""
                objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objRecp.sAcc_RM_IFSCCode = ""
                objRecp.sAcc_RM_BankName = 0
                objRecp.sAcc_RM_BranchName = ""
                objRecp.iAcc_RM_TrTypeDetails = 0
            End If

            If txtPaidAmount.Text <> "" Then
                objRecp.dAcc_RM_PaidAmount = txtPaidAmount.Text
            Else
                objRecp.dAcc_RM_PaidAmount = 0
            End If

            If sBillNo <> "" Then
                objRecp.dAcc_RM_BalanceAmount = 0
            Else
                If txtBillAmount.Text <> "" Then
                    objRecp.dAcc_RM_BalanceAmount = txtBillAmount.Text - txtPaidAmount.Text
                Else
                    objRecp.dAcc_RM_BalanceAmount = 0 - txtPaidAmount.Text
                End If
            End If

            objRecp.iAcc_RM_AttachID = iAttachID

            objRecp.iACC_RM_ZoneID = ddlAccZone.SelectedValue
            objRecp.iACC_RM_RegionID = ddlAccRgn.SelectedValue
            objRecp.iACC_RM_AreaID = ddlAccArea.SelectedValue
            objRecp.iACC_RM_BranchID = ddlAccBrnch.SelectedValue

            If ddlOrderNo.SelectedIndex > 0 Then
                objRecp.iAcc_RM_OrderNO = ddlOrderNo.SelectedValue
            Else
                objRecp.iAcc_RM_OrderNO = 0
            End If
            If txtOrderDate.Text = "" Then
                objRecp.dAcc_RM_OrderDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objRecp.dAcc_RM_OrderDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If ddlPaymentType.SelectedIndex > 0 Then
                objRecp.iAcc_RM_PaymentType = ddlPaymentType.SelectedIndex
            Else
                objRecp.iAcc_RM_PaymentType = 0
            End If
            If txtFEAmt.Text <> "" Then
                objRecp.dAcc_RM_FETotalAmt = txtFEAmt.Text.Trim
                objRecp.dAcc_RM_CurrencyAmt = objRecp.GetFECRates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                'objRecp.sAcc_RM_CurrencyTime = objRecp.GetFECTime(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                sTime = objRecp.GetFECTime(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                If sTime <> "" Then
                    objRecp.sAcc_RM_CurrencyTime = sTime
                Else
                    objRecp.sAcc_RM_CurrencyTime = " "
                End If
            Else
                objRecp.dAcc_RM_FETotalAmt = 0
                objRecp.dAcc_RM_CurrencyAmt = 0
                objRecp.sAcc_RM_CurrencyTime = " "
            End If
            If ddlCurrency.SelectedIndex > 0 Then
                objRecp.iAcc_RM_Currency = ddlCurrency.SelectedValue
            Else
                objRecp.iAcc_RM_Currency = 0
            End If
            If txtDiffAmount.Text <> "" Then
                objRecp.dAcc_RM_DiffAmount = txtDiffAmount.Text.Trim
            Else
                objRecp.dAcc_RM_DiffAmount = 0
            End If

            objRecp.iAcc_RM_BatchNo = 0
            objRecp.iACc_RM_BaseName = 0

            sArray = objRecp.SaveReceiptMaster(sSession.AccessCode, sSession.AccessCodeID, objRecp)
            iReceiptID = sArray(1)

            For i = 0 To dgReceiptDetails.Items.Count - 1
                objRecp.iATD_TrType = 3
                objRecp.dATD_TransactionDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objRecp.iATD_BillId = iReceiptID
                objRecp.iATD_DbOrCr = dgReceiptDetails.Items(i).Cells(12).Text
                objRecp.iATD_Head = dgReceiptDetails.Items(i).Cells(1).Text
                objRecp.iATD_GL = dgReceiptDetails.Items(i).Cells(2).Text
                objRecp.iATD_SubGL = dgReceiptDetails.Items(i).Cells(3).Text

                If objRecp.iATD_DbOrCr = 1 Then
                    objRecp.dATD_Debit = dgReceiptDetails.Items(i).Cells(9).Text
                    objRecp.dATD_Credit = 0.00

                ElseIf objRecp.iATD_DbOrCr = 2 Then
                    objRecp.dATD_Debit = 0.00
                    objRecp.dATD_Credit = dgReceiptDetails.Items(i).Cells(10).Text
                End If

                objRecp.iATD_CreatedBy = sSession.UserID
                objRecp.sATD_Status = "W"
                objRecp.iATD_YearID = sSession.YearID
                objRecp.sATD_Operation = "U"
                objRecp.sATD_IPAddress = sSession.IPAddress

                objRecp.iATD_ZoneID = ddlAccZone.SelectedValue
                objRecp.iATD_RegionID = ddlAccRgn.SelectedValue
                objRecp.iATD_AreaID = ddlAccArea.SelectedValue
                objRecp.iATD_BranchID = ddlAccBrnch.SelectedValue

                objRecp.dATD_OpenDebit = "0.00"
                objRecp.dATD_OpenCredit = "0.00"
                objRecp.dATD_ClosingDebit = "0.00"
                objRecp.dATD_ClosingCredit = "0.00"
                objRecp.iATD_SeqReferenceNum = 0

                sTransArray = objRecp.SaveTransactionsDetails(sSession.AccessCode, sSession.AccessCodeID, objRecp)
            Next
            If txtDiffAmount.Text <> "" And txtDiffAmount.Text > 0 Then
                If ddlCrOtherGL.SelectedIndex > 0 Then
                    iGL = ddlCrOtherGL.SelectedValue
                End If

                If ddlCrOtherSubGL.SelectedIndex > 0 Then
                    iSubGL = ddlCrOtherSubGL.SelectedValue
                End If
                dtDiff = objRecp.LoadFEDiffDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtDiffAmount.Text.Trim, lblFEStatus.Text.Trim, ddlCrOtherHead.SelectedIndex, iGL, iSubGL)
                If dtDiff.Rows.Count > 0 Then
                    For i = 0 To dtDiff.Rows.Count - 1
                        objRecp.iATD_TrType = 3
                        objRecp.iATD_BillId = iReceiptID
                        objRecp.iATD_PaymentType = 0
                        objRecp.iATD_DbOrCr = dtDiff.Rows(i)("DebitOrCredit")
                        objRecp.iATD_Head = dtDiff.Rows(i)("HeadID")
                        objRecp.iATD_GL = dtDiff.Rows(i)("GLID")
                        objRecp.iATD_SubGL = dtDiff.Rows(i)("SubGLID")
                        If objRecp.iATD_DbOrCr = 1 Then
                            dDebit = dtDiff.Rows(i)("Debit")
                            objRecp.dATD_Debit = dDebit
                            objRecp.dATD_Credit = 0.00
                        Else
                            dCredit = dtDiff.Rows(i)("Credit")
                            objRecp.dATD_Debit = 0.00
                            objRecp.dATD_Credit = dCredit
                        End If

                        objRecp.iATD_CreatedBy = sSession.UserID
                        objRecp.sATD_Status = "W"
                        objRecp.iATD_YearID = sSession.YearID
                        objRecp.sATD_Operation = "C"
                        objRecp.sATD_IPAddress = sSession.IPAddress
                        sTransArray = objRecp.SaveTransactionsDetails(sSession.AccessCode, sSession.AccessCodeID, objRecp)
                        objRecp.UpdateFEKey(sSession.AccessCode, sSession.AccessCodeID, 3, sTransArray(1))
                    Next
                End If
            End If
            lblError.Text = "Successfully Saved & Waiting for Approval."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
            lblStatus.Text = "Waiting for Approval"

            ddlExistReceipt.DataSource = objRecp.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            ddlExistReceipt.DataTextField = "Acc_RM_TransactionNo"
            ddlExistReceipt.DataValueField = "Acc_RM_ID"
            ddlExistReceipt.DataBind()
            ddlExistReceipt.Items.Insert(0, "Existing Receipt Voucher")

            ddlExistReceipt.SelectedValue = iReceiptID
            ddlExistReceipt_SelectedIndexChanged(sender, e)

            'MakePayment(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, txtPaidAmount.Text)

            If GVBillManual.Rows.Count > 0 Then
                MakeManualPayment(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, txtPaidAmount.Text)
            ElseIf GVReceipt.Rows.Count > 0 Then
                MakePayment(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, txtPaidAmount.Text, ddlParty.SelectedValue)
            End If
            txtBillIDs.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub ddlExistReceipt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistReceipt.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            imgView.ImageUrl = "~/Images/NoImage.jpg"
            If ddlExistReceipt.SelectedIndex > 0 Then
                BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistReceipt.SelectedValue)
                ddlCustomerParty_SelectedIndexChanged(sender, e)
                ddlParty_SelectedIndexChanged(sender, e)
                ddlTransType_SelectedIndexChanged(sender, e)
                ddlPaymentType_SelectedIndexChanged(sender, e)
                'imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
            Else
                imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
                txtTransactionNo.Text = "" : ddlParty.SelectedIndex = 0 : ddlBillType.SelectedIndex = 0
                txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = ""
                lblError.Text = "" : ddlDBOtherHead.SelectedIndex = 0 : ddlCrOtherHead.SelectedIndex = 0
                dgReceiptDetails.DataSource = dt
                dgReceiptDetails.DataBind()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Modal Popup", "Closepopup();", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistReceipt_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iReceipt As Integer)
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim sExtraAmount As String = ""
        Dim iTrTypeDetails As Integer
        Try
            lblAmount.Text = ""
            dt = objRecp.GetReceiptDetails(sNameSpace, iCompID, iReceipt)
            If dt.Rows.Count > 0 Then

                If IsDBNull(dt.Rows(0)("Acc_RM_AttachID")) = False Then
                    iAttachID = dt.Rows(0)("Acc_RM_AttachID").ToString()
                Else
                    iAttachID = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_TransactionNo").ToString()) = False Then
                    txtTransactionNo.Text = dt.Rows(0)("Acc_RM_TransactionNo").ToString()
                Else
                    txtTransactionNo.Text = ""
                End If


                If IsDBNull(dt.Rows(0)("ACC_RM_Location").ToString()) = False Then
                    ddlCustomerParty.SelectedValue = Trim(dt.Rows(0)("ACC_RM_Location"))
                Else
                    ddlCustomerParty.SelectedIndex = 0
                End If


                If IsDBNull(dt.Rows(0)("Acc_RM_Party").ToString()) = False Then
                    If dt.Rows(0)("ACC_RM_Location") > 0 Then
                        LoadParty(dt.Rows(0)("ACC_RM_Location"))
                        ddlParty.SelectedValue = dt.Rows(0)("Acc_RM_Party").ToString()
                    Else
                        ddlParty.Items.Clear()
                    End If
                Else
                    ddlParty.SelectedIndex = 0
                End If


                If IsDBNull(dt.Rows(0)("Acc_RM_TransactionType").ToString()) = False Then
                    ddlTransType.SelectedIndex = dt.Rows(0)("Acc_RM_TransactionType").ToString()
                Else
                    ddlTransType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillType").ToString()) = False Then
                    ddlBillType.SelectedValue = dt.Rows(0)("Acc_RM_BillType").ToString()
                Else
                    ddlBillType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillNo").ToString()) = False Then
                    txtBillNo.Text = dt.Rows(0)("Acc_RM_BillNo").ToString()
                Else
                    txtBillNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillDate").ToString()) = False Then
                    txtBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_BillDate").ToString(), "D")
                Else
                    txtBillDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillAmount").ToString()) = False Then
                    txtBillAmount.Text = dt.Rows(0)("Acc_RM_BillAmount").ToString()
                Else
                    txtBillAmount.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillNarration").ToString()) = False Then
                    txtNarration.Text = dt.Rows(0)("Acc_RM_BillNarration").ToString()
                Else
                    txtNarration.Text = ""
                End If


                If IsDBNull(dt.Rows(0)("Acc_RM_trTypeDetails").ToString()) = False Then
                    If dt.Rows(0)("Acc_RM_trTypeDetails").ToString() = 0 Then
                        iTrTypeDetails = 0
                    Else
                        iTrTypeDetails = dt.Rows(0)("Acc_RM_trTypeDetails").ToString()
                    End If
                Else
                    iTrTypeDetails = 0
                End If

                If iTrTypeDetails = 1 Then
                    If IsDBNull(dt.Rows(0)("ACC_RM_ChequeNo").ToString()) = False Then
                        txtChequeNo.Text = dt.Rows(0)("ACC_RM_ChequeNo").ToString()
                    Else
                        txtChequeNo.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("Acc_PM_ChequeDate").ToString()) = False Then
                        If (dt.Rows(0)("Acc_RM_ChequeDate").ToString() <> "") Then
                            If (objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_ChequeDate").ToString(), "D") <> "01/01/1900") Then
                                txtChequeDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_ChequeDate").ToString(), "D")
                            Else
                                txtChequeDate.Text = ""
                            End If
                        Else
                            txtChequeDate.Text = ""
                        End If
                    Else
                        txtChequeDate.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("Acc_RM_IFSCCode").ToString()) = False Then
                        txtIFSC.Text = dt.Rows(0)("Acc_RM_IFSCCode").ToString()
                    Else
                        txtIFSC.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("ACC_RM_BankName").ToString()) = False Then
                        If dt.Rows(0)("ACC_RM_BankName").ToString() = "" Then
                            BindBankName()
                            ddlBankName.SelectedIndex = 0
                        Else
                            If dt.Rows(0)("ACC_RM_BankName").ToString() > 0 Then
                                ddlBankName.SelectedValue = dt.Rows(0)("ACC_RM_BankName").ToString()
                            Else
                                BindBankName()
                                ddlBankName.SelectedIndex = 0
                            End If
                        End If
                    Else
                        ddlBankName.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("Acc_RM_BranchName").ToString()) = False Then
                        txtBranchName.Text = dt.Rows(0)("Acc_RM_BranchName").ToString()
                    Else
                        txtBranchName.Text = ""
                    End If
                ElseIf iTrTypeDetails = 2 Then
                    If IsDBNull(dt.Rows(0)("ACC_RM_ChequeNo").ToString()) = False Then
                        txtFundTransferNo.Text = dt.Rows(0)("ACC_RM_ChequeNo").ToString()
                    Else
                        txtFundTransferNo.Text = ""

                    End If

                    If IsDBNull(dt.Rows(0)("Acc_RM_ChequeDate").ToString()) = False Then
                        If (dt.Rows(0)("Acc_RM_ChequeDate").ToString() <> "") Then
                            If (objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_ChequeDate").ToString(), "D") <> "01/01/1900") Then
                                txtFundTransferDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_ChequeDate").ToString(), "D")
                            Else
                                txtFundTransferDate.Text = ""
                            End If
                        Else
                            txtFundTransferDate.Text = ""
                        End If
                    Else
                        txtFundTransferDate.Text = ""
                    End If
                ElseIf iTrTypeDetails = 3 Then
                    If IsDBNull(dt.Rows(0)("ACC_RM_ChequeNo").ToString()) = False Then
                        txtChallenNo.Text = dt.Rows(0)("ACC_RM_ChequeNo").ToString()
                    Else
                        txtChallenNo.Text = ""

                    End If

                    If IsDBNull(dt.Rows(0)("Acc_RM_ChequeDate").ToString()) = False Then
                        If (dt.Rows(0)("Acc_RM_ChequeDate").ToString() <> "") Then
                            If (objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_ChequeDate").ToString(), "D") <> "01/01/1900") Then
                                txtChallenDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_ChequeDate").ToString(), "D")
                            Else
                                txtChallenDate.Text = ""
                            End If
                        Else
                            txtChallenDate.Text = ""
                        End If
                    Else
                        txtChallenDate.Text = ""
                    End If
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_IFSCCode").ToString()) = False Then
                    txtIFSC.Text = dt.Rows(0)("Acc_RM_IFSCCode").ToString()
                Else
                    txtIFSC.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("ACC_RM_BankName").ToString()) = False Then
                    If dt.Rows(0)("ACC_RM_BankName").ToString() > 0 Then
                        ddlBankName.SelectedValue = dt.Rows(0)("ACC_RM_BankName").ToString()
                    Else
                        BindBankName()
                        ddlBankName.SelectedIndex = 0
                    End If
                Else
                    ddlBankName.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BranchName").ToString()) = False Then
                    txtBranchName.Text = dt.Rows(0)("Acc_RM_BranchName").ToString()
                Else
                    txtBranchName.Text = ""
                End If

                If dt.Rows(0)("Acc_RM_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True : imgbtnConfirm.Visible = False
                ElseIf dt.Rows(0)("Acc_RM_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnConfirm.Visible = False
                ElseIf dt.Rows(0)("Acc_RM_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnConfirm.Visible = False
                End If

                If IsDBNull(dt.Rows(0)("acc_RM_InvoiceDate").ToString()) = False Then
                    txtInvoiceDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("acc_RM_InvoiceDate").ToString(), "D")
                Else
                    txtInvoiceDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_PaidAmount").ToString()) = False Then
                    txtPaidAmount.Text = dt.Rows(0)("Acc_RM_PaidAmount").ToString()
                Else
                    txtPaidAmount.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BalanceAmount").ToString()) = False Then
                    If dt.Rows(0)("Acc_RM_BalanceAmount") > 0 Then
                        lblAmount.Text = "Balance Amount " & dt.Rows(0)("Acc_RM_BalanceAmount") & ""
                    ElseIf dt.Rows(0)("Acc_RM_BalanceAmount") < 0 Then
                        sExtraAmount = dt.Rows(0)("Acc_RM_BalanceAmount")
                        If sExtraAmount.StartsWith("-") Then
                            sExtraAmount = sExtraAmount.Remove(0, 1)
                        End If
                        lblAmount.Text = "Extra Amount " & sExtraAmount & ""
                    End If
                End If

                If IsDBNull(dt.Rows(0)("ACC_RM_ZoneID").ToString()) = False Then
                    If (dt.Rows(0)("ACC_RM_ZoneID").ToString() = "") Then
                    Else
                        ddlAccZone.SelectedValue = dt.Rows(0)("ACC_RM_ZoneID").ToString()
                        LoadRegion(ddlAccZone.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("ACC_RM_RegionID").ToString()) = False Then
                    If (dt.Rows(0)("ACC_RM_RegionID").ToString() = "") Then
                    Else
                        ddlAccRgn.SelectedValue = dt.Rows(0)("ACC_RM_RegionID").ToString()
                        LoadArea(ddlAccRgn.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("ACC_RM_AreaID").ToString()) = False Then
                    If (dt.Rows(0)("ACC_RM_AreaID").ToString() = "") Then
                    Else
                        ddlAccArea.SelectedValue = dt.Rows(0)("ACC_RM_AreaID").ToString()
                        LoadAccBrnch(ddlAccArea.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("ACC_RM_BranchID").ToString()) = False Then
                    If (dt.Rows(0)("ACC_RM_BranchID").ToString() = "") Then
                    Else
                        ddlAccBrnch.SelectedValue = dt.Rows(0)("ACC_RM_BranchID").ToString()
                    End If
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_PaymentType").ToString()) = False Then
                    ddlPaymentType.SelectedIndex = dt.Rows(0)("Acc_RM_PaymentType")
                Else
                    ddlPaymentType.SelectedIndex = 0
                End If

                If ddlPaymentType.SelectedIndex = 1 Or ddlPaymentType.SelectedIndex = 4 Then
                    If IsDBNull(dt.Rows(0)("Acc_RM_OrderNO")) = False Then
                        If dt.Rows(0)("Acc_RM_OrderNO") > 0 Then
                            ddlOrderNo.SelectedValue = dt.Rows(0)("Acc_RM_OrderNO")
                        Else
                            LoadExistingPurchaseOrder(ddlPaymentType.SelectedIndex, ddlParty.SelectedValue)
                        End If
                    Else
                        ddlOrderNo.SelectedIndex = 0
                    End If
                Else
                    ddlOrderNo.Items.Clear()
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_OrderDate").ToString()) = False Then
                    If (dt.Rows(0)("Acc_RM_OrderDate").ToString() <> "") Then
                        If (dt.Rows(0)("Acc_RM_OrderDate").ToString() <> "01/01/1900 12:00:00 AM") Then
                            txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_OrderDate").ToString(), "D")
                        Else
                            txtOrderDate.Text = ""
                        End If
                    Else
                        txtOrderDate.Text = ""
                    End If
                Else
                    txtOrderDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("Acc_RM_FETotalAmt").ToString()) = False Then
                    If (dt.Rows(0)("Acc_RM_FETotalAmt").ToString() = "") Then
                    Else
                        lblFEAmt.Visible = True : txtFEAmt.Visible = True
                        txtFEAmt.Text = dt.Rows(0)("Acc_RM_FETotalAmt").ToString()
                    End If
                End If
                If IsDBNull(dt.Rows(0)("Acc_RM_Currency").ToString()) = False Then
                    If (dt.Rows(0)("Acc_RM_Currency").ToString() = "") Then
                    Else
                        If dt.Rows(0)("Acc_RM_Currency").ToString() = 0 Then
                        Else
                            ddlCurrency.SelectedValue = dt.Rows(0)("Acc_RM_Currency").ToString()
                        End If
                    End If
                End If
                If IsDBNull(dt.Rows(0)("Acc_RM_DiffAmount").ToString()) = False Then
                    If (dt.Rows(0)("Acc_RM_DiffAmount").ToString() = "") Then
                    Else
                        lblDiffAmount.Visible = True : txtDiffAmount.Visible = True
                        txtDiffAmount.Text = dt.Rows(0)("Acc_RM_DiffAmount").ToString()
                    End If
                End If
            End If

            dtDetails = objRecp.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iReceipt)
            Session("dtReceipt") = dtDetails
            dgReceiptDetails.DataSource = dtDetails
            dgReceiptDetails.DataBind()

            BindAllAttachments(sSession.AccessCode, iAttachID)

            GetAttachFile(ddlExistReceipt.SelectedItem.Text)
        Catch ex As Exception
            Throw
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

            dt1 = objRecp.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("FilePath") = ""
                    dRow("FileName") = dt1.Rows(i)("pge_Orignalfilename")
                    dRow("Extension") = dt1.Rows(i)("pge_ext")
                    dRow("CreatedOn") = objGen.FormatDtForRDBMS(dt1.Rows(i)("pge_createdon"), "D")
                    dt.Rows.Add(dRow)
                Next
            End If

            gvattach.DataSource = dt
            gvattach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgReceiptDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgReceiptDetails.ItemDataBound
        Dim imgbtnDelete As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDelete = CType(e.Item.FindControl("imgbtnDelete"), ImageButton)
                imgbtnDelete.ImageUrl = "~/Images/Trash16.png"
                If lblStatus.Text = "Waiting for Approval" Then
                    imgbtnDelete.Enabled = True
                ElseIf lblStatus.Text = "Not Started." Then
                    imgbtnDelete.Enabled = True
                Else
                    imgbtnDelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgReceiptDetails_ItemDataBound")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim iReceiptID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim sArray As Array, sTransArray As Array

        Dim sBillNo As String = "", sBillName As String = ""
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim lblPMID As New Label : Dim lblBillNo As New Label

        Dim dDatel, dSDateo As Date
        Dim dGridDebit As Double = 0 : Dim dGridCredit As Double = 0, dSum As Double = 0
        Dim dDate, dSDate As Date : Dim m As Integer, iBaseID As Integer = 0, iGL As Integer = 0, iSubGL As Integer = 0
        Dim sTime As String = ""
        Try
            lblError.Text = ""

            If ddlCustomerParty.SelectedIndex = 1 Then
                lblError.Text = "select " & lblParty.Text & " ."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If

            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date of Receipt (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblPaymentValidataionMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date of Receipt (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblPaymentValidataionMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblPaymentValidataionMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtBillDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblPaymentValidataionMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtBillDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            'If ddlDbOtherSubGL.Items.Count > 1 Then
            '    If ddlDbOtherSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Debit."
            '        Exit Sub
            '    End If
            'End If

            'If ddlCrOtherSubGL.Items.Count > 1 Then
            '    If ddlCrOtherSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Credit."
            '        Exit Sub
            '    End If
            'End If

            If dgReceiptDetails.Items.Count = 0 Then
                lblError.Text = "Add Amount"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Amount','', 'success');", True)
                Exit Sub
            End If

            For i = 0 To dgReceiptDetails.Items.Count - 1
                dGridDebit = dGridDebit + Convert.ToDouble(dgReceiptDetails.Items(i).Cells(9).Text)
                dGridCredit = dGridCredit + Convert.ToDouble(dgReceiptDetails.Items(i).Cells(10).Text)
            Next

            If dGridDebit <> dGridCredit Then
                lblError.Text = "Debit And Credit Amount not matching."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit And Credit Amount not matching','', 'success');", True)
                Exit Sub
            End If
            For i = 0 To dgReceiptDetails.Items.Count - 1
                dGridDebit = Convert.ToDouble(dgReceiptDetails.Items(i).Cells(9).Text)
                dSum = dSum + dGridDebit
            Next
            'If ddlTransType.SelectedItem.Text = "Bank" Then
            '    If txtChequeNo.Text = "" Then
            '        lblError.Text = "Enter Cheque No."
            '        Exit Sub
            '    End If
            '    If txtChequeDate.Text = "" Then
            '        lblError.Text = "Enter Cheque Date."
            '        Exit Sub
            '    End If
            '    If txtIFSC.Text = "" Then
            '        lblError.Text = "Enter IFSC No."
            '        Exit Sub
            '    End If
            '    If ddlBankName.SelectedIndex = 0 Then
            '        lblError.Text = "Select Bank Name."
            '        Exit Sub
            '    End If
            '    'If txtBranchName.Text = "" Then
            '    '    lblError.Text = "Enter Branch Name."
            '    '    Exit Sub
            '    'End If
            'End If

            'Cheque Date Comparision'
            If txtBillDate.Text <> "" Then
                dDatel = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDateo = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim f As Integer
                f = DateDiff(DateInterval.Day, dDatel, dSDateo)
                If f < 0 Then
                    lblError.Text = "Receipt Date (" & txtInvoiceDate.Text & ") should be Greater than or equal to Invoice Date(" & txtBillDate.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Receipt Date (" & txtInvoiceDate.Text & ") should be Greater than or equal to Invoice Date(" & txtBillDate.Text & ").','', 'success');", True)
                    txtBillDate.Focus()
                    Exit Sub
                End If
            End If
            'Cheque Date Comparision'

            'Extra'
            If GVReceipt.Rows.Count > 0 Then
                For i = 0 To GVReceipt.Rows.Count - 1
                    chkField = GVReceipt.Rows(i).FindControl("chkSelectBox")
                    lblPMID = GVReceipt.Rows(i).FindControl("lblSMID")
                    lblBillNo = GVReceipt.Rows(i).FindControl("lblBillNo")

                    If chkField.Checked = True Then
                        sBillNo = sBillNo & "," & lblPMID.Text
                        sBillName = sBillName & "," & lblBillNo.Text
                    End If
                Next
            Else
                sBillNo = ""
            End If
            'Extra'

            If ddlExistReceipt.SelectedIndex > 0 Then
                objRecp.iAcc_RM_ID = ddlExistReceipt.SelectedValue
            Else
                objRecp.iAcc_RM_ID = 0
            End If

            objRecp.sAcc_RM_TransactionNo = txtTransactionNo.Text

            If ddlCustomerParty.SelectedIndex > 0 Then
                objRecp.iAcc_RM_Location = ddlCustomerParty.SelectedIndex
            Else
                objRecp.iAcc_RM_Location = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                objRecp.iAcc_RM_Party = ddlParty.SelectedValue
            Else
                objRecp.iAcc_RM_Party = 0
            End If

            If ddlTransType.SelectedIndex > 0 Then
                objRecp.iAcc_RM_TransactionType = ddlTransType.SelectedIndex
            Else
                objRecp.iAcc_RM_TransactionType = 0
            End If

            If ddlBillType.SelectedIndex > 0 Then
                objRecp.iAcc_RM_BillType = ddlBillType.SelectedValue
            Else
                objRecp.iAcc_RM_BillType = 0
            End If

            If txtBillNo.Text <> "" Then
                objRecp.sAcc_RM_BillNo = txtBillNo.Text
            Else
                objRecp.sAcc_RM_BillNo = ""
            End If
            If txtBillDate.Text <> "" Then
                objRecp.dAcc_RM_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objRecp.dAcc_RM_BillDate = "01/01/1900"
            End If
            If txtBillAmount.Text <> "" Then
                objRecp.dAcc_RM_BillAmount = txtBillAmount.Text
            Else
                objRecp.dAcc_RM_BillAmount = 0
            End If

            objRecp.iAcc_RM_YearID = sSession.YearID
            objRecp.sAcc_RM_Status = "W"
            objRecp.iAcc_RM_CreatedBy = sSession.UserID
            objRecp.sAcc_RM_Operation = "U"
            objRecp.sAcc_RM_IPAddress = sSession.IPAddress
            objRecp.sAcc_RM_BillNarration = txtNarration.Text
            objRecp.dAcc_RM_InvoiceDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)


            If ddlTransType.SelectedIndex = 2 Then 'Cheque Details                
                If txtChequeNo.Text <> "" Then
                    objRecp.sAcc_RM_ChequeNo = txtChequeNo.Text

                    If txtChequeDate.Text = "" Then
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact(txtChequeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    If txtIFSC.Text <> "" Then
                        objRecp.sAcc_RM_IFSCCode = txtIFSC.Text
                    Else
                        objRecp.sAcc_RM_IFSCCode = ""
                    End If

                    If ddlBankName.SelectedIndex > 0 Then
                        objRecp.sAcc_RM_BankName = ddlBankName.SelectedValue
                    Else
                        objRecp.sAcc_RM_BankName = 0
                    End If

                    If txtBranchName.Text <> "" Then
                        objRecp.sAcc_RM_BranchName = txtBranchName.Text
                    Else
                        objRecp.sAcc_RM_BranchName = ""
                    End If
                    objRecp.iAcc_RM_TrTypeDetails = 1
                ElseIf txtFundTransferNo.Text <> "" Then
                    objRecp.sAcc_RM_ChequeNo = txtFundTransferNo.Text

                    If txtFundTransferDate.Text = "" Then
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact(txtFundTransferDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    objRecp.sAcc_RM_IFSCCode = ""
                    objRecp.sAcc_RM_BankName = 0
                    objRecp.sAcc_RM_BranchName = ""
                    objRecp.iAcc_RM_TrTypeDetails = 2
                ElseIf txtChallenNo.Text <> "" Then
                    objRecp.sAcc_RM_ChequeNo = txtChallenNo.Text

                    If txtChallenDate.Text = "" Then
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objRecp.dAcc_RM_ChequeDate = Date.ParseExact(txtChallenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    objRecp.sAcc_RM_IFSCCode = ""
                    objRecp.sAcc_RM_BankName = 0
                    objRecp.sAcc_RM_BranchName = ""
                    objRecp.iAcc_RM_TrTypeDetails = 3
                Else
                    objRecp.sAcc_RM_ChequeNo = ""
                    objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    objRecp.sAcc_RM_IFSCCode = ""
                    objRecp.sAcc_RM_BankName = 0
                    objRecp.sAcc_RM_BranchName = ""
                    objRecp.iAcc_RM_TrTypeDetails = 0
                End If

            Else
                objRecp.sAcc_RM_ChequeNo = ""
                objRecp.dAcc_RM_ChequeDate = "01/01/1900"
                objRecp.sAcc_RM_IFSCCode = ""
                objRecp.sAcc_RM_BankName = "0"
                objRecp.sAcc_RM_BranchName = ""
            End If

            If txtPaidAmount.Text <> "" Then
                objRecp.dAcc_RM_PaidAmount = txtPaidAmount.Text
            Else
                objRecp.dAcc_RM_PaidAmount = 0
            End If

            If sBillNo <> "" Then
                objRecp.dAcc_RM_BalanceAmount = 0
            Else
                If txtBillAmount.Text <> "" Then
                    objRecp.dAcc_RM_BalanceAmount = txtBillAmount.Text - txtPaidAmount.Text
                Else
                    objRecp.dAcc_RM_BalanceAmount = 0 - txtPaidAmount.Text
                End If
            End If

            objRecp.iAcc_RM_AttachID = iAttachID

            objRecp.iACC_RM_ZoneID = ddlAccZone.SelectedValue
            objRecp.iACC_RM_RegionID = ddlAccRgn.SelectedValue
            objRecp.iACC_RM_AreaID = ddlAccArea.SelectedValue
            objRecp.iACC_RM_BranchID = ddlAccBrnch.SelectedValue
            iBaseID = objRecp.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)

            If txtFEAmt.Text <> "" Then
                objRecp.dAcc_RM_FETotalAmt = txtFEAmt.Text.Trim
                objRecp.dAcc_RM_CurrencyAmt = objRecp.GetFECRates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                sTime = objRecp.GetFECTime(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                If sTime <> "" Then
                    objRecp.sAcc_RM_CurrencyTime = sTime
                Else
                    objRecp.sAcc_RM_CurrencyTime = " "
                End If
            Else
                objRecp.dAcc_RM_FETotalAmt = 0
                objRecp.dAcc_RM_CurrencyAmt = 0
                objRecp.sAcc_RM_CurrencyTime = " "
            End If

            If ddlCurrency.SelectedIndex > 0 Then
                objRecp.iAcc_RM_Currency = ddlCurrency.SelectedValue
            Else
                objRecp.iAcc_RM_Currency = 0
            End If
            If txtDiffAmount.Text <> "" Then
                objRecp.dAcc_RM_DiffAmount = txtDiffAmount.Text.Trim
            Else
                objRecp.dAcc_RM_DiffAmount = 0
            End If

            objRecp.iAcc_RM_BatchNo = 0
            objRecp.iAcc_RM_BaseName = 0

            sArray = objRecp.SaveReceiptMaster(sSession.AccessCode, sSession.AccessCodeID, objRecp)
            iReceiptID = sArray(1)

            objRecp.DeleteTransactionsDetails(sSession.AccessCode, sSession.AccessCodeID, 3, iReceiptID)

            For i = 0 To dgReceiptDetails.Items.Count - 1
                objRecp.iATD_TrType = 3
                objRecp.dATD_TransactionDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objRecp.iATD_BillId = iReceiptID
                objRecp.iATD_DbOrCr = dgReceiptDetails.Items(i).Cells(12).Text
                objRecp.iATD_Head = dgReceiptDetails.Items(i).Cells(1).Text
                objRecp.iATD_GL = dgReceiptDetails.Items(i).Cells(2).Text
                objRecp.iATD_SubGL = dgReceiptDetails.Items(i).Cells(3).Text

                If objRecp.iATD_DbOrCr = 1 Then
                    objRecp.dATD_Debit = dgReceiptDetails.Items(i).Cells(9).Text
                    objRecp.dATD_Credit = 0.00

                ElseIf objRecp.iATD_DbOrCr = 2 Then
                    objRecp.dATD_Debit = 0.00
                    objRecp.dATD_Credit = dgReceiptDetails.Items(i).Cells(10).Text
                End If

                objRecp.iATD_CreatedBy = sSession.UserID
                objRecp.sATD_Status = "W"
                objRecp.iATD_YearID = sSession.YearID
                objRecp.sATD_Operation = "U"
                objRecp.sATD_IPAddress = sSession.IPAddress

                objRecp.iATD_ZoneID = ddlAccZone.SelectedValue
                objRecp.iATD_RegionID = ddlAccRgn.SelectedValue
                objRecp.iATD_AreaID = ddlAccArea.SelectedValue
                objRecp.iATD_BranchID = ddlAccBrnch.SelectedValue

                sTransArray = objRecp.SaveTransactionsDetails(sSession.AccessCode, sSession.AccessCodeID, objRecp)
            Next
            If txtDiffAmount.Text <> "" And txtDiffAmount.Text > 0 Then
                If ddlCrOtherGL.SelectedIndex > 0 Then
                    iGL = ddlCrOtherGL.SelectedValue
                End If

                If ddlCrOtherSubGL.SelectedIndex > 0 Then
                    iSubGL = ddlCrOtherSubGL.SelectedValue
                End If
                dtDiff = objRecp.LoadFEDiffDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtDiffAmount.Text.Trim, lblFEStatus.Text.Trim, ddlCrOtherHead.SelectedIndex, iGL, iSubGL)
                If dtDiff.Rows.Count > 0 Then
                    For i = 0 To dtDiff.Rows.Count - 1
                        objRecp.iATD_TrType = 3
                        objRecp.iATD_BillId = iReceiptID
                        objRecp.iATD_PaymentType = 0
                        objRecp.iATD_DbOrCr = dtDiff.Rows(i)("DebitOrCredit")
                        objRecp.iATD_Head = dtDiff.Rows(i)("HeadID")
                        objRecp.iATD_GL = dtDiff.Rows(i)("GLID")
                        objRecp.iATD_SubGL = dtDiff.Rows(i)("SubGLID")
                        If objRecp.iATD_DbOrCr = 1 Then
                            dDebit = dtDiff.Rows(i)("Debit")
                            objRecp.dATD_Debit = dDebit
                            objRecp.dATD_Credit = 0.00
                        Else
                            dCredit = dtDiff.Rows(i)("Credit")
                            objRecp.dATD_Debit = 0.00
                            objRecp.dATD_Credit = dCredit
                        End If

                        objRecp.iATD_CreatedBy = sSession.UserID
                        objRecp.sATD_Status = "W"
                        objRecp.iATD_YearID = sSession.YearID
                        objRecp.sATD_Operation = "C"
                        objRecp.sATD_IPAddress = sSession.IPAddress
                        sTransArray = objRecp.SaveTransactionsDetails(sSession.AccessCode, sSession.AccessCodeID, objRecp)
                        objRecp.UpdateFEKey(sSession.AccessCode, sSession.AccessCodeID, 3, sTransArray(1))
                    Next
                End If
            End If
            lblError.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

            dgReceiptDetails.DataSource = objRecp.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistReceipt.SelectedValue)
            dgReceiptDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Private Sub dgReceiptDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgReceiptDetails.ItemCommand
        Dim dt As New DataTable
        Try
            If e.CommandName = "Delete" Then
                If lblStatus.Text = "Activated" Then
                    lblError.Text = "This Payment has been Approved, you can not delete transactions."
                    Exit Sub
                End If
                dt = Session("dtReceipt")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                If dt.Rows.Count > 0 Then
                    Session("dtReceipt") = dt
                Else
                    Session("dtReceipt") = Nothing
                End If
            End If

            dgReceiptDetails.DataSource = dt
            dgReceiptDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgReceiptDetails_ItemCommand")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim dt As New DataTable
        Try
            txtTransactionNo.Text = objRecp.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

            ddlBillType.SelectedIndex = 0 : ddlExistReceipt.SelectedIndex = 0
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = "" : lblStatus.Text = ""
            lblError.Text = "" : ddlDBOtherHead.SelectedIndex = 0 : ddlCrOtherHead.SelectedIndex = 0
            dgReceiptDetails.DataSource = dt
            dgReceiptDetails.DataBind()
            LoadSubGL()
            ddlDbOtherGL.DataSource = dt
            ddlDbOtherGL.DataBind()

            ddlCrOtherGL.DataSource = dt
            ddlCrOtherGL.DataBind()

            ddlTransType.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0
            ddlBankName.Items.Clear() : txtChequeDate.Text = "" : txtChequeNo.Text = "" : txtBranchName.Text = "" : txtIFSC.Text = ""

            divcollapseChequeDetails.Visible = False
            divcollapseFundDetails.Visible = False
            divcollapseChallenDetails.Visible = False
            'divCollapseOthersDetails.Visible = True

            dgAttach.DataSource = Nothing
            dgAttach.DataBind()
            txtPaidAmount.Text = 0
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False : imgbtnConfirm.Visible = False
            ddlCurrency.Enabled = True : lblFEStat.Visible = False : lblFEStatus.Text = "" : lblFEAmt.Visible = False : txtFEAmt.Visible = False
            lblDiffAmount.Visible = False : txtDiffAmount.Visible = False
            txtFEAmt.Text = "" : txtDiffAmount.Text = ""
            LoadCurrencyType() : GetAppSettings()

            dgReceiptDetails.DataSource = Nothing
            dgReceiptDetails.DataBind()
            ddlPaymentType.SelectedIndex = 0 : ddlParty.Items.Clear() : txtInvoiceDate.Text = "" : lblAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub ddlTransType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransType.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Try
            lblError.Text = ""
            If ddlTransType.SelectedIndex > 0 Then
                ddlDbOtherGL.DataSource = dt
                ddlDbOtherGL.DataBind()

                ddlDbOtherSubGL.DataSource = dt
                ddlDbOtherSubGL.DataBind()

                If ddlTransType.SelectedIndex = 1 Then  'Cash
                    sPerm = objRecp.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Cash", "Cash")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")

                    ddlDBOtherHead.SelectedIndex = sArray1(0)
                    ddlDBOtherHead_SelectedIndexChanged(sender, e)
                    ddlDbOtherGL.SelectedValue = sArray1(3)
                    'If sArray1(4) <> 0 Then
                    '    ddlDbOtherSubGL.SelectedValue = sArray1(4)
                    'End If
                    divcollapseChequeDetails.Visible = False
                    divcollapseFundDetails.Visible = False
                    divcollapseChallenDetails.Visible = False

                ElseIf ddlTransType.SelectedIndex = 2 Then  'Bank
                    sPerm = objRecp.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Bank", "Bank")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")

                    ddlDBOtherHead.SelectedIndex = sArray1(0)
                    ddlDBOtherHead_SelectedIndexChanged(sender, e)
                    ddlDbOtherGL.SelectedValue = sArray1(3)
                    ddlDBOtherGL_SelectedIndexChanged(sender, e)
                    'If sArray1(4) <> 0 Then
                    '    ddlDbOtherSubGL.SelectedValue = sArray1(4)
                    'End If
                    BindBankName()
                    divcollapseChequeDetails.Visible = True
                    divcollapseFundDetails.Visible = True
                    divcollapseChallenDetails.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTransType_SelectedIndexChanged")
        End Try
    End Sub
    Protected Function loaddetails(ByVal sOrderID As String, ByVal ibillNo As String) As Double
        Dim dt As New DataTable
        Dim dt4 As New DataTable
        Dim dt5 As New DataTable
        Dim Duniqe As New DataTable
        Dim dtComp, dtVendor As New DataTable
        Dim dRow As DataRow
        Dim ctin1 As String = "" : Dim Cpan1 As String = "" : Dim Span1 As String = "" : Dim Stin1 As String = "" : Dim company As String = "" : Dim suplierId As String = "" : Dim temp1 As String = ""
        Dim s3, s4, s5, s6, s7, s8, s9, s10, s11, Total, qty, s0 As Double
        Dim Totalamt As String = "" : Dim OrderNo, BillNo As Integer
        Dim TotalinWord As String = "" : Dim PGM_DocumentRefNo As String = "" : Dim PGM_InvoiceDate As String = "" : Dim POM_OrderDate As String = ""
        Dim Totaltax, DiscountAmt, GrandTotal, dimGtotal, CstAmnt, vatAmnt, ExceAmnt As Decimal
        Dim dTotalAmount As Double = 0
        Try
            dt4 = objRecp.loadDetails(sSession.AccessCode, sSession.AccessCodeID, sOrderID, BillNo)
            For i = 0 To dt4.Rows.Count - 1
                If i = dt4.Rows.Count - 1 Then
                    Totalamt = dt4.Rows(i)("TotalAmount")
                    dimGtotal = dt4.Rows(i)("TotalAmount")
                    CstAmnt = CstAmnt + dt4.Rows(i)("CSTAmt")
                    vatAmnt = vatAmnt + dt4.Rows(i)("VATAmt")
                    ExceAmnt = ExceAmnt + dt4.Rows(i)("ExiseAmt")
                    PGM_DocumentRefNo = dt4.Rows(i)("PGM_DocumentRefNo")
                    PGM_InvoiceDate = dt4.Rows(i)("PGM_InvoiceDate")
                    POM_OrderDate = dt4.Rows(i)("POM_OrderDate")
                    ' TotalinWord = NumberToWord(String.Format("{0:0.00}", dt4.Rows(i)("TotalAmount"))) & " Only"
                End If
            Next
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Colour")
            dt.Columns.Add("t0")
            dt.Columns.Add("t3")
            dt.Columns.Add("t4")
            dt.Columns.Add("t5")
            dt.Columns.Add("t6")
            dt.Columns.Add("t7")
            dt.Columns.Add("t8")
            dt.Columns.Add("t9")
            dt.Columns.Add("t10")
            dt.Columns.Add("t11")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("MRP")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("Amount")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("CName")
            dt.Columns.Add("CAdd")
            dt.Columns.Add("CPh")
            dt.Columns.Add("CEmail")
            dt.Columns.Add("Ctin")
            dt.Columns.Add("CPan")
            dt.Columns.Add("InvoiceNO")
            dt.Columns.Add("OrderNo")
            dt.Columns.Add("Saname")
            dt.Columns.Add("Sadd")
            dt.Columns.Add("Sph")
            dt.Columns.Add("SEmail")
            dt.Columns.Add("Stin")
            dt.Columns.Add("SPan")
            dt.Columns.Add("Totalamt")
            dt.Columns.Add("TotalinWord")
            dt.Columns.Add("NetAmnt")
            dt.Columns.Add("GrandTotal")
            dt5 = dt4.Copy
            Duniqe = objRecp.RemoveDublicate(dt5)
            For j = 0 To Duniqe.Rows.Count - 2
                dRow = dt.NewRow()
                qty = 0 : s0 = 0
                Totaltax = 0
                Totalamt = 0
                For i = 0 To dt4.Rows.Count - 2
                    dRow("SlNo") = i + 1
                    If dt4.Rows(i)("Commodity") = "<b>Total</b>" Then
                        dRow("SlNo") = ""
                    End If
                    dRow("Colour") = dt4.Rows(i)("Colour")
                    If (Duniqe.Rows(j)("Description") = dt4.Rows(i)("Description")) Then
                        If (dt4.Rows(i)("Description") <> "<b>Total</b>") Then
                            dRow("Description") = dt4.Rows(i)("Description")
                            dRow("Commodity") = dt4.Rows(i)("Commodity")

                            If ((dt4.Rows(i)("t0") <> 0) And (dt4.Rows(i)("t0").ToString() <> "")) Then
                                s0 = s0 + dt4.Rows(i)("t0")
                                dRow("t0") = s0
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t0"))
                            End If
                            If ((dt4.Rows(i)("t3") <> 0) And (dt4.Rows(i)("t3").ToString() <> "")) Then

                                dRow("t3") = dt4.Rows(i)("t3")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t3"))
                            End If

                            If ((dt4.Rows(i)("t4") <> 0 And dt4.Rows(i)("t4") <> "")) Then
                                dRow("t4") = dt4.Rows(i)("t4")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t4"))
                            End If

                            If ((dt4.Rows(i)("t5") <> 0 And dt4.Rows(i)("t5") <> "")) Then
                                dRow("t5") = dt4.Rows(i)("t5")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t5"))
                            End If

                            If ((dt4.Rows(i)("t6") <> 0 And dt4.Rows(i)("t6") <> "")) Then
                                dRow("t6") = dt4.Rows(i)("t6")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t6"))
                            End If

                            If ((dt4.Rows(i)("t7") <> 0 And dt4.Rows(i)("t7") <> "")) Then
                                dRow("t7") = dt4.Rows(i)("t7")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t7"))
                            End If

                            If ((dt4.Rows(i)("t8") <> 0 And dt4.Rows(i)("t8") <> "")) Then
                                dRow("t8") = dt4.Rows(i)("t8")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t8"))
                            End If

                            If ((dt4.Rows(i)("t9") <> 0 And dt4.Rows(i)("t9") <> "")) Then
                                dRow("t9") = dt4.Rows(i)("t9")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t9"))
                            End If
                            If ((dt4.Rows(i)("t10") <> 0 And dt4.Rows(i)("t10") <> "")) Then
                                dRow("t10") = dt4.Rows(i)("t10")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t10"))
                            End If

                            If ((dt4.Rows(i)("t11") <> 0 And dt4.Rows(i)("t11") <> "")) Then
                                dRow("t11") = dt4.Rows(i)("t11")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t11"))
                            End If
                            Totaltax = Totaltax + Convert.ToDecimal(dt4.Rows(i)("VATAmt")) + Convert.ToDecimal(dt4.Rows(i)("CSTAmt")) + Convert.ToDecimal(dt4.Rows(i)("ExiseAmt"))
                            dRow("MRP") = dt4.Rows(i)("Rate")
                            dRow("Rate") = dt4.Rows(i)("Rate")
                            DiscountAmt = DiscountAmt + Convert.ToDecimal(dt4.Rows(i)("DiscountAmt"))
                            Totalamt = Convert.ToDecimal(dRow("Rate")) * qty
                            dRow("CSTAmt") = CstAmnt
                            dRow("ExiseAmt") = ExceAmnt
                            dRow("VATAmt") = vatAmnt
                            dRow("NetAmnt") = dimGtotal - (vatAmnt + ExceAmnt + CstAmnt)
                        End If
                    End If

                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(Totalamt))
                    dRow("TotalQty") = qty
                    dRow("DiscountAmt") = DiscountAmt
                    dRow("Discount") = dt4.Rows(i)("Discount")
                    dRow("VAT") = dt4.Rows(i)("VAT")
                    dRow("Exise") = dt4.Rows(i)("Exise")
                    dRow("Ctin") = ctin1
                    dRow("CPan") = Cpan1
                    dRow("Totalamt") = "Total Net Amount  Rs  " & Totalamt
                    dRow("TotalinWord") = TotalinWord
                    dRow("NetAmnt") = dimGtotal - (vatAmnt + ExceAmnt + CstAmnt)
                    dRow("GrandTotal") = dimGtotal
                Next
                GrandTotal = GrandTotal + Totalamt
                dt.Rows.Add(dRow)
            Next

            If dt.Rows.Count > 0 Then
                dTotalAmount = dt.Rows(0)("GrandTotal").ToString()
                Return Convert.ToDecimal(dTotalAmount).ToString("#,##0.00")
            Else
                Return dTotalAmount
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Dim i As Integer = 0
        Dim sBillNo As String = ""
        Dim sBillName As String = ""
        Try
            If ddlParty.SelectedIndex > 0 Then
                chkBillNo.DataSource = objRecp.LoadBillNo(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue, ddlPaymentType.SelectedIndex)
                chkBillNo.DataTextField = "BillNo"
                chkBillNo.DataValueField = "BillID"
                chkBillNo.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalBillList').modal('show');", True)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub

    Private Sub btnOkBill_Click(sender As Object, e As EventArgs) Handles btnOkBill.Click
        Dim i As Integer = 0, iDiff As Integer = 0
        Dim sBillNo As String = "", sBillName As String = "", sBillID As String = ""
        Dim dt As New DataTable
        Try
            For i = 0 To chkBillNo.Items.Count - 1
                If chkBillNo.Items(i).Selected = True Then
                    sBillNo = sBillNo & "," & chkBillNo.Items(i).Value
                    sBillName = sBillName & "," & chkBillNo.Items(i).Text
                End If
            Next
            If sBillNo <> "" Then
                If sBillNo.StartsWith(",") Then
                    sBillID = sBillNo.Remove(0, 1)
                End If
                iDiff = objRecp.GetCurrency(sSession.AccessCode, sSession.AccessCodeID, sBillID, ddlCurrency.SelectedValue)
                If iDiff = 0 Then
                    imgbtnConfirm.Visible = False : imgbtnSave.Visible = False
                    lblError.Text = "Currency does not match with the Purchase Order.So, Difference can not be find out."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Currency does not match with the Purchase Order.So, Difference can not be find out.','', 'warning');", True)
                    Exit Sub
                End If
                lblAmount.Text = "Paid Amount for this invoice is " & objRecp.GetPaidAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillName)
                txtBillNo.Text = sBillName.Remove(0, 1)
                txtBillAmount.Text = objRecp.GetBillAmount(sSession.AccessCode, sSession.AccessCodeID, sBillNo, sBillName)
                txtPaidAmount_TextChanged(sender, e)

                If ddlPaymentType.SelectedIndex = 1 Then
                    LoadExistingPurchaseOrder(ddlPaymentType.SelectedIndex, ddlParty.SelectedValue)
                    ddlOrderNo.SelectedValue = objRecp.GetOrderNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo)

                    dt = objRecp.GetOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("SPO_OrderDate").ToString()) = False Then
                            If (dt.Rows(0)("SPO_OrderDate").ToString() <> "") Then
                                If (dt.Rows(0)("SPO_OrderDate").ToString() <> "01/01/1990 12:00:00 AM") Then
                                    txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SPO_OrderDate").ToString(), "D")
                                Else
                                    txtOrderDate.Text = ""
                                End If
                            Else
                                txtOrderDate.Text = ""
                            End If
                        Else
                            txtOrderDate.Text = ""
                        End If
                    End If
                End If

            Else
                txtBillAmount.Text = ""
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalBillList').modal('hide');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOkBill_Click")
        End Try
    End Sub

    'Private Sub txtBillAmount_TextChanged(sender As Object, e As EventArgs) Handles txtBillAmount.TextChanged
    '    Dim dtCOA As New DataTable
    '    Dim iGL As Integer = 0, iSubGL As Integer = 0
    '    Try
    '        If txtBillAmount.Text <> "" Then

    '            dtCOA = objRecp.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

    '            'Debit
    '            If ddldbGL.SelectedIndex > 0 Then
    '                iGL = ddldbGL.SelectedValue
    '            Else
    '                iGL = 0
    '            End If

    '            If ddldbsUbGL.SelectedIndex > 0 Then
    '                iSubGL = ddldbsUbGL.SelectedValue
    '            Else
    '                iSubGL = 0
    '            End If

    '            dtReceipt = objRecp.LoadReceipts(sSession.AccessCode, sSession.AccessCodeID, ddldbHead.SelectedIndex, iGL, iSubGL, txtPaidAmount.Text, 1, dtReceipt, dtCOA)

    '            'Credit
    '            If ddlCrGL.SelectedIndex > 0 Then
    '                iGL = ddlCrGL.SelectedValue
    '            Else
    '                iGL = 0
    '            End If

    '            If ddlCrSubGL.SelectedIndex > 0 Then
    '                iSubGL = ddlCrSubGL.SelectedValue
    '            Else
    '                iSubGL = 0
    '            End If
    '            dtReceipt = objRecp.LoadReceipts(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedIndex, iGL, iSubGL, txtPaidAmount.Text, 2, dtReceipt, dtCOA)

    '            Session("dtReceipt") = dtReceipt
    '            dgReceiptDetails.DataSource = dtReceipt
    '            dgReceiptDetails.DataBind()
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtBillAmount_TextChanged")
    '    End Try
    'End Sub

    Private Sub ddlDBOtherHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDBOtherHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlDBOtherHead.SelectedIndex > 0 Then
                ddlDBOtherGL.DataSource = objRecp.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDBOtherHead.SelectedIndex)
                ddlDBOtherGL.DataTextField = "GlDesc"
                ddlDBOtherGL.DataValueField = "gl_Id"
                ddlDBOtherGL.DataBind()
                ddlDBOtherGL.Items.Insert(0, "Select GL Code")
            Else
                ddlDBOtherGL.DataSource = dt
                ddlDBOtherGL.DataBind()

                'ddlDBOtherSubGL.DataSource = dt
                'ddlDBOtherSubGL.DataBind()
                LoadSubGL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDBOtherHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCrOtherHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCrOtherHead.SelectedIndex > 0 Then
                ddlCrOtherGL.DataSource = objRecp.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherHead.SelectedIndex)
                ddlCrOtherGL.DataTextField = "GlDesc"
                ddlCrOtherGL.DataValueField = "gl_Id"
                ddlCrOtherGL.DataBind()
                ddlCrOtherGL.Items.Insert(0, "Select GL Code")
            Else
                ddlCrOtherGL.DataSource = dt
                ddlCrOtherGL.DataBind()

                'ddlCrOtherSubGL.DataSource = dt
                'ddlCrOtherSubGL.DataBind()
                LoadSubGL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrOtherHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlDBOtherGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDBOtherGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlDBOtherGL.SelectedIndex > 0 Then
                ddlDBOtherSubGL.DataSource = objRecp.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDBOtherGL.SelectedValue)
                ddlDBOtherSubGL.DataTextField = "GlDesc"
                ddlDBOtherSubGL.DataValueField = "gl_Id"
                ddlDBOtherSubGL.DataBind()
                ddlDBOtherSubGL.Items.Insert(0, "Select SubGL Code")
            Else
                ddlDBOtherSubGL.DataSource = dt
                ddlDBOtherSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDBOtherGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlDBOtherSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDBOtherSubGL.SelectedIndexChanged
        Dim iHead As Integer
        Try
            If ddlDBOtherSubGL.SelectedIndex > 0 Then
                iHead = objRecp.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlDBOtherSubGL.SelectedValue)
                ddlDBOtherGL.DataSource = objRecp.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead)
                ddlDBOtherGL.DataTextField = "GlDesc"
                ddlDBOtherGL.DataValueField = "gl_Id"
                ddlDBOtherGL.DataBind()
                ddlDBOtherGL.Items.Insert(0, "Select GL Code")

                ddlDBOtherGL.SelectedValue = objRecp.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddlDBOtherSubGL.SelectedValue)
                ddlDBOtherHead.SelectedIndex = iHead
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDBOtherSubGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCrOtherGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherGL.SelectedIndexChanged  'ok
        Dim dt As New DataTable
        Try
            If ddlCrOtherGL.SelectedIndex > 0 Then
                ddlCrOtherSubGL.DataSource = objRecp.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherGL.SelectedValue)
                ddlCrOtherSubGL.DataTextField = "GlDesc"
                ddlCrOtherSubGL.DataValueField = "gl_Id"
                ddlCrOtherSubGL.DataBind()
                ddlCrOtherSubGL.Items.Insert(0, "Select SubGL Code")
            Else
                ddlCrOtherSubGL.DataSource = dt
                ddlCrOtherSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrOtherGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCrOtherSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherSubGL.SelectedIndexChanged
        Dim iHead As Integer
        Try
            If ddlCrOtherSubGL.SelectedIndex > 0 Then
                iHead = objRecp.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherSubGL.SelectedValue)
                ddlCrOtherGL.DataSource = objRecp.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead)
                ddlCrOtherGL.DataTextField = "GlDesc"
                ddlCrOtherGL.DataValueField = "gl_Id"
                ddlCrOtherGL.DataBind()
                ddlCrOtherGL.Items.Insert(0, "Select GL Code")

                ddlCrOtherGL.SelectedValue = objRecp.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherSubGL.SelectedValue)
                ddlCrOtherHead.SelectedIndex = iHead
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrOtherSubGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub btnAddDebit_Click(sender As Object, e As EventArgs) Handles btnAddDebit.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dDebit As Double = 0
        Dim dtDetails As New DataTable
        Try
            'If ddlDbOtherSubGL.Items.Count > 1 Then
            '    If ddlDbOtherSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Debit."
            '        Exit Sub
            '    End If
            'End If

            If IsNothing(Session("dtReceipt")) Then
                dtReceipt = dtDetails
            Else
                dtReceipt = Session("dtReceipt")
            End If

            dtCOA = objRecp.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

            'Debit
            If ddlDbOtherGL.SelectedIndex > 0 Then
                iGL = ddlDbOtherGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlDbOtherSubGL.SelectedIndex > 0 Then
                iSubGL = ddlDbOtherSubGL.SelectedValue
            Else
                iSubGL = 0
            End If

            If txtOtherDebit.Text <> "" Then
                dDebit = txtOtherDebit.Text
            Else
                dDebit = 0.00
            End If

            dtReceipt = objRecp.LoadReceipts(sSession.AccessCode, sSession.AccessCodeID, ddlDBOtherHead.SelectedIndex, iGL, iSubGL, dDebit, 1, dtReceipt, dtCOA)
            Session("dtReceipt") = dtReceipt

            dgReceiptDetails.DataSource = dtReceipt
            dgReceiptDetails.DataBind()

            LoadSubGL()
            ddlDBOtherHead.SelectedIndex = 0 : ddlDbOtherGL.Items.Clear() : ddlDbOtherSubGL.SelectedIndex = 0 : txtOtherDebit.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDebit_Click")
        End Try
    End Sub
    Private Sub btnAddCredit_Click(sender As Object, e As EventArgs) Handles btnAddCredit.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dCredit As Double = 0
        Dim dtDetails As New DataTable
        Try
            'If ddlCrOtherSubGL.Items.Count > 1 Then
            '    If ddlCrOtherSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Credit."
            '        Exit Sub
            '    End If
            'End If

            If IsNothing(Session("dtReceipt")) Then
                dtReceipt = dtDetails
            Else
                dtReceipt = Session("dtReceipt")
            End If
            dtCOA = objRecp.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

            'Debit
            If ddlCrOtherGL.SelectedIndex > 0 Then
                iGL = ddlCrOtherGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlCrOtherSubGL.SelectedIndex > 0 Then
                iSubGL = ddlCrOtherSubGL.SelectedValue
            Else
                iSubGL = 0
            End If

            If txtOtherCredit.Text <> "" Then
                dCredit = txtOtherCredit.Text
            Else
                dCredit = 0.00
            End If

            dtReceipt = objRecp.LoadReceipts(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherHead.SelectedIndex, iGL, iSubGL, dCredit, 2, dtReceipt, dtCOA)

            Session("dtReceipt") = dtReceipt
            dgReceiptDetails.DataSource = dtReceipt
            dgReceiptDetails.DataBind()

            LoadSubGL()
            ddlCrOtherHead.SelectedIndex = 0 : ddlCrOtherGL.Items.Clear() : ddlCrOtherSubGL.SelectedIndex = 0 : txtOtherCredit.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddCredit_Click")
        End Try
    End Sub
    Private Sub GVReceipt_PreRender(sender As Object, e As EventArgs) Handles GVReceipt.PreRender
        Try
            If GVReceipt.Rows.Count > 0 Then
                GVReceipt.UseAccessibleHeader = True
                GVReceipt.HeaderRow.TableSection = TableRowSection.TableHeader
                GVReceipt.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVReceipt_PreRender")
        End Try
    End Sub

    Private Sub btnAddBillAmount_Click(sender As Object, e As EventArgs) Handles btnAddBillAmount.Click
        Dim i As Integer = 0, iDiff As Integer = 0
        Dim sBillNo As String = "", sBillName As String = "", sBill As String = ""

        Dim chkSelectBox As New CheckBox, chkAll As New CheckBox
        Dim lblSMID As New Label : Dim lblBillNo As New Label
        Try
            lblError.Text = ""

            For i = 0 To GVReceipt.Rows.Count - 1
                chkSelectBox = GVReceipt.Rows(i).FindControl("chkSelectBox")
                lblSMID = GVReceipt.Rows(i).FindControl("lblSMID")
                lblBillNo = GVReceipt.Rows(i).FindControl("lblBillNo")

                If chkSelectBox.Checked = True Then
                    sBillNo = sBillNo & "," & lblSMID.Text
                    sBillName = sBillName & "," & lblBillNo.Text
                End If
                txtBillIDs.Text = sBillNo
            Next

            If sBillNo <> "" Then
                sBill = sBillNo
                If sBill.StartsWith(",") Then
                    sBill = sBill.Remove(0, 1)
                End If
                iDiff = objRecp.GetCurrency(sSession.AccessCode, sSession.AccessCodeID, sBill, ddlCurrency.SelectedValue)
                If iDiff = 0 Then
                    imgbtnConfirm.Visible = False : imgbtnSave.Visible = False
                    lblError.Text = "Currency does not match with the Sales Order.So, Difference can not be find out."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Currency does not match with the Sales Order.So, Difference can not be find out.','', 'success');", True)
                    Exit Sub
                End If
                txtBillNo.Text = sBillName.Remove(0, 1)
                txtBillAmount.Text = objRecp.GetDerivedBillAmount(sSession.AccessCode, sSession.AccessCodeID, sBillNo, sBillName)
                txtPaidAmount_TextChanged(sender, e)
            Else
                txtBillAmount.Text = ""
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddBillAmount_Click")
        End Try
    End Sub
    Public Function MakePayment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal dPaidAmount As Double, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtFromTable As New DataTable
        Dim dPaidAmt As Double
        Dim str As String
        Dim UpdateAmount As Double

        Dim sBillNo As String = "", sBillName As String = ""
        Dim chk As New CheckBox, chkAll As New CheckBox
        Dim lblSMID As New Label : Dim lblBillNo As New Label
        Try
            sBillNo = txtBillIDs.Text

            If sBillNo <> "" Then
                dPaidAmt = dPaidAmount
                dtFromTable = objRecp.DataFromPurchase(sNameSpace, iCompID, iYearID, sBillNo)
                For i = 0 To dtFromTable.Rows.Count - 1
                    dt = objRecp.DataFromPurchaseDetails(sNameSpace, iCompID, iYearID, dtFromTable.Rows(i)("Acc_Sales_ID"))
                    For j = 0 To dt.Rows.Count - 1
                        If (dt.Rows(j)("Acc_SMD_PendingAmount") > dPaidAmt) Then
                            UpdateAmount = dt.Rows(j)("Acc_SMD_PendingAmount") - dPaidAmt
                            str = objRecp.UpdateEditedData(sNameSpace, iCompID, dt.Rows(j)("Acc_SMD_ID"), dtFromTable.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                            dPaidAmt = 0
                        ElseIf (dt.Rows(j)("Acc_SMD_PendingAmount") < dPaidAmt) Then
                            dPaidAmt = dPaidAmt - dt.Rows(j)("Acc_SMD_PendingAmount")
                            UpdateAmount = 0
                            str = objRecp.UpdateEditedData(sNameSpace, iCompID, dt.Rows(j)("Acc_SMD_ID"), dtFromTable.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                        Else
                            UpdateAmount = 0
                            str = objRecp.UpdateEditedData(sNameSpace, iCompID, dt.Rows(j)("Acc_SMD_ID"), dtFromTable.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                        End If
                    Next
                Next

                Dim dtNextInvoice, dtNextRows As New DataTable
                'If Rows are there after selected invoice no'
                If dPaidAmt > 0 Then
                    dtNextInvoice = objRecp.DataFromNextPurchaseInvoice(sNameSpace, iCompID, iYearID, sBillNo, iPartyID)
                    For i = 0 To dtNextInvoice.Rows.Count - 1
                        dtNextRows = objRecp.DataFromPurchaseDetails(sNameSpace, iCompID, iYearID, dtNextInvoice.Rows(i)("Acc_Sales_ID"))
                        For j = 0 To dtNextRows.Rows.Count - 1
                            If (dtNextRows.Rows(j)("Acc_SMD_PendingAmount") > dPaidAmt) Then
                                UpdateAmount = dtNextRows.Rows(j)("Acc_SMD_PendingAmount") - dPaidAmt
                                str = objRecp.UpdateEditedData(sNameSpace, iCompID, dtNextRows.Rows(j)("Acc_SMD_ID"), dtNextInvoice.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                                dPaidAmt = 0
                            ElseIf (dtNextRows.Rows(j)("Acc_SMD_PendingAmount") < dPaidAmt) Then
                                dPaidAmt = dPaidAmt - dtNextRows.Rows(j)("Acc_SMD_PendingAmount")
                                UpdateAmount = 0
                                str = objRecp.UpdateEditedData(sNameSpace, iCompID, dtNextRows.Rows(j)("Acc_SMD_ID"), dtNextInvoice.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                            Else
                                UpdateAmount = 0
                                str = objRecp.UpdateEditedData(sNameSpace, iCompID, dtNextRows.Rows(j)("Acc_SMD_ID"), dtNextInvoice.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                            End If
                        Next
                    Next
                End If
                'If Rows are there after selected invoice no'
            End If

            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MakePayment")
        End Try
    End Function
    Private Sub txtPaidAmount_TextChanged(sender As Object, e As EventArgs) Handles txtPaidAmount.TextChanged
        Dim dtCOA As New DataTable
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Try
            lblError.Text = ""
            'If ddlDbOtherSubGL.Items.Count > 1 Then
            '    If ddlDbOtherSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Debit."
            '        txtPaidAmount.Text = ""
            '        Exit Sub
            '    End If
            'End If

            'If ddlCrOtherSubGL.Items.Count > 1 Then
            '    If ddlCrOtherSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Credit."
            '        txtPaidAmount.Text = ""
            '        Exit Sub
            '    End If
            'End If
            If txtPaidAmount.Text <> 0 Then
                If dgReceiptDetails.Columns.Count = 0 Then
                    If ddlDbOtherSubGL.Items.Count > 1 Then
                        If ddlDbOtherSubGL.SelectedIndex > 0 Then
                        Else
                            lblError.Text = "Select the Sub General Ledger for Debit."
                            txtPaidAmount.Text = ""
                            Exit Sub
                        End If
                    End If

                    If ddlCrOtherSubGL.Items.Count > 1 Then
                        If ddlCrOtherSubGL.SelectedIndex > 0 Then
                        Else
                            lblError.Text = "Select the Sub General Ledger for Credit."
                            txtPaidAmount.Text = ""
                            Exit Sub
                        End If
                    End If
                End If
            End If


            ' If txtPaidAmount.Text <> "" Then
            If txtPaidAmount.Text <> 0 Then
                dtCOA = objRecp.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

                'Debit
                If ddlDbOtherGL.SelectedIndex > 0 Then
                    iGL = ddlDbOtherGL.SelectedValue
                Else
                    iGL = 0
                End If

                If ddlDbOtherSubGL.SelectedIndex > 0 Then
                    iSubGL = ddlDbOtherSubGL.SelectedValue
                Else
                    iSubGL = 0
                End If

                dtReceipt = objRecp.LoadReceipts(sSession.AccessCode, sSession.AccessCodeID, ddlDBOtherHead.SelectedIndex, iGL, iSubGL, txtPaidAmount.Text, 1, dtReceipt, dtCOA)

                'Credit
                If ddlCrOtherGL.SelectedIndex > 0 Then
                    iGL = ddlCrOtherGL.SelectedValue
                Else
                    iGL = 0
                End If

                If ddlCrOtherSubGL.SelectedIndex > 0 Then
                    iSubGL = ddlCrOtherSubGL.SelectedValue
                Else
                    iSubGL = 0
                End If
                dtReceipt = objRecp.LoadReceipts(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherHead.SelectedIndex, iGL, iSubGL, txtPaidAmount.Text, 2, dtReceipt, dtCOA)

                Session("dtReceipt") = dtReceipt
                dgReceiptDetails.DataSource = dtReceipt
                dgReceiptDetails.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtPaidAmount_TextChanged")
        End Try
    End Sub
    Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            dgAttach.CurrentPageIndex = 0
            ds = objRecp.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
            If ds.Tables(0).Rows.Count > dgAttach.PageSize Then
                dgAttach.AllowPaging = True
            Else
                dgAttach.AllowPaging = False
            End If
            If ds.Tables(0).Rows.Count > 0 Then
                divcollapseAttachments.Visible = True
            Else
                divcollapseAttachments.Visible = False
            End If
            dgAttach.PageSize = 1000
            dgAttach.DataSource = ds
            dgAttach.DataBind()
            lblBadgeCount.Text = dgAttach.Items.Count
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Dim sTempPath As String = ""
        Dim lSize As Long
        Dim dt As New DataTable
        Try
            lblMsg.Text = ""
            dtAttach.Columns.Add("FilePath")
            dtAttach.Columns.Add("FileName")
            lblError.Text = "" : iDocID = 0

            Dim hfc As HttpFileCollection = Request.Files
            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        lSize = CType(hpf.ContentLength, Integer)
                        If (sSession.FileSize * 1024 * 1024) < lSize Then
                            lblMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                            Exit Sub
                        End If
                        dRow = dtAttach.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")

                        If sTempPath.EndsWith("\") = True Then
                            sTempPath = sTempPath & "Temp\Attachment\"
                        Else
                            sTempPath = sTempPath & "Temp\Attachment\"
                        End If

                        objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sTempPath)
                        'objclsGeneralFunctions.ClearBrowseDirectory(sTempPath)
                        hpf.SaveAs(sTempPath & sFilesNames)
                        dRow("FilePath") = sTempPath & sFilesNames
                        dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName) & "." & System.IO.Path.GetExtension(hpf.FileName)
                        If System.IO.File.Exists(dRow("FilePath")) = True Then
                            iAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, dRow("FilePath"), sSession.UserID, iAttachID)
                            If iAttachID > 0 Then
                                BindAllAttachments(sSession.AccessCode, iAttachID)
                            End If
                        Else
                            lblMsg.Text = "No file to Attach."
                        End If
                        dtAttach.Rows.Add(dRow)
                    End If
                Next
            End If
            If dtAttach.Rows.Count = 0 Then
                lblMsg.Text = "No file to Attach."
            End If

            dtAttach = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
            Throw
        End Try
    End Sub
    Private Sub dgAttach_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAttach.ItemDataBound
        Dim lblExt As New Label, lblFile As New Label
        Dim File As New LinkButton
        Dim imgbtnView As New ImageButton, imgbtnAdd As New ImageButton, imgbtnDownload As New ImageButton, imgbtnRemove As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnView = CType(e.Item.FindControl("imgbtnView"), ImageButton)
                imgbtnView.ImageUrl = "~/Images/View16.png"
                imgbtnAdd = CType(e.Item.FindControl("imgbtnAdd"), ImageButton)
                imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
                imgbtnDownload = CType(e.Item.FindControl("imgbtnDownload"), ImageButton)
                imgbtnDownload.ImageUrl = "~/Images/Download16.png"
                imgbtnRemove = CType(e.Item.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                lblFile = CType(e.Item.FindControl("lblFile"), Label)
                File = CType(e.Item.FindControl("File"), LinkButton)
                lblExt = CType(e.Item.FindControl("lblExt"), Label)
                lblExt.Text = UCase(lblExt.Text)

                dgAttach.Columns(4).Visible = False : dgAttach.Columns(6).Visible = False : dgAttach.Columns(7).Visible = False

                If sINWDownload = "YES" Then
                    dgAttach.Columns(6).Visible = True
                End If

                If sINWView = "YES" Then
                    dgAttach.Columns(4).Visible = True
                End If

                If sWFDelete = "YES" Then
                    dgAttach.Columns(7).Visible = True
                End If

                If (lblExt.Text = "JPG" Or lblExt.Text = "JPEG" Or lblExt.Text = "BMP" Or lblExt.Text = "GIF" Or lblExt.Text = "BRK" Or lblExt.Text = "CAL" Or lblExt.Text = "PDF" Or
                    lblExt.Text = "CLP" Or lblExt.Text = "DCX" Or lblExt.Text = "EPS" Or lblExt.Text = "ICO" Or lblExt.Text = "IFF" Or lblExt.Text = "IMT" Or
                    lblExt.Text = "ICA" Or lblExt.Text = "PCT" Or lblExt.Text = "PCX" Or lblExt.Text = "PNG" Or lblExt.Text = "PSD" Or lblExt.Text = "RAS" Or
                    lblExt.Text = "SGI" Or lblExt.Text = "TGA" Or lblExt.Text = "XBM" Or lblExt.Text = "XPM" Or lblExt.Text = "XWD" Or lblExt.Text = "TIF" Or lblExt.Text = "TIFF" Or lblExt.Text = "TXT") Then
                    imgbtnView.Enabled = True
                    lblFile.Visible = True
                    File.Visible = False
                Else
                    imgbtnView.Enabled = False
                    lblFile.Visible = False
                    File.Visible = True
                End If

                If (lblExt.Text = "JPG" Or lblExt.Text = "JPEG" Or lblExt.Text = "BMP" Or lblExt.Text = "GIF" Or lblExt.Text = "PNG") Then
                    lblFile.Visible = True : File.Visible = False
                Else
                    lblFile.Visible = False : File.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemDataBound")
        End Try
    End Sub
    Private Sub dgAttach_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAttach.ItemCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Dim sExtn As String = ""
        Try
            lblError.Text = "" : lblMsg.Text = ""

            If e.CommandName = "OPENPAGE" Or e.CommandName = "VIEW" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                'sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "TempPath")
                sPaths = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")
                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Temp\Attachment\"
                Else
                    sPaths = sPaths & "\Temp\Attachment\"
                End If
                If e.CommandName = "VIEW" Then
                    Dim oImgFilePath As New Object, oDocumentID As New Object, oFileID As New Object, oInwrdID As Object, oStatus As Object, oBackToFormID As Object

                    sDestFilePath = objclsAttachments.GetOriginalDocumentPathNew(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                    oImgFilePath = HttpUtility.UrlEncode(objGen.EncryptQueryString(sDestFilePath))
                    oDocumentID = HttpUtility.UrlEncode(objGen.EncryptQueryString(iAttachID))
                    oFileID = HttpUtility.UrlEncode(objGen.EncryptQueryString(iDocID))
                    'oInwrdID = HttpUtility.UrlDecode(objclsEdictGeneral.EncryptQueryString(iInwardID))
                    oStatus = HttpUtility.UrlDecode(objGen.EncryptQueryString(iStatus))
                    oBackToFormID = HttpUtility.UrlDecode(objGen.EncryptQueryString(2))

                    'SelId ---> InwardID
                    'SelectedChecksIDs ---> Status

                    'Response.Redirect(String.Format("~/VSAnnotation/AnnotationDemo.aspx?SelId={0}&SelectedChecksIDs={1}&DocumentID={2}&FileID={3}&ImgFilePath={4}&BackToFormID={5}", oInwrdID, oStatus, oDocumentID, oFileID, oImgFilePath, oBackToFormID), False)

                    sExtn = objRecp.GetExtension(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                    'Response.Redirect(String.Format("~/ViewAttachment/View.aspx?ImgFilePath={0}&sExtn={1}&AttachID={2}&DocID={3}", oImgFilePath, sExtn, iAttachID, iDocID), False)
                    ViewContent(sDestFilePath, sExtn)

                ElseIf e.CommandName = "OPENPAGE" Then
                    sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                    DownloadMyFile(sDestFilePath)
                End If
            End If
            If e.CommandName = "REMOVE" Then
                txtDescription.Text = ""
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                BindAllAttachments(sSession.AccessCode, iAttachID)
            End If
            If e.CommandName = "ADDDESC" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                lblFDescription = e.Item.FindControl("lblFDescription")
                lblHeadingDescription.Visible = True : txtDescription.Text = "" : txtDescription.Visible = True : btnAddDesc.Visible = True
                txtDescription.Text = lblFDescription.Text
                txtDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemCommand")
        End Try
    End Sub
    Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnAddDesc_Click(sender As Object, e As EventArgs) Handles btnAddDesc.Click
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If txtDescription.Text.Trim.Length > 1000 Then
                lblMsg.Text = "Description exceeded maximum size(max 1000 characters)."
                txtDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                Exit Try
            End If
            objclsAttachments.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID, Replace(txtDescription.Text.Trim, "'", "`"))
            lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
            iDocID = 0
            BindAllAttachments(sSession.AccessCode, iAttachID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
        Catch ex As Exception
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDesc_Click")
        End Try
    End Sub
    Public Sub ViewContent(ByVal sPath As String, ByVal sExtn As String)
        Try
            If UCase(sExtn) = "JPG" Or UCase(sExtn) = "PNG" Or UCase(sExtn) = "JPEG" Or UCase(sExtn) = "GIF" Or UCase(sExtn) = "BMP" Then
                Dim bytes As Byte() = System.IO.File.ReadAllBytes(sPath)
                Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                imgView.ImageUrl = imageDataURL1
            Else
                'imgView.Visible = False
                imgView.ImageUrl = "~/Images/NoImage.jpg"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub txtBillAmount_TextChanged(sender As Object, e As EventArgs) Handles txtBillAmount.TextChanged
        Try
            If ddlDbOtherSubGL.Items.Count > 1 Then
                If ddlDbOtherSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Debit."
                    txtBillAmount.Text = ""
                    Exit Sub
                End If
            End If

            If ddlCrOtherSubGL.Items.Count > 1 Then
                If ddlCrOtherSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Credit."
                    txtBillAmount.Text = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtBillAmount_TextChanged")
        End Try
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Dim dtParty As New DataTable
        Dim chkField As New CheckBox
        Try
            lblError.Text = ""
            DivBillManual.Visible = True
            dtParty = objRecp.CheckBillForParty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue, ddlPaymentType.SelectedIndex)
            If dtParty.Rows.Count > 0 Then
                DivBill.Visible = False : DivBillManual.Visible = True
                GVReceipt.DataSource = Nothing
                GVReceipt.DataBind()

                For iIndx = 0 To GVBillManual.Rows.Count - 1
                    chkField = GVBillManual.Rows(iIndx).FindControl("chkSelectBox")
                    chkField.Checked = False
                Next
                GVBillManual.DataSource = objRecp.BindManualBillDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                GVBillManual.DataBind()
            Else
                DivBill.Visible = False : DivBillManual.Visible = False
                GVReceipt.DataSource = Nothing
                GVReceipt.DataBind()

                GVBillManual.DataSource = Nothing
                GVBillManual.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnYes_Click")
        End Try
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Dim dtParty As New DataTable
        Dim chkField As New CheckBox
        Try
            lblError.Text = ""
            DivBill.Visible = True
            dtParty = objRecp.CheckBillForParty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue, ddlPaymentType.SelectedIndex)
            If dtParty.Rows.Count > 0 Then
                DivBill.Visible = True : DivBillManual.Visible = False
                GVBillManual.DataSource = Nothing
                GVBillManual.DataBind()

                For iIndx = 0 To GVReceipt.Rows.Count - 1
                    chkField = GVReceipt.Rows(iIndx).FindControl("chkSelectBox")
                    chkField.Checked = False
                Next
                GVReceipt.DataSource = objRecp.BindBillDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                GVReceipt.DataBind()
            Else
                DivBill.Visible = False : DivBillManual.Visible = False
                GVReceipt.DataSource = Nothing
                GVReceipt.DataBind()

                GVBillManual.DataSource = Nothing
                GVBillManual.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNo_Click")
        End Try
    End Sub

    Private Sub GVBillManual_PreRender(sender As Object, e As EventArgs) Handles GVBillManual.PreRender
        Try
            If GVBillManual.Rows.Count > 0 Then
                GVBillManual.UseAccessibleHeader = True
                GVBillManual.HeaderRow.TableSection = TableRowSection.TableHeader
                GVBillManual.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVBillManual_PreRender")
        End Try
    End Sub

    Private Sub btnAddManualBillAMT_Click(sender As Object, e As EventArgs) Handles btnAddManualBillAMT.Click
        Dim i As Integer = 0
        Dim sBillNo As String = "", sBillName As String = ""

        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim lblSMID As New Label : Dim lblBillNo As New Label
        Dim txtAmountPaid As New TextBox
        Dim dAmountPaid As Double = 0 : Dim dTotalAmountPaid As Double = 0
        Try
            lblError.Text = ""

            For i = 0 To GVBillManual.Rows.Count - 1
                chkField = GVBillManual.Rows(i).FindControl("chkSelect")
                lblSMID = GVBillManual.Rows(i).FindControl("lblSMID")
                lblBillNo = GVBillManual.Rows(i).FindControl("lblBillNo")
                txtAmountPaid = GVBillManual.Rows(i).FindControl("txtAmountPaid")

                If txtAmountPaid.Text <> "" Then
                    sBillNo = sBillNo & "," & lblSMID.Text
                    sBillName = sBillName & "," & lblBillNo.Text

                    dAmountPaid = txtAmountPaid.Text
                    dTotalAmountPaid = dTotalAmountPaid + dAmountPaid
                End If
                dAmountPaid = 0
            Next

            If sBillNo <> "" Then
                txtBillNo.Text = sBillName.Remove(0, 1)
                txtPaidAmount.Text = dTotalAmountPaid
                txtPaidAmount_TextChanged(sender, e)
            Else
                txtBillAmount.Text = ""
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddManualBillAMT_Click")
        End Try
    End Sub
    Public Function MakeManualPayment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal dPaidAmount As Double) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtFromTable As New DataTable
        Dim dPaidAmt As Double
        Dim str As String
        Dim UpdateAmount As Double

        Dim sBillNo As String = "", sBillName As String = ""
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim lblSMID As New Label : Dim lblBillNo As New Label
        Dim txtAmountPaid As New TextBox
        Try

            If GVBillManual.Rows.Count > 0 Then
                For k = 0 To GVBillManual.Rows.Count - 1
                    chkField = GVBillManual.Rows(k).FindControl("chkSelect")
                    lblSMID = GVBillManual.Rows(k).FindControl("lblSMID")
                    lblBillNo = GVBillManual.Rows(k).FindControl("lblBillNo")
                    txtAmountPaid = GVBillManual.Rows(k).FindControl("txtAmountPaid")

                    If txtAmountPaid.Text <> "" Then
                        'sBillNo = sBillNo & "," & lblPMID.Text
                        'sBillName = sBillName & "," & lblBillNo.Text
                        sBillNo = "," & lblSMID.Text
                        sBillName = lblBillNo.Text

                        If sBillNo <> "" Then
                            dPaidAmt = dPaidAmount
                            dtFromTable = objRecp.DataFromPurchase(sNameSpace, iCompID, iYearID, sBillNo)
                            For i = 0 To dtFromTable.Rows.Count - 1
                                dt = objRecp.DataFromPurchaseDetails(sNameSpace, iCompID, iYearID, dtFromTable.Rows(i)("Acc_Sales_ID"))
                                For j = 0 To dt.Rows.Count - 1
                                    If (dt.Rows(j)("Acc_SMD_PendingAmount") > txtAmountPaid.Text) Then
                                        UpdateAmount = dt.Rows(j)("Acc_SMD_PendingAmount") - txtAmountPaid.Text
                                        str = objRecp.UpdateEditedData(sNameSpace, iCompID, dt.Rows(j)("Acc_SMD_ID"), dtFromTable.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                                        txtAmountPaid.Text = 0
                                        'ElseIf (dt.Rows(j)("Acc_SMD_PendingAmount") < dPaidAmt) Then
                                        '    dPaidAmt = dPaidAmt - dt.Rows(j)("Acc_SMD_PendingAmount")
                                        '    UpdateAmount = 0
                                        '    str = objRecp.UpdateEditedData(sNameSpace, iCompID, dt.Rows(j)("Acc_SMD_ID"), dtFromTable.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                                        'Else
                                        '    UpdateAmount = 0
                                        '    str = objRecp.UpdateEditedData(sNameSpace, iCompID, dt.Rows(j)("Acc_SMD_ID"), dtFromTable.Rows(i)("Acc_Sales_ID"), UpdateAmount)
                                    End If
                                Next
                            Next

                        End If

                    End If
                Next
            Else
                sBillNo = ""
            End If

            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MakeManualPayment")
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
    Public Sub OrderVisibleTrue()
        Try
            lblOrderNo.Visible = True : lblorderDate.Visible = True
            ddlOrderNo.Visible = True : txtOrderDate.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub OrderVisibleFalse()
        Try
            lblOrderNo.Visible = False : lblorderDate.Visible = False
            ddlOrderNo.Visible = False : txtOrderDate.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub BillNoVisibleTrue()
        Try
            lblBillNo.Visible = True : txtBillNo.Visible = True : imgbtnSearch.Visible = True : lblBillDate.Visible = True : txtBillDate.Visible = True
            lblBillAmount.Visible = True : txtBillAmount.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BillNoVisibleFalse()
        Try
            lblBillNo.Visible = False : txtBillNo.Visible = False : imgbtnSearch.Visible = False : lblBillDate.Visible = False : txtBillDate.Visible = False
            lblBillAmount.Visible = False : txtBillAmount.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentType.SelectedIndexChanged
        OrderVisibleFalse() : BillNoVisibleFalse()
        If ddlPaymentType.SelectedIndex = 1 Then    'With Inventory Advance
            BillNoVisibleTrue()
            OrderVisibleTrue()
        ElseIf ddlPaymentType.SelectedIndex = 2 Then    'Without Inventory Advance
            BillNoVisibleTrue()
        ElseIf ddlPaymentType.SelectedIndex = 3 Then    'General
            'InvoiceVisibleTrue()
            BillNoVisibleTrue()
            imgbtnSearch.Visible = False
        ElseIf ddlPaymentType.SelectedIndex = 4 Then    'With Inventory receipt
            BillNoVisibleTrue()
        ElseIf ddlPaymentType.SelectedIndex = 5 Then    'Without Inventory receipt
            BillNoVisibleTrue()
        Else
            OrderVisibleFalse() : BillNoVisibleFalse()
        End If
    End Sub
    'Public Sub InvoiceVisibleTrue()
    '    Try
    '        lblInvNo.Visible = True
    '        txtInvoice.Visible = True : lblInvDate.Visible = True : txtInvDate.Visible = True
    '        lblInvAmount.Visible = True : txtInvAmount.Visible = True
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Sub InvoiceVisibleFalse()
    '    Try
    '        lblInvNo.Visible = False : txtInvoice.Visible = False : lblInvDate.Visible = False : txtInvDate.Visible = False
    '        lblInvAmount.Visible = False : txtInvAmount.Visible = False
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub ddlOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrderNo.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlOrderNo.SelectedIndex > 0 Then
                dt = objRecp.GetOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("SPO_OrderDate").ToString()) = False Then
                        If (dt.Rows(0)("SPO_OrderDate").ToString() <> "") Then
                            If (dt.Rows(0)("SPO_OrderDate").ToString() <> "01/01/1990 12:00:00 AM") Then
                                txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SPO_OrderDate").ToString(), "D")
                            Else
                                txtOrderDate.Text = ""
                            End If
                        Else
                            txtOrderDate.Text = ""
                        End If
                    Else
                        txtOrderDate.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
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

            If ddlExistReceipt.SelectedIndex = 0 Then
                lblError.Text = "Select Existing Receipt No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlExistReceipt.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlExistReceipt.SelectedItem.Text)
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
                            objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objGen.SafeSQL(txtTitle.Text.Trim)
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
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
                            objIndex.sPGEKeyWORD = objGen.SafeSQL(sKeywords)
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
                            objIndex.spgeOrignalFileName = objGen.SafeSQL(sFileName)
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
                        lblPaymentValidataionMsg.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)

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
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
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

            If ddlExistReceipt.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Existing Receipt No."
                ddlExistReceipt.Focus()
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
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
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
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
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
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim iCabinetID, iSubCabinetID, iFolderID As Integer
        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Dim dt As New DataTable
        Try
            If ddlExistReceipt.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Payment Voucher")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlExistReceipt.SelectedItem.Text)

                    dt = objRecp.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
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
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                iParent = objRecp.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                ddlAccArea.SelectedValue = iParent
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iParent = objRecp.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                ddlAccRgn.SelectedValue = iParent
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iParent = objRecp.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                ddlAccZone.SelectedValue = iParent
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
                iBaseID = objRecp.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
                If ddlCurrency.SelectedValue = iBaseID Then
                Else
                    iCurrID = objRecp.GetFEID(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                    If iCurrID = 0 Then
                        lblError.Text = "Please set the exchange rates in Currency Master."
                        lblPaymentValidataionMsg.Text = "Please set the exchange rates in Currency Master."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCurrency_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub imgbtnConfirm_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnConfirm.Click
    '    Dim dGridDebit As Double = 0, dGridCredit As Double = 0, dSum As Double = 0, dSDebit As Double = 0, dTotal As Double = 0, dDiff As Double = 0
    '    Dim iBaseID As Integer = 0
    '    Dim chkSelectBox As New CheckBox
    '    Dim lblSMID As New Label, lblBillNo As New Label

    '    Dim txtAmountPaid As New TextBox
    '    Dim BillNo As String = "", sBillID As String = "", sBillIDs As String = ""
    '    Dim aBill() As String
    '    Try
    '        lblError.Text = ""
    '        lblFEAmt.Visible = False : txtFEAmt.Visible = False : lblDiffAmount.Visible = False : txtDiffAmount.Visible = False
    '        For i = 0 To dgReceiptDetails.Items.Count - 1
    '            dGridDebit = dGridDebit + Convert.ToDouble(dgReceiptDetails.Items(i).Cells(9).Text)
    '            dGridCredit = dGridCredit + Convert.ToDouble(dgReceiptDetails.Items(i).Cells(10).Text)
    '        Next

    '        If dGridDebit <> dGridCredit Then
    '            lblError.Text = "Debit And Credit Amount not matching."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit And Credit Amount not matching','', 'success');", True)
    '            Exit Sub
    '        End If
    '        For i = 0 To dgReceiptDetails.Items.Count - 1
    '            dGridDebit = Convert.ToDouble(dgReceiptDetails.Items(i).Cells(9).Text)
    '            dSum = dSum + dGridDebit
    '        Next
    '        lblFEAmt.Visible = True : txtFEAmt.Visible = True
    '        iBaseID = objRecp.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
    '        If ddlCurrency.SelectedIndex > 0 Then
    '            If ddlCurrency.SelectedValue = iBaseID Then
    '                txtFEAmt.Text = dSum
    '            Else
    '                txtFEAmt.Text = objRecp.GetFERates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue, objGen.SafeSQL(dSum))
    '            End If
    '        End If
    '        If txtBillNo.Text <> "" Then
    '            If txtBillNo.Text.ToString.Contains(",") = True Then
    '                BillNo = txtBillNo.Text.ToString()
    '                aBill = BillNo.Split(",")
    '                For i = 0 To aBill.Length - 1
    '                    If aBill(i).Contains("SI-") = True Then
    '                        sBillID = aBill(i).Remove(0, 3)
    '                    End If
    '                    sBillIDs = sBillIDs & "," & sBillID
    '                Next
    '                If sBillIDs.StartsWith(",") Then
    '                    sBillID = sBillID.Remove(0, 1)
    '                End If
    '            Else
    '                If txtBillNo.Text.ToString.Contains("SI-") = True Then
    '                    BillNo = txtBillNo.Text.ToString()
    '                    sBillID = BillNo.Remove(0, 3)
    '                End If
    '            End If
    '        End If

    '        lblDiffAmount.Visible = True : txtDiffAmount.Visible = True
    '        If sBillID = "" Then
    '            txtDiffAmount.Text = 0
    '        Else
    '            dTotal = objRecp.GetSum(sSession.AccessCode, sSession.AccessCodeID, sBillID)
    '            dDiff = dTotal - txtFEAmt.Text

    '            lblFEStat.Visible = True
    '            If dDiff > 0 Then
    '                txtDiffAmount.Text = Math.Round(dDiff, 2, MidpointRounding.AwayFromZero)
    '                lblFEStatus.Text = "Gain"
    '            Else
    '                dDiff = txtFEAmt.Text - dTotal
    '                txtDiffAmount.Text = Math.Round(dDiff, 2, MidpointRounding.AwayFromZero)
    '                lblFEStatus.Text = "Loss"
    '            End If
    '        End If
    '        If lblStatus.Text = "Not Started." Then
    '            imgbtnSave.Visible = True : imgbtnConfirm.Visible = False
    '        Else
    '            imgbtnSave.Visible = False : imgbtnConfirm.Visible = False : imgbtnUpdate.Visible = True
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnConfirm_Click")
    '    End Try
    'End Sub
    Private Sub imgbtnConfirm_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnConfirm.Click
        Dim dGridDebit As Double = 0, dGridCredit As Double = 0, dSum As Double = 0, dSDebit As Double = 0, dTotal As Double = 0, dDiff As Double = 0
        Dim iBaseID As Integer = 0
        Dim chkSelectBox As New CheckBox
        Dim lblSMID As New Label, lblBillNo As New Label

        Dim txtAmountPaid As New TextBox
        Dim BillNo As String = "", sBillID As String = "", sBillIDs As String = ""
        Dim aBill() As String
        Try
            lblError.Text = ""

            If ddlCustomerParty.SelectedIndex = 1 Then
                lblError.Text = "select " & lblParty.Text & " ."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If

            lblFEAmt.Visible = False : txtFEAmt.Visible = False : lblDiffAmount.Visible = False : txtDiffAmount.Visible = False
            For i = 0 To dgReceiptDetails.Items.Count - 1
                dGridDebit = dGridDebit + Convert.ToDouble(dgReceiptDetails.Items(i).Cells(9).Text)
                dGridCredit = dGridCredit + Convert.ToDouble(dgReceiptDetails.Items(i).Cells(10).Text)
            Next

            If dGridDebit <> dGridCredit Then
                lblError.Text = "Debit And Credit Amount not matching."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit And Credit Amount not matching','', 'success');", True)
                Exit Sub
            End If
            For i = 0 To dgReceiptDetails.Items.Count - 1
                dGridDebit = Convert.ToDouble(dgReceiptDetails.Items(i).Cells(9).Text)
                dSum = dSum + dGridDebit
            Next
            lblFEAmt.Visible = True : txtFEAmt.Visible = True
            iBaseID = objRecp.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
            If ddlCurrency.SelectedIndex > 0 Then
                If ddlCurrency.SelectedValue = iBaseID Then
                    txtFEAmt.Text = dSum
                Else
                    txtFEAmt.Text = objRecp.GetFERates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue, objGen.SafeSQL(dSum))
                End If
            End If
            If txtFEAmt.Text <> 0 Then
                txtPaidAmount.Text = txtFEAmt.Text
                'txtPaidAmount_TextChanged(sender, e) commented by vijayalakshmi 09-09-2020 to avoid auto fetching ledger details to grid
            End If
            If txtBillNo.Text <> "" Then
                If txtBillNo.Text.ToString.Contains(",") = True Then
                    BillNo = txtBillNo.Text.ToString()
                    aBill = BillNo.Split(",")
                    For i = 0 To aBill.Length - 1
                        If aBill(i).Contains("SI-") = True Then
                            sBillID = aBill(i).Remove(0, 3)
                        End If
                        sBillIDs = sBillIDs & "," & sBillID
                    Next
                    If sBillIDs.StartsWith(",") Then
                        sBillID = sBillID.Remove(0, 1)
                    End If
                Else
                    If txtBillNo.Text.ToString.Contains("SI-") = True Then
                        BillNo = txtBillNo.Text.ToString()
                        sBillID = BillNo.Remove(0, 3)
                    End If
                End If
            End If

            lblDiffAmount.Visible = True : txtDiffAmount.Visible = True
            If sBillID = "" Then
                txtDiffAmount.Text = 0
            Else
                dTotal = objRecp.GetSum(sSession.AccessCode, sSession.AccessCodeID, sBillID)
                dDiff = dTotal - txtFEAmt.Text

                lblFEStat.Visible = True
                If dDiff > 0 Then
                    txtDiffAmount.Text = Math.Round(dDiff, 2, MidpointRounding.AwayFromZero)
                    If txtDiffAmount.Text = 0 Then
                        lblFEStatus.Text = ""
                    Else
                        lblFEStatus.Text = "Gain"
                    End If
                Else
                    dDiff = txtFEAmt.Text - dTotal
                    txtDiffAmount.Text = Math.Round(dDiff, 2, MidpointRounding.AwayFromZero)
                    If txtDiffAmount.Text = 0 Then
                        lblFEStatus.Text = ""
                    Else
                        lblFEStatus.Text = "Loss"
                    End If
                End If
            End If
            If lblStatus.Text = "Not Started." Then
                imgbtnSave.Visible = True : imgbtnConfirm.Visible = False
            Else
                imgbtnSave.Visible = False : imgbtnConfirm.Visible = False : imgbtnUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnConfirm_Click")
        End Try
    End Sub
    'Create Debit GL
    Private Sub imgbtnDrOtherGL_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDrOtherGL.Click

        Try

            lblErrorGl.Text = "" : btnDescDeActivate.Visible = False
            txtCode.Text = "" : txtName.Text = "" : txtGlDesc.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)

        Catch ex As Exception
            Throw
        End Try
    End Sub


    Private Sub ddlHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHead.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = "" : lblErrorGl.Text = ""
            ddlGroup.SelectedIndex = -1 : ddlSubGroup.SelectedIndex = -1 : txtCode.Text = "" : txtName.Text = "" : txtGlDesc.Text = ""
            btnDescUpdate.Visible = False : btnDescDeActivate.Visible = False
            If ddlHead.SelectedIndex > 0 Then
                ddlGroup.DataSource = objRecp.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedIndex)
                ddlGroup.DataTextField = "GlDesc"
                ddlGroup.DataValueField = "gl_Id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorGl.Text = "Select Head."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlHead_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroup.SelectedIndexChanged
        Try
            lblErrorGl.Text = "" : lblModelError.Text = ""
            ddlSubGroup.SelectedIndex = -1 : txtCode.Text = "" : txtName.Text = "" : txtGlDesc.Text = ""
            If ddlHead.SelectedIndex > 0 Then
                ddlSubGroup.DataSource = objRecp.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)
                ddlSubGroup.DataTextField = "GlDesc"
                ddlSubGroup.DataValueField = "gl_Id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select SubGroup")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorGl.Text = "Select Head."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Try
            lblErrorGl.Text = "" : lblModelError.Text = ""
            txtCode.Text = "" : txtName.Text = "" : txtGlDesc.Text = "" : lblModalGlId.Text = ""
            If (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex > 0) And (ddlSubGroup.SelectedIndex > 0) Then
                txtCode.Text = objCOA.GenerateGLCode(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedIndex, ddlSubGroup.SelectedValue)
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorGl.Text = "Select Sub Group."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click
        Dim iRet As Integer = 0
        Dim sRet As String
        Dim sArray As Array
        Try
            lblErrorGl.Text = ""
            If ddlHead.SelectedIndex > 0 Then
                objCOA.igl_head = 0
                objCOA.igl_Parent = 0
            End If

            If ddlGroup.SelectedIndex > 0 Then
                objCOA.igl_head = 1
                objCOA.igl_Parent = ddlGroup.SelectedValue
            End If

            If ddlSubGroup.SelectedIndex > 0 Then
                objCOA.igl_head = 2
                objCOA.igl_Parent = ddlSubGroup.SelectedValue
            End If

            objCOA.igl_id = 0
            objCOA.sgl_glcode = txtCode.Text
            objCOA.sgl_Desc = objGen.SafeSQL(txtName.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescription.Text)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = ddlHead.SelectedIndex
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "W"
            objCOA.sgl_IPAddress = sSession.IPAddress
            sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            sArray = sRet.Split(",")
            lblModalGlId.Text = sArray(0)
            If sArray(1) = 0 Then
                lblErrorGl.Text = "Successfully Saved and Waiting for Approval."
                btnDescSave.Visible = False
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            ElseIf sArray(1) = 1 Then
                lblErrorGl.Text = "The Name( " & txtName.Text & ") Already Exists. Enter New Name"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtName.Text = "" : txtDescription.Text = ""
                Exit Sub
            End If

            If objCOA.igl_head = 2 Then
                ddlDbOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                ddlDbOtherGL.DataTextField = "Description"
                ddlDbOtherGL.DataValueField = "gl_id"
                ddlDbOtherGL.DataBind()
                ddlDbOtherGL.Items.Insert(0, "Select GL")
                ddlDBOtherHead.SelectedIndex = ddlHead.SelectedIndex
                'ddlDbOtherGL.SelectedValue = sArray(0)
                'ddlDbOtherGL_SelectedIndexChanged(sender, e)
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub btnDescUpdate_Click(sender As Object, e As EventArgs) Handles btnDescUpdate.Click
        Try
            lblErrorGl.Text = ""
            If ddlHead.SelectedIndex = 0 Or ddlHead.SelectedIndex = -1 Then
                lblErrorGl.Text = "Select Head"
                Exit Sub
            End If
            If ddlGroup.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalGlId.Text
            End If

            If ddlSubGroup.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalGlId.Text
            End If



            objCOA.sgl_Desc = objGen.SafeSQL(txtName.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescription.Text)
            objCOA.igl_UpdatedBy = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_IPAddress = sSession.IPAddress
            objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)

            lblErrorGl.Text = "Successfully Updated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            If ddlSubGroup.SelectedValue > 0 Then
                ddlDbOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                ddlDbOtherGL.DataTextField = "Description"
                ddlDbOtherGL.DataValueField = "gl_id"
                ddlDbOtherGL.DataBind()
                ddlDbOtherGL.Items.Insert(0, "Select GL")
                ddlDBOtherHead.SelectedIndex = ddlHead.SelectedIndex
                ddlDbOtherGL.SelectedValue = objCOA.igl_id
                ddlDBOtherGL_SelectedIndexChanged(sender, e)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescUpdate_Click")
        End Try
    End Sub
    Private Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            lblModelError.Text = ""
            lblErrorGl.Text = "" : ddlHead.SelectedIndex = -1 : ddlGroup.SelectedIndex = -1 : btnDescDeActivate.Visible = False
            ddlSubGroup.SelectedIndex = -1 : txtCode.Text = "" : txtName.Text = "" : txtGlDesc.Text = ""
            btnDescDeActivate.Visible = False : btnDescActivate.Visible = True : btnDescUpdate.Visible = False : btnDescSave.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNew_Click")
        End Try
    End Sub

    Private Sub btnDescActivate_Click(sender As Object, e As EventArgs) Handles btnDescActivate.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Dim iGLShrt As Integer = 0
        Try
            lblErrorGl.Text = ""
            If ddlSubGroup.SelectedIndex > 0 Then
                iGlID = ddlSubGroup.SelectedValue
            End If

            iGLShrt = Convert.ToInt32(lblModalGlId.Text)

            objCOA.ActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGLShrt)
            lblErrorGl.Text = "Successfully Activated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            ' If objCOA.igl_head = 2 Then
            ddlDbOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
            ddlDbOtherGL.DataTextField = "Description"
            ddlDbOtherGL.DataValueField = "gl_id"
            ddlDbOtherGL.DataBind()
            ddlDbOtherGL.Items.Insert(0, "Select GL")
            ddlDBOtherHead.SelectedIndex = ddlHead.SelectedIndex
            ddlDbOtherGL.SelectedValue = iGLShrt
            ddlDBOtherGL_SelectedIndexChanged(sender, e)
            ' End If

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGLShrt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCode.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescription.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    btnDescSave.Visible = False : btnDescDeActivate.Visible = False : btnDescActivate.Visible = True
                    btnDescUpdate.Visible = True


                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    btnDescSave.Visible = False : imgbtnUpdate.Visible = False : btnDescActivate.Visible = True : btnDescDeActivate.Visible = False
                    btnDescUpdate.Visible = False

                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    btnDescSave.Visible = False : btnDescDeActivate.Visible = True : btnDescActivate.Visible = False

                    btnDescUpdate.Visible = True
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDsceActivate_Click")
        End Try
    End Sub

    Private Sub btnDescDeActivate_Click(sender As Object, e As EventArgs) Handles btnDescDeActivate.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Dim iGLShrt As Integer = 0
        Try
            lblErrorGl.Text = ""

            If ddlSubGroup.SelectedIndex > 0 Then
                iGlID = ddlSubGroup.SelectedValue
            End If
            iGLShrt = Convert.ToInt32(lblModalGlId.Text)

            objCOA.DeActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGLShrt)
            lblErrorGl.Text = "Successfully De-Activated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)


            ddlDbOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
            ddlDbOtherGL.DataTextField = "Description"
            ddlDbOtherGL.DataValueField = "gl_id"
            ddlDbOtherGL.DataBind()
            ddlDbOtherGL.Items.Insert(0, "Select GL")
            ddlDBOtherHead.SelectedIndex = ddlHead.SelectedIndex

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGLShrt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCode.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescription.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    btnDescSave.Visible = False : btnDescDeActivate.Visible = False : btnDescActivate.Visible = True
                    btnDescUpdate.Visible = True

                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    btnDescSave.Visible = False : btnDescActivate.Visible = True : btnDescDeActivate.Visible = False
                    btnDescUpdate.Visible = False

                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    btnDescSave.Visible = False : btnDescActivate.Visible = False : btnDescDeActivate.Visible = True
                    btnDescUpdate.Visible = True
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescDeActivate")
        End Try
    End Sub

    'Create Credit GL
    Private Sub imgbtnCrOtherGL_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCrOtherGL.Click
        Try

            lblErrorGl.Text = "" : btnDescDeActivateCRGL.Visible = False
            txtCode.Text = "" : txtName.Text = "" : txtGlDesc.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlCRGLHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCRGLHead.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = "" : lblErrorCRGl.Text = ""
            ddlCRGLGroup.SelectedIndex = -1 : ddlCRGLSubGroup.SelectedIndex = -1 : txtCRGLCode.Text = "" : txtCRGLName.Text = "" : txtCRGlDesc.Text = ""
            btnDescUpdateCRGL.Visible = False : btnDescDeActivateCRGL.Visible = False
            If ddlCRGLHead.SelectedIndex > 0 Then
                ddlCRGLGroup.DataSource = objRecp.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLHead.SelectedIndex)
                ddlCRGLGroup.DataTextField = "GlDesc"
                ddlCRGLGroup.DataValueField = "gl_Id"
                ddlCRGLGroup.DataBind()
                ddlCRGLGroup.Items.Insert(0, "Select Group")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorCRGl.Text = "Select Head."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCRGLHead_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlCRGLGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCRGLGroup.SelectedIndexChanged
        Try
            lblErrorCRGl.Text = "" : lblModelError.Text = ""
            ddlCRGLSubGroup.SelectedIndex = -1 : txtCRGLCode.Text = "" : txtCRGLName.Text = "" : txtCRGlDesc.Text = ""
            If ddlCRGLHead.SelectedIndex > 0 Then
                ddlCRGLSubGroup.DataSource = objRecp.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLGroup.SelectedValue)
                ddlCRGLSubGroup.DataTextField = "GlDesc"
                ddlCRGLSubGroup.DataValueField = "gl_Id"
                ddlCRGLSubGroup.DataBind()
                ddlCRGLSubGroup.Items.Insert(0, "Select SubGroup")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorCRGl.Text = "Select Head."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCRGLGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCRGLSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCRGLSubGroup.SelectedIndexChanged
        Try
            lblErrorCRGl.Text = "" : lblModelError.Text = ""
            txtCRGLCode.Text = "" : txtCRGLName.Text = "" : txtCRGlDesc.Text = "" : lblModalGlId.Text = ""
            If (ddlCRGLHead.SelectedIndex > 0) And (ddlCRGLGroup.SelectedIndex > 0) And (ddlCRGLSubGroup.SelectedIndex > 0) Then
                txtCRGLCode.Text = objCOA.GenerateGLCode(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLHead.SelectedIndex, ddlCRGLSubGroup.SelectedValue)
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorCRGl.Text = "Select Sub Group."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCRGLGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub btnDescSaveCRGL_Click(sender As Object, e As EventArgs) Handles btnDescSaveCRGL.Click
        Dim iRet As Integer = 0
        Dim sRet As String
        Dim sArray As Array
        Try
            lblModalCRGlId.Text = ""
            If ddlCRGLHead.SelectedIndex > 0 Then
                objCOA.igl_head = 0
                objCOA.igl_Parent = 0
            End If

            If ddlCRGLGroup.SelectedIndex > 0 Then
                objCOA.igl_head = 1
                objCOA.igl_Parent = ddlCRGLGroup.SelectedValue
            End If

            If ddlCRGLSubGroup.SelectedIndex > 0 Then
                objCOA.igl_head = 2
                objCOA.igl_Parent = ddlCRGLSubGroup.SelectedValue
            End If

            objCOA.igl_id = 0
            objCOA.sgl_glcode = txtCRGLCode.Text
            objCOA.sgl_Desc = objGen.SafeSQL(txtCRGLName.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescription.Text)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = ddlCRGLHead.SelectedIndex
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "W"
            objCOA.sgl_IPAddress = sSession.IPAddress
            sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            sArray = sRet.Split(",")
            lblModalCRGlId.Text = sArray(0)
            If sArray(1) = 0 Then
                lblErrorCRGl.Text = "Successfully Saved and Waiting for Approval."
                btnDescSaveCRGL.Visible = False
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            ElseIf sArray(1) = 1 Then
                lblErrorCRGl.Text = "The Name( " & txtCRGLName.Text & ") Already Exists. Enter New Name"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
                txtCRGLName.Text = "" : txtDescription.Text = ""
                Exit Sub
            End If

            If objCOA.igl_head = 2 Then
                ddlCrOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLSubGroup.SelectedValue)
                ddlCrOtherGL.DataTextField = "Description"
                ddlCrOtherGL.DataValueField = "gl_id"
                ddlCrOtherGL.DataBind()
                ddlCrOtherGL.Items.Insert(0, "Select GL")
                ddlCrOtherHead.SelectedIndex = ddlCRGLHead.SelectedIndex
                'ddlCrOtherGL.SelectedValue = sArray(0)
                'ddlCrOtherGL_SelectedIndexChanged(sender, e)
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub btnDescUpdateCRGL_Click(sender As Object, e As EventArgs) Handles btnDescUpdateCRGL.Click
        Try
            lblErrorCRGl.Text = ""
            If ddlCRGLHead.SelectedIndex = 0 Or ddlCRGLHead.SelectedIndex = -1 Then
                lblErrorCRGl.Text = "Select Head"
                Exit Sub
            End If
            If ddlCRGLGroup.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalCRGlId.Text
            End If

            If ddlCRGLSubGroup.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalCRGlId.Text
            End If



            objCOA.sgl_Desc = objGen.SafeSQL(txtCRGLName.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescription.Text)
            objCOA.igl_UpdatedBy = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_IPAddress = sSession.IPAddress
            objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)

            lblErrorCRGl.Text = "Successfully Updated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            If ddlCRGLSubGroup.SelectedValue > 0 Then
                ddlCrOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLSubGroup.SelectedValue)
                ddlCrOtherGL.DataTextField = "Description"
                ddlCrOtherGL.DataValueField = "gl_id"
                ddlCrOtherGL.DataBind()
                ddlCrOtherGL.Items.Insert(0, "Select GL")
                ddlCrOtherHead.SelectedIndex = ddlCRGLHead.SelectedIndex
                ddlCrOtherGL.SelectedValue = objCOA.igl_id
                ddlCrOtherGL_SelectedIndexChanged(sender, e)
                LoadParty(ddlCustomerParty.SelectedIndex)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescUpdateCRGL_Click")
        End Try
    End Sub
    Private Sub btnDescNewCRGL_Click(sender As Object, e As EventArgs) Handles btnDescNewCRGL.Click
        Try
            lblModelError.Text = ""
            lblErrorCRGl.Text = "" : ddlCRGLHead.SelectedIndex = -1 : ddlCRGLGroup.SelectedIndex = -1 : btnDescDeActivateCRGL.Visible = False
            ddlCRGLSubGroup.SelectedIndex = -1 : txtCRGLCode.Text = "" : txtCRGLName.Text = "" : txtCRGlDesc.Text = ""
            btnDescDeActivateCRGL.Visible = False : btnDescActivateCRGL.Visible = True : btnDescUpdateCRGL.Visible = False : btnDescSaveCRGL.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNewCRGL_Click")
        End Try
    End Sub

    Private Sub btnDescActivateCRGL_Click(sender As Object, e As EventArgs) Handles btnDescActivateCRGL.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Dim iGLShrt As Integer = 0
        Try
            lblErrorCRGl.Text = ""
            If ddlCRGLSubGroup.SelectedIndex > 0 Then
                iGlID = ddlCRGLSubGroup.SelectedValue
            End If

            iGLShrt = Convert.ToInt32(lblModalCRGlId.Text)

            objCOA.ActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGLShrt)
            lblErrorCRGl.Text = "Successfully Activated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            ' If objCOA.igl_head = 2 Then
            ddlCrOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLSubGroup.SelectedValue)
            ddlCrOtherGL.DataTextField = "Description"
            ddlCrOtherGL.DataValueField = "gl_id"
            ddlCrOtherGL.DataBind()
            ddlCrOtherGL.Items.Insert(0, "Select GL")
            ddlCrOtherHead.SelectedIndex = ddlCRGLHead.SelectedIndex
            ddlCrOtherGL.SelectedValue = iGLShrt
            ddlCrOtherGL_SelectedIndexChanged(sender, e)
            LoadParty(ddlCustomerParty.SelectedIndex)
            ' End If

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGLShrt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCRGLCode.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCRGLCode.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtCRGLName.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtCRGLName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescription.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    btnDescSaveCRGL.Visible = False : btnDescDeActivateCRGL.Visible = False : btnDescActivateCRGL.Visible = True
                    btnDescUpdateCRGL.Visible = True


                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    btnDescSaveCRGL.Visible = False : imgbtnUpdate.Visible = False : btnDescActivateCRGL.Visible = True : btnDescDeActivateCRGL.Visible = False
                    btnDescUpdateCRGL.Visible = False

                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    btnDescSaveCRGL.Visible = False : btnDescDeActivateCRGL.Visible = True : btnDescActivateCRGL.Visible = False

                    btnDescUpdateCRGL.Visible = True
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDsceActivate_Click")
        End Try
    End Sub

    Private Sub btnDescDeActivateCRGL_Click(sender As Object, e As EventArgs) Handles btnDescDeActivateCRGL.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Dim iGLShrt As Integer = 0
        Try
            lblErrorCRGl.Text = ""

            If ddlCRGLSubGroup.SelectedIndex > 0 Then
                iGlID = ddlCRGLSubGroup.SelectedValue
            End If
            iGLShrt = Convert.ToInt32(lblModalCRGlId.Text)

            objCOA.DeActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGLShrt)
            lblErrorCRGl.Text = "Successfully De-Activated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)


            ddlCrOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLSubGroup.SelectedValue)
            ddlCrOtherGL.DataTextField = "Description"
            ddlCrOtherGL.DataValueField = "gl_id"
            ddlCrOtherGL.DataBind()
            ddlCrOtherGL.Items.Insert(0, "Select GL")
            ddlCrOtherHead.SelectedIndex = ddlCRGLHead.SelectedIndex
            LoadParty(ddlCustomerParty.SelectedIndex)

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGLShrt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCRGLCode.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCRGLCode.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtCRGLName.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtCRGLName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescription.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    btnDescSaveCRGL.Visible = False : btnDescDeActivateCRGL.Visible = False : btnDescActivateCRGL.Visible = True
                    btnDescUpdateCRGL.Visible = True

                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    btnDescSaveCRGL.Visible = False : btnDescActivateCRGL.Visible = True : btnDescDeActivateCRGL.Visible = False
                    btnDescUpdateCRGL.Visible = False

                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    btnDescSaveCRGL.Visible = False : btnDescActivateCRGL.Visible = False : btnDescDeActivateCRGL.Visible = True
                    btnDescUpdateCRGL.Visible = True
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescDeActivateCRGL")
        End Try
    End Sub

    'Create debit SGL
    Private Sub imgbtnDrOtherSubGL_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDrOtherSubGL.Click
        Try

            lblErrorSGL.Text = "" : btnDeactivateSgl.Visible = False
            txtCodeSgl.Text = "" : txtNameSgl.Text = "" : txtDescSgl.Text = "" : btnDeactivateSgl.Visible = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlHeadSgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHeadSgl.SelectedIndexChanged
        Try
            lblErrorSGL.Text = "" : lblModelError.Text = ""
            ddlGroupSgl.SelectedIndex = -1 : ddlSubGroupSgl.SelectedIndex = -1 : txtCodeSgl.Text = "" : txtNameSgl.Text = "" : txtDescSgl.Text = ""
            btnUpdateSgl.Visible = False : btnUpdateSgl.Visible = False
            If ddlHeadSgl.SelectedIndex > 0 Then
                ddlGroupSgl.DataSource = objRecp.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHeadSgl.SelectedIndex)
                ddlGroupSgl.DataTextField = "GlDesc"
                ddlGroupSgl.DataValueField = "gl_Id"
                ddlGroupSgl.DataBind()
                ddlGroupSgl.Items.Insert(0, "Select Group")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorSGL.Text = "Select Head."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " ddlHeadSgl_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlGroupSgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroupSgl.SelectedIndexChanged
        Try
            lblErrorSGL.Text = "" : lblModelError.Text = ""
            ddlSubGroupSgl.SelectedIndex = -1 : txtCodeSgl.Text = "" : txtNameSgl.Text = "" : txtDescSgl.Text = ""
            If ddlHeadSgl.SelectedIndex > 0 Then
                ddlSubGroupSgl.DataSource = objRecp.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroupSgl.SelectedValue)
                ddlSubGroupSgl.DataTextField = "GlDesc"
                ddlSubGroupSgl.DataValueField = "gl_Id"
                ddlSubGroupSgl.DataBind()
                ddlSubGroupSgl.Items.Insert(0, "Select SubGroup")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorSGL.Text = "Select Head."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGroupSgl_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSubGroupSgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubGroupSgl.SelectedIndexChanged
        Try
            lblErrorSGL.Text = "" : lblModelError.Text = ""
            txtCodeSgl.Text = "" : txtNameSgl.Text = "" : txtDescSgl.Text = ""
            If ddlGroupSgl.SelectedIndex > 0 Then
                ddlGLSgl.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupSgl.SelectedValue)
                ddlGLSgl.DataTextField = "Description"
                ddlGLSgl.DataValueField = "gl_Id"
                ddlGLSgl.DataBind()
                ddlGLSgl.Items.Insert(0, "Select GL")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorSGL.Text = "Select Group."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubGroupSgl_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlGLSgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGLSgl.SelectedIndexChanged
        Try
            lblErrorSGL.Text = "" : lblModelError.Text = ""
            txtCodeSgl.Text = "" : txtNameSgl.Text = "" : txtDescSgl.Text = ""
            If (ddlHeadSgl.SelectedIndex > 0) And (ddlGroupSgl.SelectedIndex > 0) And (ddlSubGroupSgl.SelectedIndex > 0) And (ddlGLSgl.SelectedIndex > 0) Then
                txtCodeSgl.Text = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, ddlHeadSgl.SelectedIndex, ddlGLSgl.SelectedValue)
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorSGL.Text = "Select General Ledger."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " ddlGLSgl_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub btnNewSgl_Click(sender As Object, e As EventArgs) Handles btnNewSgl.Click
        Try
            lblModelError.Text = ""
            lblErrorSGL.Text = "" : ddlHeadSgl.SelectedIndex = -1 : ddlGroupSgl.SelectedIndex = -1
            ddlSubGroupSgl.SelectedIndex = -1 : ddlGLSgl.SelectedIndex = -1 : txtCodeSgl.Text = "" : txtNameSgl.Text = "" : txtDescSgl.Text = ""
            btnDeactivateSgl.Visible = False : btnActivateSgl.Visible = True : btnSaveSgl.Visible = True : btnUpdateSgl.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewSgl_Click")
        End Try
    End Sub

    Private Sub btnSaveSgl_Click(sender As Object, e As EventArgs) Handles btnSaveSgl.Click
        Dim iRet As Integer = 0
        Dim sRet As String
        Dim sArray As Array
        Try

            If ddlHeadSgl.SelectedIndex > 0 Then
                objCOA.igl_head = 0
                objCOA.igl_Parent = 0
            End If

            If ddlGroupSgl.SelectedIndex > 0 Then
                objCOA.igl_head = 1
                objCOA.igl_Parent = ddlGroupSgl.SelectedValue
            End If

            If ddlSubGroupSgl.SelectedIndex > 0 Then
                objCOA.igl_head = 2
                objCOA.igl_Parent = ddlSubGroupSgl.SelectedValue
            End If

            If ddlGLSgl.SelectedIndex > 0 Then
                objCOA.igl_head = 3
                objCOA.igl_Parent = ddlGLSgl.SelectedValue
            End If

            objCOA.igl_id = 0
            objCOA.sgl_glcode = txtCodeSgl.Text
            objCOA.sgl_Desc = objGen.SafeSQL(txtNameSgl.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescSgl.Text)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = ddlHeadSgl.SelectedIndex
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "W"
            objCOA.sgl_IPAddress = sSession.IPAddress
            sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            sArray = sRet.Split(",")
            lblModalSglId.Text = sArray(0)
            If sArray(1) = 0 Then
                lblErrorSGL.Text = "Successfully Saved and Waiting for Approval."
                btnSaveSgl.Visible = False
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            ElseIf sArray(1) = 1 Then
                lblErrorSGL.Text = "The Name( " & txtNameSgl.Text & ") Already Exists. Enter New Name"
                txtNameSgl.Text = "" : txtDescSgl.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
                Exit Sub
            End If


            If objCOA.igl_head = 3 Then
                ddlDbOtherSubGL.DataSource = objRecp.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGLSgl.SelectedValue)
                ddlDbOtherSubGL.DataTextField = "Description"
                ddlDbOtherSubGL.DataValueField = "gl_id"
                ddlDbOtherSubGL.DataBind()
                ddlDbOtherSubGL.Items.Insert(0, "Select GL")
                ddlDBOtherHead.SelectedIndex = ddlHeadSgl.SelectedIndex
                ddlDbOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupSgl.SelectedValue)
                ddlDbOtherGL.DataTextField = "Description"
                ddlDbOtherGL.DataValueField = "gl_id"
                ddlDbOtherGL.DataBind()
                ddlDbOtherGL.Items.Insert(0, "Select SGL")
                ddlDbOtherGL.SelectedValue = ddlGLSgl.SelectedValue
                ddlDBOtherGL_SelectedIndexChanged(sender, e)
            End If



            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveSgl_Click")
        End Try
    End Sub
    Private Sub btnUpdateSgl_Click(sender As Object, e As EventArgs) Handles btnUpdateSgl.Click
        Try
            lblErrorSGL.Text = ""
            If ddlHeadSgl.SelectedIndex = 0 Or ddlHeadSgl.SelectedIndex = -1 Then
                lblErrorSGL.Text = "Select Head"
                Exit Sub
            End If
            If ddlGroupSgl.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalSglId.Text
            End If

            If ddlSubGroupSgl.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalSglId.Text
            End If

            If ddlGLSgl.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalSglId.Text
            End If


            objCOA.sgl_Desc = objGen.SafeSQL(txtNameSgl.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescSgl.Text)
            objCOA.igl_UpdatedBy = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_IPAddress = sSession.IPAddress
            objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)

            lblErrorSGL.Text = "Successfully Updated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            If ddlGLSgl.SelectedValue > 0 Then
                ddlDbOtherSubGL.DataSource = objRecp.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGLSgl.SelectedValue)
                ddlDbOtherSubGL.DataTextField = "Description"
                ddlDbOtherSubGL.DataValueField = "gl_id"
                ddlDbOtherSubGL.DataBind()
                ddlDbOtherSubGL.Items.Insert(0, "Select GL")
                ddlDBOtherHead.SelectedIndex = ddlHeadSgl.SelectedIndex
                ddlDbOtherGL.SelectedValue = ddlGLSgl.SelectedValue
                ddlDBOtherGL_SelectedIndexChanged(sender, e)
                ddlDbOtherSubGL.SelectedValue = objCOA.igl_id
                ddlDBOtherSubGL_SelectedIndexChanged(sender, e)
            End If


            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateSgl_Click")
        End Try
    End Sub
    Private Sub btnActivateSgl_Click(sender As Object, e As EventArgs) Handles btnActivateSgl.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Dim iGLShrt As Integer = 0
        Try
            lblErrorSGL.Text = ""
            If ddlGLSgl.SelectedIndex > 0 Then
                iGlID = ddlGLSgl.SelectedValue
            End If

            iGLShrt = Convert.ToInt32(lblModalSglId.Text)

            objCOA.ActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGLShrt)
            lblErrorSGL.Text = "Successfully Activated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            ' If objCOA.igl_head = 2 Then

            ddlDbOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupSgl.SelectedValue)
            ddlDbOtherGL.DataTextField = "Description"
            ddlDbOtherGL.DataValueField = "gl_id"
            ddlDbOtherGL.DataBind()
            ddlDbOtherGL.Items.Insert(0, "Select GL")
            ddlDBOtherHead.SelectedIndex = ddlHead.SelectedIndex
            ddlDbOtherGL.SelectedValue = ddlGLSgl.SelectedValue
            ddlDBOtherGL_SelectedIndexChanged(sender, e)
            ddlDbOtherSubGL.SelectedValue = iGLShrt
            ddlDBOtherSubGL_SelectedIndexChanged(sender, e)


            ' End If

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGLShrt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCodeSgl.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCodeSgl.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtNameSgl.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtNameSgl.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescSgl.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescSgl.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    btnSaveSgl.Visible = False : btnDeactivateSgl.Visible = False : btnActivateSgl.Visible = True
                    btnUpdateSgl.Visible = True


                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    btnSaveSgl.Visible = False : btnActivateSgl.Visible = True : btnDeactivateSgl.Visible = False
                    btnUpdateSgl.Visible = False

                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    btnSaveSgl.Visible = False : btnDeactivateSgl.Visible = True : btnActivateSgl.Visible = False
                    btnUpdateSgl.Visible = True
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDsceActivate_Click")
        End Try
    End Sub

    Private Sub btnDeactivateSgl_Click(sender As Object, e As EventArgs) Handles btnDeactivateSgl.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Dim iGLShrt As Integer = 0
        Try

            lblErrorSGL.Text = ""
            If ddlGLSgl.SelectedIndex > 0 Then
                iGlID = ddlGLSgl.SelectedValue
            End If
            iGLShrt = Convert.ToInt32(lblModalSglId.Text)

            objCOA.DeActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGLShrt)
            lblErrorSGL.Text = "Successfully De-Activated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)


            ddlDbOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlGLSgl.SelectedValue)
            ddlDbOtherGL.DataTextField = "Description"
            ddlDbOtherGL.DataValueField = "gl_id"
            ddlDbOtherGL.DataBind()
            ddlDbOtherGL.Items.Insert(0, "Select GL")
            ddlDBOtherHead.SelectedIndex = ddlHead.SelectedIndex
            ddlDbOtherSubGL.DataSource = objRecp.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGLSgl.SelectedValue)
            ddlDbOtherSubGL.DataTextField = "Description"
            ddlDbOtherSubGL.DataValueField = "gl_id"
            ddlDbOtherSubGL.DataBind()
            ddlDbOtherSubGL.Items.Insert(0, "Select SGL")



            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGLShrt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCodeSgl.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCodeSgl.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtNameSgl.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtNameSgl.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescSgl.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescSgl.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    btnSaveSgl.Visible = False : btnDeactivateSgl.Visible = False : btnActivateSgl.Visible = True
                    btnUpdateSgl.Visible = True

                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    btnSaveSgl.Visible = False : btnActivateSgl.Visible = True : btnDeactivateSgl.Visible = False
                    btnUpdateSgl.Visible = False

                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    btnSaveSgl.Visible = False : btnActivateSgl.Visible = False : btnDeactivateSgl.Visible = True
                    btnUpdateSgl.Visible = True
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDeactivateSgl_Click")
        End Try
    End Sub


    'Create Credit SGL
    Private Sub imgbtnCrOtherSGL_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCrOtherSGL.Click
        Try

            lblErrorSGL.Text = "" : btnDeactivateSgl.Visible = False
            txtCodeSgl.Text = "" : txtNameSgl.Text = "" : txtDescSgl.Text = "" : btnDeactivateSgl.Visible = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlHeadCRSgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHeadCRSgl.SelectedIndexChanged
        Try
            lblErrorCRSGL.Text = "" : lblModelError.Text = ""
            ddlGroupCRSgl.SelectedIndex = -1 : ddlSubGroupCRSgl.SelectedIndex = -1 : txtCodeCRSgl.Text = "" : txtNameCRSgl.Text = "" : txtDescCRSgl.Text = ""
            btnUpdateCRSgl.Visible = False : btnDeactivateCRSgl.Visible = False
            If ddlHeadCRSgl.SelectedIndex > 0 Then
                ddlGroupCRSgl.DataSource = objRecp.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHeadCRSgl.SelectedIndex)
                ddlGroupCRSgl.DataTextField = "GlDesc"
                ddlGroupCRSgl.DataValueField = "gl_Id"
                ddlGroupCRSgl.DataBind()
                ddlGroupCRSgl.Items.Insert(0, "Select Group")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorCRSGL.Text = "Select Head."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " ddlHeadCRSgl_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlGroupCRSgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroupCRSgl.SelectedIndexChanged
        Try
            lblErrorCRSGL.Text = "" : lblModelError.Text = ""
            ddlSubGroupCRSgl.SelectedIndex = -1 : txtCodeCRSgl.Text = "" : txtNameCRSgl.Text = "" : txtDescCRSgl.Text = ""
            If ddlHeadCRSgl.SelectedIndex > 0 Then
                ddlSubGroupCRSgl.DataSource = objRecp.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroupCRSgl.SelectedValue)
                ddlSubGroupCRSgl.DataTextField = "GlDesc"
                ddlSubGroupCRSgl.DataValueField = "gl_Id"
                ddlSubGroupCRSgl.DataBind()
                ddlSubGroupCRSgl.Items.Insert(0, "Select SubGroup")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorCRSGL.Text = "Select Head."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGroupCRSgl_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSubGroupCRSgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubGroupCRSgl.SelectedIndexChanged
        Try
            lblErrorCRSGL.Text = "" : lblModelError.Text = ""
            txtCodeCRSgl.Text = "" : txtNameCRSgl.Text = "" : txtDescCRSgl.Text = ""
            If ddlGroupCRSgl.SelectedIndex > 0 Then
                ddlGLCRSgl.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupCRSgl.SelectedValue)
                ddlGLCRSgl.DataTextField = "Description"
                ddlGLCRSgl.DataValueField = "gl_Id"
                ddlGLCRSgl.DataBind()
                ddlGLCRSgl.Items.Insert(0, "Select GL")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorCRSGL.Text = "Select Group."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubGroupCRSgl_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlGLCRSgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGLCRSgl.SelectedIndexChanged
        Try
            lblErrorCRSGL.Text = "" : lblModelError.Text = ""
            txtCodeCRSgl.Text = "" : txtNameCRSgl.Text = "" : txtDescCRSgl.Text = ""
            If (ddlHeadCRSgl.SelectedIndex > 0) And (ddlGroupCRSgl.SelectedIndex > 0) And (ddlSubGroupCRSgl.SelectedIndex > 0) And (ddlGLCRSgl.SelectedIndex > 0) Then
                txtCodeCRSgl.Text = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, ddlHeadCRSgl.SelectedIndex, ddlGLCRSgl.SelectedValue)
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorCRSGL.Text = "Select General Ledger."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " ddlGLCRSgl_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub btnNewCRSgl_Click(sender As Object, e As EventArgs) Handles btnNewCRSgl.Click
        Try
            lblModelError.Text = ""
            lblErrorCRSGL.Text = "" : ddlHeadCRSgl.SelectedIndex = -1 : ddlGroupCRSgl.SelectedIndex = -1
            ddlSubGroupCRSgl.SelectedIndex = -1 : ddlGLCRSgl.SelectedIndex = -1 : txtCodeCRSgl.Text = "" : txtNameCRSgl.Text = "" : txtDescCRSgl.Text = ""
            btnDeactivateCRSgl.Visible = False : btnActivateCRSgl.Visible = True : btnSaveCRSgl.Visible = True : btnUpdateCRSgl.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewCRSgl_Click")
        End Try
    End Sub

    Private Sub btnSaveCRSgl_Click(sender As Object, e As EventArgs) Handles btnSaveCRSgl.Click
        Dim iRet As Integer = 0
        Dim sRet As String
        Dim sArray As Array
        Try

            If ddlHeadCRSgl.SelectedIndex > 0 Then
                objCOA.igl_head = 0
                objCOA.igl_Parent = 0
            End If

            If ddlGroupCRSgl.SelectedIndex > 0 Then
                objCOA.igl_head = 1
                objCOA.igl_Parent = ddlGroupCRSgl.SelectedValue
            End If

            If ddlSubGroupCRSgl.SelectedIndex > 0 Then
                objCOA.igl_head = 2
                objCOA.igl_Parent = ddlSubGroupCRSgl.SelectedValue
            End If

            If ddlGLCRSgl.SelectedIndex > 0 Then
                objCOA.igl_head = 3
                objCOA.igl_Parent = ddlGLCRSgl.SelectedValue
            End If

            objCOA.igl_id = 0
            objCOA.sgl_glcode = txtCodeCRSgl.Text
            objCOA.sgl_Desc = objGen.SafeSQL(txtNameCRSgl.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescCRSgl.Text)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = ddlHeadCRSgl.SelectedIndex
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "W"
            objCOA.sgl_IPAddress = sSession.IPAddress
            sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            sArray = sRet.Split(",")
            lblModalCRSGLID.Text = sArray(0)
            If sArray(1) = 0 Then
                lblErrorCRSGL.Text = "Successfully Saved and Waiting for Approval."
                btnSaveCRSgl.Visible = False
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            ElseIf sArray(1) = 1 Then
                lblErrorCRSGL.Text = "The Name( " & txtNameCRSgl.Text & ") Already Exists. Enter New Name"
                txtNameCRSgl.Text = "" : txtDescCRSgl.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
                Exit Sub
            End If


            If objCOA.igl_head = 3 Then
                ddlCrOtherSubGL.DataSource = objRecp.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGLCRSgl.SelectedValue)
                ddlCrOtherSubGL.DataTextField = "Description"
                ddlCrOtherSubGL.DataValueField = "gl_id"
                ddlCrOtherSubGL.DataBind()
                ddlCrOtherSubGL.Items.Insert(0, "Select SGL")
                ddlCrOtherHead.SelectedIndex = ddlHeadCRSgl.SelectedIndex
                'ddlCrOtherGL.SelectedValue = ddlGLCRSgl.SelectedValue
                'ddlCrOtherGL_SelectedIndexChanged(sender, e)

                ddlCrOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupCRSgl.SelectedValue)
                ddlCrOtherGL.DataTextField = "Description"
                ddlCrOtherGL.DataValueField = "gl_id"
                ddlCrOtherGL.DataBind()
                ddlCrOtherGL.Items.Insert(0, "Select GL")
                ddlCrOtherGL.SelectedValue = ddlGLCRSgl.SelectedValue
                ddlCrOtherGL_SelectedIndexChanged(sender, e)
            End If



            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveCRSgl_Click")
        End Try
    End Sub
    Private Sub btnUpdateCRSgl_Click(sender As Object, e As EventArgs) Handles btnUpdateCRSgl.Click
        Try
            lblErrorCRSGL.Text = ""
            If ddlHeadCRSgl.SelectedIndex = 0 Or ddlHeadCRSgl.SelectedIndex = -1 Then
                lblErrorCRSGL.Text = "Select Head"
                Exit Sub
            End If
            If ddlGroupCRSgl.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalCRSGLID.Text
            End If

            If ddlSubGroupCRSgl.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalCRSGLID.Text
            End If

            If ddlGLCRSgl.SelectedIndex > 0 Then
                objCOA.igl_id = lblModalCRSGLID.Text
            End If


            objCOA.sgl_Desc = objGen.SafeSQL(txtNameCRSgl.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescCRSgl.Text)
            objCOA.igl_UpdatedBy = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_IPAddress = sSession.IPAddress
            objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)

            lblErrorCRSGL.Text = "Successfully Updated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            If ddlGLCRSgl.SelectedValue > 0 Then
                ddlCrOtherSubGL.DataSource = objRecp.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGLCRSgl.SelectedValue)
                ddlCrOtherSubGL.DataTextField = "Description"
                ddlCrOtherSubGL.DataValueField = "gl_id"
                ddlCrOtherSubGL.DataBind()
                ddlCrOtherSubGL.Items.Insert(0, "Select GL")
                ddlCrOtherHead.SelectedIndex = ddlHeadCRSgl.SelectedIndex
                ddlCrOtherGL.SelectedValue = ddlGLCRSgl.SelectedValue
                ddlCrOtherGL_SelectedIndexChanged(sender, e)
                ddlCrOtherSubGL.SelectedValue = objCOA.igl_id
                ddlCrOtherSubGL_SelectedIndexChanged(sender, e)
                LoadParty(ddlCustomerParty.SelectedIndex)
            End If


            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateCRSgl_Click")
        End Try
    End Sub
    Private Sub btnActivateCRSgl_Click(sender As Object, e As EventArgs) Handles btnActivateCRSgl.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Dim iGLShrt As Integer = 0
        Try
            lblErrorCRSGL.Text = ""
            If ddlGLCRSgl.SelectedIndex > 0 Then
                iGlID = ddlGLCRSgl.SelectedValue
            End If

            iGLShrt = Convert.ToInt32(lblModalCRSGLID.Text)

            objCOA.ActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGLShrt)
            lblErrorCRSGL.Text = "Successfully Activated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            ' If objCOA.igl_head = 2 Then

            ddlCrOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupCRSgl.SelectedValue)
            ddlCrOtherGL.DataTextField = "Description"
            ddlCrOtherGL.DataValueField = "gl_id"
            ddlCrOtherGL.DataBind()
            ddlCrOtherGL.Items.Insert(0, "Select GL")
            ddlCrOtherHead.SelectedIndex = ddlHead.SelectedIndex
            ddlCrOtherGL.SelectedValue = ddlGLCRSgl.SelectedValue
            ddlCrOtherGL_SelectedIndexChanged(sender, e)
            ddlCrOtherSubGL.SelectedValue = iGLShrt
            ddlCrOtherSubGL_SelectedIndexChanged(sender, e)
            LoadParty(ddlCustomerParty.SelectedIndex)

            ' End If

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGLShrt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCodeCRSgl.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCodeCRSgl.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtNameCRSgl.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtNameCRSgl.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescCRSgl.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescCRSgl.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    btnSaveCRSgl.Visible = False : btnDeactivateCRSgl.Visible = False : btnActivateCRSgl.Visible = True
                    btnUpdateCRSgl.Visible = True


                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    btnSaveCRSgl.Visible = False : btnActivateCRSgl.Visible = True : btnDeactivateCRSgl.Visible = False
                    btnUpdateCRSgl.Visible = False

                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    btnSaveCRSgl.Visible = False : btnDeactivateCRSgl.Visible = True : btnActivateCRSgl.Visible = False
                    btnUpdateCRSgl.Visible = True
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDsceActivate_Click")
        End Try
    End Sub

    Private Sub btnDeactivateCRSgl_Click(sender As Object, e As EventArgs) Handles btnDeactivateCRSgl.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Dim iGLShrt As Integer = 0
        Try

            lblErrorCRSGL.Text = ""
            If ddlGLCRSgl.SelectedIndex > 0 Then
                iGlID = ddlGLCRSgl.SelectedValue
            End If
            iGLShrt = Convert.ToInt32(lblModalCRSGLID.Text)

            objCOA.DeActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGLShrt)
            lblErrorCRSGL.Text = "Successfully De-Activated."
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)


            ddlCrOtherGL.DataSource = objRecp.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupCRSgl.SelectedValue)
            ddlCrOtherGL.DataTextField = "Description"
            ddlCrOtherGL.DataValueField = "gl_id"
            ddlCrOtherGL.DataBind()
            ddlCrOtherGL.Items.Insert(0, "Select GL")
            ddlCrOtherHead.SelectedIndex = ddlHead.SelectedIndex
            ddlCrOtherSubGL.DataSource = objRecp.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGLCRSgl.SelectedValue)
            ddlCrOtherSubGL.DataTextField = "Description"
            ddlCrOtherSubGL.DataValueField = "gl_id"
            ddlCrOtherSubGL.DataBind()
            ddlCrOtherSubGL.Items.Insert(0, "Select SGL")
            LoadParty(ddlCustomerParty.SelectedIndex)


            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGLShrt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCodeCRSgl.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCodeCRSgl.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtNameCRSgl.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtNameCRSgl.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescCRSgl.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescCRSgl.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    btnSaveCRSgl.Visible = False : btnDeactivateCRSgl.Visible = False : btnActivateCRSgl.Visible = True
                    btnUpdateCRSgl.Visible = True

                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    btnSaveCRSgl.Visible = False : btnActivateCRSgl.Visible = True : btnDeactivateCRSgl.Visible = False
                    btnUpdateCRSgl.Visible = False

                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    btnSaveCRSgl.Visible = False : btnActivateCRSgl.Visible = False : btnDeactivateCRSgl.Visible = True
                    btnUpdateCRSgl.Visible = True
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalCRSGL').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDeactivateCRSgl_Click")
        End Try
    End Sub


End Class
