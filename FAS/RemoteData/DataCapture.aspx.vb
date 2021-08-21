Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports GleamTech.DocumentUltimateExamples.WebForms.CS.DocumentViewer
Partial Class RemoteData_DataCapture
    Inherits System.Web.UI.Page
    Private sFormName As String = "Datacapture"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objDBL As New DatabaseLayer.DBHelper
    Private objJE As New ClsJE
    Private objclsSearch As New clsSearch
    Private objGen As New clsFASGeneral
    Private objclsview As New clsView
    Private objSearch As New clsSearch
    Private objclsFolders As New clsFolders
    Dim objDataCap As New ClsDataCapture
    Private Shared sSession As AllSession
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAttachments As New clsAttachments

    Private objPO As New ClsPO
    Private objAccSetting As New clsAccountSetting
    Private Shared sPOSave As String

    Private Shared iSelectedFirstID As Integer = 0
    Private Shared iPageNext As Integer
    Private Shared sImgFilePath As String = ""
    Private Shared iNextPage As Integer
    Private Shared sBaseName() As String
    Private Shared iCheck As Integer = 0
    Private Shared sSelectedChecksIDs As String = ""
    Private Shared sDetailsId As String = ""
    Private Shared sSelectedCabID As String = ""
    Private Shared sSelectedSubCabID As String = ""
    Private Shared sSelectedFolID As String = ""
    Private Shared sSelectedDocTypeID As String = ""
    Private Shared sSelectedKWID As String = ""
    Private Shared sSelectedDescID As String = ""
    Private Shared sSelectedFrmtID As String = ""
    Private Shared sSelectedCrByID As String = ""
    Private Shared iSelectedIndexID As Integer = 0
    Private Shared iDocID As Integer
    Private Shared sSelId As String = ""
    Private Shared iAttachID As Integer

    Private Shared iSelectedImageID As Integer = 0
    Private Shared sSelectedImageExt As String = ""
    Private Shared sInvalidImageIDs As String = ""

    Private objIndex As New clsIndexing
    Dim dt As New DataTable
    Private objSO As New ClsSO
    Private objPC As New ClsPetty
    Dim dtMerge As New DataTable
    Private objRecp As New ClsRemoteReceipt
    Dim objPayment As New ClsPay
    Private Shared iAdd As Integer = 0
    Public dtPayment As New DataTable
    Private objGIN As New ClsGIN
    Private objGreturn As New ClsReturnPurchase
    Private objCS As New ClsCashSales
    Private objDis As New ClsSalesDispatch
    Private objDispatch As New ClsInvoiceSales
    Private objSR As New ClsSR
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/BackWard24.png"
        imgbtnDADD.ImageUrl = "~/Images/Add24.png"
        imgbtnOtherCADD.ImageUrl = "~/Images/Add24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnNavDocFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNavDoc.ImageUrl = "~/Images/SearchImage/Previous16.png"
        imgbtnNextNavDoc.ImageUrl = "~/Images/SearchImage/Next16.png"
        imgbtnNavDocFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNav.ImageUrl = "~/Images/SearchImage/Preview16.png"
        imgbtnNextNav.ImageUrl = "~/Images/SearchImage/Nextt16.png"
        imgbtnFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnAnnotation.ImageUrl = "~/Images/Annotation24.png"
        imgbtnSendBackImage.ImageUrl = "~/Images/SendBackImage32.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        'imgbtnInvalidImage.ImageUrl = "~/Images/Invalid_image20.png"
        imgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnSendBackImage.ImageUrl = "~/Images/SendBackImage32.png"
        imgbtnInvalidImage.ImageUrl = "~/Images/Invalid_Image32.png"
        imgbtnCreateCustomer.ImageUrl = "~/Images/Add16.png"
        imgbtnAddBillAmt.ImageUrl = "~/Images/Add16.png"


        imgbtnRefresh.ImageUrl = "~/Images/Reresh16.png"

        imgbtnRefreshPO.ImageUrl = "~/Images/Reresh16.png"
        imgbtnSavePO.ImageUrl = "~/Images/Add16.png"
        imgbtnPrintPo.ImageUrl = "~/Images/Download16.png"
        imgbtnAddChargePo.ImageUrl = "~/Images/Add16.png"

        imgbtnRefreshSale.ImageUrl = "~/Images/Reresh16.png"
        imgbtnAddSale.ImageUrl = "~/Images/Add16.png"
        imgbtnPrintSale.ImageUrl = "~/Images/Download16.png"

        imgbtnAddRPJ.ImageUrl = "~/Images/Add16.png"
        imgbtnApproveRPJ.ImageUrl = "~/Images/Checkmark24.png"

        imgbtnDADDPay.ImageUrl = "~/Images/Add24.png"
        imgbtnOtherCADDPay.ImageUrl = "~/Images/Add24.png"

        imgbtnAddPay.ImageUrl = "~/Images/Add16.png"

        imgbtnRefreshCDI.ImageUrl = "~/Images/Reresh16.png"
        imgbtnAddCDI.ImageUrl = "~/Images/Add16.png"
        imgbtnPrintcashS.ImageUrl = "~/Images/Download16.png"

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sBatchID As String = "" : Dim sCustomerID As String = "" : Dim sTrTypeID As String = ""
        Dim iDefaultBranch As Integer

        Dim BaseID As Integer, i As Integer = 0
        Dim FileSelectedID As String = ""

        Dim sFile As String
        Dim sExt As String
        Dim sFileName As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                RFVtxtOtherDAmount.ErrorMessage = "Enter Debit Amount"
                RFVOtherCAmount.ErrorMessage = "Enter Debit Amount"

                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                divPurchase.Visible = False : divSales.Visible = False : divRPJ.Visible = False

                Session("DataCapture") = Nothing : Session("JE") = Nothing : Session("Petty") = Nothing : Session("dtReceipt") = Nothing
                iAttachID = 0 : lblBadgeCount.Text = 0 : sInvalidImageIDs = "" : sSelectedImageExt = "" : iSelectedImageID = 0
                imgbtnUpdate.Visible = False : imgbtnSave.Visible = True

                'Email From
                RFVEmailFrom.ControlToValidate = "txtEmailFrom" : RFVEmailFrom.ErrorMessage = "Enter From."
                REVEmailFrom.ErrorMessage = "Enter valid E-Mail." : REVEmailFrom.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                'Password
                RFVPassword.ControlToValidate = "txtPassword" : RFVPassword.ErrorMessage = "Enter Password."
                'Email To
                REVEmailTo.ErrorMessage = "Enter valid E-Mail." : REVEmailTo.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                'Subject
                RFVSubject.ControlToValidate = "txtSubject" : RFVSubject.ErrorMessage = "Enter Subject."
                REVSubject.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVSubject.ValidationExpression = "^[\s\S]{0,200}$"
                'Body
                RFVBody.ControlToValidate = "txtBody" : RFVBody.ErrorMessage = "Enter Body."
                REVBody.ErrorMessage = "Body exceeded maximum size(max 200 characters)." : REVBody.ValidationExpression = "^[\s\S]{0,200}$"

                BindUsers(sSession.UserID) : LoadCompany() : BindHeadofAccounts() : LoadPaymentType()
                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)

                iDefaultBranch = objDataCap.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                RFVddlCompany.InitialValue = "Select Company" : RFVddlCompany.ErrorMessage = "Select Company"

                'RFVddlParty.InitialValue = "Select Customer/Supplier" : RFVddlParty.ErrorMessage = "Select Customer/Supplier"
                RFVddlTrType.InitialValue = "Select Transaction Type" : RFVddlTrType.ErrorMessage = "Select Transaction Type"
                RFVddlBatchNo.InitialValue = "Select BatchNo" : RFVddlBatchNo.ErrorMessage = "Select BatchNo"
                RFVddlPaymentType.InitialValue = "Select Payment Type" : RFVddlPaymentType.ErrorMessage = "Select Payment Type"

                RFVAccZone.InitialValue = "Select Zone" : RFVAccZone.ErrorMessage = "Select Zone."
                RFVAccRgn.InitialValue = "Select Region" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "Select Area" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "Select Branch" : RFVAccBrnch.ErrorMessage = "Select Branch."

                RFVtxtVoucherNo.ErrorMessage = "Enter Voucher No"
                RFVtxtDate.ErrorMessage = "Enter Transaction Date"

                Session("DataTable") = Nothing : Session("dtPayment") = Nothing : Session("ChargesMaster") = Nothing : Session("Petty") = Nothing
                Session("ImageID") = Nothing

                ReceiptVisibleFalse()
                LoadPurchaseFunctions()
                LoadSalesFunctions()
                LoadRPJ()
                LoadPayment()

                LoadGINFunctions()
                LoadPRFunctions()

                LoadCashSalesFunctions()
                LoadSalesReturn()

                sBatchID = Request.QueryString("BatchID")
                sCustomerID = Request.QueryString("CustomerID")
                sTrTypeID = Request.QueryString("TrTypeID")

                sFile = String.Format("~/Images/SearchImage/NoImage.jpg")
                documentViewer.Document = sFile

                If sBatchID <> "" Then
                    ddlCompany.SelectedValue = sCustomerID
                    BindTrType(ddlCompany.SelectedValue)

                    ddlTrType.SelectedValue = sTrTypeID
                    ddlTrType_SelectedIndexChanged(sender, e)
                    BindBatchNo(ddlTrType.SelectedValue)

                    ddlBatchNo.SelectedValue = sBatchID
                    ddlBatchNo_SelectedIndexChanged(sender, e)
                End If

            End If

            If Session("ImageID") <> Nothing Then
                lblDocID.Text = Session("ImageID")
                iSelectedImageID = Session("ImageID")

                sFile = objSearch.GetPageFromEdict(sSession.AccessCode, Session("ImageID"))
                sImgFilePath = sFile
                If Trim(sFile.Length) = 0 Then Exit Sub
                sExt = Path.GetExtension(sFile)
                sExt = sExt.Remove(0, 1)
                sSelectedImageExt = sExt

                Select Case UCase(sExt)
                    Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                        Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
                        Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                        Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                        'RetrieveImage.ImageUrl = imageDataURL
                        documentViewer.Document = sFile
                        Dim fi As New IO.FileInfo(sFile)
                        Dim iDocType As Integer = objSearch.GetDocTypeID(sSession.AccessCode, Session("ImageID"))
                    Case "PDF"
                        Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
                        'RetrieveImage.ImageUrl = imageDataURL
                        documentViewer.Document = sFile
                        Dim fi As New IO.FileInfo(sFile)
                    Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PPS", "XLR", "XML", "TIF", "TIFF"

                End Select
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadSalesReturn()
        Try
            LoadExistingSalesReturn()
            txtReturnNoSR.Text = objSR.GenerateReturnNo(sSession.AccessCode, sSession.AccessCodeID)
            LoadCommoditySR()
            LoadCustomerSR()
            LoadChargeTypeSR()
            LoadDiscountSR()
            loadDescitionStartSR()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCustomerSR()
        Try
            ddlCustomerSR.DataSource = objSR.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerSR.DataTextField = "BM_Name"
            ddlCustomerSR.DataValueField = "BM_ID"
            ddlCustomerSR.DataBind()
            ddlCustomerSR.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommoditySR()
        Try
            ddlCommoditySR.DataSource = objSR.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommoditySR.DataTextField = "Inv_Description"
            ddlCommoditySR.DataValueField = "Inv_ID"
            ddlCommoditySR.DataBind()
            ddlCommoditySR.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingSalesReturn()
        Try
            ddlExistSalesReturn.DataSource = objSR.LoadExistingReturnNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistSalesReturn.DataTextField = "Sales_Return_ReturnNo"
            ddlExistSalesReturn.DataValueField = "Sales_Return_ID"
            ddlExistSalesReturn.DataBind()
            ddlExistSalesReturn.Items.Insert(0, "Select Existing Return No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadPayment()
        Try
            txtPaidAmount.Text = "0"
            imgbtnUpdate.Visible = False
            Session("dtPayment") = Nothing

            LoadExistingPayment() : BinPartyOrCustomerORGLPayment()
            BindTransactionType() : BindHeadofAccountsPay() : BindBankNamePayment()
            LoadBillTypePay() : LoadSubGL()
            BindPaymentType()

            txtTransactionNoPay.Text = objPayment.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            lblStatusPay.Text = "Not Started"

            RFVInvoiceDate.ErrorMessage = "Enter Date of Payment."
            REVInvoiceDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
            REVInvoiceDate.ErrorMessage = "Enter Valid Date Format."

            RFVddlPaymentTypePay.InitialValue = "Select Payment Type" : RFVddlPaymentTypePay.ErrorMessage = "Select Payment Type"
            RFVddlCustomerPartyPay.InitialValue = "Select Customer/Supplier/GL"
            RFVTransType.InitialValue = "Select Transaction Type" : RFVTransType.ErrorMessage = "Select Transaction Type."
            RFVddlBillTypePay.InitialValue = "Select Payment Voucher Type" : RFVddlBillTypePay.ErrorMessage = "Select Payment Voucher Type."

            REFBillDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
            REFBillDate.ErrorMessage = "Enter Valid Date Format."

            RFVEBillAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
            RFVEBillAmount.ErrorMessage = "Enter Valid Bill Amount."

            RFVddlDrOtherHead.InitialValue = "Select Head of Account"
            RFVddlDbOtherGL.InitialValue = "Select GL Code"

            RFVddlCrOtherHead.InitialValue = "Select Head of Account"
            RFVddlCrOtherGL.InitialValue = "Select GL Code"

            REVtxtChequeNoPay.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
            REVtxtChequeNoPay.ErrorMessage = "Enter Valid Cheque No."

            REVtxtChequeDatePay.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
            REVtxtChequeDatePay.ErrorMessage = "Enter Valid cheque Date."
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadBillTypePay()
        Dim dt As New DataTable
        Try
            dt = objPayment.LoadBIllType(sSession.AccessCode, sSession.AccessCodeID)
            If dt.Rows.Count > 0 Then
                ddlBillTypePay.DataSource = dt
                ddlBillTypePay.DataTextField = "Mas_Desc"
                ddlBillTypePay.DataValueField = "Mas_ID"
                ddlBillTypePay.DataBind()
                ddlBillTypePay.Items.Insert(0, "Select Payment Voucher Type")
            Else
                lblError.Text = "Create The Payment Voucher Type in General Master."
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BinPartyOrCustomerORGLPayment()
        Try
            ddlCustomerPartyPay.Items.Add(New ListItem("Select Customer/Supplier/GL", 0))
            ddlCustomerPartyPay.Items.Add(New ListItem("Customer", 1))
            ddlCustomerPartyPay.Items.Add(New ListItem("Supplier", 2))
            ddlCustomerPartyPay.Items.Add(New ListItem("General Ledger", 3))
            ddlCustomerPartyPay.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindPaymentType()
        Try
            ddlPaymentTypePay.Items.Insert(0, "Select Payment Type")
            ddlPaymentTypePay.Items.Insert(1, "With Inventory Advance")
            ddlPaymentTypePay.Items.Insert(2, "Without Inventory Advance")
            ddlPaymentTypePay.Items.Insert(3, "Expenses Advance")
            ddlPaymentTypePay.Items.Insert(4, "With Inventory Payment")
            ddlPaymentTypePay.Items.Insert(5, "Without Inventory Payment")
            ddlPaymentTypePay.Items.Insert(6, "Expenses Payment")
            'ddlPaymentType.Items.Insert(7, "Non-Trading Advance")
            'ddlPaymentType.Items.Insert(8, "Non-Trading Payment")
            ddlPaymentTypePay.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindTransactionType()
        Try
            ddlTransType.Items.Insert(0, "Select Transaction Type")
            ddlTransType.Items.Insert(1, "Cash")
            ddlTransType.Items.Insert(2, "Bank")
            ddlTransType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BillNoVisibleFalse()
        Try
            lblBillNo1.Visible = False : txtBillNo.Visible = False : lblBillDate1.Visible = False : txtBillDate.Visible = False
            lblBillAmount1.Visible = False : txtBillAmount.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub OrderVisibleFalse()
        Try
            txtOrderDate.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ReceiptVisibleFalse()
        Try
            lblReceiptType.Visible = False : ddlReceiptType.Visible = False : lblReceiptTrType.Visible = False : ddlReceiptTrType.Visible = False
            lblReceiptVoucherType.Visible = False : ddlReceiptVoucherType.Visible = False : lblReceiptPaidAmt.Visible = False : txtReceiptPaidAmt.Visible = False

            lblReceiptInvoiceNo.Visible = False : txtReceiptInvoiceNo.Visible = False : lblReceiptBillDate.Visible = False : txtReceiptBillDate.Visible = False
            lblReceiptInvoiceAmt.Visible = False : txtReceiptInvoiceAmt.Visible = False : lblReceiptOrderNo.Visible = False : txtReceiptSalesOrderNo.Visible = False
            lblReceiptorderDate.Visible = False : txtReceiptOrderDate.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ReceiptVisibleTrue()
        Try
            lblReceiptType.Visible = True : ddlReceiptType.Visible = True : lblReceiptTrType.Visible = True : ddlReceiptTrType.Visible = True
            lblReceiptVoucherType.Visible = True : ddlReceiptVoucherType.Visible = True : lblReceiptPaidAmt.Visible = True : txtReceiptPaidAmt.Visible = True

            lblReceiptInvoiceNo.Visible = True : txtReceiptInvoiceNo.Visible = True : lblReceiptBillDate.Visible = True : txtReceiptBillDate.Visible = True
            lblReceiptInvoiceAmt.Visible = True : txtReceiptInvoiceAmt.Visible = True : lblReceiptOrderNo.Visible = True : txtReceiptSalesOrderNo.Visible = True
            lblReceiptorderDate.Visible = True : txtReceiptOrderDate.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadRPJ()
        Try
            ReceiptVisibleFalse()
            BindReceiptType() : BindReceiptTrType() : LoadReceiptVoucherType()

            ddlJEType.Visible = False
            'BindTransactionType()
            LoadJEType()

            Session("Petty") = Nothing
            dtMerge = Nothing
            Session("datatable") = Nothing
            imgbtnUpdate.Visible = False

            BinPartyOrCustomerORGL() : LoadBillType()
            LoadSubGL()

            lblStatus.Text = "Not Started"
            ddlCrOtherHead.SelectedIndex = 1
            'ddlCrOtherHead_SelectedIndexChanged(sender, e)

            RFVInvoiceDate.ErrorMessage = "Enter Valid Invoice Date."
            REVInvoiceDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
            REVInvoiceDate.ErrorMessage = "Enter Valid Invoice Date"
            RFVCustomerParty.InitialValue = "Select Customer/Supplier/GL" : RFVCustomerParty.ErrorMessage = "Select Customer/Supplier/GL."
            RFVddlCSP.InitialValue = "Select Customer/Supplier/Party" : RFVddlCSP.ErrorMessage = "Select Customer/Supplier/Party"

            imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindReceiptType()
        Try
            ddlReceiptType.Items.Insert(0, "Select Receipt Type")
            ddlReceiptType.Items.Insert(1, "With Inventory Advance")
            ddlReceiptType.Items.Insert(2, "Without Inventory Advance")
            ddlReceiptType.Items.Insert(3, "General")
            ddlReceiptType.Items.Insert(4, "With Inventory Receipt")
            ddlReceiptType.Items.Insert(5, "Without Inventory Receipt")
            ddlReceiptType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindReceiptTrType()
        Try
            ddlReceiptTrType.Items.Insert(0, "Select Transaction Type")
            ddlReceiptTrType.Items.Insert(1, "Cash")
            ddlReceiptTrType.Items.Insert(2, "Bank(Cheque)")
            ddlReceiptTrType.Items.Insert(3, "Demand Draft")
            ddlReceiptTrType.Items.Insert(4, "RTGS")
            ddlReceiptTrType.Items.Insert(5, "NEFT")
            ddlReceiptTrType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadReceiptVoucherType()
        Try
            ddlReceiptVoucherType.DataSource = objRecp.LoadBIllType(sSession.AccessCode, sSession.AccessCodeID)
            ddlReceiptVoucherType.DataTextField = "Mas_Desc"
            ddlReceiptVoucherType.DataValueField = "Mas_ID"
            ddlReceiptVoucherType.DataBind()
            ddlReceiptVoucherType.Items.Insert(0, "Select Receipt Voucher Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub BindTransactionType()
    '    Try
    '        ddlTransactionType.Items.Insert(0, "Select TransactionType")
    '        ddlTransactionType.Items.Insert(1, "Journal Entry")
    '        ddlTransactionType.Items.Insert(2, "Petty Cash")
    '        ddlTransactionType.Items.Insert(3, "Receipt")
    '        ddlTransactionType.SelectedIndex = 0
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub LoadBillType()
        Try
            ddlBillType.DataSource = objPC.LoadBIllType(sSession.AccessCode, sSession.AccessCodeID)
            ddlBillType.DataTextField = "Mas_Desc"
            ddlBillType.DataValueField = "Mas_ID"
            ddlBillType.DataBind()
            ddlBillType.Items.Insert(0, "Select Bill Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BinPartyOrCustomerORGL()
        Try
            ddlCustomerParty.Items.Insert(0, "Select Customer/Supplier/GL")
            ddlCustomerParty.Items.Insert(1, "Customer")
            ddlCustomerParty.Items.Insert(2, "Supplier")
            ddlCustomerParty.Items.Insert(3, "General Ledger")
            ddlCustomerParty.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCustomerParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerParty.SelectedIndexChanged
        Try
            If ddlCustomerParty.SelectedIndex = 1 Then
                lblParty.Text = "* Customer"
                LoadParty(1)
            ElseIf ddlCustomerParty.SelectedIndex = 2 Then
                lblParty.Text = "* Supplier"
                LoadParty(2)
            ElseIf ddlCustomerParty.SelectedIndex = 3 Then
                lblParty.Text = "* General Ledger"
                LoadParty(3)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadParty(ByVal iType As Integer)
        Try
            If iType = 3 Then
                ddlCSP.DataSource = objPC.LoadAllGLCodes(sSession.AccessCode, sSession.AccessCodeID)
                ddlCSP.DataTextField = "GlDesc"
                ddlCSP.DataValueField = "gl_Id"
                ddlCSP.DataBind()
                ddlCSP.Items.Insert(0, "Select Party")
            ElseIf iType = 1 Then
                ddlCSP.DataSource = objPC.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
                ddlCSP.DataTextField = "Name"
                ddlCSP.DataValueField = "BM_ID"
                ddlCSP.DataBind()
                ddlCSP.Items.Insert(0, "Select Customer")
            ElseIf iType = 2 Then
                ddlCSP.DataSource = objPC.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
                ddlCSP.DataTextField = "Name"
                ddlCSP.DataValueField = "CSM_ID"
                ddlCSP.DataBind()
                ddlCSP.Items.Insert(0, "Select Supplier")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadSalesFunctions()
        Dim sDate As String = ""
        Try
            'Sales Validation'
            RFVPatryS.InitialValue = "Select Customer"
            RFVPaymentTypeS.InitialValue = "Select Payment Type"
            RFVCommodityS.InitialValue = "Select Commodity"
            RFVddlUnitOfMeassurement.InitialValue = "Select Unit Of Meassurement"
            'Sales validation'

            GenerateOrderCodeAnddate()

            LoadExistingSalesOrderNo()
            LoadCommoditySales()
            LoadPaymentTypeSales()
            LoadModeOfCommunication()
            LoadPartySales()
            LoadMethodOfShipingSales()
            LoadSalesMan()
            BindDescription(0)

            'LoadDiscount()
            LoadCategory()

            dgExistingProFormaSalesOrder.DataSource = Nothing
            dgExistingProFormaSalesOrder.DataBind()
            dgExistingProFormaSalesOrder.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSalesFunctions")
        End Try
    End Sub
    Public Sub LoadPaymentTypeSales()
        Try
            ddlPaymentTypeS.DataSource = objSO.BindPaymentType(sSession.AccessCode, sSession.AccessCodeID)
            ddlPaymentTypeS.DataTextField = "Mas_Desc"
            ddlPaymentTypeS.DataValueField = "Mas_ID"
            ddlPaymentTypeS.DataBind()
            ddlPaymentTypeS.Items.Insert(0, "Select Payment Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDescription(ByVal iCommodityID As Integer)
        Try
            lstBoxDescription.DataSource = objSO.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID)
            lstBoxDescription.DataTextField = "INV_Code"
            lstBoxDescription.DataValueField = "INV_ID"
            lstBoxDescription.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadModeOfCommunication()
        Try
            ddlModeOfCommunication.DataSource = objSO.BindModeOfCommunication(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfCommunication.DataTextField = "Mas_Desc"
            ddlModeOfCommunication.DataValueField = "Mas_ID"
            ddlModeOfCommunication.DataBind()
            ddlModeOfCommunication.Items.Insert(0, "Select Mode Of Communication")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCategory()
        Try
            ddlCategory.DataSource = objSO.BindCategory(sSession.AccessCode, sSession.AccessCodeID)
            ddlCategory.DataTextField = "Mas_Desc"
            ddlCategory.DataValueField = "Mas_ID"
            ddlCategory.DataBind()
            ddlCategory.Items.Insert(0, "Select Category")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadPartySales()
        Try
            ddlPatryS.DataSource = objSO.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlPatryS.DataTextField = "BM_Name"
            ddlPatryS.DataValueField = "BM_ID"
            ddlPatryS.DataBind()
            ddlPatryS.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadMethodOfShipingSales()
        Try
            ddlShipping.DataSource = objSO.LoadMethodOfShiping(sSession.AccessCode, sSession.AccessCodeID)
            ddlShipping.DataTextField = "Mas_desc"
            ddlShipping.DataValueField = "Mas_id"
            ddlShipping.DataBind()
            ddlShipping.Items.Insert(0, "Select Mode of Shipping")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSalesMan()
        Try
            ddlSalesMan.DataSource = objSO.LoadSalesMan(sSession.AccessCode, sSession.AccessCodeID)
            ddlSalesMan.DataTextField = "username"
            ddlSalesMan.DataValueField = "Usr_id"
            ddlSalesMan.DataBind()
            ddlSalesMan.Items.Insert(0, "Select Sales Person")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadGINFunctions()
        Try
            RFVddlSupplierGIN.InitialValue = "Select Supplier"

            LoadExistingInwardNo()
            GenerateGINNo()
            LoadSuppliersGIN()
            LoadCommodityGIN()
            loadDescitionStartGIN()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPurchaseFunctions")
        End Try
    End Sub
    Public Sub LoadPRFunctions()
        Try
            LoadExistingGoodsReturn()
            GeneratePRNo()
            LoadSuppliersPR()
            BindPurchaseReturnReason()
            LoadCommodityPR()
            loadDescitionStartPR()
            LoadChargeTypePR()

            Session("ChargesMaster") = Nothing

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPurchaseFunctions")
        End Try
    End Sub
    Public Sub BindPurchaseReturnReason()
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
    Public Sub LoadExistingGoodsReturn()
        Try
            ddlExistingReturnNor.DataSource = objGreturn.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingReturnNor.DataTextField = "GRM_ReturnNo"
            ddlExistingReturnNor.DataValueField = "GRM_ID"
            ddlExistingReturnNor.DataBind()
            ddlExistingReturnNor.Items.Insert(0, "Existing Return No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadPurchaseFunctions()
        Try
            'Purchase Validation'
            ReFVOdate.ErrorMessage = "Enter Order Date."
            RFVSupplier.InitialValue = "Select Supplier"
            RFVDschdule.InitialValue = "Select Delivery Schdule"
            RFVMshipping.InitialValue = "Select Mode of Shipping"
            RFVMpayment.InitialValue = "Select Method Of Payment"
            RFVCmdty.InitialValue = "Select Brand"

            RFVPterms.InitialValue = "Select Payment Terms"
            RFVddlUnit.InitialValue = "Unit of Measurement"
            ReDiscount.ErrorMessage = "Only Integer." : ReDiscount.ValidationExpression = "^\s*-?[0-9]\d*(\.\d{1,2})?\s*$"
            'Purchase Validation'

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

            Session("ChargesMaster") = Nothing
            LoadChargeType()
            BindCompanyType()
            BindBranch()

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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPurchaseFunctions")
        End Try
    End Sub
    Public Sub LoadPaymentType()
        Try
            ddlPaymentType.DataSource = objDataCap.BindPaymentType(sSession.AccessCode, sSession.AccessCodeID)
            ddlPaymentType.DataTextField = "Mas_Desc"
            ddlPaymentType.DataValueField = "Mas_ID"
            ddlPaymentType.DataBind()
            ddlPaymentType.Items.Insert(0, "Select Payment Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindUsers(ByVal iUserID As Integer)
        Try
            lstUsers.DataSource = objDataCap.LoadUser(sSession.AccessCode, sSession.AccessCodeID, iUserID)
            lstUsers.DataTextField = "Usr_FullName"
            lstUsers.DataValueField = "Usr_ID"
            lstUsers.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCompany()
        Try
            ddlCompany.DataSource = objDataCap.LoadCompany(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompany.DataTextField = "Name"
            ddlCompany.DataValueField = "CBN_NODE"
            ddlCompany.DataBind()
            ddlCompany.Items.Insert(0, "Select Company")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCustomerSupplier()
        Try
            If ddlTrType.SelectedIndex > 0 Then
                ddlParty.DataSource = objDataCap.LoadCustomerSupplier(sSession.AccessCode, sSession.AccessCodeID, ddlTrType.SelectedItem.Text)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Supplier")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
        Try
            'loadimage(sender, e)
            Dim sFile As String = String.Format("~/Images/SearchImage/NoImage.jpg")
            documentViewer.Document = sFile
            BindTrType(ddlCompany.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCompany_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadSubGL()
        Try
            ddlCrOtherSubGL.DataSource = objDataCap.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlCrOtherSubGL.DataTextField = "GlDesc"
            ddlCrOtherSubGL.DataValueField = "gl_Id"
            ddlCrOtherSubGL.DataBind()
            ddlCrOtherSubGL.Items.Insert(0, "Select SubGL Code")

            ddlDbOtherSubGL.DataSource = objDataCap.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlDbOtherSubGL.DataTextField = "GlDesc"
            ddlDbOtherSubGL.DataValueField = "gl_Id"
            ddlDbOtherSubGL.DataBind()
            ddlDbOtherSubGL.Items.Insert(0, "Select SubGL Code")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindTrType(ByVal iCustomerID As Integer)
        Try
            'ddlTrType.Items.Insert(0, "Select Transaction Type")
            'ddlTrType.Items.Insert(1, "Purchase")
            'ddlTrType.Items.Insert(2, "Sales")
            'ddlTrType.Items.Insert(3, "Payment")
            'ddlTrType.Items.Insert(4, "Receipt")
            'ddlTrType.Items.Insert(5, "Petty Cash")
            'ddlTrType.Items.Insert(6, "Journal Entry")

            ddlTrType.DataSource = objDataCap.BindTrType(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCustomerID)
            ddlTrType.DataValueField = "CBN_NODE"
            ddlTrType.DataTextField = "CBN_NAME"
            ddlTrType.DataBind()
            ddlTrType.Items.Insert(0, "Select Transaction Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindHeadofAccounts()
        Try
            ddlDrOtherHead.Items.Insert(0, "Select Head of Account")
            ddlDrOtherHead.Items.Insert(1, "Asset")
            ddlDrOtherHead.Items.Insert(2, "Income")
            ddlDrOtherHead.Items.Insert(3, "Expenditure")
            ddlDrOtherHead.Items.Insert(4, "Liabilities")
            ddlDrOtherHead.SelectedIndex = 0

            ddlCrOtherHead.Items.Insert(0, "Select Head of Account")
            ddlCrOtherHead.Items.Insert(1, "Asset")
            ddlCrOtherHead.Items.Insert(2, "Income")
            ddlCrOtherHead.Items.Insert(3, "Expenditure")
            ddlCrOtherHead.Items.Insert(4, "Liabilities")
            ddlCrOtherHead.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlDrOtherHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDrOtherHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlDrOtherHead.SelectedIndex > 0 Then
                ddlDbOtherGL.DataSource = objDataCap.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDrOtherHead.SelectedIndex)
                ddlDbOtherGL.DataTextField = "GlDesc"
                ddlDbOtherGL.DataValueField = "gl_Id"
                ddlDbOtherGL.DataBind()
                ddlDbOtherGL.Items.Insert(0, "Select GL Code")
            Else
                ddlDbOtherGL.DataSource = dt
                ddlDbOtherGL.DataBind()
                LoadSubGL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDrOtherHead_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlDbOtherGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDbOtherGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlDbOtherGL.SelectedIndex > 0 Then
                ddlDbOtherSubGL.DataSource = objDataCap.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDbOtherGL.SelectedValue)
                ddlDbOtherSubGL.DataTextField = "GlDesc"
                ddlDbOtherSubGL.DataValueField = "gl_Id"
                ddlDbOtherSubGL.DataBind()
                ddlDbOtherSubGL.Items.Insert(0, "Select SubGL Code")
            Else
                ddlDbOtherSubGL.DataSource = dt
                ddlDbOtherSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDbOtherGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCrOtherHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCrOtherHead.SelectedIndex > 0 Then
                ddlCrOtherGL.DataSource = objDataCap.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherHead.SelectedIndex)
                ddlCrOtherGL.DataTextField = "GlDesc"
                ddlCrOtherGL.DataValueField = "gl_Id"
                ddlCrOtherGL.DataBind()
                ddlCrOtherGL.Items.Insert(0, "Select GL Code")
            Else
                ddlCrOtherGL.DataSource = dt
                ddlCrOtherGL.DataBind()
                LoadSubGL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrOtherHead_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCrOtherGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCrOtherGL.SelectedIndex > 0 Then
                ddlCrOtherSubGL.DataSource = objDataCap.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherGL.SelectedValue)
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

    Private Sub imgbtnDADD_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDADD.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dDebit As Double = 0
        Dim dtDetails As New DataTable
        Dim dtPayment As New DataTable
        Dim dDebitAmt As Double = 0.0 : Dim dCreditAmt As Double = 0.0 : Dim dCreditTotalAmt As Double = 0.0
        Try

            '//Preeti
            If ddlDrOtherHead.SelectedIndex = 0 Then
                lblRPJ.Text = "Select Head of Accounts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Head of Accounts.','', 'info');", True)
                ddlDrOtherHead.Focus()
                Exit Sub
            End If
            If ddlDbOtherGL.SelectedIndex = 0 Then
                lblRPJ.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select General Ledger.','', 'info');", True)
                ddlDbOtherGL.Focus()
                Exit Sub
            End If
            If txtOtherDAmount.Text = "" Then
                lblRPJ.Text = "Enter Debit Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Debit Amount.','', 'info');", True)
                txtOtherDAmount.Focus()
                Exit Sub
            End If

            If ddlDbOtherSubGL.Items.Count > 1 Then
                If ddlDbOtherSubGL.SelectedIndex > 0 Then
                Else
                    lblRPJ.Text = "Select the Sub General Ledger for Debit."
                    Exit Sub
                End If
            End If


            If IsNothing(Session("DataCapture")) Then
                dtPayment = dtDetails
            Else
                dtPayment = Session("DataCapture")
            End If

            dtCOA = objDataCap.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

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

            If txtOtherDAmount.Text <> "" Then
                dDebit = txtOtherDAmount.Text
            Else
                dDebit = 0.00
            End If

            '//Preeti

            If ddlTrType.SelectedItem.Text = "Purchase" Or ddlTrType.SelectedItem.Text = "Receipt" Or ddlTrType.SelectedItem.Text = "Journal Entry" Then
                If ddlTrType.SelectedItem.Text = "Purchase" Then
                    dtPayment = objPO.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHead.SelectedIndex, iGL, iSubGL, dDebit, 1, dtPayment, dtCOA)
                    Session("DataCapture") = dtPayment
                    dgPaymentDetails.DataSource = dtPayment
                    dgPaymentDetails.DataBind()
                ElseIf ddlTrType.SelectedItem.Text = "Receipt" Then
                    dtPayment = objRecp.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHead.SelectedIndex, iGL, iSubGL, dDebit, 1, dtPayment, dtCOA)
                    Session("DataCapture") = dtPayment
                    dgPaymentDetails.DataSource = dtPayment
                    dgPaymentDetails.DataBind()
                ElseIf ddlTrType.SelectedItem.Text = "Journal Entry" Then
                    dtPayment = objJE.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHead.SelectedIndex, iGL, iSubGL, dDebit, 1, dtPayment, dtCOA)
                    Session("DataCapture") = dtPayment
                    dgPaymentDetails.DataSource = dtPayment
                    dgPaymentDetails.DataBind()
                End If

            ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then
                'dtPayment = objDataCap.LoadPaymentsMaster1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHead.SelectedIndex, iGL, iSubGL, dDebit, 1, dtPayment, dtCOA)
                dtPayment = objPC.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHead.SelectedIndex, iGL, iSubGL, dDebit, 1, dtPayment, dtCOA)
                Session("DataCapture") = dtPayment
                dgPettyCashDetails.DataSource = dtPayment
                dgPettyCashDetails.DataBind()
            End If
            If ddlTrType.SelectedItem.Text = "Journal Entry" Or ddlTrType.SelectedItem.Text = "Petty Cash" Then
                dDebitAmt = txtOtherDAmount.Text
                If txtOtherCAmount.Text <> "" Then
                    dCreditAmt = txtOtherCAmount.Text
                Else
                    txtOtherCAmount.Text = txtOtherDAmount.Text
                End If
                dCreditTotalAmt = dCreditAmt + dDebitAmt
                txtOtherCAmount.Text = dCreditTotalAmt
            End If

            LoadSubGL()
            ddlDrOtherHead.SelectedIndex = 0 : ddlDbOtherGL.Items.Clear() : ddlDbOtherSubGL.Items.Clear() : txtOtherDAmount.Text = "" : ddlCrOtherSubGL.Items.Clear()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDADD_Click")
        End Try
    End Sub

    Private Sub imgbtnOtherCADD_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnOtherCADD.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dCredit As Double = 0
        Dim dtDetails As New DataTable
        Dim dtPayment As New DataTable
        Try

            If ddlCrOtherHead.SelectedIndex = 0 Then
                lblRPJ.Text = "Select Head of Accounts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Head of Accounts.','', 'info');", True)
                ddlCrOtherHead.Focus()
                Exit Sub
            End If
            If ddlCrOtherGL.SelectedIndex = 0 Then
                lblRPJ.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select General Ledger.','', 'info');", True)
                ddlCrOtherGL.Focus()
                Exit Sub
            End If
            If txtOtherCAmount.Text = "" Then
                lblRPJ.Text = "Enter Debit Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Debit Amount.','', 'info');", True)
                txtOtherCAmount.Focus()
                Exit Sub
            End If


            If ddlCrOtherSubGL.Items.Count > 1 Then
                If ddlCrOtherSubGL.SelectedIndex > 0 Then
                Else
                    lblRPJ.Text = "Select the Sub General Ledger for Credit."
                    Exit Sub
                End If
            End If

            If IsNothing(Session("DataCapture")) Then
                dtPayment = dtDetails
            Else

                dtPayment = Session("DataCapture")
            End If

            dtCOA = objDataCap.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

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

            If txtOtherCAmount.Text <> "" Then
                dCredit = txtOtherCAmount.Text
            Else
                dCredit = 0.00
            End If
            '//Preeti   

            If ddlTrType.SelectedItem.Text = "Purchase" Or ddlTrType.SelectedItem.Text = "Receipt" Or ddlTrType.SelectedItem.Text = "Journal Entry" Then
                If ddlTrType.SelectedItem.Text = "Purchase" Then
                    dtPayment = objPO.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrOtherHead.SelectedIndex, iGL, iSubGL, dCredit, 2, dtPayment, dtCOA)
                    Session("DataCapture") = dtPayment
                    dgPaymentDetails.DataSource = dtPayment
                    dgPaymentDetails.DataBind()
                ElseIf ddlTrType.SelectedItem.Text = "Receipt" Then
                    dtPayment = objRecp.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrOtherHead.SelectedIndex, iGL, iSubGL, dCredit, 2, dtPayment, dtCOA)
                    Session("DataCapture") = dtPayment
                    dgPaymentDetails.DataSource = dtPayment
                    dgPaymentDetails.DataBind()
                ElseIf ddlTrType.SelectedItem.Text = "Journal Entry" Then
                    dtPayment = objJE.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrOtherHead.SelectedIndex, iGL, iSubGL, dCredit, 2, dtPayment, dtCOA)
                    Session("DataCapture") = dtPayment
                    dgPaymentDetails.DataSource = dtPayment
                    dgPaymentDetails.DataBind()
                End If
            ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then
                'dtPayment = objDataCap.LoadPaymentsMaster1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHead.SelectedIndex, iGL, iSubGL, dCredit, 2, dtPayment, dtCOA)
                dtPayment = objPC.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrOtherHead.SelectedIndex, iGL, iSubGL, dCredit, 2, dtPayment, dtCOA)
                Session("DataCapture") = dtPayment
                dgPettyCashDetails.DataSource = dtPayment
                dgPettyCashDetails.DataBind()
            End If


            LoadSubGL()
            ddlCrOtherHead.SelectedIndex = 0 : ddlCrOtherGL.Items.Clear() : ddlCrOtherSubGL.Items.Clear() : txtOtherCAmount.Text = "" : ddlDbOtherSubGL.Items.Clear()

            If ddlTrType.SelectedItem.Text = "Journal Entry" Then

            ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then
                Dim dTotalAmt As Double = 0
                For i = 0 To dgPetty.Items.Count - 1
                    dTotalAmt = dTotalAmt + Convert.ToDouble(dgPetty.Items(i).Cells(4).Text)
                Next
                'txtBillAmount.Text = txtCreditAmount.Text
                txtBillAmount.Text = dTotalAmt
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnOtherCADD_Click")
        End Try
    End Sub
    Public Sub loadimage(sender As Object, e As EventArgs)
        Dim Cab, Subcab, Fol As Integer
        Try
            BindCabinet()
            Cab = objDataCap.GetCabID(sSession.AccessCode, sSession.AccessCodeID, ddlCompany.SelectedItem.Text)
            If Cab = 0 Then
                lblError.Text = "Cabinet Not Found"
                Exit Sub
            End If
            ddlCabinet.SelectedValue = Cab
            ddlCabinet_SelectedIndexChanged(sender, e)
            Subcab = objDataCap.GetSubCabID(sSession.AccessCode, sSession.AccessCodeID, ddlTrType.SelectedItem.Text, ddlCabinet.SelectedValue)
            If Subcab = 0 Then
                lblError.Text = "SubCabinet Not Found"
                Exit Sub
            End If
            ddlSubCabinet.SelectedValue = Subcab
            ddlSubCabinet_SelectedIndexChanged(sender, e)
            Fol = objDataCap.GetFoldID(sSession.AccessCode, sSession.AccessCodeID, ddlBatchNo.SelectedItem.Text, ddlSubCabinet.SelectedValue)
            If Fol = 0 Then
                lblError.Text = "Folder Not Found"
                Exit Sub
            End If
            ddlFolder.SelectedValue = Fol
            ddlFolder_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    '*****************************************img viewer*************************************************************
    'Public Sub loadimage(sender As Object, e As EventArgs)
    '    Dim Cab, Subcab, Fol As Integer
    '    Try
    '        BindCabinet()
    '        Cab = objDataCap.GetCabID(sSession.AccessCode, sSession.AccessCodeID, ddlCompany.SelectedItem.Text)
    '        If Cab = 0 Then
    '            lblError.Text = "Cabinet Not Found"
    '            Exit Sub
    '        End If
    '        'ddlCabinet.SelectedValue = Cab
    '        'ddlCabinet_SelectedIndexChanged(sender, e)

    '        ddlCabinet.SelectedIndex = 0
    '        Dim liCabinetID As ListItem = ddlCabinet.Items.FindByValue(Val(Cab))
    '        If IsNothing(liCabinetID) = False Then
    '            ddlCabinet.SelectedValue = Cab
    '            ddlCabinet_SelectedIndexChanged(sender, e)
    '        Else
    '            lblError.Text = "Cabinet Not Found"
    '            Exit Sub
    '        End If

    '        Subcab = objDataCap.GetSubCabID(sSession.AccessCode, sSession.AccessCodeID, ddlTrType.SelectedItem.Text, ddlCabinet.SelectedValue)
    '        If Subcab = 0 Then
    '            lblError.Text = "SubCabinet Not Found"
    '            Exit Sub
    '        End If
    '        'ddlSubCabinet.SelectedValue = Subcab
    '        'ddlSubCabinet_SelectedIndexChanged(sender, e)

    '        ddlSubCabinet.SelectedIndex = 0
    '        Dim liSubCabinetID As ListItem = ddlSubCabinet.Items.FindByValue(Val(Subcab))
    '        If IsNothing(liSubCabinetID) = False Then
    '            ddlSubCabinet.SelectedValue = Subcab
    '            ddlSubCabinet_SelectedIndexChanged(sender, e)
    '        Else
    '            lblError.Text = "SubCabinet Not Found"
    '            Exit Sub
    '        End If

    '        Fol = objDataCap.GetFoldID(sSession.AccessCode, sSession.AccessCodeID, ddlBatchNo.SelectedItem.Text, ddlSubCabinet.SelectedValue)
    '        If Fol = 0 Then
    '            lblError.Text = "Folder Not Found"
    '            Exit Sub
    '        End If
    '        'ddlFolder.SelectedValue = Fol
    '        'ddlFolder_SelectedIndexChanged(sender, e)

    '        ddlFolder.SelectedIndex = 0
    '        Dim liFolderID As ListItem = ddlFolder.Items.FindByValue(Val(Fol))
    '        If IsNothing(liFolderID) = False Then
    '            ddlFolder.SelectedValue = Fol
    '            ddlFolder_SelectedIndexChanged(sender, e)
    '        Else
    '            lblError.Text = "Folder Not Found"
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub BindCabinet()
        Dim ds As New DataSet
        Try
            ds = objclsview.LoadCabinet(sSession.AccessCode, sSession.AccessCodeID)
            ddlCabinet.DataSource = ds
            ddlCabinet.DataTextField = "CBN_NAME"
            ddlCabinet.DataValueField = "CBN_NODE"
            ddlCabinet.DataBind()
            ddlCabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindexistingSubCab()
        Dim ds As New DataSet
        Try
            ds = objclsFolders.LoadSubCab(sSession.AccessCode, ddlCabinet.SelectedValue)
            ddlSubCabinet.DataSource = ds
            ddlSubCabinet.DataTextField = "CBN_NAME"
            ddlSubCabinet.DataValueField = "CBN_NODE"
            ddlSubCabinet.DataBind()
            ddlSubCabinet.Items.Insert(0, "Select Sub Cabinet")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub BindexistingFolder()
        Dim ds As New DataSet
        Try
            ds = objclsview.LoadExistingFolder(sSession.AccessCode, sSession.AccessCodeID, ddlSubCabinet.SelectedValue)
            ddlFolder.DataSource = ds
            ddlFolder.DataTextField = "FOL_Name"
            ddlFolder.DataValueField = "FOL_FolID"
            ddlFolder.DataBind()
            ddlFolder.Items.Insert(0, "Select Folder")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnPreviousNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNavDoc.Click
        Dim dtFiles As New DataTable
        Dim ivalue As Integer = 0 : Dim sExt As String = ""
        lblError.Text = ""
        Try
            iPageNext = 0 : iCheck = 0
            If (iCheck = 1 Or lstDocument.Items.Count = 1) Then
                ivalue = Val(txtID.Text) - 1
            Else
                ivalue = Val(txtID.Text)
            End If

            If Val(ivalue) >= 1 Then
                If lstDocument.Items.Count <> -1 Then
                    iCheck = 0
                    lstFiles.Items.Clear()
                    If lstDocument.Items.Count = txtID.Text Then
                        If Val(txtID.Text) = 0 Then
                            'txtNavDoc.Text = "1 Of " & lstDocument.Items.Count
                            txtNavDoc.Text = 1
                            lblNavDoc.Text = "/" & lstDocument.Items.Count
                        Else
                            txtPreId.Text = Val(txtPreId.Text) - 1
                            'txtNavDoc.Text = "" & Val(txtPreId.Text) & " Of " & lstDocument.Items.Count
                            txtNavDoc.Text = Val(txtPreId.Text)
                            lblNavDoc.Text = "/" & lstDocument.Items.Count
                        End If
                        txtID.Text = Val(txtID.Text) - 2

                        dtFiles = objclsview.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                        If dtFiles.Rows.Count <> 0 Then
                            For i = 0 To dtFiles.Rows.Count - 1
                                lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                            Next
                            'txtNav.Text = "1 Of " & lstFiles.Items.Count
                            txtNav.Text = 1
                            lblNav.Text = "/" & lstFiles.Items.Count
                        Else
                            txtNav.Text = ""
                        End If

                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                    Else
                        txtPreId.Text = Val(txtPreId.Text) - 1
                        'txtNavDoc.Text = "" & Val(txtPreId.Text) & " Of " & lstDocument.Items.Count
                        txtNavDoc.Text = Val(txtPreId.Text)
                        lblNavDoc.Text = "/" & lstDocument.Items.Count
                        txtID.Text = Val(txtID.Text) - 1
                        dtFiles = objclsview.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                        If dtFiles.Rows.Count <> 0 Then
                            For i = 0 To dtFiles.Rows.Count - 1
                                lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                            Next
                            txtNav.Text = 1
                            lblNav.Text = "/" & lstFiles.Items.Count
                        Else
                            txtNav.Text = ""
                        End If
                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                    End If
                End If
            Else
                txtID.Text = 0
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name','', 'error');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPreviousNavDoc_Click")
        End Try
    End Sub
    Private Sub imgbtnNextNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNextNavDoc.Click
        Dim dtFiles As New DataTable
        lblError.Text = ""
        Try
            iPageNext = 0
            If lstDocument.Items.Count <= txtID.Text Then
                txtID.Text = lstDocument.Items.Count
                Exit Sub
            End If
            txtID.Text = Val(txtID.Text) + 1
            If lstDocument.Items.Count <> txtID.Text Then
                If lstDocument.Items.Count <> -1 Then
                    iCheck = 1
                    lstFiles.Items.Clear()
                    If lstDocument.Items.Count >= txtID.Text Then
                        txtPreId.Text = Val(txtPreId.Text) + 1
                        If txtNavDoc.Text > Val(txtID.Text) Then
                            txtNavDoc.Text = Val(txtNavDoc.Text)
                        Else
                            txtNavDoc.Text = Val(txtID.Text) + 1
                        End If
                        lblNavDoc.Text = "/" & lstDocument.Items.Count
                        lstDocument.SelectedIndex = Val(txtID.Text)
                        dtFiles = objclsview.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                        If dtFiles.Rows.Count <> 0 Then
                            For i = 0 To dtFiles.Rows.Count - 1
                                lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                            Next
                            txtNav.Text = 1
                            lblNav.Text = "/" & lstFiles.Items.Count
                        Else
                            txtNav.Text = ""
                        End If
                        If lstFiles.Items.Count = 1 Then
                            txtNav.Enabled = False
                        End If
                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data.','', 'success');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data.','', 'success');", True)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name.','', 'success');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNextNavDoc_Click")
        End Try
    End Sub
    Private Sub imgbtnNavDocFastRewind_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNavDocFastRewind.Click
        lblError.Text = ""
        Try
            txtPreId.Text = 2
            txtID.Text = 1
            imgbtnPreviousNavDoc_Click(sender, e)
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name.','', 'success');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNavDocFastRewind_Click")
        End Try
    End Sub
    Private Sub imgbtnNavDocFastForword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNavDocFastForword.Click
        lblError.Text = ""
        Try
            txtPreId.Text = lstDocument.Items.Count - 1
            txtID.Text = lstDocument.Items.Count - 2
            imgbtnNextNavDoc_Click(sender, e)
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name.','', 'success');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNavDocFastForword_Click")
        End Try
    End Sub

    Private Sub imgbtnPreviousNav_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNav.Click
        lblError.Text = ""
        Try
            If lstFiles.Items.Count > 0 Then
                If lstFiles.Items.Count <> -1 And iPageNext > 0 Then
                    iPageNext = iPageNext - 1
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                    Dim iPage As Integer = iPageNext
                    txtNav.Text = iPage + 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                Else
                    iPageNext = 0
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name.','', 'success');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPreviousNav_Click")
        End Try
    End Sub
    Private Sub imgbtnNextNav_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNextNav.Click
        lblError.Text = ""
        Try
            If iNextPage <> 0 Then
                iPageNext = iNextPage
                iNextPage = 0
            End If
            If lstFiles.Items.Count = 0 Then
                Exit Sub
            End If
            If lstFiles.Items.Count > iPageNext Then
                iPageNext = iPageNext + 1
                If lstFiles.SelectedIndex <> -1 And lstFiles.Items.Count > iPageNext Then
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                    Dim iPage As Integer = iPageNext
                    'txtNav.Text = iPage + 1 & " of " & lstFiles.Items.Count
                    txtNav.Text = iPage + 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                txtNavDoc.Text = ""
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name.','', 'success');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNextNav_Click")
        End Try
    End Sub

    Private Sub imgbtnFastRewind_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnFastRewind.Click
        lblError.Text = ""
        Try
            iPageNext = 0
            lstFiles.SelectedIndex = iPageNext
            lstFiles_SelectedIndexChanged(sender, e)
            If lstFiles.Items.Count > 0 Then
                txtNav.Text = 1
                lblNav.Text = "/" & lstFiles.Items.Count
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name.','', 'success');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnFastRewind_Click")
        End Try
    End Sub
    Private Sub imgbtnFastForword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnFastForword.Click
        Try

            Dim ObjDbGen As New DatabaseLayer.DBHelper
            Dim objSearch As New clsView
            Dim sExt As String = ""
            lblError.Text = ""
            If lstFiles.Items.Count > 0 Then
                iPageNext = lstFiles.Items.Count
                iPageNext = iPageNext - 1
                lstFiles.SelectedIndex = iPageNext
                lstFiles_SelectedIndexChanged(sender, e)
                txtNav.Text = iPageNext + 1
                lblNav.Text = "/" & lstFiles.Items.Count
            Else
                iPageNext = 0
                lstFiles.SelectedIndex = iPageNext
                lstFiles_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name.','', 'error');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnFastForword_Click")
        End Try
    End Sub
    Private Sub lstFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFiles.SelectedIndexChanged
        Dim sFile As String
        Dim sExt As String
        Dim dt As New DataTable, dtIndexType As New DataTable
        Dim sFileName As String = ""
        Dim dtTrans As New DataTable
        Try
            sSelectedImageExt = ""
            If lstFiles.Items.Count = -1 Then Exit Sub
            lblDocID.Text = lstFiles.SelectedItem.Text
            iSelectedImageID = lstFiles.SelectedItem.Text

            sFile = objSearch.GetPageFromEdict(sSession.AccessCode, lstFiles.SelectedItem.Text)
            sImgFilePath = sFile
            If Trim(sFile.Length) = 0 Then Exit Sub
            sExt = Path.GetExtension(sFile)
            sExt = sExt.Remove(0, 1)
            sSelectedImageExt = sExt

            Select Case UCase(sExt)
                Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                    Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                    'RetrieveImage.ImageUrl = imageDataURL
                    documentViewer.Document = sFile
                    Dim fi As New IO.FileInfo(sFile)
                    Dim iDocType As Integer = objSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    Session("ImageID") = lstFiles.SelectedItem.Text
                Case "PDF"
                    Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
                    'RetrieveImage.ImageUrl = imageDataURL
                    documentViewer.Document = sFile
                    Dim fi As New IO.FileInfo(sFile)
                Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PPS", "XLR", "XML", "TIF", "TIFF"
                    documentViewer.Document = sFile
            End Select

            'Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
            'documentViewer.Document = System.IO.File.ReadAllBytes(sFile)

            ClearAll()
            txtTransactionNo.Text = objDataCap.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            'Extra to fetch the data'
            dt = objDataCap.GetDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCompany.SelectedValue, ddlTrType.SelectedValue, ddlBatchNo.SelectedValue, lstFiles.SelectedItem.Text)
            If dt.Rows.Count > 0 Then
                imgbtnUpdate.Visible = True : imgbtnSave.Visible = False
                For i = 0 To dt.Rows.Count - 1

                    If IsDBNull(dt.Rows(i)("DC_TransactionNo")) = False Then
                        txtTransactionNo.Text = dt.Rows(i)("DC_TransactionNo")
                    Else
                        txtTransactionNo.Text = ""
                    End If
                    If IsDBNull(dt.Rows(i)("DC_TrDate")) = False Then
                        txtDate.Text = dt.Rows(i)("DC_TrDate")
                    Else
                        txtDate.Text = ""
                    End If
                    If IsDBNull(dt.Rows(i)("DC_Company")) = False Then
                        ddlCompany.SelectedValue = dt.Rows(i)("DC_Company")
                    Else
                        ddlCompany.SelectedIndex = 0
                    End If
                    If IsDBNull(dt.Rows(i)("DC_TrType")) = False Then
                        ddlTrType.SelectedValue = dt.Rows(i)("DC_TrType")
                        LoadCustomerSupplier()
                    Else
                        ddlTrType.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(i)("DC_Customer")) = False Then
                        If dt.Rows(i)("DC_Customer") > 0 Then
                            ddlParty.SelectedValue = dt.Rows(i)("DC_Customer")
                        Else
                            ddlParty.SelectedIndex = 0
                        End If
                    Else
                        ddlParty.Items.Clear()
                    End If

                    If IsDBNull(dt.Rows(i)("DC_BatchNo")) = False Then
                        ddlBatchNo.SelectedValue = dt.Rows(i)("DC_BatchNo")
                    Else
                        ddlBatchNo.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(i)("DC_VoucherNo")) = False Then
                        txtVoucherNo.Text = dt.Rows(i)("DC_VoucherNo")
                    Else
                        txtVoucherNo.Text = ""
                    End If
                    If IsDBNull(dt.Rows(i)("DC_Zone")) = False Then
                        ddlAccZone.SelectedValue = dt.Rows(i)("DC_Zone")
                        LoadRegion(ddlAccZone.SelectedValue)
                    End If
                    If IsDBNull(dt.Rows(i)("DC_Region")) = False Then
                        ddlAccRgn.SelectedValue = dt.Rows(i)("DC_Region")
                        LoadArea(ddlAccRgn.SelectedValue)
                    End If
                    If IsDBNull(dt.Rows(i)("DC_Area")) = False Then
                        ddlAccArea.SelectedValue = dt.Rows(i)("DC_Area")
                        LoadAccBrnch(ddlAccArea.SelectedValue)
                    End If
                    If IsDBNull(dt.Rows(i)("DC_Branch")) = False Then
                        ddlAccBrnch.SelectedValue = dt.Rows(i)("DC_Branch")
                    End If
                    If IsDBNull(dt.Rows(i)("DC_PaymentType")) = False Then
                        ddlPaymentType.SelectedValue = dt.Rows(i)("DC_PaymentType")
                    Else
                        ddlPaymentType.SelectedIndex = 0
                    End If
                    If IsDBNull(dt.Rows(i)("DC_Narration")) = False Then
                        txtNarration.Text = dt.Rows(i)("DC_Narration")
                    Else
                        txtNarration.Text = ""
                    End If
                    If IsDBNull(dt.Rows(i)("DC_Delfalg")) = False Then
                    Else
                    End If
                    If IsDBNull(dt.Rows(i)("DC_Status")) = False Then
                        If dt.Rows(i)("DC_Status") = "W" Then
                            lblStatus.Text = "Waiting For Approval"
                            imgbtnSave.ImageUrl = "~Images/Update24.png"
                        ElseIf dt.Rows(i)("DC_Status") = "A" Then
                            lblStatus.Text = "Activated"
                        End If
                    Else
                        lblStatus.Text = ""
                    End If

                    BindExistingNo(ddlCompany.SelectedValue, ddlTrType.SelectedValue, ddlBatchNo.SelectedValue)
                    ddlExisting.SelectedValue = dt.Rows(i)("DC_ID")

                Next
            End If

            'If ddlExisting.SelectedIndex > 0 Then
            '    If dtTrans.Rows.Count > 0 Then
            '        dtTrans = objDataCap.LoadSavedTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExisting.SelectedValue, ddlTrType.SelectedValue)
            '        dgPaymentDetails.DataSource = dtTrans
            '        dgPaymentDetails.DataBind()
            '        Session("DataCapture") = dtTrans
            '    Else
            '        dgPaymentDetails.DataSource = Nothing
            '        dgPaymentDetails.DataBind()
            '        Session("DataCapture") = Nothing
            '    End If
            'End If
            'Extra to fetch the data'

            If ddlTrType.SelectedItem.Text = "Purchase" Or ddlTrType.SelectedItem.Text = "Cash Purchase" Then
                ClearPurchaseAll()
                BindPODetails()
            ElseIf ddlTrType.SelectedItem.Text = "Sales" Then
                SalesClear()
                BindSODetails()
            ElseIf ddlTrType.SelectedItem.Text = "Payment" Then
                ClearPayment()
                BindPayment()
            ElseIf ddlTrType.SelectedItem.Text = "Receipt" Then
                ClearReceipt()
                BindReceiptDetails()
            ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then
                CleaPettyCash()
                BindPettyCash()
            ElseIf ddlTrType.SelectedItem.Text = "Journal Entry" Then
                ClearJE()
                BindJE()
            ElseIf ddlTrType.SelectedItem.Text = "GIN" Then
                ClearGIN()
                BindGIN()
            ElseIf ddlTrType.SelectedItem.Text = "Purchase Return" Then
                clearPurchaseReturn()
                BindPurchaseReturn()

            ElseIf ddlTrType.SelectedItem.Text = "Cash Sales" Then
                ClearCDI() : ClearCash()
                BindCashDetails()

            ElseIf ddlTrType.SelectedItem.Text = "Sales Dispatch" Then
                ClearCDI() : ClearDispatch()
                BindDispatch()

            ElseIf ddlTrType.SelectedItem.Text = "Sales Invoice" Then
                ClearCDI() : ClearInvoice()
                BindInvoice()

            ElseIf ddlTrType.SelectedItem.Text = "Sales Return" Then
                clearSalesReturn()
                BindSalesReturn()
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                'RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstFiles_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindPurchaseReturn()
        Dim dtMaster As New DataTable
        Try
            lblError.Text = ""
            dtMaster = objGreturn.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dtMaster.Rows.Count > 0 Then
                For i = 0 To dtMaster.Rows.Count - 1
                    If IsDBNull(dtMaster.Rows(i)("GRM_ID")) = False Then
                        ddlExistingReturnNor.SelectedValue = dtMaster.Rows(i)("GRM_ID")
                        imgbtnSavePR.ImageUrl = "~/Images/Update16.png"
                    Else
                        ddlExistingReturnNor.SelectedIndex = 0
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRM_OrderNo")) = False Then
                        txtOrderNoPR.Text = dtMaster.Rows(i)("GRM_OrderNo")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRM_OrderDate")) = False Then
                        txtOrderDatePR.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("GRM_OrderDate"), "D")
                    End If

                    If IsDBNull(dtMaster.Rows(i)("GRM_InvoiceNo")) = False Then
                        txtInvoiceNoPR.Text = dtMaster.Rows(i)("GRM_InvoiceNo")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRM_GINInvDate")) = False Then
                        txtInvoiceDatePR.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("GRM_GINInvDate"), "D")
                    End If

                    If IsDBNull(dtMaster.Rows(i)("GRM_ReturnNo")) = False Then
                        txtOrderCodePR.Text = dtMaster.Rows(i)("GRM_ReturnNo")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRM_ReturnRefNo")) = False Then
                        txtReturnRefNo.Text = dtMaster.Rows(i)("GRM_ReturnRefNo")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRM_ReturnDate")) = False Then
                        txtReturnDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("GRM_ReturnDate"), "D")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRM_Supplier")) = False Then
                        ddlSupplierPR.SelectedValue = dtMaster.Rows(i)("GRM_Supplier")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRM_DelFlag")) = False Then
                        If dtMaster.Rows(i)("GRM_DelFlag") = "A" Then
                            lblStatusPR.Text = "Status : Activated"
                        ElseIf dtMaster.Rows(i)("GRM_DelFlag") = "W" Then
                            lblStatusPR.Text = "Status : Waiting For Approval"
                        End If
                    End If
                Next
                loadDescitionStartPR()
            End If

            If ddlExistingReturnNor.SelectedIndex > 0 Then
                dgPurchaseReturn.DataSource = objGreturn.LoadGoodsreturnDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingReturnNor.SelectedValue)
                dgPurchaseReturn.DataBind()

                Dim dtCharge As New DataTable
                dtCharge = objGreturn.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingReturnNor.SelectedValue, 0, 0)
                GvChargePR.DataSource = dtCharge
                GvChargePR.DataBind()
                Session("ChargesMaster") = dtCharge
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPurchaseReturn()")
        End Try
    End Sub
    Public Sub clearPurchaseReturn()
        Try
            ddlExistingReturnNor.SelectedIndex = 0 : ddlSupplierPR.SelectedIndex = 0 : ddlChargeTypePR.SelectedIndex = 0 : txtShippingRatePR.Text = ""
            lblStatusPR.Text = "" : lblError.Text = "" : txtOrderCodePR.Text = "" : txtReturnDate.Text = "" : txtReturnRefNo.Text = "" : ddlCommodityPR.SelectedIndex = 0 : ddlreturntype.SelectedIndex = 0

            ddlUnitPR.Items.Clear() : txtRatePR.Text = "" : txtReturnQuantity.Text = "" : txtRateAmountPR.Text = "" : txtDiscountPR.Text = "" : txtDiscountAmountPR.Text = ""
            txtGSTRatePR.Text = "" : txtGSTAmountPR.Text = "" : txtTotalAmountPR.Text = ""
            dgPurchaseReturn.DataSource = Nothing
            dgPurchaseReturn.DataBind()

            GvChargePR.DataSource = Nothing
            GvChargePR.DataBind()
            GeneratePRNo()
            imgbtnSavePR.ImageUrl = "~/Images/Save16.png"

            For i = 0 To chkCategoryPR.Items.Count - 1
                chkCategoryPR.Items(i).Selected = False
            Next

            txtOrderNoPR.Text = "" : txtOrderDatePR.Text = "" : txtInvoiceNoPR.Text = "" : txtInvoiceDatePR.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub clearPurchaseReturnDetails()
        Try
            ddlUnitPR.Items.Clear() : txtRatePR.Text = "" : txtReturnQuantity.Text = "" : txtRateAmountPR.Text = "" : txtDiscountPR.Text = "" : txtDiscountAmountPR.Text = ""
            txtGSTRatePR.Text = "" : txtGSTAmountPR.Text = "" : txtTotalAmountPR.Text = "" : ddlreturntype.SelectedIndex = 0

            For i = 0 To chkCategoryPR.Items.Count - 1
                chkCategoryPR.Items(i).Selected = False
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindGIN()
        Dim dtable As New DataTable
        Try
            dtable = objGIN.ExistingInwardDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dtable.Rows.Count > 0 Then
                For i = 0 To dtable.Rows.Count - 1
                    If dtable.Rows(i)("PGM_ID") > 0 Then
                        ddlExistingInwardNo.SelectedValue = dtable.Rows(i)("PGM_ID")
                    Else
                        ddlExistingInwardNo.SelectedIndex = 0
                    End If
                    txtOrderNoGIN.Text = objGen.ReplaceSafeSQL(dtable.Rows(i)("PGM_OrderNo"))
                    txtOrderDateGIN.Text = objGen.FormatDtForRDBMS(dtable.Rows(i)("PGM_OrderDate"), "D")
                    txtDocRefNo.Text = objGen.ReplaceSafeSQL(dtable.Rows(i)("PGM_DocumentRefNo"))
                    txtInvoiceDateGIN.Text = objGen.FormatDtForRDBMS(dtable.Rows(i)("PGM_InvoiceDate"), "D")
                    txtESugamNo.Text = objGen.ReplaceSafeSQL(dtable.Rows(i)("PGM_ESugamNo"))
                    txtOrderCodeGIN.Text = objGen.ReplaceSafeSQL(dtable.Rows(i)("PGM_Gin_Number"))
                    txtDcNo.Text = objGen.ReplaceSafeSQL(dtable.Rows(i)("PGM_DcNo"))
                    ddlSupplierGIN.SelectedValue = objGen.ReplaceSafeSQL(dtable.Rows(i)("PGM_Supplier"))
                    txtESugamNo.Text = objGen.ReplaceSafeSQL(dtable.Rows(i)("PGM_ESugamNo"))
                    If dtable.Rows(i)("PGM_Status") = "W" Then
                        lblStatusGIN.Text = "Waiting For Approval."
                    ElseIf dtable.Rows(i)("PGM_Status") = "A" Then
                        lblStatusGIN.Text = "Approved."
                    End If
                Next
                dgInward.DataSource = objGIN.LoadInwardDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInwardNo.SelectedValue)
                dgInward.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindGIN()")
        End Try
    End Sub
    Public Sub ClearGIN()
        Try
            For i = 0 To chkCategoryGIN.Items.Count - 1
                chkCategoryGIN.Items(i).Selected = False
            Next
            lblError.Text = "" : lblStatusGIN.Text = "" : txtOrderCodeGIN.Text = ""
            ddlExistingInwardNo.SelectedIndex = 0 : txtInvoiceDateGIN.Text = "" : ddlSupplierGIN.SelectedIndex = 0 : ddlCommodityGIN.SelectedIndex = 0 : txtDocRefNo.Text = "" : txtESugamNo.Text = ""
            txtBatchNo.Text = "" : txtManufactureDate.Text = "" : txtExpireDate.Text = "" : txtReceivedQty.Text = "" : txtOrderedQty.Text = "" : txtAcceptedQty.Text = "" : txtRejectedQty.Text = "" : txtExcessQty.Text = ""
            ddlUnitGIN.Items.Clear() : txtRateGIN.Text = "" : txtDcNo.Text = ""
            dgInward.DataSource = Nothing
            dgInward.DataBind()

            txtOrderNoGIN.Text = "" : txtOrderDateGIN.Text = ""

            GenerateGINNo()
            imgbtnSaveGIN.ImageUrl = "~/Images/Save16.png"
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearGINDetails()
        Try
            For i = 0 To chkCategoryGIN.Items.Count - 1
                chkCategoryGIN.Items(i).Selected = False
            Next
            txtBatchNo.Text = "" : txtManufactureDate.Text = "" : txtExpireDate.Text = "" : txtReceivedQty.Text = "" : txtOrderedQty.Text = "" : txtAcceptedQty.Text = "" : txtRejectedQty.Text = "" : txtExcessQty.Text = ""
            ddlUnitGIN.Items.Clear() : txtRateGIN.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindPayment()
        Dim dt As New DataTable, dtTrans As New DataTable
        Dim sExtraAmount As String = ""
        Try
            lblAmount.Text = ""
            dt = objPayment.GetPaymentDetails(sSession.AccessCode, sSession.AccessCodeID, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dt.Rows.Count > 0 Then
                imgbtnSavePay.ImageUrl = "~/Images/Update16.png"

                If IsDBNull(dt.Rows(0)("Acc_PM_AttachID")) = False Then
                    iAttachID = dt.Rows(0)("Acc_PM_AttachID").ToString()
                Else
                    iAttachID = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_ID").ToString()) = False Then
                    ddlExistPayment.SelectedValue = dt.Rows(0)("Acc_PM_ID").ToString()
                Else
                    ddlExistPayment.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_TransactionNo").ToString()) = False Then
                    txtTransactionNoPay.Text = dt.Rows(0)("Acc_PM_TransactionNo").ToString()
                Else
                    txtTransactionNoPay.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("ACC_PM_Location").ToString()) = False Then
                    ddlCustomerPartyPay.SelectedValue = Trim(dt.Rows(0)("ACC_PM_Location"))
                Else
                    ddlCustomerPartyPay.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_Party").ToString()) = False Then
                    If dt.Rows(0)("ACC_PM_Location") > 0 Then
                        LoadPartyPay(dt.Rows(0)("ACC_PM_Location"))
                        ddlPartyPay.SelectedValue = dt.Rows(0)("Acc_PM_Party").ToString()
                    Else
                        ddlPartyPay.Items.Clear()
                    End If
                Else
                    ddlPartyPay.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_TransactionType").ToString()) = False Then
                    ddlTransType.SelectedIndex = dt.Rows(0)("Acc_PM_TransactionType").ToString()
                Else
                    ddlTransType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_BillType").ToString()) = False Then
                    ddlBillTypePay.SelectedValue = dt.Rows(0)("Acc_PM_BillType").ToString()
                Else
                    ddlBillTypePay.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_BillNo").ToString()) = False Then
                    txtBillNoPay.Text = dt.Rows(0)("Acc_PM_BillNo").ToString()
                Else
                    txtBillNoPay.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("Acc_PM_BillDate").ToString()) = False Then
                    If (dt.Rows(0)("Acc_PM_BillDate").ToString() <> "") Then
                        If (dt.Rows(0)("Acc_PM_BillDate").ToString() <> "01/01/1900 12:00:00 AM") Then
                            txtBillDatePay.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_PM_BillDate").ToString(), "D")
                        Else
                            txtBillDatePay.Text = ""
                        End If
                    Else
                        txtBillDatePay.Text = ""
                    End If
                Else
                    txtBillDatePay.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_BillAmount").ToString()) = False Then
                    txtBillAmountPay.Text = dt.Rows(0)("Acc_PM_BillAmount").ToString()
                Else
                    txtBillAmountPay.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_Bill_Narration").ToString()) = False Then
                    txtNarrationPay.Text = dt.Rows(0)("Acc_Bill_Narration").ToString()
                Else
                    txtNarrationPay.Text = ""
                End If

                If dt.Rows(0)("Acc_PM_Status").ToString() = "W" Then
                    lblStatusPay.Text = "Waiting for Approval"
                ElseIf dt.Rows(0)("Acc_PM_Status").ToString() = "D" Then
                    lblStatusPay.Text = "De-Activated"
                ElseIf dt.Rows(0)("Acc_PM_Status").ToString() = "A" Then
                    lblStatusPay.Text = "Activated"
                End If

                If IsDBNull(dt.Rows(0)("ACC_PM_ChequeNo").ToString()) = False Then
                    txtChequeNoPay.Text = dt.Rows(0)("ACC_PM_ChequeNo").ToString()
                Else
                    txtChequeNoPay.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_ChequeDate").ToString()) = False Then
                    If (dt.Rows(0)("Acc_PM_ChequeDate").ToString() <> "") Then
                        If (dt.Rows(0)("Acc_PM_ChequeDate").ToString() <> "01/01/1990 12:00:00 AM") Then
                            txtChequeDatePay.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_PM_ChequeDate").ToString(), "D")
                        Else
                            txtChequeDatePay.Text = ""
                        End If
                    Else
                        txtChequeDatePay.Text = ""
                    End If
                Else
                    txtChequeDatePay.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_IFSCCode").ToString()) = False Then
                    txtIFSCPay.Text = dt.Rows(0)("Acc_PM_IFSCCode").ToString()
                Else
                    txtIFSCPay.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("ACC_PM_BankName").ToString()) = False Then
                    If dt.Rows(0)("ACC_PM_BankName").ToString() = "" Then
                        BindBankName()
                        ddlBankPay.SelectedIndex = 0
                    Else
                        If dt.Rows(0)("ACC_PM_BankName").ToString() > 0 Then
                            ddlBankPay.SelectedValue = dt.Rows(0)("ACC_PM_BankName").ToString()
                        Else
                            BindBankName()
                            ddlBankPay.SelectedIndex = 0
                        End If
                    End If
                Else
                    ddlBankPay.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_BranchName").ToString()) = False Then
                    txtBranchPay.Text = dt.Rows(0)("Acc_PM_BranchName").ToString()
                Else
                    txtBranchPay.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("acc_PM_InvoiceDate").ToString()) = False Then
                    txtInvoiceDatePay.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("acc_PM_InvoiceDate").ToString(), "D")
                Else
                    txtInvoiceDatePay.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_PaidAmount").ToString()) = False Then
                    txtPaidAmount.Text = dt.Rows(0)("Acc_PM_PaidAmount").ToString()
                Else
                    txtPaidAmount.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_BalanceAmount").ToString()) = False Then
                    If dt.Rows(0)("Acc_PM_BalanceAmount") > 0 Then
                        lblAmount.Text = "Balance Amount " & dt.Rows(0)("Acc_PM_BalanceAmount") & ""
                    ElseIf dt.Rows(0)("Acc_PM_BalanceAmount") < 0 Then
                        sExtraAmount = dt.Rows(0)("Acc_PM_BalanceAmount")
                        If sExtraAmount.StartsWith("-") Then
                            sExtraAmount = sExtraAmount.Remove(0, 1)
                        End If
                        lblAmount.Text = "Extra Amount " & sExtraAmount & ""
                    End If
                End If

                If IsDBNull(dt.Rows(0)("Acc_PM_PaymentType").ToString()) = False Then
                    ddlPaymentTypePay.SelectedIndex = dt.Rows(0)("Acc_PM_PaymentType")
                Else
                    ddlPaymentTypePay.SelectedIndex = 0
                End If

                dtTrans = objPayment.LoadSavedTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("Acc_PM_ID"))
                dgPaymentDetails.DataSource = dtTrans
                dgPaymentDetails.DataBind()
                Session("dtPayment") = dtTrans

            End If


            'BindAllAttachments(sSession.AccessCode, iAttachID)
            'GetAttachFile(ddlExistPayment.SelectedItem.Text)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPayment()")
        End Try
    End Sub
    Public Sub ClearPayment()
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblStatus.Text = "" : lblStatusPay.Text = ""
            ddlExistPayment.SelectedIndex = 0
            txtTransactionNoPay.Text = objPayment.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)

            ddlBillTypePay.SelectedIndex = 0 : ddlPaymentTypePay.SelectedIndex = 0
            txtBillNoPay.Text = "" : txtBillDatePay.Text = "" : txtBillAmountPay.Text = "" : txtOtherDAmountPay.Text = "" : txtOtherCAmountPay.Text = ""
            txtInvoiceDatePay.Text = "" : ddlPartyPay.SelectedIndex = -1
            lblError.Text = "" : ddlDrOtherHeadPay.SelectedIndex = 0 : ddlCrOtherHeadPay.SelectedIndex = 0
            iAdd = 1

            lblStatusPay.Text = "Not Started"

            LoadSubGL()
            ddlDbOtherGLPay.DataSource = dt
            ddlDbOtherGLPay.DataBind()

            ddlCrOtherGLPay.DataSource = dt
            ddlCrOtherGLPay.DataBind()

            ddlTransType.SelectedIndex = 0 : ddlCustomerPartyPay.SelectedIndex = 0
            ddlBankName.Items.Clear() : txtChequeDate.Text = "" : txtChequeNo.Text = "" : txtBranchName.Text = "" : txtIFSC.Text = ""
            divcollapseChequeDetails.Visible = False

            txtPaidAmount.Text = 0

            'imgbtnUpdate.Visible = False
            Session("dtPayment") = Nothing
            dgPaymentDetails.DataSource = Nothing
            dgPaymentDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearPayment()")
        End Try
    End Sub
    Public Sub ClearJE()
        Dim dtNew As New DataTable
        Try
            lblError.Text = "" : lblRPJ.Text = ""
            dtMerge = dtNew : Session("Datatable") = Nothing
            txtTrNo.Text = ""
            ddlExistingTrnRPJ.SelectedValue = Nothing
            txtTrNo.Text = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerParty.SelectedIndex = 0 : ddlCSP.SelectedIndex = -1 : txtInvoiceDate.Text = ""
            ddlBillType.SelectedIndex = 0 : txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = ""
            ddlParty.SelectedIndex = 0 : ddlBillType.SelectedIndex = 0 : txtInvoiceDate.Text = "" : ddlCustomerParty.SelectedIndex = 0
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = "" : ddlJEType.SelectedIndex = 0
            ddlDrOtherHead.SelectedIndex = 0 : ddlCrOtherHead.SelectedIndex = 0 : lblStatus.Text = "" : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            dgPaymentDetails.DataSource = Nothing
            dgPaymentDetails.DataBind()
            LoadSubGL()
            ddlDbOtherGL.DataSource = dtNew
            ddlDbOtherGL.DataBind()

            ddlCrOtherGL.DataSource = dtNew
            ddlCrOtherGL.DataBind()

            Session("JE") = Nothing
            dgPetty.DataSource = Nothing
            dgPetty.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearJE")
        End Try
    End Sub
    Public Sub BindJE()
        Dim dt As New DataTable, dtTrans As New DataTable
        Try
            lblRPJ.Text = ""
            dt = objJE.GetPaymentTypeDetails(sSession.AccessCode, sSession.AccessCodeID, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dt.Rows.Count > 0 Then

                If IsDBNull(dt.Rows(0)("acc_JE_AttachID")) = False Then
                    iAttachID = dt.Rows(0)("acc_JE_AttachID").ToString()
                Else
                    iAttachID = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_ID")) = False Then
                    LoadExistingTransactionRPJ(dt.Rows(0)("Acc_JE_ID"))
                    ddlExistingTrnRPJ.SelectedIndex = dt.Rows(0)("Acc_JE_ID")
                Else
                    ddlExistingTrnRPJ.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_TransactionNo").ToString()) = False Then
                    txtTrNo.Text = dt.Rows(0)("Acc_JE_TransactionNo").ToString()
                Else
                    txtTrNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_Location").ToString()) = False Then
                    ddlCustomerParty.SelectedIndex = dt.Rows(0)("Acc_JE_Location").ToString()
                Else
                    ddlCustomerParty.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_Party").ToString()) = False Then
                    If dt.Rows(0)("Acc_JE_Location") > 0 Then
                        LoadParty(dt.Rows(0)("Acc_JE_Location"))
                        ddlCSP.SelectedValue = dt.Rows(0)("Acc_JE_Party").ToString()
                    Else
                        ddlCSP.Items.Clear()
                    End If
                Else
                    ddlCSP.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_BillType").ToString()) = False Then
                    ddlBillType.SelectedValue = dt.Rows(0)("Acc_JE_BillType").ToString()
                Else
                    ddlBillType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_BillNo").ToString()) = False Then
                    txtBillNo.Text = dt.Rows(0)("Acc_JE_BillNo").ToString()
                Else
                    txtBillNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_BillDate").ToString()) = False Then
                    txtBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_JE_BillDate").ToString(), "D")
                Else
                    txtBillDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("Acc_JE_BillAmount").ToString()) = False Then
                    txtBillAmount.Text = dt.Rows(0)("Acc_JE_BillAmount").ToString()
                Else
                    txtBillAmount.Text = ""
                End If

                If dt.Rows(0)("Acc_JE_Status").ToString() = "W" Then
                    lblRPJ.Text = "Waiting for Approval"
                ElseIf dt.Rows(0)("Acc_JE_Status").ToString() = "D" Then
                    lblRPJ.Text = "De-Activated"
                ElseIf dt.Rows(0)("Acc_JE_Status").ToString() = "A" Then
                    lblRPJ.Text = "Activated"
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_InvoiceDate").ToString()) = False Then
                    txtInvoiceDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_JE_InvoiceDate").ToString(), "D")
                Else
                    txtInvoiceDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_JEType").ToString()) = False Then
                    ddlJEType.SelectedIndex = dt.Rows(0)("Acc_JE_JEType").ToString()
                Else
                    ddlJEType.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("Acc_JE_PaymentNarration").ToString()) = False Then
                    txtNarration.Text = dt.Rows(0)("Acc_JE_PaymentNarration").ToString()
                Else
                    txtNarration.Text = ""
                End If

                dtTrans = objJE.LoadSavedTransactionDetailsJE(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("ACC_JE_ID"))
                dgPaymentDetails.DataSource = dtTrans
                dgPaymentDetails.DataBind()
                Session("DataTable") = dtTrans

                If dtTrans.Rows.Count = 0 Then
                    dgPaymentDetails.Visible = False
                End If
                Dim dtPetty As New DataTable
                dtPetty = objJE.BindPettyDetails(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("ACC_JE_ID"))
                dgPetty.DataSource = dtPetty
                dgPetty.DataBind()
                Session("JE") = dtPetty
                If dtPetty.Rows.Count = 0 Then
                    dgPetty.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindJE()")
        End Try
    End Sub
    Public Sub BindPettyCash()
        Dim dt As New DataTable, dtTrans As New DataTable
        Dim iAdvanceAmount As Integer = 0
        Dim dtPetty As New DataTable
        Try
            lblRPJ.Text = ""
            dt = objPC.GetPaymentDetails(sSession.AccessCode, sSession.AccessCodeID, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dt.Rows.Count > 0 Then

                If IsDBNull(dt.Rows(0)("Acc_PCM_AttachID")) = False Then
                    iAttachID = dt.Rows(0)("Acc_PCM_AttachID").ToString()
                Else
                    iAttachID = ""
                End If
                If IsDBNull(dt.Rows(0)("Acc_PCM_ID")) = False Then
                    LoadExistingTransactionRPJ(dt.Rows(0)("Acc_PCM_ID"))
                    ddlExistingTrnRPJ.SelectedValue = dt.Rows(0)("Acc_PCM_ID")
                Else
                    ddlExistingTrnRPJ.SelectedIndex = 0
                End If


                If IsDBNull(dt.Rows(0)("Acc_PCM_TransactionNo").ToString()) = False Then
                    txtTrNo.Text = dt.Rows(0)("Acc_PCM_TransactionNo").ToString()
                Else
                    txtTrNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_Location").ToString()) = False Then
                    ddlCustomerParty.SelectedIndex = dt.Rows(0)("Acc_PCM_Location").ToString()
                Else
                    ddlCustomerParty.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_Party").ToString()) = False Then
                    If dt.Rows(0)("Acc_PCM_Location") > 0 Then
                        LoadParty(dt.Rows(0)("Acc_PCM_Location"))
                        ddlCSP.SelectedValue = dt.Rows(0)("Acc_PCM_Party").ToString()
                    Else
                        ddlCSP.Items.Clear()
                    End If
                Else
                    ddlCSP.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_BillType").ToString()) = False Then
                    If dt.Rows(0)("Acc_PCM_BillType") > 0 Then
                        ddlBillType.SelectedValue = dt.Rows(0)("Acc_PCM_BillType")
                    Else
                        ddlBillType.SelectedIndex = 0
                    End If
                Else
                    ddlBillType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_BillNo").ToString()) = False Then
                    txtBillNo.Text = dt.Rows(0)("Acc_PCM_BillNo").ToString()
                Else
                    txtBillNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_BillDate").ToString()) = False Then
                    If (objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_PCM_BillDate").ToString(), "D") <> "01/01/1990" And objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_PCM_BillDate").ToString(), "D") <> "01-01-1990") Then
                        txtBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_PCM_BillDate").ToString(), "D")
                    Else
                        txtBillDate.Text = ""
                    End If
                Else
                    txtBillDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_BillAmount").ToString()) = False Then
                    txtBillAmount.Text = dt.Rows(0)("Acc_PCM_BillAmount").ToString()
                Else
                    txtBillAmount.Text = ""
                End If


                If dt.Rows(0)("Acc_PCM_Status").ToString() = "W" Then
                    lblRPJ.Text = "Waiting for Approval"
                ElseIf dt.Rows(0)("Acc_PCM_Status").ToString() = "D" Then
                    lblRPJ.Text = "De-Activated"
                ElseIf dt.Rows(0)("Acc_PCM_Status").ToString() = "A" Then
                    lblRPJ.Text = "Activated"
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_InvoiceDate").ToString()) = False Then
                    txtInvoiceDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_PCM_InvoiceDate").ToString(), "D")
                Else
                    txtInvoiceDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_PaymentNarration").ToString()) = False Then
                    txtNarration.Text = dt.Rows(0)("Acc_PCM_PaymentNarration").ToString()
                Else
                    txtNarration.Text = ""
                End If

                '///Preeti
                dtTrans = objPC.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("Acc_PCM_ID"))
                dgPettyCashDetails.DataSource = dtTrans
                dgPettyCashDetails.DataBind()
                Session("DataTable") = dtTrans
                dtPetty = objPC.BindPettyDetails(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("Acc_PCM_ID"))
                If dtPetty.Rows.Count > 0 Then
                    dgPetty.DataSource = dtPetty
                    dgPetty.DataBind()
                    Session("Petty") = dtPetty
                Else
                    dgPetty.DataSource = Nothing
                    dgPetty.DataBind()
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPettyCash()")
        End Try
    End Sub

    Public Sub CleaPettyCash()
        Dim dtNew As New DataTable
        Try
            lblError.Text = "" : lblRPJ.Text = ""
            dtMerge = dtNew : Session("Datatable") = Nothing
            txtTrNo.Text = ""
            ddlExistingTrnRPJ.SelectedValue = Nothing
            txtTrNo.Text = objPC.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerParty.SelectedIndex = 0 : ddlCSP.SelectedIndex = -1 : txtInvoiceDate.Text = ""
            ddlBillType.SelectedIndex = 0 : txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = ""

            ddlExisting.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.SelectedIndex = 0 : ddlBillType.SelectedIndex = 0
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = "" : ddlDrOtherHead.SelectedIndex = 0 : ddlCrOtherHead.SelectedIndex = 0
            lblStatus.Text = "" : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False

            dgPettyCashDetails.DataSource = dtNew
            dgPettyCashDetails.DataBind()
            LoadSubGL()
            ddlDbOtherGL.DataSource = dtNew
            ddlDbOtherGL.DataBind()

            ddlCrOtherGL.DataSource = dtNew
            ddlCrOtherGL.DataBind()

            Session("Petty") = Nothing
            dgPetty.DataSource = Nothing
            dgPetty.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearPettyCash()")
        End Try
    End Sub
    Public Sub BindReceiptDetails()
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim sExtraAmount As String = ""
        Try
            lblRPJ.Text = ""
            dt = objRecp.GetReceiptDetails(sSession.AccessCode, sSession.AccessCodeID, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dt.Rows.Count > 0 Then
                imgbtnSaveRPJ.ImageUrl = "~/Images/Update16.png"
                If IsDBNull(dt.Rows(0)("Acc_RM_AttachID")) = False Then
                    iAttachID = dt.Rows(0)("Acc_RM_AttachID").ToString()
                Else
                    iAttachID = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_ID").ToString()) = False Then
                    LoadExistingTransactionRPJ(dt.Rows(0)("Acc_RM_ID"))
                    ddlExistingTrnRPJ.SelectedValue = Trim(dt.Rows(0)("Acc_RM_ID"))
                Else
                    ddlExistingTrnRPJ.SelectedValue = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_TransactionNo").ToString()) = False Then
                    txtTrNo.Text = dt.Rows(0)("Acc_RM_TransactionNo").ToString()
                Else
                    txtTrNo.Text = ""
                End If


                If IsDBNull(dt.Rows(0)("ACC_RM_Location").ToString()) = False Then
                    ddlCustomerParty.SelectedIndex = Trim(dt.Rows(0)("ACC_RM_Location"))
                Else
                    ddlCustomerParty.SelectedIndex = 0
                End If


                If IsDBNull(dt.Rows(0)("Acc_RM_Party").ToString()) = False Then
                    If dt.Rows(0)("ACC_RM_Location") > 0 Then
                        LoadParty(dt.Rows(0)("ACC_RM_Location"))
                        ddlCSP.SelectedValue = dt.Rows(0)("Acc_RM_Party").ToString()
                    Else
                        ddlCSP.Items.Clear()
                    End If
                Else
                    ddlCSP.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_TransactionType").ToString()) = False Then
                    ddlReceiptTrType.SelectedIndex = dt.Rows(0)("Acc_RM_TransactionType").ToString()
                Else
                    ddlReceiptTrType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillType").ToString()) = False Then
                    ddlReceiptVoucherType.SelectedValue = dt.Rows(0)("Acc_RM_BillType").ToString()
                Else
                    ddlReceiptVoucherType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillNo").ToString()) = False Then
                    txtReceiptInvoiceNo.Text = dt.Rows(0)("Acc_RM_BillNo").ToString()
                Else
                    txtReceiptInvoiceNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillDate").ToString()) = False Then
                    txtReceiptBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_BillDate").ToString(), "D")
                Else
                    txtReceiptBillDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillAmount").ToString()) = False Then
                    txtReceiptInvoiceAmt.Text = dt.Rows(0)("Acc_RM_BillAmount").ToString()
                Else
                    txtReceiptInvoiceAmt.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_BillNarration").ToString()) = False Then
                    txtNarration.Text = dt.Rows(0)("Acc_RM_BillNarration").ToString()
                Else
                    txtNarration.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("ACC_RM_ChequeNo").ToString()) = False Then
                    txtChequeNo.Text = dt.Rows(0)("ACC_RM_ChequeNo").ToString()
                Else
                    txtChequeNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_ChequeDate").ToString()) = False Then
                    If dt.Rows(0)("Acc_RM_ChequeDate").ToString() <> "" Then
                        txtChequeDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_ChequeDate").ToString(), "D")
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
                    lblRPJ.Text = "Waiting for Approval"
                ElseIf dt.Rows(0)("Acc_RM_Status").ToString() = "D" Then
                    lblRPJ.Text = "De-Activated"
                ElseIf dt.Rows(0)("Acc_RM_Status").ToString() = "A" Then
                    lblRPJ.Text = "Activated"
                End If

                If IsDBNull(dt.Rows(0)("acc_RM_InvoiceDate").ToString()) = False Then
                    txtReceiptBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("acc_RM_InvoiceDate").ToString(), "D")
                Else
                    txtReceiptBillDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_PaidAmount").ToString()) = False Then
                    txtReceiptPaidAmt.Text = dt.Rows(0)("Acc_RM_PaidAmount").ToString()
                Else
                    txtReceiptPaidAmt.Text = ""
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

                If IsDBNull(dt.Rows(0)("Acc_RM_PaymentType").ToString()) = False Then
                    ddlReceiptType.SelectedIndex = dt.Rows(0)("Acc_RM_PaymentType")
                Else
                    ddlReceiptType.SelectedIndex = 0
                End If

                If ddlReceiptType.SelectedIndex = 1 Or ddlReceiptType.SelectedIndex = 4 Then
                    If IsDBNull(dt.Rows(0)("Acc_RM_OrderNO")) = False Then
                        If dt.Rows(0)("Acc_RM_OrderNO") > 0 Then
                            txtReceiptSalesOrderNo.Text = dt.Rows(0)("Acc_RM_OrderNO")
                        Else
                            txtReceiptSalesOrderNo.Text = 0
                        End If
                    Else
                        txtReceiptSalesOrderNo.Text = 0
                    End If
                Else
                    txtReceiptSalesOrderNo.Text = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_RM_OrderDate").ToString()) = False Then
                    If (dt.Rows(0)("Acc_RM_OrderDate").ToString() <> "") Then
                        If (dt.Rows(0)("Acc_RM_OrderDate").ToString() <> "01/01/1900 12:00:00 AM") Then
                            txtInvoiceDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_RM_OrderDate").ToString(), "D")
                        Else
                            txtInvoiceDate.Text = ""
                        End If
                    Else
                        txtInvoiceDate.Text = ""
                    End If
                Else
                    txtInvoiceDate.Text = ""
                End If

                dtDetails = objRecp.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("Acc_RM_ID"))
                Session("dtReceipt") = dtDetails
                dgPaymentDetails.DataSource = dtDetails
                dgPaymentDetails.DataBind()

            End If
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
    Public Sub BindBankNamePayment()
        Try
            ' If ddlCrGL.SelectedIndex > 0 Then
            ddlBankPay.DataSource = objPayment.LoadBankNames(sSession.AccessCode, sSession.AccessCodeID)
            ddlBankPay.DataTextField = "GL_Desc"
            ddlBankPay.DataValueField = "GL_Id"
            ddlBankPay.DataBind()
            ddlBankPay.Items.Insert(0, "Select Bank Name")
            ' End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearReceipt()
        Dim dt As New DataTable
        Try
            ddlExistingTrnRPJ.SelectedValue = Nothing
            txtTrNo.Text = ""
            lblRPJ.Text = ""
            txtTrNo.Text = objRecp.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)

            ddlBillType.SelectedIndex = 0
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = "" : lblStatus.Text = ""
            lblError.Text = ""
            ddlDrOtherHead.SelectedIndex = 0 : ddlCrOtherHead.SelectedIndex = 0

            LoadSubGL()
            ddlDbOtherGL.DataSource = dt
            ddlDbOtherGL.DataBind()
            ddlCrOtherGL.DataSource = dt
            ddlCrOtherGL.DataBind()
            ddlTransType.SelectedIndex = 0
            ddlCustomerParty.SelectedIndex = 0 : ddlCSP.SelectedIndex = -1 : txtInvoiceDate.Text = ""
            ddlBankName.Items.Clear() : txtChequeDate.Text = "" : txtChequeNo.Text = "" : txtBranchName.Text = "" : txtIFSC.Text = ""
            ddlReceiptTrType.SelectedIndex = 0 : ddlReceiptVoucherType.SelectedIndex = 0 : txtReceiptInvoiceNo.Text = ""
            txtReceiptBillDate.Text = "" : txtReceiptInvoiceAmt.Text = "" : txtNarration.Text = "" : txtReceiptBillDate.Text = ""
            txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFSC.Text = ""
            txtBranchName.Text = "" : txtReceiptPaidAmt.Text = "" : txtReceiptInvoiceAmt.Text = ""
            txtReceiptSalesOrderNo.Text = ""
            ddlReceiptType.SelectedIndex = 0

            divcollapseChequeDetails.Visible = False
            txtPaidAmount.Text = 0
            imgbtnSaveRPJ.ImageUrl = "~/Images/Save16.png"

            dgPaymentDetails.DataSource = Nothing
            dgPaymentDetails.DataBind()
            ddlPaymentType.SelectedIndex = 0 : txtInvoiceDate.Text = "" : lblAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearReceipt()")
        End Try
    End Sub
    Public Sub BindPODetails()
        Try
            dgPurchase.DataSource = objPO.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            dgPurchase.DataBind()
            dt = objPO.LoadPurchaseOderMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("POM_OrderNo").ToString()) = False Then
                    ddlExistingOrder.SelectedValue = dt.Rows(0)("POM_ID")
                Else
                    ddlExistingOrder.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("POM_DcNo")) = False Then
                    txtVoucherPO.Text = dt.Rows(0)("POM_DcNo")
                Else
                    txtVoucherPO.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("POM_OrderNo").ToString()) = False Then
                    txtOrderCode.Text = dt.Rows(0)("POM_OrderNo").ToString()
                Else
                    txtOrderCode.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("POM_OrderDate").ToString()) = False Then
                    txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("POM_OrderDate"), "D")
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

                If IsDBNull(dt.Rows(0)("POM_Status").ToString()) = False Then
                    If (dt.Rows(0)("POM_Status") = "W") Then
                        txtOrderDate.Enabled = True
                        lblPO.Text = "Waiting For approval"
                    ElseIf dt.Rows(0)("POM_Status") = "A" Then
                        lblPO.Text = "Approved."
                        txtOrderDate.Enabled = False
                    Else
                    End If
                End If
                If txtDeliveryFromAddress.Text <> "" Then
                    chkboxFrom.Checked = True
                Else
                    chkboxFrom.Checked = False
                End If

                If txtDeleveryAddress.Text <> "" Then
                    chkboxTo.Checked = True
                Else
                    chkboxTo.Checked = False
                End If

                If ddlGSTCategory.SelectedIndex <> -1 Then
                    Dim description As String
                    description = objPO.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryFromGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNo.Enabled = True
                    End If
                End If

                'If IsDBNull(dt.Rows(0)("POM_Status").ToString()) = False Then
                '    If (dt.Rows(0)("POM_Status") = "W") Then
                '        txtOrderDate.Enabled = True
                '        lblStatus.Text = "Waiting For approval"
                '    ElseIf dt.Rows(0)("POM_Status") = "A" Then
                '        lblStatus.Text = "Approved."
                '        txtOrderDate.Enabled = False
                '    Else
                '    End If
                'End If

                Dim dtCharge As New DataTable
                dtCharge = objPO.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, 0, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedItem.Text)
                GvCharge.DataSource = dtCharge
                GvCharge.DataBind()
                Session("ChargesMaster") = dtCharge

                GetAttachFilePO(ddlBatchNo.SelectedValue, lstFiles.SelectedValue)

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub ClearPurchaseAll()
        Try
            lblError.Text = "" : lblPO.Text = ""
            lblStatusGIN.Text = "" : lblStatusPR.Text = "" : lblStatus.Text = "" : txtVoucherPO.Text = ""

            GvCharge.DataSource = Nothing
            GvCharge.DataBind()

            ddlNumberOfDays.SelectedIndex = 0 : chkboxFrom.Checked = False : chkboxTo.Checked = False

            For i = 0 To chkCategory.Items.Count - 1
                chkCategory.Items(i).Selected = False
            Next
            ddlExistingOrder.SelectedIndex = 0
            ddlUnit.Items.Clear()
            txtRate.Text = ""

            hfDiscountAmount.Value = ""
            hfGSTAmount.Value = ""

            ddlSupplier.SelectedIndex = 0 : txtQuantity.Text = "" : txtRateAmount.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = ""

            txtTotalAmount.Text = ""
            txtOrderDate.Enabled = True

            txtOrderCode.Text = "" : txtOrderDate.Text = "" : ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.Items.Clear() : txtBillingAddress.Text = "" : txtBillingGSTNRegNo.Text = ""
            txtDeliveryFromAddress.Text = "" : txtDeliveryFromGSTNRegNo.Text = "" : ddlDSchedule.SelectedIndex = 0 : ddlMPayment.SelectedIndex = 0 : ddlPterms.SelectedIndex = 0

            dgPurchase.DataSource = Nothing
            dgPurchase.DataBind()
            Session("ChargesMaster") = Nothing
            GenerateOrderCodeAnddate()

            ClearPurchaseItemdetails()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearPurchaseItemdetails()
        Try
            For i = 0 To chkCategory.Items.Count - 1
                chkCategory.Items(i).Selected = False
            Next
            ddlUnit.Items.Clear()
            txtRate.Text = ""

            hfDiscountAmount.Value = ""
            hfGSTAmount.Value = ""

            txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtGSTRate.Text = "" : txtGSTAmount.Text = ""

            txtTotalAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearPurchaseItemdetails()")
        End Try
    End Sub
    Public Sub ClearAllDataCapture()
        Try
            ddlAccZone.SelectedIndex = 0 : ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear() : lblStatus.Text = ""
            txtVoucherNo.Text = "" : txtDate.Text = "" : txtTransactionNo.Text = "" : ddlPaymentType.SelectedIndex = 0 : ddlExisting.Items.Clear() : ddlParty.SelectedIndex = 0

            dgPaymentDetails.DataSource = Nothing
            dgPaymentDetails.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindSODetails()
        Dim dtMaster As New DataTable
        Dim bCheck As String = ""
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If lstFiles.SelectedValue > 0 Then

                lstBoxDescription.Enabled = True
                dtMaster = objSO.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("SPO_ID")) = False Then
                            ddlExistingSalesNo.SelectedValue = dtMaster.Rows(i)("SPO_ID")
                        Else
                            ddlExistingSalesNo.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_OrderCode")) = False Then
                            txtOrderCodeS.Text = dtMaster.Rows(i)("SPO_OrderCode")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_OrderDate")) = False Then
                            txtOrderDateS.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_OrderDate"), "D")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_ModeOfDispatch")) = False Then
                            If dtMaster.Rows(i)("SPO_ModeOfDispatch") > 0 Then
                                ddlShipping.SelectedValue = dtMaster.Rows(i)("SPO_ModeOfDispatch")
                            Else
                                ddlShipping.SelectedIndex = 0
                            End If
                        Else
                            ddlShipping.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_SalesManID")) = False Then
                            If dtMaster.Rows(i)("SPO_SalesManID") > 0 Then
                                ddlSalesMan.SelectedValue = dtMaster.Rows(i)("SPO_SalesManID")
                            Else
                                ddlSalesMan.SelectedIndex = 0
                            End If
                        Else
                            ddlSalesMan.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_BuyerOrderNo")) = False Then
                            txtBuyerPurOrderNo.Text = dtMaster.Rows(i)("SPO_BuyerOrderNo")
                        Else
                            txtBuyerPurOrderNo.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_BuyerOrderDate")) = False Then
                            If (dtMaster.Rows(i)("SPO_BuyerOrderDate")) <> "1899-12-30 00:00:00.000" Then
                                txtBuyerOrderDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_BuyerOrderDate"), "D")
                            Else
                                txtBuyerOrderDate.Text = ""
                            End If
                        Else
                            txtBuyerOrderDate.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_Category")) = False Then
                            If dtMaster.Rows(i)("SPO_Category") > 0 Then
                                ddlCategory.SelectedValue = dtMaster.Rows(i)("SPO_Category")
                            Else
                                ddlCategory.SelectedIndex = 0
                            End If
                        Else
                            ddlCategory.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_Remarks")) = False Then
                            txtRemarks.Text = dtMaster.Rows(i)("SPO_Remarks")
                        Else
                            txtRemarks.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_ShippingDate")) = False Then
                            If (dtMaster.Rows(i)("SPO_ShippingDate")) <> "1899-12-30 00:00:00.000" Then
                                txtShippingDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_ShippingDate"), "D")
                            Else
                                txtShippingDate.Text = ""
                            End If
                        Else
                            txtShippingDate.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_PaymentType")) = False Then
                            ddlPaymentTypeS.SelectedValue = dtMaster.Rows(i)("SPO_PaymentType")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_PartyCode")) = False Then
                            txtPartyNo.Text = dtMaster.Rows(i)("SPO_PartyCode")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_PartyName")) = False Then
                            ddlPatryS.SelectedValue = dtMaster.Rows(i)("SPO_PartyName")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_ContantNo")) = False Then
                            txtContactNo.Text = dtMaster.Rows(i)("SPO_ContantNo")
                        Else
                            txtContactNo.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_Address")) = False Then
                            txtAddress.Text = dtMaster.Rows(i)("SPO_Address")
                        Else
                            txtAddress.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_ModeOfCommunication")) = False Then
                            If dtMaster.Rows(i)("SPO_ModeOfCommunication") > 0 Then
                                ddlModeOfCommunication.SelectedValue = dtMaster.Rows(i)("SPO_ModeOfCommunication")
                            Else
                                ddlModeOfCommunication.SelectedIndex = 0
                            End If
                        Else
                            ddlModeOfCommunication.SelectedIndex = 0
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_InputBy")) = False Then
                            txtInputBy.Text = dtMaster.Rows(i)("SPO_InputBy")
                        Else
                            txtInputBy.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_ShippingCharge")) = False Then
                            If dtMaster.Rows(i)("SPO_ShippingCharge") > 0 Then
                                ddlShippingCharges.SelectedValue = dtMaster.Rows(i)("SPO_ShippingCharge")
                            Else
                                ddlShippingCharges.SelectedIndex = 0
                            End If
                        Else
                            ddlShippingCharges.SelectedIndex = 0
                        End If

                    Next
                End If
                'lstBoxDescription.Items.Clear()
                LoadExistingOrderGrid(ddlBatchNo.SelectedValue, lstFiles.SelectedValue)

                GetAttachFileSO(ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            Else
                txtsearch.Text = ""
                SalesClear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSoDetails()")
        End Try
    End Sub
    Private Sub lstDocument_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDocument.SelectedIndexChanged
        Dim dtDocument As New DataTable
        Dim i As Integer
        Try
            If lstDocument.Items.Count <> -1 Then
                lstFiles.Items.Clear()
                If lstDocument.SelectedItem.Text <> "" Then
                    dtDocument = objclsview.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                    If dtDocument.Rows.Count <> 0 Then
                        For i = 0 To dtDocument.Rows.Count - 1
                            lstFiles.Items.Add(dtDocument.Rows(i)("pge_basename"))
                        Next
                        txtNav.Text = 1
                        lblNav.Text = "/" & lstFiles.Items.Count
                    End If
                    If lstFiles.Items.Count = 1 Then
                        txtNav.Enabled = False
                    End If
                    If lstFiles.Items.Count <> 0 Then
                        lstFiles.SelectedIndex = 0
                    End If
                Else
                    lblError.Text = "No documents found."
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name.','', 'error');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstDocument_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Try
            txtNav.Text = "" : lblNav.Text = ""

            If ddlCabinet.SelectedIndex > 0 Then
                BindexistingSubCab()
                'ddlSubCabinet_SelectedIndexChanged(sender, e)
            Else
                ddlSubCabinet.Items.Clear()
            End If
            If Request.QueryString("SubCabID") IsNot Nothing Then
                BindexistingSubCab()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlFolder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFolder.SelectedIndexChanged
        Dim lblDetID As New Label
        Dim sImagePath As String = ""
        Dim BaseID As Integer, i As Integer = 0
        Dim FileSelectedID As String
        Dim oImagePath As Object
        Dim oSelectedDocTypeID As Object
        Dim dt As New DataTable
        Dim aSelectedChecksIDs() As String
        Dim sRFID As String = ""
        Try
            'Clear
            txtNav.Text = "" : lblNav.Text = ""
            lstDocument.Items.Clear()
            lstFiles.Items.Clear()
            sSelId = String.Empty : sSelectedChecksIDs = String.Empty
            sSelectedCabID = String.Empty : sSelectedSubCabID = String.Empty : sSelectedFolID = String.Empty
            sSelectedDocTypeID = String.Empty : sSelectedKWID = String.Empty : sSelectedDescID = String.Empty
            sSelectedFrmtID = String.Empty : sSelectedCrByID = String.Empty
            iSelectedIndexID = 0 : iSelectedFirstID = 0 : iSelectedImageID = 0
            sDetailsId = ""
            If (ddlFolder.SelectedIndex > 0) Then
                lblError.Text = ""
                dt = objclsview.LoadBaseIdFromFolder(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue, ddlFolder.SelectedValue, sRFID)
                If (dt.Rows.Count > 0) Then
                    BaseID = dt.Rows(0).Item("PGE_BASENAME")
                    FileSelectedID = dt.Rows(0).Item("PGE_BASENAME")
                    iSelectedImageID = FileSelectedID

                    sSelectedDocTypeID = dt.Rows(0).Item("PGE_DOCUMENT_TYPE")
                    For i = 0 To dt.Rows.Count - 1
                        sDetailsId = sDetailsId & "," & dt.Rows(i).Item("PGE_BASENAME")
                        If (sDetailsId.Length > 0) Then
                            If (sDetailsId.Chars(0).ToString = ",") Then
                                sDetailsId = sDetailsId.Remove(0, 1)
                            End If
                        End If
                    Next
                End If
                sSelectedChecksIDs = sDetailsId
                If Not sSelectedChecksIDs Is Nothing Then
                    If (sSelectedChecksIDs.Length > 0) Then
                        If (sSelectedChecksIDs.Chars(0).ToString = ",") Then
                            sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                        End If
                        aSelectedChecksIDs = sSelectedChecksIDs.Split(",")
                        If aSelectedChecksIDs.Length > 0 Then
                            iSelectedFirstID = aSelectedChecksIDs(0)
                        End If
                    End If
                End If
                oImagePath = objclsview.GetPageFromEdict(sSession.AccessCode, BaseID, sSession.UserID)

                sSelId = sSelId
                sSelectedCabID = ddlCabinet.SelectedValue
                sSelectedSubCabID = ddlSubCabinet.SelectedValue
                sSelectedFolID = ddlFolder.SelectedValue
                oSelectedDocTypeID = sSelectedDocTypeID
                sSelectedKWID = sSelectedKWID
                sSelectedDescID = sSelectedDescID
                sSelectedFrmtID = sSelectedFrmtID
                sSelectedCrByID = sSelectedCrByID


                'Files Details
                txtID.Text = "0" : txtPreId.Text = "1" : iPageNext = 0
                sBaseName = sSelectedChecksIDs.Split(",")
                For i = 0 To sBaseName.Length - 1
                    lstDocument.Items.Add(sBaseName(i))
                Next
                txtNavDoc.Text = 1
                lblNavDoc.Text = "/" & lstDocument.Items.Count
                If lstDocument.Items.Count = 1 Then
                    txtNavDoc.Enabled = False
                End If
                sSelectedDocTypeID = 0
                FileSelectedID = 0
                Dim iDocSelectedID As Integer = 0, iFileSelectedID As Integer = 0
                If lstDocument.Items.Count <> 0 Then
                    If sSelectedDocTypeID IsNot Nothing Then
                        Try
                            iDocSelectedID = 0
                            txtNavDoc.Text = iDocSelectedID + 1
                        Catch ex As Exception
                        End Try
                    End If
                    lstDocument.SelectedIndex = iDocSelectedID
                    lstDocument_SelectedIndexChanged(sender, e)
                End If
                If lstFiles.Items.Count <> 0 Then
                    If FileSelectedID IsNot Nothing Then
                        Try
                            iFileSelectedID = 0
                            txtNav.Text = iFileSelectedID + 1
                            iPageNext = iFileSelectedID
                        Catch ex As Exception
                        End Try
                    End If
                    lstFiles.SelectedIndex = iFileSelectedID
                    lstFiles_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFolder_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlSubCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubCabinet.SelectedIndexChanged
        Try
            If ddlSubCabinet.SelectedIndex > 0 Then
                BindexistingFolder()
            Else
                ddlFolder.Items.Clear()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubCabinet_SelectedIndexChanged")
        End Try
    End Sub
    '****************************************************************************************************************

    Private Sub imgbtnSendBackImage_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSendBackImage.Click
        Dim sFromEmailID As String = ""
        Try
            If sInvalidImageIDs <> "" Then
                'sFromEmailID = objDataCap.GetLoginUserEmailID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)

                'If sFromEmailID <> "" Then
                '    txtEmailFrom.Enabled = False
                '    txtEmailFrom.Text = sFromEmailID
                'Else
                '    txtEmailFrom.Enabled = True
                'End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#mySendMail').modal('show');", True)
            Else
                lblError.Text = "No documents found."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#mySendMail').modal('hide');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSendBackImage_Click")
        End Try
    End Sub

    Private Sub btnSendMail_Click(sender As Object, e As EventArgs) Handles btnSendMail.Click
        Dim iCount As Integer = 0
        Dim sToEmailIDs As String = "", sToEmails As String = ""
        Try
            lblError.Text = "" : lblSendMailModelError.Text = ""

            For j = 0 To lstUsers.Items.Count - 1
                If lstUsers.Items(j).Selected = True Then
                    iCount = 1
                End If
            Next
            If iCount = 0 And txtEmailTo.Text = "" Then
                lblSendMailModelError.Text = "Select Users/Enter EmailID."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#mySendMail').modal('show');", True)
                Exit Sub
            End If

            For j = 0 To lstUsers.Items.Count - 1
                If lstUsers.Items(j).Selected = True Then
                    sToEmailIDs = sToEmailIDs & "," & Val(lstUsers.Items(j).Value)
                    iCount = 1
                End If
            Next

            If iCount > 0 Then
                If sToEmailIDs.StartsWith(",") Then
                    sToEmailIDs = sToEmailIDs.Remove(0, 1)
                End If
                If sToEmailIDs.EndsWith(",") Then
                    sToEmailIDs = sToEmailIDs.Remove(Len(sToEmailIDs) - 1, 1)
                End If

                sToEmails = objDataCap.LoadUserEmailIDs(sSession.AccessCode, sSession.AccessCodeID, sToEmailIDs)
            End If

            If txtEmailTo.Text <> "" Then
                sToEmails = sToEmails & "," & txtEmailTo.Text
            End If

            If sToEmails.StartsWith(",") Then
                sToEmails = sToEmails.Remove(0, 1)
            End If
            If sToEmails.EndsWith(",") Then
                sToEmails = sToEmails.Remove(Len(sToEmailIDs) - 1, 1)
            End If

            SendTodaysMail(sToEmails)
            'Extra'
            SaveIncompleteTransactions()
            'Extra
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#mySendMail').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSendMail_Click")
        End Try
    End Sub

    Public Sub SendTodaysMail(ByVal sToEmailIDs As String)
        Dim aInvalidImageIDs() As String, sImageIDs As String = "", sInvalidImageIDExt As String = "", sImagePKIDs As String = "", sInvalidImagePKIDs As String = ""
        Dim aImageIDs() As String
        Dim iEmailSentUserID As Integer = 0, iImageId As Integer = 0
        Try
            sImagePKIDs = "" : sInvalidImagePKIDs = ""
            If sInvalidImageIDs <> "" Then

                Using mm As New MailMessage(txtEmailFrom.Text, sToEmailIDs)
                    iEmailSentUserID = objDataCap.GetEmailSentUserID(sSession.AccessCode, sSession.AccessCodeID, txtEmailFrom.Text)

                    mm.Subject = txtSubject.Text
                    mm.Body = txtBody.Text

                    aInvalidImageIDs = sInvalidImageIDs.Split(",")
                    For i = 0 To aInvalidImageIDs.Length - 1
                        sImageIDs = aInvalidImageIDs(i)
                        Dim sAttachment = New System.Net.Mail.Attachment("C:\inetpub\wwwroot\FASPro\FAS\temp\BITMAPS\0\" & sImageIDs & "")
                        mm.Attachments.Add(sAttachment)

                        If sImageIDs <> "" Then
                            sInvalidImageIDExt = sInvalidImageIDExt & "," & sImageIDs
                        End If

                        If sInvalidImageIDExt.StartsWith(",") Then
                            sInvalidImageIDExt = sInvalidImageIDExt.Remove(0, 1)
                        End If
                        If sInvalidImageIDExt.EndsWith(",") Then
                            sInvalidImageIDExt = sInvalidImageIDExt.Remove(Len(sToEmailIDs) - 1, 1)
                        End If
                        sImagePKIDs = sImageIDs
                        If sImagePKIDs.Contains(".") = True Then
                            sImagePKIDs = sImagePKIDs.Substring(0, sImagePKIDs.Length - 4)
                            sInvalidImagePKIDs = sInvalidImagePKIDs & "," & sImagePKIDs
                        End If
                        If sInvalidImagePKIDs.StartsWith(",") Then
                            sInvalidImagePKIDs = sInvalidImagePKIDs.Remove(0, 1)
                        End If
                        sImageIDs = ""
                    Next

                    mm.IsBodyHtml = False
                    Dim smtp As New SmtpClient()
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    Dim NetworkCred As New NetworkCredential(txtEmailFrom.Text, txtPassword.Text)
                    smtp.UseDefaultCredentials = True
                    smtp.Credentials = NetworkCred
                    smtp.Port = 587
                    Try
                        smtp.Send(mm)
                        aImageIDs = sInvalidImagePKIDs.Split(",")
                        For i = 0 To aImageIDs.Length - 1
                            iImageId = aImageIDs(i)
                            objDataCap.SaveSentEmailDetails(sSession.AccessCode, iImageId, sSession.YearID, "DataCapture", txtEmailFrom.Text, sToEmailIDs, "", txtSubject.Text, txtBody.Text, "YES", "C:\GRACePA-INFO\BITMAPS\0\", sInvalidImageIDExt, iEmailSentUserID, sSession.UserID, sSession.IPAddress, sSession.AccessCodeID)
                        Next
                        lblSendMailModelError.Text = "Mail sent successfully."
                    Catch ex As Exception
                        aImageIDs = sInvalidImagePKIDs.Split(",")
                        For i = 0 To aImageIDs.Length - 1
                            iImageId = aImageIDs(i)
                            objDataCap.SaveSentEmailDetails(sSession.AccessCode, sInvalidImagePKIDs, sSession.YearID, "DataCapture", txtEmailFrom.Text, sToEmailIDs, "", txtSubject.Text, txtBody.Text, "NO", "C:\GRACePA-INFO\BITMAPS\0\", sInvalidImageIDExt, iEmailSentUserID, sSession.UserID, sSession.IPAddress, sSession.AccessCodeID)
                        Next
                        lblSendMailModelError.Text = "Failure Sending Mail."
                    End Try
                End Using
            Else
                lblSendMailModelError.Text = "No documents found."
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnCancelMail_Click(sender As Object, e As EventArgs) Handles btnCancelMail.Click
        Try
            lblError.Text = "" : lblSendMailModelError.Text = ""
            txtEmailFrom.Text = "" : txtEmailTo.Text = "" : txtPassword.Text = "" : txtSubject.Text = "" : sInvalidImageIDs = "" : lblBadgeCount.Text = 0
            For j = 0 To lstUsers.Items.Count - 1
                If lstUsers.Items(j).Selected = True Then
                    lstUsers.Items(j).Selected = False
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#mySendMail').modal('hide');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCancelMail_Click")
        End Try
    End Sub
    Private Sub imgbtnInvalidImage_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnInvalidImage.Click
        Dim sSelectedInvalidImage As String = ""
        Dim aInvalidImageIDs() As String
        Dim iImageAttacheCount As Integer = 0
        Try
            If iSelectedImageID > 0 Then
                sSelectedInvalidImage = iSelectedImageID & "." & sSelectedImageExt

                If sInvalidImageIDs <> "" Then
                    If sInvalidImageIDs.Contains(sSelectedInvalidImage) = True Then
                        lblError.Text = "Document already attached."
                    Else
                        sInvalidImageIDs = sInvalidImageIDs & "," & sSelectedInvalidImage
                    End If

                Else
                    sInvalidImageIDs = sInvalidImageIDs & "," & sSelectedInvalidImage
                End If
                If sInvalidImageIDs.StartsWith(",") Then
                    sInvalidImageIDs = sInvalidImageIDs.Remove(0, 1)
                End If
                If sInvalidImageIDs.EndsWith(",") Then
                    sInvalidImageIDs = sInvalidImageIDs.Remove(Len(sInvalidImageIDs) - 1, 1)
                End If

                aInvalidImageIDs = sInvalidImageIDs.Split(",")
                For i = 0 To aInvalidImageIDs.Length - 1
                    iImageAttacheCount = i + 1
                Next
                lblBadgeCount.Text = iImageAttacheCount
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnInvalidImage_Click")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            If lblStatus.Text = "Activated" Then
                lblError.Text = "This Transaction has been approved,it can not be update."
                Exit Sub
            End If
            If ddlTrType.SelectedItem.Text = "Petty Cash" Or ddlTrType.SelectedItem.Text = "Journal Entry" Then
            Else
                If ddlParty.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Supplier/Customer"
                    Exit Sub
                End If
            End If
            SaveUpdate()
            lblError.Text = "Successfully Saved"
            imgbtnSave.ImageUrl = "~Images/Update24.png"
            imgbtnUpdate.Visible = True : imgbtnSave.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Public Sub SaveUpdate()
        Dim objDC As ClsDataCapture.Data
        Dim Arr() As String
        Dim dDebit As Double = 0, dCredit As Double = 0, dSum As Double = 0, dSDebit As Double = 0
        Dim iMasterID As Integer
        Dim dDate, dSDate As Date
        Dim m As Integer
        Try
            'Cheque Date Comparision'
            If txtDate.Text <> "" Then
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Date (" & txtDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Date (" & txtDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtDate.Focus()
                    Exit Sub
                End If
            End If
            'Cheque Date Comparision'

            objDC.DC_ID = 0
            objDC.DC_TransactionNo = txtTransactionNo.Text
            objDC.DC_TrDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objDC.DC_Company = ddlCompany.SelectedValue
            objDC.DC_Customer = ddlParty.SelectedValue
            objDC.DC_TrType = ddlTrType.SelectedValue
            objDC.DC_BatchNo = ddlBatchNo.SelectedValue
            objDC.DC_VoucherNo = txtVoucherNo.Text
            objDC.DC_BASENAME = lstFiles.SelectedItem.Text  'BaseName ID
            objDC.DC_Zone = ddlAccZone.SelectedValue
            objDC.DC_Region = ddlAccRgn.SelectedValue
            objDC.DC_Area = ddlAccArea.SelectedValue
            objDC.DC_Branch = ddlAccBrnch.SelectedValue
            If ddlPaymentType.SelectedIndex > 0 Then
                objDC.DC_PaymentType = ddlPaymentType.SelectedValue
            Else
                objDC.DC_PaymentType = 0
            End If
            objDC.DC_Narration = txtNarration.Text
            objDC.DC_Delfalg = "W"
            objDC.DC_Status = "W"
            objDC.DC_CompID = sSession.AccessCodeID
            objDC.DC_YearID = sSession.YearID
            objDC.DC_CrBy = sSession.UserID
            objDC.DC_CrOn = System.DateTime.Today
            objDC.DC_UpdatedBy = sSession.UserID
            objDC.DC_UpdatedOn = System.DateTime.Today
            objDC.DC_Operation = "C"
            objDC.DC_IPAddress = sSession.IPAddress

            Arr = objDataCap.SaveDataCapture(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDC)
            iMasterID = Arr(1)

            BindExistingNo(ddlCompany.SelectedValue, ddlTrType.SelectedValue, ddlBatchNo.SelectedValue)
            ddlExisting.SelectedValue = iMasterID

            'Dim lblId As New Label
            'Extra'
            If dgPaymentDetails.Items.Count > 0 Then

                'Delete Transactions
                objDataCap.DeleteTransaction(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID, ddlTrType.SelectedValue)
                'Delete Transactions

                For i = 0 To dgPaymentDetails.Items.Count - 1

                    'lblId = dgPaymentDetails.Items(i).FindControl("lblId")
                    'If lblId.Text <> "" Then
                    '    objDataCap.iRATD_ID = lblId.Text
                    'Else
                    '    objDataCap.iRATD_ID = 0
                    'End If
                    objDataCap.iRATD_ID = 0
                    objDataCap.dRATD_TransactionDate = System.DateTime.Today
                    objDataCap.iRATD_TrType = ddlTrType.SelectedValue
                    objDataCap.iRATD_BillId = iMasterID
                    objDataCap.iRATD_PaymentType = 0
                    objDataCap.iRATD_DbOrCr = dgPaymentDetails.Items(i).Cells(12).Text
                    objDataCap.iRATD_Head = dgPaymentDetails.Items(i).Cells(1).Text
                    objDataCap.iRATD_GL = dgPaymentDetails.Items(i).Cells(2).Text
                    objDataCap.iRATD_SubGL = dgPaymentDetails.Items(i).Cells(3).Text
                    If objDataCap.iRATD_DbOrCr = 1 Then
                        dDebit = dgPaymentDetails.Items(i).Cells(9).Text
                        objDataCap.dRATD_Debit = dDebit
                        objDataCap.dRATD_Credit = 0.00
                    Else
                        dCredit = dgPaymentDetails.Items(i).Cells(10).Text
                        objDataCap.dRATD_Debit = 0.00
                        objDataCap.dRATD_Credit = dCredit
                    End If

                    objDataCap.iRATD_CreatedBy = sSession.UserID
                    objDataCap.sRATD_Status = "A"
                    objDataCap.iRATD_YearID = sSession.YearID
                    objDataCap.sRATD_Operation = "C"
                    objDataCap.sRATD_IPAddress = sSession.IPAddress
                    objDataCap.iRATD_CompID = sSession.AccessCodeID
                    objDataCap.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, 0, objDataCap)
                Next
            End If
            'Extra'
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExisting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExisting.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlExisting.SelectedIndex > 0 Then
                dt = objDataCap.GetDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCompany.SelectedValue, ddlTrType.SelectedValue, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
                If dt.Rows.Count > 0 Then
                    imgbtnUpdate.Visible = True : imgbtnSave.Visible = False
                    For i = 0 To dt.Rows.Count - 1

                        If IsDBNull(dt.Rows(i)("DC_TransactionNo")) = False Then
                            txtTransactionNo.Text = dt.Rows(i)("DC_TransactionNo")
                        Else
                            txtTransactionNo.Text = ""
                        End If
                        If IsDBNull(dt.Rows(i)("DC_TrDate")) = False Then
                            txtDate.Text = dt.Rows(i)("DC_TrDate")
                        Else
                            txtDate.Text = ""
                        End If
                        If IsDBNull(dt.Rows(i)("DC_Customer")) = False Then
                            ddlCompany.SelectedValue = dt.Rows(i)("DC_Customer")
                        Else
                            ddlCompany.SelectedIndex = 0
                        End If
                        If IsDBNull(dt.Rows(i)("DC_TrType")) = False Then
                            ddlTrType.SelectedValue = dt.Rows(i)("DC_TrType")
                        Else
                            ddlTrType.SelectedIndex = 0
                        End If
                        If IsDBNull(dt.Rows(i)("DC_BatchNo")) = False Then
                            ddlBatchNo.SelectedValue = dt.Rows(i)("DC_BatchNo")
                        Else
                            ddlBatchNo.SelectedIndex = 0
                        End If
                        If IsDBNull(dt.Rows(i)("DC_Customer")) = False Then
                            ddlParty.SelectedValue = dt.Rows(i)("DC_Customer")
                        Else
                            ddlParty.SelectedIndex = 0
                        End If
                        If IsDBNull(dt.Rows(i)("DC_VoucherNo")) = False Then
                            txtVoucherNo.Text = dt.Rows(i)("DC_VoucherNo")
                        Else
                            txtVoucherNo.Text = ""
                        End If
                        If IsDBNull(dt.Rows(i)("DC_PaymentType")) = False Then
                            ddlPaymentType.SelectedValue = dt.Rows(i)("DC_PaymentType")
                        Else
                            ddlPaymentType.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("DC_Zone")) = False Then
                            ddlAccZone.SelectedValue = dt.Rows(i)("DC_Zone")
                        End If
                        If IsDBNull(dt.Rows(i)("DC_Region")) = False Then
                            ddlAccRgn.SelectedValue = dt.Rows(i)("DC_Region")
                        End If
                        If IsDBNull(dt.Rows(i)("DC_Area")) = False Then
                            ddlAccArea.SelectedValue = dt.Rows(i)("DC_Area")
                        End If
                        If IsDBNull(dt.Rows(i)("DC_Branch")) = False Then
                            ddlAccBrnch.SelectedValue = dt.Rows(i)("DC_Branch")
                        End If
                        If IsDBNull(dt.Rows(i)("DC_PaymentType")) = False Then
                            ddlPaymentType.SelectedValue = dt.Rows(i)("DC_PaymentType")
                        Else
                            ddlPaymentType.SelectedIndex = 0
                        End If
                        If IsDBNull(dt.Rows(i)("DC_Narration")) = False Then
                            txtNarration.Text = dt.Rows(i)("DC_Narration")
                        Else
                            txtNarration.Text = ""
                        End If
                        If IsDBNull(dt.Rows(i)("DC_Delfalg")) = False Then

                        Else

                        End If
                        If IsDBNull(dt.Rows(i)("DC_Status")) = False Then

                        Else

                        End If

                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExisting_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlBatchNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBatchNo.SelectedIndexChanged
        Try
            If ddlBatchNo.SelectedIndex > 0 Then
                BindExistingNo(ddlCompany.SelectedValue, ddlTrType.SelectedValue, ddlBatchNo.SelectedValue)
            End If
            loadimage(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBatchNo_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindBatchNo(ByVal iSubCabinetID As Integer)
        Try
            ddlBatchNo.DataSource = objDataCap.BindBatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iSubCabinetID)
            ddlBatchNo.DataValueField = "FOL_FOLID"
            ddlBatchNo.DataTextField = "FOL_NAME"
            ddlBatchNo.DataBind()
            ddlBatchNo.Items.Insert(0, "Select BatchNo")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlTrType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTrType.SelectedIndexChanged
        Try
            Dim sFile As String = String.Format("~/Images/SearchImage/NoImage.jpg")
            documentViewer.Document = sFile
            If ddlTrType.SelectedIndex > 0 Then
                ClearDC() : ClearPurchaseAll() : SalesClear() : ClearReceipt() : ClearPayment() : CleaPettyCash() : ClearJE() : ClearGIN() : clearPurchaseReturn()

                ClearCDI() : ClearCash() : ClearDispatch() : ClearInvoice() : clearSalesReturn()

                BindBatchNo(ddlTrType.SelectedValue)
                LoadCustomerSupplier()

                divPurchase.Visible = False : divSales.Visible = False : divRPJ.Visible = False : divPayment.Visible = False
                divcollapseChequeDetails.Visible = False : lblJEType.Visible = False : ddlJEType.Visible = False
                ReceiptVisibleFalse() : dgPettyCashDetails.Visible = False : dgPaymentDetails.Visible = False

                LoadExistingTransactionRPJ(0)
                If ddlTrType.SelectedItem.Text = "Purchase" Then
                    lnkbtnPurchase_Click(sender, e)
                    divPurchase.Visible = True : divPurchaseGrid.Visible = True
                ElseIf ddlTrType.SelectedItem.Text = "Sales" Then
                    lnkbtnSales_Click(sender, e)
                    divSales.Visible = True
                ElseIf ddlTrType.SelectedItem.Text = "Payment" Then
                    lnkbtnPayment_Click(sender, e)
                    divPayment.Visible = True
                    dgPaymentDetails.Visible = True

                ElseIf ddlTrType.SelectedItem.Text = "Receipt" Then
                    lnkbtnRPJ_Click(sender, e)
                    divRPJ.Visible = True

                    lblBillNoRPJ.Visible = False
                    txtBillNo.Visible = False
                    lblBillDateRPJ.Visible = False
                    txtBillDate.Visible = False
                    lblBillAmountRPJ.Visible = False
                    txtBillAmount.Visible = False
                    imgbtnAddBillAmt.Visible = False
                    lblBillTypeRPJ.Visible = False
                    ddlBillType.Visible = False

                    ReceiptVisibleTrue()
                    txtTransactionNo.Text = objRecp.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
                    dgPaymentDetails.Visible = True

                ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then
                    lnkbtnRPJ_Click(sender, e)
                    divRPJ.Visible = True

                    divcollapseChequeDetails.Visible = False
                    txtTrNo.Text = objPC.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
                    dgPettyCashDetails.Visible = True

                ElseIf ddlTrType.SelectedItem.Text = "Journal Entry" Then
                    lnkbtnRPJ_Click(sender, e)
                    divRPJ.Visible = True

                    lblJEType.Visible = True : ddlJEType.Visible = True
                    txtTrNo.Text = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
                    dgPaymentDetails.Visible = True

                ElseIf ddlTrType.SelectedItem.Text = "GIN" Then
                    lnkbtnPurchase_Click(sender, e)
                    divPurchase.Visible = True : divPurchaseGrid.Visible = True
                ElseIf ddlTrType.SelectedItem.Text = "Purchase Return" Then
                    lnkbtnPurchase_Click(sender, e)
                    divPurchase.Visible = True : divPurchaseGrid.Visible = True
                ElseIf ddlTrType.SelectedItem.Text = "Cash Purchase" Then
                    lnkbtnPurchase_Click(sender, e)
                    divPurchase.Visible = True : divPurchaseGrid.Visible = True

                ElseIf ddlTrType.SelectedItem.Text = "Cash Sales" Then
                    lnkbtnSales_Click(sender, e)
                    divSales.Visible = True
                ElseIf ddlTrType.SelectedItem.Text = "Sales Dispatch" Then
                    lnkbtnSales_Click(sender, e)
                    divSales.Visible = True
                ElseIf ddlTrType.SelectedItem.Text = "Sales Invoice" Then
                    lnkbtnSales_Click(sender, e)
                    divSales.Visible = True
                ElseIf ddlTrType.SelectedItem.Text = "Sales Return" Then
                    lnkbtnSales_Click(sender, e)
                    divSales.Visible = True
                End If
            Else
                ClearDC() : ClearPurchaseAll() : SalesClear() : ClearReceipt() : ClearPayment() : CleaPettyCash() : ClearJE() : ClearGIN() : clearPurchaseReturn()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTrType_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindSalesReturn()
        Dim dt, dtCharge As New DataTable
        Try
            lblError.Text = ""
            dt = objSR.LoadSalesReturnDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0).Item("Sales_Return_ID").ToString()) = False Then
                    ddlExistSalesReturn.SelectedValue = dt.Rows(0).Item("Sales_Return_ID").ToString()
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_ReturnNo").ToString()) = False Then
                    txtReturnNoSR.Text = dt.Rows(0).Item("Sales_Return_ReturnNo").ToString()
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_RetrunDate").ToString()) = False Then
                    txtReturnDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0).Item("Sales_Return_RetrunDate").ToString(), "D")
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_Invoice").ToString()) = False Then
                    txtInvoiceNoSR.Text = dt.Rows(0).Item("Sales_Return_Invoice").ToString()
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_InvoiceDate").ToString()) = False Then
                    txtInvoiceDateSR.Text = objGen.FormatDtForRDBMS(dt.Rows(0).Item("Sales_Return_InvoiceDate").ToString(), "D")
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_Order").ToString()) = False Then
                    txtOrderNoSR.Text = dt.Rows(0).Item("Sales_Return_Order").ToString()
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_Dispatch").ToString()) = False Then
                    txtDispatchNoSR.Text = dt.Rows(0).Item("Sales_Return_Dispatch").ToString()
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_Customer").ToString()) = False Then
                    ddlCustomerSR.SelectedValue = dt.Rows(0).Item("Sales_Return_Customer").ToString()
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_ShipTo").ToString()) = False Then
                    txtShipTo.Text = dt.Rows(0).Item("Sales_Return_ShipTo").ToString()
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_DelFlag").ToString()) = False Then
                    If dt.Rows(0).Item("Sales_Return_DelFlag").ToString() = "W" Then
                        lblStatusSR.Text = "Waiting for Approval"
                    ElseIf dt.Rows(0).Item("Sales_Return_DelFlag").ToString() = "A" Then
                        lblStatusSR.Text = "Activated"
                    ElseIf dt.Rows(0).Item("Sales_Return_DelFlag").ToString() = "D" Then
                        lblStatusSR.Text = "De-Activated"
                    End If
                End If
                If IsDBNull(dt.Rows(0).Item("Sales_Return_GoodsReturnNo").ToString()) = False Then
                    txtGoodsReturnRefNo.Text = dt.Rows(0).Item("Sales_Return_GoodsReturnNo").ToString()
                End If
                dgSRItemDetails.DataSource = objSR.LoadSalesReturn(sSession.AccessCode, sSession.AccessCodeID, ddlExistSalesReturn.SelectedValue)
                dgSRItemDetails.DataBind()

            End If

            'dtCharge = objSR.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistSalesReturn.SelectedValue)
            'GvCharge.DataSource = dtCharge
            'GvCharge.DataBind()
            'Session("ChargesMaster") = dtCharge
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistSales_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindExistingNo(ByVal iCabinetID As Integer, ByVal iSubCabinetID As Integer, ByVal iFolderID As Integer)
        Try
            ddlExisting.DataSource = objDataCap.BindExistingNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
            ddlExisting.DataValueField = "DC_ID"
            ddlExisting.DataTextField = "DC_TransactionNo"
            ddlExisting.DataBind()
            ddlExisting.Items.Insert(0, "Select Existing No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect("~/RemoteData/DataCaptureMaster.aspx?")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Try
            If ddlExisting.SelectedIndex > 0 Then
                If lblStatus.Text = "Activated" Then
                    lblError.Text = "This Transaction has been approved,it can not be approve again."
                    Exit Sub
                End If
                objDataCap.ApproveDC(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCompany.SelectedValue, ddlTrType.SelectedValue, ddlBatchNo.SelectedValue, ddlExisting.SelectedValue)
                lblError.Text = "Successfully Approved"
            Else
                lblError.Text = "Select the Existing Transaction No to Approve."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            If lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved,it can not be Updated."
                Exit Sub
            End If
            SaveUpdate()
            lblError.Text = "Successfully Updated"
            imgbtnUpdate.Visible = False : imgbtnSave.Visible = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Private Sub dgPaymentDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPaymentDetails.ItemCommand
        Dim dt As New DataTable
        Dim lblId As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Delete" Then

                If lblStatus.Text = "Activated" Then
                    lblError.Text = "This Payment has been Approved, you can not delete transactions."
                    Exit Sub
                End If

                dt = Session("DataCapture")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                If dt.Rows.Count > 0 Then
                    Session("DataCapture") = dt
                Else
                    Session("DataCapture") = Nothing
                End If
            End If

            dgPaymentDetails.DataSource = dt
            dgPaymentDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPaymentDetails_ItemCommand")
        End Try
    End Sub
    Public Sub SaveIncompleteTransactions()
        'Dim objDC As ClsDataCapture.Incomplete
        'Dim Arr() As String
        'Dim dDebit As Double = 0, dCredit As Double = 0, dSum As Double = 0, dSDebit As Double = 0
        'Dim iMasterID As Integer
        'Try
        '    objDC.IT_ID = 0
        '    objDC.IT_TransactionNo = txtTransactionNo.Text
        '    objDC.IT_TrDate = txtDate.Text
        '    objDC.IT_Company = ddlCompany.SelectedValue
        '    objDC.IT_Customer = ddlParty.SelectedValue
        '    objDC.IT_TrType = ddlTrType.SelectedValue
        '    objDC.IT_BatchNo = ddlBatchNo.SelectedValue
        '    objDC.IT_VoucherNo = txtVoucherNo.Text
        '    objDC.IT_BASENAME = lstFiles.SelectedItem.Text  'BaseName ID
        '    objDC.IT_Zone = ddlAccZone.SelectedValue
        '    objDC.IT_Region = ddlAccRgn.SelectedValue
        '    objDC.IT_Area = ddlAccArea.SelectedValue
        '    objDC.IT_Branch = ddlAccBrnch.SelectedValue
        '    If ddlPaymentType.SelectedIndex > 0 Then
        '        objDC.IT_PaymentType = ddlPaymentType.SelectedValue
        '    Else
        '        objDC.IT_PaymentType = 0
        '    End If
        '    objDC.IT_Narration = txtNarration.Text
        '    objDC.IT_Delfalg = "W"
        '    objDC.IT_Status = "W"
        '    objDC.IT_CompID = sSession.AccessCodeID
        '    objDC.IT_YearID = sSession.YearID
        '    objDC.IT_CrBy = sSession.UserID
        '    objDC.IT_CrOn = Date.Today
        '    objDC.IT_UpdatedBy = sSession.UserID
        '    objDC.IT_UpdatedOn = Date.Today
        '    objDC.IT_Operation = "C"
        '    objDC.IT_IPAddress = sSession.IPAddress

        '    Arr = objDataCap.SaveIncompleteDataCapture(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDC)
        '    iMasterID = Arr(1)

        'Catch ex As Exception
        '    Throw
        'End Try
    End Sub
    Private Sub lnkbtnPurchase_Click(sender As Object, e As EventArgs) Handles lnkbtnPurchase.Click
        Try
            lblTab.Text = 1
            liRPJ.Attributes.Remove("class")
            liSales.Attributes.Remove("class")
            liPurchase.Attributes.Add("class", "active")
            liPayment.Attributes.Remove("class")

            divRPJ.Attributes.Add("class", "tab-pane")
            divSales.Attributes.Add("class", "tab-pane")
            divPurchase.Attributes.Add("class", "tab-pane active")
            divPayment.Attributes.Add("class", "tab-pane")
            If ddlTrType.SelectedItem.Text = "Purchase" Or ddlTrType.SelectedItem.Text = "Cash Purchase" Then
                divPO.Visible = True : divGIN.Visible = False : divPR.Visible = False
            ElseIf ddlTrType.SelectedItem.Text = "GIN" Then
                divPO.Visible = False : divGIN.Visible = True : divPR.Visible = False
            ElseIf ddlTrType.SelectedItem.Text = "Purchase Return" Then
                divPO.Visible = False : divGIN.Visible = False : divPR.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPurchase_Click")
        End Try
    End Sub
    Private Sub lnkbtnSales_Click(sender As Object, e As EventArgs) Handles lnkbtnSales.Click
        Try
            lblTab.Text = 2
            liPurchase.Attributes.Remove("class")
            liRPJ.Attributes.Remove("class")
            liSales.Attributes.Add("class", "active")
            liPayment.Attributes.Remove("class")

            divPurchase.Attributes.Add("class", "tab-pane")
            divRPJ.Attributes.Add("class", "tab-pane")
            divSales.Attributes.Add("class", "tab-pane active")
            divPayment.Attributes.Add("class", "tab-pane")

            divSO.Visible = False : divCS.Visible = False : divSR.Visible = False
            divCash.Visible = False : divDispatch.Visible = False : divInvoice.Visible = False

            If ddlTrType.SelectedItem.Text = "Sales" Then
                divSO.Visible = True

            ElseIf ddlTrType.SelectedItem.Text = "Cash Sales" Then
                divCS.Visible = True : divCash.Visible = True

            ElseIf ddlTrType.SelectedItem.Text = "Sales Dispatch" Then
                divCS.Visible = True : divDispatch.Visible = True

            ElseIf ddlTrType.SelectedItem.Text = "Sales Invoice" Then
                divCS.Visible = True : divInvoice.Visible = True

            ElseIf ddlTrType.SelectedItem.Text = "Sales Return" Then
                divSR.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSales_Click")
        End Try
    End Sub
    Private Sub lnkbtnRPJ_Click(sender As Object, e As EventArgs) Handles lnkbtnRPJ.Click
        Try
            lblTab.Text = 3
            liPurchase.Attributes.Remove("class")
            liSales.Attributes.Remove("class")
            liRPJ.Attributes.Add("class", "active")
            liPayment.Attributes.Remove("class")

            divPurchase.Attributes.Add("class", "tab-pane")
            divSales.Attributes.Add("class", "tab-pane")
            divRPJ.Attributes.Add("class", "tab-pane active")
            divPayment.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRPJ_Click")
        End Try
    End Sub
    Private Sub lnkbtnPayment_Click(sender As Object, e As EventArgs) Handles lnkbtnPayment.Click
        Try
            lblTab.Text = 4
            liPurchase.Attributes.Remove("class")
            liSales.Attributes.Remove("class")
            liPayment.Attributes.Remove("class")
            liPayment.Attributes.Add("class", "active")

            divPurchase.Attributes.Add("class", "tab-pane")
            divSales.Attributes.Add("class", "tab-pane")
            divRPJ.Attributes.Add("class", "tab-pane")
            divPayment.Attributes.Add("class", "tab-pane active")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPayment_Click")
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
    Public Sub LoadUnit()
        Try
            ddlUnit.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
            ddlUnit.DataTextField = "Mas_Desc"
            ddlUnit.DataValueField = "Mas_ID"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, "Unit of Measurement")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadUnitGIN()
        Try
            ddlUnitGIN.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
            ddlUnitGIN.DataTextField = "Mas_Desc"
            ddlUnitGIN.DataValueField = "Mas_ID"
            ddlUnitGIN.DataBind()
            ddlUnitGIN.Items.Insert(0, "Unit of Measurement")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadUnitPR()
        Try
            ddlUnitPR.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
            ddlUnitPR.DataTextField = "Mas_Desc"
            ddlUnitPR.DataValueField = "Mas_ID"
            ddlUnitPR.DataBind()
            ddlUnitPR.Items.Insert(0, "Unit of Measurement")
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
    Private Sub LoadChargeTypeSR()
        Try
            ddlChargeTypeSR.DataSource = objSR.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeTypeSR.DataTextField = "Mas_desc"
            ddlChargeTypeSR.DataValueField = "Mas_id"
            ddlChargeTypeSR.DataBind()
            ddlChargeTypeSR.Items.Insert(0, "Select Charge Type")
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
                    txtGSTRate.Text = objPO.LoadExciseUsingDate(sSession.AccessCode, sSession.AccessCodeID, oDate, txtHistoryID.Text)
                    'txtGSTRate.Text = objInvD.LoadExciseUsingDate(sSession.AccessCode, sSession.AccessCodeID, txtOrderDate.Text, txtHistoryID.Text)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExciseUsingDate")
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
            txtOrderCodeS.Text = objSO.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            txtOrderDateS.Text = objGen.FormatDtForRDBMS(System.DateTime.Now, "D")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCodeAnddate")
        End Try
    End Sub
    Public Sub GenerateGINNo()
        Try
            txtOrderCodeGIN.Text = objGIN.GenerateInwardCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateGINNo")
        End Try
    End Sub
    Public Sub GeneratePRNo()
        Try
            txtOrderCodePR.Text = objGreturn.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GeneratePRNo")
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
    Private Sub LoadSuppliersGIN()
        Try
            ddlSupplierGIN.DataSource = objPO.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplierGIN.DataTextField = "CSM_Name"
            ddlSupplierGIN.DataValueField = "CSM_ID"
            ddlSupplierGIN.DataBind()
            ddlSupplierGIN.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSuppliersPR()
        Try
            ddlSupplierPR.DataSource = objPO.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplierPR.DataTextField = "CSM_Name"
            ddlSupplierPR.DataValueField = "CSM_ID"
            ddlSupplierPR.DataBind()
            ddlSupplierPR.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingPurchaseOrder()
        Try
            ddlExistingOrder.DataSource = objPO.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingOrder.DataTextField = "POM_OrderNo"
            ddlExistingOrder.DataValueField = "POM_ID"
            ddlExistingOrder.DataBind()
            ddlExistingOrder.Items.Insert(0, "Existing Purchase Order")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommoditySales()
        Try
            ddlCommodityS.DataSource = objSO.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodityS.DataTextField = "Inv_Description"
            ddlCommodityS.DataValueField = "Inv_ID"
            ddlCommodityS.DataBind()
            ddlCommodityS.Items.Insert(0, "Select Brand")
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
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommodityGIN()
        Try
            ddlCommodityGIN.DataSource = objPO.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodityGIN.DataTextField = "Inv_Description"
            ddlCommodityGIN.DataValueField = "Inv_ID"
            ddlCommodityGIN.DataBind()
            ddlCommodityGIN.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommodityPR()
        Try
            ddlCommodityPR.DataSource = objPO.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodityPR.DataTextField = "Inv_Description"
            ddlCommodityPR.DataValueField = "Inv_ID"
            ddlCommodityPR.DataBind()
            ddlCommodityPR.Items.Insert(0, "Select Commodity")
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
    Private Sub loadDescitionStartSR()
        Try
            lstBoxDescriptionSR.DataSource = objPO.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            lstBoxDescriptionSR.DataTextField = "Inv_Code"
            lstBoxDescriptionSR.DataValueField = "Inv_ID"
            lstBoxDescriptionSR.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadDescitionStartGIN()
        Try
            chkCategoryGIN.DataSource = objPO.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            chkCategoryGIN.DataTextField = "Inv_Code"
            chkCategoryGIN.DataValueField = "Inv_ID"
            chkCategoryGIN.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadDescitionStartPR()
        Try
            chkCategoryPR.DataSource = objPO.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            chkCategoryPR.DataTextField = "Inv_Code"
            chkCategoryPR.DataValueField = "Inv_ID"
            chkCategoryPR.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
        Dim altPices As Integer
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            lblError.Text = ""
            If (chkCategory.SelectedValue > 0) Then

                If txtOrderDate.Text <> "" Then
                    'Cheque Date Comparision'
                    dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m < 0 Then
                        lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                        lblValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                        txtOrderDate.Focus()
                        Exit Sub
                    End If

                    dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m > 0 Then
                        lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                        lblValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
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
            'LoadDesciptionDetails()
            LoadUnit()
            'altPices = objPO.GetAlterNatePiceValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
            'txtPices.Text = altPices
            'ddlUnit.SelectedValue = objPO.GetUnitsValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
            'hfTotalPieces.Value = txtPices.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub LoadDesciptionDetails()
    '    Dim dt As New DataTable
    '    Dim sArray As Array
    '    Dim dOrderDate As Date
    '    Dim iGSTRate As Integer
    '    Try
    '        'ddlRate.DataSource = dt
    '        'ddlRate.DataBind()
    '        txtRate.Text = "" : txtRateAmount.Text = ""
    '        txtQuantity.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
    '        txtGSTRate.Text = "" : txtGSTAmount.Text = ""
    '        txtTotalAmount.Text = ""

    '        If lblDescID.Text <> "0" Then
    '            iGSTRate = objPO.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
    '            If iGSTRate > 0 Then
    '            Else
    '                lblError.Text = "Enter the HSN Details in Inventory Master."
    '                lblValidationMsg.Text = lblError.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If txtOrderDate.Text <> "" Then
    '                dOrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '                dt = objPO.CheckDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text, dOrderDate)
    '                If dt.Rows.Count = 0 Then
    '                    lblError.Text = "Enter Details in Inventory Master Details"
    '                    lblValidationMsg.Text = lblError.Text
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
    '                    Exit Sub
    '                End If
    '                'If dt.Rows.Count > 1 Then
    '                '    ddlRate.DataSource = dt
    '                '    ddlRate.DataTextField = "INVH_PreDeterminedPrice"
    '                '    ddlRate.DataValueField = "InvH_ID"
    '                '    ddlRate.DataBind()
    '                '    ddlRate.Enabled = True
    '                '    'txtRate.Enabled = False
    '                '    txtHistoryID.Text = ddlRate.SelectedValue
    '                '    sArray = ddlRate.SelectedItem.Text.Split("-")
    '                '    txtRate.Text = sArray(0)
    '                '    'LoadExciseUsingDate()

    '                '    GetOtherDetails(txtHistoryID.Text)
    '                'Else
    '                '    sArray = dt.Rows(0)(1).ToString().Split("-")
    '                '    txtRate.Text = sArray(0)
    '                '    txtHistoryID.Text = dt.Rows(0)(0).ToString()

    '                '    'LoadExciseUsingDate()
    '                '    'ddlRate.Enabled = False
    '                '    'txtRate.Enabled = True
    '                '    If txtHistoryID.Text <> "" Then
    '                '        ' GetPurchaseDetails(txtHistoryID.Text)
    '                '        GetOtherDetails(txtHistoryID.Text)
    '                '    End If
    '                'End If
    '                ddlUnit.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
    '                ddlUnit.DataTextField = "Mas_Desc"
    '                ddlUnit.DataValueField = "Mas_ID"
    '                ddlUnit.DataBind()
    '                ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")

    '                txtGSTID.Text = 0
    '                txtGSTRate.Text = 0

    '                Dim sGSTRate As String = ""
    '                sGSTRate = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
    '                If sGSTRate <> "HSN" Then
    '                    txtGSTID.Text = 0
    '                    'txtGSTRate.Text = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
    '                    txtGSTRate.Text = 0
    '                Else
    '                    txtGSTID.Text = objPO.GetGSTID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
    '                    txtGSTRate.Text = objPO.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
    '                End If
    '            End If

    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDesciptionDetails")
    '    End Try
    'End Sub
    Private Sub GetOtherDetails(ByVal iHistoryId As Integer)
        'Dim dt As New DataTable
        Try
            'dt = objPO.GetOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            'If dt.Rows.Count > 0 Then
            '    If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
            '        txtGSTRate.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
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
            '    txtExcise.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
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
                    txtGSTRate.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                Else
                    txtGSTRate.Text = "0"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetPurchaseDetails")
        End Try
    End Function
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
            txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtGSTRate.Text = "" : txtGSTAmount.Text = ""
            txtTotalAmount.Text = ""
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
        Try
            lblError.Text = ""
            lblStatus.Text = ""
            If ddlExistingOrder.SelectedIndex > 0 Then
                ddlCommodity.SelectedIndex = 0
                chkCategory.Items.Clear()
                ClearAll()
                dgPurchase.DataSource = objPO.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, 0, 0)
                dgPurchase.DataBind()
                dt = objPO.LoadPurchaseOderMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, 0, 0)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("POM_OrderNo").ToString()) = False Then
                        txtOrderCode.Text = dt.Rows(0)("POM_OrderNo").ToString()
                    Else
                        txtOrderCode.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("POM_OrderDate").ToString()) = False Then
                        txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("POM_OrderDate"), "D")
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
                    dtCharge = objPO.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, 0, 0, 0, 0)
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

                    GetAttachFilePO(ddlBatchNo.SelectedValue, lstFiles.SelectedValue)

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
            'ddlRate.Enabled = False
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
                    txtBillingAddress.Text = dtCustomer.Rows(0)("CSM_Address")
                    'txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("CSM_GSTNRegNo")
                    'ddlCompanyType.SelectedValue = dtCustomer.Rows(0)("CSM_CompanyType")
                    If IsDBNull(dtCustomer.Rows(0)("CSM_GSTNRegNo")) = False Then
                        txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("CSM_GSTNRegNo")
                    Else
                        txtBillingGSTNRegNo.Text = ""
                    End If
                    If IsDBNull(dtCustomer.Rows(0)("CSM_CompanyType")) = False Then
                        ddlCompanyType.SelectedValue = dtCustomer.Rows(0)("CSM_CompanyType")
                    Else
                        ddlCompanyType.SelectedIndex = 0
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
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lnkDescription = DirectCast(clickedRow.FindControl("lnkGoods"), Label)
            lblcomodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
            lblDescriptionId = DirectCast(clickedRow.FindControl("lblDescriptionID"), Label)
            lblHistoryID = DirectCast(clickedRow.FindControl("lblHistoryID"), Label)
            If e.CommandName = "Delete1" Then
                objPO.DeleteOrderValues(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text, lblDescriptionId.Text, lblHistoryID.Text)
                lblStatus.Text = "Sucessfully Deleted"
                dgPurchase.DataSource = objPO.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtMasterID.Text, 0, 0)
                dgPurchase.DataBind()
                If (dgPurchase.Rows.Count = 0) Then
                    objPO.DeleteOrderValuesFromMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text)
                End If
                LoadExistingPurchaseOrder()
            End If
            If e.CommandName = "Edit1" Then
                If (objPO.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtMasterID.Text) = "W") Then
                    txtHistoryID.Text = lblHistoryID.Text

                    dt = objPO.LoadPurchaseOderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text, lblcomodityID.Text, lblDescriptionId.Text, lblHistoryID.Text)
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
    Private Sub imgbtnPrintPO_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPrintPo.Click
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPrintPO_Click")
        End Try
    End Sub
    Private Sub imgbtnRefreshPO_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefreshPO.Click
        Try
            ClearPurchaseAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefreshPO_Click")
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
    Private Sub imgbtnAddChargePO_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddChargePo.Click
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

                Else
                    lblError.Text = "Enter Amount charged."
                End If
            Else
                lblError.Text = "Select Charge Type."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddChargePO_Click")
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
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtOrderDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtOrderDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtOrderDate_TextChanged")
        End Try
    End Sub
    'Public Sub LoadRefreshImage()
    '    Dim BaseID As Integer, i As Integer = 0
    '    Dim FileSelectedID As String = ""
    '    Dim sRFID As String = ""
    '    Dim sFile As String
    '    Dim sExt As String
    '    Dim dt As New DataTable, dtIndexType As New DataTable
    '    Dim sFileName As String = ""
    '    Try
    '        If ddlCompany.SelectedIndex > 0 Then
    '            If ddlTrType.SelectedIndex > 0 Then
    '                If ddlBatchNo.SelectedIndex > 0 Then
    '                    'ddlBatchNo_SelectedIndexChanged(sender, e)

    '                    dt = objclsview.LoadBaseIdFromFolder(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue, ddlFolder.SelectedValue, sRFID)
    '                    If (dt.Rows.Count > 0) Then
    '                        BaseID = dt.Rows(0).Item("PGE_BASENAME")
    '                        FileSelectedID = dt.Rows(0).Item("PGE_BASENAME")
    '                        iSelectedImageID = FileSelectedID

    '                        sSelectedDocTypeID = dt.Rows(0).Item("PGE_DOCUMENT_TYPE")
    '                        For i = 0 To dt.Rows.Count - 1
    '                            sDetailsId = sDetailsId & "," & dt.Rows(i).Item("PGE_BASENAME")
    '                            If (sDetailsId.Length > 0) Then
    '                                If (sDetailsId.Chars(0).ToString = ",") Then
    '                                    sDetailsId = sDetailsId.Remove(0, 1)
    '                                End If
    '                            End If
    '                        Next
    '                    End If

    '                    Dim iDocSelectedID As Integer = 0, iFileSelectedID As Integer = 0

    '                    If lstFiles.Items.Count <> 0 Then
    '                        If FileSelectedID IsNot Nothing Then
    '                            Try
    '                                iFileSelectedID = 0
    '                                txtNav.Text = iFileSelectedID + 1
    '                                iPageNext = iFileSelectedID
    '                            Catch ex As Exception
    '                            End Try
    '                        End If
    '                        lstFiles.SelectedIndex = iFileSelectedID

    '                        lblDocID.Text = lstFiles.SelectedItem.Text
    '                        iSelectedImageID = lstFiles.SelectedItem.Text

    '                        sFile = objSearch.GetPageFromEdict(sSession.AccessCode, lstFiles.SelectedItem.Text)
    '                        sImgFilePath = sFile
    '                        If Trim(sFile.Length) = 0 Then Exit Sub
    '                        sExt = Path.GetExtension(sFile)
    '                        sExt = sExt.Remove(0, 1)
    '                        sSelectedImageExt = sExt

    '                        Select Case UCase(sExt)
    '                            Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
    '                                Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
    '                                Dim imageBase64Data As String = Convert.ToBase64String(bytes)
    '                                Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
    '                                'RetrieveImage.ImageUrl = imageDataURL
    '                                documentViewer.Document = sFile
    '                                Dim fi As New IO.FileInfo(sFile)
    '                                Dim iDocType As Integer = objSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
    '                            Case "PDF"
    '                                Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
    '                                'RetrieveImage.ImageUrl = imageDataURL
    '                                documentViewer.Document = sFile
    '                                Dim fi As New IO.FileInfo(sFile)
    '                            Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PPS", "XLR", "XML", "TIF", "TIFF"

    '                        End Select

    '                    End If

    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
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
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                ddlBranch.SelectedValue = ddlAccBrnch.SelectedValue
                ddlBranch_SelectedIndexChanged(sender, e)

                If ddlAccBrnch.SelectedIndex > 0 Then
                    iParent = objDataCap.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                    ddlAccArea.SelectedValue = iParent
                End If
                If ddlAccArea.SelectedIndex > 0 Then
                    iParent = objDataCap.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                    ddlAccRgn.SelectedValue = iParent
                End If
                If ddlAccRgn.SelectedIndex > 0 Then
                    iParent = objDataCap.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnIndex_Click")
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
                        lblValidationMsg.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)

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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAttch_Click")
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
    Public Sub GetAttachFilePO(ByVal iBatchNo As Integer, ByVal iBaseName As Integer)
        Dim dRow As DataRow
        Dim dt, dt1 As New DataTable
        Try
            dt.Columns.Add("FilePath")
            dt.Columns.Add("FileName")
            dt.Columns.Add("Extension")
            dt.Columns.Add("CreatedOn")

            dt1 = objPO.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, iBatchNo, iBaseName)
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
    Public Sub GetAttachFileSO(ByVal iBatchNo As Integer, ByVal iBaseName As Integer)
        Dim dRow As DataRow
        Dim dt, dt1 As New DataTable
        Try
            dt.Columns.Add("FilePath")
            dt.Columns.Add("FileName")
            dt.Columns.Add("Extension")
            dt.Columns.Add("CreatedOn")

            dt1 = objSO.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, iBatchNo, iBaseName)
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
    Public Sub BindBranch()
        Try
            ddlBranch.DataSource = objPO.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranch.DataTextField = "Org_Name"
            ddlBranch.DataValueField = "Org_Node"
            ddlBranch.DataBind()
            ddlBranch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnSavePO_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSavePO.Click
        Dim iMasterID As Integer = 0
        Dim dOrderDate As Date
        Dim dRequiredDate As Date
        Dim dDate, dSDate As Date : Dim m As Integer

        Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Dim Arr() As String : Dim bCheck As Boolean
        Try
            If (ddlAccBrnch.SelectedIndex = 0) Then
                lblError.Text = "Select Branch."
                Exit Sub
            End If

            If txtVoucherPO.Text <> "" Then
                bCheck = objPO.CheckVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtVoucherPO.Text)
                If bCheck = True Then
                    lblPO.Text = "This Voucher No already saved."
                    lblValidationMsg.Text = lblPO.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtOrderCode.Text = objPO.GeneratePurchaseOrderCode(sSession.AccessCode, sSession.AccessCodeID)
                    Exit Sub
                End If
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

            ''Extra
            'If txtGSTRate.Text = "" Or ddlUnit.Items.Count = 0 Then
            '    lblError.Text = "For this item add the Inventory Details in Inventory Details form."
            '    lblValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            '    Exit Sub
            'End If
            ''Extra

            If txtOrderDate.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtOrderDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtOrderDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If (ddlExistingOrder.SelectedIndex > 0) Then
                If (objPO.CheckApprovedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue) = False) Then
                    lblError.Text = "Already Approved "
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
            If hfTotalAmount.Value <> "" Then
                objPO.sPOD_TotalAmount = Request.Form(hfTotalAmount.UniqueID)
            Else
                objPO.sPOD_TotalAmount = txtTotalAmount.Text
            End If
            If ddlTrType.SelectedItem.Text = "Purchase" Then
                objPO.sOralOrPO = "P"
            ElseIf ddlTrType.SelectedItem.Text = "Cash Purchase" Then
                objPO.sOralOrPO = "O"
            End If
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

            objPO.POM_CompanyType = ddlCompanyType.SelectedValue
            objPO.POM_GSTNCategory = ddlGSTCategory.SelectedValue

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

            If ddlBatchNo.SelectedIndex > 0 Then
                objPO.POM_BatchNo = ddlBatchNo.SelectedValue
            Else
                objPO.POM_BatchNo = 0
            End If

            objPO.POM_BaseName = lstFiles.SelectedItem.Text  'BaseName ID
            objPO.POM_BuyerRefNo = txtVoucherPO.Text

            iMasterID = objPO.SavePurchaseOrder(sSession.AccessCode, sSession.AccessCodeID, dOrderDate, objPO)
            txtMasterID.Text = iMasterID

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
                objPO.sPOD_RateAmount = txtRateAmount.Text
            Else
                objPO.sPOD_RateAmount = 0
            End If

            If txtQuantity.Text = "" Then
                objPO.sPOD_Quantity = "0"
            Else
                objPO.sPOD_Quantity = txtQuantity.Text
            End If
            If txtDiscount.Text = "" Then
                objPO.sPOD_Discount = "0"
            Else
                objPO.sPOD_Discount = txtDiscount.Text
            End If
            If txtDiscountAmount.Text <> "" Then
                objPO.sPOD_DiscountAmount = txtDiscountAmount.Text
            Else
                objPO.sPOD_DiscountAmount = 0
            End If

            objPO.sPOD_Excise = "0"
            objPO.sPOD_ExciseAmount = 0

            objPO.sPOD_Frieght = "0"
            objPO.sPOD_FrieghtAmount = "0"

            objPO.sPOD_VAT = "0"
            objPO.sPOD_VATAmount = 0
            objPO.sPOD_CST = "0"
            objPO.sPOD_CSTAmount = 0
            objPO.dPOD_RequiredDate = "01/01/1900"

            If txtGSTID.Text <> "" Then
                objPO.POD_GST_ID = txtGSTID.Text
            Else
                txtGSTID.Text = 0
            End If

            objPO.POD_GSTRate = txtGSTRate.Text
            objPO.POD_GSTAmount = txtGSTAmount.Text

            If txtTotalAmount.Text <> "" Then
                objPO.sPOD_TotalAmount = txtTotalAmount.Text
            Else
                objPO.sPOD_TotalAmount = 0
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

            Dim iID As Integer
            iID = objPO.SavePurchaseOrderDetails(sSession.AccessCode, sSession.AccessCodeID, dRequiredDate, objPO)

            If iID > 0 Then
                lblError.Text = "Successfully Updated"
                imgbtnSavePO.ImageUrl = "~/Images/Add24.png"

                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            ElseIf iID = 0 Then
                lblError.Text = "Successfully Saved"
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            End If

            SaveCharges(iMasterID)

            objPO.POD_ReceivedQty = 0
            objPO.POD_Rejected = 0
            objPO.POD_Accepted = 0

            LoadExistingPurchaseOrder()
            ddlExistingOrder.SelectedValue = iMasterID
            dgPurchase.DataSource = objPO.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID, 0, 0)
            dgPurchase.DataBind()
            ClearAll()

            lblPO.Text = "Waiting for aprroval."
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSavePO_Click")
        End Try
    End Sub
    Private Sub ddlPatryS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPatryS.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Try

            lblError.Text = ""
            ClearonPartySelection()

            BindDescription(0)
            If ddlPatryS.SelectedIndex > 0 Then
                If txtOrderDateS.Text <> "" Then
                    lstBoxDescription.Enabled = True
                    dt = objSO.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlPatryS.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            txtPartyNo.Text = dt.Rows(i)("BM_Code")
                            txtAddress.Text = dt.Rows(i)("BM_Address")
                            txtContactNo.Text = dt.Rows(i)("BM_MobileNo")

                            If IsDBNull(dt.Rows(i)("BM_GenCategory")) = False Then
                                If dt.Rows(i)("BM_GenCategory") > 0 Then
                                    ddlCategory.SelectedValue = dt.Rows(i)("BM_GenCategory")
                                Else
                                    ddlCategory.SelectedIndex = 0
                                End If
                            Else
                                ddlCategory.SelectedIndex = 0
                            End If
                        Next
                    End If

                End If
            Else
                txtPartyNo.Text = "" : txtAddress.Text = "" : txtContactNo.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPartyS_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub ClearonPartySelection()
        Try
            ddlModeOfShipping.SelectedIndex = 0 : txtShippingDate.Text = ""
            ddlPaymentType.SelectedIndex = 0 : ddlModeOfCommunication.SelectedIndex = 0 : ddlCommodity.SelectedIndex = 0
            txtInputBy.Text = "" : ddlShippingCharges.SelectedIndex = 0
            txtOrderID.Text = "" : txtMRPFromTable.Text = ""

            lstBoxDescription.Items.Clear()
            ddlUnitOfMeassurement.Items.Clear() : txtMRP.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            dgExistingProFormaSalesOrder.DataSource = Nothing
            dgExistingProFormaSalesOrder.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearonPartySelection")
        End Try
    End Sub

    Private Sub ddlCommodityS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodityS.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCommodityS.SelectedIndex > 0 Then
                BindDescription(ddlCommodityS.SelectedValue)
            Else
                BindDescription(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodityS_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnRefreshSale_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefreshSale.Click
        Try
            SalesClear()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefreshSale_Click")
        End Try
    End Sub
    Public Sub SalesClear()
        Try
            lblError.Text = "" : txtsearch.Text = "" : lblStatusSO.Text = ""
            imgbtnAddSale.ImageUrl = "~/Images/Add24.png"

            txtOrderCodeS.Text = "" : txtOrderDateS.Text = "" : ddlShipping.SelectedIndex = 0 : txtShippingDate.Text = ""
            ddlPaymentTypeS.SelectedIndex = 0 : ddlModeOfCommunication.SelectedIndex = 0 : ddlCommodityS.SelectedIndex = 0
            ddlPatryS.SelectedIndex = 0 : txtPartyNo.Text = "" : txtContactNo.Text = "" : txtAddress.Text = ""
            txtInputBy.Text = "" : ddlShippingCharges.SelectedIndex = 0
            txtOrderID.Text = "" : txtMRPFromTable.Text = ""

            lstBoxDescription.Items.Clear()
            ddlUnitOfMeassurement.Items.Clear() : txtMRP.Text = ""
            txtQuantitySales.Text = "" : txtAmount.Text = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            dgExistingProFormaSalesOrder.DataSource = Nothing
            dgExistingProFormaSalesOrder.DataBind()

            txtBuyerPurOrderNo.Text = "" : txtBuyerOrderDate.Text = "" : txtRemarks.Text = "" : ddlPaymentTypeS.SelectedIndex = 0
            GenerateOrderCodeAnddate()
            BindDescription(0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SalesClear()")
        End Try
    End Sub
    Public Sub SalesClearDetails()
        Try
            For i = 0 To lstBoxDescription.Items.Count - 1
                lstBoxDescription.Items(i).Selected = False
            Next
            ddlUnitOfMeassurement.Items.Clear() : txtMRP.Text = ""
            txtQuantitySales.Text = "" : txtAmount.Text = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SalesClearDetails()")
        End Try
    End Sub
    Private Sub imgbtnAddSale_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddSale.Click
        Dim Arr() As String
        Dim dt As New DataTable
        Dim iMasterID As Integer
        Dim dOrderDate As Date
        Dim sCode As String = "" : Dim sStr As String = ""
        Dim iOrderID As Integer
        Dim bCheck As String = "" : Dim sStatus As String = ""
        Dim dDate, dSDate As Date : Dim m As Integer

        Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Try
            lblError.Text = ""

            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If

            'Check Application Settings'
            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer")
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

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST")
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

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales")
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
            dSDate = Date.ParseExact(txtOrderDateS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Order Date (" & txtOrderDateS.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtOrderDateS.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtOrderDateS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Order Date (" & txtOrderDateS.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtOrderDateS.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            If txtOrderID.Text <> "" Then
                iOrderID = txtOrderID.Text
            Else
                iOrderID = 0
            End If

            'Save Master'
            objSO.SPO_OrderCode = objGen.SafeSQL(Trim(txtOrderCodeS.Text))
            If txtOrderDateS.Text = "" Then
                lblError.Text = "Enter Order Date"
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtOrderDateS.Focus()
                Exit Sub
            End If
            objSO.SPO_OrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDateS.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            objSO.SPO_PartyCode = objGen.SafeSQL(Trim(txtPartyNo.Text))
            objSO.SPO_PartyName = objGen.SafeSQL(Trim(ddlPatryS.SelectedValue))
            objSO.SPO_Address = objGen.SafeSQL(Trim(txtAddress.Text))
            objSO.SPO_ContantNo = objGen.SafeSQL(Trim(txtContactNo.Text))

            If ddlModeOfShipping.SelectedIndex > 0 Then
                objSO.SPO_ModeOfDispatch = objGen.SafeSQL(Trim(ddlModeOfShipping.SelectedValue))
            Else
                objSO.SPO_ModeOfDispatch = 0
            End If
            If txtShippingDate.Text <> "" Then
                objSO.SPO_ShippingDate = Date.ParseExact(objGen.SafeSQL(Trim(txtShippingDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtShippingDate.Text <> "" Then
                'Dim dDate, dSDate As Date
                'Cheque Date Comparision'
                dDate = Date.ParseExact(txtOrderDateS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtShippingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'Dim m As Integer
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Shipping Date (" & txtShippingDate.Text & ") should be Greater than or equal to Order Date(" & txtOrderDateS.Text & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtShippingDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            objSO.SPO_PaymentType = objGen.SafeSQL(Trim(ddlPaymentTypeS.SelectedValue))

            If ddlModeOfCommunication.SelectedIndex > 0 Then
                objSO.SPO_ModeOfCommunication = objGen.SafeSQL(Trim(ddlModeOfCommunication.SelectedValue))
            Else
                objSO.SPO_ModeOfCommunication = 0
            End If
            If txtInputBy.Text <> "" Then
                objSO.SPO_InputBy = objGen.SafeSQL(Trim(txtInputBy.Text))
            Else
                objSO.SPO_InputBy = ""
            End If

            If ddlShippingCharges.SelectedIndex > 0 Then
                objSO.SPO_ShippingCharge = objGen.SafeSQL(Trim(ddlShippingCharges.SelectedValue))
            Else
                objSO.SPO_ShippingCharge = 0
            End If

            objSO.SPO_CreatedBy = sSession.UserID
            objSO.SPO_CreatedOn = DateTime.Today
            objSO.SPO_Status = "A"
            objSO.SPO_Operation = "C"
            objSO.SPO_IPAddress = sSession.IPAddress

            objSO.SPO_OrderType = "S"
            objSO.SPO_DispatchFlag = 0

            If ddlSalesMan.SelectedIndex > 0 Then
                objSO.SPO_SalesManID = objGen.SafeSQL(Trim(ddlSalesMan.SelectedValue))
            Else
                objSO.SPO_SalesManID = 0
            End If

            If txtBuyerPurOrderNo.Text <> "" Then
                objSO.SPO_BuyerOrderNo = objGen.SafeSQL(Trim(txtBuyerPurOrderNo.Text))
            Else
                objSO.SPO_BuyerOrderNo = "Oral"
            End If

            If ddlCategory.SelectedIndex > 0 Then
                objSO.SPO_Category = objGen.SafeSQL(Trim(ddlCategory.SelectedValue))
            Else
                objSO.SPO_Category = 0
            End If

            If txtRemarks.Text <> "" Then
                objSO.SPO_Remarks = objGen.SafeSQL(Trim(txtRemarks.Text))
            Else
                objSO.SPO_Remarks = ""
            End If

            If txtBuyerOrderDate.Text <> "" Then
                objSO.SPO_BuyerOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtBuyerOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            objSO.SPO_SalesType = 0
            objSO.SPO_OtherType = 0

            Dim dChequeDate As Date
            dChequeDate = "01/01/1900"

            objSO.SPO_ChequeNo = ""
            objSO.SPO_ChequeDate = dChequeDate
            objSO.SPO_IFSCCode = ""
            objSO.SPO_BankName = ""
            objSO.SPO_Branch = ""

            objSO.SPO_GoThroughDispatch = 0
            objSO.SPO_DispatchRefNo = ""
            objSO.SPO_ESugamNo = ""

            Dim dDispatchDate As Date
            dDispatchDate = "01/01/1900"
            objSO.SPO_DispatchDate = dDispatchDate

            '***********Preethika*************************
            objSO.SPO_ZoneID = ddlAccZone.SelectedValue
            objSO.SPO_RegionID = ddlAccRgn.SelectedValue
            objSO.SPO_AreaID = ddlAccArea.SelectedValue
            objSO.SPO_BranchID = ddlAccBrnch.SelectedValue

            objSO.SPO_CompanyAddress = ""
            objSO.SPO_BillingAddress = ""
            objSO.SPO_DeliveryFrom = ""
            objSO.SPO_DeliveryAddress = ""
            objSO.SPO_CompanyGSTNRegNo = ""
            objSO.SPO_BillingGSTNRegNo = ""
            objSO.SPO_DeliveryFromGSTNRegNo = ""
            objSO.SPO_DeliveryGSTNRegNo = ""
            objSO.SPO_DispatchStatus = ""
            objSO.SPO_CompanyType = 0
            objSO.SPO_GSTNCategory = 0
            objSO.SPO_State = ""

            If ddlBatchNo.SelectedIndex > 0 Then
                objSO.SPO_BatchNo = ddlBatchNo.SelectedValue
            Else
                objSO.SPO_BatchNo = 0
            End If
            objSO.SPO_BaseName = lstFiles.SelectedItem.Text  'BaseName ID

            Arr = objSO.SavePROFormaMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objSO)
            dt = Session("UpdateTab")
            iMasterID = Arr(1)

            objSO.SPOD_SOID = iMasterID

            If txtQuantitySales.Text <> "" Then

                objSO.SPOD_CommodityID = objGen.SafeSQL(Trim(ddlCommodityS.SelectedValue))
                objSO.SPOD_ItemID = objGen.SafeSQL(Trim(lstBoxDescription.SelectedValue))
                objSO.SPOD_Quantity = objGen.SafeSQL(Trim(txtQuantitySales.Text))

                'If ddlDiscount.SelectedIndex > 0 Then
                '    ObjSO.SPOD_Discount = objGen.SafeSQL(Trim(ddlDiscount.SelectedItem.Text))
                'Else
                '    ObjSO.SPOD_Discount = 0
                'End If

                objSO.SPOD_UnitofMeasurement = objGen.SafeSQL(Trim(ddlUnitOfMeassurement.SelectedValue))

                If hfAmount.Value <> "" Then
                    objSO.SPOD_RateAmount = Request.Form(hfAmount.UniqueID)
                Else
                    objSO.SPOD_RateAmount = txtAmount.Text
                End If

                'If hfDiscountAmount.Value <> "" Then
                '    ObjSO.SPOD_DiscountRate = Request.Form(hfDiscountAmount.UniqueID)
                'Else
                '    ObjSO.SPOD_DiscountRate = 0
                'End If

                If txtOrderDateS.Text <> "" Then
                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDateS.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                sCode = objSO.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPatryS.SelectedValue)
                If sCode.StartsWith("P") Then
                    sStr = "P"
                Else
                    sStr = "C"
                End If

                If txtHistoryID.Text <> "" Then
                    objSO.SPOD_HistoryID = txtHistoryID.Text
                Else
                    objSO.SPOD_HistoryID = 0
                End If
                'Working'
                'ObjSO.SPOD_HistoryID = ObjSO.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, ObjSO.SPO_Category, sStr, dOrderDate)
                'Working'

                objSO.SPOD_CompiD = sSession.AccessCodeID
                objSO.SPOD_Status = "A"

                If txtMRP.Text <> "" Then
                    hfMRP.Value = txtMRP.Text
                End If
                If hfMRP.Value <> "" Then
                    objSO.SPOD_MRPRate = hfMRP.Value
                Else
                    objSO.SPOD_MRPRate = 0
                End If

                If hfNetAmount.Value <> "" Then
                    objSO.SPOD_TotalAmount = Request.Form(hfNetAmount.UniqueID)
                Else
                    objSO.SPOD_TotalAmount = 0
                End If

                objSO.SPOD_Operation = "C"
                objSO.SPOD_IPAddress = sSession.IPAddress

                If ddlCategory.SelectedIndex > 0 Then
                    objSO.SPOD_Category = ddlCategory.SelectedValue
                Else
                    objSO.SPOD_Category = 0
                End If

                objSO.SPOD_CreatedBy = sSession.UserID
                objSO.SPOD_CreatedOn = DateTime.Today
                objSO.SPOD_UpdatedBy = sSession.UserID
                objSO.SPOD_UpdatedOn = DateTime.Today

                objSO.SPOD_GST_ID = 0
                objSO.SPOD_GSTRate = 0
                objSO.SPOD_GSTAmount = 0
                objSO.SPOD_SGST = 0
                objSO.SPOD_SGSTAmount = 0
                objSO.SPOD_CGST = 0
                objSO.SPOD_CGSTAmount = 0
                objSO.SPOD_IGST = 0
                objSO.SPOD_IGSTAmount = 0

                Arr = objSO.SavePROFormaMasterDetails(sSession.AccessCode, objSO, sSession.YearID)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnAddSale.ImageUrl = "~/Images/Add24.png"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
            End If

            LoadExistingSalesOrderNo()
            ddlExistingSalesNo.SelectedValue = iMasterID
            LoadExistingOrderGrid(ddlBatchNo.SelectedValue, lstFiles.SelectedValue)

            SalesClearDetails()
            lblStatusSO.Text = "Waiting For Approve"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddSales_Click")
        End Try
    End Sub
    Public Sub LoadExistingOrderGrid(ByVal iBatchNo As Integer, ByVal iBaseName As Integer)
        Dim dt As New DataTable
        Dim iTotal As Double
        Dim iGrandTotal As Double
        Try
            dt = objSO.BindExistingOrder(sSession.AccessCode, sSession.AccessCodeID, iBatchNo, iBaseName)
            If dt.Rows.Count > 0 Then
                dgExistingProFormaSalesOrder.DataSource = dt
                dgExistingProFormaSalesOrder.DataBind()
                dgExistingProFormaSalesOrder.Visible = True
                Session("PROFORMA") = dt
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If IsDBNull(dt.Rows(i)("NetAmount")) = False Then
                            iTotal = dt.Rows(i)("NetAmount")
                            iGrandTotal = iGrandTotal + iTotal
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingOrderGrid")
        End Try
    End Sub
    Private Sub lstBoxDescription_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxDescription.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""

        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer

        Dim dOrderDate As Date
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            hfAvailableQty.Value = ""
            lblError.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = "" : hfAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            If txtOrderDateS.Text <> "" Then
                If ddlPatryS.SelectedIndex > 0 Then
                    If lstBoxDescription.SelectedIndex <> -1 Then
                        ddlCommodityS.SelectedValue = objSO.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                        If txtOrderDateS.Text <> "" Then
                            dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDateS.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            'Cheque Date Comparision'
                            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            dSDate = Date.ParseExact(txtOrderDateS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            m = DateDiff(DateInterval.Day, dDate, dSDate)
                            If m < 0 Then
                                lblError.Text = "Order Date (" & txtOrderDateS.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                                lblValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                                txtOrderDate.Focus()
                                Exit Sub
                            End If

                            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            dSDate = Date.ParseExact(txtOrderDateS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            m = DateDiff(DateInterval.Day, dDate, dSDate)
                            If m > 0 Then
                                lblError.Text = "Order Date (" & txtOrderDateS.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                                lblValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                                txtOrderDate.Focus()
                                Exit Sub
                            End If
                            'Cheque Date Comparision'
                        End If
                        BindUnitOfMeassurement(lstBoxDescription.SelectedValue)

                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstBoxDescription_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindUnitOfMeassurement(ByVal iItemID As Integer)
        Try
            ddlUnitOfMeassurement.DataSource = objSO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iItemID)
            ddlUnitOfMeassurement.DataTextField = "Mas_Desc"
            ddlUnitOfMeassurement.DataValueField = "Mas_ID"
            ddlUnitOfMeassurement.DataBind()
            ddlUnitOfMeassurement.Items.Insert(0, "Select Unit Of Meassurement")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgExistingProFormaSalesOrder_PreRender(sender As Object, e As EventArgs) Handles dgExistingProFormaSalesOrder.PreRender
        Dim dt As New DataTable
        Try
            If dgExistingProFormaSalesOrder.Rows.Count > 0 Then
                dgExistingProFormaSalesOrder.UseAccessibleHeader = True
                dgExistingProFormaSalesOrder.HeaderRow.TableSection = TableRowSection.TableHeader
                dgExistingProFormaSalesOrder.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_PreRender")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            ddlCompany.SelectedIndex = 0 : ddlTrType.Items.Clear() : ddlAccZone.SelectedIndex = 0 : ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            ClearDC()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub
    Public Sub ClearDC()
        Try
            ddlExisting.Items.Clear() : lblStatus.Text = ""
            ddlBatchNo.Items.Clear() : ddlParty.Items.Clear() : txtVoucherNo.Text = "" : ddlPaymentType.SelectedIndex = 0 : txtTransactionNo.Text = "" : txtDate.Text = ""
            divPurchase.Visible = False : divSales.Visible = False : divRPJ.Visible = False : divPayment.Visible = False
            dgExistingProFormaSalesOrder.DataSource = Nothing
            dgExistingProFormaSalesOrder.DataBind()

            lstFiles.Items.Clear()
            lstFiles.DataSource = Nothing
            lstFiles.DataBind()

            lstDocument.Items.Clear()
            lstDocument.DataSource = Nothing
            lstDocument.DataBind()

            Dim sFile As String = String.Format("~/Images/SearchImage/NoImage.jpg")
            documentViewer.Document = sFile
            'RetrieveImage.ImageUrl = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Private Sub ddlTransactionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransactionType.SelectedIndexChanged
    '    Try
    '        ddlJEType.Visible = False
    '        If ddlTransactionType.SelectedIndex = 1 Then    'JE
    '            ddlJEType.Visible = True
    '            txtTrNo.Text = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
    '        ElseIf ddlTransactionType.SelectedIndex = 2 Then    'Petty Cash
    '            divcollapseChequeDetails.Visible = False
    '            txtTrNo.Text = objPC.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
    '        ElseIf ddlTransactionType.SelectedIndex = 3 Then    'Receipt

    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub imgbtnAddBillAmt_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddBillAmt.Click
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dTotalAmt As Double
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("PettyID")
            dt.Columns.Add("BillNo")
            dt.Columns.Add("BillDate")
            dt.Columns.Add("BillAmount")

            If IsNothing(Session("Petty")) = False Then
                dt = Session("Petty")
            End If

            dr = dt.NewRow
            dr("ID") = 0
            dr("PettyID") = 0
            dr("BillNo") = txtBillNo.Text
            dr("BillDate") = txtBillDate.Text
            dr("BillAmount") = txtBillAmount.Text

            dt.Rows.Add(dr)

            dgPetty.DataSource = dt
            dgPetty.DataBind()

            Session("Petty") = dt

            For i = 0 To dgPetty.Items.Count - 1
                dTotalAmt = dTotalAmt + Convert.ToDouble(dgPetty.Items(i).Cells(4).Text)
            Next
            txtOtherDAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = ""
            dTotalAmt = 0

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddBillAmt_Click")
        End Try
    End Sub
    Private Sub dgPetty_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPetty.ItemCommand
        Dim dt As New DataTable
        Dim dTotalAmt As Double
        Try
            If e.CommandName = "Delete" Then
                dt = Session("Petty")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                Session("Petty") = dt
            End If
            dgPetty.DataSource = dt
            dgPetty.DataBind()

            For i = 0 To dgPetty.Items.Count - 1
                dTotalAmt = dTotalAmt + Convert.ToDouble(dgPetty.Items(i).Cells(4).Text)
            Next
            txtOtherDAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPetty_ItemCommand")
        End Try
    End Sub
    Private Sub imgbtnSaveRPJ_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveRPJ.Click
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim dDate1, dSDate1 As Date : Dim m1 As Integer
        Try
            lblRPJ.Text = ""
            If ddlAccBrnch.SelectedIndex = 0 Then
                lblRPJ.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If

            If ddlTrType.SelectedItem.Text = "Journal Entry" Then    'JE
                lblRPJ.Text = ""
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblRPJ.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblRPJ.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If

                'dDate1 = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'dSDate1 = Date.ParseExact(txtReceiptBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'm1 = DateDiff(DateInterval.Day, dDate1, dSDate1)
                'If m1 < 0 Then
                '    lblRPJ.Text = "Invoice Date  (" & txtReceiptBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                '    lblValidationMsg.Text = lblError.Text
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                '    txtInvoiceDate.Focus()
                '    Exit Sub
                'End If

                'dDate1 = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'dSDate1 = Date.ParseExact(txtReceiptBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'm1 = DateDiff(DateInterval.Day, dDate1, dSDate1)
                'If m1 > 0 Then
                '    lblRPJ.Text = "Invoice Date (" & txtReceiptBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                '    lblValidationMsg.Text = lblError.Text
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                '    txtInvoiceDate.Focus()
                '    Exit Sub
                'End If

                If dgPaymentDetails.Items.Count = 0 Or dgPaymentDetails.Items.Count = Nothing Then
                    lblRPJ.Text = "Add Debit and Credit Details."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Debit and Credit Details.','', 'info');", True)
                    Exit Sub
                End If
                If dgPetty.Items.Count = 0 Or dgPetty.Items.Count = Nothing Then
                    lblRPJ.Text = "No data for Bill Details."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data for Bill Details..','', 'info');", True)
                    Exit Sub
                End If

                iRet = CheckDebitAndCredit()
                If iRet = 1 Then
                    lblRPJ.Text = "Debit Amount and Credit Amount Not Matched."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit Amount and Credit Amount Not Matched.','', 'info');", True)
                    Exit Sub
                ElseIf iRet = 2 Then
                    lblRPJ.Text = "Amount not Matched with Bill Amount."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not Matched with Bill Amount.','', 'info');", True)
                    Exit Sub
                End If
                SaveJE()
            ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then    'Petty
                '///preeti
                lblRPJ.Text = ""

                If dgPettyCashDetails.Items.Count = 0 Or dgPettyCashDetails.Items.Count = Nothing Then
                    lblRPJ.Text = "Add Debit and Credit Details."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Debit and Credit Details.','', 'info');", True)
                    Exit Sub
                End If

                iRet = CheckDebitAndCreditJE()
                If iRet = 1 Then
                    lblRPJ.Text = "Debit Amount and Credit Amount Not Matched."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit Amount and Credit Amount Not Matched.','', 'info');", True)
                    Exit Sub
                ElseIf iRet = 2 Then
                    lblRPJ.Text = "Amount not Matched with Bill Amount."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not Matched with Bill Amount.','', 'info');", True)
                    Exit Sub
                End If
                SavePetty()
            ElseIf ddlTrType.SelectedItem.Text = "Receipt" Then    'Receipt
                lblRPJ.Text = ""
                If ddlReceiptType.SelectedIndex = 0 Then
                    lblRPJ.Text = "Select the Receipt Type."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select the Receipt Type.','', 'info');", True)
                    Exit Sub
                End If
                If ddlReceiptTrType.SelectedIndex = 0 Then
                    lblRPJ.Text = "Select the Tr Type."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select the Tr Type.','', 'info');", True)
                    Exit Sub
                End If
                If ddlReceiptVoucherType.SelectedIndex = 0 Then
                    lblRPJ.Text = "Select the Voucher Type."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select the Voucher Type.','', 'info');", True)
                    Exit Sub
                End If
                If txtReceiptPaidAmt.Text = "" Then
                    lblRPJ.Text = "Enter the Paid Amount."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter the Paid Amount.','', 'info');", True)
                    Exit Sub
                End If

                If txtReceiptBillDate.Text = "" Then
                    lblRPJ.Text = "Enter the Invoice Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter the Invoice Date.','', 'info');", True)
                    Exit Sub
                End If

                If txtReceiptInvoiceAmt.Text = "" Then
                    lblRPJ.Text = "Enter the Invoice Amount."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter the Invoice Amount.','', 'info');", True)
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblRPJ.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblRPJ.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If

                dDate1 = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate1 = Date.ParseExact(txtReceiptBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m1 = DateDiff(DateInterval.Day, dDate1, dSDate1)
                If m1 < 0 Then
                    lblRPJ.Text = "Invoice Date  (" & txtReceiptBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If

                dDate1 = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate1 = Date.ParseExact(txtReceiptBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m1 = DateDiff(DateInterval.Day, dDate1, dSDate1)
                If m1 > 0 Then
                    lblRPJ.Text = "Invoice Date (" & txtReceiptBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If
                If dgPaymentDetails.Items.Count = 0 Or dgPaymentDetails.Items.Count = Nothing Then
                    lblRPJ.Text = "No data for Debit and Credit Details."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data for Debit and Credit Details.','', 'info');", True)
                    Exit Sub
                End If
                iRet = CheckDebitAndCredit()
                If iRet = 1 Then
                    lblRPJ.Text = "Debit Amount and Credit Amount Not Matched."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit Amount and Credit Amount Not Matched.','', 'info');", True)
                    Exit Sub
                ElseIf iRet = 2 Then
                    lblRPJ.Text = "Amount not Matched with Bill Amount."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not Matched with Bill Amount.','', 'info');", True)
                    Exit Sub
                End If
                SaveReceipt()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveRPJ_Click")
        End Try
    End Sub
    Public Sub SaveReceipt()
        Dim sArray As Array
        Dim sTransArray() As String
        Dim iReceiptID As Integer
        Dim sId As String
        Try
            lblRPJ.Text = ""
            If dgPaymentDetails.Items.Count = 0 Then
                lblRPJ.Text = "Add Debit and Credit details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Debit and Credit details','', 'success');", True)
                Exit Sub
            End If
            objRecp.iAcc_RM_ID = 0
            objRecp.sAcc_RM_TransactionNo = txtTrNo.Text

            If ddlCustomerParty.SelectedIndex > 0 Then
                objRecp.iAcc_RM_Location = ddlCustomerParty.SelectedIndex
            Else
                objRecp.iAcc_RM_Location = 0
            End If

            If ddlCSP.SelectedIndex > 0 Then
                objRecp.iAcc_RM_Party = ddlCSP.SelectedValue
            Else
                objRecp.iAcc_RM_Party = 0
                End If

            If ddlReceiptTrType.SelectedIndex > 0 Then
                objRecp.iAcc_RM_TransactionType = ddlReceiptTrType.SelectedIndex
            Else
                objRecp.iAcc_RM_TransactionType = 0
            End If

            If ddlReceiptVoucherType.SelectedIndex > 0 Then
                objRecp.iAcc_RM_BillType = ddlReceiptVoucherType.SelectedValue
            Else
                objRecp.iAcc_RM_BillType = 0
            End If

            If txtReceiptInvoiceNo.Text <> "" Then
                objRecp.sAcc_RM_BillNo = txtReceiptInvoiceNo.Text
            Else
                objRecp.sAcc_RM_BillNo = ""
            End If
            If txtReceiptBillDate.Text <> "" Then
                objRecp.dAcc_RM_BillDate = Date.ParseExact(txtReceiptBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objRecp.dAcc_RM_BillDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtReceiptInvoiceAmt.Text <> "" Then
                objRecp.dAcc_RM_BillAmount = txtReceiptInvoiceAmt.Text
            Else
                objRecp.dAcc_RM_BillAmount = 0
            End If

            objRecp.iAcc_RM_YearID = sSession.YearID
            objRecp.sAcc_RM_Status = "W"
            objRecp.iAcc_RM_CreatedBy = sSession.UserID
            objRecp.sAcc_RM_Operation = "U"
            objRecp.sAcc_RM_IPAddress = sSession.IPAddress
            objRecp.sAcc_RM_BillNarration = txtNarration.Text
            If txtReceiptBillDate.Text <> "" Then
                objRecp.dAcc_RM_InvoiceDate = Date.ParseExact(txtReceiptBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objRecp.dAcc_RM_InvoiceDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If ddlTransType.SelectedIndex = 2 Then 'Cheque Details                
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

            Else
                objRecp.sAcc_RM_ChequeNo = ""
                objRecp.dAcc_RM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objRecp.sAcc_RM_IFSCCode = ""
                objRecp.sAcc_RM_BankName = 0
                objRecp.sAcc_RM_BranchName = ""
            End If

            If txtReceiptPaidAmt.Text <> "" Then
                objRecp.dAcc_RM_PaidAmount = txtReceiptPaidAmt.Text
            Else
                objRecp.dAcc_RM_PaidAmount = 0
            End If

            If txtReceiptInvoiceAmt.Text <> "" Then
                objRecp.dAcc_RM_BalanceAmount = txtReceiptInvoiceAmt.Text - txtReceiptPaidAmt.Text
            Else
                objRecp.dAcc_RM_BalanceAmount = 0 - txtReceiptPaidAmt.Text
            End If

            objRecp.iAcc_RM_AttachID = iAttachID

            objRecp.iACC_RM_ZoneID = ddlAccZone.SelectedValue
            objRecp.iACC_RM_RegionID = ddlAccRgn.SelectedValue
            objRecp.iACC_RM_AreaID = ddlAccArea.SelectedValue
            objRecp.iACC_RM_BranchID = ddlAccBrnch.SelectedValue

            If txtReceiptSalesOrderNo.Text <> "" Then
                objRecp.iAcc_RM_OrderNO = txtReceiptSalesOrderNo.Text
            Else
                objRecp.iAcc_RM_OrderNO = 0
            End If
            If txtInvoiceDate.Text = "" Then
                objRecp.dAcc_RM_OrderDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objRecp.dAcc_RM_OrderDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If ddlReceiptType.SelectedIndex > 0 Then
                objRecp.iAcc_RM_PaymentType = ddlReceiptType.SelectedIndex
            Else
                objRecp.iAcc_RM_PaymentType = 0
            End If

            objRecp.dAcc_RM_FETotalAmt = 0
            objRecp.dAcc_RM_CurrencyAmt = 0
            objRecp.sAcc_RM_CurrencyTime = ""
            objRecp.iAcc_RM_Currency = 0
            objRecp.dAcc_RM_DiffAmount = 0

            If ddlBatchNo.SelectedIndex > 0 Then
                objRecp.Acc_RM_BatchNo = ddlBatchNo.SelectedValue
            Else
                objRecp.Acc_RM_BatchNo = 0
            End If
            objRecp.Acc_RM_BaseName = lstFiles.SelectedItem.Text  'BaseName ID

            sArray = objRecp.SaveReceiptMaster(sSession.AccessCode, sSession.AccessCodeID, objRecp)
            iReceiptID = sArray(1)

            If dgPaymentDetails.Items.Count > 0 Then
                For i = 0 To dgPaymentDetails.Items.Count - 1
                    objRecp.iATD_TrType = 3
                    objRecp.dATD_TransactionDate = Date.Today
                    objRecp.iATD_BillId = iReceiptID
                    objRecp.iATD_DbOrCr = dgPaymentDetails.Items(i).Cells(12).Text
                    objRecp.iATD_Head = dgPaymentDetails.Items(i).Cells(1).Text
                    objRecp.iATD_GL = dgPaymentDetails.Items(i).Cells(2).Text
                    objRecp.iATD_SubGL = dgPaymentDetails.Items(i).Cells(3).Text

                    If objRecp.iATD_DbOrCr = 1 Then
                        objRecp.dATD_Debit = dgPaymentDetails.Items(i).Cells(9).Text
                        objRecp.dATD_Credit = 0.00

                    ElseIf objRecp.iATD_DbOrCr = 2 Then
                        objRecp.dATD_Debit = 0.00
                        objRecp.dATD_Credit = dgPaymentDetails.Items(i).Cells(10).Text
                    End If

                    objRecp.iATD_CreatedBy = sSession.UserID
                    objRecp.sATD_Status = "A"
                    objRecp.iATD_YearID = sSession.YearID
                    objRecp.sATD_Operation = "U"
                    objRecp.sATD_IPAddress = sSession.IPAddress

                    objRecp.iATD_ZoneID = ddlAccZone.SelectedValue
                    objRecp.iATD_RegionID = ddlAccRgn.SelectedValue
                    objRecp.iATD_AreaID = ddlAccArea.SelectedValue
                    objRecp.iATD_BranchID = ddlAccBrnch.SelectedValue

                    sTransArray = objRecp.SaveTransactionsDetails(sSession.AccessCode, sSession.AccessCodeID, objRecp)
                    '//preeti
                    If sTransArray(0) = "2" Then
                        lblRPJ.Text = "Successfully Updated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
                        imgbtnSaveRPJ.ImageUrl = "~/Images/Save16.png"
                    ElseIf sTransArray(0) = "3" Then
                        lblRPJ.Text = "Successfully Saved & Waiting for Approval."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                        imgbtnSaveRPJ.ImageUrl = "~/Images/update16.png"
                    End If
                Next
            Else
                lblRPJ.Text = "Add Debit and Credit Details."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Debit and Credit Details.','', 'success');", True)
                    Exit Sub
                End If
                '//preeti
                LoadExistingTransactionRPJ(iReceiptID)
                ddlExistingTrnRPJ.SelectedValue = iReceiptID
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub SaveJE()
        Dim Arr() As String
        Dim objJEE As New ClsJE.JE
        Dim iTransID As Integer
        Dim sTransArray() As String
        Dim sId As String
        Try
            lblRPJ.Text = ""
            If dgPaymentDetails.Items.Count = 0 Then
                lblRPJ.Text = "Add Debit and Credit details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Debit and Credit details','', 'success');", True)
                Exit Sub
            End If


            objJE.iAcc_JE_ID = 0
            objJE.sAcc_JE_TransactionNo = txtTrNo.Text

            If ddlCustomerParty.SelectedIndex > 0 Then
                objJE.iAcc_JE_Location = ddlCustomerParty.SelectedIndex
            Else
                objJE.iAcc_JE_Location = 0
            End If

            If ddlCSP.SelectedIndex > 0 Then
                objJE.iAcc_JE_Party = ddlCSP.SelectedValue
            Else
                objJE.iAcc_JE_Party = 0
            End If

            If ddlBillType.SelectedIndex > 0 Then
                objJE.iAcc_JE_BillType = ddlBillType.SelectedValue
            Else
                objJE.iAcc_JE_BillType = 0
            End If

            If txtChequeNo.Text <> "" Then
                objJE.sAcc_JE_ChequeNo = txtChequeNo.Text
            Else
                objJE.sAcc_JE_ChequeNo = ""
            End If

            If txtChequeDate.Text = "" Then
                objJE.dAcc_JE_ChequeDate = "01/01/1900"
            Else
                objJE.dAcc_JE_ChequeDate = Date.ParseExact(txtChequeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtIFSC.Text <> "" Then
                objJE.sAcc_JE_IFSCCode = txtIFSC.Text
            Else
                objJE.sAcc_JE_IFSCCode = ""
            End If

            If ddlBankName.SelectedIndex > 0 Then
                objJE.sAcc_JE_BankName = ddlBankName.SelectedValue
            Else
                objJE.sAcc_JE_BankName = 0
            End If

            If txtBranchName.Text <> "" Then
                objJE.sAcc_JE_BranchName = txtBranchName.Text
            Else
                objJE.sAcc_JE_BranchName = ""
            End If

            If txtNarration.Text <> "" Then
                objJE.sAcc_JE_AdvanceNaration = txtNarration.Text
            Else
                objJE.sAcc_JE_AdvanceNaration = ""
            End If

            If txtBillNo.Text <> "" Then
                objJE.sAcc_JE_BillNo = txtBillNo.Text
            Else
                objJE.sAcc_JE_BillNo = ""
            End If

            If txtBillDate.Text <> "" Then
                objJE.dAcc_JE_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objJE.dAcc_JE_BillDate = "01/01/1990"
            End If

            If txtBillAmount.Text <> "" Then
                objJE.dAcc_JE_BillAmount = txtBillAmount.Text
            Else
                objJE.dAcc_JE_BillAmount = 0
            End If
            objJE.iAcc_JE_YearID = sSession.YearID
            objJE.sAcc_JE_Status = "W"
            objJE.iAcc_JE_CreatedBy = sSession.UserID
            objJE.iAcc_JE_UpdatedBy = sSession.UserID
            objJE.sAcc_JE_Operation = "C"
            objJE.sAcc_JE_IPAddress = sSession.IPAddress
            If txtInvoiceDate.Text <> "" Then
                objJE.dAcc_JE_InvoiceDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objJE.dAcc_JE_InvoiceDate = "01/01/1900"
            End If

            objJE.iAcc_JE_AttachID = iAttachID

            objJE.iACC_JE_ZoneID = ddlAccZone.SelectedValue
            objJE.iACC_JE_RegionID = ddlAccRgn.SelectedValue
            objJE.iACC_JE_AreaID = ddlAccArea.SelectedValue
            objJE.iACC_JE_BranchID = ddlAccBrnch.SelectedValue

            If ddlJEType.SelectedIndex > 0 Then
                objJE.iAcc_JE_JEType = ddlJEType.SelectedIndex
            Else
                objJE.iAcc_JE_JEType = 0
            End If

            If ddlBatchNo.SelectedIndex > 0 Then
                objJE.Acc_JE_BatchNo = ddlBatchNo.SelectedValue
            Else
                objJE.Acc_JE_BatchNo = 0
            End If
            objJE.Acc_JE_BaseName = lstFiles.SelectedItem.Text  'BaseName ID

            Arr = objJE.SaveJournalMaster(sSession.AccessCode, sSession.AccessCodeID, objJE)
            iTransID = Arr(1)

            'Multiple BillNo Saving Option'
            If dgPetty.Items.Count > 0 Then
                For j = 0 To dgPetty.Items.Count - 1
                    objJEE.iJE_ID = 0
                    objJEE.iJE_MasterID = iTransID
                    If (IsDBNull(dgPetty.Items(j).Cells(2).Text) = False) And (dgPetty.Items(j).Cells(2).Text <> "&nbsp;") Then
                        objJEE.sJE_BillNo = dgPetty.Items(j).Cells(2).Text
                    Else
                        objJEE.sJE_BillNo = ""
                    End If
                    If (IsDBNull(dgPetty.Items(j).Cells(3).Text) = False) And (dgPetty.Items(j).Cells(3).Text <> "&nbsp;") Then
                        objJEE.dJE_BillDate = Date.ParseExact(dgPetty.Items(j).Cells(3).Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objJEE.dJE_BillDate = "01/01/1900"
                    End If
                    If (IsDBNull(dgPetty.Items(j).Cells(4).Text) = False) And (dgPetty.Items(j).Cells(4).Text <> "&nbsp;") Then
                        objJEE.dJE_BillAmount = dgPetty.Items(j).Cells(4).Text
                    Else
                        objJEE.dJE_BillAmount = 0
                    End If
                    objJEE.sJE_Status = "W"
                    objJEE.iJE_CreatedBy = sSession.UserID
                    objJEE.dJE_CreatedOn = Date.Today
                    objJEE.iJE_CompID = sSession.AccessCodeID
                    objJEE.iJE_YearID = sSession.YearID
                    objJEE.sJE_Operation = "C"
                    objJEE.sJE_IPAddress = sSession.IPAddress

                    objJE.SaveJEBreakUp(sSession.AccessCode, sSession.AccessCodeID, objJEE)
                Next
            End If
            If dgPaymentDetails.Items.Count > 0 Then
                For i = 0 To dgPaymentDetails.Items.Count - 1
                    objJE.iATD_TrType = 4
                    objJE.iATD_BillId = iTransID
                    objJE.iATD_PaymentType = 0
                    objJE.dATD_TransactionDate = Date.Today

                    If (IsDBNull(dgPaymentDetails.Items(i).Cells(1).Text) = False) And (dgPaymentDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                        objJE.iATD_Head = dgPaymentDetails.Items(i).Cells(1).Text
                    Else
                        objJE.iATD_Head = 0
                    End If

                    If (IsDBNull(dgPaymentDetails.Items(i).Cells(2).Text) = False) And (dgPaymentDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                        objJE.iATD_GL = dgPaymentDetails.Items(i).Cells(2).Text
                    Else
                        objJE.iATD_GL = 0
                    End If

                    If (IsDBNull(dgPaymentDetails.Items(i).Cells(3).Text) = False) And (dgPaymentDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                        objJE.iATD_SubGL = dgPaymentDetails.Items(i).Cells(3).Text
                    Else
                        objJE.iATD_SubGL = 0
                    End If

                    If (IsDBNull(dgPaymentDetails.Items(i).Cells(9).Text) = False) And (dgPaymentDetails.Items(i).Cells(9).Text <> "&nbsp;") Then
                        objJE.dATD_Debit = Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                        objJE.iATD_DbOrCr = 1
                    Else
                        objJE.dATD_Debit = 0
                    End If

                    If (IsDBNull(dgPaymentDetails.Items(i).Cells(10).Text) = False) And (dgPaymentDetails.Items(i).Cells(10).Text <> "&nbsp;") Then
                        objJE.dATD_Credit = Convert.ToDouble(dgPaymentDetails.Items(i).Cells(10).Text)
                        objJE.iATD_DbOrCr = 2
                    Else
                        objJE.dATD_Credit = 0
                    End If

                    objJE.iATD_CreatedBy = sSession.UserID
                    objJE.iATD_UpdatedBy = sSession.UserID
                    objJE.sATD_Status = "A"
                    objJE.iATD_YearID = sSession.YearID
                    objJE.sATD_Operation = "C"
                    objJE.sATD_IPAddress = sSession.IPAddress

                    objJE.iATD_ZoneID = ddlAccZone.SelectedValue
                    objJE.iATD_RegionID = ddlAccRgn.SelectedValue
                    objJE.iATD_AreaID = ddlAccArea.SelectedValue
                    objJE.iATD_BranchID = ddlAccBrnch.SelectedValue

                    sTransArray = objJE.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objJE)
                    If sTransArray(0) = "2" Then
                        lblError.Text = "Successfully Updated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
                        imgbtnSaveRPJ.ImageUrl = "~/Images/Save16.png"
                    ElseIf sTransArray(0) = "3" Then
                        lblError.Text = "Successfully Saved & Waiting for Approval."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                        imgbtnSaveRPJ.ImageUrl = "~/Images/update16.png"
                    End If
                Next
            Else
                lblError.Text = "Add Debit and Credit Details."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Debit and Credit Details.','', 'success');", True)
                Exit Sub
            End If
            LoadExistingTransactionRPJ(iTransID)
            ddlExistingTrnRPJ.SelectedValue = iTransID
            dgPaymentDetails.DataSource = objJE.LoadSavedTransactionDetailsJE(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID)
            dgPaymentDetails.DataBind()
            'imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
            'lblRPJ.Text = "Waiting for Approval"
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SavePetty()
        Dim Arr() As String
        Dim objPett As New ClsPetty.Petty
        Dim iTransID As Integer
        Dim sTransArray() As String
        Dim sId As String
        Try
            lblError.Text = ""
            If dgPettyCashDetails.Items.Count = 0 Then
                lblError.Text = "Add Debit and Credit details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Debit and Credit details','', 'success');", True)
                Exit Sub
            End If

            objPC.iAcc_PCM_ID = 0
                objPC.sAcc_PCM_TransactionNo = txtTrNo.Text

            If ddlCustomerParty.SelectedIndex > 0 Then
                objPC.iAcc_PCM_Location = ddlCustomerParty.SelectedIndex
            Else
                objPC.iAcc_PCM_Location = 0
                End If

                If ddlCSP.SelectedIndex > 0 Then
                    objPC.iAcc_PCM_Party = ddlCSP.SelectedValue
                Else
                    objPC.iAcc_PCM_Party = 0
                End If

                If ddlBillType.SelectedIndex > 0 Then
                    objPC.iAcc_PCM_BillType = ddlBillType.SelectedValue
                Else
                    objPC.iAcc_PCM_BillType = 0
                End If

                If txtBillNo.Text <> "" Then
                    objPC.sAcc_PCM_BillNo = txtBillNo.Text
                Else
                    objPC.sAcc_PCM_BillNo = ""
                End If
                If txtBillDate.Text <> "" Then
                    objPC.dAcc_PCM_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    objPC.dAcc_PCM_BillDate = "01/01/1990"
                End If
                If txtBillAmount.Text <> "" Then
                    objPC.dAcc_PCM_BillAmount = txtBillAmount.Text
                Else
                    objPC.dAcc_PCM_BillAmount = 0
                End If
                objPC.sAcc_PCM_PaymentNarration = txtNarration.Text

                objPC.iAcc_PCM_YearID = sSession.YearID
                objPC.sAcc_PCM_Status = "W"
                objPC.iAcc_PCM_CreatedBy = sSession.UserID
                objPC.iAcc_PCM_UpdatedBy = sSession.UserID
                objPC.sAcc_PCM_Operation = "C"
                objPC.sAcc_PCM_IPAddress = sSession.IPAddress

                If txtInvoiceDate.Text <> "" Then
                    objPC.dAcc_PCM_InvoiceDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    objPC.dAcc_PCM_InvoiceDate = "01/01/1990"
                End If

                objPC.iAcc_PCM_AttachID = iAttachID

                objPC.iACC_PCM_ZoneID = ddlAccZone.SelectedValue
                objPC.iACC_PCM_RegionID = ddlAccRgn.SelectedValue
                objPC.iACC_PCM_AreaID = ddlAccArea.SelectedValue
                objPC.iACC_PCM_BranchID = ddlAccBrnch.SelectedValue

                If ddlBatchNo.SelectedIndex > 0 Then
                    objPC.Acc_PCM_BatchNo = ddlBatchNo.SelectedValue
                Else
                    objPC.Acc_PCM_BatchNo = 0
                End If
                objPC.Acc_PCM_BaseName = lstFiles.SelectedItem.Text  'BaseName ID

                Arr = objPC.SavePettyCashDetails(sSession.AccessCode, sSession.AccessCodeID, objPC)
                iTransID = Arr(1)

                'Multiple BillNo Saving Option'
                If dgPetty.Items.Count > 0 Then
                    For j = 0 To dgPetty.Items.Count - 1
                        objPett.iAPMD_ID = 0
                        objPett.iAPMD_MasterID = iTransID
                        If (IsDBNull(dgPetty.Items(j).Cells(2).Text) = False) And (dgPetty.Items(j).Cells(2).Text <> "&nbsp;") Then
                            objPett.sAPMD_BillNo = dgPetty.Items(j).Cells(2).Text
                        Else
                            objPett.sAPMD_BillNo = ""
                        End If
                        If (IsDBNull(dgPetty.Items(j).Cells(3).Text) = False) And (dgPetty.Items(j).Cells(3).Text <> "&nbsp;") Then
                            objPett.dAPMD_BillDate = Date.ParseExact(dgPetty.Items(j).Cells(3).Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Else
                            objPett.dAPMD_BillDate = "01/01/1900"
                        End If
                        If (IsDBNull(dgPetty.Items(j).Cells(4).Text) = False) And (dgPetty.Items(j).Cells(4).Text <> "&nbsp;") Then
                            objPett.dAPMD_BillAmount = dgPetty.Items(j).Cells(4).Text
                        Else
                            objPett.dAPMD_BillAmount = 0
                        End If
                        objPett.sAPMD_Status = "W"
                        objPett.iAPMD_CreatedBy = sSession.UserID
                        objPett.dAPMD_CreatedOn = Date.Today
                        objPett.iAPMD_CompID = sSession.AccessCodeID
                        objPett.iAPMD_YearID = sSession.YearID
                        objPett.sAPMD_Operation = "C"
                        objPett.sAPMD_IPAddress = sSession.IPAddress

                        objPC.SavePettyBreakUp(sSession.AccessCode, sSession.AccessCodeID, objPett)
                    Next
                End If
                If dgPettyCashDetails.Items.Count > 0 Then
                    For i = 0 To dgPettyCashDetails.Items.Count - 1
                        objPC.iATD_TrType = 2
                        objPC.iATD_BillId = iTransID
                        objPC.iATD_PaymentType = 0
                        objPC.dATD_TransactionDate = Date.Today

                        If (IsDBNull(dgPettyCashDetails.Items(i).Cells(1).Text) = False) And (dgPettyCashDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                            objPC.iATD_Head = dgPettyCashDetails.Items(i).Cells(1).Text
                        Else
                            objPC.iATD_Head = 0
                        End If


                        If (IsDBNull(dgPettyCashDetails.Items(i).Cells(2).Text) = False) And (dgPettyCashDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                            objPC.iATD_GL = dgPettyCashDetails.Items(i).Cells(2).Text
                        Else
                            objPC.iATD_GL = 0
                        End If

                        If (IsDBNull(dgPettyCashDetails.Items(i).Cells(3).Text) = False) And (dgPettyCashDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                            objPC.iATD_SubGL = dgPettyCashDetails.Items(i).Cells(3).Text
                        Else
                            objPC.iATD_SubGL = 0
                        End If

                        If (IsDBNull(dgPettyCashDetails.Items(i).Cells(10).Text) = False) And (dgPettyCashDetails.Items(i).Cells(10).Text <> "&nbsp;") Then
                            objPC.dATD_Debit = Convert.ToDouble(dgPettyCashDetails.Items(i).Cells(10).Text)
                            objPC.iATD_DbOrCr = 1
                        Else
                            objPC.dATD_Debit = 0
                        End If

                        If (IsDBNull(dgPettyCashDetails.Items(i).Cells(11).Text) = False) And (dgPettyCashDetails.Items(i).Cells(11).Text <> "&nbsp;") Then
                            objPC.dATD_Credit = Convert.ToDouble(dgPettyCashDetails.Items(i).Cells(11).Text)
                            objPC.iATD_DbOrCr = 2
                        Else
                            objPC.dATD_Credit = 0
                        End If

                        objPC.iATD_CreatedBy = sSession.UserID
                        objPC.iATD_UpdatedBy = sSession.UserID
                        objPC.sATD_Status = "A"
                        objPC.iATD_YearID = sSession.YearID
                        objPC.sATD_Operation = "C"
                        objPC.sATD_IPAddress = sSession.IPAddress

                        objPC.iATD_ZoneID = ddlAccZone.SelectedValue
                        objPC.iATD_RegionID = ddlAccRgn.SelectedValue
                        objPC.iATD_AreaID = ddlAccArea.SelectedValue
                        objPC.iATD_BranchID = ddlAccBrnch.SelectedValue

                        sTransArray = objPC.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPC)
                        If sTransArray(0) = "2" Then
                            lblRPJ.Text = "Successfully Updated."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
                            imgbtnSaveRPJ.ImageUrl = "~/Images/Save16.png"
                        ElseIf sTransArray(0) = "3" Then
                            lblRPJ.Text = "Successfully Saved & Waiting for Approval."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                            imgbtnSaveRPJ.ImageUrl = "~/Images/update16.png"
                        End If
                    Next
                Else
                    lblRPJ.Text = "Add Debit and Credit Details."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Debit and Credit Details.','', 'success');", True)
                    Exit Sub
                End If
                '///preeti
                LoadExistingTransactionRPJ(iTransID)
                ddlExistingTrnRPJ.SelectedValue = iTransID
                dgPettyCashDetails.DataSource = objPC.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID)
                dgPettyCashDetails.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveReceipt(ByVal iTransID As Integer)
        Try
            For i = 0 To dgPaymentDetails.Items.Count - 1

                objPC.iATD_TrType = 3
                objPC.iATD_BillId = iTransID
                objPC.iATD_PaymentType = 0
                objPC.dATD_TransactionDate = Date.Today

                If (IsDBNull(dgPaymentDetails.Items(i).Cells(1).Text) = False) And (dgPaymentDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objPC.iATD_Head = dgPaymentDetails.Items(i).Cells(1).Text
                Else
                    objPC.iATD_Head = 0
                End If

                If (IsDBNull(dgPaymentDetails.Items(i).Cells(2).Text) = False) And (dgPaymentDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objPC.iATD_GL = dgPaymentDetails.Items(i).Cells(2).Text
                Else
                    objPC.iATD_GL = 0
                End If

                If (IsDBNull(dgPaymentDetails.Items(i).Cells(3).Text) = False) And (dgPaymentDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objPC.iATD_SubGL = dgPaymentDetails.Items(i).Cells(3).Text
                Else
                    objPC.iATD_SubGL = 0
                End If

                If (IsDBNull(dgPaymentDetails.Items(i).Cells(9).Text) = False) And (dgPaymentDetails.Items(i).Cells(9).Text <> "&nbsp;") Then
                    objPC.dATD_Debit = Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                    objPC.iATD_DbOrCr = 1
                Else
                    objPC.dATD_Debit = 0
                End If

                If (IsDBNull(dgPaymentDetails.Items(i).Cells(10).Text) = False) And (dgPaymentDetails.Items(i).Cells(10).Text <> "&nbsp;") Then
                    objPC.dATD_Credit = Convert.ToDouble(dgPaymentDetails.Items(i).Cells(10).Text)
                    objPC.iATD_DbOrCr = 2
                Else
                    objPC.dATD_Credit = 0
                End If

                objPC.iATD_CreatedBy = sSession.UserID
                objPC.iATD_UpdatedBy = sSession.UserID
                objPC.sATD_Status = "A"
                objPC.iATD_YearID = sSession.YearID
                objPC.sATD_Operation = "C"
                objPC.sATD_IPAddress = sSession.IPAddress
                objPC.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPC)
            Next

            dgPaymentDetails.DataSource = objPC.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID)
            dgPaymentDetails.DataBind()
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True

            lblStatus.Text = "Waiting for Approval"
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function CheckDebitAndCreditJE() As Integer
        Dim i As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Try
            For i = 0 To dgPettyCashDetails.Items.Count - 1
                If (IsDBNull(dgPettyCashDetails.Items(i).Cells(10).Text) = False) And (dgPettyCashDetails.Items(i).Cells(10).Text <> "&nbsp;") Then
                    dDebit = dDebit + Convert.ToDouble(dgPettyCashDetails.Items(i).Cells(10).Text)
                End If

                If (IsDBNull(dgPettyCashDetails.Items(i).Cells(11).Text) = False) And (dgPettyCashDetails.Items(i).Cells(11).Text <> "&nbsp;") Then
                    dCredit = dCredit + Convert.ToDouble(dgPettyCashDetails.Items(i).Cells(11).Text)
                End If
            Next

            If dDebit <> dCredit Then
                Return 1  ' Debit and Credit amount not Matched
                'ElseIf dDebit <> txtBillAmount.Text.Trim Then
                '    Return 2
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CheckDebitAndCredit() As Integer
        Dim i As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Try
            For i = 0 To dgPaymentDetails.Items.Count - 1
                If (IsDBNull(dgPaymentDetails.Items(i).Cells(9).Text) = False) And (dgPaymentDetails.Items(i).Cells(9).Text <> "&nbsp;") Then
                    dDebit = dDebit + Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                End If

                If (IsDBNull(dgPaymentDetails.Items(i).Cells(10).Text) = False) And (dgPaymentDetails.Items(i).Cells(10).Text <> "&nbsp;") Then
                    dCredit = dCredit + Convert.ToDouble(dgPaymentDetails.Items(i).Cells(10).Text)
                End If
            Next

            If dDebit <> dCredit Then
                Return 1  ' Debit and Credit amount not Matched
                'ElseIf dDebit <> txtBillAmount.Text.Trim Then
                '    Return 2
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub imgbtnAddRPJ_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddRPJ.Click
        Try
            If ddlTrType.SelectedItem.Text = "Journal Entry" Then    'JE
                ClearJE()
            ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then    'Petty
                ClearPetty()
            ElseIf ddlTrType.SelectedItem.Text = "Receipt" Then    'Receipt
                ClearReceipt()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddRPJ_Click")
        End Try
    End Sub
    Public Sub ClearPetty()
        Dim dtNew As New DataTable
        Try
            lblError.Text = ""
            dtMerge = dtNew : Session("Datatable") = Nothing
            txtTransactionNo.Text = objPC.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlExisting.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.SelectedIndex = 0 : ddlBillType.SelectedIndex = 0
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = "" : ddlDrOtherHead.SelectedIndex = 0 : ddlCrOtherHead.SelectedIndex = 0
            lblStatus.Text = "" : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            dgPaymentDetails.DataSource = dtNew
            dgPaymentDetails.DataBind()
            LoadSubGL()
            ddlDbOtherGL.DataSource = dtNew
            ddlDbOtherGL.DataBind()

            ddlCrOtherGL.DataSource = dtNew
            ddlCrOtherGL.DataBind()

            Session("Petty") = Nothing
            dgPetty.DataSource = Nothing
            dgPetty.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub LoadJEType()
        Try
            ddlJEType.Items.Insert(0, "Select JE Type")
            ddlJEType.Items.Insert(1, "Purchase Correction")
            ddlJEType.Items.Insert(2, "Sales Correction")
            ddlJEType.Items.Insert(3, "Petty Cash Correction")
            ddlJEType.Items.Insert(4, "Payment Correction")
            ddlJEType.Items.Insert(5, "Recipt Correction")
            ddlJEType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub BindHeadofAccountsPay()
        Try
            ddlDrOtherHeadPay.Items.Insert(0, "Select Head of Account")
            ddlDrOtherHeadPay.Items.Insert(1, "Asset")
            ddlDrOtherHeadPay.Items.Insert(2, "Income")
            ddlDrOtherHeadPay.Items.Insert(3, "Expenditure")
            ddlDrOtherHeadPay.Items.Insert(4, "Liabilities")
            ddlDrOtherHeadPay.SelectedIndex = 0

            ddlCrOtherHeadPay.Items.Insert(0, "Select Head of Account")
            ddlCrOtherHeadPay.Items.Insert(1, "Asset")
            ddlCrOtherHeadPay.Items.Insert(2, "Income")
            ddlCrOtherHeadPay.Items.Insert(3, "Expenditure")
            ddlCrOtherHeadPay.Items.Insert(4, "Liabilities")
            ddlCrOtherHeadPay.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlDrOtherHeadPay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDrOtherHeadPay.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlDrOtherHeadPay.SelectedIndex > 0 Then
                ddlDbOtherGLPay.DataSource = objDataCap.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDrOtherHeadPay.SelectedIndex)
                ddlDbOtherGLPay.DataTextField = "GlDesc"
                ddlDbOtherGLPay.DataValueField = "gl_Id"
                ddlDbOtherGLPay.DataBind()
                ddlDbOtherGLPay.Items.Insert(0, "Select GL Code")
            Else
                ddlDbOtherGLPay.DataSource = dt
                ddlDbOtherGLPay.DataBind()
                LoadSubGL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDrOtherHeadPay_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlDbOtherGLPay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDbOtherGLPay.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlDbOtherGLPay.SelectedIndex > 0 Then
                ddlDbOtherSubGLPay.DataSource = objDataCap.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDbOtherGLPay.SelectedValue)
                ddlDbOtherSubGLPay.DataTextField = "GlDesc"
                ddlDbOtherSubGLPay.DataValueField = "gl_Id"
                ddlDbOtherSubGLPay.DataBind()
                ddlDbOtherSubGLPay.Items.Insert(0, "Select SubGL Code")
            Else
                ddlDbOtherSubGLPay.DataSource = dt
                ddlDbOtherSubGLPay.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDbOtherGLPay_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCrOtherHeadPay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherHeadPay.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCrOtherHeadPay.SelectedIndex > 0 Then
                ddlCrOtherGLPay.DataSource = objDataCap.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherHeadPay.SelectedIndex)
                ddlCrOtherGLPay.DataTextField = "GlDesc"
                ddlCrOtherGLPay.DataValueField = "gl_Id"
                ddlCrOtherGLPay.DataBind()
                ddlCrOtherGLPay.Items.Insert(0, "Select GL Code")
            Else
                ddlCrOtherGLPay.DataSource = dt
                ddlCrOtherGLPay.DataBind()
                LoadSubGL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrOtherHeadPay_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCrOtherGLPay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherGLPay.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCrOtherGLPay.SelectedIndex > 0 Then
                ddlCrOtherSubGLPay.DataSource = objDataCap.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherGLPay.SelectedValue)
                ddlCrOtherSubGLPay.DataTextField = "GlDesc"
                ddlCrOtherSubGLPay.DataValueField = "gl_Id"
                ddlCrOtherSubGLPay.DataBind()
                ddlCrOtherSubGLPay.Items.Insert(0, "Select SubGL Code")
            Else
                ddlCrOtherSubGLPay.DataSource = dt
                ddlCrOtherSubGLPay.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrOtherGLPay_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnDADDPay_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDADDPay.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dDebit As Double = 0
        Dim dtDetails As New DataTable
        Dim dtPayment As New DataTable
        Dim dDebitAmt As Double = 0.0 : Dim dCreditAmt As Double = 0.0 : Dim dCreditTotalAmt As Double = 0.0
        Try

            If IsNothing(Session("DataCapture")) Then
                dtPayment = dtDetails
            Else
                dtPayment = Session("DataCapture")
            End If

            dtCOA = objDataCap.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

            'Debit
            If ddlDbOtherGLPay.SelectedIndex > 0 Then
                iGL = ddlDbOtherGLPay.SelectedValue
            Else
                iGL = 0
            End If

            If ddlDbOtherSubGLPay.SelectedIndex > 0 Then
                iSubGL = ddlDbOtherSubGLPay.SelectedValue
            Else
                iSubGL = 0
            End If

            If txtOtherDAmountPay.Text <> "" Then
                dDebit = txtOtherDAmountPay.Text
            Else
                dDebit = 0.00
            End If

            dtPayment = objDataCap.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHeadPay.SelectedIndex, iGL, iSubGL, dDebit, 1, dtPayment, dtCOA)

            Session("DataCapture") = dtPayment
            dgPaymentDetails.DataSource = dtPayment
            dgPaymentDetails.DataBind()

            If ddlTrType.SelectedItem.Text = "Journal Entry" Or ddlTrType.SelectedItem.Text = "Petty Cash" Then
                dDebitAmt = txtOtherDAmountPay.Text
                If txtOtherCAmountPay.Text <> "" Then
                    dCreditAmt = txtOtherCAmountPay.Text
                Else
                    txtOtherCAmountPay.Text = txtOtherDAmountPay.Text
                End If
                dCreditTotalAmt = dCreditAmt + dDebitAmt
                txtOtherCAmountPay.Text = dCreditTotalAmt
            End If

            LoadSubGL()
            ddlDrOtherHeadPay.SelectedIndex = 0 : ddlDbOtherGLPay.Items.Clear() : ddlDbOtherSubGLPay.Items.Clear() : txtOtherDAmountPay.Text = "" : ddlCrOtherSubGLPay.Items.Clear()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDADDPay_Click")
        End Try
    End Sub

    Private Sub imgbtnOtherCADDPay_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnOtherCADDPay.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dCredit As Double = 0
        Dim dtDetails As New DataTable
        Dim dtPayment As New DataTable
        Try

            If IsNothing(Session("DataCapture")) Then
                dtPayment = dtDetails
            Else

                dtPayment = Session("DataCapture")
            End If

            dtCOA = objDataCap.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

            'Debit
            If ddlCrOtherGLPay.SelectedIndex > 0 Then
                iGL = ddlCrOtherGLPay.SelectedValue
            Else
                iGL = 0
            End If

            If ddlCrOtherSubGLPay.SelectedIndex > 0 Then
                iSubGL = ddlCrOtherSubGLPay.SelectedValue
            Else
                iSubGL = 0
            End If

            If txtOtherCAmountPay.Text <> "" Then
                dCredit = txtOtherCAmountPay.Text
            Else
                dCredit = 0.00
            End If

            dtPayment = objDataCap.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrOtherHeadPay.SelectedIndex, iGL, iSubGL, dCredit, 2, dtPayment, dtCOA)

            Session("DataCapture") = dtPayment
            dgPaymentDetails.DataSource = dtPayment
            dgPaymentDetails.DataBind()

            LoadSubGL()
            ddlCrOtherHeadPay.SelectedIndex = 0 : ddlCrOtherGLPay.Items.Clear() : ddlCrOtherSubGLPay.Items.Clear() : txtOtherCAmountPay.Text = "" : ddlDbOtherSubGLPay.Items.Clear()

            If ddlTrType.SelectedItem.Text = "Journal Entry" Then

            ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then
                Dim dTotalAmt As Double = 0
                For i = 0 To dgPetty.Items.Count - 1
                    dTotalAmt = dTotalAmt + Convert.ToDouble(dgPetty.Items(i).Cells(4).Text)
                Next
                'txtBillAmount.Text = txtCreditAmount.Text
                txtBillAmount.Text = dTotalAmt
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnOtherCADDPay_Click")
        End Try
    End Sub
    Private Sub ddlPartyPay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPartyPay.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dtParty As New DataTable, dtPay As New DataTable
        Dim chkField As New CheckBox
        Dim iPKID As Integer = 0
        Dim sSupplier As String = ""
        Dim dDebit As Double = 0, dCredit As Double = 0, dSum As Double = 0, dGridDebit As Double = 0, dGridSum As Double = 0, dValue As Double = 0
        Dim iExistID As Integer = 0
        Try
            lblError.Text = ""
            If ddlPartyPay.SelectedIndex > 0 Then
                If ddlCustomerPartyPay.SelectedIndex = 3 Then  ' General Ledger
                    ddlDrOtherHeadPay.SelectedIndex = objPayment.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlPartyPay.SelectedValue)
                    ddlDrOtherHeadPay_SelectedIndexChanged(sender, e)

                    ddlDbOtherGLPay.SelectedValue = ddlPartyPay.SelectedValue
                    ddlDbOtherGLPay_SelectedIndexChanged(sender, e)

                ElseIf ddlCustomerPartyPay.SelectedIndex = 1 Then   'Customer
                    dt = objPayment.getCustomerLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlPartyPay.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        ddlDrOtherHeadPay.SelectedIndex = objPayment.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("BM_GL").ToString())
                        ddlDrOtherHeadPay_SelectedIndexChanged(sender, e)

                        ddlDbOtherGLPay.SelectedValue = dt.Rows(0)("BM_GL").ToString()
                        ddlDbOtherGLPay_SelectedIndexChanged(sender, e)

                        If dt.Rows(0)("BM_SubGL").ToString() = "0" Then
                            ddlDbOtherSubGLPay.SelectedIndex = -1
                        Else
                            ddlDbOtherSubGLPay.SelectedValue = dt.Rows(0)("BM_SubGL").ToString()
                        End If
                    End If
                ElseIf ddlCustomerPartyPay.SelectedIndex = 2 Then   'Suppliers

                    'If ddlExistPayment.SelectedIndex > 0 Then
                    '    LoadExistingPurchaseOrder(ddlExistPayment.SelectedValue, ddlPaymentType.SelectedIndex, ddlParty.SelectedValue)
                    'Else
                    '    LoadExistingPurchaseOrder(0, ddlPaymentType.SelectedIndex, ddlParty.SelectedValue)
                    'End If

                    dt = objPayment.getSuppliersLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlPartyPay.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        ddlDrOtherHeadPay.SelectedIndex = objPayment.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CSM_GL").ToString())
                        ddlDrOtherHeadPay_SelectedIndexChanged(sender, e)

                        ddlDbOtherGLPay.SelectedValue = dt.Rows(0)("CSM_GL").ToString()
                        ddlDbOtherGLPay_SelectedIndexChanged(sender, e)

                        If dt.Rows(0)("CSM_SubGL").ToString() = "0" Then
                            ddlDbOtherSubGLPay.SelectedIndex = -1
                        Else
                            ddlDbOtherSubGLPay.SelectedValue = dt.Rows(0)("CSM_SubGL").ToString()
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPartyPay_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadSupplierPay()
        Try
            ddlPartyPay.DataSource = objPayment.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlPartyPay.DataTextField = "Name"
            ddlPartyPay.DataValueField = "CSM_ID"
            ddlPartyPay.DataBind()
            ddlPartyPay.Items.Insert(0, "Select Customer/Supplier/Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCustomersPay()
        Try
            ddlPartyPay.DataSource = objPayment.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlPartyPay.DataTextField = "Name"
            ddlPartyPay.DataValueField = "BM_ID"
            ddlPartyPay.DataBind()
            ddlPartyPay.Items.Insert(0, "Select Customer/Supplier/Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadPartyPay(ByVal iType As Integer)
        Try
            If iType = 3 Then
                ddlPartyPay.DataSource = objPayment.LoadAllGLCodes(sSession.AccessCode, sSession.AccessCodeID)
                ddlPartyPay.DataTextField = "GlDesc"
                ddlPartyPay.DataValueField = "gl_Id"
                ddlPartyPay.DataBind()
                ddlPartyPay.Items.Insert(0, "Select Customer/Supplier/Party")
            ElseIf iType = 1 Then
                ddlPartyPay.DataSource = objPayment.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
                ddlPartyPay.DataTextField = "Name"
                ddlPartyPay.DataValueField = "BM_ID"
                ddlPartyPay.DataBind()
                ddlPartyPay.Items.Insert(0, "Select Customer/Supplier/Party")
            ElseIf iType = 2 Then
                ddlPartyPay.DataSource = objPayment.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
                ddlPartyPay.DataTextField = "Name"
                ddlPartyPay.DataValueField = "CSM_ID"
                ddlPartyPay.DataBind()
                ddlPartyPay.Items.Insert(0, "Select Customer/Supplier/Party")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCustomerPartyPay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerPartyPay.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlCustomerPartyPay.SelectedIndex = 1 Then
                lblParty.Text = "* Customer"
                If ddlTransType.SelectedIndex = 0 Then
                    ddlCustomerPartyPay.SelectedIndex = 0
                End If
                If ddlTransType.SelectedIndex > 0 Then
                    LoadCustomersPay()
                End If
            ElseIf ddlCustomerPartyPay.SelectedIndex = 2 Then
                lblParty.Text = "* Supplier"
                If ddlTransType.SelectedIndex = 0 Then
                    ddlCustomerPartyPay.SelectedIndex = 0
                End If
                If ddlTransType.SelectedIndex > 0 Then
                    LoadSupplierPay()
                End If
            ElseIf ddlCustomerPartyPay.SelectedIndex = 3 Then
                lblParty.Text = "* General Ledger"
                LoadPartyPay(3)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerPartyPay_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub txtPaidAmount_TextChanged(sender As Object, e As EventArgs) Handles txtPaidAmount.TextChanged
        Dim dtCOA As New DataTable
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Try
            lblError.Text = ""

            If ddlDbOtherSubGLPay.Items.Count > 1 Then
                If ddlDbOtherSubGLPay.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Debit."
                    txtPaidAmount.Text = 0
                    Exit Sub
                End If
            End If

            If ddlCrOtherSubGLPay.Items.Count > 1 Then
                If ddlCrOtherSubGLPay.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Credit."
                    txtPaidAmount.Text = 0
                    Exit Sub
                End If
            End If

            If txtPaidAmount.Text <> "" Then
                dtCOA = objPayment.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

                'Debit
                If ddlDbOtherGLPay.SelectedIndex > 0 Then
                    iGL = ddlDbOtherGLPay.SelectedValue
                Else
                    iGL = 0
                End If

                If ddlDbOtherSubGLPay.SelectedIndex > 0 Then
                    iSubGL = ddlDbOtherSubGLPay.SelectedValue
                Else
                    iSubGL = 0
                End If

                dtPayment = objPayment.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHeadPay.SelectedIndex, iGL, iSubGL, txtPaidAmount.Text, 1, dtPayment, dtCOA)

                'Credit
                If ddlCrOtherGLPay.SelectedIndex > 0 Then
                    iGL = ddlCrOtherGLPay.SelectedValue
                Else
                    iGL = 0
                End If

                If ddlCrOtherSubGLPay.SelectedIndex > 0 Then
                    iSubGL = ddlCrOtherSubGLPay.SelectedValue
                Else
                    iSubGL = 0
                End If

                dtPayment = objPayment.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrOtherHeadPay.SelectedIndex, iGL, iSubGL, txtPaidAmount.Text, 2, dtPayment, dtCOA)

                Session("dtPayment") = dtPayment
                dgPaymentDetails.DataSource = dtPayment
                dgPaymentDetails.DataBind()

                'txtBalanceAmt_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtPaidAmount_TextChanged")
        End Try
    End Sub
    Private Sub ddlTransType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransType.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlTransType.SelectedIndex > 0 Then
                ddlCrOtherGLPay.DataSource = dt
                ddlCrOtherGLPay.DataBind()

                ddlCrOtherSubGLPay.DataSource = dt
                ddlCrOtherSubGLPay.DataBind()
                If ddlTransType.SelectedIndex = 1 Then  'Cash

                    Dim sPerm As String = ""
                    Dim sArray1 As Array
                    sPerm = objPayment.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Cash", "Cash")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")

                    ddlCrOtherHeadPay.SelectedIndex = sArray1(0) '1
                    ddlCrOtherHeadPay_SelectedIndexChanged(sender, e)
                    ddlCrOtherGLPay.SelectedValue = sArray1(3) '70
                    ddlCrOtherGLPay_SelectedIndexChanged(sender, e)
                    DivcollapseChequeDetailsPay.Visible = False

                ElseIf ddlTransType.SelectedIndex = 2 Then  'Bank

                    Dim sPerm As String = ""
                    Dim sArray1 As Array
                    sPerm = objPayment.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Bank", "Bank")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")

                    ddlCrOtherHeadPay.SelectedIndex = sArray1(0) '1
                    ddlCrOtherHeadPay_SelectedIndexChanged(sender, e)
                    ddlCrOtherGLPay.SelectedValue = sArray1(3) '71
                    ddlCrOtherGLPay_SelectedIndexChanged(sender, e)
                    DivcollapseChequeDetailsPay.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTransType_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSavePay_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSavePay.Click
        Dim dDebit As Double = 0, dCredit As Double = 0, dSum As Double = 0, dSDebit As Double = 0
        Dim arr As Array
        Dim iPayment As Integer = 0

        Dim sBillNo As String = "", sBillName As String = ""
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim lblPMID As New Label : Dim lblBillNo As New Label

        Dim dDatel, dSDateo As Date
        Dim dGridDebit As Double = 0 : Dim dGridCredit As Double = 0
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            lblError.Text = ""

            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date of Payment (" & txtInvoiceDatePay.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtInvoiceDatePay.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date of Payment (" & txtInvoiceDatePay.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtInvoiceDatePay.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'
            If txtBillDate.Text <> "" Then
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtBillDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Bill Date (" & txtBillDatePay.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtBillDatePay.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtBillDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Bill Date (" & txtBillDatePay.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtBillDatePay.Focus()
                    Exit Sub
                End If
                If txtBillDate.Text <> "" Then
                    dDatel = Date.ParseExact(txtBillDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDateo = Date.ParseExact(txtInvoiceDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim f As Integer
                    f = DateDiff(DateInterval.Day, dDatel, dSDateo)
                    If f < 0 Then
                        lblError.Text = "Payment Date (" & txtInvoiceDatePay.Text & ") should be Greater than or equal to Bill Date(" & txtBillDatePay.Text & ")."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Payment Date (" & txtInvoiceDate.Text & ") should be Greater than or equal to Bill Date(" & txtBillDate.Text & ").','', 'success');", True)
                        txtBillDatePay.Focus()
                        Exit Sub
                    End If
                End If
            End If

            If dgPaymentDetails.Items.Count = 0 Then
                lblError.Text = "Add Amount"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Amount','', 'success');", True)
                Exit Sub
            End If

            For i = 0 To dgPaymentDetails.Items.Count - 1
                dGridDebit = dGridDebit + Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                dGridCredit = dGridCredit + Convert.ToDouble(dgPaymentDetails.Items(i).Cells(10).Text)
            Next
            If dGridDebit <> dGridCredit Then
                lblError.Text = "Debit And Credit Amount not matching."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit And Credit Amount not matching','', 'success');", True)
                Exit Sub
            End If
            For i = 0 To dgPaymentDetails.Items.Count - 1
                dSDebit = Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                dSum = dSum + dSDebit
            Next
            'If txtBillAmount.Text <> "" Then
            '    If dSum <> Convert.ToDouble(txtBillAmount.Text) Then
            '        lblError.Text = "Debit And Credit Amount not matching with Balance Amount."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit And Credit Amount not matching with Balance Amount','', 'success');", True)
            '        Exit Sub
            '    End If
            'End If

            If ddlExistPayment.SelectedIndex > 0 Then
                objPayment.iAcc_PM_ID = ddlExistPayment.SelectedValue
            Else
                objPayment.iAcc_PM_ID = 0
            End If

            objPayment.sAcc_PM_TransactionNo = txtTransactionNoPay.Text

            If ddlCustomerPartyPay.SelectedIndex > 0 Then
                objPayment.iAcc_PM_Location = ddlCustomerPartyPay.SelectedIndex
            Else
                objPayment.iAcc_PM_Location = 0
            End If

            If ddlPartyPay.SelectedIndex > 0 Then
                objPayment.iAcc_PM_Party = ddlPartyPay.SelectedValue
            Else
                objPayment.iAcc_PM_Party = 0
            End If

            If ddlTransType.SelectedIndex > 0 Then
                objPayment.iAcc_PM_TransactionType = ddlTransType.SelectedIndex
            Else
                objPayment.iAcc_PM_TransactionType = 0
            End If

            If ddlBillTypePay.SelectedIndex > 0 Then
                objPayment.iAcc_PM_BillType = ddlBillTypePay.SelectedValue
            Else
                objPayment.iAcc_PM_BillType = 0
            End If

            If txtBillNoPay.Text <> "" Then
                objPayment.sAcc_PM_BillNo = txtBillNoPay.Text
            Else
                objPayment.sAcc_PM_BillNo = ""
            End If
            If txtBillDatePay.Text <> "" Then
                objPayment.dAcc_PM_BillDate = Date.ParseExact(txtBillDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objPayment.dAcc_PM_BillDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtBillAmountPay.Text <> "" Then
                objPayment.dAcc_PM_BillAmount = txtBillAmountPay.Text
            Else
                objPayment.dAcc_PM_BillAmount = 0
            End If

            objPayment.iAcc_PM_YearID = sSession.YearID
            objPayment.sAcc_PM_Status = "W"
            objPayment.iAcc_PM_CreatedBy = sSession.UserID
            objPayment.sAcc_PM_Operation = "U"
            objPayment.sAcc_PM_IPAddress = sSession.IPAddress
            objPayment.sAcc_Bill_Narration = txtNarrationPay.Text
            objPayment.sAcc_PM_AdvanceNaration = txtNarrationPay.Text
            objPayment.iAcc_PM_CompID = sSession.AccessCodeID
            If txtInvoiceDatePay.Text <> "" Then
                objPayment.dAcc_PM_InvoiceDate = Date.ParseExact(txtInvoiceDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objPayment.dAcc_PM_InvoiceDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If ddlTransType.SelectedIndex = 2 Then

                If txtChequeNoPay.Text <> "" Then
                    objPayment.sAcc_PM_ChequeNo = txtChequeNoPay.Text
                Else
                    objPayment.sAcc_PM_ChequeNo = ""
                End If

                If txtChequeDatePay.Text = "" Then
                    objPayment.dAcc_PM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    objPayment.dAcc_PM_ChequeDate = Date.ParseExact(txtChequeDatePay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If

                If txtIFSCPay.Text <> "" Then
                    objPayment.sAcc_PM_IFSCCode = txtIFSCPay.Text
                Else
                    objPayment.sAcc_PM_IFSCCode = ""
                End If

                If ddlBankPay.SelectedIndex > 0 Then
                    objPayment.sAcc_PM_BankName = ddlBankPay.SelectedValue
                Else
                    objPayment.sAcc_PM_BankName = 0
                End If

                If txtBranchPay.Text <> "" Then
                    objPayment.sAcc_PM_BranchName = txtBranchPay.Text
                Else
                    objPayment.sAcc_PM_BranchName = ""
                End If

            Else
                objPayment.sAcc_PM_ChequeNo = ""
                objPayment.dAcc_PM_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objPayment.sAcc_PM_IFSCCode = ""
                objPayment.sAcc_PM_BankName = 0
                objPayment.sAcc_PM_BranchName = ""
            End If

            If txtPaidAmount.Text <> "" Then
                objPayment.dAcc_PM_PaidAmount = txtPaidAmount.Text
            Else
                objPayment.dAcc_PM_PaidAmount = 0
            End If

            If sBillNo <> "" Then
                objPayment.dAcc_PM_BalanceAmount = 0
            Else
                If txtBillAmount.Text <> "" Then
                    objPayment.dAcc_PM_BalanceAmount = txtBillAmount.Text - txtPaidAmount.Text
                Else
                    objPayment.dAcc_PM_BalanceAmount = 0 - txtPaidAmount.Text
                End If
            End If
            objPayment.iAcc_PM_AttachID = iAttachID

            '********************Preethika ***********************************************
            objPayment.iACC_PM_ZoneID = ddlAccZone.SelectedValue
            objPayment.iACC_PM_RegionID = ddlAccRgn.SelectedValue
            objPayment.iACC_PM_AreaID = ddlAccArea.SelectedValue
            objPayment.iACC_PM_BranchID = ddlAccBrnch.SelectedValue

            objPayment.iAcc_PM_OrderNO = 0

            If txtOrderDate.Text = "" Then
                objPayment.dAcc_PM_OrderDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objPayment.dAcc_PM_OrderDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If ddlPaymentTypePay.SelectedIndex > 0 Then
                objPayment.iAcc_PM_PaymentType = ddlPaymentTypePay.SelectedIndex
            Else
                objPayment.iAcc_PM_PaymentType = 0
            End If

            objPayment.sAcc_PM_FETotalAmt = 0
            objPayment.dAcc_PM_CurrencyAmt = 0
            objPayment.sAcc_PM_CurrencyTime = ""
            objPayment.iAcc_PM_Currency = 0
            objPayment.dAcc_PM_DiffAmount = 0

            If ddlBatchNo.SelectedIndex > 0 Then
                objPayment.Acc_PM_BatchNo = ddlBatchNo.SelectedValue
            Else
                objPayment.Acc_PM_BatchNo = 0
            End If
            objPayment.Acc_PM_BaseName = lstFiles.SelectedItem.Text  'BaseName ID

            arr = objPayment.SavePaymentMaster(sSession.AccessCode, sSession.AccessCodeID, 0, objPayment)
            iPayment = arr(1)

            Dim lblId As New Label
            If dgPaymentDetails.Items.Count > 0 Then
                For i = 0 To dgPaymentDetails.Items.Count - 1
                    lblId = dgPaymentDetails.Items(i).FindControl("lblId")
                    objPayment.iATD_ID = lblId.Text
                    objPayment.iATD_TrType = 1
                    objPayment.iATD_BillId = iPayment
                    objPayment.iATD_PaymentType = 0
                    objPayment.iATD_DbOrCr = dgPaymentDetails.Items(i).Cells(12).Text
                    objPayment.iATD_Head = dgPaymentDetails.Items(i).Cells(1).Text
                    objPayment.iATD_GL = dgPaymentDetails.Items(i).Cells(2).Text
                    objPayment.iATD_SubGL = dgPaymentDetails.Items(i).Cells(3).Text
                    If objPayment.iATD_DbOrCr = 1 Then
                        dDebit = dgPaymentDetails.Items(i).Cells(9).Text
                        objPayment.dATD_Debit = dDebit
                        objPayment.dATD_Credit = 0.00
                    Else
                        dCredit = dgPaymentDetails.Items(i).Cells(10).Text
                        objPayment.dATD_Debit = 0.00
                        objPayment.dATD_Credit = dCredit
                    End If

                    objPayment.iATD_CreatedBy = sSession.UserID
                    objPayment.sATD_Status = "A"
                    objPayment.iATD_YearID = sSession.YearID
                    objPayment.sATD_Operation = "C"
                    objPayment.sATD_IPAddress = sSession.IPAddress
                    objPayment.iATD_CompID = sSession.AccessCodeID

                    objPayment.iATD_ZoneID = ddlAccZone.SelectedValue
                    objPayment.iATD_RegionID = ddlAccRgn.SelectedValue
                    objPayment.iATD_AreaID = ddlAccArea.SelectedValue
                    objPayment.iATD_BranchID = ddlAccBrnch.SelectedValue

                    objPayment.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, 0, objPayment)
                Next
            End If

            iAdd = 0
            'ddlParty_SelectedIndexChanged(sender, e)
            PartyDetails(sender, e)
            dtPayment = objPayment.LoadSavedTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPayment)
            Session("dtPayment") = dtPayment

            dgPaymentDetails.DataSource = dtPayment
            dgPaymentDetails.DataBind()

            LoadExistingPayment()
            ddlExistPayment.SelectedValue = iPayment

            lblError.Text = "Successfully Saved & Waiting for Approval."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
            lblStatus.Text = "Waiting for Approval"
            imgbtnSavePay.ImageUrl = "~/Images/Update16.png"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), " document.getElementById('ModalBillAdjusment').style.display='none';", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSavePay_Click")
        End Try
    End Sub
    Private Sub LoadExistingPayment()
        Try
            ddlExistPayment.DataSource = objPayment.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            ddlExistPayment.DataTextField = "Acc_PM_TransactionNo"
            ddlExistPayment.DataValueField = "Acc_PM_ID"
            ddlExistPayment.DataBind()
            ddlExistPayment.Items.Insert(0, "Existing Payment Voucher")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub PartyDetails(sender, e)
        Dim dt As New DataTable
        Dim dtParty As New DataTable, dtPay As New DataTable
        Dim chkField As New CheckBox
        Dim iPKID As Integer = 0
        Dim sSupplier As String = ""
        Dim dDebit As Double = 0, dCredit As Double = 0, dSum As Double = 0, dGridDebit As Double = 0, dGridSum As Double = 0, dValue As Double = 0
        Dim iExistID As Integer = 0
        Try
            lblError.Text = ""
            If ddlPartyPay.SelectedIndex > 0 Then
                If ddlPaymentTypePay.SelectedIndex = 3 Then  ' General Ledger
                    ddlDrOtherHeadPay.SelectedIndex = objPayment.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlPartyPay.SelectedValue)
                    ddlDrOtherHeadPay_SelectedIndexChanged(sender, e)

                    ddlDbOtherGLPay.SelectedValue = ddlParty.SelectedValue
                    ddlDbOtherGLPay_SelectedIndexChanged(sender, e)

                ElseIf ddlPaymentTypePay.SelectedIndex = 1 Then   'Customer
                    dt = objPayment.getCustomerLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlPartyPay.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        ddlDrOtherHeadPay.SelectedIndex = objPayment.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("BM_GL").ToString())
                        ddlDrOtherHeadPay_SelectedIndexChanged(sender, e)

                        ddlDbOtherGLPay.SelectedValue = dt.Rows(0)("BM_GL").ToString()
                        ddlDbOtherGLPay_SelectedIndexChanged(sender, e)

                        If dt.Rows(0)("BM_SubGL").ToString() = "0" Then
                            ddlDbOtherSubGLPay.SelectedIndex = -1
                        Else
                            ddlDbOtherSubGLPay.SelectedValue = dt.Rows(0)("BM_SubGL").ToString()
                        End If
                    End If
                ElseIf ddlPaymentTypePay.SelectedIndex = 2 Then   'Suppliers

                    'If ddlExistPayment.SelectedIndex > 0 Then
                    '    LoadExistingPurchaseOrder(ddlExistPayment.SelectedValue, ddlPaymentType.SelectedIndex, ddlParty.SelectedValue)
                    'Else
                    '    LoadExistingPurchaseOrder(0, ddlPaymentType.SelectedIndex, ddlParty.SelectedValue)
                    'End If

                    dt = objPayment.getSuppliersLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlPartyPay.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        ddlDrOtherHeadPay.SelectedIndex = objPayment.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CSM_GL").ToString())
                        ddlDrOtherHeadPay_SelectedIndexChanged(sender, e)

                        ddlDbOtherGLPay.SelectedValue = dt.Rows(0)("CSM_GL").ToString()
                        ddlDbOtherGLPay_SelectedIndexChanged(sender, e)

                        If dt.Rows(0)("CSM_SubGL").ToString() = "0" Then
                            ddlDbOtherSubGLPay.SelectedIndex = -1
                        Else
                            ddlDbOtherSubGLPay.SelectedValue = dt.Rows(0)("CSM_SubGL").ToString()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAddPay_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddPay.Click
        Try
            ClearPayment()
            imgbtnSavePay.ImageUrl = "~/Images/Save16.png"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddPay_Click")
        End Try
    End Sub
    Private Sub imgbtnApprovePay_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprovePay.Click
        Dim sStatus As String = ""
        Try
            sStatus = objPayment.getStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPayment.SelectedValue)
            If sStatus = "W" Then
                objPayment.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistPayment.SelectedValue, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                objPayment.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, ddlExistPayment.SelectedValue, sSession.YearID, sSession.UserID, sSession.IPAddress)
                lblError.Text = "Successfully Approved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'info');", True)
            ElseIf sStatus = "D" Then
                objPayment.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistPayment.SelectedValue, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                lblError.Text = "Successfully Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
            ElseIf sStatus = "A" Then
                lblError.Text = "This is already approved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This is already approved','', 'success');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprovePay_Click")
        End Try
    End Sub

    Private Sub chkCategoryGIN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategoryGIN.SelectedIndexChanged
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            lblError.Text = ""
            If (chkCategoryGIN.SelectedValue > 0) Then

                If txtInvoiceDateGIN.Text <> "" Then
                    'Cheque Date Comparision'
                    dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtInvoiceDateGIN.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m < 0 Then
                        lblError.Text = "Invoice Date (" & txtInvoiceDateGIN.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                        lblValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                        txtInvoiceDateGIN.Focus()
                        Exit Sub
                    End If

                    dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtInvoiceDateGIN.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m > 0 Then
                        lblError.Text = "Invoice Date (" & txtInvoiceDateGIN.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                        lblValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                        txtInvoiceDateGIN.Focus()
                        Exit Sub
                    End If
                    'Cheque Date Comparision'
                End If

                ddlCommodityGIN.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategoryGIN.SelectedValue)
            End If
            lblDescID.Text = chkCategoryGIN.SelectedValue
            LoadUnitGIN()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategoryGIN_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub chkCategoryPR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategoryPR.SelectedIndexChanged
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            lblError.Text = ""
            If (chkCategoryPR.SelectedValue > 0) Then

                If txtReturnDate.Text <> "" Then
                    'Cheque Date Comparision'
                    dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtReturnDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m < 0 Then
                        lblError.Text = "Return Date (" & txtReturnDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                        lblValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                        txtReturnDate.Focus()
                        Exit Sub
                    End If

                    dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtReturnDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m > 0 Then
                        lblError.Text = "Return Date (" & txtReturnDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                        lblValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                        txtReturnDate.Focus()
                        Exit Sub
                    End If
                    'Cheque Date Comparision'
                End If

                ddlCommodityPR.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategoryPR.SelectedValue)
            End If
            lblDescID.Text = chkCategoryPR.SelectedValue
            LoadUnitPR()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategoryPR_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSaveGIN_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveGIN.Click
        Dim Arr() As String
        Dim ObjGoods As New ClsGIN
        Dim iMasterID As Integer
        Dim sCurrentMonth As String = "", sYear As String = "", sStaus As String = "", sStatus As String = "", Check As String
        Dim ddlUnit As New DropDownList
        Dim lblMRP As New Label
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
        Dim row As GridViewRow
        Dim dDate, dSDate As Date : Dim m As Integer

        Dim objGoodsReturn As New clsGoodsReturn
        Dim purchaseReturnNo As String = "" : Dim dtMaster As New DataTable
        Try
            lblStatus.Text = ""

            ''Goods Return No Generation'
            'purchaseReturnNo = objGoodsReturn.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            ''Goods Return No Generation'

            If txtInvoiceDateGIN.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDateGIN.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Invoice Date (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtInvoiceDateGIN.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDateGIN.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Invoice Date (" & txtInvoiceDateGIN.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtInvoiceDateGIN.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            lblError.Text = ""
            If (txtDocRefNo.Text = "") Then
                lblError.Text = "Enter Document reference"
                Exit Sub
            End If
            Check = objGIN.CheckInwardedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, txtDocRefNo.Text)
            If (Check = True) Then
                lblError.Text = "Document reference no already exist"
                Exit Sub
            Else
                'sCurrentMonthID = objGIN.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'sCurrentMonth = objGnrlFnctn.GetMonthNameFromMothID(sCurrentMonthID)
                'sYear = objGnrlFnctn.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If txtOrderDateGIN.Text <> "" Then
                    ObjGoods.PGM_OrderDate = Date.ParseExact(Trim(txtOrderDateGIN.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    ObjGoods.PGM_OrderDate = "01/01/1900"
                End If
                ObjGoods.PGM_DocRefNo = objGen.SafeSQL(txtDocRefNo.Text)
                ObjGoods.PGM_CreatedBy = sSession.UserID
                ObjGoods.PGM_ESugamNo = objGen.SafeSQL(txtESugamNo.Text)
                ObjGoods.PGM_Supplier = ddlSupplierGIN.SelectedValue

                ObjGoods.PGM_DocRefNo = objGen.SafeSQL(txtDocRefNo.Text)

                ObjGoods.GIND_DCNO = txtDcNo.Text
                ObjGoods.PGM_ID = 0
                ObjGoods.PGM_CompID = sSession.AccessCodeID
                ObjGoods.PGM_CrBy = sSession.UserID
                '  ObjGoods.PGM_CrOn = DateTime.Today
                ObjGoods.PGM_Status = "W"
                ObjGoods.PGM_DelFlag = "X"
                ObjGoods.PGM_YearID = sSession.YearID
                ObjGoods.PGM_Operation = "C"
                ObjGoods.PGM_IPAddress = sSession.IPAddress
                ObjGoods.PGM_Gin_Number = objGen.SafeSQL(txtOrderCodeGIN.Text)
                If txtInvoiceDateGIN.Text <> "" Then
                    ObjGoods.PGM_InvoiceDate = Date.ParseExact(Trim(txtInvoiceDateGIN.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    ObjGoods.PGM_InvoiceDate = "01/01/1900"
                End If
                ObjGoods.PGM_ESugamNo = objGen.SafeSQL(txtESugamNo.Text)
                ObjGoods.PGM_OrderID = 0

                If ddlBatchNo.SelectedIndex > 0 Then
                    ObjGoods.PGM_BatchNo = ddlBatchNo.SelectedValue
                Else
                    ObjGoods.PGM_BatchNo = 0
                End If
                ObjGoods.PGM_BaseName = lstFiles.SelectedItem.Text  'BaseName ID

                If txtOrderNoGIN.Text <> "" Then
                    ObjGoods.PGM_OrderNo = txtOrderNoGIN.Text
                Else
                    ObjGoods.PGM_OrderNo = ""
                End If

                Arr = objGIN.SaveMaster(sSession.AccessCode, ObjGoods, 0)
                iMasterID = Arr(1)

                If txtReceivedQty.Text > 0 Then
                    ObjGoods.PGD_MasterID = iMasterID
                    If txtRateGIN.Text <> "" Then
                        ObjGoods.PGD_MRP = txtRateGIN.Text
                    Else
                        ObjGoods.PGD_MRP = 0
                    End If

                    If ddlUnitGIN.SelectedIndex > 0 Then
                        ObjGoods.PGD_UnitID = ddlUnitGIN.SelectedValue
                    Else
                        ObjGoods.PGD_UnitID = 0
                    End If
                    ObjGoods.PGD_HistoryID = 0

                    If chkCategoryGIN.SelectedValue > 0 Then
                        ObjGoods.PGD_DescriptionID = chkCategoryGIN.SelectedValue
                    End If

                    If ddlCommodityGIN.SelectedIndex > 0 Then
                        ObjGoods.PGD_CommodityID = ddlCommodityGIN.SelectedValue
                    End If

                    If txtOrderedQty.Text <> "" Then
                        ObjGoods.PGD_OrderQnt = txtOrderedQty.Text
                    Else
                        ObjGoods.PGD_OrderQnt = 0
                    End If
                    If txtReceivedQty.Text <> "" Then
                        ObjGoods.PGD_ReceivedQnt = txtReceivedQty.Text
                    Else
                        ObjGoods.PGD_ReceivedQnt = 0
                    End If

                    If txtAcceptedQty.Text <> "" Then
                        ObjGoods.PGD_Accepted = txtAcceptedQty.Text
                    Else
                        ObjGoods.PGD_Accepted = 0
                    End If
                    If txtRejectedQty.Text <> "" Then
                        ObjGoods.PGD_RejectedQnt = txtRejectedQty.Text
                    Else
                        ObjGoods.PGD_RejectedQnt = 0
                    End If

                    If txtExcessQty.Text <> "" Then
                        ObjGoods.PGD_Excess = txtExcessQty.Text
                        ObjGoods.PGD_Delflag = "E"
                    Else
                        ObjGoods.PGD_Excess = 0
                        ObjGoods.PGD_Delflag = "A"
                    End If

                    If txtManufactureDate.Text <> "" Then
                        ObjGoods.PGD_ManufactureDate = Date.ParseExact(Trim(txtManufactureDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        ObjGoods.PGD_ManufactureDate = "01/01/1900"
                    End If

                    If txtExpireDate.Text <> "" Then
                        ObjGoods.PGD_ExpireDate = Date.ParseExact(Trim(txtExpireDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        ObjGoods.PGD_ExpireDate = "01/01/1900"
                    End If

                    ObjGoods.PGD_PendingItem = 0
                    ObjGoods.PGD_CompID = sSession.AccessCodeID
                    ObjGoods.PGD_Status = "W"
                    ObjGoods.PGD_Operation = "C"
                    ObjGoods.PGD_IPAddress = sSession.IPAddress

                    If txtBatchNo.Text <> "" Then
                        ObjGoods.GIND_BatchNo = txtBatchNo.Text
                    Else
                        ObjGoods.GIND_BatchNo = " "
                    End If
                    ObjGoods.PGD_OrderID = 0
                    Arr = objGIN.SaveMasterDetails(sSession.AccessCode, ObjGoods)

                End If
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnSaveGIN.ImageUrl = "~/Images/Save16.png"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
                lblStatusGIN.Text = "Waiting For Approve"
                LoadExistingInwardNo()
                ddlExistingInwardNo.SelectedValue = iMasterID
                ClearGINDetails()
                dgInward.DataSource = objGIN.LoadInwardDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
                dgInward.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveGIN_Click")
        End Try
    End Sub
    Private Sub LoadExistingInwardNo()
        Try
            ddlExistingInwardNo.DataSource = objGIN.LoadExistingInwardNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingInwardNo.DataTextField = "PGM_GIN_Number"
            ddlExistingInwardNo.DataValueField = "PGM_ID"
            ddlExistingInwardNo.DataBind()
            ddlExistingInwardNo.Items.Insert(0, "Existing Inward No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCommodityGIN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodityGIN.SelectedIndexChanged
        Try
            If ddlCommodityGIN.SelectedIndex > 0 Then
                chkCategoryGIN.DataSource = objPO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodityGIN.SelectedValue)
                chkCategoryGIN.DataTextField = "Inv_Code"
                chkCategoryGIN.DataValueField = "Inv_ID"
                chkCategoryGIN.DataBind()
            Else
                loadDescitionStartGIN()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodityGIN_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCommodityPR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodityPR.SelectedIndexChanged
        Try
            If ddlCommodityPR.SelectedIndex > 0 Then
                chkCategoryPR.DataSource = objPO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodityPR.SelectedValue)
                chkCategoryPR.DataTextField = "Inv_Code"
                chkCategoryPR.DataValueField = "Inv_ID"
                chkCategoryPR.DataBind()
            Else
                loadDescitionStartPR()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodityPR_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnRefreshGIN_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefreshGIN.Click
        Try
            ClearGIN()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefreshGIN_Click")
        End Try
    End Sub
    Protected Sub dgInward_PreRender(sender As Object, e As EventArgs) Handles dgInward.PreRender
        Dim dt As New DataTable
        Try
            If dgInward.Rows.Count > 0 Then
                dgInward.UseAccessibleHeader = True
                dgInward.HeaderRow.TableSection = TableRowSection.TableHeader
                dgInward.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInward_PreRender")
        End Try
    End Sub
    Private Sub dgInward_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgInward.RowCommand
        Dim chkSelect As New CheckBox
        Dim lblID, lblComodityId, lblItemId As New Label
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
            lblComodityId = DirectCast(clickedRow.FindControl("lblComodityId"), Label)
            lblItemId = DirectCast(clickedRow.FindControl("lblItemId"), Label)

            If e.CommandName = "Edit" Then
                sStatus = objGIN.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInwardNo.SelectedValue)
                If (sStatus = "W") Then
                    imgbtnSaveGIN.ImageUrl = "~/Images/Update16.png"
                    dt = objGIN.LoadGINDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text, lblComodityId.Text, lblItemId.Text)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("PGD_CommodityID").ToString()) = False Then
                            ddlCommodityGIN.SelectedValue = dt.Rows(0)("PGD_CommodityID").ToString()
                            chkCategoryGIN.DataSource = objPO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodityGIN.SelectedValue)
                            chkCategoryGIN.DataTextField = "Inv_Code"
                            chkCategoryGIN.DataValueField = "Inv_ID"
                            chkCategoryGIN.DataBind()
                            chkCategoryGIN.SelectedValue = dt.Rows(0)("PGD_DescriptionID").ToString()
                            lblDescID.Text = dt.Rows(0)("PGD_DescriptionID").ToString()
                        Else
                            ddlCommodityGIN.SelectedIndex = 0
                        End If

                        LoadUnitGIN()
                        If IsDBNull(dt.Rows(0)("PGD_UnitID").ToString()) = False Then
                            If dt.Rows(0)("PGD_UnitID") > 0 Then
                                ddlUnitGIN.SelectedValue = dt.Rows(0)("PGD_UnitID")
                            Else
                                ddlUnitGIN.SelectedIndex = 0
                            End If
                        Else
                            ddlUnitGIN.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(0)("PGD_MRP").ToString()) = False Then
                            txtRateGIN.Text = dt.Rows(0)("PGD_MRP").ToString()
                        Else
                            txtRateGIN.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("PGD_OrderQnt").ToString()) = False Then
                            txtOrderedQty.Text = dt.Rows(0)("PGD_OrderQnt").ToString()
                        Else
                            txtOrderedQty.Text = "0"
                        End If

                        If IsDBNull(dt.Rows(0)("PGD_ReceivedQnt").ToString()) = False Then
                            txtReceivedQty.Text = dt.Rows(0)("PGD_ReceivedQnt").ToString()
                        Else
                            txtReceivedQty.Text = 0
                        End If

                        If IsDBNull(dt.Rows(0)("PGD_RejectedQnt").ToString()) = False Then
                            txtRejectedQty.Text = dt.Rows(0)("PGD_RejectedQnt").ToString()
                        Else
                            txtRejectedQty.Text = 0
                        End If

                        If IsDBNull(dt.Rows(0)("PGD_Accepted").ToString()) = False Then
                            txtAcceptedQty.Text = dt.Rows(0)("PGD_Accepted").ToString()
                        Else
                            txtAcceptedQty.Text = 0
                        End If

                        If IsDBNull(dt.Rows(0)("PGD_Excess").ToString()) = False Then
                            txtExcessQty.Text = dt.Rows(0)("PGD_Excess").ToString()
                        Else
                            txtExcessQty.Text = 0
                        End If

                        If IsDBNull(dt.Rows(0)("PGD_BatchNumber").ToString()) = False Then
                            txtBatchNo.Text = dt.Rows(0)("PGD_BatchNumber").ToString()
                        Else
                            txtBatchNo.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("PGD_ManufactureDate").ToString()) = False Then
                            txtManufactureDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("PGD_ManufactureDate"), "D")
                        Else
                            txtManufactureDate.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("PGD_ExpireDate").ToString()) = False Then
                            txtExpireDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("PGD_ExpireDate"), "D")
                        Else
                            txtExpireDate.Text = ""
                        End If

                    End If
                ElseIf sStatus = "A" Then
                    lblError.Text = "This is Already Approved,You can not Edit."
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInward_RowCommand")
        End Try
    End Sub
    Private Sub dgInward_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgInward.RowEditing

    End Sub
    Private Sub imgbtnApproveGIN_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApproveGIN.Click
        Dim sStatus As String = ""
        Try
            sStatus = objGIN.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInwardNo.SelectedValue)
            If sStatus = "A" Then
                lblError.Text = "It Is Already Approved."
                Exit Sub
            End If
            If ddlExistingInwardNo.SelectedIndex > 0 Then
                objGIN.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlExistingInwardNo.SelectedValue)
                lblError.Text = "Sucessfully Approved"
                lblStatusGIN.Text = "Approved"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApproveGIN_Click")
        End Try
    End Sub
    Private Sub imgbtnSavePR_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSavePR.Click
        Dim iMasterID As Integer = 0
        Dim dReturnDate As Date
        Dim dOrderDate As Date
        Dim dInvoiceDate As Date
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim dQuantity As Integer : Dim dHistoryID As Integer : Dim dInvoiceID As Integer
        Dim dtMaster As New DataTable : Dim dtDetails As New DataTable
        Dim sStatusPR As String = ""
        Dim Arr() As String
        Try
            lblError.Text = ""

            'dDate = Date.ParseExact(Trim(txtReturnDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'dSDate = Date.ParseExact(Trim(txtInvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'm = DateDiff(DateInterval.Day, dDate, dSDate)
            'If m > 0 Then
            '    lblError.Text = "Return Date (" & txtReturnDate.Text & ") should be Greater than or Equal to Invoice Date(" & txtInvoiceDate.Text & ")."
            '    txtReturnDate.Focus()
            '    Exit Sub
            'End If

            If ddlExistingReturnNor.SelectedIndex > 0 Then
                sStatusPR = objGreturn.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingReturnNor.SelectedValue, 0, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
                If sStatusPR = "A" Then
                    lblError.Text = "Already Approved."
                    Exit Sub
                End If
            End If

            dReturnDate = Date.ParseExact(Trim(txtReturnDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dOrderDate = Date.ParseExact(Trim(txtOrderDatePR.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dInvoiceDate = Date.ParseExact(Trim(txtInvoiceDatePR.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            objGreturn.sGRM_GRID = 0
            objGreturn.sGRM_PRID = 1

            objGreturn.sPRM_ReturnNo = txtOrderCodePR.Text
            objGreturn.sPRM_ReturnRefNo = txtReturnRefNo.Text
            objGreturn.sPRM_OrderID = 0
            objGreturn.sPRM_GINInvID = 0
            objGreturn.sPRM_GINInvNo = ""
            objGreturn.iPRM_Supplier = ddlSupplierPR.SelectedValue
            objGreturn.iPRD_Commodity = ddlCommodityPR.SelectedValue
            objGreturn.iPRM_CreatedBy = sSession.UserID
            objGreturn.iPRM_YearID = sSession.YearID
            objGreturn.iPRD_DescriptionID = chkCategoryPR.SelectedValue
            objGreturn.sPRD_IPAddress = sSession.IPAddress
            objGreturn.iPRD_HistoryID = 0

            If ddlUnitPR.SelectedIndex > 0 Then
                objGreturn.sGRD_UnitID = ddlUnitPR.SelectedValue
            Else
                objGreturn.sGRD_UnitID = 0
            End If
            objGreturn.sGRD_Rate = txtRatePR.Text
            objGreturn.sGRD_Amount = hfAmount.Value
            objGreturn.sGRD_Total = txtRateAmountPR.Text
            objGreturn.sGRD_Quantity = txtReturnQuantity.Text
            If txtDiscountPR.Text <> "" Then
                objGreturn.sGRD_Discount = txtDiscountPR.Text
            Else
                objGreturn.sGRD_Discount = 0
            End If
            If txtDiscountAmountPR.Text <> "" Then
                objGreturn.sGRD_DiscountAmount = txtDiscountAmountPR.Text
            Else
                objGreturn.sGRD_DiscountAmount = 0
            End If

            objGreturn.sGRD_TotalAmount = txtTotalAmountPR.Text
            objGreturn.sGRD_ChargesPerItem = 0
            objGreturn.sGRD_GST_ID = 0
            If txtGSTRatePR.Text <> "" Then
                objGreturn.sGRD_GSTRate = txtGSTRatePR.Text
            Else
                objGreturn.sGRD_GSTRate = 0
            End If
            If txtGSTAmountPR.Text <> "" Then
                objGreturn.sGRD_GSTAmount = txtGSTAmountPR.Text
            Else
                objGreturn.sGRD_GSTAmount = 0
            End If
            objGreturn.sGRM_InvoiceSatus = ""
            objGreturn.sGRM_State = ""

            objGreturn.sGRD_SGST = 0
            objGreturn.sGRD_CGST = 0
            objGreturn.sGRD_SGSTAmount = 0
            objGreturn.sGRD_CGSTAmount = 0
            objGreturn.sGRD_IGST = 0
            objGreturn.sGRD_IGSTAmount = 0

            If (ddlreturntype.SelectedIndex > 0) Then
                objGreturn.iPRM_TypeOfReturn = ddlreturntype.SelectedIndex
            Else
                objGreturn.iPRM_TypeOfReturn = 0
            End If
            If txtRemarksPR.Text = "" Then
                objGreturn.sPRM_Remarks = ""
            Else
                objGreturn.sPRM_Remarks = txtRemarksPR.Text
            End If
            objGreturn.sPRM_Status = "W"

            If ddlBatchNo.SelectedIndex > 0 Then
                objGreturn.PRM_BatchNo = ddlBatchNo.SelectedValue
            Else
                objGreturn.PRM_BatchNo = 0
            End If
            objGreturn.PRM_BaseName = lstFiles.SelectedItem.Text  'BaseName ID

            If txtOrderNoPR.Text <> "" Then
                objGreturn.sPRM_OrderNo = txtOrderNoPR.Text
            Else
                objGreturn.sPRM_OrderNo = ""
            End If

            If txtInvoiceNoPR.Text <> "" Then
                objGreturn.sPRM_InvoiceNo = txtInvoiceNoPR.Text
            Else
                objGreturn.sPRM_InvoiceNo = ""
            End If

            iMasterID = objGreturn.SaveGoodsReturn(sSession.AccessCode, sSession.AccessCodeID, dReturnDate, dOrderDate, dInvoiceDate, objGreturn)

            SaveChargesPR(iMasterID)

            Dim iReturnID As Integer
            iReturnID = objGreturn.SaveGoodsReturnDetails(sSession.AccessCode, sSession.AccessCodeID, objGreturn, iMasterID)
            If iReturnID > 0 Then
                lblError.Text = "Successfully Updated"
                imgbtnSavePR.ImageUrl = "~/Images/Add24.png"
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            ElseIf iReturnID = 0 Then
                lblError.Text = "Successfully Saved"
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            End If

            LoadExistingGoodsReturn()
            ddlExistingReturnNor.SelectedValue = iMasterID

            dgPurchaseReturn.DataSource = objGreturn.LoadGoodsreturnDetails(sSession.AccessCode, sSession.AccessCodeID, iMasterID)
            dgPurchaseReturn.DataBind()

            clearPurchaseReturnDetails()
            lblStatusPR.Text = "Waiting For Approval"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSavePR_Click")
        End Try
    End Sub
    Private Sub imgbtnAddChargePR_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddChargePR.Click
        Dim dt, dtTable As New DataTable
        Dim dchargeTotal As Double
        Try
            If ddlChargeTypePR.SelectedIndex > 0 Then
                If txtShippingRatePR.Text <> "" Then
                    dt = AddChargesPR()
                    dtTable = objGreturn.RemoveChargeDublicate(dt)
                    GvChargePR.DataSource = dtTable
                    GvChargePR.DataBind()

                    If GvChargePR.Items.Count > 0 Then
                        For i = 0 To GvChargePR.Items.Count - 1
                            dchargeTotal = dchargeTotal + GvChargePR.Items(i).Cells(2).Text
                        Next
                    End If

                Else
                    lblError.Text = "Enter Amount charged."
                End If
            Else
                lblError.Text = "Select Charge Type."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddChargePR_Click")
        End Try
    End Sub
    Public Function AddChargesPR() As DataTable
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
            dr("ChargeID") = ddlChargeTypePR.SelectedValue
            dr("ChargeType") = Trim(ddlChargeTypePR.SelectedItem.Text)
            dr("ChargeAmount") = Trim(txtShippingRatePR.Text)
            dt1.Rows.Add(dr)

            Session("ChargesMaster") = dt1
            Return dt1
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AddChargesPR")
        End Try
    End Function
    Private Sub GvChargePR_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles GvChargePR.ItemCommand
        Dim dt As New DataTable
        Dim dchargeTotal As Double
        Try
            If e.CommandName = "Delete" Then
                dt = Session("ChargesMaster")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                Session("ChargesMaster") = dt
            End If
            GvChargePR.DataSource = dt
            GvChargePR.DataBind()

            If GvChargePR.Items.Count > 0 Then
                For i = 0 To GvChargePR.Items.Count - 1
                    dchargeTotal = dchargeTotal + GvChargePR.Items(i).Cells(2).Text
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvChargePR_ItemCommand")
        End Try
    End Sub
    Private Sub LoadChargeTypePR()
        Try
            ddlChargeTypePR.DataSource = objGreturn.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeTypePR.DataTextField = "Mas_desc"
            ddlChargeTypePR.DataValueField = "Mas_id"
            ddlChargeTypePR.DataBind()
            ddlChargeTypePR.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveChargesPR(ByVal iMasterID As Integer)
        Dim Arr() As String
        Try
            'Deleting charges Everytime & Saving'
            objGreturn.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvChargePR.Items.Count > 0 Then
                For i = 0 To GvChargePR.Items.Count - 1

                    objGreturn.C_POrderID = 0
                    objGreturn.C_PGinID = 0
                    objGreturn.C_PInvoiceDocRef = 0
                    objGreturn.C_OrderType = ""
                    objGreturn.C_ChargeID = GvChargePR.Items(i).Cells(0).Text
                    objGreturn.C_ChargeType = GvChargePR.Items(i).Cells(1).Text
                    objGreturn.C_ChargeAmount = GvChargePR.Items(i).Cells(2).Text
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveChargesPR")
        End Try
    End Sub
    Protected Sub dgPurchaseReturn_PreRender(sender As Object, e As EventArgs) Handles dgPurchaseReturn.PreRender
        Dim dt As New DataTable
        Try
            If dgPurchaseReturn.Rows.Count > 0 Then
                dgPurchaseReturn.UseAccessibleHeader = True
                dgPurchaseReturn.HeaderRow.TableSection = TableRowSection.TableHeader
                dgPurchaseReturn.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchaseReturn_PreRender")
        End Try
    End Sub
    Private Sub dgPurchaseReturn_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPurchaseReturn.RowCommand
        Dim dtMaster As New DataTable
        Dim lblID, lblCommodityID, lblDid As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
            lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
            lblDid = DirectCast(clickedRow.FindControl("lblItemID"), Label)

            If e.CommandName.Equals("EditRow") Then
                LoadAllReturnDetails(lblID.Text, lblCommodityID.Text, lblDid.Text)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchaseReturn_RowCommand")
        End Try
    End Sub
    Public Sub LoadAllReturnDetails(ByVal iID As Integer, ByVal iCommodityID As Integer, ByVal GRDescriptionID As Integer)
        Dim dtMaster As New DataTable
        Try
            ddlreturntype.SelectedIndex = 0 : txtRemarksPR.Text = ""
            chkCategoryPR.SelectedValue = GRDescriptionID
            lblDescID.Text = GRDescriptionID
            ddlCommodityPR.SelectedValue = iCommodityID
            LoadUnitPR()
            dtMaster = objGreturn.LoadGRDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingReturnNor.SelectedValue, iID)
            If dtMaster.Rows.Count > 0 Then
                For i = 0 To dtMaster.Rows.Count - 1
                    If IsDBNull(dtMaster.Rows(i)("GRD_UnitID")) = False Then
                        ddlUnitPR.SelectedValue = dtMaster.Rows(i)("GRD_UnitID")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_RateAmount")) = False Then
                        txtRatePR.Text = Format(dtMaster.Rows(i)("GRD_RateAmount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Discount")) = False Then
                        txtDiscountPR.Text = Format(dtMaster.Rows(i)("GRD_Discount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_GSTRate")) = False Then
                        txtGSTRatePR.Text = Format(dtMaster.Rows(i)("GRD_GSTRate"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Quantity")) = False Then
                        txtReturnQuantity.Text = dtMaster.Rows(i)("GRD_Quantity")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Reason")) = False Then
                        ddlreturntype.SelectedIndex = dtMaster.Rows(i)("GRD_Reason")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Remarks")) = False Then
                        txtRemarksPR.Text = dtMaster.Rows(i)("GRD_Remarks")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Total")) = False Then
                        txtRateAmountPR.Text = Format(dtMaster.Rows(i)("GRD_Total"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_DiscountAmount")) = False Then
                        txtDiscountAmountPR.Text = Format(dtMaster.Rows(i)("GRD_DiscountAmount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_GSTAmount")) = False Then
                        txtGSTAmountPR.Text = Format(dtMaster.Rows(i)("GRD_GSTAmount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_TotalAmount")) = False Then
                        txtTotalAmountPR.Text = Format(dtMaster.Rows(i)("GRD_TotalAmount"), "0.00")
                    End If

                Next
            End If

            dtMaster = objGreturn.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingReturnNor.SelectedValue, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllReturnDetails")
        End Try
    End Sub
    Private Sub imgbtnRefreshPR_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefreshPR.Click
        Try
            clearPurchaseReturn()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefreshPR_Click")
        End Try
    End Sub
    Private Sub imgbtnApprovePR_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprovePR.Click
        Dim status As String = ""
        Try
            If ddlExistingReturnNor.SelectedIndex > 0 Then
                status = objGreturn.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingReturnNor.SelectedValue, 0, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
                If status = "A" Then
                    lblError.Text = "Already Approved"
                    Exit Sub
                ElseIf status = "W" Then
                    objGreturn.SaveApproveGR(sSession.AccessCode, sSession.AccessCodeID, ddlExistingReturnNor.SelectedValue, 0, 0, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)

                    lblError.Text = "Successfully Approved"
                    lblStatusPR.Text = "Activated"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprovePR_Click")
        End Try
    End Sub
    Public Sub clearSalesReturn()
        Try
            ddlExistSalesReturn.SelectedIndex = 0 : ddlCustomerSR.SelectedIndex = 0 : ddlChargeTypeSR.SelectedIndex = 0 : txtShippingRateSR.Text = ""
            lblStatusSR.Text = "" : lblError.Text = "" : txtReturnNoSR.Text = "" : txtReturnDateSR.Text = "" : txtGoodsReturnRefNo.Text = "" : ddlCommoditySR.SelectedIndex = 0 : ddlReturn.SelectedIndex = 0

            ddlUnitSR.Items.Clear() : txtRateSR.Text = "" : txtReturnQuantitySR.Text = "" : txtAmountSR.Text = "" : ddlDiscountSR.SelectedIndex = 0 : txtDiscountAmountSR.Text = ""
            txtGSTRateSR.Text = "" : txtGSTAmountSR.Text = "" : txtTotalAmountSR.Text = "" : txtOrderNoSR.Text = "" : txtDispatchNoSR.Text = "" : txtInvoiceNoSR.Text = "" : txtInvoiceDateSR.Text = "" : txtShipTo.Text = ""
            dgSRItemDetails.DataSource = Nothing
            dgSRItemDetails.DataBind()

            GVChargeSR.DataSource = Nothing
            GVChargeSR.DataBind()
            GeneratePRNo()
            imgbtnSavePR.ImageUrl = "~/Images/Save16.png"

            For i = 0 To lstBoxDescriptionSR.Items.Count - 1
                lstBoxDescriptionSR.Items(i).Selected = False
            Next
            txtReturnNoSR.Text = objSR.GenerateReturnNo(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub clearSalesReturnDetails()
        Try
            For i = 0 To lstBoxDescriptionSR.Items.Count - 1
                lstBoxDescriptionSR.Items(i).Selected = False
            Next
            lblStatusSR.Text = "" : lblError.Text = "" : ddlCommoditySR.SelectedIndex = 0 : ddlReturn.SelectedIndex = 0

            ddlUnitSR.Items.Clear() : txtRateSR.Text = "" : txtReturnQuantitySR.Text = "" : txtAmountSR.Text = "" : ddlDiscountSR.SelectedIndex = 0 : txtDiscountAmountSR.Text = ""
            txtGSTRateSR.Text = "" : txtGSTAmountSR.Text = "" : txtTotalAmountSR.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingTransactionRPJ(ByVal iMasterId As Integer)
        Try
            If ddlTrType.SelectedItem.Text = "Receipt" Then
                ddlExistingTrnRPJ.DataSource = objRecp.LoadExistingTrnNo(sSession.AccessCode, sSession.AccessCodeID, iMasterId)
                ddlExistingTrnRPJ.DataTextField = "Acc_RM_TransactionNo"
                ddlExistingTrnRPJ.DataValueField = "Acc_RM_ID"
                ddlExistingTrnRPJ.DataBind()
                ddlExistingTrnRPJ.Items.Insert(0, "Select ExistingTrnNo")
            ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then
                ddlExistingTrnRPJ.DataSource = objPC.LoadExistingTrnNo(sSession.AccessCode, sSession.AccessCodeID, iMasterId)
                ddlExistingTrnRPJ.DataTextField = "Acc_PCM_TransactionNo"
                ddlExistingTrnRPJ.DataValueField = "Acc_PCM_ID"
                ddlExistingTrnRPJ.DataBind()
                ddlExistingTrnRPJ.Items.Insert(0, "Select ExistingTrnNo")
            ElseIf ddlTrType.SelectedItem.Text = "Journal Entry" Then
                ddlExistingTrnRPJ.DataSource = objJE.LoadExistingTrnNo(sSession.AccessCode, sSession.AccessCodeID, iMasterId)
                ddlExistingTrnRPJ.DataTextField = "Acc_JE_TransactionNo"
                ddlExistingTrnRPJ.DataValueField = "Acc_JE_ID"
                ddlExistingTrnRPJ.DataBind()
                ddlExistingTrnRPJ.Items.Insert(0, "Select ExistingTrnNo")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingTransactionRPJ")
        End Try
    End Sub

    Private Sub dgPettyCashDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPettyCashDetails.ItemDataBound
        Dim imgbtnDeletePetty As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDeletePetty = CType(e.Item.FindControl("imgbtnDeletePetty"), ImageButton)
                imgbtnDeletePetty.ImageUrl = "~/Images/Trash24.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPettyCashDetails_ItemDataBound")
        End Try
    End Sub
    Private Sub dgPettyCashDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPettyCashDetails.ItemCommand
        Dim dt As New DataTable
        Dim lblId As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "DeletePetty" Then

                If lblStatus.Text = "Activated" Then
                    lblError.Text = "This Payment has been Approved, you can not delete transactions."
                    Exit Sub
                End If

                dt = Session("DataCapture")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                If dt.Rows.Count > 0 Then
                    Session("DataCapture") = dt
                Else
                    Session("DataCapture") = Nothing
                End If
            End If
            dgPettyCashDetails.DataSource = dt
            dgPettyCashDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPettyCashDetails_ItemCommand")
        End Try
    End Sub
    Private Sub imgbtnApproveRPJ_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApproveRPJ.Click
        Dim sId As String
        Try
            lblError.Text = ""
            lblRPJ.Text = ""
            If ddlExistingTrnRPJ.SelectedValue > 0 Then
                If ddlTrType.SelectedItem.Text = "Receipt" Then
                    If dgPaymentDetails.Items.Count > 0 Then
                        sId = objRecp.getStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue)
                        If sId = "A" Then
                            lblRPJ.Text = "Already Approved."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                            Exit Sub
                        Else
                            objRecp.UpdateReceiptStatus1(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                            objRecp.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue, sSession.YearID, sSession.UserID, sSession.IPAddress)
                            lblRPJ.Text = "Successfully Approved."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                        End If

                    Else
                        lblRPJ.Text = "No data"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data ','', 'info');", True)
                        Exit Sub
                    End If
                ElseIf ddlTrType.SelectedItem.Text = "Journal Entry" Then
                    If dgPaymentDetails.Items.Count > 0 Then
                        sId = objJE.getStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue)
                        If sId = "A" Then
                            lblRPJ.Text = "Already Approved."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                            Exit Sub
                        Else
                            objJE.UpdateJEMasterStatus1(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                            objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue, sSession.YearID, sSession.UserID, sSession.IPAddress)
                            lblRPJ.Text = "Successfully Approved."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                        End If
                    Else
                        lblRPJ.Text = "No data"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data ','', 'info');", True)
                        Exit Sub
                    End If
                ElseIf ddlTrType.SelectedItem.Text = "Petty Cash" Then
                    If dgPettyCashDetails.Items.Count > 0 Then
                        sId = objPC.getStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue)
                        If sId = "A" Then
                            lblRPJ.Text = "Already Approved."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                            Exit Sub
                        Else
                            objPC.UpdatePaymentMasterStatus1(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                            objPC.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTrnRPJ.SelectedValue, sSession.YearID, sSession.UserID, sSession.IPAddress)
                            lblRPJ.Text = "Successfully Approved."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                        End If
                    Else
                        lblRPJ.Text = "No data"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data ','', 'info');", True)
                        Exit Sub
                    End If
                End If
            Else
                lblRPJ.Text = "You can't Approve,first Save then Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('You can't Approve,first Save then Approve','', 'info');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApproveRPJ_Click")
        End Try
    End Sub
    Public Sub LoadCashSalesFunctions()
        Dim sDate As String = "" : Dim taxcategory As String = ""
        Try
            'Sales Validation'
            RFVddlPatryCS.InitialValue = "Select Customer"
            RFVddlPaymentTypeCS.InitialValue = "Select Payment Type"
            RFVddlUnitOfMeassurementCS.InitialValue = "Select Unit Of Meassurement"
            'Sales validation'

            LoadExistingCashsaleOrderNo()
            LoadExistingDispatchNo()
            LoadExistingSalesInvoiceNo()

            GeneratCasheOrderCode()

            LoadCashCommoditySales()
            LoadCashPaymentTypeSales()
            LoadCashModeOfCommunication()
            LoadCashPartySales()
            LoadCashMethodOfShipingSales()
            LoadCashSalesMan()
            BindCashDescription(0)
            BindCashCompanyType()
            'LoadDiscount()
            LoadCashCategory()
            LoadDiscount()

            dgExistingProFormaSalesOrder.DataSource = Nothing
            dgExistingProFormaSalesOrder.DataBind()
            dgExistingProFormaSalesOrder.Visible = False

            dt = objCS.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("CUST_COMM_Address")) = True Or IsDBNull(dt.Rows(0)("CUST_ProvisionalNo")) = True Or IsDBNull(dt.Rows(0)("CUST_INDTypeID")) = True Or IsDBNull(dt.Rows(0)("CUST_TAXPayableCategory")) = True Then
                    lblError.Text = "Fill the details in Company Master"
                    Exit Sub
                End If
                txtCompanyAddressCS.Text = dt.Rows(0)("CUST_COMM_Address")
                txtCompanyGSTNRegNoCS.Text = dt.Rows(0)("CUST_ProvisionalNo")
                ddlCompanyTypeCS.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                BindCashSalesGSTNCategory(ddlCompanyTypeCS.SelectedItem.Text)
                ddlGSTCategoryCS.SelectedValue = dt.Rows(0)("CUST_TAXPayableCategory")

                txtDeliveryFromAddressCS.Text = txtCompanyAddressCS.Text
                txtDeliveryFromGSTNRegNoCS.Text = txtCompanyGSTNRegNoCS.Text

                If ddlGSTCategoryCS.SelectedIndex > 0 Then
                    taxcategory = objCS.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategoryCS.SelectedValue)
                    If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryFromGSTNRegNoCS.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNoCS.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCashSalesFunctions()")
        End Try
    End Sub
    Private Sub LoadDiscount()
        Dim dt As New DataTable
        Try
            dt = objCS.LoadDiscounts(sSession.AccessCode, sSession.AccessCodeID)
            ddlDiscountcs.DataSource = dt
            ddlDiscountcs.DataTextField = "Mas_Desc"
            ddlDiscountcs.DataValueField = "Mas_Id"
            ddlDiscountcs.DataBind()
            ddlDiscountcs.Items.Insert(0, "Select Discount")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDiscountSR()
        Dim dt As New DataTable
        Try
            dt = objSR.LoadDiscounts(sSession.AccessCode, sSession.AccessCodeID)
            ddlDiscountSR.DataSource = dt
            ddlDiscountSR.DataTextField = "Mas_Desc"
            ddlDiscountSR.DataValueField = "Mas_Id"
            ddlDiscountSR.DataBind()
            ddlDiscountSR.Items.Insert(0, "Select Discount")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindCashSalesGSTNCategory(ByVal sCompanyType As String)
        Dim dt As New DataTable
        Try
            dt = objCS.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategoryCS.DataSource = dt
            ddlGSTCategoryCS.DataTextField = "GC_GSTCategory"
            ddlGSTCategoryCS.DataValueField = "GC_Id"
            ddlGSTCategoryCS.DataBind()
            ddlGSTCategoryCS.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCashCommoditySales()
        Try
            ddlCommodityCS.DataSource = objCS.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodityCS.DataTextField = "Inv_Description"
            ddlCommodityCS.DataValueField = "Inv_ID"
            ddlCommodityCS.DataBind()
            ddlCommodityCS.Items.Insert(0, "Select Brand")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCashPaymentTypeSales()
        Try
            ddlPaymentTypeCS.DataSource = objCS.BindPaymentType(sSession.AccessCode, sSession.AccessCodeID)
            ddlPaymentTypeCS.DataTextField = "Mas_Desc"
            ddlPaymentTypeCS.DataValueField = "Mas_ID"
            ddlPaymentTypeCS.DataBind()
            ddlPaymentTypeCS.Items.Insert(0, "Select Payment Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindCashDescription(ByVal iCommodityID As Integer)
        Try
            lstBoxDescriptionCS.DataSource = objCS.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID)
            lstBoxDescriptionCS.DataTextField = "INV_Code"
            lstBoxDescriptionCS.DataValueField = "INV_ID"
            lstBoxDescriptionCS.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCashModeOfCommunication()
        Try
            ddlModeOfCommunicationCS.DataSource = objCS.BindModeOfCommunication(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfCommunicationCS.DataTextField = "Mas_Desc"
            ddlModeOfCommunicationCS.DataValueField = "Mas_ID"
            ddlModeOfCommunicationCS.DataBind()
            ddlModeOfCommunicationCS.Items.Insert(0, "Select Mode Of Communication")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCashCategory()
        Try
            ddlCategoryCS.DataSource = objCS.BindCategory(sSession.AccessCode, sSession.AccessCodeID)
            ddlCategoryCS.DataTextField = "Mas_Desc"
            ddlCategoryCS.DataValueField = "Mas_ID"
            ddlCategoryCS.DataBind()
            ddlCategoryCS.Items.Insert(0, "Select Category")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCashPartySales()
        Try
            ddlPatryCS.DataSource = objCS.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlPatryCS.DataTextField = "BM_Name"
            ddlPatryCS.DataValueField = "BM_ID"
            ddlPatryCS.DataBind()
            ddlPatryCS.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCashMethodOfShipingSales()
        Try
            ddlShippingCS.DataSource = objCS.LoadMethodOfShiping(sSession.AccessCode, sSession.AccessCodeID)
            ddlShippingCS.DataTextField = "Mas_desc"
            ddlShippingCS.DataValueField = "Mas_id"
            ddlShippingCS.DataBind()
            ddlShippingCS.Items.Insert(0, "Select Mode of Shipping")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCashSalesMan()
        Try
            ddlSalesManCS.DataSource = objCS.LoadSalesMan(sSession.AccessCode, sSession.AccessCodeID)
            ddlSalesManCS.DataTextField = "username"
            ddlSalesManCS.DataValueField = "Usr_id"
            ddlSalesManCS.DataBind()
            ddlSalesManCS.Items.Insert(0, "Select Sales Person")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GeneratCasheOrderCode()
        Try
            txtOrderCodeCS.Text = objCS.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            txtDispatchNo.Text = objCS.GenerateDispatchCode(sSession.AccessCode, sSession.AccessCodeID)
            txtsaleInvoiceNo.Text = objCS.GenerateSalesInvoiceCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GeneratCasheOrderCode")
        End Try
    End Sub
    Private Sub chkboxFromCS_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxFromCS.CheckedChanged
        Try
            If chkboxFromCS.Checked = True Then
                txtDeliveryFromAddressCS.Text = ""
                txtDeliveryFromGSTNRegNoCS.Text = ""
            Else
                txtDeliveryFromAddressCS.Text = txtCompanyAddressCS.Text
                txtDeliveryFromGSTNRegNoCS.Text = txtCompanyGSTNRegNoCS.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkboxFromCS_CheckedChanged")
        End Try
    End Sub
    Private Sub chkboxToCS_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxToCS.CheckedChanged
        Try
            If chkboxToCS.Checked = True Then
                txtDeleveryAddressCS.Text = ""
                txtDeliveryGSTNRegNoCS.Text = ""
            Else
                txtDeleveryAddressCS.Text = txtBillingAddressCS.Text
                txtDeliveryGSTNRegNoCS.Text = txtBillingGSTNRegNoCS.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkboxToCS_CheckedChanged")
        End Try
    End Sub
    Private Sub ddlPatryCS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPatryCS.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Dim description As String
        Try
            lblError.Text = ""
            ClearonPartySelection()

            BindCashDescription(0)
            If ddlPatryCS.SelectedIndex > 0 Then

                lstBoxDescriptionCS.Enabled = True
                dt = objCS.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlPatryCS.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        txtPartyNoCS.Text = dt.Rows(i)("BM_Code")
                        txtAddressCS.Text = dt.Rows(i)("BM_Address")
                        txtContactNoCS.Text = dt.Rows(i)("BM_MobileNo")

                        If IsDBNull(dt.Rows(i)("BM_GenCategory")) = False Then
                            If dt.Rows(i)("BM_GenCategory") > 0 Then
                                ddlCategoryCS.SelectedValue = dt.Rows(i)("BM_GenCategory")
                            Else
                                ddlCategoryCS.SelectedIndex = 0
                            End If
                        Else
                            ddlCategoryCS.SelectedIndex = 0
                        End If

                        txtBillingAddressCS.Text = dt.Rows(i)("BM_Address")
                        txtBillingGSTNRegNoCS.Text = dt.Rows(i)("BM_GSTNRegNo")

                        txtDeleveryAddressCS.Text = dt.Rows(i)("BM_Address")
                        txtDeliveryGSTNRegNoCS.Text = dt.Rows(i)("BM_GSTNRegNo")

                        description = objCS.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("BM_GSTNCategory"))
                        If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                            txtDeliveryGSTNRegNoCS.Enabled = False
                        Else
                            txtDeliveryGSTNRegNoCS.Enabled = True
                        End If

                    Next
                End If

            Else
                txtPartyNoCS.Text = "" : txtAddressCS.Text = "" : txtContactNoCS.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPartyCS_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub lstBoxDescriptionCS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxDescriptionCS.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Dim sStockHistoryID As String = ""
        Dim dOrderDate As Date
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            hfAvailableQty.Value = ""
            lblError.Text = ""
            txtQuantityCashSales.Text = "" : txtAmountCS.Text = "" : txtNetAmountCS.Text = ""

            If ddlPatryCS.SelectedIndex > 0 Then
                If lstBoxDescriptionCS.SelectedIndex <> -1 Then
                    ddlCommodityCS.SelectedValue = objSO.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescriptionCS.SelectedValue)
                    If txtOrderDateCS.Text <> "" Then
                        dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDateCS.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                        'Cheque Date Comparision'
                        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        dSDate = Date.ParseExact(txtOrderDateCS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        m = DateDiff(DateInterval.Day, dDate, dSDate)
                        If m < 0 Then
                            lblError.Text = "Order Date (" & txtOrderDateCS.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                            lblValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                            txtOrderDateCS.Focus()
                            Exit Sub
                        End If

                        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        dSDate = Date.ParseExact(txtOrderDateCS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        m = DateDiff(DateInterval.Day, dDate, dSDate)
                        If m > 0 Then
                            lblError.Text = "Order Date (" & txtOrderDateCS.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                            lblValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                            txtOrderDateCS.Focus()
                            Exit Sub
                        End If
                        'Cheque Date Comparision'
                    End If
                    BindCashSalesUnitOfMeassurement(lstBoxDescriptionCS.SelectedValue)

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstBoxDescriptionCS_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindCashSalesUnitOfMeassurement(ByVal iItemID As Integer)
        Try
            ddlUnitOfMeassurementCS.DataSource = objCS.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iItemID)
            ddlUnitOfMeassurementCS.DataTextField = "Mas_Desc"
            ddlUnitOfMeassurementCS.DataValueField = "Mas_ID"
            ddlUnitOfMeassurementCS.DataBind()
            ddlUnitOfMeassurementCS.Items.Insert(0, "Select Unit Of Meassurement")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearCDI()
        Try
            ClearCash() : ClearDispatch() : ClearInvoice() : ddlExistingCashSaleNo.SelectedIndex = 0 : ddlExistingDispatchNo.SelectedIndex = 0 : ddlExistingSalesInvoiceNo.SelectedIndex = 0
            lblError.Text = "" : lblStatusCash.Text = "" : lblStatusDispatch.Text = "" : lblStatusInvoice.Text = ""
            ddlPatryCS.SelectedIndex = 0 : txtPartyNoCS.Text = "" : txtAddressCS.Text = "" : txtContactNoCS.Text = "" : ddlCategoryCS.SelectedIndex = 0
            ddlShippingCS.SelectedIndex = 0 : ddlPaymentTypeCS.SelectedIndex = 0 : ddlSalesManCS.SelectedIndex = 0 : txtRemarksCS.Text = ""
            txtDeleveryAddressCS.Text = "" : txtBillingAddressCS.Text = ""
            txtDeliveryGSTNRegNoCS.Text = "" : txtBillingGSTNRegNoCS.Text = ""

            GeneratCasheOrderCode()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearCDIDetails()
        Try
            ddlCommodityCS.SelectedIndex = 0
            For i = 0 To lstBoxDescriptionCS.Items.Count - 1
                lstBoxDescriptionCS.Items(i).Selected = False
            Next
            ddlUnitOfMeassurementCS.Items.Clear() : txtQuantityCashSales.Text = "" : txtMRPCS.Text = "" : txtAmountCS.Text = "" : ddlDiscountcs.SelectedIndex = 0 : txtDiscountAmountCS.Text = ""
            txtGSTCS.Text = "" : txtGSTAmountCS.Text = "" : txtNetAmountCS.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearCash()
        Try
            txtOrderCodeCS.Text = "" : txtOrderDateCS.Text = "" : ddlModeOfCommunicationCS.SelectedIndex = 0 : txtBuyerPurOrderNoCS.Text = "" : txtBuyerOrderDateCS.Text = ""
            GeneratCasheOrderCode()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearDispatch()
        Try
            txtDispatchNo.Text = "" : txtDispatchRefNo.Text = "" : txtDispatchDate.Text = "" : txtOrderNoDispatch.Text = "" : txtOrderDateDispatch.Text = ""
            txtAllocationNoDispatch.Text = ""
            GeneratCasheOrderCode()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearInvoice()
        Try
            txtsaleInvoiceNo.Text = "" : txtSalesInvoiceDate.Text = "" : txtSalesInvoiceRefNo.Text = "" : txtDispatchNoInvoice.Text = "" : txtDispatchDateInvoice.Text = ""
            txtOrderNoInvoice.Text = "" : txtOrderDateInvoice.Text = "" : txtAllocationInvoice.Text = ""
            GeneratCasheOrderCode()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnRefreshCDI_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefreshCDI.Click
        Try
            ClearCDI()
            ClearCDIDetails()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindCashCompanyType()
        Try
            ddlCompanyTypeCS.DataSource = objCS.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyTypeCS.DataTextField = "Mas_Desc"
            ddlCompanyTypeCS.DataValueField = "Mas_Id"
            ddlCompanyTypeCS.DataBind()
            ddlCompanyTypeCS.Items.Insert(0, "Select Company Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingSalesOrderNo()
        Dim dt As New DataTable
        Try
            dt = objSO.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "")
            ddlExistingSalesNo.DataSource = dt
            ddlExistingSalesNo.DataTextField = "SPO_OrderCode"
            ddlExistingSalesNo.DataValueField = "SPO_ID"
            ddlExistingSalesNo.DataBind()
            ddlExistingSalesNo.Items.Insert(0, "Existing Order No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingCashsaleOrderNo()
        Dim dt As New DataTable
        Try
            dt = objCS.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "")
            ddlExistingCashSaleNo.DataSource = dt
            ddlExistingCashSaleNo.DataTextField = "SPO_OrderCode"
            ddlExistingCashSaleNo.DataValueField = "SPO_ID"
            ddlExistingCashSaleNo.DataBind()
            ddlExistingCashSaleNo.Items.Insert(0, "Existing Order No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingDispatchNo()
        Dim dt As New DataTable
        Try
            dt = objCS.GetDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingDispatchNo.DataSource = dt
            ddlExistingDispatchNo.DataTextField = "DM_Code"
            ddlExistingDispatchNo.DataValueField = "DM_ID"
            ddlExistingDispatchNo.DataBind()
            ddlExistingDispatchNo.Items.Insert(0, "Existing Dispatch No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingSalesInvoiceNo()
        Dim dt As New DataTable
        Try
            dt = objCS.GetSalesInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingSalesInvoiceNo.DataSource = dt
            ddlExistingSalesInvoiceNo.DataTextField = "SDM_Code"
            ddlExistingSalesInvoiceNo.DataValueField = "SDM_ID"
            ddlExistingSalesInvoiceNo.DataBind()
            ddlExistingSalesInvoiceNo.Items.Insert(0, "Existing Invoice No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnAddCDI_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCDI.Click
        Try
            If UCase(ddlTrType.SelectedItem.Text) = UCase("Cash Sales") Then
                If txtOrderDateCS.Text = "" Then
                    lblError.Text = "Enter Order Date"
                    Exit Sub
                End If
                SaveCash()

            ElseIf UCase(ddlTrType.SelectedItem.Text) = UCase("Sales Dispatch") Then
                If txtDispatchDate.Text = "" Then
                    lblError.Text = "Enter Dispatch Date"
                    Exit Sub
                End If
                If txtOrderNoDispatch.Text = "" Then
                    lblError.Text = "Enter Order No"
                    Exit Sub
                End If
                If txtOrderDateDispatch.Text = "" Then
                    lblError.Text = "Enter Order Date"
                    Exit Sub
                End If
                SaveDispatch()

            ElseIf UCase(ddlTrType.SelectedItem.Text) = UCase("Sales Invoice") Then
                If txtSalesInvoiceDate.Text = "" Then
                    lblError.Text = "Enter Invoice Date"
                    Exit Sub
                End If
                If txtOrderNoInvoice.Text = "" Then
                    lblError.Text = "Enete Order No"
                    Exit Sub
                End If
                If txtOrderDateInvoice.Text = "" Then
                    lblError.Text = "Enter Order Date"
                    Exit Sub
                End If
                If txtDispatchNoInvoice.Text = "" Then
                    lblError.Text = "Enter Dispatch No"
                    Exit Sub
                End If
                If txtDispatchDateInvoice.Text = "" Then
                    lblError.Text = "Enter Dispatch date"
                    Exit Sub
                End If
                SaveInvoice()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddCDI_Click")
        End Try
    End Sub
    Public Sub SaveCash()
        Dim objOral As New ClsCashSales
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim Arr() As String : Dim iMasterID As Integer
        Dim dOrderDate As Date
        Try
            objOral.SPO_OrderCode = objGen.SafeSQL(Trim(txtOrderCodeCS.Text))
            objOral.SPO_OrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDateCS.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objOral.SPO_PartyCode = objGen.SafeSQL(Trim(txtPartyNoCS.Text))
            objOral.SPO_PartyName = objGen.SafeSQL(Trim(ddlPatryCS.SelectedValue))
            objOral.SPO_Address = objGen.SafeSQL(Trim(txtAddressCS.Text))
            objOral.SPO_ContantNo = objGen.SafeSQL(Trim(txtContactNoCS.Text))

            If ddlShippingCS.SelectedIndex > 0 Then
                objOral.SPO_ModeOfDispatch = objGen.SafeSQL(Trim(ddlShippingCS.SelectedValue))
            Else
                objOral.SPO_ModeOfDispatch = 0
            End If
            If txtShippingDateCS.Text <> "" Then
                objOral.SPO_ShippingDate = Date.ParseExact(objGen.SafeSQL(Trim(txtShippingDateCS.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtShippingDateCS.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(txtOrderDateCS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtShippingDateCS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Shipping Date (" & txtShippingDateCS.Text & ") should be Greater than or equal to Order Date(" & txtOrderDateCS.Text & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtShippingDateCS.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            objOral.SPO_PaymentType = objGen.SafeSQL(Trim(ddlPaymentTypeCS.SelectedValue))

            If ddlModeOfCommunicationCS.SelectedIndex > 0 Then
                objOral.SPO_ModeOfCommunication = objGen.SafeSQL(Trim(ddlModeOfCommunicationCS.SelectedValue))
            Else
                objOral.SPO_ModeOfCommunication = 0
            End If
            If txtInputByCS.Text <> "" Then
                objOral.SPO_InputBy = objGen.SafeSQL(Trim(txtInputByCS.Text))
            Else
                objOral.SPO_InputBy = ""
            End If

            If ddlShippingCharges.SelectedIndex > 0 Then
                objOral.SPO_ShippingCharge = objGen.SafeSQL(Trim(ddlShippingCharges.SelectedValue))
            Else
                objOral.SPO_ShippingCharge = 0
            End If

            objOral.SPO_CreatedBy = sSession.UserID
            objOral.SPO_CreatedOn = DateTime.Today
            objOral.SPO_Status = "A"
            objOral.SPO_Operation = "C"
            objOral.SPO_IPAddress = sSession.IPAddress

            objOral.SPO_OrderType = "O"
            objOral.SPO_DispatchFlag = 0

            If ddlSalesManCS.SelectedIndex > 0 Then
                objOral.SPO_SalesManID = objGen.SafeSQL(Trim(ddlSalesManCS.SelectedValue))
            Else
                objOral.SPO_SalesManID = 0
            End If

            If txtBuyerPurOrderNoCS.Text <> "" Then
                objOral.SPO_BuyerOrderNo = objGen.SafeSQL(Trim(txtBuyerPurOrderNoCS.Text))
            Else
                objOral.SPO_BuyerOrderNo = "Oral"
            End If

            If ddlCategoryCS.SelectedIndex > 0 Then
                objOral.SPO_Category = objGen.SafeSQL(Trim(ddlCategoryCS.SelectedValue))
            Else
                objOral.SPO_Category = 0
            End If

            If txtRemarksCS.Text <> "" Then
                objOral.SPO_Remarks = objGen.SafeSQL(Trim(txtRemarksCS.Text))
            Else
                objOral.SPO_Remarks = ""
            End If

            If txtBuyerOrderDateCS.Text <> "" Then
                objOral.SPO_BuyerOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtBuyerOrderDateCS.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            objOral.SPO_SalesType = 1
            objOral.SPO_OtherType = 0

            objOral.SPO_ChequeNo = ""
            objOral.SPO_ChequeDate = "01/01/1900"
            objOral.SPO_IFSCCode = ""
            objOral.SPO_BankName = 0
            objOral.SPO_Branch = ""

            objOral.SPO_GoThroughDispatch = 0

            objOral.SPO_DispatchRefNo = ""
            objOral.SPO_ESugamNo = ""

            objOral.SPO_DispatchDate = "01/01/1900"

            objOral.SPO_ZoneID = ddlAccZone.SelectedValue
            objOral.SPO_RegionID = ddlAccRgn.SelectedValue
            objOral.SPO_AreaID = ddlAccArea.SelectedValue
            'objOral.SPO_BranchID = ddlAccBrnch.SelectedValue
            If ddlAccBrnch.SelectedIndex > 0 Then
                objOral.SPO_BranchID = ddlAccBrnch.SelectedValue
            Else
                objOral.SPO_BranchID = 0
            End If

            objOral.SPO_TrType = 10 'Cash Sales

            If txtCompanyAddressCS.Text <> "" Then
                objOral.SPO_CompanyAddress = txtCompanyAddressCS.Text
            Else
                objOral.SPO_CompanyAddress = ""
            End If

            If txtBillingAddressCS.Text <> "" Then
                objOral.SPO_BillingAddress = txtBillingAddressCS.Text
            Else
                objOral.SPO_BillingAddress = ""
            End If

            If txtDeliveryFromAddressCS.Text <> "" Then
                objOral.SPO_DeliveryFrom = txtDeliveryFromAddressCS.Text
            Else
                objOral.SPO_DeliveryFrom = ""
            End If

            If txtDeleveryAddressCS.Text <> "" Then
                objOral.SPO_DeliveryAddress = txtDeleveryAddressCS.Text
            Else
                objOral.SPO_DeliveryAddress = ""
            End If

            If txtCompanyGSTNRegNoCS.Text <> "" Then
                objOral.SPO_CompanyGSTNRegNo = txtCompanyGSTNRegNoCS.Text
            Else
                objOral.SPO_CompanyGSTNRegNo = ""
            End If

            If txtBillingGSTNRegNoCS.Text <> "" Then
                objOral.SPO_BillingGSTNRegNo = txtBillingGSTNRegNoCS.Text
            Else
                objOral.SPO_BillingGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNoCS.Text <> "" Then
                objOral.SPO_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNoCS.Text
            Else
                objOral.SPO_DeliveryFromGSTNRegNo = ""
            End If

            If txtDeliveryGSTNRegNoCS.Text <> "" Then
                objOral.SPO_DeliveryGSTNRegNo = txtDeliveryGSTNRegNoCS.Text
            Else
                objOral.SPO_DeliveryGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                objOral.SPO_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNoCS.Text), Trim(txtDeliveryGSTNRegNoCS.Text))
            ElseIf txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                objOral.SPO_DispatchStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                objOral.SPO_DispatchStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                objOral.SPO_DispatchStatus = "Local"
            End If
            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
            '    objOral.SPO_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            'End If

            objOral.SPO_CompanyType = ddlCompanyTypeCS.SelectedValue
            objOral.SPO_GSTNCategory = ddlGSTCategoryCS.SelectedValue

            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
            '    objOral.SPO_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            'End If
            If txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                objOral.SPO_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNoCS.Text), Trim(txtDeliveryGSTNRegNoCS.Text))
            ElseIf txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                objOral.SPO_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNoCS.Text), (""))
            ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                objOral.SPO_State = CheckSourceDestinationState((""), Trim(txtDeliveryGSTNRegNoCS.Text))
            ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                Dim ibranch As Integer
                ibranch = objOral.getBranchFromPO(sSession.AccessCode, sSession.AccessCodeID, txtOrderCodeCS.Text)
                If ibranch > 0 Then 'branch 
                    objOral.SPO_State = objOral.CheckDetailsofBranchState(sSession.AccessCode, sSession.AccessCodeID, txtOrderCodeCS.Text)
                    If objOral.SPO_State = "" Then
                        lblError.Text = "Update state in branch master"
                        lblValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in branch master.','', 'success');", True)
                        Exit Sub
                    End If
                Else 'Companyl
                    objOral.SPO_State = objOral.CheckDetailsofCompState(sSession.AccessCode, sSession.AccessCodeID)
                    If objOral.SPO_State = "" Then
                        lblError.Text = "Update state in company master"
                        lblValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in company master.','', 'success');", True)
                        Exit Sub
                    End If
                End If
            End If

            If ddlBatchNo.SelectedIndex > 0 Then
                objOral.SPO_BatchNo = ddlBatchNo.SelectedValue
            Else
                objOral.SPO_BatchNo = 0
            End If
            objOral.SPO_BaseName = lstFiles.SelectedItem.Text  'BaseName ID

            Arr = objOral.SavePROFormaMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objOral)
            dt = Session("UpdateTab")
            iMasterID = Arr(1)

            objOral.SPOD_SOID = objGen.SafeSQL(Trim(iMasterID))

            If txtQuantityCashSales.Text <> "" Then

                objOral.SPOD_CommodityID = objGen.SafeSQL(Trim(ddlCommodityCS.SelectedValue))
                objOral.SPOD_ItemID = objGen.SafeSQL(Trim(lstBoxDescriptionCS.SelectedValue))
                objOral.SPOD_Quantity = objGen.SafeSQL(Trim(txtQuantityCashSales.Text))

                If ddlDiscountcs.SelectedIndex > 0 Then
                    objOral.SPOD_Discount = objGen.SafeSQL(Trim(ddlDiscountcs.SelectedItem.Text))
                Else
                    objOral.SPOD_Discount = 0
                End If

                objOral.SPOD_UnitofMeasurement = objGen.SafeSQL(Trim(ddlUnitOfMeassurementCS.SelectedValue))

                If txtAmountCS.Text <> "" Then
                    objOral.SPOD_RateAmount = txtAmountCS.Text
                Else
                    objOral.SPOD_RateAmount = 0
                End If

                If txtDiscountAmountCS.Text <> "" Then
                    objOral.SPOD_DiscountRate = txtDiscountAmountCS.Text
                Else
                    objOral.SPOD_DiscountRate = 0
                End If

                If txtOrderDateCS.Text <> "" Then
                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDateCS.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If

                objOral.SPOD_HistoryID = 0

                objOral.SPOD_CompiD = sSession.AccessCodeID
                objOral.SPOD_Status = "A"

                If txtMRPCS.Text <> "" Then
                    objOral.SPOD_MRPRate = txtMRPCS.Text
                Else
                    objOral.SPOD_MRPRate = 0
                End If

                objOral.SPOD_VAT = 0
                objOral.SPOD_VATAmount = 0
                objOral.SPOD_Excise = 0
                objOral.SPOD_ExciseAmount = 0

                objOral.SPOD_Operation = "C"
                objOral.SPOD_IPAddress = sSession.IPAddress

                If ddlCategoryCS.SelectedIndex > 0 Then
                    objOral.SPOD_Category = ddlCategoryCS.SelectedValue
                Else
                    objOral.SPOD_Category = 0
                End If

                objOral.SPOD_CreatedBy = sSession.UserID
                objOral.SPOD_CreatedOn = DateTime.Today
                objOral.SPOD_UpdatedBy = sSession.UserID
                objOral.SPOD_UpdatedOn = DateTime.Today

                objOral.SPOD_GST_ID = 0
                If txtGSTCS.Text <> "" Then
                    objOral.SPOD_GSTRate = txtGSTCS.Text
                Else
                    objOral.SPOD_GSTRate = 0
                End If
                If txtGSTAmountCS.Text <> "" Then
                    objOral.SPOD_GSTAmount = txtGSTAmountCS.Text
                Else
                    objOral.SPOD_GSTAmount = 0
                End If

                If objOral.SPO_DispatchStatus = "Local" Then
                    objOral.SPOD_SGST = objOral.SPOD_GSTRate / 2
                    objOral.SPOD_SGSTAmount = objOral.SPOD_GSTAmount / 2
                    objOral.SPOD_CGST = objOral.SPOD_GSTRate / 2
                    objOral.SPOD_CGSTAmount = objOral.SPOD_GSTAmount / 2
                    objOral.SPOD_IGST = 0
                    objOral.SPOD_IGSTAmount = 0
                ElseIf objOral.SPO_DispatchStatus = "Inter State" Then
                    objOral.SPOD_SGST = 0
                    objOral.SPOD_SGSTAmount = 0
                    objOral.SPOD_CGST = 0
                    objOral.SPOD_CGSTAmount = 0
                    objOral.SPOD_IGST = objOral.SPOD_GSTRate
                    objOral.SPOD_IGSTAmount = objOral.SPOD_GSTAmount
                End If

                If txtNetAmountCS.Text <> "" Then
                    objOral.SPOD_TotalAmount = txtNetAmountCS.Text
                Else
                    objOral.SPOD_TotalAmount = 0
                End If

                Arr = objOral.SavePROFormaMasterDetails(sSession.AccessCode, objOral, sSession.YearID)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnAddCDI.ImageUrl = "~/Images/Add24.png"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
            End If
            LoadExistingCashsaleOrderNo()
            ddlExistingCashSaleNo.SelectedValue = iMasterID
            LoadExistingOrderGrid(ddlBatchNo.SelectedValue, lstFiles.SelectedValue)

            ClearCDIDetails()
            SaveCharges(iMasterID)
            lblStatusCash.Text = "Waiting For Approve"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveCash()")
        End Try
    End Sub
    Public Function CheckSourceDestinationState(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            If sBillingAddress <> "" And sDeliveryAddress = "" Then
                sSource = sBillingAddress.Substring(0, 2)
                CheckSourceDestinationState = objCS.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sDeliveryAddress <> "" Then
                sDestination = sDeliveryAddress.Substring(0, 2)
                CheckSourceDestinationState = objCS.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sDeliveryAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sDeliveryAddress.Substring(0, 2)
                If sSource = sDestination Then
                    CheckSourceDestinationState = objCS.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    CheckSourceDestinationState = objCS.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return CheckSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationState")
        End Try
    End Function
    Public Sub BindCashDetails()
        Dim dtMaster As New DataTable
        Dim bCheck As String = ""
        Dim sStatus As String = ""
        Try
            lblError.Text = ""

            imgbtnAddCDI.ImageUrl = "~/Images/Update24.png"

            dtMaster = objCS.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If dtMaster.Rows.Count > 0 Then
                For i = 0 To dtMaster.Rows.Count - 1
                    If IsDBNull(dtMaster.Rows(i)("SPO_ID")) = False Then
                        ddlExistingCashSaleNo.SelectedValue = dtMaster.Rows(i)("SPO_ID")
                    Else
                        ddlExistingCashSaleNo.SelectedIndex = 0
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_OrderCode")) = False Then
                        txtOrderCodeCS.Text = dtMaster.Rows(i)("SPO_OrderCode")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("SPO_OrderDate")) = False Then
                        txtOrderDateCS.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_OrderDate"), "D")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("SPO_ModeOfDispatch")) = False Then
                        If dtMaster.Rows(i)("SPO_ModeOfDispatch") > 0 Then
                            ddlShippingCS.SelectedValue = dtMaster.Rows(i)("SPO_ModeOfDispatch")
                        Else
                            ddlShippingCS.SelectedIndex = 0
                        End If
                    Else
                        ddlShippingCS.SelectedIndex = 0
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_SalesManID")) = False Then
                        If dtMaster.Rows(i)("SPO_SalesManID") > 0 Then
                            ddlSalesManCS.SelectedValue = dtMaster.Rows(i)("SPO_SalesManID")
                        Else
                            ddlSalesManCS.SelectedIndex = 0
                        End If
                    Else
                        ddlSalesManCS.SelectedIndex = 0
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_BuyerOrderNo")) = False Then
                        txtBuyerPurOrderNoCS.Text = dtMaster.Rows(i)("SPO_BuyerOrderNo")
                    Else
                        txtBuyerPurOrderNoCS.Text = ""
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_BuyerOrderDate")) = False Then
                        If (dtMaster.Rows(i)("SPO_BuyerOrderDate")) <> "1899-12-30 00:00:00.000" Then
                            txtBuyerOrderDateCS.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_BuyerOrderDate"), "D")
                        Else
                            txtBuyerOrderDateCS.Text = ""
                        End If
                    Else
                        txtBuyerOrderDateCS.Text = ""
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_Category")) = False Then
                        If dtMaster.Rows(i)("SPO_Category") > 0 Then
                            ddlCategoryCS.SelectedValue = dtMaster.Rows(i)("SPO_Category")
                        Else
                            ddlCategoryCS.SelectedIndex = 0
                        End If
                    Else
                        ddlCategoryCS.SelectedIndex = 0
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_Remarks")) = False Then
                        txtRemarksCS.Text = dtMaster.Rows(i)("SPO_Remarks")
                    Else
                        txtRemarksCS.Text = ""
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_ShippingDate")) = False Then
                        If (dtMaster.Rows(i)("SPO_ShippingDate")) <> "1899-12-30 00:00:00.000" Then
                            txtShippingDateCS.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_ShippingDate"), "D")
                        Else
                            txtShippingDateCS.Text = ""
                        End If
                    Else
                        txtShippingDateCS.Text = ""
                    End If
                    If IsDBNull(dtMaster.Rows(i)("SPO_PaymentType")) = False Then
                        ddlPaymentTypeCS.SelectedValue = dtMaster.Rows(i)("SPO_PaymentType")
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_PartyCode")) = False Then
                        txtPartyNoCS.Text = dtMaster.Rows(i)("SPO_PartyCode")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("SPO_PartyName")) = False Then
                        ddlPatryCS.SelectedValue = dtMaster.Rows(i)("SPO_PartyName")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("SPO_ContantNo")) = False Then
                        txtContactNoCS.Text = dtMaster.Rows(i)("SPO_ContantNo")
                    Else
                        txtContactNoCS.Text = ""
                    End If
                    If IsDBNull(dtMaster.Rows(i)("SPO_Address")) = False Then
                        txtAddressCS.Text = dtMaster.Rows(i)("SPO_Address")
                    Else
                        txtAddressCS.Text = ""
                    End If
                    If IsDBNull(dtMaster.Rows(i)("SPO_ModeOfCommunication")) = False Then
                        If dtMaster.Rows(i)("SPO_ModeOfCommunication") > 0 Then
                            ddlModeOfCommunicationCS.SelectedValue = dtMaster.Rows(i)("SPO_ModeOfCommunication")
                        Else
                            ddlModeOfCommunicationCS.SelectedIndex = 0
                        End If
                    Else
                        ddlModeOfCommunicationCS.SelectedIndex = 0
                    End If
                    If IsDBNull(dtMaster.Rows(i)("SPO_InputBy")) = False Then
                        txtInputByCS.Text = dtMaster.Rows(i)("SPO_InputBy")
                    Else
                        txtInputByCS.Text = ""
                    End If

                    If IsDBNull(dtMaster.Rows(i)("SPO_ShippingCharge")) = False Then
                        If dtMaster.Rows(i)("SPO_ShippingCharge") > 0 Then
                            ddlShippingChargesCS.SelectedValue = dtMaster.Rows(i)("SPO_ShippingCharge")
                        Else
                            ddlShippingChargesCS.SelectedIndex = 0
                        End If
                    Else
                        ddlShippingChargesCS.SelectedIndex = 0
                    End If

                    If IsDBNull(dtMaster.Rows(0)("SPO_CompanyAddress")) = False Then
                        txtCompanyAddressCS.Text = dtMaster.Rows(0)("SPO_CompanyAddress")
                    Else
                        txtCompanyAddressCS.Text = ""
                    End If
                    If IsDBNull(dtMaster.Rows(0)("SPO_CompanyGSTNRegNo")) = False Then
                        txtCompanyGSTNRegNoCS.Text = dtMaster.Rows(0)("SPO_CompanyGSTNRegNo")
                    Else
                        txtCompanyGSTNRegNoCS.Text = ""
                    End If

                    If IsDBNull(dtMaster.Rows(0)("SPO_BillingAddress")) = False Then
                        txtBillingAddressCS.Text = dtMaster.Rows(0)("SPO_BillingAddress")
                    Else
                        txtBillingAddressCS.Text = ""
                    End If
                    If IsDBNull(dtMaster.Rows(0)("SPO_BillingGSTNRegNo")) = False Then
                        txtBillingGSTNRegNoCS.Text = dtMaster.Rows(0)("SPO_BillingGSTNRegNo")
                    Else
                        txtBillingGSTNRegNoCS.Text = ""
                    End If

                    If IsDBNull(dtMaster.Rows(0)("SPO_DeliveryFrom")) = False Then
                        txtDeliveryFromAddressCS.Text = dtMaster.Rows(0)("SPO_DeliveryFrom")
                    Else
                        txtDeliveryFromAddressCS.Text = ""
                    End If
                    If IsDBNull(dtMaster.Rows(0)("SPO_DeliveryFromGSTNRegNo")) = False Then
                        txtDeliveryFromGSTNRegNoCS.Text = dtMaster.Rows(0)("SPO_DeliveryFromGSTNRegNo")
                    Else
                        txtDeliveryFromGSTNRegNoCS.Text = ""
                    End If

                    If IsDBNull(dtMaster.Rows(0)("SPO_DeliveryAddress")) = False Then
                        txtDeleveryAddressCS.Text = dtMaster.Rows(0)("SPO_DeliveryAddress")
                    Else
                        txtDeleveryAddressCS.Text = ""
                    End If
                    If IsDBNull(dtMaster.Rows(0)("SPO_DeliveryGSTNRegNo")) = False Then
                        txtDeliveryGSTNRegNoCS.Text = dtMaster.Rows(0)("SPO_DeliveryGSTNRegNo")
                    Else
                        txtDeliveryGSTNRegNoCS.Text = ""
                    End If
                Next
            End If
            lstBoxDescriptionCS.Items.Clear()
            LoadExistingOrderGrid(ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSearch_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub SaveDispatch()
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
            If txtOrderNoDispatch.Text <> "" Then

                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Invoice Date (" & txtDispatchDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtDispatchDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Invoice Date (" & txtDispatchDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtDispatchDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'

                'If GvCharge.Items.Count = 0 Then
                '    lblError.Text = "Enter Charges"
                '    Exit Sub
                'End If

                objDis.DM_Code = txtDispatchNo.Text
                objDis.DM_OrderID = 0
                If txtOrderDateDispatch.Text <> "" Then
                    objDis.DM_OrderDate = Date.ParseExact(Trim(txtOrderDateDispatch.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                objDis.DM_SupplierID = objGen.SafeSQL(Trim(ddlPatryCS.SelectedValue))
                If ddlShippingCS.SelectedIndex > 0 Then
                    objDis.DM_ModeOfShipping = ddlShippingCS.SelectedValue
                Else
                    objDis.DM_ModeOfShipping = 0
                End If
                If txtDispatchDate.Text <> "" Then
                    objDis.DM_DispatchDate = Date.ParseExact(Trim(txtDispatchDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If

                'Dim dDate, dSDate As Date
                'Cheque Date Comparision'
                dDate = Date.ParseExact(txtOrderDateDispatch.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'Dim m As Integer
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Dispatch Date (" & txtDispatchDate.Text & ") should be Greater than or equal to Order Date(" & txtOrderDateDispatch.Text & ")."
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    txtDispatchDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'

                'Check Source & Destination State Code'
                Dim sSStr As String = "" : Dim sDStr As String = ""
                If txtDeliveryFromGSTNRegNoCS.Text <> "" Then
                    sSStr = objDis.CheckStateCode(sSession.AccessCode, sSession.AccessCodeID, Trim(txtDeliveryFromGSTNRegNoCS.Text))
                    If sSStr = False Then
                        lblError.Text = "Delivery From GSTN Reg.No Does Not Exists."
                        Exit Sub
                    End If
                End If
                If txtDeliveryGSTNRegNoCS.Text <> "" Then
                    sDStr = objDis.CheckStateCode(sSession.AccessCode, sSession.AccessCodeID, Trim(txtDeliveryGSTNRegNoCS.Text))
                    If sDStr = False Then
                        lblError.Text = "Shipping To GSTN Reg.No Does Not Exists."
                        Exit Sub
                    End If
                End If
                'Check Source & Destination State Code'

                objDis.DM_PaymentType = ddlPaymentTypeCS.SelectedValue
                If txtShippingRate.Text <> "" Then
                    objDis.DM_ShippingRate = objGen.SafeSQL(Trim(txtShippingRate.Text))
                Else
                    objDis.DM_ShippingRate = 0
                End If
                objDis.DM_ExpectedDays = 0

                objDis.DM_Status = "W"
                objDis.DM_CompID = sSession.AccessCodeID
                objDis.DM_YearID = sSession.YearID
                objDis.DM_CreatedBy = sSession.UserID
                objDis.DM_CreatedOn = System.DateTime.Now

                objDis.DM_Operation = "C"
                objDis.DM_IPAddress = sSession.IPAddress

                objDis.DM_ChequeNo = ""
                objDis.DM_ChequeDate = "01/01/1900"
                objDis.DM_IFSCCode = ""
                objDis.DM_BankName = 0
                objDis.DM_Branch = ""

                objDis.DM_GrandDiscount = 0
                objDis.DM_GrandDiscountAmt = 0
                objDis.DM_GrandTotal = 0
                objDis.DM_GrandTotalAmt = 0

                If ddlSalesManCS.SelectedIndex > 0 Then
                    objDis.DM_SalesManID = ddlSalesManCS.SelectedValue
                Else
                    objDis.DM_SalesManID = 0
                End If

                If txtDispatchRefNo.Text <> "" Then
                    objDis.DM_DispatchRefNo = txtDispatchRefNo.Text
                Else
                    objDis.DM_DispatchRefNo = ""
                End If

                If txtESugamNo.Text <> "" Then
                    objDis.DM_ESugamNo = txtESugamNo.Text
                Else
                    objDis.DM_ESugamNo = ""
                End If

                If txtRemarksCS.Text <> "" Then
                    objDis.DM_Remarks = txtRemarksCS.Text
                Else
                    objDis.DM_Remarks = ""
                End If

                objDis.DM_SaleType = 0
                objDis.DM_OtherType = 0

                objDis.DM_AllocateID = 0

                objDis.DM_TrType = 4

                If txtCompanyAddressCS.Text <> "" Then
                    objDis.DM_CompanyAddress = txtCompanyAddressCS.Text
                Else
                    objDis.DM_CompanyAddress = ""
                End If

                If txtBillingAddressCS.Text <> "" Then
                    objDis.DM_BillingAddress = txtBillingAddressCS.Text
                Else
                    objDis.DM_BillingAddress = ""
                End If

                If txtDeliveryFromAddressCS.Text <> "" Then
                    objDis.DM_DeliveryFrom = txtDeliveryFromAddressCS.Text
                Else
                    objDis.DM_DeliveryFrom = ""
                End If

                If txtDeleveryAddressCS.Text <> "" Then
                    objDis.DM_DeliveryAddress = txtDeleveryAddressCS.Text
                Else
                    objDis.DM_DeliveryAddress = ""
                End If

                If txtCompanyGSTNRegNoCS.Text <> "" Then
                    objDis.DM_CompanyGSTNRegNo = txtCompanyGSTNRegNoCS.Text
                Else
                    objDis.DM_CompanyGSTNRegNo = ""
                End If

                If txtBillingGSTNRegNoCS.Text <> "" Then
                    objDis.DM_BillingGSTNRegNo = txtBillingGSTNRegNoCS.Text
                Else
                    objDis.DM_BillingGSTNRegNo = ""
                End If

                If txtDeliveryFromGSTNRegNoCS.Text <> "" Then
                    objDis.DM_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNoCS.Text
                Else
                    objDis.DM_DeliveryFromGSTNRegNo = ""
                End If

                If txtDeliveryGSTNRegNoCS.Text <> "" Then
                    objDis.DM_DeliveryGSTNRegNo = txtDeliveryGSTNRegNoCS.Text
                Else
                    objDis.DM_DeliveryGSTNRegNo = ""
                End If

                If txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                    objDis.DM_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNoCS.Text), Trim(txtDeliveryGSTNRegNoCS.Text))
                ElseIf txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                    objDis.DM_DispatchStatus = "Local"
                ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                    objDis.DM_DispatchStatus = "Local"
                ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                    objDis.DM_DispatchStatus = "Local"
                End If
                'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                '    objDis.DM_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
                'End If

                objDis.DM_CompanyType = ddlCompanyTypeCS.SelectedValue
                objDis.DM_GSTNCategory = ddlGSTCategoryCS.SelectedValue

                'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                '    objDis.DM_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
                'End If
                If txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                    objDis.DM_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNoCS.Text), Trim(txtDeliveryGSTNRegNoCS.Text))
                ElseIf txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                    objDis.DM_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNoCS.Text), (""))
                ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                    objDis.DM_State = CheckSourceDestinationState((""), Trim(txtDeliveryGSTNRegNoCS.Text))
                ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text = "" Then

                    If ddlAccBrnch.SelectedIndex > 0 Then 'branch 
                        objDis.DM_State = objDis.CheckDetailsofBranchState(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                        If objDis.DM_State = "" Then
                            lblError.Text = "Update state in branch master"
                            lblValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in branch master.','', 'success');", True)
                            Exit Sub
                        End If
                    Else 'Company
                        objDis.DM_State = objDis.CheckDetailsofCompState(sSession.AccessCode, sSession.AccessCodeID)
                        If objDis.DM_State = "" Then
                            lblError.Text = "Update state in company master"
                            lblValidationMsg.Text = lblError.Text
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
                sName = "Sale Of Product " & objDis.DM_State
                txtGLID.Text = objDis.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                If txtGLID.Text > 0 Then
                    'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
                Else
                    iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
                End If
                'Chart Of Accounts'

                Dim dtGSTRates As New DataTable
                dtGSTRates = objDis.BindGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'Extra'
                dtGSTRates.Rows.Add("0")
                'Extra'
                If dtGSTRates.Rows.Count > 0 Then
                    For x = 0 To dtGSTRates.Rows.Count - 1

                        sName = "Local GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objDis.DM_State & " Sale Account"
                        txtGLID.Text = objDis.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                        End If

                        sName = "Inter State GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objDis.DM_State & " Sale Account"
                        txtGLID.Text = objDis.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
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

                        sName = "OUTPUT SGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objDis.DM_State & " Sale Account"
                        txtGLID.Text = objDis.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                        End If

                        sName = "OUTPUT CGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objDis.DM_State & " Sale Account"
                        txtGLID.Text = objDis.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                        End If

                        sName = "OUTPUT IGST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objDis.DM_State & " Sale Account"
                        txtGLID.Text = objDis.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                        End If

                    Next
                End If

                If txtOrderNoDispatch.Text <> "" Then
                    objDis.DM_OrderNo = txtOrderNoDispatch.Text
                Else
                    objDis.DM_OrderNo = ""
                End If

                If txtAllocationNoDispatch.Text <> "" Then
                    objDis.DM_AllocationNo = txtAllocationNoDispatch.Text
                Else
                    objDis.DM_AllocationNo = ""
                End If

                If ddlBatchNo.SelectedIndex > 0 Then
                    objDis.DM_BatchNo = ddlBatchNo.SelectedValue
                Else
                    objDis.DM_BatchNo = 0
                End If
                objDis.DM_BaseName = lstFiles.SelectedItem.Text 'BaseName

                Arr = objDis.SaveDispatchMaster(sSession.AccessCode, objDis)
                iMasterID = Arr(1)
                txtMasterID.Text = iMasterID

                'lblCommodityID = grdDispatchDetails.Rows(i).FindControl("lblCommodityID")
                'lblItemID = grdDispatchDetails.Rows(i).FindControl("lblItemID")
                'lblHistoryID = grdDispatchDetails.Rows(i).FindControl("lblHistoryID")
                'lblUnitID = grdDispatchDetails.Rows(i).FindControl("lblUnitID")
                'lblCommodity = grdDispatchDetails.Rows(i).FindControl("lblCommodity")
                'lblGoods = grdDispatchDetails.Rows(i).FindControl("lblGoods")
                'lblUnit = grdDispatchDetails.Rows(i).FindControl("lblUnit")
                'lblMRP = grdDispatchDetails.Rows(i).FindControl("lblMRP")
                'lblOrderedQty = grdDispatchDetails.Rows(i).FindControl("lblOrderedQty")
                'lblTotalAmount = grdDispatchDetails.Rows(i).FindControl("lblTotal")

                'HFDiscountAmount = grdDispatchDetails.Rows(i).FindControl("HFDiscountAmount")

                'HFNetAmount = grdDispatchDetails.Rows(i).FindControl("HFNetAmount")

                'ddlDiscount = grdDispatchDetails.Rows(i).FindControl("ddlDiscount")

                'lblGSTID = grdDispatchDetails.Rows(i).FindControl("lblGSTID")
                'lblGSTRate = grdDispatchDetails.Rows(i).FindControl("lblGSTRate")
                'HFGSTAmount = grdDispatchDetails.Rows(i).FindControl("HFGSTAmount")

                objDis.DD_MasterID = iMasterID
                objDis.DD_CommodityID = ddlCommodityCS.SelectedValue
                objDis.DD_DescID = lstBoxDescriptionCS.SelectedValue
                objDis.DD_HistoryID = 0
                objDis.DD_UnitID = ddlUnitOfMeassurementCS.SelectedValue

                objDis.DD_Rate = txtMRPCS.Text
                objDis.DD_Quantity = txtQuantityCashSales.Text
                objDis.DD_RateAmount = txtAmountCS.Text

                objDis.DD_GST_ID = 0
                If txtGSTCS.Text <> "" Then
                    objDis.DD_GSTRate = txtGSTCS.Text
                Else
                    objDis.DD_GSTRate = 0
                End If

                objDis.DD_Status = "W"
                objDis.DD_CompID = sSession.AccessCodeID
                objDis.DD_Operation = "C"
                objDis.DD_IPAddress = sSession.IPAddress
                objDis.DD_CreatedBy = sSession.UserID
                objDis.DD_CreatedOn = System.DateTime.Now

                Arr = objDis.SaveDispatchDetails(sSession.AccessCode, objDis)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    lblValidationMsg.Text = lblError.Text
                    imgbtnAddCDI.ImageUrl = "~/Images/Save16.png"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If

                LoadExistingDispatchNo()
                ddlExistingDispatchNo.SelectedValue = iMasterID
                ClearCDIDetails()

                grdDispatch.DataSource = objDis.BindDispatchedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
                grdDispatch.DataBind()

                SaveCharges(iMasterID)
                lblStatusDispatch.Text = "Waiting For Approval"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveDispatch()")
        End Try
    End Sub
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
    Private Sub grdDispatch_PreRender(sender As Object, e As EventArgs) Handles grdDispatch.PreRender
        Dim dt As New DataTable
        Try
            If grdDispatch.Rows.Count > 0 Then
                grdDispatch.UseAccessibleHeader = True
                grdDispatch.HeaderRow.TableSection = TableRowSection.TableHeader
                grdDispatch.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdDispatch_PreRender")
        End Try
    End Sub
    Private Sub grdDispatch_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDispatch.RowCommand
        Dim iItemID As Integer, iCommodityID As Integer
        Dim lblCommodityID As New Label
        Dim lblItemID As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)

                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)
                iCommodityID = lblCommodityID.Text
                iItemID = lblItemID.Text

                dt = objDis.BindDispatchData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingDispatchNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        BindDescription(dt.Rows(i)("DD_CommodityID"))
                        lstBoxDescriptionCS.SelectedValue = dt.Rows(i)("DD_DescID")

                        txtQuantityCashSales.Text = dt.Rows(i)("DD_Quantity")

                        txtMRPCS.Text = dt.Rows(i)("DD_Rate")
                        hfMRP.Value = dt.Rows(i)("DD_Rate")

                        txtAmountCS.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_RateAmount")))
                        hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_RateAmount")))

                        txtNetAmountCS.Text = 0
                        hfNetAmount.Value = 0

                        LoadDiscount()
                        'If dt.Rows(i)("DM_Discount") > 0 Then
                        '    ddlDiscountcs.SelectedValue = objSO.GetDiscountID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("DM_Discount"))
                        'Else
                        ddlDiscountcs.SelectedIndex = 0
                        'End If
                        txtDiscountAmountCS.Text = 0

                        BindCashSalesUnitOfMeassurement(lstBoxDescription.SelectedValue)
                        ddlUnitOfMeassurementCS.SelectedValue = dt.Rows(i)("DD_UnitID")
                    Next
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdDispatch_RowCommand")
        End Try
    End Sub
    Private Sub dgExistingProFormaSalesOrder_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgExistingProFormaSalesOrder.RowCommand
        Dim iItemID As Integer, iCommodityID As Integer, iOrderNo As Integer
        Dim dt As New DataTable
        Dim sCode As String = "", sStatus As String = ""
        Dim sStr As String = ""
        Dim dtRate As New DataTable

        Dim bCheck As String = ""
        Dim iCategoryID As Integer

        Dim lblCommodityID, lblPKID As New Label
        Dim lblItemID As New Label
        Try
            lblError.Text = ""

            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)

                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)
                lblPKID = DirectCast(clickedRow.FindControl("lblPKID"), Label)

                iCommodityID = lblCommodityID.Text
                iItemID = lblItemID.Text

                If ddlTrType.SelectedItem.Text = "Sales" Then
                    iOrderNo = ddlExistingSalesNo.SelectedValue
                    imgbtnAddSale.ImageUrl = "~/Images/Update24.png"

                    ddlCommodityS.SelectedValue = iCommodityID

                    dt = objSO.BindExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iItemID, iOrderNo, lblPKID.Text)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            BindDescription(dt.Rows(i)("SPOD_CommodityID"))
                            lstBoxDescription.SelectedValue = dt.Rows(i)("SPOD_ItemID")

                            txtQuantity.Text = dt.Rows(i)("SPOD_Quantity")

                            txtMRP.Text = dt.Rows(i)("SPOD_MRPRate")
                            hfMRP.Value = dt.Rows(i)("SPOD_MRPRate")

                            txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))
                            hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))

                            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))
                            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))

                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue)

                            ddlUnitOfMeassurement.SelectedValue = dt.Rows(i)("SPOD_UnitOfMeasurement")
                            txtMRPFromTable.Text = txtMRP.Text
                        Next
                    End If
                ElseIf ddlTrType.SelectedItem.Text = "Cash sales" Then
                    iOrderNo = ddlExistingCashSaleNo.SelectedValue
                    imgbtnAddCDI.ImageUrl = "~/Images/Update24.png"

                    ddlCommodityCS.SelectedValue = iCommodityID

                    dt = objCS.BindExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iItemID, iOrderNo, lblPKID.Text)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            BindCashDescription(dt.Rows(i)("SPOD_CommodityID"))
                            lstBoxDescriptionCS.SelectedValue = dt.Rows(i)("SPOD_ItemID")

                            txtQuantityCashSales.Text = dt.Rows(i)("SPOD_Quantity")

                            txtMRPCS.Text = dt.Rows(i)("SPOD_MRPRate")

                            txtAmountCS.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))
                            hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))

                            txtNetAmountCS.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))
                            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))

                            LoadDiscount()
                            If dt.Rows(i)("SPOD_Discount") > 0 Then
                                ddlDiscountcs.SelectedValue = objCS.GetDiscountID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("SPOD_Discount"))
                            Else
                                ddlDiscountcs.SelectedIndex = 0
                            End If
                            txtDiscountAmountCS.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_DiscountRate")))

                            txtGSTCS.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_GSTRate")))
                            txtGSTAmountCS.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_GSTAmount")))

                            BindCashSalesUnitOfMeassurement(lstBoxDescriptionCS.SelectedValue)

                            ddlUnitOfMeassurementCS.SelectedValue = dt.Rows(i)("SPOD_UnitOfMeasurement")
                            txtMRPFromTable.Text = txtMRP.Text

                        Next
                    End If
                End If
            ElseIf e.CommandName = "Cancel" Then

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_RowCommand")
        End Try
    End Sub
    Public Sub BindDispatch()
        Try
            Dim dt As New DataTable
            Dim dtCharge As New DataTable
            Try
                dt = objDis.BindDispatchMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
                If IsNothing(dt) = False Then

                    If IsDBNull(dt.Rows(0)("DM_OrderNo")) = False Then
                        ddlExistingDispatchNo.SelectedValue = dt.Rows(0)("DM_ID")
                    Else
                        ddlExistingDispatchNo.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("DM_OrderNo")) = False Then
                        txtOrderNoDispatch.Text = dt.Rows(0)("DM_OrderNo")
                    Else
                        txtOrderNoDispatch.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("DM_AllocationNo")) = False Then
                        txtAllocationNoDispatch.Text = dt.Rows(0)("DM_AllocationNo")
                    Else
                        txtAllocationNoDispatch.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("DM_Code")) = False Then
                        txtDispatchNo.Text = dt.Rows(0)("DM_Code")
                    Else
                        txtDispatchNo.Text = ""
                    End If
                    txtOrderDateDispatch.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("DM_OrderDate"), "D")
                    txtDispatchDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("DM_DispatchDate"), "D")
                    ddlPatryCS.SelectedValue = dt.Rows(0)("DM_SupplierID")
                    txtCode.Value = objDis.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlPatryCS.SelectedValue)

                    ddlCompanyTypeCS.SelectedValue = dt.Rows(0)("DM_CompanyType")
                    BindCashSalesGSTNCategory(ddlCompanyTypeCS.SelectedItem.Text)
                    ddlGSTCategoryCS.SelectedValue = dt.Rows(0)("DM_GSTNCategory")

                    If IsDBNull(dt.Rows(0)("DM_ModeOfShipping")) = False Then
                        If dt.Rows(0)("DM_ModeOfShipping") > 0 Then
                            ddlShippingCS.SelectedValue = dt.Rows(0)("DM_ModeOfShipping")
                        Else
                            ddlShippingCS.SelectedIndex = 0
                        End If
                    Else
                        ddlShippingCS.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("DM_SalesManID")) = False Then
                        If dt.Rows(0)("DM_SalesManID") > 0 Then
                            ddlSalesManCS.SelectedValue = dt.Rows(0)("DM_SalesManID")
                        Else
                            ddlSalesManCS.SelectedIndex = 0
                        End If
                    Else
                        ddlSalesManCS.SelectedIndex = 0
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


                    ddlPaymentTypeCS.SelectedValue = dt.Rows(0)("DM_PaymentType")
                    'If UCase(ddlPaymentType.SelectedItem.Text) = UCase("Cheque") Then
                    '    'divcollapseChequeDetails.Visible = True
                    '    divcollapseChequeDetails.Visible = False
                    '    GetBankDDL()
                    '    txtChequeNo.Text = dt.Rows(0)("DM_ChequeNo") : txtChequeDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("DM_ChequeDate"), "D")
                    '    txtIFSCCode.Text = dt.Rows(0)("DM_IFSCCode")
                    '    If IsDBNull(dt.Rows(0)("DM_BankName")) = False Then
                    '        If dt.Rows(0)("DM_BankName") > 0 Then
                    '            ddlBankName.SelectedValue = dt.Rows(0)("DM_BankName")
                    '        Else
                    '            ddlBankName.SelectedIndex = 0
                    '        End If
                    '    Else
                    '        ddlBankName.SelectedIndex = 0
                    '    End If
                    '    txtBranch.Text = dt.Rows(0)("DM_Branch")
                    'Else
                    '    divcollapseChequeDetails.Visible = False
                    'End If

                    If IsDBNull(dt.Rows(0)("DM_Status")) = False Then
                        If dt.Rows(0)("DM_Status") = "A" Then
                            lblStatusDispatch.Text = "Approved"
                        Else
                            lblStatusDispatch.Text = "Waiting For Approval"
                        End If
                    Else
                        lblStatusDispatch.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("DM_CompanyAddress")) = False Then
                        txtCompanyAddressCS.Text = dt.Rows(0)("DM_CompanyAddress")
                    Else
                        txtCompanyAddressCS.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("DM_CompanyGSTNRegNo")) = False Then
                        txtCompanyGSTNRegNoCS.Text = dt.Rows(0)("DM_CompanyGSTNRegNo")
                    Else
                        txtCompanyGSTNRegNoCS.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("DM_BillingAddress")) = False Then
                        txtBillingAddressCS.Text = dt.Rows(0)("DM_BillingAddress")
                    Else
                        txtBillingAddressCS.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("DM_BillingGSTNRegNo")) = False Then
                        txtBillingGSTNRegNoCS.Text = dt.Rows(0)("DM_BillingGSTNRegNo")
                    Else
                        txtBillingGSTNRegNoCS.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("DM_DeliveryFrom")) = False Then
                        txtDeliveryFromAddressCS.Text = dt.Rows(0)("DM_DeliveryFrom")
                    Else
                        txtDeliveryFromAddressCS.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("DM_DeliveryFromGSTNRegNo")) = False Then
                        txtDeliveryFromGSTNRegNoCS.Text = dt.Rows(0)("DM_DeliveryFromGSTNRegNo")
                    Else
                        txtDeliveryFromGSTNRegNoCS.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("DM_DeliveryAddress")) = False Then
                        txtDeleveryAddressCS.Text = dt.Rows(0)("DM_DeliveryAddress")
                    Else
                        txtDeleveryAddressCS.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("DM_DeliveryGSTNRegNo")) = False Then
                        txtDeliveryGSTNRegNoCS.Text = dt.Rows(0)("DM_DeliveryGSTNRegNo")
                    Else
                        txtDeliveryGSTNRegNoCS.Text = ""
                    End If

                    Dim taxcategory As String
                    taxcategory = objDis.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategoryCS.SelectedValue)
                    If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryFromGSTNRegNoCS.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNoCS.Enabled = True
                    End If


                    grdDispatch.DataSource = objDis.BindDispatchedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingDispatchNo.SelectedValue)
                    grdDispatch.DataBind()

                    'dtCharge = objDis.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlExistingDispatchNo.SelectedValue, 0)
                    'GvCharge.DataSource = dtCharge
                    'GvCharge.DataBind()
                    'Session("ChargesMaster") = dtCharge
                End If
            Catch ex As Exception
                Throw
            End Try
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveInvoice()
        Dim Arr() As String, OrderNo As String = ""
        Dim iMasterID As Integer = 0
        Dim bCheck As String = ""
        Dim iAllocateID As Integer

        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblGoods, lblUnit, lblMRP, lblOrderedQty, lblTotalAmount, lblCST As New Label
        Dim HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount As HiddenField
        Dim ddlDiscount, ddlVAT, ddlCST, ddlExice As New DropDownList
        Dim sSource As String = "" : Dim sDestination As String = ""
        Dim lblGSTRate As New Label : Dim HFGSTAmount, HFCharges, HFAmount As HiddenField
        Dim lblGSTID As New Label
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtSalesInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Invoice Date (" & txtSalesInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtSalesInvoiceDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtSalesInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Invoice Date (" & txtSalesInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtSalesInvoiceDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            objDispatch.SDM_Code = txtsaleInvoiceNo.Text
            objDispatch.SDM_OrderID = 0
            If txtOrderDateInvoice.Text <> "" Then
                objDispatch.SDM_OrderDate = Date.ParseExact(Trim(txtOrderDateInvoice.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            objDispatch.SDM_SupplierID = objGen.SafeSQL(Trim(ddlPatryCS.SelectedValue))
            If ddlShippingCS.SelectedIndex > 0 Then
                objDispatch.SDM_ModeOfShipping = ddlShippingCS.SelectedValue
            Else
                objDispatch.SDM_ModeOfShipping = 0
            End If

            If txtDispatchDateInvoice.Text <> "" Then
                objDispatch.SDM_DispatchDate = Date.ParseExact(Trim(txtDispatchDateInvoice.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            'Cheque Date Comparision'
            dDate = Date.ParseExact(txtOrderDateInvoice.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtSalesInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'Dim m As Integer
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Dispatch Date (" & txtSalesInvoiceDate.Text & ") should be Greater than or equal to Order Date(" & txtOrderDateInvoice.Text & ")."
                lblValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtSalesInvoiceDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            'Check Source & Destination State Code'
            Dim sSStr As String = "" : Dim sDStr As String = ""
            If txtDeliveryFromGSTNRegNoCS.Text <> "" Then
                sSStr = objDispatch.CheckStateCode(sSession.AccessCode, sSession.AccessCodeID, Trim(txtDeliveryFromGSTNRegNoCS.Text))
                If sSStr = False Then
                    lblError.Text = "Delivery From GSTN Reg.No Does Not Exists."
                    Exit Sub
                End If
            End If
            If txtDeliveryGSTNRegNoCS.Text <> "" Then
                sDStr = objDispatch.CheckStateCode(sSession.AccessCode, sSession.AccessCodeID, Trim(txtDeliveryGSTNRegNoCS.Text))
                If sDStr = False Then
                    lblError.Text = "Shipping To GSTN Reg.No Does Not Exists."
                    Exit Sub
                End If
            End If
            'Check Source & Destination State Code'

            objDispatch.SDM_PaymentType = ddlPaymentTypeCS.SelectedValue
            objDispatch.SDM_ShippingRate = 0
            objDispatch.SDM_ExpectedDays = 0

            objDispatch.SDM_Status = "W"
            objDispatch.SDM_CompID = sSession.AccessCodeID
            objDispatch.SDM_YearID = sSession.YearID
            objDispatch.SDM_CreatedBy = sSession.UserID
            objDispatch.SDM_CreatedOn = System.DateTime.Now

            objDispatch.SDM_Operation = "C"
            objDispatch.SDM_IPAddress = sSession.IPAddress

            objDispatch.SDM_ChequeNo = ""
            objDispatch.SDM_ChequeDate = "01/01/1900"
            objDispatch.SDM_IFSCCode = ""
            objDispatch.SDM_BankName = 0
            objDispatch.SDM_Branch = ""

            objDispatch.SDM_GrandDiscount = 0
            objDispatch.SDM_GrandDiscountAmt = 0
            objDispatch.SDM_GrandTotal = 0
            objDispatch.SDM_GrandTotalAmt = 0

            If ddlSalesManCS.SelectedIndex > 0 Then
                objDispatch.SDM_SalesManID = ddlSalesManCS.SelectedValue
            Else
                objDispatch.SDM_SalesManID = 0
            End If

            If txtSalesInvoiceRefNo.Text <> "" Then
                objDispatch.SDM_DispatchRefNo = txtSalesInvoiceRefNo.Text
            Else
                objDispatch.SDM_DispatchRefNo = ""
            End If

            If txtESugamNo.Text <> "" Then
                objDispatch.SDM_ESugamNo = txtESugamNo.Text
            Else
                objDispatch.SDM_ESugamNo = ""
            End If

            If txtRemarksCS.Text <> "" Then
                objDispatch.SDM_Remarks = txtRemarksCS.Text
            Else
                objDispatch.SDM_Remarks = ""
            End If

            objDispatch.SDM_SaleType = 0
            objDispatch.SDM_OtherType = 0

            objDispatch.SDM_AllocateID = 0
            objDispatch.SDM_TrType = 5

            If txtCompanyAddressCS.Text <> "" Then
                objDispatch.SDM_CompanyAddress = txtCompanyAddressCS.Text
            Else
                objDispatch.SDM_CompanyAddress = ""
            End If

            If txtBillingAddressCS.Text <> "" Then
                objDispatch.SDM_BillingAddress = txtBillingAddressCS.Text
            Else
                objDispatch.SDM_BillingAddress = ""
            End If

            If txtDeliveryFromAddressCS.Text <> "" Then
                objDispatch.SDM_DeliveryFrom = txtDeliveryFromAddressCS.Text
            Else
                objDispatch.SDM_DeliveryFrom = ""
            End If

            If txtDeleveryAddressCS.Text <> "" Then
                objDispatch.SDM_DeliveryAddress = txtDeleveryAddressCS.Text
            Else
                objDispatch.SDM_DeliveryAddress = ""
            End If

            If txtCompanyGSTNRegNoCS.Text <> "" Then
                objDispatch.SDM_CompanyGSTNRegNo = txtCompanyGSTNRegNoCS.Text
            Else
                objDispatch.SDM_CompanyGSTNRegNo = ""
            End If

            If txtBillingGSTNRegNoCS.Text <> "" Then
                objDispatch.SDM_BillingGSTNRegNo = txtBillingGSTNRegNoCS.Text
            Else
                objDispatch.SDM_BillingGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNoCS.Text <> "" Then
                objDispatch.SDM_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNoCS.Text
            Else
                objDispatch.SDM_DeliveryFromGSTNRegNo = ""
            End If

            If txtDeliveryGSTNRegNoCS.Text <> "" Then
                objDispatch.SDM_DeliveryGSTNRegNo = txtDeliveryGSTNRegNoCS.Text
            Else
                objDispatch.SDM_DeliveryGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                objDispatch.SDM_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNoCS.Text), Trim(txtDeliveryGSTNRegNoCS.Text))
            ElseIf txtDeliveryFromGSTNRegNoCS.Text <> "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                objDispatch.SDM_DispatchStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text <> "" Then
                objDispatch.SDM_DispatchStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNoCS.Text = "" And txtDeliveryGSTNRegNoCS.Text = "" Then
                objDispatch.SDM_DispatchStatus = "Local"
            End If
            objDispatch.SDM_DispatchID = 0

            objDispatch.SDM_CompanyType = ddlCompanyTypeCS.SelectedValue
            objDispatch.SDM_GSTNCategory = ddlGSTCategoryCS.SelectedValue


            If txtOrderNoInvoice.Text <> "" Then
                objDispatch.SDM_OrderNo = txtOrderNoInvoice.Text
            Else
                objDispatch.SDM_OrderNo = ""
            End If

            If txtAllocationInvoice.Text <> "" Then
                objDispatch.SDM_AllocationNo = txtAllocationInvoice.Text
            Else
                objDispatch.SDM_AllocationNo = ""
            End If

            If txtDispatchNoInvoice.Text <> "" Then
                objDispatch.SDM_DispatchNo = txtDispatchNoInvoice.Text
            Else
                objDispatch.SDM_DispatchNo = ""
            End If

            If ddlBatchNo.SelectedIndex > 0 Then
                objDispatch.SDM_BatchNo = ddlBatchNo.SelectedValue
            Else
                objDispatch.SDM_BatchNo = 0
            End If
            objDispatch.SDM_BaseName = lstFiles.SelectedItem.Text 'BaseName

            Arr = objDispatch.SaveDispatchMaster(sSession.AccessCode, objDispatch)
            iMasterID = Arr(1)
            txtMasterID.Text = iMasterID

            If txtQuantityCashSales.Text <> "" Then

                'lblCommodityID = grdDispatchDetails.Rows(i).FindControl("lblCommodityID")
                'lblItemID = grdDispatchDetails.Rows(i).FindControl("lblItemID")
                'lblHistoryID = grdDispatchDetails.Rows(i).FindControl("lblHistoryID")
                'lblUnitID = grdDispatchDetails.Rows(i).FindControl("lblUnitID")
                'lblCommodity = grdDispatchDetails.Rows(i).FindControl("lblCommodity")
                'lblGoods = grdDispatchDetails.Rows(i).FindControl("lblGoods")
                'lblUnit = grdDispatchDetails.Rows(i).FindControl("lblUnit")
                'lblMRP = grdDispatchDetails.Rows(i).FindControl("lblMRP")
                'lblOrderedQty = grdDispatchDetails.Rows(i).FindControl("lblOrderedQty")
                'lblTotalAmount = grdDispatchDetails.Rows(i).FindControl("lblTotal")

                'HFDiscountAmount = grdDispatchDetails.Rows(i).FindControl("HFDiscountAmount")

                'HFNetAmount = grdDispatchDetails.Rows(i).FindControl("HFNetAmount")

                'ddlDiscount = grdDispatchDetails.Rows(i).FindControl("ddlDiscount")

                'HFCharges = grdDispatchDetails.Rows(i).FindControl("HFCharges")
                'HFAmount = grdDispatchDetails.Rows(i).FindControl("HFAmount")

                'lblGSTID = grdDispatchDetails.Rows(i).FindControl("lblGSTID")
                'lblGSTRate = grdDispatchDetails.Rows(i).FindControl("lblGSTRate")
                'HFGSTAmount = grdDispatchDetails.Rows(i).FindControl("HFGSTAmount")

                objDispatch.SDD_MasterID = iMasterID
                objDispatch.SDD_CommodityID = ddlCommodityCS.SelectedValue
                objDispatch.SDD_DescID = lstBoxDescriptionCS.SelectedValue
                objDispatch.SDD_HistoryID = 0
                objDispatch.SDD_UnitID = ddlUnitOfMeassurementCS.SelectedValue

                objDispatch.SDD_Rate = txtMRPCS.Text
                objDispatch.SDD_Quantity = txtQuantityCashSales.Text
                objDispatch.SDD_RateAmount = txtAmountCS.Text

                If ddlDiscountcs.SelectedIndex > 0 Then
                    objDispatch.SDD_Discount = ddlDiscountcs.SelectedItem.Text
                Else
                    objDispatch.SDD_Discount = 0
                End If
                If txtDiscountAmountCS.Text <> "" Then
                    objDispatch.SDD_DiscountAmount = txtDiscountAmountCS.Text
                Else
                    objDispatch.SDD_DiscountAmount = 0
                End If

                objDispatch.SDD_ChargesPeritem = 0
                objDispatch.SDD_Amount = ((objDispatch.SDD_RateAmount - objDispatch.SDD_DiscountAmount) + objDispatch.SDD_ChargesPeritem)

                objDispatch.SDD_GST_ID = 0
                If txtGSTCS.Text <> "" Then
                    objDispatch.SDD_GSTRate = txtGSTCS.Text
                Else
                    objDispatch.SDD_GSTRate = 0
                End If
                If txtGSTAmountCS.Text <> "" Then
                    objDispatch.SDD_GSTAmount = txtGSTAmountCS.Text
                Else
                    objDispatch.SDD_GSTAmount = 0
                End If

                objDispatch.SDD_VAT = 0
                objDispatch.SDD_VATAmount = 0
                objDispatch.SDD_CST = 0
                objDispatch.SDD_CSTAmount = 0
                objDispatch.SDD_Excise = 0
                objDispatch.SDD_ExciseAmount = 0

                If txtNetAmountCS.Text <> "" Then
                    objDispatch.SDD_TotalAmount = txtNetAmountCS.Text
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

                Arr = objDispatch.SaveDispatchDetails(sSession.AccessCode, objDispatch)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
            End If

            LoadExistingSalesInvoiceNo()
            ddlExistingSalesInvoiceNo.SelectedValue = iMasterID
            ClearCDIDetails()

            grdDispatchDetails.DataSource = objDispatch.BindDispatchedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            grdDispatchDetails.DataBind()

            'SaveCharges(iMasterID)
            lblStatusInvoice.Text = "Waiting For Approval"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveInvoice()")
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
    Private Sub grdDispatchDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDispatchDetails.RowCommand
        Dim iItemID As Integer, iCommodityID As Integer
        Dim lblCommodityID As New Label
        Dim lblItemID As New Label

        Try
            lblError.Text = ""
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)

                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)
                iCommodityID = lblCommodityID.Text
                iItemID = lblItemID.Text

                dt = objDispatch.BindInvoiceData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingDispatchNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        BindDescription(dt.Rows(i)("SDD_CommodityID"))
                        lstBoxDescriptionCS.SelectedValue = dt.Rows(i)("SDD_DescID")

                        txtQuantityCashSales.Text = dt.Rows(i)("SDD_Quantity")

                        txtMRPCS.Text = dt.Rows(i)("SDD_Rate")
                        hfMRP.Value = dt.Rows(i)("SDD_Rate")

                        txtAmountCS.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_RateAmount")))
                        hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_RateAmount")))

                        txtNetAmountCS.Text = 0
                        hfNetAmount.Value = 0

                        LoadDiscount()
                        If dt.Rows(i)("SDD_Discount") > 0 Then
                            ddlDiscountcs.SelectedValue = objSO.GetDiscountID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("DM_Discount"))
                        Else
                            ddlDiscountcs.SelectedIndex = 0
                        End If
                        txtDiscountAmountCS.Text = 0

                        txtGSTCS.Text = dt.Rows(i)("SDD_GSTRate")
                        txtGSTAmountCS.Text = dt.Rows(i)("SDD_GSTAmount")

                        BindCashSalesUnitOfMeassurement(lstBoxDescription.SelectedValue)
                        ddlUnitOfMeassurementCS.SelectedValue = dt.Rows(i)("SDD_UnitID")
                    Next
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdDispatchDetails_RowCommand")
        End Try
    End Sub
    Public Sub BindInvoice()
        Dim dt As New DataTable
        Dim dtCharge As New DataTable
        Try
            imgbtnAddCDI.ImageUrl = "~/Images/Update24.png"
            imgbtnAddCDI.ToolTip = "Update"
            dt = objDispatch.BindDispatchMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlBatchNo.SelectedValue, lstFiles.SelectedValue)
            If IsNothing(dt) = False Then
                If IsDBNull(dt.Rows(0)("SDM_ID")) = False Then
                    ddlExistingSalesInvoiceNo.SelectedValue = dt.Rows(0)("SDM_ID")
                Else
                    ddlExistingSalesInvoiceNo.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("SDM_DispatchNo")) = False Then
                    txtDispatchNoInvoice.Text = dt.Rows(0)("SDM_DispatchNo")
                Else
                    txtDispatchNoInvoice.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_OrderNo")) = False Then
                    txtOrderNoInvoice.Text = dt.Rows(0)("SDM_OrderNo")
                Else
                    txtOrderNoInvoice.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_AllocationNo")) = False Then
                    txtAllocationInvoice.Text = dt.Rows(0)("SDM_AllocationNo")
                Else
                    txtAllocationInvoice.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_Code")) = False Then
                    txtsaleInvoiceNo.Text = dt.Rows(0)("SDM_Code")
                Else
                    txtsaleInvoiceNo.Text = ""
                End If
                txtOrderDateInvoice.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SDM_OrderDate"), "D")
                txtDispatchDateInvoice.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SDM_DispatchDate"), "D")
                ddlPatryCS.SelectedValue = dt.Rows(0)("SDM_SupplierID")
                txtCode.Value = objDispatch.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlPatryCS.SelectedValue)

                ddlCompanyTypeCS.SelectedValue = dt.Rows(0)("SDM_CompanyType")
                BindCashSalesGSTNCategory(ddlCompanyTypeCS.SelectedItem.Text)
                ddlGSTCategoryCS.SelectedValue = dt.Rows(0)("SDM_GSTNCategory")

                If IsDBNull(dt.Rows(0)("SDM_ModeOfShipping")) = False Then
                    If dt.Rows(0)("SDM_ModeOfShipping") > 0 Then
                        ddlShippingCS.SelectedValue = dt.Rows(0)("SDM_ModeOfShipping")
                    Else
                        ddlShippingCS.SelectedIndex = 0
                    End If
                Else
                    ddlShippingCS.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("SDM_SalesManID")) = False Then
                    If dt.Rows(0)("SDM_SalesManID") > 0 Then
                        ddlSalesManCS.SelectedValue = dt.Rows(0)("SDM_SalesManID")
                    Else
                        ddlSalesManCS.SelectedIndex = 0
                    End If
                Else
                    ddlSalesManCS.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("SDM_DispatchRefNo")) = False Then
                    txtSalesInvoiceRefNo.Text = dt.Rows(0)("SDM_DispatchRefNo")
                Else
                    txtSalesInvoiceRefNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_ESugamNo")) = False Then
                    txtESugamNo.Text = dt.Rows(0)("SDM_ESugamNo")
                Else
                    txtESugamNo.Text = ""
                End If

                ddlPaymentTypeCS.SelectedValue = dt.Rows(0)("SDM_PaymentType")
                'If UCase(ddlPaymentTypeCS.SelectedItem.Text) = UCase("Cheque") Then
                '    divcollapseChequeDetails.Visible = True
                '    GetBankDDL()
                '    txtChequeNo.Text = dt.Rows(0)("SDM_ChequeNo") : txtChequeDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SDM_ChequeDate"), "D")
                '    txtIFSCCode.Text = dt.Rows(0)("SDM_IFSCCode")
                '    If IsDBNull(dt.Rows(0)("SDM_BankName")) = False Then
                '        If dt.Rows(0)("SDM_BankName") > 0 Then
                '            ddlBankName.SelectedValue = dt.Rows(0)("SDM_BankName")
                '        Else
                '            ddlBankName.SelectedIndex = 0
                '        End If
                '    Else
                '        ddlBankName.SelectedIndex = 0
                '    End If
                '    txtBranch.Text = dt.Rows(0)("SDM_Branch")
                'Else
                '    divcollapseChequeDetails.Visible = False
                'End If


                If IsDBNull(dt.Rows(0)("SDM_Status")) = False Then
                    If dt.Rows(0)("SDM_Status") = "A" Then
                        lblStatusInvoice.Text = "Approved"
                    Else
                        lblStatusInvoice.Text = "Waiting For Approval"
                    End If
                Else
                    lblStatusInvoice.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_CompanyAddress")) = False Then
                    txtCompanyAddressCS.Text = dt.Rows(0)("SDM_CompanyAddress")
                Else
                    txtCompanyAddressCS.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_CompanyGSTNRegNo")) = False Then
                    txtCompanyGSTNRegNoCS.Text = dt.Rows(0)("SDM_CompanyGSTNRegNo")
                Else
                    txtCompanyGSTNRegNoCS.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_BillingAddress")) = False Then
                    txtBillingAddressCS.Text = dt.Rows(0)("SDM_BillingAddress")
                Else
                    txtBillingAddressCS.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_BillingGSTNRegNo")) = False Then
                    txtBillingGSTNRegNoCS.Text = dt.Rows(0)("SDM_BillingGSTNRegNo")
                Else
                    txtBillingGSTNRegNoCS.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_DeliveryFrom")) = False Then
                    txtDeliveryFromAddressCS.Text = dt.Rows(0)("SDM_DeliveryFrom")
                Else
                    txtDeliveryFromAddressCS.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_DeliveryFromGSTNRegNo")) = False Then
                    txtDeliveryFromGSTNRegNoCS.Text = dt.Rows(0)("SDM_DeliveryFromGSTNRegNo")
                Else
                    txtDeliveryFromGSTNRegNoCS.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_DeliveryAddress")) = False Then
                    txtDeleveryAddressCS.Text = dt.Rows(0)("SDM_DeliveryAddress")
                Else
                    txtDeleveryAddressCS.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_DeliveryGSTNRegNo")) = False Then
                    txtDeliveryGSTNRegNoCS.Text = dt.Rows(0)("SDM_DeliveryGSTNRegNo")
                Else
                    txtDeliveryGSTNRegNoCS.Text = ""
                End If

                grdDispatchDetails.DataSource = objDispatch.BindDispatchedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingSalesInvoiceNo.SelectedValue)
                grdDispatchDetails.DataBind()

                'dtCharge = objDispatch.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, 0, 0)
                'GvCharge.DataSource = dtCharge
                'GvCharge.DataBind()
                'Session("ChargesMaster") = dtCharge

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lstBoxDescriptionSR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxDescriptionSR.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Dim sStockHistoryID As String = ""
        Dim dOrderDate As Date
        Dim dDate, dSDate As Date : Dim m As Integer
        Try

            lblError.Text = ""
            txtReturnQuantitySR.Text = "" : txtAmountSR.Text = "" : txtTotalAmountSR.Text = ""

            If ddlCustomerSR.SelectedIndex > 0 Then
                If lstBoxDescriptionSR.SelectedIndex <> -1 Then
                    ddlCommoditySR.SelectedValue = objSO.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescriptionSR.SelectedValue)
                    If txtReturnDateSR.Text <> "" Then
                        'Cheque Date Comparision'
                        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        dSDate = Date.ParseExact(txtReturnDateSR.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        m = DateDiff(DateInterval.Day, dDate, dSDate)
                        If m < 0 Then
                            lblError.Text = "Return Date (" & txtReturnDateSR.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                            lblValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                            txtReturnDateSR.Focus()
                            Exit Sub
                        End If

                        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        dSDate = Date.ParseExact(txtReturnDateSR.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        m = DateDiff(DateInterval.Day, dDate, dSDate)
                        If m > 0 Then
                            lblError.Text = "Return Date (" & txtReturnDateSR.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                            lblValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                            txtReturnDateSR.Focus()
                            Exit Sub
                        End If
                        'Cheque Date Comparision'
                    End If
                    BindSalesReturnUni()

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstBoxDescriptionCS_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindSalesReturnUni()
        Try
            ddlUnitSR.DataSource = objSR.LoadDescription(sSession.AccessCode, sSession.AccessCodeID)
            ddlUnitSR.DataTextField = "Mas_Desc"
            ddlUnitSR.DataValueField = "Mas_ID"
            ddlUnitSR.DataBind()
            ddlUnitSR.Items.Insert(0, "Select Unit Of Meassurement")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnSaveSR_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveSR.Click
        Dim Arr() As String, Arr1() As String
        Dim iMasterID As Integer = 0, iHistoryID As Integer = 0
        Dim sDispatchStatus As String = "", sState As String = ""
        Try
            lblError.Text = ""
            If ddlCommoditySR.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Commodity." : lblError.Text = "Select Commodity."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                ddlCommoditySR.Focus()
                Exit Sub
            End If
            If ddlReturn.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Reason For Return." : lblError.Text = "Select Reason For Return."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                ddlReturn.Focus()
                Exit Sub
            End If
            'If txtCharges.Text.Trim() = "" Then
            '    lblSalesValidationMsg.Text = "Enter Charges." : lblError.Text = "Enter Charges."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
            '    txtCharges.Focus()
            '    Exit Sub
            'End If
            If txtRemarksSR.Text.Length > 2000 Then
                lblValidationMsg.Text = "Remarks exceeded maximum size(max 2000 characters)." : lblError.Text = "Remarks exceeded maximum size(max 2000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                txtRemarksSR.Focus()
                Exit Sub
            End If
            If ddlExistSalesReturn.SelectedIndex > 0 Then
                objSR.Sales_Return_ID = ddlExistSalesReturn.SelectedValue
            Else
                objSR.Sales_Return_ID = 0
            End If
            objSR.Sales_Return_Year = sSession.YearID
            objSR.Sales_Return_ReturnNo = txtReturnNoSR.Text
            objSR.Sales_Return_RetrunDate = Date.ParseExact(txtReturnDateSR.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSR.Sales_Return_InvoiceNo = 0
            objSR.Sales_Return_InvoiceDate = Date.ParseExact(txtInvoiceDateSR.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSR.Sales_Return_OrderNo = 0
            objSR.Sales_Return_DispatchNo = 0
            objSR.Sales_Return_Customer = ddlCustomerSR.SelectedValue
            objSR.Sales_Return_ShipTo = objGen.SafeSQL(txtShipTo.Text.Trim())
            objSR.Sales_Return_CreatedBy = sSession.UserID
            objSR.Sales_Return_UpdatedBy = sSession.UserID
            objSR.Sales_Return_IPAddress = sSession.IPAddress
            objSR.Sales_Return_CompID = sSession.AccessCodeID
            'sDispatchStatus = objSR.GetStateStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDispatchNo.SelectedValue, "D")
            'sState = objSR.GetStateStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDispatchNo.SelectedValue, "S")
            objSR.Sales_Return_DispatchStatus = sDispatchStatus
            objSR.Sales_Return_State = sState

            objSR.Sales_Return_GoodsReturnNo = txtGoodsReturnRefNo.Text

            objSR.Sales_Return_Order = txtOrderNoSR.Text
            objSR.Sales_Return_Dispatch = txtDispatchNoSR.Text
            objSR.Sales_Return_Invoice = txtInvoiceNoSR.Text

            If ddlBatchNo.SelectedIndex > 0 Then
                objSR.Sales_Return_BatchNo = ddlBatchNo.SelectedValue
            Else
                objSR.Sales_Return_BatchNo = 0
            End If
            objSR.Sales_Return_BaseName = lstFiles.SelectedItem.Text    'BaseName

            Arr = objSR.SaveSalesReturnMaster(sSession.AccessCode, sSession.AccessCodeID, objSR)
            iMasterID = Arr(1)
            'If lblID.Text <> "" Then
            '    objSR.SRD_ID = lblID.Text
            'Else
            objSR.SRD_ID = 0
            'End If
            objSR.SRD_MasterID = iMasterID
            objSR.SRD_Commodity = ddlCommoditySR.SelectedValue
            objSR.SRD_Item = lstBoxDescriptionSR.SelectedValue
            objSR.SRD_UnitID = ddlUnitSR.SelectedValue
            iHistoryID = 0
            objSR.SRD_HistoryID = iHistoryID
            objSR.SRD_Rate = txtRateSR.Text.Trim
            objSR.SRD_Quantity = txtReturnQuantitySR.Text.Trim
            objSR.SRD_RateAmount = txtAmountSR.Text.Trim
            If ddlDiscountSR.SelectedIndex > 0 Then
                objSR.SRD_Discount = ddlDiscountSR.SelectedValue
                objSR.SRD_DiscountAmount = txtDiscountAmountSR.Text
            Else
                objSR.SRD_Discount = 0
                objSR.SRD_DiscountAmount = 0
            End If

            objSR.SRD_TotalAmount = txtTotalAmountSR.Text

            'objSR.SRD_Amount = (txtAmount.Text.Trim - objSR.SRD_DiscountAmount) + txtCharges.Text.Trim
            objSR.SRD_Amount = (txtAmountSR.Text.Trim - objSR.SRD_DiscountAmount)
            objSR.SRD_Reason = ddlReturn.SelectedValue

            'If txtCharges.Text <> "" Then
            '    objSR.SRD_Charges = txtCharges.Text.Trim
            'Else
            '    objSR.SRD_Charges = 0
            'End If
            objSR.SRD_Charges = 0

            objSR.SRD_GST_ID = 0

            If txtGSTRateSR.Text <> "" Then
                objSR.SRD_GSTRate = txtGSTRateSR.Text
            Else
                objSR.SRD_GSTRate = 0
            End If
            If txtGSTAmountSR.Text <> "" Then
                objSR.SRD_GSTAmount = txtGSTAmountSR.Text
            Else
                objSR.SRD_GSTAmount = 0
            End If

            If objSR.Sales_Return_DispatchStatus = "Local" Then
                objSR.SRD_SGST = objSR.SRD_GSTRate / 2
                objSR.SRD_SGSTAmount = objSR.SRD_GSTAmount / 2
                objSR.SRD_CGST = objSR.SRD_GSTRate / 2
                objSR.SRD_CGSTAmount = objSR.SRD_GSTAmount / 2
                objSR.SRD_IGST = 0
                objSR.SRD_IGSTAmount = 0
            ElseIf objSR.Sales_Return_DispatchStatus = "Inter State" Then
                objSR.SRD_SGST = 0
                objSR.SRD_SGSTAmount = 0
                objSR.SRD_CGST = 0
                objSR.SRD_CGSTAmount = 0
                objSR.SRD_IGST = objSR.SRD_GSTRate
                objSR.SRD_IGSTAmount = objSR.SRD_GSTAmount
            End If
            objSR.SRD_Remarks = objGen.SafeSQL(txtRemarksSR.Text)
            objSR.SRD_IPAddress = sSession.IPAddress
            objSR.SRD_CompID = sSession.AccessCodeID
            Arr1 = objSR.SaveSalesReturnDetails(sSession.AccessCode, sSession.AccessCodeID, objSR)
            If Arr1(0) = 2 Then
                lblValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            Else
                lblValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            End If
            SaveCharges(iMasterID)

            LoadExistingSalesReturn()
            ddlExistSalesReturn.SelectedValue = iMasterID

            dgSRItemDetails.DataSource = objSR.LoadSalesReturn(sSession.AccessCode, sSession.AccessCodeID, iMasterID)
            dgSRItemDetails.DataBind()
            clearSalesReturnDetails()
            lblStatusSR.Text = "Waiting For Approval"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveSR_Click")
        End Try
    End Sub
    Private Sub imgbtnRefreshSR_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefreshSR.Click
        Try
            clearSalesReturn()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgSRItemDetails_PreRender(sender As Object, e As EventArgs) Handles dgSRItemDetails.PreRender
        Dim dt As New DataTable
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
End Class
