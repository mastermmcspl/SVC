Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_PurchaseSalesJEDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts_JETransactionDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objJE As New ClsPurchaseSalesJE
    Private Shared sSession As AllSession
    Public dtMerge As New DataTable
    Private objclsModulePermission As New clsModulePermission
    Private Shared iDbOrCr As Integer = 0
    Dim objPSJEDetails As New ClsPurchaseSalesJEDetails
    Private objDBL As New DatabaseLayer.DBHelper

    Private Shared sTypeName As String
    Private Shared sMasterName As String
    Private Shared iMasterID As Integer
    Private Shared iPKID As Integer
    Private Shared sTableName As String
    Private Shared sPSJESave As String
    Private Shared sGMFlag As String
    Private Shared sGMBackStatus As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        'imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        'imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        'imgCredit.ImageUrl = "~/Images/Add16.png"
        'imgDebit.ImageUrl = "~/Images/Add16.png"
        imgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterType As String = ""
        Dim sMasterID As String = ""
        Dim sStr As String = ""
        Dim sFormButtons As String = ""
        'Dim dStartDate As Date : Dim dEndDate As Date
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                divAdvance.Visible = False : divPayment.Visible = False
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PSJT")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgDebit.Visible = False : imgCredit.Visible = False
                sPSJESave = "NO"
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
                        'imgDebit.Visible = True : imgCredit.Visible = True
                        sPSJESave = "YES"
                    End If
                End If
                'dStartDate = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'dEndDate = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                'rgvtxtBillDate.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvtxtBillDate.MinimumValue = "" & dStartDate & ""
                'rgvtxtBillDate.MaximumValue = "" & dEndDate & ""

                'txtStartDate.Text = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtEndDate.Text = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                'LoadParty()
                LoadSubGL()
                LoadLocation(0)
                'txtTransactionNo.Text = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
                Me.txtAdvancePayment.Attributes.Add("onblur", "return CheckAdvancePayment()")

                RFVdbGL.InitialValue = "Select GL Code" : RFVdbGL.ErrorMessage = "Select General Ledger."
                RFVCrGL.InitialValue = "Select GL Code" : RFVCrGL.ErrorMessage = "Select General Ledger."
                RFVParty.InitialValue = "Select Customer/Supplier/Party" : RFVParty.ErrorMessage = "Select Customer/Supplier/Party."
                'RFVBillType.InitialValue = "Select Payment Voucher Type" : RFVBillType.ErrorMessage = "Select Payment Voucher Type."
                RFVPaymentType.InitialValue = "0" : RFVPaymentType.ErrorMessage = "Select Payment Type."
                'RFVBillNo.ErrorMessage = "Enter Valid Bill Number."

                REFBillDate.ValidationExpression = "^[0-3]?[0-9]\/[01]?[0-9]\/[12][90][0-9][0-9]$"
                REFBillDate.ErrorMessage = "Enter Valid Date Format."

                RFVEBillAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVEBillAmount.ErrorMessage = "Enter Valid Bill Amount."

                REVAdvance.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                REVAdvance.ErrorMessage = "Enter Valid Advance Amount."

                sStr = Request.QueryString("sPSStr")
                If sStr = "Purchase" Then
                    'imgbtnSave.ImageUrl = "~/Images/Update24.png"
                    LabelHeading.Text = "Purchase Journal Entry"
                    txtTransactionNo.Text = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "P")
                    'LoadParty("P")
                    LoadSupplier()
                    LoadBillNo(0, "P")
                    dgJEDetails.DataSource = Nothing
                    dgJEDetails.DataBind()
                ElseIf sStr = "Sales" Then
                    'imgbtnSave.ImageUrl = "~/Images/Update24.png"
                    LabelHeading.Text = "Sales Journal Entry"
                    txtTransactionNo.Text = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "S")
                    'LoadParty("S")
                    LoadCustomers()
                    LoadBillNo(0, "S")
                    dgJEDetails.DataSource = Nothing
                    dgJEDetails.DataBind()
                End If
                LoadBillType()
                LoadExistingJEs()
                LoadJornalType()

                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    ddlExistJE.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExistJE_SelectedIndexChanged(sender, e)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadSupplier()
        Try
            ddlParty.DataSource = objJE.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlParty.DataSource = objJE.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadBillNo(ByVal iParty As Integer, ByVal sStr As String)
        Try
            If sStr = "P" Then
                ddlBillNo.DataSource = objPSJEDetails.LoadBillNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iParty)
                ddlBillNo.DataTextField = "PV_DocRefNo"
                ddlBillNo.DataValueField = "PV_OrderNo"
                ddlBillNo.DataBind()
                ddlBillNo.Items.Insert(0, "Select")
            ElseIf sStr = "S" Then
                ddlBillNo.DataSource = objPSJEDetails.LoadSales(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iParty)
                ddlBillNo.DataTextField = "SDM_Code"
                ddlBillNo.DataValueField = "SDM_ID"
                ddlBillNo.DataBind()
                ddlBillNo.Items.Insert(0, "Select")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSubGL()
        Try
            ddlCrSubGL.DataSource = objJE.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlCrSubGL.DataTextField = "GlDesc"
            ddlCrSubGL.DataValueField = "gl_Id"
            ddlCrSubGL.DataBind()
            ddlCrSubGL.Items.Insert(0, "Select SubGL Code")

            ddldbsUbGL.DataSource = objJE.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddldbsUbGL.DataTextField = "GlDesc"
            ddldbsUbGL.DataValueField = "gl_Id"
            ddldbsUbGL.DataBind()
            ddldbsUbGL.Items.Insert(0, "Select SubGL Code")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadBillType()
        Try
            ddlBillType.DataSource = objJE.LoadBIllType(sSession.AccessCode, sSession.AccessCodeID)
            ddlBillType.DataTextField = "Mas_Desc"
            ddlBillType.DataValueField = "Mas_ID"
            ddlBillType.DataBind()
            ddlBillType.Items.Insert(0, "Select Payment Voucher Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadJornalType()
        Try
            ddlPaymentType.Items.Insert(0, "Journal Type")
            ddlPaymentType.Items.Insert(1, "Advance Payment")
            ddlPaymentType.Items.Insert(2, "TDS")
            ddlPaymentType.Items.Insert(3, "Payment")
            ddlPaymentType.Items.Insert(4, "Cheque Details")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingJEs()
        Try
            If (txtTransactionNo.Text).StartsWith("P") Then
                ddlExistJE.DataSource = objJE.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, "P")
                ddlExistJE.DataTextField = "Acc_PJE_TransactionNo"
                ddlExistJE.DataValueField = "Acc_PJE_ID"
                ddlExistJE.DataBind()
                ddlExistJE.Items.Insert(0, "Existing JE Voucher")
            ElseIf (txtTransactionNo.Text).StartsWith("S") Then
                ddlExistJE.DataSource = objJE.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, "S")
                ddlExistJE.DataTextField = "Acc_SJE_TransactionNo"
                ddlExistJE.DataValueField = "Acc_SJE_ID"
                ddlExistJE.DataBind()
                ddlExistJE.Items.Insert(0, "Existing JE Voucher")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadParty(ByVal sStr As String)
        Try
            ddlParty.DataSource = objJE.LoadParty(sSession.AccessCode, sSession.AccessCodeID, sStr)
            ddlParty.DataTextField = "Name"
            ddlParty.DataValueField = "ACM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("PurchaseSalesJE.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub LoadLocation(ByVal iParty As Integer)
        Try
            ddlLocation.DataSource = objJE.LoadLocations(sSession.AccessCode, sSession.AccessCodeID, iParty)
            ddlLocation.DataTextField = "Org_Name"
            ddlLocation.DataValueField = "Org_Node"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, "Select Locations")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Try
            If ddlParty.SelectedIndex > 0 Then
                'ddlLocation.DataSource = objJE.LoadLocations(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                'ddlLocation.DataTextField = "Name"
                'ddlLocation.DataValueField = "Mas_ID"
                'ddlLocation.DataBind()
                'ddlLocation.Items.Insert(0, "Select Locations")
                'LoadLocation(ddlParty.SelectedValue)

                If txtTransactionNo.Text <> "" Then
                    If (txtTransactionNo.Text).StartsWith("P") Then
                        LoadBillNo(ddlParty.SelectedValue, "P")
                    ElseIf (txtTransactionNo.Text).StartsWith("S") Then
                        LoadBillNo(ddlParty.SelectedValue, "S")
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddldbHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbHead.SelectedIndexChanged
        Try
            If ddldbHead.SelectedIndex > 0 Then
                ddldbGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbHead.SelectedValue, "")
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
                ddldbsUbGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbGL.SelectedValue, "")
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
            If ddlCrHead.SelectedIndex > 0 Then
                ddlCrGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedValue, "")
                ddlCrGL.DataTextField = "GlDesc"
                ddlCrGL.DataValueField = "gl_Id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL Code")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCrGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrGL.SelectedIndexChanged
        Try
            If ddlCrGL.SelectedIndex > 0 Then
                ddlCrSubGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue, "")
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
    Private Sub ddlPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentType.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dAvance As Double = 0, dBillAmount As Double = 0
        Dim dBalance As Double = 0
        Try
            If ddlPaymentType.SelectedIndex = "1" Then
                divAdvance.Visible = True : divPayment.Visible = False
            ElseIf ddlPaymentType.SelectedIndex = "2" Then
                'divAdvance.Visible = False : divPayment.Visible = True
            ElseIf ddlPaymentType.SelectedIndex = "3" Then
                divAdvance.Visible = False : divPayment.Visible = True
            ElseIf ddlPaymentType.SelectedIndex = "4" Then
                divAdvance.Visible = False : divPayment.Visible = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            ElseIf ddlPaymentType.SelectedIndex = "0" Then
                divAdvance.Visible = False : divPayment.Visible = False
            End If
            If ddlExistJE.SelectedIndex > 0 Then
                dt = objPSJEDetails.GetPaymentTypeDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistJE.SelectedValue, ddlExistJE.SelectedItem.Text)
                If dt.Rows.Count > 0 Then
                    If ddlPaymentType.SelectedIndex = 1 Then  'Advance
                        If IsDBNull(dt.Rows(0)("Acc_JE_AdvanceAmount").ToString()) = False Then
                            If dt.Rows(0)("Acc_JE_AdvanceAmount").ToString() <> "0" Then
                                dAvance = dt.Rows(0)("Acc_JE_AdvanceAmount").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(0)("Acc_JE_BillAmount").ToString()) = False Then
                            If dt.Rows(0)("Acc_JE_BillAmount").ToString() <> "0" Then
                                dBillAmount = dt.Rows(0)("Acc_JE_BillAmount").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(0)("Acc_JE_BalanceAmount").ToString()) = False Then
                            If dt.Rows(0)("Acc_JE_BalanceAmount").ToString() <> "0" Then
                                dBalance = dt.Rows(0)("Acc_JE_BalanceAmount").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(0)("Acc_JE_AdvanceNaration").ToString()) = False Then
                            If dt.Rows(0)("Acc_JE_AdvanceNaration").ToString() <> "0" Then
                                txtNarration.Text = dt.Rows(0)("Acc_JE_AdvanceNaration").ToString()
                            End If
                        End If

                        txtAdvancePayment.Text = dAvance
                        txtBalanceAmount.Text = dBalance


                    ElseIf ddlPaymentType.SelectedIndex = 3 Then   'Payment
                        If IsDBNull(dt.Rows(0)("Acc_JE_BillAmount").ToString()) = False Then
                            If dt.Rows(0)("Acc_JE_BillAmount").ToString() <> "0" Then
                                dBillAmount = dt.Rows(0)("Acc_JE_BillAmount").ToString()
                            End If
                        End If
                        txtNetAmount.Text = dBillAmount

                        If IsDBNull(dt.Rows(0)("ACC_JE_PaymentNarration").ToString()) = False Then
                            If dt.Rows(0)("ACC_JE_PaymentNarration").ToString() <> "0" Then
                                txtNarration.Text = dt.Rows(0)("ACC_JE_PaymentNarration").ToString()
                            End If
                        End If
                    End If
                End If
            Else
                If ddlPaymentType.SelectedIndex = 1 Then  'Advance
                    txtBalanceAmount.Text = txtBillAmount.Text
                ElseIf ddlPaymentType.SelectedIndex = 3 Then 'Payment
                    txtNetAmount.Text = txtBillAmount.Text
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPaymentType_SelectedIndexChanged")
        End Try
    End Sub
    Private Function CheckDebitAndCredit() As Integer
        Dim i As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0, dBillAmt As Double = 0
        Try
            For i = 0 To dgJEDetails.Items.Count - 1
                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    dDebit = dDebit + Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    dCredit = dCredit + Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                End If
            Next

            If String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dDebit))) <> String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dCredit))) Then
                Return 1  ' Debit and Credit amount not Matched
            End If

            If ddlPaymentType.SelectedIndex = 1 Then  'Advance Payment

                If txtAdvancePayment.Text <> dDebit Then
                    Return 2  ' Amount not Matched with Advance Payment
                End If
            ElseIf ddlPaymentType.SelectedIndex = 3 Then   'Payment
                If txtNetAmount.Text <> dDebit Then
                    Return 3  ' Amount not Matched with Net Amount
                End If

            End If

            dBillAmt = txtBillAmount.Text
            If dDebit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dDebit))) <> String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dBillAmt))) Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            Else
                If dDebit <> dBillAmt Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            End If

            If dCredit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dCredit))) <> String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dBillAmt))) Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            Else
                If dCredit <> dBillAmt Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckDebitAndCredit")
        End Try
    End Function
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim sStatus As String = "" : Dim iPSID As Integer
        Try
            'If txtTransactionNo.Text <> "" Then
            '    If (txtTransactionNo.Text).StartsWith("P") Then

            '    ElseIf (txtTransactionNo.Text).StartsWith("S") Then

            '    End If
            'End If
            lblError.Text = ""
            If txtTransactionNo.Text.StartsWith("P") Then
                iPSID = 0
            Else
                iPSID = 1
            End If
            sStatus = objPSJEDetails.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, "W", sSession.UserID, iPSID, sSession.IPAddress, sSession.YearID)
            If sStatus = "A" Then
                lblError.Text = "This Transaction is already Approved,It can not be updated."
                Exit Sub
            End If

            If txtBillDate.Text <> "" Then
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
            End If

            iRet = CheckDebitAndCredit()

            If iRet = 1 Then
                lblCustomerValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 2 Then
                lblCustomerValidationMsg.Text = "Amount Not Matched with Advance Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 3 Then
                lblCustomerValidationMsg.Text = "Amount Not Matched with Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 4 Then
                lblCustomerValidationMsg.Text = "Total Debit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 5 Then
                lblCustomerValidationMsg.Text = "Total Credit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            If (ddlPaymentType.SelectedIndex = 1) Or (ddlPaymentType.SelectedIndex = 2) Or (ddlPaymentType.SelectedIndex = 3) Then
                If ddldbHead.SelectedIndex = 0 Then
                    lblCustomerValidationMsg.Text = "Enter Debit Details."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)
                    Exit Sub
                End If

                If ddlCrHead.SelectedIndex = 0 Then
                    lblCustomerValidationMsg.Text = "Enter Credit Details."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)
                    Exit Sub
                End If
            End If

            If ddlExistJE.SelectedIndex > 0 Then
                objPSJEDetails.iAcc_JE_ID = ddlExistJE.SelectedValue
            Else
                objPSJEDetails.iAcc_JE_ID = 0
            End If

            objPSJEDetails.sAcc_JE_TransactionNo = txtTransactionNo.Text

            If ddlParty.SelectedIndex > 0 Then
                objPSJEDetails.iAcc_JE_Party = ddlParty.SelectedValue
            Else
                objPSJEDetails.iAcc_JE_Party = 0
            End If

            If ddlLocation.SelectedIndex > 0 Then
                objPSJEDetails.iAcc_JE_Location = ddlLocation.SelectedValue
            Else
                objPSJEDetails.iAcc_JE_Location = 0
            End If

            If ddlBillType.SelectedIndex > 0 Then
                objPSJEDetails.iAcc_JE_BillType = ddlBillType.SelectedValue
            Else
                objPSJEDetails.iAcc_JE_BillType = 0
            End If
            objPSJEDetails.iAcc_JE_InvoiceID = 0
            'ddlBillNo.SelectedValue
            objPSJEDetails.sAcc_JE_BillNo = txtBillNo.Text
            'ddlBillNo.SelectedItem.Text

            objPSJEDetails.dAcc_JE_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objPSJEDetails.dAcc_JE_BillAmount = txtBillAmount.Text
            objPSJEDetails.iAcc_JE_YearID = sSession.YearID
            objPSJEDetails.sAcc_JE_Status = "W"
            objPSJEDetails.iAcc_JE_CreatedBy = sSession.UserID
            objPSJEDetails.iAcc_JE_CreatedOn = DateTime.Today
            objPSJEDetails.sAcc_JE_Operation = "C"
            objPSJEDetails.sAcc_JE_IPAddress = sSession.IPAddress
            objPSJEDetails.dAcc_JE_BillCreatedDate = DateTime.Today

            'If ddlPaymentType.SelectedIndex = 1 Then
            '    objPSJEDetails.dAcc_JE_AdvanceAmount = 0.00 : objPSJEDetails.dAcc_JE_BalanceAmount = 0.00
            '    objPSJEDetails.sAcc_JE_AdvanceNaration = ""

            'ElseIf ddlPaymentType.SelectedIndex = 3 Then
            '    objPSJEDetails.dAcc_JE_NetAmount = 0.00
            '    objPSJEDetails.sAcc_JE_PaymentNarration = ""

            'ElseIf ddlPaymentType.SelectedIndex = 4 Then
            '    objPSJEDetails.sAcc_JE_ChequeNo = ""
            '    objPSJEDetails.sAcc_JE_IFSCCode = "" : objPSJEDetails.sAcc_JE_BankName = "" : objPSJEDetails.sAcc_JE_BranchName = ""
            'End If

            objPSJEDetails.sAcc_JE_AdvanceNaration = ""
            objPSJEDetails.sAcc_JE_PaymentNarration = ""
            objPSJEDetails.sAcc_JE_ChequeNo = ""
            objPSJEDetails.sAcc_JE_IFSCCode = ""
            objPSJEDetails.sAcc_JE_BankName = ""
            objPSJEDetails.sAcc_JE_BranchName = ""

            'If ddlPaymentType.SelectedIndex = "1" Then   ' Advance 
            '    iPaymentType = 1
            '    If txtAdvancePayment.Text <> "" Then
            '        objPSJEDetails.dAcc_JE_AdvanceAmount = txtAdvancePayment.Text
            '    Else
            '        objPSJEDetails.dAcc_JE_AdvanceAmount = 0.00
            '    End If
            '    objPSJEDetails.dAcc_JE_BalanceAmount = txtBalanceAmount.Text
            '    objPSJEDetails.sAcc_JE_AdvanceNaration = txtNarration.Text

            'ElseIf ddlPaymentType.SelectedIndex = "3" Then   'Payment
            '    iPaymentType = 3
            '    If txtNarration.Text <> "" Then
            '        objPSJEDetails.dAcc_JE_NetAmount = txtNarration.Text
            '    Else
            '        objPSJEDetails.dAcc_JE_NetAmount = 0.00
            '    End If
            '    objPSJEDetails.sAcc_JE_PaymentNarration = txtNarration.Text

            'ElseIf ddlPaymentType.SelectedIndex = "4" Then   'Cheque Details
            '    iPaymentType = 4
            '    objPSJEDetails.sAcc_JE_ChequeNo = txtChequeNo.Text
            '    objPSJEDetails.dAcc_JE_ChequeDate = txtChequeDate.Text
            '    objPSJEDetails.sAcc_JE_IFSCCode = txtIFSC.Text
            '    objPSJEDetails.sAcc_JE_BankName = txtBankName.Text
            '    objPSJEDetails.sAcc_JE_BranchName = txtBranchName.Text
            'End If

            objPSJEDetails.iAcc_JE_UpdatedBy = sSession.UserID
            objPSJEDetails.iAcc_JE_UpdatedOn = DateTime.Today
            objPSJEDetails.iAcc_JE_CompID = sSession.AccessCodeID

            objPSJEDetails.dAcc_JE_PendingAmount = txtBillAmount.Text

            If txtTransactionNo.Text <> "" Then
                If objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("P") Then
                    Arr = objPSJEDetails.SavePurchaseJournalMaster(sSession.AccessCode, objPSJEDetails)
                    iTransID = Arr(1)
                ElseIf objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("S") Then
                    objPSJEDetails.sAcc_JE_Type = "SI"
                    Arr = objPSJEDetails.SaveSalesJournalMaster(sSession.AccessCode, objPSJEDetails)
                    iTransID = Arr(1)
                End If
            End If

            For i = 0 To dgJEDetails.Items.Count - 1

                If objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("P") Then
                    objPSJEDetails.iATD_TrType = 5
                ElseIf objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("S") Then
                    objPSJEDetails.iATD_TrType = 6
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objPSJEDetails.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objPSJEDetails.iATD_ID = 0
                End If

                objPSJEDetails.dATD_TransactionDate = DateTime.Today

                objPSJEDetails.iATD_BillId = iTransID
                objPSJEDetails.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
                'iPaymentType

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objPSJEDetails.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objPSJEDetails.iATD_Head = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objPSJEDetails.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objPSJEDetails.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objPSJEDetails.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objPSJEDetails.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objPSJEDetails.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objPSJEDetails.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objPSJEDetails.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objPSJEDetails.dATD_Credit = 0
                End If

                If objPSJEDetails.dATD_Debit > 0 And objPSJEDetails.dATD_Credit = 0 Then
                    objPSJEDetails.iATD_DbOrCr = 1 'Debit
                ElseIf objPSJEDetails.dATD_Debit = 0 And objPSJEDetails.dATD_Credit > 0 Then
                    objPSJEDetails.iATD_DbOrCr = 2 'Credit
                End If

                objPSJEDetails.iATD_CreatedBy = sSession.UserID
                objPSJEDetails.dATD_CreatedOn = DateTime.Today

                objPSJEDetails.sATD_Status = "W"
                objPSJEDetails.iATD_YearID = sSession.YearID
                objPSJEDetails.sATD_Operation = "C"
                objPSJEDetails.sATD_IPAddress = sSession.IPAddress

                objPSJEDetails.iATD_UpdatedBy = sSession.UserID
                objPSJEDetails.dATD_UpdatedOn = DateTime.Today

                objPSJEDetails.iATD_CompID = sSession.AccessCodeID

                objPSJEDetails.dATD_OpenDebit = "0.00"
                objPSJEDetails.dATD_OpenCredit = "0.00"
                objPSJEDetails.dATD_ClosingDebit = "0.00"
                objPSJEDetails.dATD_ClosingCredit = "0.00"
                objPSJEDetails.iATD_SeqReferenceNum = 0


                Arr = objPSJEDetails.SaveUpdateTransactionDetails(sSession.AccessCode, objPSJEDetails)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    'imgbtnSave.ImageUrl = "~/Images/Save24.png"

                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
            Next

            dgJEDetails.DataSource = objPSJEDetails.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID, txtTransactionNo.Text)
            dgJEDetails.DataBind()

            LoadExistingJEs()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub ddlExistJE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistJE.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistJE.SelectedIndex > 0 Then
                'imgbtnSave.ImageUrl = "~/Images/Update24.png"
                BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistJE.SelectedValue, ddlExistJE.SelectedItem.Text)
                'imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
            Else
                ' imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistJE_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPayment As Integer, ByVal sExiJV As String)
        Dim dt As New DataTable
        Dim iPaymentType As Integer
        Dim dtDetails As New DataTable
        Try

            dt = objPSJEDetails.GetPaymentTypeDetails(sNameSpace, iCompID, iYearID, iPayment, sExiJV)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("TrNo").ToString()) = False Then
                    txtTransactionNo.Text = dt.Rows(0)("TrNo").ToString()
                Else
                    txtTransactionNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Party").ToString()) = False Then
                    ddlParty.SelectedValue = dt.Rows(0)("Party").ToString()
                Else
                    ddlParty.SelectedIndex = 0
                End If

                'If ddlParty.SelectedIndex > 0 Then
                '    ddlLocation.DataSource = objJE.LoadLocations(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                '    ddlLocation.DataTextField = "Name"
                '    ddlLocation.DataValueField = "Mas_ID"
                '    ddlLocation.DataBind()
                '    ddlLocation.Items.Insert(0, "Select Locations")
                'End If


                If IsDBNull(dt.Rows(0)("Location").ToString()) = False Then
                    If dt.Rows(0)("Location").ToString() = "0" Then
                        ddlLocation.SelectedIndex = 0
                    Else
                        ddlLocation.SelectedValue = dt.Rows(0)("Location").ToString()
                    End If
                Else
                    ddlLocation.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("BillType").ToString()) = False Then
                    If dt.Rows(0)("BillType") > 0 Then
                        ddlBillType.SelectedValue = dt.Rows(0)("BillType").ToString()
                    Else
                        ddlBillType.SelectedIndex = 0
                    End If
                Else
                    ddlBillType.SelectedIndex = 0
                End If

                If (txtTransactionNo.Text).StartsWith("P") Then
                    LoadBillNo(ddlParty.SelectedValue, "P")
                ElseIf (txtTransactionNo.Text).StartsWith("S") Then
                    LoadBillNo(ddlParty.SelectedValue, "S")
                End If

                If IsDBNull(dt.Rows(0)("BillNo").ToString()) = False Then
                    txtBillNo.Text = dt.Rows(0)("BillNo").ToString()
                Else
                    txtBillNo.Text = ""
                End If
                'If IsDBNull(dt.Rows(0)("BillNo").ToString()) = False Then
                '    ddlBillNo.SelectedValue = dt.Rows(0)("BillNo")
                'Else
                '    ddlBillNo.SelectedIndex = 0
                'End If


                If IsDBNull(dt.Rows(0)("BillDate").ToString()) = False Then
                    txtBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("BillDate").ToString(), "D")
                Else
                    txtBillDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BillAmount").ToString()) = False Then
                    txtBillAmount.Text = dt.Rows(0)("BillAmount").ToString()
                Else
                    txtBillAmount.Text = ""
                End If


                If IsDBNull(dt.Rows(0)("AdvanceAmount").ToString()) = False Then
                    If dt.Rows(0)("AdvanceAmount").ToString() <> "" Then
                        txtNetAmount.Text = Convert.ToDecimal(txtBillAmount.Text - dt.Rows(0)("AdvanceAmount").ToString())
                    Else
                        txtNetAmount.Text = ""
                        'dt.Rows(0)("Acc_PJE_BillNo").ToString()
                    End If
                Else
                    txtNetAmount.Text = ""
                    'dt.Rows(0)("Acc_PJE_BillNo").ToString()
                End If

                If dt.Rows(0)("Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                ElseIf dt.Rows(0)("Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                ElseIf dt.Rows(0)("Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                End If

                If IsDBNull(dt.Rows(0)("ChequeNo").ToString()) = False Then
                    txtChequeNo.Text = dt.Rows(0)("ChequeNo").ToString()
                Else
                    txtChequeNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("ChequeDate").ToString()) = False Then
                    If dt.Rows(0)("ChequeDate").ToString() <> "" Then
                        txtChequeDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("ChequeDate").ToString(), "D")
                    Else
                        txtChequeDate.Text = ""
                    End If
                Else
                    txtChequeDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("IFSCCode").ToString()) = False Then
                    txtIFSC.Text = dt.Rows(0)("IFSCCode").ToString()
                Else
                    txtIFSC.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BankName").ToString()) = False Then
                    txtBankName.Text = dt.Rows(0)("BankName").ToString()
                Else
                    txtBankName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BranchName").ToString()) = False Then
                    txtBranchName.Text = dt.Rows(0)("BranchName").ToString()
                Else
                    txtBranchName.Text = ""
                End If

            End If

            'iPaymentType = objPSJEDetails.GetPaymentTypeID(sNameSpace, iCompID, iPayment, sExiJV)
            'If iPaymentType > 0 Then
            '    ddlPaymentType.SelectedValue = iPaymentType
            'Else
            '    ddlPaymentType.SelectedIndex = 0
            'End If

            dtDetails = objPSJEDetails.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPayment, sExiJV)
            dgJEDetails.DataSource = dtDetails
            dgJEDetails.DataBind()
            Session("DetailsGrid") = dtDetails

            dgItems.DataSource = objPSJEDetails.GetItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("BillNo").ToString(), sExiJV, dt.Rows(0)("Type"), dt.Rows(0)("InvoiceID"))
            dgItems.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dgJEDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgJEDetails.ItemDataBound
        Dim imgbtnDelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                'imgbtnDelete = CType(e.Item.FindControl("imgbtnDelete"), ImageButton)
                'imgbtnEdit = CType(e.Item.FindControl("imgbtnedit"), ImageButton)
                'imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                'imgbtnDelete.ImageUrl = "~/Images/4delete.gif"
                dgJEDetails.Columns(15).Visible = False
                If sPSJESave = "YES" Then
                    dgJEDetails.Columns(15).Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJEDetails_ItemDataBound")
        End Try
    End Sub

    'Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
    '    Dim iPaymentType As Integer = 0 : Dim iTransID As Integer = 0
    '    Dim dDebit As Double = 0, dCredit As Double = 0
    '    Try
    '        objPSJEDetails.iAcc_JE_ID = ddlExistJE.SelectedValue
    '        objPSJEDetails.sAcc_JE_TransactionNo = txtTransactionNo.Text
    '        If ddlParty.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_Party = ddlParty.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_Party = 0
    '        End If

    '        If ddlLocation.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_Location = ddlLocation.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_Location = 0
    '        End If

    '        If ddlBillType.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_BillType = ddlBillType.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_BillType = 0
    '        End If

    '        objPSJEDetails.sAcc_JE_BillNo = txtBillNo.Text
    '        objPSJEDetails.dAcc_JE_BillDate = txtBillDate.Text
    '        objPSJEDetails.dAcc_JE_BillAmount = txtBillAmount.Text
    '        objPSJEDetails.iAcc_JE_YearID = sSession.YearID
    '        objPSJEDetails.sAcc_JE_Status = "W"
    '        objPSJEDetails.iAcc_JE_CreatedBy = sSession.UserID
    '        objPSJEDetails.sAcc_JE_Operation = "U"
    '        objPSJEDetails.sAcc_JE_IPAddress = sSession.IPAddress

    '        If ddlPaymentType.SelectedIndex = 1 Then
    '            objPSJEDetails.dAcc_JE_AdvanceAmount = 0.00 : objPSJEDetails.dAcc_JE_BalanceAmount = 0.00
    '            objPSJEDetails.sAcc_JE_AdvanceNaration = ""

    '        ElseIf ddlPaymentType.SelectedIndex = 3 Then
    '            objPSJEDetails.dAcc_JE_NetAmount = 0.00
    '            objPSJEDetails.sAcc_JE_PaymentNarration = ""

    '        ElseIf ddlPaymentType.SelectedIndex = 4 Then
    '            objPSJEDetails.sAcc_JE_ChequeNo = ""
    '            objPSJEDetails.sAcc_JE_IFSCCode = "" : objPSJEDetails.sAcc_JE_BankName = "" : objPSJEDetails.sAcc_JE_BranchName = ""
    '        End If

    '        If ddlPaymentType.SelectedIndex = "1" Then   ' Advance 
    '            iPaymentType = 1
    '            If txtAdvancePayment.Text <> "" Then
    '                objPSJEDetails.dAcc_JE_AdvanceAmount = txtAdvancePayment.Text
    '            Else
    '                objPSJEDetails.dAcc_JE_AdvanceAmount = 0.00
    '            End If
    '            objPSJEDetails.dAcc_JE_BalanceAmount = txtBalanceAmount.Text
    '            objPSJEDetails.sAcc_JE_AdvanceNaration = txtNarration.Text


    '        ElseIf ddlPaymentType.SelectedIndex = "3" Then   'Payment
    '            iPaymentType = 3
    '            If txtNetAmount.Text <> "" Then
    '                objPSJEDetails.dAcc_JE_NetAmount = txtNetAmount.Text
    '            Else
    '                objPSJEDetails.dAcc_JE_NetAmount = 0.00
    '            End If
    '            objPSJEDetails.sAcc_JE_PaymentNarration = txtNarration.Text

    '        ElseIf ddlPaymentType.SelectedIndex = "4" Then   'Cheque Details
    '            iPaymentType = 4
    '            objPSJEDetails.sAcc_JE_ChequeNo = txtChequeNo.Text
    '            objPSJEDetails.dAcc_JE_ChequeDate = txtChequeDate.Text
    '            objPSJEDetails.sAcc_JE_IFSCCode = txtIFSC.Text
    '            objPSJEDetails.sAcc_JE_BankName = txtBankName.Text
    '            objPSJEDetails.sAcc_JE_BranchName = txtBranchName.Text
    '        End If

    '        iTransID = objPSJEDetails.UpdatePaymentMaster(sSession.AccessCode, sSession.AccessCodeID, iPaymentType, objPSJEDetails)

    '        If lblTransID.Text > 0 Then

    '            If iPaymentType = 1 Then
    '                dDebit = txtAdvancePayment.Text : dCredit = txtAdvancePayment.Text
    '            ElseIf iPaymentType = 3 Then
    '                dDebit = txtNetAmount.Text : dCredit = txtNetAmount.Text
    '            End If
    '            objPSJEDetails.iATD_ID = lblTransID.Text

    '            If (txtTransactionNo.Text).StartsWith("P") Then
    '                objPSJEDetails.iATD_TrType = 5
    '            ElseIf (txtTransactionNo.Text).StartsWith("S") Then
    '                objPSJEDetails.iATD_TrType = 6
    '            End If

    '            'Debit
    '            If iDbOrCr = 1 Then
    '                objPSJEDetails.iATD_DbOrCr = 1
    '                'objPSJEDetails.iATD_TrType = 1
    '                objPSJEDetails.iATD_BillId = iTransID
    '                objPSJEDetails.iATD_PaymentType = iPaymentType

    '                If ddldbHead.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_Head = ddldbHead.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_Head = 0
    '                End If

    '                If ddldbGL.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_GL = ddldbGL.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_GL = 0
    '                End If

    '                If ddldbsUbGL.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_SubGL = ddldbsUbGL.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_SubGL = 0
    '                End If

    '                objPSJEDetails.dATD_Debit = dDebit
    '                objPSJEDetails.dATD_Credit = 0.00
    '                objPSJEDetails.iATD_CreatedBy = sSession.UserID
    '                objPSJEDetails.dATD_CreatedOn = DateTime.Today

    '                objPSJEDetails.sATD_Status = "A"
    '                objPSJEDetails.iATD_YearID = sSession.YearID
    '                objPSJEDetails.sATD_Operation = "U"
    '                objPSJEDetails.sATD_IPAddress = sSession.IPAddress

    '                objPSJEDetails.iATD_UpdatedBy = sSession.UserID
    '                objPSJEDetails.dATD_UpdatedOn = DateTime.Today
    '                objPSJEDetails.SaveUpdateTransactionDetails(sSession.AccessCode, objPSJEDetails)
    '            End If

    '            'Credit
    '            If iDbOrCr = 2 Then
    '                objPSJEDetails.iATD_DbOrCr = 2
    '                'objPSJEDetails.iATD_TrType = 1
    '                objPSJEDetails.iATD_BillId = iTransID
    '                objPSJEDetails.iATD_PaymentType = iPaymentType

    '                If ddlCrHead.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_Head = ddlCrHead.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_Head = 0
    '                End If

    '                If ddlCrGL.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_GL = ddlCrGL.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_GL = 0
    '                End If

    '                If ddlCrSubGL.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_SubGL = ddlCrSubGL.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_SubGL = 0
    '                End If

    '                objPSJEDetails.dATD_Debit = 0.00
    '                objPSJEDetails.dATD_Credit = dCredit
    '                objPSJEDetails.iATD_CreatedBy = sSession.UserID
    '                objPSJEDetails.dATD_CreatedOn = DateTime.Today
    '                objPSJEDetails.sATD_Status = "A"
    '                objPSJEDetails.iATD_YearID = sSession.YearID
    '                objPSJEDetails.sATD_Operation = "U"
    '                objPSJEDetails.sATD_IPAddress = sSession.IPAddress

    '                objPSJEDetails.iATD_UpdatedBy = sSession.UserID
    '                objPSJEDetails.dATD_UpdatedOn = DateTime.Today

    '                objPSJEDetails.SaveUpdateTransactionDetails(sSession.AccessCode, objPSJEDetails)
    '            End If
    '        Else

    '            If iPaymentType > 0 Then

    '                If iPaymentType = 1 Then
    '                    dDebit = txtAdvancePayment.Text : dCredit = txtAdvancePayment.Text
    '                ElseIf iPaymentType = 3 Then
    '                    dDebit = txtNetAmount.Text : dCredit = txtNetAmount.Text
    '                End If

    '                'Debit
    '                objPSJEDetails.iATD_TrType = 1
    '                objPSJEDetails.iATD_BillId = iTransID
    '                objPSJEDetails.iATD_PaymentType = iPaymentType
    '                objPSJEDetails.iATD_DbOrCr = 1

    '                If ddldbHead.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_Head = ddldbHead.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_Head = 0
    '                End If

    '                If ddldbGL.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_GL = ddldbGL.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_GL = 0
    '                End If

    '                If ddldbsUbGL.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_SubGL = ddldbsUbGL.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_SubGL = 0
    '                End If

    '                objPSJEDetails.dATD_Debit = dDebit
    '                objPSJEDetails.dATD_Credit = 0.00
    '                objPSJEDetails.iATD_CreatedBy = sSession.UserID
    '                objPSJEDetails.dATD_CreatedOn = DateTime.Today

    '                objPSJEDetails.sATD_Status = "A"
    '                objPSJEDetails.iATD_YearID = sSession.YearID
    '                objPSJEDetails.sATD_Operation = "U"
    '                objPSJEDetails.sATD_IPAddress = sSession.IPAddress

    '                objPSJEDetails.iATD_UpdatedBy = sSession.UserID
    '                objPSJEDetails.dATD_UpdatedOn = DateTime.Today

    '                objPSJEDetails.SaveUpdateTransactionDetails(sSession.AccessCode, objPSJEDetails)

    '                'Credit
    '                objPSJEDetails.iATD_TrType = 1
    '                objPSJEDetails.iATD_BillId = iTransID
    '                objPSJEDetails.iATD_PaymentType = iPaymentType
    '                objPSJEDetails.iATD_DbOrCr = 2

    '                If ddlCrHead.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_Head = ddlCrHead.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_Head = 0
    '                End If

    '                If ddlCrGL.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_GL = ddlCrGL.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_GL = 0
    '                End If

    '                If ddlCrSubGL.SelectedIndex > 0 Then
    '                    objPSJEDetails.iATD_SubGL = ddlCrSubGL.SelectedValue
    '                Else
    '                    objPSJEDetails.iATD_SubGL = 0
    '                End If

    '                objPSJEDetails.dATD_Debit = 0.00
    '                objPSJEDetails.dATD_Credit = dCredit
    '                objPSJEDetails.iATD_CreatedBy = sSession.UserID
    '                objPSJEDetails.dATD_CreatedOn = DateTime.Today
    '                objPSJEDetails.sATD_Status = "A"
    '                objPSJEDetails.iATD_YearID = sSession.YearID
    '                objPSJEDetails.sATD_Operation = "U"
    '                objPSJEDetails.sATD_IPAddress = sSession.IPAddress
    '                objPSJEDetails.iATD_UpdatedBy = sSession.UserID
    '                objPSJEDetails.dATD_UpdatedOn = DateTime.Today
    '                objPSJEDetails.SaveUpdateTransactionDetails(sSession.AccessCode, objPSJEDetails)

    '            End If
    '        End If
    '        dgJEDetails.DataSource = objPSJEDetails.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistJE.SelectedValue, ddlExistJE.SelectedItem.Text)
    '        dgJEDetails.DataBind()
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
    '    End Try
    'End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim dt As New DataTable
        Try
            lblError.Text = ""

            ddlParty.SelectedIndex = 0 : ddlBillType.SelectedIndex = 0 : ddlExistJE.SelectedIndex = 0
            txtBillDate.Text = "" : txtBillAmount.Text = "" : txtNetAmount.Text = ""
            ddlPaymentType.SelectedIndex = 0 : txtAdvancePayment.Text = "" : txtBalanceAmount.Text = ""
            lblError.Text = "" : ddldbHead.SelectedIndex = 0 : ddlCrHead.SelectedIndex = 0
            'dgJEDetails.DataSource = dt
            'dgJEDetails.DataBind()
            LoadSubGL()
            ddldbGL.DataSource = dt
            ddldbGL.DataBind()

            ddlCrGL.DataSource = dt
            ddlCrGL.DataBind()

            'ddlBillNo.Items.Clear()
            ddlBillNo.SelectedIndex = 0
            ddlLocation.Items.Clear()
            'ddlParty_SelectedIndexChanged(sender, e)

            txtBillNo.Text = ""
            dgJEDetails.DataSource = Nothing
            dgJEDetails.DataBind()
            lblStatus.Text = ""
            '  imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            imgbtnSave.ImageUrl = "~/Images/Save24.png"

            dgItems.DataSource = Nothing
            dgItems.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub dgJEDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgJEDetails.ItemCommand
        Dim dt As New DataTable
        Try
            If e.CommandName = "Delete" Then
                objJE.DeletePaymentDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text)
            End If

            If e.CommandName = "Edit" Then
                lblTransID.Text = e.Item.Cells(0).Text

                'If e.Item.Cells(4).Text > 0 Then
                '    If e.Item.Cells(4).Text > 0 Then
                '        ddlPaymentType.SelectedIndex = e.Item.Cells(4).Text
                '    Else
                '        ddlPaymentType.SelectedIndex = 0
                '    End If

                '    If ddlPaymentType.SelectedIndex = "1" Then
                '        divAdvance.Visible = True : divPayment.Visible = False
                '    ElseIf ddlPaymentType.SelectedIndex = "3" Then
                '        txtNetAmount.Text = txtBillAmount.Text
                '        divAdvance.Visible = False : divPayment.Visible = True
                '    ElseIf ddlPaymentType.SelectedIndex = "4" Then
                '        divAdvance.Visible = False : divPayment.Visible = False
                '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                '    ElseIf ddlPaymentType.SelectedIndex = "0" Then
                '        divAdvance.Visible = False : divPayment.Visible = False
                '    End If
                'Else
                '    ddlPaymentType.SelectedIndex = 0
                'End If

                If e.Item.Cells(12).Text > 0 And e.Item.Cells(13).Text = 0 Then 'Debit
                    ddldbHead.SelectedIndex = 0 : ddldbGL.Items.Clear() : ddldbsUbGL.SelectedIndex = 0 : txtDebitAmount.Text = ""

                    ddlCrHead.SelectedIndex = 0 : ddlCrGL.Items.Clear() : ddlCrSubGL.SelectedIndex = 0 : txtCreditAmount.Text = ""

                    lblDbATRID.Text = e.Item.Cells(0).Text
                    'ddldbHead.SelectedValue = e.Item.Cells(1).Text
                    'If ddldbHead.SelectedIndex > 0 Then
                    '    ddldbGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbHead.SelectedValue)
                    '    ddldbGL.DataTextField = "GlDesc"
                    '    ddldbGL.DataValueField = "gl_Id"
                    '    ddldbGL.DataBind()
                    '    ddldbGL.Items.Insert(0, "Select GL Code")
                    'Else
                    '    ddldbGL.DataSource = dt
                    '    ddldbGL.DataBind()

                    '    ddldbsUbGL.DataSource = dt
                    '    ddldbsUbGL.DataBind()
                    'End If
                    'ddldbGL.SelectedValue = e.Item.Cells(2).Text

                    'If ddldbGL.SelectedIndex > 0 Then
                    '    ddldbsUbGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbGL.SelectedValue)
                    '    ddldbsUbGL.DataTextField = "GlDesc"
                    '    ddldbsUbGL.DataValueField = "gl_Id"
                    '    ddldbsUbGL.DataBind()
                    '    ddldbsUbGL.Items.Insert(0, "Select SubGL Code")
                    'Else
                    '    ddldbsUbGL.DataSource = dt
                    '    ddldbsUbGL.DataBind()
                    'End If

                    'If e.Item.Cells(3).Text > 0 Then
                    '    ddldbsUbGL.SelectedValue = e.Item.Cells(3).Text
                    'Else
                    '    ddldbsUbGL.SelectedIndex = 0
                    'End If

                    If e.Item.Cells(12).Text > 0 Then
                        txtDebitAmount.Text = e.Item.Cells(12).Text
                    End If

                ElseIf e.Item.Cells(12).Text = 0 And e.Item.Cells(13).Text > 0 Then 'Credit

                    ddldbHead.SelectedIndex = 0 : ddldbGL.Items.Clear() : ddldbsUbGL.SelectedIndex = 0 : txtDebitAmount.Text = ""

                    ddlCrHead.SelectedIndex = 0 : ddlCrGL.Items.Clear() : ddlCrSubGL.SelectedIndex = 0 : txtCreditAmount.Text = ""

                    lblCrATRID.Text = e.Item.Cells(0).Text
                    'ddlCrHead.SelectedValue = e.Item.Cells(1).Text
                    'If ddlCrHead.SelectedIndex > 0 Then
                    '    ddlCrGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedValue)
                    '    ddlCrGL.DataTextField = "GlDesc"
                    '    ddlCrGL.DataValueField = "gl_Id"
                    '    ddlCrGL.DataBind()
                    '    ddlCrGL.Items.Insert(0, "Select GL Code")
                    'Else
                    '    ddlCrGL.DataSource = dt
                    '    ddlCrGL.DataBind()

                    '    ddlCrSubGL.DataSource = dt
                    '    ddlCrSubGL.DataBind()
                    'End If
                    'ddlCrGL.SelectedValue = e.Item.Cells(2).Text

                    'If ddlCrGL.SelectedIndex > 0 Then
                    '    ddlCrSubGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue)
                    '    ddlCrSubGL.DataTextField = "GlDesc"
                    '    ddlCrSubGL.DataValueField = "gl_Id"
                    '    ddlCrSubGL.DataBind()
                    '    ddlCrSubGL.Items.Insert(0, "Select SubGL Code")
                    'Else
                    '    ddlCrSubGL.DataSource = dt
                    '    ddlCrSubGL.DataBind()
                    'End If
                    'ddlCrSubGL.SelectedValue = e.Item.Cells(3).Text

                    If e.Item.Cells(13).Text > 0 Then
                        txtCreditAmount.Text = e.Item.Cells(13).Text
                    End If

                End If

                'If txtTransactionNo.Text <> "" Then
                '    If (txtTransactionNo.Text).StartsWith("P") Then
                '        dt = objJE.GetTransactionsDetails(sSession.AccessCode, sSession.AccessCodeID, 5, e.Item.Cells(0).Text)
                '    ElseIf (txtTransactionNo.Text).StartsWith("S") Then
                '        dt = objJE.GetTransactionsDetails(sSession.AccessCode, sSession.AccessCodeID, 6, e.Item.Cells(0).Text)
                '    End If
                'End If
                'If dt.Rows.Count > 0 Then
                '    If IsDBNull(dt.Rows(0)("ATD_ID").ToString()) = False Then
                '        lblTransID.Text = dt.Rows(0)("ATD_ID").ToString()
                '    Else
                '        lblTransID.Text = "0"
                '    End If

                '    If IsDBNull(dt.Rows(0)("ATD_PaymentType").ToString()) = False Then
                '        If dt.Rows(0)("ATD_PaymentType") > 0 Then
                '            ddlPaymentType.SelectedIndex = dt.Rows(0)("ATD_PaymentType")
                '        Else
                '            ddlPaymentType.SelectedIndex = 0
                '        End If

                '        If ddlPaymentType.SelectedIndex = "1" Then
                '            divAdvance.Visible = True : divPayment.Visible = False
                '        ElseIf ddlPaymentType.SelectedIndex = "3" Then
                '            txtNetAmount.Text = txtBillAmount.Text
                '            divAdvance.Visible = False : divPayment.Visible = True
                '        ElseIf ddlPaymentType.SelectedIndex = "4" Then
                '            divAdvance.Visible = False : divPayment.Visible = False
                '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                '        ElseIf ddlPaymentType.SelectedIndex = "0" Then
                '            divAdvance.Visible = False : divPayment.Visible = False
                '        End If

                '    Else
                '        ddlPaymentType.SelectedIndex = 0
                '    End If

                '    iDbOrCr = dt.Rows(0)("ATD_DbOrCr").ToString()

                '    If iDbOrCr = 1 Then  'Debit
                '        lblDbATRID.Text = e.Item.Cells(0).Text
                '        ddldbHead.SelectedValue = dt.Rows(0)("ATD_Head").ToString()

                '        If ddldbHead.SelectedIndex > 0 Then
                '            ddldbGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbHead.SelectedValue)
                '            ddldbGL.DataTextField = "GlDesc"
                '            ddldbGL.DataValueField = "gl_Id"
                '            ddldbGL.DataBind()
                '            ddldbGL.Items.Insert(0, "Select GL Code")
                '        Else
                '            ddldbGL.DataSource = dt
                '            ddldbGL.DataBind()

                '            ddldbsUbGL.DataSource = dt
                '            ddldbsUbGL.DataBind()
                '        End If

                '        ddldbGL.SelectedValue = dt.Rows(0)("ATD_GL").ToString()
                '        If ddldbGL.SelectedIndex > 0 Then
                '            ddldbsUbGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbGL.SelectedValue)
                '            ddldbsUbGL.DataTextField = "GlDesc"
                '            ddldbsUbGL.DataValueField = "gl_Id"
                '            ddldbsUbGL.DataBind()
                '            ddldbsUbGL.Items.Insert(0, "Select SubGL Code")
                '        Else
                '            ddldbsUbGL.DataSource = dt
                '            ddldbsUbGL.DataBind()
                '        End If

                '        ddldbsUbGL.SelectedValue = dt.Rows(0)("ATD_SubGL").ToString()

                '        'If dt.Rows(0)("ATD_Debit") > 0 Then
                '        '    txtDebitAmount.Text = dt.Rows(0)("ATD_Debit")
                '        'End If

                '    End If

                '    If iDbOrCr = 2 Then  'Credit
                '        lblCrATRID.Text = e.Item.Cells(0).Text
                '        ddlCrHead.SelectedValue = dt.Rows(0)("ATD_Head").ToString()

                '        If ddlCrHead.SelectedIndex > 0 Then
                '            ddlCrGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedValue)
                '            ddlCrGL.DataTextField = "GlDesc"
                '            ddlCrGL.DataValueField = "gl_Id"
                '            ddlCrGL.DataBind()
                '            ddlCrGL.Items.Insert(0, "Select GL Code")
                '        Else
                '            ddlCrGL.DataSource = dt
                '            ddlCrGL.DataBind()

                '            ddlCrSubGL.DataSource = dt
                '            ddlCrSubGL.DataBind()
                '        End If

                '        If dt.Rows(0)("ATD_GL").ToString() = "0" Then
                '            ddlCrGL.SelectedIndex = 0
                '        Else
                '            ddlCrGL.SelectedValue = dt.Rows(0)("ATD_GL").ToString()
                '        End If

                '        If ddlCrGL.SelectedIndex > 0 Then
                '            ddlCrSubGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue)
                '            ddlCrSubGL.DataTextField = "GlDesc"
                '            ddlCrSubGL.DataValueField = "gl_Id"
                '            ddlCrSubGL.DataBind()
                '            ddlCrSubGL.Items.Insert(0, "Select SubGL Code")
                '        Else
                '            ddlCrSubGL.DataSource = dt
                '            ddlCrSubGL.DataBind()
                '        End If

                '        If dt.Rows(0)("ATD_SubGL").ToString() = "0" Then
                '            ddlCrSubGL.SelectedIndex = 0
                '        Else
                '            ddlCrSubGL.SelectedValue = dt.Rows(0)("ATD_SubGL").ToString()
                '        End If

                '        If dt.Rows(0)("ATD_Credit") > 0 Then
                '            txtCreditAmount.Text = dt.Rows(0)("ATD_Credit")
                '        End If

                '    End If
                'End If

                Dim dAvance As Double = 0, dBillAmount As Double = 0
                Dim dBalance As Double = 0
                dt = objPSJEDetails.GetPaymentTypeDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistJE.SelectedValue, ddlExistJE.SelectedItem.Text)
                If dt.Rows.Count > 0 Then
                    If ddlPaymentType.SelectedIndex = 1 Then  'Advance
                        If IsDBNull(dt.Rows(0)("AdvanceAmount").ToString()) = False Then
                            If dt.Rows(0)("AdvanceAmount").ToString() <> "0" Then
                                dAvance = dt.Rows(0)("AdvanceAmount").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(0)("BillAmount").ToString()) = False Then
                            If dt.Rows(0)("BillAmount").ToString() <> "0" Then
                                dBillAmount = dt.Rows(0)("BillAmount").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(0)("BalanceAmount").ToString()) = False Then
                            If dt.Rows(0)("BalanceAmount").ToString() <> "0" Then
                                dBalance = dt.Rows(0)("BalanceAmount").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(0)("AdvanceNaration").ToString()) = False Then
                            If dt.Rows(0)("AdvanceNaration").ToString() <> "0" Then
                                txtNarration.Text = dt.Rows(0)("AdvanceNaration").ToString()
                            End If
                        End If

                        txtAdvancePayment.Text = dAvance
                        txtBalanceAmount.Text = dBalance

                    ElseIf ddlPaymentType.SelectedIndex = 3 Then   'Payment
                        If IsDBNull(dt.Rows(0)("BillAmount").ToString()) = False Then
                            If dt.Rows(0)("BillAmount").ToString() <> "0" Then
                                dBillAmount = dt.Rows(0)("BillAmount").ToString()
                            End If
                        End If
                        txtNetAmount.Text = dBillAmount

                        If IsDBNull(dt.Rows(0)("PaymentNarration").ToString()) = False Then
                            If dt.Rows(0)("PaymentNarration").ToString() <> "0" Then
                                txtNarration.Text = dt.Rows(0)("PaymentNarration").ToString()
                            End If
                        End If
                    End If
                End If
            End If

            'dgJEDetails.DataSource = objPSJEDetails.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistJE.SelectedValue, ddlExistJE.SelectedItem.Text)
            'dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJEDetails_ItemCommand")
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If ddlExistJE.SelectedIndex > 0 Then
                objPSJEDetails.iAcc_JE_ID = ddlExistJE.SelectedValue
                objPSJEDetails.sAcc_JE_ChequeNo = txtChequeNo.Text
                objPSJEDetails.dAcc_JE_ChequeDate = txtChequeDate.Text
                objPSJEDetails.sAcc_JE_IFSCCode = txtIFSC.Text
                objPSJEDetails.sAcc_JE_BankName = txtBankName.Text
                objPSJEDetails.sAcc_JE_BranchName = txtBranchName.Text
                objPSJEDetails.sAcc_JE_BranchName = txtBranchName.Text
                objPSJEDetails.SaveChequeDetails(sSession.AccessCode, sSession.AccessCodeID, objPSJEDetails)
            End If
            lblCustomerValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFSC.Text = "" : txtBankName.Text = "" : txtBranchName.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNew_Click")
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFSC.Text = "" : txtBankName.Text = "" : txtBranchName.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnClose_Click")
        End Try
    End Sub
    Private Sub ddldbsUbGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbsUbGL.SelectedIndexChanged
        Dim iHead As Integer
        Try
            If ddldbsUbGL.SelectedIndex > 0 Then
                iHead = objJE.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddldbsUbGL.SelectedValue)
                ddldbGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead, "")
                ddldbGL.DataTextField = "GlDesc"
                ddldbGL.DataValueField = "gl_Id"
                ddldbGL.DataBind()
                ddldbGL.Items.Insert(0, "Select GL Code")

                ddldbGL.SelectedValue = objJE.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddldbsUbGL.SelectedValue)
                ddldbHead.SelectedValue = iHead
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
                iHead = objJE.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlCrSubGL.SelectedValue)
                ddlCrGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead, "")
                ddlCrGL.DataTextField = "GlDesc"
                ddlCrGL.DataValueField = "gl_Id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL Code")

                ddlCrGL.SelectedValue = objJE.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddlCrSubGL.SelectedValue)
                ddlCrHead.SelectedValue = iHead
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrSubGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgDebit_Click(sender As Object, e As ImageClickEventArgs) Handles imgDebit.Click
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sArray As Array
        Dim dtDetails As New DataTable
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

            If IsNothing(Session("DetailsGrid")) = False Then
                dtDetails = Session("DetailsGrid")
            End If

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1

                    If dtDetails.Rows(i)("Id") = lblDbATRID.Text Then
                        dRow = dt.NewRow
                        If lblDbATRID.Text <> "" Then
                            dRow("Id") = lblDbATRID.Text
                        End If
                        If ddldbHead.SelectedIndex > 0 Then
                            dRow("HeadID") = ddldbHead.SelectedValue
                        End If
                        If ddldbGL.SelectedIndex > 0 Then
                            dRow("GLID") = ddldbGL.SelectedValue
                        End If
                        If ddldbsUbGL.SelectedIndex > 0 Then
                            dRow("SubGLID") = ddldbsUbGL.SelectedValue
                        End If
                        'If ddlPaymentType.SelectedIndex > 0 Then
                        '    dRow("PaymentID") = ddlPaymentType.SelectedIndex
                        'End If
                        dRow("PaymentID") = dtDetails.Rows(i)("PaymentID")

                        dRow("SrNo") = dt.Rows.Count + 1
                        'If ddlPaymentType.SelectedIndex > 0 Then
                        '    dRow("Type") = ddlPaymentType.SelectedItem.Text
                        'End If
                        dRow("Type") = dtDetails.Rows(i)("Type")

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
                        Else
                            dRow("Debit") = 0
                        End If
                        If txtCreditAmount.Text <> "" Then
                            dRow("Credit") = txtCreditAmount.Text
                        Else
                            dRow("Credit") = 0
                        End If
                        dt.Rows.Add(dRow)

                        'dtDetails.Rows.Remove(dtDetails.Rows(i))
                        'dtDetails.AcceptChanges()
                    Else
                        dRow = dt.NewRow
                        If IsDBNull(dtDetails.Rows(i)("Id")) = False Then
                            dRow("Id") = dtDetails.Rows(i)("Id")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("HeadID")) = False Then
                            dRow("HeadID") = dtDetails.Rows(i)("HeadID")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("GLID")) = False Then
                            dRow("GLID") = dtDetails.Rows(i)("GLID")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("SubGLID")) = False Then
                            dRow("SubGLID") = dtDetails.Rows(i)("SubGLID")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("PaymentID")) = False Then
                            dRow("PaymentID") = dtDetails.Rows(i)("PaymentID")
                        End If
                        dRow("SrNo") = dt.Rows.Count + 1
                        If IsDBNull(dtDetails.Rows(i)("Type")) = False Then
                            dRow("Type") = dtDetails.Rows(i)("Type")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("GLCode")) = False Then
                            dRow("GLCode") = dtDetails.Rows(i)("GLCode")
                            dRow("GLDescription") = dtDetails.Rows(i)("GLDescription")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("SubGL")) = False Then
                            dRow("SubGL") = dtDetails.Rows(i)("SubGL")
                            dRow("SubGLDescription") = dtDetails.Rows(i)("SubGLDescription")
                        End If
                        If dtDetails.Rows(i)("Debit") > 0 Then
                            dRow("Debit") = dtDetails.Rows(i)("Debit")
                        Else
                            dRow("Debit") = 0
                        End If
                        If dtDetails.Rows(i)("Credit") > 0 Then
                            dRow("Credit") = dtDetails.Rows(i)("Credit")
                        Else
                            dRow("Credit") = 0
                        End If
                        dt.Rows.Add(dRow)

                    End If
                Next
            Else
                dRow = dt.NewRow
                If lblDbATRID.Text <> "" Then
                    dRow("Id") = lblDbATRID.Text
                Else
                    dRow("Id") = 0
                End If
                If ddldbHead.SelectedIndex > 0 Then
                    dRow("HeadID") = ddldbHead.SelectedValue
                End If
                If ddldbGL.SelectedIndex > 0 Then
                    dRow("GLID") = ddldbGL.SelectedValue
                End If
                If ddldbsUbGL.SelectedIndex > 0 Then
                    dRow("SubGLID") = ddldbsUbGL.SelectedValue
                End If
                If ddlPaymentType.SelectedIndex > 0 Then
                    dRow("PaymentID") = ddlPaymentType.SelectedIndex
                End If
                dRow("SrNo") = dt.Rows.Count + 1
                If ddlPaymentType.SelectedIndex > 0 Then
                    dRow("Type") = ddlPaymentType.SelectedItem.Text
                End If
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
                Else
                    dRow("Debit") = 0
                End If
                If txtCreditAmount.Text <> "" Then
                    dRow("Credit") = txtCreditAmount.Text
                Else
                    dRow("Credit") = 0
                End If
                dt.Rows.Add(dRow)

            End If

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()
            Session("DetailsGrid") = dt
            txtDebitAmount.Text = ""
            'ddldbHead.SelectedIndex = 0 : ddldbGL.Items.Clear() : ddldbsUbGL.SelectedIndex = 0 : txtDebitAmount.Text = ""

            'dtMerge = Session("DbDataTable")
            'If IsNothing(dtMerge) = True Then
            '    dtMerge = dt
            'End If

            'dt.Columns.Add("ID")
            'dt.Columns.Add("HeadID")
            'dt.Columns.Add("GLID")
            'dt.Columns.Add("SubGLID")
            'dt.Columns.Add("PaymentID")
            'dt.Columns.Add("SrNo")
            'dt.Columns.Add("Type")
            'dt.Columns.Add("GLCode")
            'dt.Columns.Add("GLDescription")
            'dt.Columns.Add("SubGL")
            'dt.Columns.Add("SubGLDescription")
            'dt.Columns.Add("OpeningBalance")
            'dt.Columns.Add("Debit")
            'dt.Columns.Add("Credit")
            'dt.Columns.Add("Balance")

            'dRow = dt.NewRow

            'If ddldbHead.SelectedIndex > 0 Then
            '    dRow("HeadID") = ddldbHead.SelectedValue
            'End If

            'If ddldbGL.SelectedIndex > 0 Then
            '    dRow("GLID") = ddldbGL.SelectedValue
            'End If

            'If ddldbsUbGL.SelectedIndex > 0 Then
            '    dRow("SubGLID") = ddldbsUbGL.SelectedValue
            'End If

            'If ddlPaymentType.SelectedIndex > 0 Then
            '    dRow("PaymentID") = ddlPaymentType.SelectedValue
            'End If

            'dRow("SrNo") = dtMerge.Rows.Count + 1

            'If ddlPaymentType.SelectedIndex > 0 Then
            '    dRow("Type") = ddlPaymentType.SelectedItem.Text
            'End If

            'If ddldbGL.SelectedIndex > 0 Then
            '    sArray = ddldbGL.SelectedItem.Text.Split("-")
            '    dRow("GLCode") = sArray(0)
            '    dRow("GLDescription") = sArray(1)
            'End If

            'If ddldbsUbGL.SelectedIndex > 0 Then
            '    sArray = ddldbsUbGL.SelectedItem.Text.Split("-")
            '    dRow("SubGL") = sArray(0)
            '    dRow("SubGLDescription") = sArray(1)
            'End If

            'If txtDebitAmount.Text <> "" Then
            '    dRow("Debit") = txtDebitAmount.Text
            'End If
            'dt.Rows.Add(dRow)

            'Session("DbDataTable") = dt

            'dt.Merge(dtMerge)
            'dt.AcceptChanges()

            'dt.DefaultView.Sort = "SrNo"

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgDebit_Click")
        End Try
    End Sub
    Private Sub imgCredit_Click(sender As Object, e As ImageClickEventArgs) Handles imgCredit.Click
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sArray As Array
        Dim dtDetails As New DataTable
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

            If IsNothing(Session("DetailsGrid")) = False Then
                dtDetails = Session("DetailsGrid")
            End If

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1

                    If dtDetails.Rows(i)("Id") = lblCrATRID.Text Then
                        dRow = dt.NewRow
                        If lblCrATRID.Text <> "" Then
                            dRow("Id") = lblCrATRID.Text
                        End If
                        If ddlCrHead.SelectedIndex > 0 Then
                            dRow("HeadID") = ddlCrHead.SelectedValue
                        End If
                        If ddlCrGL.SelectedIndex > 0 Then
                            dRow("GLID") = ddlCrGL.SelectedValue
                        End If
                        If ddlCrSubGL.SelectedIndex > 0 Then
                            dRow("SubGLID") = ddlCrSubGL.SelectedValue
                        End If
                        If ddlPaymentType.SelectedIndex > 0 Then
                            dRow("PaymentID") = ddlPaymentType.SelectedIndex
                        End If
                        dRow("SrNo") = dt.Rows.Count + 1
                        If ddlPaymentType.SelectedIndex > 0 Then
                            dRow("Type") = ddlPaymentType.SelectedItem.Text
                        End If
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
                        If txtDebitAmount.Text <> "" Then
                            dRow("Debit") = txtDebitAmount.Text
                        Else
                            dRow("Debit") = 0
                        End If
                        If txtCreditAmount.Text <> "" Then
                            dRow("Credit") = txtCreditAmount.Text
                        Else
                            dRow("Credit") = 0
                        End If
                        dt.Rows.Add(dRow)

                        'dtDetails.Rows.Remove(dtDetails.Rows(i))
                        'dtDetails.AcceptChanges()
                    Else
                        dRow = dt.NewRow
                        If IsDBNull(dtDetails.Rows(i)("Id")) = False Then
                            dRow("Id") = dtDetails.Rows(i)("Id")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("HeadID")) = False Then
                            dRow("HeadID") = dtDetails.Rows(i)("HeadID")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("GLID")) = False Then
                            dRow("GLID") = dtDetails.Rows(i)("GLID")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("SubGLID")) = False Then
                            dRow("SubGLID") = dtDetails.Rows(i)("SubGLID")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("PaymentID")) = False Then
                            dRow("PaymentID") = dtDetails.Rows(i)("PaymentID")
                        End If
                        dRow("SrNo") = dt.Rows.Count + 1
                        If IsDBNull(dtDetails.Rows(i)("Type")) = False Then
                            dRow("Type") = dtDetails.Rows(i)("Type")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("GLCode")) = False Then
                            dRow("GLCode") = dtDetails.Rows(i)("GLCode")
                            dRow("GLDescription") = dtDetails.Rows(i)("GLDescription")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("SubGL")) = False Then
                            dRow("SubGL") = dtDetails.Rows(i)("SubGL")
                            dRow("SubGLDescription") = dtDetails.Rows(i)("SubGLDescription")
                        End If
                        If dtDetails.Rows(i)("Debit") > 0 Then
                            dRow("Debit") = dtDetails.Rows(i)("Debit")
                        Else
                            dRow("Debit") = 0
                        End If
                        If dtDetails.Rows(i)("Credit") > 0 Then
                            dRow("Credit") = dtDetails.Rows(i)("Credit")
                        Else
                            dRow("Credit") = 0
                        End If
                        dt.Rows.Add(dRow)

                    End If
                Next
            Else
                dRow = dt.NewRow
                If lblCrATRID.Text <> "" Then
                    dRow("Id") = lblCrATRID.Text
                Else
                    dRow("Id") = 0
                End If
                If ddlCrHead.SelectedIndex > 0 Then
                    dRow("HeadID") = ddlCrHead.SelectedValue
                End If
                If ddlCrGL.SelectedIndex > 0 Then
                    dRow("GLID") = ddlCrGL.SelectedValue
                End If
                If ddlCrSubGL.SelectedIndex > 0 Then
                    dRow("SubGLID") = ddlCrSubGL.SelectedValue
                End If
                If ddlPaymentType.SelectedIndex > 0 Then
                    dRow("PaymentID") = ddlPaymentType.SelectedIndex
                End If
                dRow("SrNo") = dt.Rows.Count + 1
                If ddlPaymentType.SelectedIndex > 0 Then
                    dRow("Type") = ddlPaymentType.SelectedItem.Text
                End If
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
                If txtDebitAmount.Text <> "" Then
                    dRow("Debit") = txtDebitAmount.Text
                Else
                    dRow("Debit") = 0
                End If
                If txtCreditAmount.Text <> "" Then
                    dRow("Credit") = txtCreditAmount.Text
                Else
                    dRow("Credit") = 0
                End If
                dt.Rows.Add(dRow)
            End If

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()
            Session("DetailsGrid") = dt
            txtCreditAmount.Text = ""
            'ddlCrHead.SelectedIndex = 0 : ddlCrGL.Items.Clear() : ddlCrSubGL.SelectedIndex = 0 : txtCreditAmount.Text = ""

            'dtMerge = Session("CrDataTable")
            'If IsNothing(dtMerge) = True Then
            '    dtMerge = dt
            'End If

            'dt.Columns.Add("ID")
            'dt.Columns.Add("HeadID")
            'dt.Columns.Add("GLID")
            'dt.Columns.Add("SubGLID")
            'dt.Columns.Add("PaymentID")
            'dt.Columns.Add("SrNo")
            'dt.Columns.Add("Type")
            'dt.Columns.Add("GLCode")
            'dt.Columns.Add("GLDescription")
            'dt.Columns.Add("SubGL")
            'dt.Columns.Add("SubGLDescription")
            'dt.Columns.Add("OpeningBalance")
            'dt.Columns.Add("Debit")
            'dt.Columns.Add("Credit")
            'dt.Columns.Add("Balance")

            'dRow = dt.NewRow

            'If ddlCrHead.SelectedIndex > 0 Then
            '    dRow("HeadID") = ddlCrHead.SelectedValue
            'End If

            'If ddlCrGL.SelectedIndex > 0 Then
            '    dRow("GLID") = ddlCrGL.SelectedValue
            'End If

            'If ddlCrSubGL.SelectedIndex > 0 Then
            '    dRow("SubGLID") = ddlCrSubGL.SelectedValue
            'End If

            'If ddlPaymentType.SelectedIndex > 0 Then
            '    dRow("PaymentID") = ddlPaymentType.SelectedValue
            'End If

            'dRow("SrNo") = dtMerge.Rows.Count + 1

            'If ddlPaymentType.SelectedIndex > 0 Then
            '    dRow("Type") = ddlPaymentType.SelectedItem.Text
            'End If

            'If ddlCrGL.SelectedIndex > 0 Then
            '    sArray = ddlCrGL.SelectedItem.Text.Split("-")
            '    dRow("GLCode") = sArray(0)
            '    dRow("GLDescription") = sArray(1)
            'End If

            'If ddlCrSubGL.SelectedIndex > 0 Then
            '    sArray = ddlCrSubGL.SelectedItem.Text.Split("-")
            '    dRow("SubGL") = sArray(0)
            '    dRow("SubGLDescription") = sArray(1)
            'End If

            'If txtCreditAmount.Text <> "" Then
            '    dRow("Credit") = txtCreditAmount.Text
            'End If
            'dt.Rows.Add(dRow)

            'Session("CrDataTable") = dt

            'dt.Merge(dtMerge)
            'dt.AcceptChanges()

            'dt.DefaultView.Sort = "SrNo"

            'dgJEDetails.DataSource = dt
            'dgJEDetails.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgCredit_Click")
        End Try
    End Sub
    'Private Sub ddlBillNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBillNo.SelectedIndexChanged
    '    Dim i As Integer = 0
    '    Dim sBillNo As String = ""
    '    Dim sBillName As String = ""
    '    Try
    '        If ddlBillNo.Items.Count > 0 Then
    '            For i = 0 To ddlBillNo.Items.Count - 1
    '                If ddlBillNo.Items(i).Selected = True Then
    '                    sBillNo = sBillNo & "," & ddlBillNo.Items(i).Value
    '                    sBillName = sBillName & "," & ddlBillNo.Items(i).Text
    '                End If
    '            Next
    '            If (txtTransactionNo.Text).StartsWith("P") Then
    '                If sBillNo <> "" Then
    '                    txtBillNo.Text = sBillName
    '                    txtBillAmount.Text = loaddetails(sBillNo, 0)
    '                Else
    '                    txtBillAmount.Text = ""
    '                End If
    '            ElseIf (txtTransactionNo.Text).StartsWith("S") Then
    '                If sBillNo <> "" Then
    '                    txtBillNo.Text = sBillName
    '                    If sBillNo.StartsWith(",") Then
    '                        sBillNo = sBillNo.Remove(0, 1)
    '                    End If
    '                    If sBillNo.StartsWith(",") Then
    '                        sBillNo = sBillNo.Remove(Len(sBillNo) - 1, 1)
    '                    End If
    '                    txtBillAmount.Text = LoadSalesDetails(sBillNo)
    '                Else
    '                    txtBillAmount.Text = ""
    '                End If
    '            End If

    '        End If
    '    Catch ex As Exception
    '        'lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBillNo_SelectedIndexChanged")
    '    End Try
    'End Sub
    Protected Function LoadSalesDetails(ByVal sDispatchNo As String) As DataTable
        Dim dtDetails, dt As New DataTable
        Dim dTotalAmt As Double
        Dim dVatAmt As Double : Dim dCSTAmt As Double : Dim dExciseAmt As Double : Dim dBasicPrice As Double
        Dim dtR As New DataTable
        Dim dRow As DataRow
        Try
            dtDetails = objPSJEDetails.GetAllSalesDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sDispatchNo)
            dt = GetSinkGrid(dtDetails)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dTotalAmt = dTotalAmt + dt.Rows(i)("Total")
                    dVatAmt = dVatAmt + dt.Rows(i)("VATAmt")
                    dCSTAmt = dCSTAmt + dt.Rows(i)("CSTAmt")
                    dExciseAmt = dExciseAmt + dt.Rows(i)("ExciseAmt")
                    dBasicPrice = dBasicPrice + dt.Rows(i)("BasicPrice")
                Next
            End If

            dtR.Columns.Add("Total")
            dtR.Columns.Add("VATAmt")
            dtR.Columns.Add("CSTAmt")
            dtR.Columns.Add("ExciseAmt")
            dtR.Columns.Add("BasicPrice")

            dRow = dtR.NewRow
            dRow("Total") = dTotalAmt
            dRow("VATAmt") = dVatAmt
            dRow("CSTAmt") = dCSTAmt
            dRow("ExciseAmt") = dExciseAmt
            dRow("BasicPrice") = dBasicPrice
            dtR.Rows.Add(dRow)

            Return dtR
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Function GetSinkGrid(ByVal dtData As DataTable) As DataTable
        Dim dt, dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim sOrderNO As String = "" : Dim sCommodity As String = ""

        Dim iTotalOrderedQty, iTotalAllocatedQty, iTotalDispatchedQty, iTotalPendingQty As Double
        Dim dTotalDiscount, dTotalDiscountAmt, dMRPRate As Double
        Dim dTotalVATAmt As Double
        Dim dTotalCST, dTotalCSTAmt As Double
        Dim dTotalExcise, dTotalExciseAmt As Double
        Dim dBasicAmt, dTotalNetAmt As Double

        Dim iOrderID As Integer
        Dim dtDetails As New DataTable
        Dim sOrderID As String = "" : Dim sParty As String = "" : Dim sOrderDate As String = "" : Dim sDispatchDate As String = ""
        Dim dShipping, dTradeDiscount, dTradeDiscountAmt As Double

        Dim sDispatchNo As String = "" : Dim sDispatchRefNo As String = ""
        Dim dUnitTotal, dVAT, dCST, dExcise As Double
        Dim dTotalVAT As Double

        Dim sVat As String = "" : Dim sVatD As String = ""
        Dim sVatA As String = "" : Dim sVatDA As String = ""
        Dim dVatSingleAmt As Double
        Dim dLastVatAmt As Double
        Dim sDisplayVat As String = ""

        Dim sCst As String = "" : Dim sCstD As String = ""
        Dim sCstA As String = "" : Dim sCstDA As String = ""
        Dim dCstSingleAmt As Double
        Dim dLastCstAmt As Double
        Dim sDisplayCst As String = ""

        Dim sArrayV As String() : Dim sbretV As String = "" : Dim sRVat As String = ""
        Dim sArrayC As String() : Dim sbretC As String = "" : Dim sRCst As String = ""

        Dim ReturnStr, temp, ReturnStrC, tempC As String
        Dim sInvoice, sInvoiceStr As String : Dim sInvoiceDate, sInvoiceDateStr As String

        Dim sStrDRefNo As String = ""
        Try
            dt1.Columns.Add("OrderNo")
            dt1.Columns.Add("Commodity")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("OrderDate")
            dt1.Columns.Add("Party")
            dt1.Columns.Add("DispatchedNo")
            dt1.Columns.Add("DispatchRefNo")
            dt1.Columns.Add("DispatchedDate")
            dt1.Columns.Add("ShippingRate")
            dt1.Columns.Add("MRPRate")
            dt1.Columns.Add("OrderedQty")
            dt1.Columns.Add("AllocatedQty")
            dt1.Columns.Add("PendingQty")
            dt1.Columns.Add("DispatchedQty")
            dt1.Columns.Add("TradeDiscount")
            dt1.Columns.Add("TradeDiscountAmt")
            dt1.Columns.Add("Discount")
            dt1.Columns.Add("DiscountAmt")
            dt1.Columns.Add("VAT")
            dt1.Columns.Add("VATAmt")
            dt1.Columns.Add("CST")
            dt1.Columns.Add("CSTAmt")
            dt1.Columns.Add("Excise")
            dt1.Columns.Add("ExciseAmt")
            dt1.Columns.Add("BasicPrice")
            dt1.Columns.Add("Total")

            dt = dtData
            Dim dview As New DataView(dt)

            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    iOrderID = dt.Rows(j)("SDM_OrderID")

                    'sStrDRefNo = clsSalesDynamicReport.GetDispatchRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)

                    If sOrderID.Contains(iOrderID) = False Then
                        If (iOrderID > 0) Then
                            dview = dt.DefaultView
                            dview.RowFilter = "SDM_OrderID='" & iOrderID & "'"
                            dtDetails = dview.ToTable
                            sOrderID = sOrderID & "," & iOrderID

                            If dtDetails.Rows.Count > 0 Then
                                For i = 0 To dtDetails.Rows.Count - 1
                                    dRow = dt1.NewRow

                                    dRow("OrderNo") = dtDetails.Rows(i)("SPO_OrderCode")
                                    If dRow("OrderNo") = sOrderNO Then
                                        dRow("OrderNo") = ""
                                    End If
                                    sOrderNO = dtDetails.Rows(i)("SPO_OrderCode")

                                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                                    If dRow("Commodity") = sCommodity Then
                                        dRow("Commodity") = ""
                                    End If
                                    sCommodity = dtDetails.Rows(i)("Commodity")

                                    dRow("Description") = dtDetails.Rows(i)("Item")

                                    'dRow("OrderDate") = clsTRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_OrderDate"), "D")
                                    'sOrderDate = clsTRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_OrderDate"), "D")

                                    dRow("Party") = dtDetails.Rows(i)("Party")
                                    sParty = dtDetails.Rows(i)("Party")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_Code")) = False Then
                                        dRow("DispatchedNo") = dtDetails.Rows(i)("SDM_Code")
                                        sDispatchNo = sDispatchNo & "," & dtDetails.Rows(i)("SDM_Code")
                                    Else
                                        dRow("DispatchedNo") = ""
                                        sDispatchNo = sDispatchNo & "," & ""
                                    End If

                                    'If IsDBNull(dtDetails.Rows(i)("SDM_DispatchRefNo")) = False Then
                                    '    dRow("DispatchRefNo") = dtDetails.Rows(i)("SDM_DispatchRefNo")
                                    '    sDispatchRefNo = sDispatchRefNo & "," & dtDetails.Rows(i)("SDM_DispatchRefNo")
                                    'Else
                                    '    dRow("DispatchRefNo") = ""
                                    '    sDispatchRefNo = sDispatchRefNo & "," & ""
                                    'End If
                                    sDispatchRefNo = sStrDRefNo

                                    'dRow("DispatchedDate") = clsTRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_DispatchDate"), "D")
                                    'sDispatchDate = sDispatchDate & "," & clsTRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_DispatchDate"), "D")

                                    dShipping = dShipping + dtDetails.Rows(i)("SDM_ShippingRate")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_ShippingRate")) = False Then
                                        dRow("ShippingRate") = dtDetails.Rows(i)("SDM_ShippingRate")
                                    Else
                                        dRow("ShippingRate") = ""
                                    End If


                                    dRow("MRPRate") = dtDetails.Rows(i)("SDD_Rate")
                                    dMRPRate = dMRPRate + dRow("MRPRate")

                                    dRow("OrderedQty") = dtDetails.Rows(i)("SPOD_Quantity")
                                    'iTotalOrderedQty = iTotalOrderedQty + dRow("OrderedQty")
                                    'iTotalOrderedQty = dRow("OrderedQty")
                                    'iTotalOrderedQty = clsSalesDynamicReport.GetDispatchedOrderTotalQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dt.Rows(j)("SDM_ID"))

                                    If IsDBNull(dtDetails.Rows(i)("SAD_PlacedQnt")) = False Then
                                        dRow("AllocatedQty") = dtDetails.Rows(i)("SAD_PlacedQnt")
                                        iTotalAllocatedQty = iTotalAllocatedQty + dRow("AllocatedQty")
                                    End If

                                    'If IsDBNull(dtDetails.Rows(i)("SAD_PlacedQnt")) = False Then
                                    '    dRow("PendingQty") = dtDetails.Rows(i)("SPOD_Quantity") - dtDetails.Rows(i)("SAD_PlacedQnt")
                                    '    iTotalPendingQty = iTotalPendingQty + dRow("PendingQty")
                                    'End If
                                    dRow("PendingQty") = dtDetails.Rows(i)("SAD_PendingQty")

                                    dRow("DispatchedQty") = dtDetails.Rows(i)("SDD_Quantity")
                                    iTotalDispatchedQty = iTotalDispatchedQty + dRow("DispatchedQty")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_GrandDiscount")) = False Then
                                        'dRow("TradeDiscount") = dtDetails.Rows(i)("SDM_GrandDiscount")
                                        dRow("TradeDiscount") = ""
                                        dTradeDiscount = dtDetails.Rows(i)("SDM_GrandDiscount")
                                    Else
                                        'dRow("TradeDiscount") = 0
                                        dRow("TradeDiscount") = ""
                                    End If

                                    If IsDBNull(dtDetails.Rows(i)("SDM_GrandDiscountAmt")) = False Then
                                        'dRow("TradeDiscountAmt") = dtDetails.Rows(i)("SDM_GrandDiscountAmt")
                                        dRow("TradeDiscountAmt") = ""
                                        dTradeDiscountAmt = dtDetails.Rows(i)("SDM_GrandDiscountAmt")
                                    Else
                                        'dRow("TradeDiscountAmt") = 0
                                        dRow("TradeDiscountAmt") = ""
                                    End If

                                    dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Discount")))
                                    'dTotalDiscount = dTotalDiscount + dRow("Discount")
                                    dTotalDiscount = dRow("Discount")

                                    dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_DiscountAmount")))
                                    dTotalDiscountAmt = dTotalDiscountAmt + dRow("DiscountAmt")

                                    sVat = sVat & "," & dtDetails.Rows(i)("SDD_Vat")
                                    'dRow("VAT") = dtDetails.Rows(i)("SDD_Vat")
                                    'dTotalVAT = dTotalVAT + dRow("VAT")

                                    'Now Commented'
                                    'sVat = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Vat")))
                                    'If sVatD.Contains(sVat) Then
                                    'Else
                                    '    sVatD = sVatD & "," & String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Vat")))
                                    'End If
                                    'Now Commented'

                                    'sVatA = dtDetails.Rows(i)("SDD_Vat")
                                    'If sVatDA.Contains(sVatA) Then
                                    '    dTotalVATAmt = dTotalVATAmt + dtDetails.Rows(i)("SDD_VatAmount")
                                    'Else
                                    '    sVatDA = sVatDA & "," & dtDetails.Rows(i)("SDD_Vat")
                                    '    dVatSingleAmt = dVatSingleAmt + dtDetails.Rows(i)("SDD_VatAmount")
                                    'End If

                                    'Now Commented'
                                    'dRow("VAT") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Vat")))
                                    'If dRow("VAT") = sDisplayVat Then
                                    '    dTotalVATAmt = dTotalVATAmt + String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_VatAmount")))
                                    'Else
                                    '    sVatDA = sVatDA & "," & String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Vat")))
                                    '    dVatSingleAmt = dVatSingleAmt + String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_VatAmount")))
                                    'End If
                                    'If dtDetails.Rows.Count - 1 = i Then
                                    'Else
                                    '    sDisplayVat = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i + 1)("SDD_Vat")))
                                    'End If
                                    'Now Commented'

                                    'String.Format("{0:0.00}", Convert.ToDecimal())
                                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_VatAmount")))
                                    dLastVatAmt = dLastVatAmt + dRow("VATAmt")

                                    sCst = sCst & "," & dtDetails.Rows(i)("SDD_CST")

                                    'sCst = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CST")))
                                    'If sCstD.Contains(sCst) Then
                                    'Else
                                    '    sCstD = sCstD & "," & String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CST")))
                                    'End If

                                    'dRow("CST") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CST")))
                                    'If dRow("CST") = sDisplayCst Then
                                    '    dTotalCSTAmt = dTotalCSTAmt + String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CSTAmount")))
                                    'Else
                                    '    sCstDA = sCstDA & "," & String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CST")))
                                    '    dCstSingleAmt = dCstSingleAmt + String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CSTAmount")))
                                    'End If
                                    'If dtDetails.Rows.Count - 1 = i Then
                                    'Else
                                    '    sDisplayCst = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i + 1)("SDD_CST")))
                                    'End If

                                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CSTAmount")))
                                    dLastCstAmt = dLastCstAmt + dRow("CSTAmt")

                                    'dRow("CST") = dtDetails.Rows(i)("SDD_CST")
                                    'dTotalCST = dTotalCST + dRow("CST")

                                    'dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CSTAmount")))
                                    'dTotalCSTAmt = dTotalCSTAmt + dRow("CSTAmt")

                                    dRow("Excise") = dtDetails.Rows(i)("SDD_Excise")
                                    dTotalExcise = dTotalExcise + dRow("Excise")

                                    dRow("ExciseAmt") = dtDetails.Rows(i)("SDD_ExciseAmount")
                                    dTotalExciseAmt = dTotalExciseAmt + dRow("ExciseAmt")

                                    'dRow("Total") = dtDetails.Rows(i)("SDD_TotalAmount")
                                    'dTotalNetAmt = dTotalNetAmt + dRow("Total")

                                    'dRow("BasicPrice") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount")))
                                    'dBasicAmt = dBasicAmt + dRow("BasicPrice")

                                    If dtDetails.Rows(i)("SDD_CSTAmount") > 0 Then
                                        dRow("BasicPrice") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") - dtDetails.Rows(i)("SDD_CSTAmount")))
                                        dBasicAmt = dBasicAmt + dRow("BasicPrice")
                                    Else
                                        dRow("BasicPrice") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") - dtDetails.Rows(i)("SDD_VatAmount")))
                                        dBasicAmt = dBasicAmt + dRow("BasicPrice")
                                    End If

                                    dUnitTotal = dtDetails.Rows(i)("SDD_TotalAmount")
                                    dVAT = dtDetails.Rows(i)("SDD_VatAmount")
                                    dCST = dtDetails.Rows(i)("SDD_CSTAmount")
                                    dExcise = dtDetails.Rows(i)("SDD_ExciseAmount")

                                    'If IsDBNull(dtDetails.Rows(i)("SDD_CSTAmount")) = False And dtDetails.Rows(i)("SDD_CSTAmount") > 0 Then
                                    '    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dUnitTotal + dCST + dExcise))
                                    '    dTotalNetAmt = dTotalNetAmt + dRow("BasicPrice")
                                    'Else
                                    '    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dUnitTotal + dVAT + dExcise))
                                    '    dTotalNetAmt = dTotalNetAmt + dRow("BasicPrice")
                                    'End If

                                    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dUnitTotal + dExcise))
                                    dTotalNetAmt = dTotalNetAmt + dRow("BasicPrice")

                                    dUnitTotal = 0 : dVAT = 0 : dCST = 0 : dExcise = 0
                                    'dt1.Rows.Add(dRow)
                                Next

                                If sVat.StartsWith(",") Then
                                    sVat = sVat.Remove(0, 1)
                                End If
                                If sVat.EndsWith(",") Then
                                    sVat = sVat.Remove(Len(sVat) - 1, 1)
                                End If

                                sbretV = sVat
                                sArrayV = sbretV.Split(",")
                                For i = 0 To sArrayV.Length - 1
                                    If sArrayV(i) <> "0.0000" And sArrayV(i) <> "0" Then
                                        sRVat = sRVat & "," & sArrayV(i)
                                    End If
                                Next
                                If sRVat.StartsWith(",") Then
                                    sRVat = sRVat.Remove(0, 1)
                                End If
                                If sRVat.EndsWith(",") Then
                                    sRVat = sRVat.Remove(Len(sRVat) - 1, 1)
                                End If

                                ReturnStr = sRVat
                                temp = String.Join(",", ReturnStr.Split(","c).Distinct().ToArray())

                                If sCst.StartsWith(",") Then
                                    sCst = sCst.Remove(0, 1)
                                End If
                                If sCst.EndsWith(",") Then
                                    sCst = sCst.Remove(Len(sCst) - 1, 1)
                                End If

                                sbretC = sCst
                                sArrayC = sbretC.Split(",")
                                For i = 0 To sArrayC.Length - 1
                                    If sArrayC(i) <> "0.0000" And sArrayC(i) <> "0" Then
                                        sRCst = sRCst & "," & sArrayC(i)
                                    End If
                                Next
                                If sRCst.StartsWith(",") Then
                                    sRCst = sRCst.Remove(0, 1)
                                End If
                                If sRCst.EndsWith(",") Then
                                    sRCst = sRCst.Remove(Len(sRCst) - 1, 1)
                                End If

                                ReturnStrC = sRCst
                                tempC = String.Join(",", ReturnStrC.Split(","c).Distinct().ToArray())


                                If sDispatchNo.StartsWith(",") Then
                                    sDispatchNo = sDispatchNo.Remove(0, 1)
                                End If
                                If sDispatchNo.EndsWith(",") Then
                                    sDispatchNo = sDispatchNo.Remove(Len(sDispatchNo) - 1, 1)
                                End If

                                If sDispatchRefNo.StartsWith(",") Then
                                    sDispatchRefNo = sDispatchRefNo.Remove(0, 1)
                                End If
                                If sDispatchRefNo.EndsWith(",") Then
                                    sDispatchRefNo = sDispatchRefNo.Remove(Len(sDispatchRefNo) - 1, 1)
                                End If

                                If sDispatchDate.StartsWith(",") Then
                                    sDispatchDate = sDispatchDate.Remove(0, 1)
                                End If
                                If sDispatchDate.EndsWith(",") Then
                                    sDispatchDate = sDispatchDate.Remove(Len(sDispatchDate) - 1, 1)
                                End If

                                sInvoiceStr = sDispatchNo
                                sInvoice = String.Join(",", sInvoiceStr.Split(","c).Distinct().ToArray())

                                sInvoiceDateStr = sDispatchDate
                                sInvoiceDate = String.Join(",", sInvoiceDateStr.Split(","c).Distinct().ToArray())

                                dRow = dt1.NewRow
                                dRow("OrderNo") = "<B>" & sOrderNO & "</B>"
                                dRow("Commodity") = ""
                                dRow("Description") = ""
                                dRow("OrderDate") = sOrderDate
                                dRow("Party") = sParty
                                dRow("DispatchedNo") = sInvoice
                                dRow("DispatchRefNo") = sDispatchRefNo
                                dRow("DispatchedDate") = sInvoiceDate
                                dRow("ShippingRate") = "<B>" & dShipping & "</B>"
                                dRow("MRPRate") = ""
                                dRow("OrderedQty") = "<B>" & iTotalOrderedQty & "</B>"
                                dRow("AllocatedQty") = "<B>" & iTotalAllocatedQty & "</B>"
                                dRow("PendingQty") = "<B>" & iTotalOrderedQty - iTotalAllocatedQty & "</B>"
                                dRow("DispatchedQty") = "<B>" & iTotalDispatchedQty & "</B>"
                                dRow("TradeDiscount") = "<B>" & dTradeDiscount & "</B>"
                                dRow("TradeDiscountAmt") = "<B>" & dTradeDiscountAmt & "</B>"
                                dRow("Discount") = "<B>" & dTotalDiscount & "</B>"
                                dRow("DiscountAmt") = "<B>" & dTotalDiscountAmt & "</B>"
                                dRow("VAT") = ""
                                '"<B>" & temp & "</B>"
                                dRow("VATAmt") = dLastVatAmt
                                dRow("CST") = ""
                                '"<B>" & tempC & "</B>"
                                dRow("CSTAmt") = dLastCstAmt
                                dRow("Excise") = ""
                                '"<B>" & dTotalExcise & "</B>"
                                dRow("ExciseAmt") = dTotalExciseAmt
                                dRow("BasicPrice") = dBasicAmt
                                If dLastCstAmt > 0 Then
                                    dRow("Total") = ((dTotalNetAmt - dTradeDiscountAmt) + dLastCstAmt + dTotalExciseAmt + dShipping)
                                Else
                                    dRow("Total") = ((dTotalNetAmt - dTradeDiscountAmt) + dLastVatAmt + dTotalExciseAmt + dShipping)
                                End If

                                dt1.Rows.Add(dRow)

                                dShipping = 0 : dTradeDiscountAmt = 0 : dTradeDiscountAmt = 0
                                dMRPRate = 0 : iTotalOrderedQty = 0 : iTotalAllocatedQty = 0 : iTotalDispatchedQty = 0 : dTotalDiscount = 0 : dTotalDiscountAmt = 0
                                dTotalVAT = 0 : dTotalVATAmt = 0 : dTotalCST = 0 : dTotalCSTAmt = 0 : dTotalExcise = 0 : dTotalExciseAmt = 0 : dTotalNetAmt = 0 : iTotalPendingQty = 0
                                sDispatchNo = "" : sDispatchRefNo = "" : sDispatchDate = ""
                                sVat = "" : sVatD = "" : sDisplayVat = "" : sVatDA = "" : dVatSingleAmt = 0 : dLastVatAmt = 0 : dBasicAmt = 0
                                sCst = "" : sCstD = "" : sDisplayCst = "" : sCstDA = "" : dCstSingleAmt = 0 : dLastCstAmt = 0
                                temp = "" : tempC = "" : ReturnStr = "" : ReturnStrC = "" : sRVat = "" : sRCst = ""

                                sInvoice = "" : sInvoiceStr = "" : sInvoiceDate = "" : sInvoiceDateStr = ""
                                sStrDRefNo = ""
                            End If

                        End If

                    End If
                Next
            End If
            'Session("Report") = dt1
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
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
            dt4 = objPSJEDetails.loadDetails(sSession.AccessCode, sSession.AccessCodeID, sOrderID, BillNo)
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
            Duniqe = objPSJEDetails.RemoveDublicate(dt5)
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
    'Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
    '    Dim iPaymentType As Integer = 0, iTransID As Integer = 0
    '    Dim dDebit As Double = 0, dCredit As Double = 0
    '    Dim iRet As Integer = 0
    '    Dim Arr() As String
    '    Try

    '        iRet = CheckDebitAndCredit()

    '        If iRet = 1 Then
    '            lblCustomerValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            Exit Sub
    '        ElseIf iRet = 2 Then
    '            lblCustomerValidationMsg.Text = "Amount Not Matched with Advance Payment."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            Exit Sub
    '        ElseIf iRet = 3 Then
    '            lblCustomerValidationMsg.Text = "Amount Not Matched with Payment."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            Exit Sub
    '        ElseIf iRet = 4 Then
    '            lblCustomerValidationMsg.Text = "Total Debit Amount Not Matched with Invoice Total Bill Amount."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            Exit Sub
    '        ElseIf iRet = 5 Then
    '            lblCustomerValidationMsg.Text = "Total Credit Amount Not Matched with Invoice Total Bill Amount."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            Exit Sub
    '        End If

    '        If (ddlPaymentType.SelectedIndex = 1) Or (ddlPaymentType.SelectedIndex = 2) Or (ddlPaymentType.SelectedIndex = 3) Then
    '            If ddldbHead.SelectedIndex = 0 Then
    '                lblCustomerValidationMsg.Text = "Enter Debit Details."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If ddlCrHead.SelectedIndex = 0 Then
    '                lblCustomerValidationMsg.Text = "Enter Credit Details."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)
    '                Exit Sub
    '            End If
    '        End If

    '        If ddlExistJE.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_ID = ddlExistJE.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_ID = 0
    '        End If

    '        objPSJEDetails.sAcc_JE_TransactionNo = txtTransactionNo.Text

    '        If ddlParty.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_Party = ddlParty.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_Party = 0
    '        End If

    '        If ddlLocation.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_Location = ddlLocation.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_Location = 0
    '        End If

    '        If ddlBillType.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_BillType = ddlBillType.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_BillType = 0
    '        End If
    '        'Commented'
    '        'objPSJEDetails.iAcc_JE_InvoiceID = ddlBillNo.SelectedValue
    '        'Commented'

    '        'objPSJEDetails.sAcc_JE_BillNo = ddlBillNo.SelectedItem.Text
    '        objPSJEDetails.sAcc_JE_BillNo = txtBillNo.Text
    '        'txtBillNo.Text
    '        objPSJEDetails.dAcc_JE_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objPSJEDetails.dAcc_JE_BillAmount = txtBillAmount.Text
    '        objPSJEDetails.iAcc_JE_YearID = sSession.YearID
    '        objPSJEDetails.sAcc_JE_Status = "W"
    '        objPSJEDetails.iAcc_JE_CreatedBy = sSession.UserID
    '        objPSJEDetails.iAcc_JE_CreatedOn = DateTime.Today
    '        objPSJEDetails.sAcc_JE_Operation = "U"
    '        objPSJEDetails.sAcc_JE_IPAddress = sSession.IPAddress
    '        objPSJEDetails.dAcc_JE_BillCreatedDate = DateTime.Today

    '        If ddlPaymentType.SelectedIndex = 1 Then
    '            objPSJEDetails.dAcc_JE_AdvanceAmount = 0.00 : objPSJEDetails.dAcc_JE_BalanceAmount = 0.00
    '            objPSJEDetails.sAcc_JE_AdvanceNaration = ""

    '        ElseIf ddlPaymentType.SelectedIndex = 3 Then
    '            objPSJEDetails.dAcc_JE_NetAmount = 0.00
    '            objPSJEDetails.sAcc_JE_PaymentNarration = ""

    '        ElseIf ddlPaymentType.SelectedIndex = 4 Then
    '            objPSJEDetails.sAcc_JE_ChequeNo = ""
    '            objPSJEDetails.sAcc_JE_IFSCCode = "" : objPSJEDetails.sAcc_JE_BankName = "" : objPSJEDetails.sAcc_JE_BranchName = ""
    '        End If

    '        objPSJEDetails.sAcc_JE_AdvanceNaration = ""
    '        objPSJEDetails.sAcc_JE_PaymentNarration = ""
    '        objPSJEDetails.sAcc_JE_ChequeNo = ""
    '        objPSJEDetails.sAcc_JE_IFSCCode = ""
    '        objPSJEDetails.sAcc_JE_BankName = ""
    '        objPSJEDetails.sAcc_JE_BranchName = ""

    '        If ddlPaymentType.SelectedIndex = "1" Then   ' Advance 
    '            iPaymentType = 1
    '            If txtAdvancePayment.Text <> "" Then
    '                objPSJEDetails.dAcc_JE_AdvanceAmount = txtAdvancePayment.Text
    '            Else
    '                objPSJEDetails.dAcc_JE_AdvanceAmount = 0.00
    '            End If
    '            objPSJEDetails.dAcc_JE_BalanceAmount = txtBalanceAmount.Text
    '            objPSJEDetails.sAcc_JE_AdvanceNaration = txtNarration.Text

    '        ElseIf ddlPaymentType.SelectedIndex = "3" Then   'Payment
    '            iPaymentType = 3
    '            If txtNarration.Text <> "" Then
    '                objPSJEDetails.dAcc_JE_NetAmount = txtNarration.Text
    '            Else
    '                objPSJEDetails.dAcc_JE_NetAmount = 0.00
    '            End If
    '            objPSJEDetails.sAcc_JE_PaymentNarration = txtNarration.Text

    '        ElseIf ddlPaymentType.SelectedIndex = "4" Then   'Cheque Details
    '            iPaymentType = 4
    '            objPSJEDetails.sAcc_JE_ChequeNo = txtChequeNo.Text
    '            objPSJEDetails.dAcc_JE_ChequeDate = txtChequeDate.Text
    '            objPSJEDetails.sAcc_JE_IFSCCode = txtIFSC.Text
    '            objPSJEDetails.sAcc_JE_BankName = txtBankName.Text
    '            objPSJEDetails.sAcc_JE_BranchName = txtBranchName.Text
    '        End If

    '        objPSJEDetails.iAcc_JE_UpdatedBy = sSession.UserID
    '        objPSJEDetails.iAcc_JE_UpdatedOn = DateTime.Today
    '        objPSJEDetails.iAcc_JE_CompID = sSession.AccessCodeID


    '        If txtTransactionNo.Text <> "" Then
    '            If objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("P") Then
    '                Arr = objPSJEDetails.SavePurchaseJournalMaster(sSession.AccessCode, objPSJEDetails)
    '                iTransID = Arr(1)
    '            ElseIf objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("S") Then
    '                Arr = objPSJEDetails.SaveSalesJournalMaster(sSession.AccessCode, objPSJEDetails)
    '                iTransID = Arr(1)
    '            End If
    '        End If

    '        For i = 0 To dgJEDetails.Items.Count - 1

    '            If objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("P") Then
    '                objPSJEDetails.iATD_TrType = 5
    '            ElseIf objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("S") Then
    '                objPSJEDetails.iATD_TrType = 6
    '            End If

    '            objPSJEDetails.dATD_TransactionDate = DateTime.Today

    '            If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
    '                objPSJEDetails.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
    '            Else
    '                objPSJEDetails.iATD_ID = 0
    '            End If

    '            objPSJEDetails.iATD_BillId = iTransID
    '            objPSJEDetails.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
    '            'iPaymentType

    '            If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
    '                objPSJEDetails.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
    '            Else
    '                objPSJEDetails.iATD_Head = 0
    '            End If


    '            If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
    '                objPSJEDetails.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
    '            Else
    '                objPSJEDetails.iATD_GL = 0
    '            End If

    '            If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
    '                objPSJEDetails.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
    '            Else
    '                objPSJEDetails.iATD_SubGL = 0
    '            End If

    '            If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
    '                objPSJEDetails.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
    '            Else
    '                objPSJEDetails.dATD_Debit = 0
    '            End If

    '            If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
    '                objPSJEDetails.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
    '            Else
    '                objPSJEDetails.dATD_Credit = 0
    '            End If

    '            If objPSJEDetails.dATD_Debit > 0 And objPSJEDetails.dATD_Credit = 0 Then
    '                objPSJEDetails.iATD_DbOrCr = 1 'Debit
    '            ElseIf objPSJEDetails.dATD_Debit = 0 And objPSJEDetails.dATD_Credit > 0 Then
    '                objPSJEDetails.iATD_DbOrCr = 2 'Credit
    '            End If

    '            objPSJEDetails.iATD_CreatedBy = sSession.UserID
    '            objPSJEDetails.dATD_CreatedOn = DateTime.Today

    '            objPSJEDetails.sATD_Status = "W"
    '            objPSJEDetails.iATD_YearID = sSession.YearID
    '            objPSJEDetails.sATD_Operation = "U"
    '            objPSJEDetails.sATD_IPAddress = sSession.IPAddress

    '            objPSJEDetails.iATD_UpdatedBy = sSession.UserID
    '            objPSJEDetails.dATD_UpdatedOn = DateTime.Today

    '            objPSJEDetails.iATD_CompID = sSession.AccessCodeID
    '            objPSJEDetails.SaveUpdateTransactionDetails(sSession.AccessCode, objPSJEDetails)

    '        Next

    '        lblCustomerValidationMsg.Text = "Successfully Updated."
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '        dgJEDetails.DataSource = objPSJEDetails.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID, txtTransactionNo.Text)
    '        dgJEDetails.DataBind()

    '        LoadExistingJEs()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPaymentType_SelectedIndexChanged")
    '    End Try
    'End Sub
    Private Sub ddlBillNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBillNo.SelectedIndexChanged
        Dim i As Integer = 0
        Dim sBillNo As String = ""
        Dim sBillName As String = ""
        Dim dtSales As New DataTable
        Dim dtPurchase As New DataTable
        Try
            If (txtTransactionNo.Text).StartsWith("P") Then
                If ddlBillNo.SelectedIndex > 0 Then
                    'LoadParty()
                    ddlParty.SelectedValue = objPSJEDetails.GetPurchasePartyID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlBillNo.SelectedValue)
                    LoadLocation(ddlParty.SelectedValue)

                    sBillNo = ddlBillNo.SelectedValue
                    txtBillNo.Text = ddlBillNo.SelectedItem.Text
                    txtBillAmount.Text = ""
                    dtPurchase = loadDetailsB(sSession.AccessCode, sSession.AccessCodeID, ddlBillNo.SelectedValue, 0)
                    'loaddetails(sBillNo, 0)
                    If dtPurchase.Rows.Count > 0 Then
                        For j = 0 To dtPurchase.Rows.Count - 1
                            If j = dtPurchase.Rows.Count - 1 Then
                                txtBillAmount.Text = dtPurchase.Rows(j)("GrandTotal")
                                GetDefaultGridPurchase(dtPurchase.Rows(j)("GrandTotal"), dtPurchase.Rows(j)("TotalVat"), dtPurchase.Rows(j)("CSTAmtTotal"), dtPurchase.Rows(j)("TotalExiseAmt"), dtPurchase.Rows(j)("SubTotal"))
                            End If
                        Next
                    End If
                Else
                    txtBillAmount.Text = ""
                End If
            ElseIf (txtTransactionNo.Text).StartsWith("S") Then
                If ddlBillNo.SelectedIndex > 0 Then
                    ddlParty.SelectedValue = objPSJEDetails.GetSalesPartyID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlBillNo.SelectedValue)
                    LoadLocation(ddlParty.SelectedValue)

                    txtBillNo.Text = ddlBillNo.SelectedItem.Text
                    sBillNo = ddlBillNo.SelectedValue
                    dtSales = LoadSalesDetails(sBillNo)
                    If dtSales.Rows.Count > 0 Then
                        txtBillAmount.Text = dtSales.Rows(0)("Total")
                        GetDefaultGridSales(dtSales.Rows(0)("Total"), dtSales.Rows(0)("VATAmt"), dtSales.Rows(0)("CSTAmt"), dtSales.Rows(0)("ExciseAmt"), dtSales.Rows(0)("BasicPrice"))
                    End If
                Else
                    txtBillAmount.Text = ""
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBillNo_SelectedIndexChanged")
        End Try
    End Sub
    'Public Sub GetDefaultGridSales(ByVal dTotal As Double, ByVal dVATAmt As Double, ByVal dCSTAmt As Double, ByVal dExciseAmt As Double, ByVal dBasicPrice As Double)
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim sGL As String = "" : Dim sSubGL As String = ""
    '    Dim sArray As Array
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("HeadID")
    '        dt.Columns.Add("GLID")
    '        dt.Columns.Add("SubGLID")
    '        dt.Columns.Add("PaymentID")
    '        dt.Columns.Add("SrNo")
    '        dt.Columns.Add("Type")
    '        dt.Columns.Add("GLCode")
    '        dt.Columns.Add("GLDescription")
    '        dt.Columns.Add("SubGL")
    '        dt.Columns.Add("SubGLDescription")
    '        dt.Columns.Add("OpeningBalance")
    '        dt.Columns.Add("Debit")
    '        dt.Columns.Add("Credit")
    '        dt.Columns.Add("Balance")

    '        dRow = dt.NewRow

    '        dRow("Id") = 0
    '        dRow("HeadID") = 2
    '        dRow("GLID") = 78
    '        dRow("SubGLID") = 0
    '        dRow("PaymentID") = 5
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "Bill Amount"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = 0
    '        dRow("Credit") = dBasicPrice

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'VAT

    '        dRow("Id") = 0
    '        dRow("HeadID") = 4
    '        dRow("GLID") = 257
    '        dRow("SubGLID") = 258
    '        dRow("PaymentID") = 6
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "VAT"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = 0
    '        dRow("Credit") = dVATAmt

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'CST

    '        dRow("Id") = 0
    '        dRow("HeadID") = 4
    '        dRow("GLID") = 257
    '        dRow("SubGLID") = 259
    '        dRow("PaymentID") = 7
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "CST"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = 0
    '        dRow("Credit") = dCSTAmt

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'Excise

    '        dRow("Id") = 0
    '        dRow("HeadID") = 4
    '        dRow("GLID") = 257
    '        dRow("SubGLID") = 260
    '        dRow("PaymentID") = 8
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "Excise"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = 0
    '        dRow("Credit") = dExciseAmt

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'Party/Customer

    '        dRow("Id") = 0
    '        dRow("HeadID") = 1
    '        dRow("GLID") = 146
    '        dRow("SubGLID") = objPSJEDetails.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "C")
    '        dRow("PaymentID") = 9
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "Party/Customer"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "C")
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = dTotal
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dgJEDetails.DataSource = dt
    '        dgJEDetails.DataBind()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Sub GetDefaultGridPurchase(ByVal dTotal As Double, ByVal dVATAmt As Double, ByVal dCSTAmt As Double, ByVal dExciseAmt As Double, ByVal dBasicPrice As Double)
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim sGL As String = "" : Dim sSubGL As String = ""
    '    Dim sArray As Array
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("HeadID")
    '        dt.Columns.Add("GLID")
    '        dt.Columns.Add("SubGLID")
    '        dt.Columns.Add("PaymentID")
    '        dt.Columns.Add("SrNo")
    '        dt.Columns.Add("Type")
    '        dt.Columns.Add("GLCode")
    '        dt.Columns.Add("GLDescription")
    '        dt.Columns.Add("SubGL")
    '        dt.Columns.Add("SubGLDescription")
    '        dt.Columns.Add("OpeningBalance")
    '        dt.Columns.Add("Debit")
    '        dt.Columns.Add("Credit")
    '        dt.Columns.Add("Balance")

    '        dRow = dt.NewRow

    '        dRow("Id") = 0
    '        dRow("HeadID") = 3
    '        dRow("GLID") = 93
    '        dRow("SubGLID") = 251
    '        dRow("PaymentID") = 5
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "Bill Amount"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = dBasicPrice
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'VAT

    '        dRow("Id") = 0
    '        dRow("HeadID") = 4
    '        dRow("GLID") = 253
    '        dRow("SubGLID") = 254
    '        dRow("PaymentID") = 6
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "VAT"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = dVATAmt
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'CST

    '        dRow("Id") = 0
    '        dRow("HeadID") = 4
    '        dRow("GLID") = 253
    '        dRow("SubGLID") = 255
    '        dRow("PaymentID") = 7
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "CST"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = dCSTAmt
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'Excise

    '        dRow("Id") = 0
    '        dRow("HeadID") = 4
    '        dRow("GLID") = 253
    '        dRow("SubGLID") = 256
    '        dRow("PaymentID") = 8
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "Excise"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = dExciseAmt
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'Party/Customer

    '        dRow("Id") = 0
    '        dRow("HeadID") = 4
    '        dRow("GLID") = 67
    '        dRow("SubGLID") = objPSJEDetails.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "S")
    '        dRow("PaymentID") = 9
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "Party/Customer"

    '        sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objPSJEDetails.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "S")
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = 0
    '        dRow("Credit") = dTotal

    '        dt.Rows.Add(dRow)

    '        dgJEDetails.DataSource = dt
    '        dgJEDetails.DataBind()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub GetDefaultGridSales(ByVal dTotal As Double, ByVal dVATAmt As Double, ByVal dCSTAmt As Double, ByVal dExciseAmt As Double, ByVal dBasicPrice As Double)
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
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

            dRow = dt.NewRow

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_SubGL")
            dRow("PaymentID") = 5
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Bill Amount"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = 0
            dRow("Credit") = dBasicPrice

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'VAT

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "VAT", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "VAT", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "VAT", "Acc_SubGL")
            dRow("PaymentID") = 6
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "VAT"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = 0
            dRow("Credit") = dVATAmt

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'CST

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "CST", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "CST", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "CST", "Acc_SubGL")
            dRow("PaymentID") = 7
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "CST"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = 0
            dRow("Credit") = dCSTAmt

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'Excise

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Excise", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Excise", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Excise", "Acc_SubGL")
            dRow("PaymentID") = 8
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Excise"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = 0
            dRow("Credit") = dExciseAmt

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'Party/Customer

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "C")

            dRow("PaymentID") = 9
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Party/Customer"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "C")
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = dTotal
            dRow("Credit") = 0

            dt.Rows.Add(dRow)

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GetDefaultGridPurchase(ByVal dTotal As Double, ByVal dVATAmt As Double, ByVal dCSTAmt As Double, ByVal dExciseAmt As Double, ByVal dBasicPrice As Double)
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
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

            dRow = dt.NewRow

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_SubGL")
            dRow("PaymentID") = 5
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Bill Amount"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = dBasicPrice
            dRow("Credit") = 0

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'VAT

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "VAT", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "VAT", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "VAT", "Acc_SubGL")
            dRow("PaymentID") = 6
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "VAT"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = dVATAmt
            dRow("Credit") = 0

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'CST

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "CST", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "CST", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "CST", "Acc_SubGL")
            dRow("PaymentID") = 7
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "CST"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = dCSTAmt
            dRow("Credit") = 0

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'Excise

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Excise", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Excise", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Excise", "Acc_SubGL")
            dRow("PaymentID") = 8
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Excise"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = dExciseAmt
            dRow("Credit") = 0

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'Party/Customer

            dRow("Id") = 0
            dRow("HeadID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
            dRow("GLID") = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
            dRow("SubGLID") = objPSJEDetails.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "S")
            dRow("PaymentID") = 9
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Party/Customer"

            sGL = objPSJEDetails.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objPSJEDetails.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "S")
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = 0
            dRow("Credit") = dTotal

            dt.Rows.Add(dRow)

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function loadDetailsB(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dr As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim VAT As String = "", CST As String = "", Exise As String = ""
        Dim Cstval As String = ""
        Dim Total, TotalAmt, Totaltax, TotalVat As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtExAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("CSTAmtTotal")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("TotalExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("SubTotal")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalVat")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("AltUnit")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("INVH_Mdate")
            dt.Columns.Add("BatchNumber")
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("PGM_DocumentRefNo")
            dt.Columns.Add("PGM_InvoiceDate")
            dt.Columns.Add("gtdiscountAmt")
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_ESugamNo,e.PGM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,"
            sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
            sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PGD_BatchNumber,h.PGD_ManufactureDate,h.PGD_ExpireDate,"
            sSql = sSql & " d.Inv_Description Commodity,"
            sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
            sSql = sSql & "  from Purchase_verification"
            sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            sSql = sSql & " join Inventory_Master_history InvH On b.PIA_HistoryID=InvH.InvH_ID"
            sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID "
            sSql = sSql & "  join Purchase_GIN_Details h On b.PIA_GINID=h.PGD_MasterID And b.PIA_OrderID=h.PGD_OrderID and PIA_HistoryID=h.PGD_HistoryID where b.PIA_CompID=" & iCompID & ""
            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    gtExAmt = 0
                    If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
                        gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
                    Else
                        dRow("Rate") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) * dtDetails.Rows(i)("POD_Discount")) / 100))
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                    Else
                        dRow("VAT") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        gtExAmt = dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                        gtExAmt = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_VAT") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_CST") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        gtCSTAmt = gtCSTAmt + dRow("CSTAmt")
                        Totaltax = Totaltax + dRow("CSTAmt")
                    Else
                        dRow("CSTAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDBL.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDBL.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If

                    If (dtDetails.Rows(i)("PGM_InvoiceDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
                            dRow("PGM_InvoiceDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate").ToString(), "D")
                        Else
                            dRow("PGM_InvoiceDate") = ""
                        End If
                    Else
                        dRow("PGM_InvoiceDate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PGM_DocumentRefNo")) = False Then
                        dRow("PGM_DocumentRefNo") = dtDetails.Rows(i)("PGM_DocumentRefNo")
                    End If

                    If (dtDetails.Rows(i)("PGD_ManufactureDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGD_ManufactureDate")) = False Then
                            dRow("INVH_Mdate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ManufactureDate").ToString(), "D")
                        Else
                            dRow("INVH_Mdate") = ""
                        End If
                    Else
                        dRow("INVH_Mdate") = ""
                    End If

                    If (dtDetails.Rows(i)("POM_OrderDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                            dRow("POM_OrderDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate").ToString(), "D")
                        Else
                            dRow("POM_OrderDate") = ""
                        End If
                    Else
                        dRow("POM_OrderDate") = ""
                    End If
                    If (dtDetails.Rows(i)("PGD_ExpireDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGD_ExpireDate")) = False Then
                            dRow("INVH_Edate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ExpireDate").ToString(), "D")
                        Else
                            dRow("INVH_Edate") = ""
                        End If
                    Else
                        dRow("INVH_Edate") = ""
                    End If

                    If (dRow("INVH_Edate").ToString() = "30/12/1899") Then
                        dRow("INVH_Edate") = ""
                    End If
                    If (dRow("INVH_Mdate").ToString() = "30/12/1899") Then
                        dRow("INVH_Mdate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                        dRow("INVH_MRP") = dtDetails.Rows(i)("INVH_MRP")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PGD_BatchNumber")) = False Then
                        dRow("BatchNumber") = dtDetails.Rows(i)("PGD_BatchNumber")
                    End If
                    subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = String.Format("{0:0.00}", subTotal)
                    dRow("TotalVat") = String.Format("{0:0.00}", TotalVat)
                    dRow("CSTAmtTotal") = String.Format("{0:0.00}", gtCSTAmt)
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal + TotalVat + gtCSTAmt + gtExiseAmt))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dt.Rows.Add(dRow)
                End If
            Next
            dtDetails.Clear()
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,"
            sSql = sSql & "b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,"
            sSql = sSql & " b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,"
            sSql = sSql & "c.Inv_Description,c.Inv_Color,c.Inv_Size,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,"
            sSql = sSql & "d.Inv_Description Commodity	"
            sSql = sSql & " From Purchase_verification"
            sSql = sSql & " Join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef"
            sSql = sSql & " Join Inventory_Master_history InvH On b.PIE_HistoryID=InvH.InvH_ID"
            sSql = sSql & "   Join Inventory_Master c on b.PIE_Description=c.Inv_ID"
            sSql = sSql & "  Join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID"
            sSql = sSql & " Join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID "
            sSql = sSql & " Join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"

            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo = " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Description")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> "") Then
                            dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                            gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("TotalQty") = 0
                        End If
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("POD_Rate")
                        gtMRP = gtMRP + dtDetails.Rows(i)("POD_Rate")
                    Else
                        dRow("Rate") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VATAmount")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VATAmount")
                    Else
                        dRow("VAT") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Vat")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_Vat")))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                        gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                    Else
                        dRow("CSTAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDBL.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDBL.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False And dtDetails.Rows(i)("POD_ExciseAmount") <> "" Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False And dtDetails.Rows(i)("POD_Discount") <> "" Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If
                    subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = subTotal
                    dRow("TotalVat") = TotalVat
                    dRow("CSTAmtTotal") = gtCSTAmt
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal((subTotal + TotalVat + gtCSTAmt + gtExiseAmt) - gtdiscountAmt))
                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dt.Rows.Add(dRow)
                End If
            Next

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub imgbtnDBGLSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDBGLSearch.Click
        Try
            If ddldbHead.SelectedIndex = 0 Then
                lblError.Text = "Select Head Of Account."
                txtDBGLSearch.Text = ""
            Else
                ddldbGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbHead.SelectedValue, txtDBGLSearch.Text)
                ddldbGL.DataTextField = "GlDesc"
                ddldbGL.DataValueField = "gl_Id"
                ddldbGL.DataBind()
                ddldbGL.Items.Insert(0, "Select GL Code")
                txtDBGLSearch.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDBGLSearch_Click")
        End Try
    End Sub
    Private Sub imgbtnDBSubGLSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDBSubGLSearch.Click
        Dim iHead As Integer
        Try
            If ddldbGL.SelectedIndex > 0 Then
                ddldbsUbGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbGL.SelectedValue, txtDBSubGLSearch.Text)
                ddldbsUbGL.DataTextField = "GlDesc"
                ddldbsUbGL.DataValueField = "gl_Id"
                ddldbsUbGL.DataBind()
                ddldbsUbGL.Items.Insert(0, "Select SubGL Code")
                txtDBSubGLSearch.Text = ""
            Else
                lblError.Text = "Select Sub GL."
                txtDBSubGLSearch.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDBSubGLSearch_Click")
        End Try
    End Sub
    Private Sub imgbtnCrGLSeach_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCrGLSeach.Click
        Try
            If ddlCrHead.SelectedIndex > 0 Then
                ddlCrGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedValue, txtCrGLSearch.Text)
                ddlCrGL.DataTextField = "GlDesc"
                ddlCrGL.DataValueField = "gl_Id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL Code")
                txtCrGLSearch.Text = ""
            Else
                lblError.Text = "Select Head Of Account."
                txtCrGLSearch.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnCrGLSeach_Click")
        End Try
    End Sub
    Private Sub imgbtnCrSubGLSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCrSubGLSearch.Click
        Try
            If ddlCrGL.SelectedIndex > 0 Then
                ddlCrSubGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue, txtCrSubGLSearch.Text)
                ddlCrSubGL.DataTextField = "GlDesc"
                ddlCrSubGL.DataValueField = "gl_Id"
                ddlCrSubGL.DataBind()
                ddlCrSubGL.Items.Insert(0, "Select SubGL Code")
                txtCrSubGLSearch.Text = ""
            Else
                lblError.Text = "Select GL."
                txtCrSubGLSearch.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnCrSubGLSearch_Click")
        End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim iPSID As Integer
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlExistJE.SelectedIndex > 0 Then
                If txtTransactionNo.Text.StartsWith("P") Then
                    iPSID = 0
                Else
                    iPSID = 1
                End If

                sStatus = objPSJEDetails.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, "W", sSession.UserID, iPSID, sSession.IPAddress, sSession.YearID)
                If sStatus = "A" Then
                    lblError.Text = "This Transaction is already Approved."
                    Exit Sub
                End If

                objPSJEDetails.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, "W", sSession.UserID, iPSID, sSession.IPAddress, sSession.YearID)
                objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, sSession.YearID, sSession.UserID, sSession.IPAddress, iPSID)
                lblError.Text = "Successfully Approved."
                lblStatus.Text = "Approved"
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            Else
                lblError.Text = "Select Existing JE to Approve."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    'Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
    '    Try
    '        imgbtnSave_Click(sender, e)
    '        lblCustomerValidationMsg.Text = "Successfully Updated."
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
    '    End Try
    'End Sub
End Class
