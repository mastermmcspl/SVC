Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Logistics_FrmLgstPumpBilling
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/PumpBilling"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Dim objCOA As New clsChartOfAccounts
    Private Shared sCMDSave As String
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objLPB As New clsPumpBilling
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        'btnAddDebit.ImageUrl = "~/Images/Add24.png"
        'btnAddCredit.ImageUrl = "~/Images/Add24.png"
    End Sub
    Public Function GetLineNumber(ByVal ex As Exception)
        Dim lineNumber As Int32 = 0
        Const lineSearch As String = ":line "
        Dim index = ex.StackTrace.LastIndexOf(lineSearch)
        If index <> -1 Then
            Dim lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length)
            If Int32.TryParse(lineNumberText, lineNumber) Then
            End If
        End If
        Return lineNumber
    End Function
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sStr As String = ""
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LPB")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCMDSave = "NO" : imgbtnWaiting.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True

                        sCMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If
                lblID.Text = 0
                BindDieselPump()
                GenerateBillNo()
                LoadExistingBillNo()
                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iInvID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlExstBillNo.SelectedValue = iInvID
                    'BindDetails(iInvID)
                    ddlExstBillNo_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch
        End Try
    End Sub
    Public Sub GenerateBillNo()
        Try
            txtBillNo.Text = objLPB.GenerateBillNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateBillNo")
        End Try
    End Sub
    Public Sub LoadExistingBillNo()
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlExstBillNo.DataSource = objLPB.LoadExistingBillNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

            ddlExstBillNo.DataTextField = "LPB_BillNo"
            ddlExstBillNo.DataValueField = "LPB_ID"
            ddlExstBillNo.DataBind()
            ddlExstBillNo.Items.Insert(0, "Existing Bill No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingBillNo")
        End Try
    End Sub
    Protected Sub BindDieselPump()
        Try
            ddlPump.DataSource = objLPB.LoadDieselPump(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlPump.DataTextField = "LPM_PumpName"
            ddlPump.DataValueField = "LPM_ID"
            ddlPump.DataBind()
            ddlPump.Items.Insert(0, "Select Diesel/Petrol Pump name.")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim dDate, dSDate As Date

        Dim dToDate, dSToDate As Date
        Dim m As Integer
        Try
            lblError.Text = ""
            If (txtFromDate.Text <> "" And txtToDate.Text <> "") Then
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "From Date (" & txtFromDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    txtFromDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "From Date (" & txtFromDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    txtFromDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'

                'Cheque Date Comparision'
                dToDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dToDate, dSToDate)
                If m < 0 Then
                    lblError.Text = "To Date Date (" & txtToDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    txtToDate.Focus()
                    Exit Sub
                End If

                dToDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dToDate, dSToDate)
                If m > 0 Then
                    lblError.Text = "To Date Date (" & txtToDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    txtToDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'


                'dDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'dSDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'm = DateDiff(DateInterval.Day, dDate, dSDate)
                'If m < 0 Then
                '    lblError.Text = "To Date (" & txtToDate.Text & ") should be Greater than From Date(" & txtFromDate.Text & ")."
                '    txtToDate.Focus()
                '    Exit Sub
                'End If
            End If
            LoadPumpBillingDetails()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadPumpBillingDetails()
        Dim sFromDate As String = "", sToDate As String = ""
        Try
            If txtFromDate.Text <> "" Then
                sFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            Else
                sFromDate = "01/01/1900"
            End If

            If txtToDate.Text <> "" Then
                sToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            Else
                sToDate = "01/01/1900"
            End If

            dgPumpBillDetails.DataSource = objLPB.BindPumpDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPump.SelectedValue, sFromDate, sToDate)
            dgPumpBillDetails.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim iParty As Integer = 0
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
        Dim dPartyTotal As Double
        Dim sState As String = ""
        Dim Arr() As String
        Try
            lblError.Text = ""
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

            'iParty = ddlPump.SelectedValue
            ''  sState = objLPB.SQLGetDescription(sSession.AccessCode, "Select PIM_State From Purchase_Invoice_Master Where PIM_ID=" & iInvoiceID & " And PIM_CompID=" & sSession.AccessCodeID & " And PIM_YearID=" & sSession.YearID & " ")

            'If (IsDBNull(dgPumpBillDetails.Items(0).Cells(4).Text) = False) And (dgPumpBillDetails.Items(0).Cells(4).Text <> "&nbsp;") Then
            '    dPartyTotal = dgPumpBillDetails.Items(0).Cells(4).Text
            'Else
            '    dPartyTotal = 0
            'End If


            'dRow = dt.NewRow 'Item Name
            'dRow("Id") = 0
            'dRow("HeadID") = objLPB.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Purchase of Diesel")
            'dRow("GLID") = objLPB.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Purchase of Diesel")
            ''   If sTypeOfBill = "Local" Then
            'dRow("SubGLID") = objLPB.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Diesel Expense")
            ''  ElseIf sTypeOfBill = "Inter State" Then
            '' dRow("SubGLID") = objVerification.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Inter State GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
            '' End If

            'dRow("PaymentID") = 15
            'dRow("SrNo") = dt.Rows.Count + 1
            'dRow("Type") = "Purchase of Diesel"

            'sGL = objLPB.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            'If sGL <> "" Then
            '    sArray = sGL.Split("-")
            '    dRow("GLCode") = sArray(0)
            '    dRow("GLDescription") = sArray(1)
            'End If

            'sSubGL = objLPB.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            'If sSubGL <> "" Then
            '    sArray = sSubGL.Split("-")
            '    dRow("SubGL") = sArray(0)
            '    dRow("SubGLDescription") = sArray(1)
            'End If

            'dRow("Debit") = dPartyTotal
            'dRow("Credit") = 0
            'dt.Rows.Add(dRow)

            'dRow = dt.NewRow 'Party/Customer
            'dRow("Id") = 0
            'dRow("HeadID") = objLPB.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
            'dRow("GLID") = objLPB.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
            'dRow("SubGLID") = objLPB.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
            'dRow("PaymentID") = 15
            'dRow("SrNo") = dt.Rows.Count + 1
            'dRow("Type") = "Pump Name"

            'sGL = objLPB.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            'If sGL <> "" Then
            '    sArray = sGL.Split("-")
            '    dRow("GLCode") = sArray(0)
            '    dRow("GLDescription") = sArray(1)
            'End If

            'sSubGL = objLPB.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
            'If sSubGL <> "" Then
            '    sArray = sSubGL.Split("-")
            '    dRow("SubGL") = sArray(0)
            '    dRow("SubGLDescription") = sArray(1)
            'End If



            'dRow("Debit") = 0
            'dRow("Credit") = dPartyTotal

            'txtBillAmount.Text = dPartyTotal

            'dt.Rows.Add(dRow)
            'If dt.Rows.Count > 0 Then
            '    dgJEDetails.Visible = True
            '    dgJEDetails.DataSource = dt
            '    dgJEDetails.DataBind()
            'End If
            If dgPumpBillDetails.Items.Count > 0 Then
                If ddlExstBillNo.SelectedIndex > 0 Then
                Else
                    If txtBillNo.Text <> "" Then
                    Else
                        lblError.Text = "Enter Bill No."
                        Exit Sub
                    End If
                    If txtBillDate.Text <> "" Then
                    Else
                        lblError.Text = "Select Bill Date"
                        Exit Sub
                    End If

                    If ddlPump.SelectedIndex > 0 Then
                    Else
                        lblError.Text = "Select Pump"
                        Exit Sub
                    End If

                End If
                dPartyTotal = dgPumpBillDetails.Items(0).Cells(5).Text
                txtBillAmount.Text = dPartyTotal

                objLPB.LPB_ID = lblID.Text
                objLPB.LPB_PumpID = ddlPump.SelectedValue
                '   Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLPB.LPB_FromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLPB.LPB_ToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLPB.LPB_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLPB.LPB_BillNo = txtBillNo.Text
                objLPB.LPB_TotalAmt = txtBillAmount.Text
                objLPB.LPB_TCS = ""

                If (IsDBNull(dgPumpBillDetails.Items(0).Cells(1).Text) = False) And (dgPumpBillDetails.Items(0).Cells(1).Text <> "&nbsp;") Then
                    objLPB.LPB_TotalLtr = dgPumpBillDetails.Items(0).Cells(1).Text
                Else
                    objLPB.LPB_TotalLtr = 0
                End If


                If (IsDBNull(dgPumpBillDetails.Items(0).Cells(2).Text) = False) And (dgPumpBillDetails.Items(0).Cells(2).Text <> "&nbsp;") Then
                    objLPB.LPB_TotalDieselAmt = dgPumpBillDetails.Items(0).Cells(2).Text
                Else
                    objLPB.LPB_TotalDieselAmt = 0
                End If


                If (IsDBNull(dgPumpBillDetails.Items(0).Cells(3).Text) = False) And (dgPumpBillDetails.Items(0).Cells(3).Text <> "&nbsp;") Then
                    objLPB.LPB_AdvanceAmt = dgPumpBillDetails.Items(0).Cells(3).Text
                Else
                    objLPB.LPB_AdvanceAmt = 0
                End If

                If (IsDBNull(dgPumpBillDetails.Items(0).Cells(4).Text) = False) And (dgPumpBillDetails.Items(0).Cells(4).Text <> "&nbsp;") Then
                    objLPB.LPB_OtherExpense = dgPumpBillDetails.Items(0).Cells(4).Text
                Else
                    objLPB.LPB_OtherExpense = 0
                End If

                objLPB.LPB_Delflag = "W"
                objLPB.LPB_CompID = sSession.AccessCodeID
                objLPB.LPB_Status = "C"
                objLPB.LPB_Operation = "C"
                objLPB.LPB_IPAddress = sSession.IPAddress
                objLPB.LPB_CreatedBy = sSession.UserID
                objLPB.LPB_CreatedOn = Date.Today
                objLPB.LPB_ApprovedBy = Nothing
                objLPB.LPB_ApprovedOn = Date.Today
                objLPB.LPB_DeletedBy = Nothing
                objLPB.LPB_DeletedOn = Date.Today
                objLPB.LPB_UpdatedBy = sSession.UserID
                objLPB.LPB_UpdatedOn = Date.Today
                objLPB.LPB_YearID = sSession.YearID



                Arr = objLPB.SavePumpBilling(sSession.AccessCode, objLPB)
                'txtGLID.Text = 0

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    lblPumpBillValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPumpBillValidation').modal('show');", True)
                    If sCMDSave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If
                    imgbtnSave.Visible = False 'btnDelete.Visible = True
                    'btnSave.Text = "Save"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblPumpBillValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPumpBillValidation').modal('show');", True)
                    If sCMDSave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If  'btnDelete.Visible = True
                    imgbtnSave.Visible = False
                    ' btnSave.Text = "Update"
                    imgbtnWaiting.Visible = True
                    lblStatus.Text = "Waiting For Approval"
                    imgbtnUpdate.Visible = True
                End If
                LoadExistingBillNo()
                ddlExstBillNo.SelectedValue = Arr(1)
                ddlExstBillNo_SelectedIndexChanged(sender, e)

                'dgPumpBillDetails.Visible = True
                'dgPumpBillDetails.DataSource = objLPB.BindPumpBillDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExstBillNo.SelectedValue, ddlPump.SelectedValue)
                'dgPumpBillDetails.DataBind()
            Else
                lblError.Text = "No data to save"
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub

    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlExstBillNo.SelectedIndex = 0 Then
                lblError.Text = "Select Existing Bill No to Approve."
                lblPumpBillValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            sStatus = objLPB.GetStatusOfBillNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExstBillNo.SelectedValue, ddlPump.SelectedValue)
            If UCase(sStatus) = Trim(UCase("A")) Then
                lblError.Text = "This is Already Approved."
                lblPumpBillValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            objLPB.UpdateInvStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExstBillNo.SelectedValue, sSession.YearID)
            lblError.Text = "Successfully Approved."
            lblPumpBillValidationMsg.Text = "Successfully Approved."
            lblStatus.Text = "Approved."
            imgbtnUpdate.Visible = False
            imgbtnWaiting.Visible = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub

    Private Sub ddlExstBillNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExstBillNo.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExstBillNo.SelectedIndex > 0 Then
                '  dgJEDetails.Visible = True
                BindDetails(ddlExstBillNo.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExstBillNo_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindDetails(ByVal iBillID As Integer)
        Dim dt As New DataTable
        Dim iCustCompanyType As Integer = 0
        Try
            dt = objLPB.LoadPumpBillingDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBillID)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LPB_Id")

                If IsDBNull(dt.Rows(0)("LPB_PumpID")) = False Then
                    If dt.Rows(0)("LPB_PumpID") > 0 Then
                        ddlPump.SelectedValue = dt.Rows(0)("LPB_PumpID")
                    Else
                        ddlPump.SelectedIndex = 0
                    End If
                Else
                    ddlPump.SelectedIndex = 0
                End If



                If IsDBNull(dt.Rows(0)("LPB_FromDate")) = False Then
                    txtFromDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LPB_FromDate").ToString(), "D")
                Else
                    txtFromDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPB_ToDate")) = False Then
                    txtToDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LPB_ToDate").ToString(), "D")
                Else
                    txtToDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPB_BillDate")) = False Then
                    txtBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LPB_BillDate").ToString(), "D")
                Else
                    txtBillDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPB_BillNo")) = False Then
                    txtBillNo.Text = dt.Rows(0)("LPB_BillNo")
                Else
                    txtBillNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPB_TotalAmt")) = False Then
                    txtBillAmount.Text = dt.Rows(0)("LPB_TotalAmt")
                Else
                    txtBillAmount.Text = ""
                End If
                'If IsDBNull(dt.Rows(0)("LPB_TCS")) = False Then
                '    txtCompanyAddress.Text = dt.Rows(0)("LCB_CompanyAddress")
                'Else
                '    txtCompanyAddress.Text = ""
                'End If


                If (dt.Rows(0)("LPB_Delflag") = "W") Then
                    lblError.Text = "Waiting For Approval"
                    imgbtnUpdate.Visible = True
                    imgbtnWaiting.Visible = True
                    'btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LPB_Delflag") = "D") Then
                    ' btnDelete.Text = "ReCall"
                Else
                    'btnDelete.Text = "Delete"
                End If

                imgbtnSave.Visible = False
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                ' BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                If (dt.Rows(0)("LPB_Delflag") = "X") Then
                    lblStatus.Text = "Waiting For Approval(After De-Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LPB_Delflag") = "D") Then
                    lblStatus.Text = "De-Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LPB_Delflag") = "A") Then
                    lblStatus.Text = "Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "Delete"
                    imgbtnUpdate.Visible = False
                    imgbtnSave.Visible = False
                    imgbtnWaiting.Visible = False
                End If
                If (dt.Rows(0)("LPB_Delflag") = "Y") Then
                    lblStatus.Text = "Waiting For Approval(After Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LPB_Delflag") = "W") Then
                    lblStatus.Text = "Waiting For Approval"
                    imgbtnUpdate.Visible = True
                    imgbtnSave.Visible = False
                    imgbtnWaiting.Visible = True
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If

                'If IsDBNull(dt.Rows(0)("LPM_SubGL")) = False Then
                '    txtGLID.Text = dt.Rows(0)("LPM_SubGL")
                'Else
                '    txtGLID.Text = 0
                'End If
                Dim dtPumpBil As New DataTable
                dtPumpBil = objLPB.BindPumpBillDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExstBillNo.SelectedValue, ddlPump.SelectedValue)
                If dtPumpBil.Rows.Count > 0 Then
                    dgPumpBillDetails.Visible = True
                    dgPumpBillDetails.DataSource = dtPumpBil
                    dgPumpBillDetails.DataBind()
                Else
                    dgPumpBillDetails.Visible = False
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Logistics/FrmLgstPumpBillDashboard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            '    lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Logistics/FrmLgstPumpBilling.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub ddlPump_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPump.SelectedIndexChanged
        Dim oStatusID As Object, oMasterName As String
        Dim icount As Integer
        Dim dToDate As String = ""
        Try
            lblError.Text = ""
            If ddlExstBillNo.SelectedIndex > 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                Response.Redirect(String.Format("~/Logistics/FrmLgstPumpBilling.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
            Else
                icount = objLPB.GetDatePumpCount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPump.SelectedValue)
                If icount > 0 Then
                    dToDate = objLPB.GetRoutePumpdate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPump.SelectedValue)
                    If dToDate <> "" Then
                        txtFromDate.Text = objGen.FormatDtForRDBMS(dToDate, "D")
                        '  txtFromDate.Enabled = False
                    Else
                        txtFromDate.Text = "" ' : txtFromDate.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPump_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            lblError.Text = ""
            If ddlExstBillNo.SelectedIndex > 0 Then
                imgbtnSave_Click(sender, e)
            Else
                lblError.Text = "Select Existing Bill NO."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub

End Class
