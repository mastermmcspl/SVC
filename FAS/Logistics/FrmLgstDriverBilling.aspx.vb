Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Logistics_FrmLgstDriverBilling
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/DriverBilling"
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
    Dim objLDB As New clsDriverBilling
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

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LDB")
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
                BindDriver()
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
            txtBillNo.Text = objLDB.GenerateBillNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateBillNo")
        End Try
    End Sub
    Public Sub LoadExistingBillNo()
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlExstBillNo.DataSource = objLDB.LoadExistingBillNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

            ddlExstBillNo.DataTextField = "LDB_BillNo"
            ddlExstBillNo.DataValueField = "LDB_ID"
            ddlExstBillNo.DataBind()
            ddlExstBillNo.Items.Insert(0, "Existing Bill No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingBillNo")
        End Try
    End Sub
    Protected Sub BindDriver()
        Try
            ddlDriver.DataSource = objLDB.LoadDriver(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlDriver.DataTextField = "LDM_DriverName"
            ddlDriver.DataValueField = "LDM_ID"
            ddlDriver.DataBind()
            ddlDriver.Items.Insert(0, "Select Driver name.")
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
            dgDriverBillDetails.DataSource = Nothing
            dgDriverBillDetails.DataBind()
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
            Dim dt As New DataTable
            dt = objLDB.BindDriverDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDriver.SelectedValue, sFromDate, sToDate)
            If dt.Rows.Count > 0 Then
                dgDriverBillDetails.Visible = True
                dgDriverBillDetails.DataSource = dt
                dgDriverBillDetails.DataBind()
            Else
                dgDriverBillDetails.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Logistics/FrmLgstDrivBillingDashboard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Logistics/FrmLgstDriverBilling.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub ddlExstBillNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExstBillNo.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExstBillNo.SelectedIndex > 0 Then
                ' dgJEDetails.Visible = True
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
            dt = objLDB.LoadDriverBillingDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBillID)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LDB_Id")

                If IsDBNull(dt.Rows(0)("LDB_DriverID")) = False Then
                    If dt.Rows(0)("LDB_DriverID") > 0 Then
                        ddlDriver.SelectedValue = dt.Rows(0)("LDB_DriverID")
                    Else
                        ddlDriver.SelectedIndex = 0
                    End If
                Else
                    ddlDriver.SelectedIndex = 0
                End If


                If IsDBNull(dt.Rows(0)("LDB_FromDate")) = False Then
                    txtFromDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LDB_FromDate").ToString(), "D")
                Else
                    txtFromDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDB_ToDate")) = False Then
                    txtToDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LDB_ToDate").ToString(), "D")
                Else
                    txtToDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDB_BillDate")) = False Then
                    txtBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LDB_BillDate").ToString(), "D")
                Else
                    txtBillDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDB_BillNo")) = False Then
                    txtBillNo.Text = dt.Rows(0)("LDB_BillNo")
                Else
                    txtBillNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDB_TotalAmt")) = False Then
                    txtBillAmount.Text = dt.Rows(0)("LDB_TotalAmt")
                Else
                    txtBillAmount.Text = ""
                End If
                'If IsDBNull(dt.Rows(0)("LDB_TCS")) = False Then
                '    txtCompanyAddress.Text = dt.Rows(0)("LCB_CompanyAddress")
                'Else
                '    txtCompanyAddress.Text = ""
                'End If


                If (dt.Rows(0)("LDB_Delflag") = "W") Then
                    lblError.Text = "Waiting For Approval"
                    imgbtnUpdate.Visible = True
                    imgbtnWaiting.Visible = True
                    'btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LDB_Delflag") = "D") Then
                    ' btnDelete.Text = "ReCall"
                Else
                    'btnDelete.Text = "Delete"
                End If

                imgbtnSave.Visible = False
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                ' BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                If (dt.Rows(0)("LDB_Delflag") = "X") Then
                    lblStatus.Text = "Waiting For Approval(After De-Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LDB_Delflag") = "D") Then
                    lblStatus.Text = "De-Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LDB_Delflag") = "A") Then
                    lblStatus.Text = "Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "Delete"
                    imgbtnUpdate.Visible = False
                    imgbtnSave.Visible = False
                    imgbtnWaiting.Visible = False
                End If
                If (dt.Rows(0)("LDB_Delflag") = "Y") Then
                    lblStatus.Text = "Waiting For Approval(After Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LDB_Delflag") = "W") Then
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
                dgDriverBillDetails.Visible = True
                dgDriverBillDetails.DataSource = objLDB.BindDriverBillDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExstBillNo.SelectedValue, ddlDriver.SelectedValue)
                dgDriverBillDetails.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
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
            If dgDriverBillDetails.Items.Count > 0 Then
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
                    If ddlDriver.SelectedIndex > 0 Then
                    Else
                        lblError.Text = "Select driver"
                        Exit Sub
                    End If

                End If

                dPartyTotal = dgDriverBillDetails.Items(0).Cells(1).Text
                'If dPartyTotal = "" Then
                '    dPartyTotal = 0
                'End If
                txtBillAmount.Text = dPartyTotal
                'Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) 
                objLDB.LDB_ID = lblID.Text
                objLDB.LDB_DriverID = ddlDriver.SelectedValue
                objLDB.LDB_FromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLDB.LDB_ToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLDB.LDB_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLDB.LDB_BillNo = txtBillNo.Text
                objLDB.LDB_TotalAmt = txtBillAmount.Text
                'If (IsDBNull(dgDriverBillDetails.Items(0).Cells(1).Text) = False) And (dgDriverBillDetails.Items(0).Cells(1).Text <> "&nbsp;") Then
                '    objLDB.LDB_AdvanceGvnAmt = dgDriverBillDetails.Items(0).Cells(1).Text
                'Else
                '    objLDB.LDB_AdvanceGvnAmt = 0
                'End If


                If (IsDBNull(dgDriverBillDetails.Items(0).Cells(2).Text) = False) And (dgDriverBillDetails.Items(0).Cells(2).Text <> "&nbsp;") Then
                    objLDB.LDB_AdvanceGvnAmt = dgDriverBillDetails.Items(0).Cells(2).Text
                Else
                    objLDB.LDB_AdvanceGvnAmt = 0
                End If


                If (IsDBNull(dgDriverBillDetails.Items(0).Cells(3).Text) = False) And (dgDriverBillDetails.Items(0).Cells(3).Text <> "&nbsp;") Then
                    objLDB.LDB_PendingAmt = dgDriverBillDetails.Items(0).Cells(3).Text
                Else
                    objLDB.LDB_PendingAmt = 0
                End If

                objLDB.LDB_Delflag = "W"
                objLDB.LDB_CompID = sSession.AccessCodeID
                objLDB.LDB_Status = "C"
                objLDB.LDB_Operation = "C"
                objLDB.LDB_IPAddress = sSession.IPAddress
                objLDB.LDB_CreatedBy = sSession.UserID
                objLDB.LDB_CreatedOn = Date.Today
                objLDB.LDB_ApprovedBy = Nothing
                objLDB.LDB_ApprovedOn = Date.Today
                objLDB.LDB_DeletedBy = Nothing
                objLDB.LDB_DeletedOn = Date.Today
                objLDB.LDB_UpdatedBy = sSession.UserID
                objLDB.LDB_UpdatedOn = Date.Today
                objLDB.LDB_YearID = sSession.YearID



                Arr = objLDB.SaveDriverBilling(sSession.AccessCode, objLDB)
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
                End If
                LoadExistingBillNo()
                ddlExstBillNo.SelectedValue = Arr(1)
                ddlExstBillNo_SelectedIndexChanged(sender, e)

                'dgDriverBillDetails.Visible = True
                'dgDriverBillDetails.DataSource = objLDB.BindDriverBillDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExstBillNo.SelectedValue, ddlDriver.SelectedValue)
                'dgDriverBillDetails.DataBind()
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
                lblError.Text = "Select Existing BillNo to Approve."
                lblPumpBillValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            sStatus = objLDB.GetStatusOfBillNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExstBillNo.SelectedValue, ddlDriver.SelectedValue)
            If UCase(sStatus) = Trim(UCase("A")) Then
                lblError.Text = "This is Already Approved."
                lblPumpBillValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            objLDB.UpdateInvStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExstBillNo.SelectedValue, sSession.YearID)
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

    Private Sub ddlDriver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriver.SelectedIndexChanged
        Dim oStatusID As Object, oMasterName As String
        Dim icount As Integer
        Dim dToDate As String = ""
        Try
            lblError.Text = ""
            If ddlExstBillNo.SelectedIndex > 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                Response.Redirect(String.Format("~/Logistics/FrmLgstDriverBilling.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
            Else
                icount = objLDB.GetDateDriverCount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDriver.SelectedValue)
                If icount > 0 Then
                    dToDate = objLDB.GetRouteDriverdate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDriver.SelectedValue)
                    If dToDate <> "" Then
                        txtFromDate.Text = objGen.FormatDtForRDBMS(dToDate, "D")
                    Else
                        txtFromDate.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDriver_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            lblError.Text = ""
            If ddlExstBillNo.SelectedIndex > 0 Then
                imgbtnSave_Click(sender, e)
            Else
                lblError.Text = "Select Existing Bill NO."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
End Class
