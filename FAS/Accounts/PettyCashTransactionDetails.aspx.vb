Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.Object
Partial Class Accounts_PettyCashTransactionDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PettyCashTransactionDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Dim objPC As New clsPettyCash
    Dim dtMerge As New DataTable
    Private Shared iDbOrCr As Integer = 0
    Dim objCOA As New clsChartOfAccounts

    Private objclsAttachments As New clsAttachments
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Private Shared dtAttach As New DataTable
    Private Shared sWFDelete As String
    Private Shared sINWView As String
    Private Shared sINWDownload As String
    Private Shared iInwardID As Integer
    Private objclsModulePermission As New clsModulePermission
    Private Shared iStatus As Integer
    Private objAccSetting As New clsAccountSetting
    Private objIndex As New clsIndexing
    Dim dt As New DataTable
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgCredit.ImageUrl = "~/Images/Add16.png"
        imgDebit.ImageUrl = "~/Images/Add16.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnAddBillAmt.ImageUrl = "~/Images/Add16.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"


        imgbtnDrOtherGL.ImageUrl = "~/Images/Add16.png"
        imgbtnDrOtherSubGL.ImageUrl = "~/Images/Add16.png"
        imgbtnCrOtherGL.ImageUrl = "~/Images/Add16.png"
        imgbtnCrOtherSGL.ImageUrl = "~/Images/Add16.png"
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

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PCT")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnAddBillAmt.Visible = False : imgDebit.Visible = False : imgCredit.Visible = False
                btnAddAttch.Visible = False : btnScan.Visible = False
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
                        imgbtnAddBillAmt.Visible = True : imgDebit.Visible = True : imgCredit.Visible = True
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

                Session("Petty") = Nothing
                dtMerge = Nothing
                Session("datatable") = Nothing
                'imgbtnAdd.Visible = False : imgbtnSave.Visible = False :
                imgbtnUpdate.Visible = False
                'imgbtnAdd.Visible = True : imgbtnSave.Visible = True
                LoadExistingPC() : BinPartyOrCustomerORGL() : LoadBillType()
                LoadSubGL()
                txtTransactionNo.Text = objPC.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                lblStatus.Text = "Not Started"
                ddlCrHead.SelectedIndex = 1
                ddlCrHead_SelectedIndexChanged(sender, e)

                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)

                iDefaultBranch = objPC.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                'RFVdbGL.InitialValue = "Select GL Code" : RFVdbGL.ErrorMessage = "Select General Ledger."
                'RFVCrGL.InitialValue = "Select GL Code" : RFVCrGL.ErrorMessage = "Select General Ledger."
                RFVParty.InitialValue = "Select Customer/Supplier/Party" : RFVParty.ErrorMessage = "Select Customer/Supplier/Party."
                'RFVBillType.InitialValue = "Select Voucher Type" : RFVBillType.ErrorMessage = "Select Voucher Type."
                'RFVBillNo.ErrorMessage = "Enter Valid Bill Number."

                REFBillDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REFBillDate.ErrorMessage = "Enter Valid Date Format."

                RFVEBillAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVEBillAmount.ErrorMessage = "Enter Valid Bill Amount."

                RFVInvoiceDate.ErrorMessage = "Enter Invoice Date."
                REVInvoiceDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REVInvoiceDate.ErrorMessage = "Enter Valid Date Format."

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
                    ddlExisting.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExisting_SelectedIndexChanged(sender, e)
                    divcollapseAttachments.Visible = True
                    BindAllAttachments(sSession.AccessCode, iAttachID)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
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
    Private Sub LoadExistingPC()
        Try
            ddlExisting.DataSource = objPC.LoadExistingPettyCashVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            ddlExisting.DataTextField = "Acc_PCM_TransactionNo"
            ddlExisting.DataValueField = "Acc_PCM_ID"
            ddlExisting.DataBind()
            ddlExisting.Items.Insert(0, "Existing Petty Cash Voucher")

        Catch ex As Exception
            Throw
        End Try
    End Sub
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
    Private Sub LoadSubGL()
        Try
            ddldbsUbGL.DataSource = objPC.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddldbsUbGL.DataTextField = "GlDesc"
            ddldbsUbGL.DataValueField = "gl_Id"
            ddldbsUbGL.DataBind()
            ddldbsUbGL.Items.Insert(0, "Select SubGL Code")
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
                ddlParty.DataSource = objPC.LoadAllGLCodes(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "GlDesc"
                ddlParty.DataValueField = "gl_Id"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Party")
            ElseIf iType = 1 Then
                ddlParty.DataSource = objPC.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "BM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer")
            ElseIf iType = 2 Then
                ddlParty.DataSource = objPC.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "CSM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Supplier")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExisting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExisting.SelectedIndexChanged
        Try
            imgView.ImageUrl = "~/Images/NoImage.jpg"
            If ddlExisting.SelectedIndex > 0 Then
                BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExisting.SelectedValue)
                ddlParty_SelectedIndexChanged(sender, e)
                ddlCrHead.SelectedIndex = 1
                ddlCrHead_SelectedIndexChanged(sender, e)
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
            Else
                imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExisting_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPayment As Integer)
        Dim dt As New DataTable, dtTrans As New DataTable
        Dim iAdvanceAmount As Integer = 0
        Dim dtPetty As New DataTable
        Try
            dt = objPC.GetPaymentDetails(sNameSpace, iCompID, iPayment)
            If dt.Rows.Count > 0 Then

                If IsDBNull(dt.Rows(0)("Acc_PCM_AttachID")) = False Then
                    iAttachID = dt.Rows(0)("Acc_PCM_AttachID").ToString()
                Else
                    iAttachID = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_TransactionNo").ToString()) = False Then
                    txtTransactionNo.Text = dt.Rows(0)("Acc_PCM_TransactionNo").ToString()
                Else
                    txtTransactionNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_Location").ToString()) = False Then
                    ddlCustomerParty.SelectedIndex = dt.Rows(0)("Acc_PCM_Location").ToString()
                Else
                    ddlCustomerParty.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_PCM_Party").ToString()) = False Then
                    If ddlCustomerParty.SelectedIndex = 1 Then
                        LoadParty(1)
                    ElseIf ddlCustomerParty.SelectedIndex = 2 Then
                        LoadParty(2)
                    ElseIf ddlCustomerParty.SelectedIndex = 3 Then
                        LoadParty(3)
                    End If
                    ddlParty.SelectedValue = dt.Rows(0)("Acc_PCM_Party").ToString()
                Else
                    ddlParty.SelectedIndex = 0
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
                    lblStatus.Text = "Waiting for Approval"
                ElseIf dt.Rows(0)("Acc_PCM_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                ElseIf dt.Rows(0)("Acc_PCM_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
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

                '*** Preethika ***'
                If IsDBNull(dt.Rows(0)("ACC_PCM_ZoneID").ToString()) = False Then
                    If (dt.Rows(0)("ACC_PCM_ZoneID").ToString() = "") Then
                    Else
                        ddlAccZone.SelectedValue = dt.Rows(0)("ACC_PCM_ZoneID").ToString()
                        LoadRegion(ddlAccZone.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("ACC_PCM_RegionID").ToString()) = False Then
                    If (dt.Rows(0)("ACC_PCM_RegionID").ToString() = "") Then
                    Else
                        ddlAccRgn.SelectedValue = dt.Rows(0)("ACC_PCM_RegionID").ToString()
                        LoadArea(ddlAccRgn.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("ACC_PCM_AreaID").ToString()) = False Then
                    If (dt.Rows(0)("ACC_PCM_AreaID").ToString() = "") Then
                    Else
                        ddlAccArea.SelectedValue = dt.Rows(0)("ACC_PCM_AreaID").ToString()
                        LoadAccBrnch(ddlAccArea.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("ACC_PCM_BranchID").ToString()) = False Then
                    If (dt.Rows(0)("ACC_PCM_BranchID").ToString() = "") Then
                    Else
                        ddlAccBrnch.SelectedValue = dt.Rows(0)("ACC_PCM_BranchID").ToString()
                    End If
                End If
                '*** Preethika ***'

            End If

            ddlCrHead.SelectedIndex = 0
            'ddlCrGL.SelectedIndex = 0

            dtTrans = objPC.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPayment)
            dgPettyCashDetails.DataSource = dtTrans
            dgPettyCashDetails.DataBind()
            Session("DataTable") = dtTrans

            BindAllAttachments(sSession.AccessCode, iAttachID)

            dtPetty = objPC.BindPettyDetails(sSession.AccessCode, sSession.AccessCodeID, iPayment)
            If dtPetty.Rows.Count > 0 Then
                dgPetty.DataSource = dtPetty
                dgPetty.DataBind()
                Session("Petty") = dtPetty
            Else
                dgPetty.DataSource = Nothing
                dgPetty.DataBind()
            End If

            GetAttachFile(ddlExisting.SelectedItem.Text)
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

            dt1 = objPC.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
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
    Private Sub ddldbHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbHead.SelectedIndexChanged
        Try
            If ddldbHead.SelectedIndex > 0 Then
                ddldbGL.DataSource = objPC.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbHead.SelectedIndex)
                ddldbGL.DataTextField = "GlDesc"
                ddldbGL.DataValueField = "gl_Id"
                ddldbGL.DataBind()
                ddldbGL.Items.Insert(0, "Select GL Code")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddldbGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbGL.SelectedIndexChanged
        Try
            If ddldbGL.SelectedIndex > 0 Then
                ddldbsUbGL.DataSource = objPC.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbGL.SelectedValue)
                ddldbsUbGL.DataTextField = "GlDesc"
                ddldbsUbGL.DataValueField = "gl_Id"
                ddldbsUbGL.DataBind()
                ddldbsUbGL.Items.Insert(0, "Select SubGL Code")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCrHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrHead.SelectedIndexChanged
        Try
            ddlCrGL.Items.Clear()
            If ddlCrHead.SelectedIndex > 0 Then
                ddlCrGL.DataSource = objPC.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedIndex)
                ddlCrGL.DataTextField = "GlDesc"
                ddlCrGL.DataValueField = "gl_Id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL Code")
                If ddlCrHead.SelectedIndex = 1 Then
                    ddlCrGL.SelectedValue = objPC.LoadAssetGL(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedIndex)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrHead_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCrGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrGL.SelectedIndexChanged
        Try
            If ddlCrGL.SelectedIndex > 0 Then
                ddlCrSubGL.DataSource = objPC.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue)
                ddlCrSubGL.DataTextField = "GlDesc"
                ddlCrSubGL.DataValueField = "gl_Id"
                ddlCrSubGL.DataBind()
                ddlCrSubGL.Items.Insert(0, "Select SubGL Code")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddldbsUbGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbsUbGL.SelectedIndexChanged
        Dim iHead As Integer
        Try
            If ddldbsUbGL.SelectedIndex > 0 Then
                iHead = objPC.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddldbsUbGL.SelectedValue)
                ddldbGL.DataSource = objPC.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead)
                ddldbGL.DataTextField = "GlDesc"
                ddldbGL.DataValueField = "gl_Id"
                ddldbGL.DataBind()
                ddldbGL.Items.Insert(0, "Select GL Code")

                ddldbGL.SelectedValue = objPC.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddldbsUbGL.SelectedValue)
                ddldbHead.SelectedIndex = iHead
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbsUbGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCrSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrSubGL.SelectedIndexChanged
        Dim iHead As Integer
        Try
            If ddlCrSubGL.SelectedIndex > 0 Then
                iHead = objPC.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlCrSubGL.SelectedValue)
                ddlCrGL.DataSource = objPC.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead)
                ddlCrGL.DataTextField = "GlDesc"
                ddlCrGL.DataValueField = "gl_Id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL Code")

                ddlCrGL.SelectedValue = objPC.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddlCrSubGL.SelectedValue)
                ddlCrHead.SelectedIndex = iHead
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrSubGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Function CheckDebitAndCredit() As Integer
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
            ElseIf dDebit <> txtBillAmount.Text.Trim Then
                Return 2
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim objPett As New clsPettyCash.Petty
        Dim dDate, dSDate As Date : Dim m As Integer
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
                lblError.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
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
                    lblError.Text = "BillDate (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtBillDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "BillDate (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtBillDate.Focus()
                    Exit Sub
                End If
            End If
            'Cheque Date Comparision'

            If ddldbsUbGL.Items.Count > 1 Then
                If ddldbsUbGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Debit."
                    Exit Sub
                End If
            End If

            If ddlCrSubGL.Items.Count > 1 Then
                If ddlCrSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Credit."
                    Exit Sub
                End If
            End If

            iRet = CheckDebitAndCredit()

            If iRet = 1 Then
                lblError.Text = "Debit Amount and Credit Amount Not Matched."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit Amount and Credit Amount Not Matched.','', 'info');", True)
                Exit Sub
            ElseIf iRet = 2 Then
                lblError.Text = "Amount not Matched with Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not Matched with Bill Amount.','', 'info');", True)
                Exit Sub
            End If

            If ddlExisting.SelectedIndex > 0 Then
                objPC.iAcc_PCM_ID = ddlExisting.SelectedValue
            Else
                objPC.iAcc_PCM_ID = 0
            End If

            objPC.sAcc_PCM_TransactionNo = txtTransactionNo.Text

            If ddlCustomerParty.SelectedIndex > 0 Then
                objPC.iAcc_PCM_Location = ddlCustomerParty.SelectedIndex
            Else
                objPC.iAcc_PCM_Location = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                objPC.iAcc_PCM_Party = ddlParty.SelectedValue
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

            objPC.iAcc_PCM_BatchNo = 0
            objPC.iAcc_PCM_BaseName = 0

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
            'Multiple BillNo Saving Option'

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
            End If

            For i = 0 To dgPettyCashDetails.Items.Count - 1
                objPC.iATD_TrType = 2
                objPC.dATD_TransactionDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objPC.iATD_BillId = iTransID
                objPC.iATD_PaymentType = 0
                'objPC.dATD_TransactionDate = Date.Today

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
                objPC.sATD_Status = "W"
                objPC.iATD_YearID = sSession.YearID
                objPC.sATD_Operation = "C"
                objPC.sATD_IPAddress = sSession.IPAddress

                objPC.iATD_ZoneID = ddlAccZone.SelectedValue
                objPC.iATD_RegionID = ddlAccRgn.SelectedValue
                objPC.iATD_AreaID = ddlAccArea.SelectedValue
                objPC.iATD_BranchID = ddlAccBrnch.SelectedValue

                objPC.dATD_OpenDebit = "0.00"
                objPC.dATD_OpenCredit = "0.00"
                objPC.dATD_ClosingDebit = "0.00"
                objPC.dATD_ClosingCredit = "0.00"
                objPC.iATD_SeqReferenceNum = 0

                objPC.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPC)
            Next

            dgPettyCashDetails.DataSource = objPC.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID)
            dgPettyCashDetails.DataBind()
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True

            lblStatus.Text = "Waiting for Approval"
            LoadExistingPC()
            ddlExisting.SelectedValue = iTransID

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim iPaymentType As Integer = 0 : Dim iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim Arr() As String
        Dim dtSes As New DataTable
        Dim iRet As Integer = 0
        Dim objPett As New clsPettyCash.Petty
        Dim dDate, dSDate As Date : Dim m As Integer

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
                lblError.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
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
                    lblError.Text = "BillDate (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtBillDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "BillDate (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtBillDate.Focus()
                    Exit Sub
                End If
            End If
            'Cheque Date Comparision'

            If ddldbsUbGL.Items.Count > 1 Then
                If ddldbsUbGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Debit."
                    Exit Sub
                End If
            End If

            If ddlCrSubGL.Items.Count > 1 Then
                If ddlCrSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Credit."
                    Exit Sub
                End If
            End If

            iRet = CheckDebitAndCredit()

            If iRet = 1 Then
                lblError.Text = "Debit Amount and Credit Amount Not Matched."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit Amount and Credit Amount Not Matched.','', 'info');", True)
                Exit Sub
            ElseIf iRet = 2 Then
                lblError.Text = "Amount not Matched with Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not Matched with Bill Amount.','', 'info');", True)
                Exit Sub
            End If

            objPC.iAcc_PCM_ID = ddlExisting.SelectedValue
            objPC.sAcc_PCM_TransactionNo = txtTransactionNo.Text

            If ddlCustomerParty.SelectedIndex > 0 Then
                objPC.iAcc_PCM_Location = ddlCustomerParty.SelectedIndex
            Else
                objPC.iAcc_PCM_Location = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                objPC.iAcc_PCM_Party = ddlParty.SelectedValue
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
            objPC.sAcc_PCM_Operation = "U"
            objPC.sAcc_PCM_IPAddress = sSession.IPAddress
            objPC.dAcc_PCM_InvoiceDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            objPC.iAcc_PCM_AttachID = iAttachID

            objPC.iACC_PCM_ZoneID = ddlAccZone.SelectedValue
            objPC.iACC_PCM_RegionID = ddlAccRgn.SelectedValue
            objPC.iACC_PCM_AreaID = ddlAccArea.SelectedValue
            objPC.iACC_PCM_BranchID = ddlAccBrnch.SelectedValue

            objPC.iAcc_PCM_BatchNo = 0
            objPC.iAcc_PCM_BaseName = 0

            Arr = objPC.SavePettyCashDetails(sSession.AccessCode, sSession.AccessCodeID, objPC)
            iTransID = Arr(1)

            'Multiple BillNo Saving Option'

            objPC.DeleteDetails(sSession.AccessCode, sSession.AccessCodeID, iTransID)

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
            'Multiple BillNo Saving Option'

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
            End If

            objPC.DeleteExePCTransDetails(sSession.AccessCode, sSession.AccessCodeID, iTransID)

            For i = 0 To dgPettyCashDetails.Items.Count - 1
                objPC.iATD_TrType = 2
                objPC.iATD_BillId = iTransID
                objPC.iATD_PaymentType = 0
                objPC.dATD_TransactionDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

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
                objPC.sATD_Status = "W"
                objPC.iATD_YearID = sSession.YearID
                objPC.sATD_Operation = "U"
                objPC.sATD_IPAddress = sSession.IPAddress

                objPC.iATD_ZoneID = ddlAccZone.SelectedValue
                objPC.iATD_RegionID = ddlAccRgn.SelectedValue
                objPC.iATD_AreaID = ddlAccArea.SelectedValue
                objPC.iATD_BranchID = ddlAccBrnch.SelectedValue

                objPC.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPC)
            Next
            dtSes = objPC.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExisting.SelectedValue)
            dgPettyCashDetails.DataSource = dtSes
            dgPettyCashDetails.DataBind()

            Session("Datatable") = dtSes
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
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
            ElseIf lblStatus.Text = "Not Started" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            End If
            Response.Redirect(String.Format("~/Accounts/PettyCashTransaction.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim dtNew As New DataTable
        Try
            lblError.Text = ""
            dtMerge = dtNew : Session("Datatable") = Nothing
            txtTransactionNo.Text = objPC.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExisting.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.SelectedIndex = 0 : ddlBillType.SelectedIndex = 0
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = "" : ddldbHead.SelectedIndex = 0 : ddlCrHead.SelectedIndex = 0
            lblStatus.Text = "" : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            dgPettyCashDetails.DataSource = dtNew
            dgPettyCashDetails.DataBind()
            LoadSubGL()
            ddldbGL.DataSource = dtNew
            ddldbGL.DataBind()

            ddlCrGL.DataSource = dtNew
            ddlCrGL.DataBind()

            Session("Petty") = Nothing
            dgPetty.DataSource = Nothing
            dgPetty.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgDebit_Click(sender As Object, e As ImageClickEventArgs) Handles imgDebit.Click
        Dim dt As New DataTable, dtsample As New DataTable
        Dim dRow As DataRow
        Dim sArray As Array
        Dim i As Integer = 0
        Dim dDebitAmt As Double = 0.0 : Dim dCreditAmt As Double = 0.0 : Dim dCreditTotalAmt As Double = 0.0
        Try
            lblError.Text = ""
            If ddldbHead.SelectedIndex = 0 Then
                lblError.Text = "Select Head of Accounts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Head of Accounts.','', 'info');", True)
                ddldbHead.Focus()
                Exit Sub
            End If
            If ddldbGL.SelectedIndex = 0 Then
                lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select General Ledger.','', 'info');", True)
                ddldbGL.Focus()
                Exit Sub
            End If
            If txtDebitAmount.Text = "" Then
                lblError.Text = "Enter Debit Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Debit Amount.','', 'info');", True)
                txtDebitAmount.Focus()
                Exit Sub
            End If

            If ddldbsUbGL.Items.Count > 1 Then
                If ddldbsUbGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Debit."
                    Exit Sub
                End If
            End If

            If ddlCrSubGL.Items.Count > 1 Then
                If ddlCrSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Credit."
                    Exit Sub
                End If
            End If

            dtMerge = Session("DataTable")
            If IsNothing(dtMerge) = True Then
                dtMerge = dt
            End If

            If dtMerge.Rows.Count > 0 Then
                Dim dtview As New DataView(dtMerge)
                sArray = ddldbGL.SelectedItem.Text.Split("-")
                dtview.RowFilter = "GLCode='" & sArray(0) & "' And GLDescription='" & sArray(1) & "'"
                dtview.Sort = "GLCode ASC"
                dtsample = dtview.ToTable

                If dtsample.Rows.Count > 0 Then
                    lblError.Text = "This combination already exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This combination already exists.','', 'info');", True)
                    txtCreditAmount.Focus()
                    Exit Sub
                End If
            End If

            dt.Columns.Add("ID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")

            dRow = dt.NewRow

            dRow("ID") = dtMerge.Rows.Count + 1

            If ddldbHead.SelectedIndex > 0 Then
                dRow("HeadID") = ddldbHead.SelectedIndex
            End If

            If ddldbGL.SelectedIndex > 0 Then
                dRow("GLID") = ddldbGL.SelectedValue
            End If

            If ddldbsUbGL.SelectedIndex > 0 Then
                dRow("SubGLID") = ddldbsUbGL.SelectedValue
            End If

            dRow("SrNo") = dtMerge.Rows.Count + 1

            If ddldbGL.SelectedIndex > 0 Then
                sArray = ddldbGL.SelectedItem.Text.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            If ddldbsUbGL.SelectedIndex > 0 Then
                sArray = ddldbsUbGL.SelectedItem.Text.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If

            If txtDebitAmount.Text <> "" Then
                dRow("Debit") = txtDebitAmount.Text
            End If
            dt.Rows.Add(dRow)

            Session("DataTable") = dt

            dt.Merge(dtMerge)
            dt.AcceptChanges()

            dt.DefaultView.Sort = "SrNo"

            dgPettyCashDetails.DataSource = dt
            dgPettyCashDetails.DataBind()

            dDebitAmt = txtDebitAmount.Text
            If txtCreditAmount.Text <> "" Then
                dCreditAmt = txtCreditAmount.Text
            Else
                txtCreditAmount.Text = txtDebitAmount.Text
            End If
            dCreditTotalAmt = dCreditAmt + dDebitAmt
            txtCreditAmount.Text = dCreditTotalAmt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgDebit_Click")
        End Try
    End Sub
    Private Sub imgCredit_Click(sender As Object, e As ImageClickEventArgs) Handles imgCredit.Click
        Dim dt As New DataTable, dtsample As New DataTable
        Dim dRow As DataRow
        Dim sArray As Array
        Try
            lblError.Text = ""
            If ddlCrHead.SelectedIndex = 0 Then
                lblError.Text = "Select Head of Accounts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Head of Accounts.','', 'info');", True)
                ddlCrHead.Focus()
                Exit Sub
            End If
            If ddlCrGL.SelectedIndex = 0 Then
                lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select General Ledger.','', 'info');", True)
                ddlCrGL.Focus()
                Exit Sub
            End If
            If txtCreditAmount.Text = "" Then
                lblError.Text = "Enter Credit Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Credit Amount.','', 'info');", True)
                txtCreditAmount.Focus()
                Exit Sub
            End If

            If ddldbsUbGL.Items.Count > 1 Then
                If ddldbsUbGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Debit."
                    Exit Sub
                End If
            End If

            If ddlCrSubGL.Items.Count > 1 Then
                If ddlCrSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Credit."
                    Exit Sub
                End If
            End If

            dtMerge = Session("DataTable")
            If IsNothing(dtMerge) = True Then
                dtMerge = dt
            End If

            If dtMerge.Rows.Count > 0 Then
                Dim dtview As New DataView(dtMerge)
                sArray = ddlCrGL.SelectedItem.Text.Split("-")
                dtview.RowFilter = "GLCode='" & sArray(0) & "' And GLDescription='" & sArray(1) & "'"
                dtview.Sort = "GLCode ASC"
                dtsample = dtview.ToTable

                If dtsample.Rows.Count > 0 Then
                    lblError.Text = "This combination already exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This combination already exists.','', 'info');", True)
                    txtCreditAmount.Focus()
                    Exit Sub
                End If
            End If

            dt.Columns.Add("ID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")

            dRow = dt.NewRow

            dRow("ID") = dtMerge.Rows.Count + 1

            If ddlCrHead.SelectedIndex > 0 Then
                dRow("HeadID") = ddlCrHead.SelectedIndex
            End If

            If ddlCrGL.SelectedIndex > 0 Then
                dRow("GLID") = ddlCrGL.SelectedValue
            End If

            If ddlCrSubGL.SelectedIndex > 0 Then
                dRow("SubGLID") = ddlCrSubGL.SelectedValue
            End If

            dRow("SrNo") = dtMerge.Rows.Count + 1

            If ddlCrGL.SelectedIndex > 0 Then
                sArray = ddlCrGL.SelectedItem.Text.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            If ddlCrSubGL.SelectedIndex > 0 Then
                sArray = ddlCrSubGL.SelectedItem.Text.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If

            If txtCreditAmount.Text <> "" Then
                dRow("Credit") = txtCreditAmount.Text
            End If
            dt.Rows.Add(dRow)

            Session("DataTable") = dt

            dt.Merge(dtMerge)
            dt.AcceptChanges()

            dt.DefaultView.Sort = "SrNo"

            dgPettyCashDetails.DataSource = dt
            dgPettyCashDetails.DataBind()

            txtBillAmount.Text = txtCreditAmount.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgCredit_Click")
        End Try
    End Sub
    Private Sub dgPettyCashDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPettyCashDetails.ItemDataBound
        Dim imgbtnDelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDelete = CType(e.Item.FindControl("imgbtnDelete"), ImageButton)
                imgbtnDelete.ImageUrl = "~/Images/Trash16.png"
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
            'If e.CommandName = "DeleteRow" Then
            '    lblId = e.Item.FindControl("lblId")
            '    If ddlExisting.SelectedIndex > 0 Then
            '        objPC.DeletePettyCashDetails(sSession.AccessCode, sSession.AccessCodeID, lblId.Text)
            '        dgPettyCashDetails.DataSource = objPC.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExisting.SelectedValue)
            '        dgPettyCashDetails.DataBind()
            '    Else
            '        dt = Session("DataTable")
            '        Dim DVZRBADetails As New DataView(dt)

            '        DVZRBADetails.Sort = "SrNo"
            '        Dim iIndex As Integer = DVZRBADetails.Find(lblId.Text)
            '        DVZRBADetails.Delete(iIndex)

            '        dt = DVZRBADetails.ToTable

            '        If dt.Rows.Count > 0 Then
            '            For j = 0 To dt.Rows.Count - 1
            '                dt.Rows(j)("SrNo") = j + 1
            '            Next
            '            dt.AcceptChanges()
            '        End If
            '        Session("DataTable") = dt
            '        dgPettyCashDetails.DataSource = dt
            '        dgPettyCashDetails.DataBind()
            '    End If

            'End If
            If e.CommandName = "Delete" Then
                dt = Session("Datatable")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                Session("Datatable") = dt
            End If
            dgPettyCashDetails.DataSource = dt
            dgPettyCashDetails.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPettyCashDetails_ItemCommand")
        End Try
    End Sub
    Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            dgAttach.CurrentPageIndex = 0
            ds = objPC.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
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
                    sExtn = objPC.GetExtension(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
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
                imgView.ImageUrl = "~/Images/NoImage.jpg"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlParty.SelectedIndex > 0 Then
                If ddlCustomerParty.SelectedIndex = 3 Then  ' General Ledger
                    ddldbHead.SelectedIndex = objPC.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                    ddldbHead_SelectedIndexChanged(sender, e)

                    ddldbGL.SelectedValue = ddlParty.SelectedValue
                    ddldbGL_SelectedIndexChanged(sender, e)

                ElseIf ddlCustomerParty.SelectedIndex = 1 Then   'Customer

                    dt = objPC.getCustomerLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        ddldbHead.SelectedIndex = objPC.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("BM_GL").ToString())
                        ddldbHead_SelectedIndexChanged(sender, e)

                        ddldbGL.SelectedValue = dt.Rows(0)("BM_GL").ToString()
                        ddldbGL_SelectedIndexChanged(sender, e)

                        If dt.Rows(0)("BM_SubGL").ToString() = "0" Then
                            ddldbsUbGL.SelectedIndex = -1
                        Else
                            ddldbsUbGL.SelectedValue = dt.Rows(0)("BM_SubGL").ToString()
                        End If
                    End If
                ElseIf ddlCustomerParty.SelectedIndex = 2 Then   'Suppliers

                    dt = objPC.getSuppliersLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        ddldbHead.SelectedIndex = objPC.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CSM_GL").ToString())
                        ddldbHead_SelectedIndexChanged(sender, e)

                        ddldbGL.SelectedValue = dt.Rows(0)("CSM_GL").ToString()
                        ddldbGL_SelectedIndexChanged(sender, e)

                        If dt.Rows(0)("CSM_SubGL").ToString() = "0" Then
                            ddldbsUbGL.SelectedIndex = -1
                        Else
                            ddldbsUbGL.SelectedValue = dt.Rows(0)("CSM_SubGL").ToString()
                        End If
                    End If

                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAddBillAmt_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddBillAmt.Click
        'Dim dBillAmt, dDebitAmt, dTotalBillAmt As Double
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
            txtDebitAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = ""
            dTotalAmt = 0
            'dBillAmt = txtBillAmount.Text
            'If txtDebitAmount.Text <> "" Then
            '    dDebitAmt = txtDebitAmount.Text
            'Else
            '    txtDebitAmount.Text = txtBillAmount.Text
            'End If
            'dTotalBillAmt = dDebitAmt + dBillAmt
            'txtDebitAmount.Text = dTotalBillAmt
            'txtBillAmount.Text = ""
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
            txtDebitAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPetty_ItemCommand")
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

            If ddlExisting.SelectedIndex = 0 Then
                lblError.Text = "Select Existing payment No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlExisting.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlExisting.SelectedItem.Text)
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
                        lblCustomerValidationMsg.Text = "Successfully Indexed."
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

            If ddlExisting.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Existing Payment No."
                ddlExisting.Focus()
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
            If ddlExisting.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Payment Voucher")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlExisting.SelectedItem.Text)

                    dt = objPC.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
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
                iParent = objPC.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                ddlAccArea.SelectedValue = iParent
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iParent = objPC.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                ddlAccRgn.SelectedValue = iParent
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iParent = objPC.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                ddlAccZone.SelectedValue = iParent
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Create debit GL
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
                ddlGroup.DataSource = objPC.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedIndex)
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
                ddlSubGroup.DataSource = objPC.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)
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
                ddldbGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                ddldbGL.DataTextField = "Description"
                ddldbGL.DataValueField = "gl_id"
                ddldbGL.DataBind()
                ddldbGL.Items.Insert(0, "Select GL")
                ddldbHead.SelectedIndex = ddlHead.SelectedIndex
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
                ddldbGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                ddldbGL.DataTextField = "Description"
                ddldbGL.DataValueField = "gl_id"
                ddldbGL.DataBind()
                ddldbGL.Items.Insert(0, "Select GL")
                ddldbHead.SelectedIndex = ddlHead.SelectedIndex
                ddldbGL.SelectedValue = objCOA.igl_id
                ddldbGL_SelectedIndexChanged(sender, e)
                LoadParty(ddlCustomerParty.SelectedIndex)
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
            ddldbGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
            ddldbGL.DataTextField = "Description"
            ddldbGL.DataValueField = "gl_id"
            ddldbGL.DataBind()
            ddldbGL.Items.Insert(0, "Select GL")
            ddldbHead.SelectedIndex = ddlHead.SelectedIndex
            ddldbGL.SelectedValue = iGLShrt
            ddldbGL_SelectedIndexChanged(sender, e)
            LoadParty(ddlCustomerParty.SelectedIndex)
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
                    btnDescSave.Visible = False : imgbtnUpdate.Visible = False : btnDescActivate.Visible = True

                    btnDescDeActivate.Visible = False
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


            ddldbGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
            ddldbGL.DataTextField = "Description"
            ddldbGL.DataValueField = "gl_id"
            ddldbGL.DataBind()
            ddldbGL.Items.Insert(0, "Select GL")
            ddldbHead.SelectedIndex = ddlHead.SelectedIndex
            LoadParty(ddlCustomerParty.SelectedIndex)

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
            ddlCRGLGroup.SelectedIndex = -1 : ddlCRGLSubGroup.SelectedIndex = -1 : txtCRGLCode.Text = "" : txtCRGLName.Text = ""

            txtCRGlDesc.Text = ""
            btnDescUpdateCRGL.Visible = False : btnDescDeActivateCRGL.Visible = False
            If ddlCRGLHead.SelectedIndex > 0 Then
                ddlCRGLGroup.DataSource = objPC.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLHead.SelectedIndex)
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
                ddlCRGLSubGroup.DataSource = objPC.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLGroup.SelectedValue)
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
                ddlCrGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLSubGroup.SelectedValue)
                ddlCrGL.DataTextField = "Description"
                ddlCrGL.DataValueField = "gl_id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL")
                ddlCrHead.SelectedIndex = ddlCRGLHead.SelectedIndex
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
                ddlCrGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLSubGroup.SelectedValue)
                ddlCrGL.DataTextField = "Description"
                ddlCrGL.DataValueField = "gl_id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL")
                ddlCrHead.SelectedIndex = ddlCRGLHead.SelectedIndex
                ddlCrGL.SelectedValue = objCOA.igl_id
                ddlCrGL_SelectedIndexChanged(sender, e)
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
            btnDescDeActivateCRGL.Visible = False : btnDescActivateCRGL.Visible = True : btnDescUpdateCRGL.Visible = False

            btnDescSaveCRGL.Visible = True
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
            ddlCrGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLSubGroup.SelectedValue)
            ddlCrGL.DataTextField = "Description"
            ddlCrGL.DataValueField = "gl_id"
            ddlCrGL.DataBind()
            ddlCrGL.Items.Insert(0, "Select GL")
            ddlCrHead.SelectedIndex = ddlCRGLHead.SelectedIndex
            ddlCrGL.SelectedValue = iGLShrt
            ddlCrGL_SelectedIndexChanged(sender, e)
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
                    btnDescSaveCRGL.Visible = False : imgbtnUpdate.Visible = False : btnDescActivateCRGL.Visible = True

                    btnDescDeActivateCRGL.Visible = False
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


            ddlCrGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCRGLSubGroup.SelectedValue)
            ddlCrGL.DataTextField = "Description"
            ddlCrGL.DataValueField = "gl_id"
            ddlCrGL.DataBind()
            ddlCrGL.Items.Insert(0, "Select GL")
            ddlCrHead.SelectedIndex = ddlCRGLHead.SelectedIndex

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
            ddlGroupSgl.SelectedIndex = -1 : ddlSubGroupSgl.SelectedIndex = -1 : txtCodeSgl.Text = "" : txtNameSgl.Text = ""

            txtDescSgl.Text = ""
            btnUpdateSgl.Visible = False : btnUpdateSgl.Visible = False
            If ddlHeadSgl.SelectedIndex > 0 Then
                ddlGroupSgl.DataSource = objPC.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHeadSgl.SelectedIndex)
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
                ddlSubGroupSgl.DataSource = objPC.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroupSgl.SelectedValue)
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
                ddlGLSgl.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupSgl.SelectedValue)
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
            ddlSubGroupSgl.SelectedIndex = -1 : ddlGLSgl.SelectedIndex = -1 : txtCodeSgl.Text = "" : txtNameSgl.Text = ""

            txtDescSgl.Text = ""
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
                ddldbsUbGL.DataSource = objPC.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGLSgl.SelectedValue)
                ddldbsUbGL.DataTextField = "Description"
                ddldbsUbGL.DataValueField = "gl_id"
                ddldbsUbGL.DataBind()
                ddldbsUbGL.Items.Insert(0, "Select GL")
                ddldbHead.SelectedIndex = ddlHeadSgl.SelectedIndex
                ddldbGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupSgl.SelectedValue)
                ddldbGL.DataTextField = "Description"
                ddldbGL.DataValueField = "gl_id"
                ddldbGL.DataBind()
                ddldbGL.Items.Insert(0, "Select SGL")
                ddldbGL.SelectedValue = ddlGLSgl.SelectedValue
                ddldbGL_SelectedIndexChanged(sender, e)
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
                ddldbsUbGL.DataSource = objPC.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGLSgl.SelectedValue)
                ddldbsUbGL.DataTextField = "Description"
                ddldbsUbGL.DataValueField = "gl_id"
                ddldbsUbGL.DataBind()
                ddldbsUbGL.Items.Insert(0, "Select GL")
                ddldbHead.SelectedIndex = ddlHeadSgl.SelectedIndex
                ddldbGL.SelectedValue = ddlGLSgl.SelectedValue
                ddldbGL_SelectedIndexChanged(sender, e)
                ddldbsUbGL.SelectedValue = objCOA.igl_id
                ddldbsUbGL_SelectedIndexChanged(sender, e)
                LoadParty(ddlCustomerParty.SelectedIndex)
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

            ddldbGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupSgl.SelectedValue)
            ddldbGL.DataTextField = "Description"
            ddldbGL.DataValueField = "gl_id"
            ddldbGL.DataBind()
            ddldbGL.Items.Insert(0, "Select GL")
            ddldbHead.SelectedIndex = ddlHead.SelectedIndex
            ddldbGL.SelectedValue = ddlGLSgl.SelectedValue
            ddldbGL_SelectedIndexChanged(sender, e)
            ddldbsUbGL.SelectedValue = iGLShrt
            ddldbsUbGL_SelectedIndexChanged(sender, e)
            LoadParty(ddlCustomerParty.SelectedIndex)

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


            ddldbGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlGLSgl.SelectedValue)
            ddldbGL.DataTextField = "Description"
            ddldbGL.DataValueField = "gl_id"
            ddldbGL.DataBind()
            ddldbGL.Items.Insert(0, "Select GL")
            ddldbHead.SelectedIndex = ddlHead.SelectedIndex
            ddldbsUbGL.DataSource = objPC.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGLSgl.SelectedValue)
            ddldbsUbGL.DataTextField = "Description"
            ddldbsUbGL.DataValueField = "gl_id"
            ddldbsUbGL.DataBind()
            ddldbsUbGL.Items.Insert(0, "Select SGL")
            LoadParty(ddlCustomerParty.SelectedIndex)


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
            ddlGroupCRSgl.SelectedIndex = -1 : ddlSubGroupCRSgl.SelectedIndex = -1 : txtCodeCRSgl.Text = "" : txtNameCRSgl.Text = ""

            txtDescCRSgl.Text = ""
            btnUpdateCRSgl.Visible = False : btnDeactivateCRSgl.Visible = False
            If ddlHeadCRSgl.SelectedIndex > 0 Then
                ddlGroupCRSgl.DataSource = objPC.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHeadCRSgl.SelectedIndex)
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
                ddlSubGroupCRSgl.DataSource = objPC.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroupCRSgl.SelectedValue)
                ddlSubGroupCRSgl.DataTextField = "GlDesc"
                ddlSubGroupCRSgl.DataValueField = "gl_Id"
                ddlSubGroupCRSgl.DataBind()
                ddlSubGroupCRSgl.Items.Insert(0, "Select SubGroup")
            Else
                'ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblErrorCRSGL.Text = "Select Head."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');'#ModalValidation').modal('show');", True)
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
                ddlGLCRSgl.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupCRSgl.SelectedValue)
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
            ddlSubGroupCRSgl.SelectedIndex = -1 : ddlGLCRSgl.SelectedIndex = -1 : txtCodeCRSgl.Text = "" : txtNameCRSgl.Text = ""

            txtDescCRSgl.Text = ""
            btnDeactivateCRSgl.Visible = False : btnActivateCRSgl.Visible = True : btnSaveCRSgl.Visible = True

            btnUpdateCRSgl.Visible = True
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
                ddlCrSubGL.DataSource = objPC.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGLCRSgl.SelectedValue)
                ddlCrSubGL.DataTextField = "Description"
                ddlCrSubGL.DataValueField = "gl_id"
                ddlCrSubGL.DataBind()
                ddlCrSubGL.Items.Insert(0, "Select SGL")
                ddlCrHead.SelectedIndex = ddlHeadCRSgl.SelectedIndex
                'ddlCrOtherGL.SelectedValue = ddlGLCRSgl.SelectedValue
                'ddlCrOtherGL_SelectedIndexChanged(sender, e)

                ddlCrGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupCRSgl.SelectedValue)
                ddlCrGL.DataTextField = "Description"
                ddlCrGL.DataValueField = "gl_id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL")
                ddlCrGL.SelectedValue = ddlGLCRSgl.SelectedValue
                ddlCrGL_SelectedIndexChanged(sender, e)
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
                ddlCrSubGL.DataSource = objPC.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGLCRSgl.SelectedValue)
                ddlCrSubGL.DataTextField = "Description"
                ddlCrSubGL.DataValueField = "gl_id"
                ddlCrSubGL.DataBind()
                ddlCrSubGL.Items.Insert(0, "Select GL")
                ddlCrHead.SelectedIndex = ddlHeadCRSgl.SelectedIndex
                ddlCrGL.SelectedValue = ddlGLCRSgl.SelectedValue
                ddlCrGL_SelectedIndexChanged(sender, e)
                ddlCrSubGL.SelectedValue = objCOA.igl_id
                ddlCrSubGL_SelectedIndexChanged(sender, e)
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

            ddlCrGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupCRSgl.SelectedValue)
            ddlCrGL.DataTextField = "Description"
            ddlCrGL.DataValueField = "gl_id"
            ddlCrGL.DataBind()
            ddlCrGL.Items.Insert(0, "Select GL")
            ddlCrHead.SelectedIndex = ddlHead.SelectedIndex
            ddlCrGL.SelectedValue = ddlGLCRSgl.SelectedValue
            ddlCrGL_SelectedIndexChanged(sender, e)
            ddlCrSubGL.SelectedValue = iGLShrt
            ddlCrSubGL_SelectedIndexChanged(sender, e)


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


            ddlCrGL.DataSource = objPC.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroupCRSgl.SelectedValue)
            ddlCrGL.DataTextField = "Description"
            ddlCrGL.DataValueField = "gl_id"
            ddlCrGL.DataBind()
            ddlCrGL.Items.Insert(0, "Select GL")
            ddlCrHead.SelectedIndex = ddlHead.SelectedIndex
            ddlCrSubGL.DataSource = objPC.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGLCRSgl.SelectedValue)
            ddlCrSubGL.DataTextField = "Description"
            ddlCrSubGL.DataValueField = "gl_id"
            ddlCrSubGL.DataBind()
            ddlCrSubGL.Items.Insert(0, "Select SGL")



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
