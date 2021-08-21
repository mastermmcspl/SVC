Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Logistics_FrmLgstCustomerBilling
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/CustomerBilling"
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
    Dim objLCB As New clsCustBilling
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

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LCB")
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

                txtGLID.Text = 0

                LoadParty()
                LoadRoute()
                lblID.Text = 0
                imgbtnUpdate.Visible = False
                GenerateInvoiceNo()
                LoadExistingInvoiceNo()
                'BindExistingTripGenerationNo()

                imgbtnUpdate.Visible = False

                lblID.Text = "0"


                Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")

                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iInvID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlExistingInvoiceno.SelectedValue = iInvID
                    'BindDetails(iInvID)
                    ddlExistingInvoiceno_SelectedIndexChanged(sender, e)
                End If


                ' BindHeadofAccounts()
            End If
        Catch
        End Try
    End Sub
    Public Sub LoadParty()
        Try
            ddlCustomers.DataSource = objLCB.LoadCustomer(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomers.DataTextField = "BM_Name"
            ddlCustomers.DataValueField = "BM_ID"
            ddlCustomers.DataBind()
            ddlCustomers.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub LoadPartyAll()
    '    Try
    '        ddlCustomers.DataSource = objLCB.LoadCustomerAll(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlCustomers.DataTextField = "BM_Name"
    '        ddlCustomers.DataValueField = "BM_ID"
    '        ddlCustomers.DataBind()
    '        ddlCustomers.Items.Insert(0, "Select Customer")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub LoadRoute()
        Try
            ddlRoute.DataSource = objLCB.LoadRoute(sSession.AccessCode, sSession.AccessCodeID)
            ddlRoute.DataTextField = "LRM_StartDestPlace"
            ddlRoute.DataValueField = "LRM_ID"
            ddlRoute.DataBind()
            ddlRoute.Items.Insert(0, "Select Route")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub LoadRouteAll()
    '    Try
    '        ddlRoute.DataSource = objLCB.LoadRouteAll(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlRoute.DataTextField = "LRM_StartDestPlace"
    '        ddlRoute.DataValueField = "LRM_ID"
    '        ddlRoute.DataBind()
    '        ddlRoute.Items.Insert(0, "Select Route")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub GenerateInvoiceNo()
        Try
            lblError.Text = ""
            txtInvNo.Text = objLCB.GenerateInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCode")
        End Try
    End Sub
    Public Sub LoadExistingInvoiceNo()
        Dim dt As New DataTable
        Try
            ' lblError.Text = ""
            ddlExistingInvoiceno.DataSource = objLCB.LoadExistingInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingInvoiceno.DataTextField = "LCB_InvNo"
            ddlExistingInvoiceno.DataValueField = "LCB_ID"
            ddlExistingInvoiceno.DataBind()
            ddlExistingInvoiceno.Items.Insert(0, "Existing Invoice No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingDispatchNo")
        End Try
    End Sub
    Public Sub BindDetails(ByVal iInvID As Integer)
        Dim dt As New DataTable
        Dim iCustCompanyType As Integer = 0
        Try
            lblError.Text = ""
            dt = objLCB.LoadCustBillingDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvID)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LCB_Id")

                If IsDBNull(dt.Rows(0)("LCB_CustomerID")) = False Then
                    If dt.Rows(0)("LCB_CustomerID") > 0 Then
                        ddlCustomers.SelectedValue = dt.Rows(0)("LCB_CustomerID")
                    Else
                        ddlCustomers.SelectedIndex = 0
                    End If
                Else
                    ddlCustomers.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("LCB_RouteID")) = False Then
                    '    LoadRouteAll()
                    If dt.Rows(0)("LCB_RouteID") > 0 Then
                        ddlRoute.SelectedValue = dt.Rows(0)("LCB_RouteID")
                    Else
                        ddlRoute.SelectedValue = ""
                    End If
                Else
                    ddlRoute.SelectedValue = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_FromDate")) = False Then
                    txtFromDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LCB_FromDate").ToString(), "D")
                Else
                    txtFromDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_ToDate")) = False Then
                    txtToDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LCB_ToDate").ToString(), "D")
                Else
                    txtToDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_InvDate")) = False Then
                    txtInvDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LCB_InvDate").ToString(), "D")
                Else
                    txtInvDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_InvNo")) = False Then
                    txtInvNo.Text = dt.Rows(0)("LCB_InvNo")
                Else
                    txtInvNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_CustOrderRef")) = False Then
                    txtCustOrderRef.Text = dt.Rows(0)("LCB_CustOrderRef")
                Else
                    txtCustOrderRef.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_Agreement")) = False Then
                    txtAggreement.Text = dt.Rows(0)("LCB_Agreement")
                Else
                    txtAggreement.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_TotalAmt")) = False Then
                    txtTotalAmount.Text = dt.Rows(0)("LCB_TotalAmt")
                Else
                    txtTotalAmount.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_CompanyAddress")) = False Then
                    txtCompanyAddress.Text = dt.Rows(0)("LCB_CompanyAddress")
                Else
                    txtCompanyAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_CompanyGSTNRegNo")) = False Then
                    txtCompanyGSTN.Text = dt.Rows(0)("LCB_CompanyGSTNRegNo")
                Else
                    txtCompanyGSTN.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_CustomerAddress")) = False Then
                    txtCustomerAddress.Text = dt.Rows(0)("LCB_CustomerAddress")
                Else
                    txtCustomerAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_CustomerGSTNRegNo")) = False Then
                    txtCustGSTN.Text = dt.Rows(0)("LCB_CustomerGSTNRegNo")
                Else
                    txtCustGSTN.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_GSTRate")) = False Then
                    txtCustGSTRate.Text = dt.Rows(0)("LCB_GSTRate")
                Else
                    txtCustGSTRate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LCB_EscAmt")) = False Then
                    txtEscAmt.Text = dt.Rows(0)("LCB_EscAmt")
                Else
                    txtEscAmt.Text = ""
                End If
                iCustCompanyType = dt.Rows(0)("LCB_GSTNCategory")
                txtCustGstnCategoryId.Text = iCustCompanyType
                Dim description As String
                description = objLCB.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, iCustCompanyType)
                txtCustGstnCategory.Text = description
                If (dt.Rows(0)("LCB_Delflag") = "W") Then
                    lblError.Text = "Waiting For Approval"
                    imgbtnUpdate.Visible = True
                    imgbtnWaiting.Visible = True
                    'btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LCB_Delflag") = "D") Then
                    ' btnDelete.Text = "ReCall"
                Else
                    'btnDelete.Text = "Delete"
                End If
                imgbtnSave.Visible = False
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                ' BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                If (dt.Rows(0)("LCB_Delflag") = "X") Then
                    lblStatus.Text = "Waiting For Approval(After De-Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LCB_Delflag") = "D") Then
                    lblStatus.Text = "De-Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LCB_Delflag") = "A") Then
                    lblStatus.Text = "Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "Delete"
                    imgbtnUpdate.Visible = False
                    imgbtnSave.Visible = False
                    imgbtnWaiting.Visible = False
                End If
                If (dt.Rows(0)("LCB_Delflag") = "Y") Then
                    lblStatus.Text = "Waiting For Approval(After Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LCB_Delflag") = "W") Then
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
                grdCustAmountDetails.Visible = True
                grdCustAmountDetails.DataSource = objLCB.BindAmountDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInvoiceno.SelectedValue)
                grdCustAmountDetails.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
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
            LoadCustomerBillingDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btGo_Click")
        End Try
    End Sub
    Private Sub binddgCustAmountDetails()
        Dim dt As New DataTable
        Dim drow As DataRow
        Dim dGstAmount As Double
        Dim dTotalAmount As Double
        Dim sBillStatus As String
        Dim dGrandTotal As Double = 0.0
        Try
            dt.Columns.Add("TripAmount")
            dt.Columns.Add("TotalEscAmount")
            dt.Columns.Add("TotalTripAmount")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("SGST")
            dt.Columns.Add("SGSTAmount")
            dt.Columns.Add("CGST")
            dt.Columns.Add("CGSTAmount")
            dt.Columns.Add("IGST")
            dt.Columns.Add("IGSTAmount")
            dt.Columns.Add("GrandTotal")
            If ddlCustomers.SelectedIndex > 0 Then
                drow = dt.NewRow
                If txtCompanyGSTN.Text <> "" And txtCustGSTN.Text <> "" Then
                    sBillStatus = CheckSourceDestinationOfCust(Trim(txtCompanyGSTN.Text), Trim(txtCustGSTN.Text))
                ElseIf txtCompanyGSTN.Text <> "" And txtCustGSTN.Text = "" Then
                    sBillStatus = "Local"
                ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text <> "" Then
                    sBillStatus = "Local"
                ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text = "" Then
                    sBillStatus = "Local"
                End If

                drow("TripAmount") = txtTotalAmount.Text
                drow("TotalEscAmount") = txtTotEscAmt.Text
                dGrandTotal = Val(txtTotalAmount.Text) + Val(txtTotEscAmt.Text)
                drow("TotalTripAmount") = dGrandTotal
                drow("GSTRate") = txtCustGSTRate.Text
                dTotalAmount = (drow("TotalTripAmount") * drow("GSTRate"))
                dGstAmount = (dTotalAmount / 100)
                drow("GSTAmount") = dGstAmount

                If sBillStatus = "Local" Then
                    drow("SGST") = (drow("GSTRate") / 2)
                    drow("SGSTAmount") = (dGstAmount / 2)
                    drow("CGST") = (drow("GSTRate") / 2)
                    drow("CGSTAmount") = (dGstAmount / 2)
                    drow("IGST") = 0
                    drow("IGSTAmount") = 0
                ElseIf sBillStatus = "Inter State" Then
                    drow("SGST") = 0
                    drow("SGSTAmount") = 0
                    drow("CGST") = 0
                    drow("CGSTAmount") = 0
                    drow("IGST") = drow("GSTRate")
                    drow("IGSTAmount") = dGstAmount
                End If
                drow("GrandTotal") = (drow("TotalTripAmount") + dGstAmount)
                dt.Rows.Add(drow)
            End If
            grdCustAmountDetails.DataSource = dt
            grdCustAmountDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "binddgCustAmountDetails")
        End Try
    End Sub
    Private Sub LoadCustomerBillingDetails()
        Dim dt As New DataTable, dttotal As New DataTable
        Dim i As Integer = 0
        Dim sFromDate As String = "", sToDate As String = ""

        Dim iCustomerId As Integer

        Dim dTotlaAmount As Double = 0.00
        Dim sRouteId As String = ""
        Dim stxtFromdate As String = ""
        Dim dtTripDetails As New DataTable
        Dim dEscAmt As Double = 0.0
        Try
            If txtFromDate.Text <> "" Then
                sFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                'If sFromDate = "" Then
                '    sFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                '    sFromDate = objGen.FormatDtForRDBMS(sFromDate, "T")
                'End If
            Else
                sFromDate = "01/01/1900"
            End If

            If txtToDate.Text <> "" Then
                sToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            Else
                sToDate = "01/01/1900"
            End If

            If ddlCustomers.SelectedIndex > 0 Then
                iCustomerId = ddlCustomers.SelectedValue
            End If


            'If ddlAccZone.SelectedIndex > 0 Then
            '    iZoneID = ddlAccZone.SelectedValue
            'End If
            'If ddlAccRgn.SelectedIndex > 0 Then
            '    iRegionID = ddlAccRgn.SelectedValue
            'End If
            'If ddlAccArea.SelectedIndex > 0 Then
            '    iAreaID = ddlAccArea.SelectedValue
            'End If
            'If ddlAccBrnch.SelectedIndex > 0 Then
            '    iBranchID = ddlAccBrnch.SelectedValue
            'End If
            ' sRouteId = objLCB.GetRouteIDs(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlRoute.SelectedItem.ToString())


            'sRouteId = objLCB.GetRouteIDs(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlRoute.SelectedValue)
            'If sRouteId = "" Then
            '    lblError.Text = "No Trip Found"
            '    Exit Sub
            'Else
            If Val(txtEscAmt.Text) <> 0 Then
                dEscAmt = txtEscAmt.Text
            Else
                dEscAmt = 0
            End If
            dTotlaAmount = objLCB.GetTotalAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sFromDate, sToDate, iCustomerId, ddlRoute.SelectedValue)
            If Val(txtEscAmt.Text) <> 0 Then
                txtTotEscAmt.Text = objLCB.GetEscAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sFromDate, sToDate, iCustomerId, ddlRoute.SelectedValue, dEscAmt)
            End If
            If dTotlaAmount = 0 Then
                lblError.Text = "No Data Found"
                Exit Sub
            Else
                txtTotalAmount.Text = dTotlaAmount
                binddgCustAmountDetails()
                dtTripDetails = objLCB.GetTotalTripDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sFromDate, sToDate, iCustomerId, ddlRoute.SelectedValue, dEscAmt)
                If dtTripDetails.Rows.Count > 0 Then
                    dgTipDetails.Visible = True
                    dgTipDetails.DataSource = dtTripDetails
                    dgTipDetails.DataBind()
                Else
                    dgTipDetails.Visible = False
                End If
            End If
            'End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomerBillingDetails")
        End Try
    End Sub
    Private Sub ddlCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomers.SelectedIndexChanged
        Dim dtCustomer As New DataTable
        Dim dtCompany As New DataTable
        Dim iCustCompanyType As Integer
        Dim sCustCompanyType As String
        Try
            lblError.Text = ""
            If ddlCustomers.SelectedIndex > 0 Then
                txtCustGstnCategoryId.Text = 0
                dtCustomer = objLCB.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue)
                If dtCustomer.Rows.Count > 0 Then
                    If IsDBNull(dtCustomer.Rows(0)("BM_GSTNRegNo")) = False Then
                        txtCustGSTN.Text = dtCustomer.Rows(0)("BM_GSTNRegNo")
                    Else
                        txtCustGSTN.Text = ""
                    End If

                    If IsDBNull(dtCustomer.Rows(0)("BM_Address1")) = False Then
                        txtCustomerAddress.Text = dtCustomer.Rows(0)("BM_Address1")
                    Else
                        txtCustomerAddress.Text = ""
                    End If

                    iCustCompanyType = dtCustomer.Rows(0)("BM_GSTNCategory")
                    txtCustGstnCategoryId.Text = iCustCompanyType
                    Dim description As String
                    description = objLCB.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, iCustCompanyType)
                    If UCase(description) = UCase("RCM DEALER") Then
                        txtCustGSTRate.Text = 0
                        txtCustGstnCategory.Text = "RCM DEALER"
                    End If
                    If UCase(description) = UCase("NORMAL GST DEALER") Then
                        txtCustGSTRate.Text = 12
                        txtCustGstnCategory.Text = "NORMAL GST DEALER"
                    End If
                    If UCase(description) = UCase("REDUCED GST DEALER") Then
                        txtCustGSTRate.Text = 5
                        txtCustGstnCategory.Text = "REDUCED GST DEALER"
                    End If

                    'ddlDBOtherHead.SelectedIndex = objLCB.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dtCustomer.Rows(0)("BM_GL").ToString())
                    'ddldbOtherHead_SelectedIndexChanged(sender, e)

                    'ddlDbOtherGL.SelectedValue = dtCustomer.Rows(0)("BM_GL").ToString()
                    'ddlDBOtherGL_SelectedIndexChanged(sender, e)

                    'If dtCustomer.Rows(0)("BM_SubGL").ToString() = "0" Then
                    '    ddlDbOtherSubGL.SelectedIndex = -1
                    'Else
                    '    ddlDbOtherSubGL.SelectedValue = dtCustomer.Rows(0)("BM_SubGL").ToString()
                    '    'ddlDBOtherSubGL_SelectedIndexChanged(sender, e)
                    'End If

                End If

                dtCompany = objLCB.GetCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
                If dtCompany.Rows.Count > 0 Then
                    If IsDBNull(dtCompany.Rows(0)("Cust_FinalNo")) = False Then
                        txtCompanyGSTN.Text = dtCompany.Rows(0)("Cust_FinalNo")
                    Else
                        txtCompanyGSTN.Text = ""
                    End If

                    If IsDBNull(dtCompany.Rows(0)("Cust_comm_address")) = False Then
                        txtCompanyAddress.Text = dtCompany.Rows(0)("Cust_comm_address")
                    Else
                        txtCompanyAddress.Text = ""
                    End If
                End If
            Else
                'lblScode.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomers_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sSubGrpcode As String = "", sGLCode As String = ""
        Dim lParentId As Integer = 0, iHead As Integer = 2
        Dim bCheck As Boolean
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim lblTotalAmount, lblGSTRate, lblGSTAmount, lblSGST, lblSGSTAmount, lblCGST, lblCGSTAmount, lblIGST, lblIGSTAmount, lblGrandTotal As New Label
        Try
            lblError.Text = ""
            If txtInvDate.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtInvDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Invoice Date (" & txtInvDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustBillingValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                    txtInvDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtInvDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Invoice Date (" & txtInvDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustBillingValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                    txtInvDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If ddlExistingInvoiceno.SelectedIndex > 0 Then
            Else
                If txtInvNo.Text <> "" Then
                Else
                    lblError.Text = "Enter invoice No."
                    Exit Sub
                End If
                If txtInvDate.Text <> "" Then
                Else
                    lblError.Text = "Select Invoice Date"
                    Exit Sub
                End If

                If txtCompanyGSTN.Text <> "" Then
                Else
                    lblError.Text = "Enter Company GSTNNo in Compamy Master"
                    Exit Sub
                End If
                If txtCustGstnCategory.Text <> "" Then
                Else
                    lblError.Text = "Enter Customer GSTNNo in Compamy Master"
                    Exit Sub
                End If
                If ddlCustomers.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Customer"
                    Exit Sub
                End If
                If ddlRoute.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Route"
                    Exit Sub
                End If

            End If


            objLCB.LCB_ID = lblID.Text
            If ddlCustomers.SelectedIndex > 0 Then
                objLCB.LCB_CustomerID = ddlCustomers.SelectedValue
            Else
                objLCB.LCB_CustomerID = 0
            End If

            ' Dim sRouteName As String = ""

            ' sRouteName = ddlRoute.SelectedItem.ToString()

            If ddlRoute.SelectedIndex > 0 Then
                objLCB.LCB_RouteID = ddlRoute.SelectedValue
            Else
                objLCB.LCB_RouteID = 0
            End If

            'objLCB.LCB_RouteName = sRouteName
            'Date.ParseExact(txtInvDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) 
            objLCB.LCB_FromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objLCB.LCB_ToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objLCB.LCB_InvDate = Date.ParseExact(txtInvDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objLCB.LCB_InvNo = txtInvNo.Text
            objLCB.LCB_CustOrderRef = txtCustOrderRef.Text

            objLCB.LCB_Agreement = txtAggreement.Text

            '    objLCB.LCB_TotalAmt = Val(txtTotalAmount.Text) + Val(txtTotEscAmt.Text)
            objLCB.LCB_CompanyAddress = txtCompanyAddress.Text
            objLCB.LCB_CompanyGSTNRegNo = txtCompanyGSTN.Text

            objLCB.LCB_CustomerAddress = txtCustomerAddress.Text
            objLCB.LCB_CustomerGSTNRegNo = txtCustGSTN.Text

            objLCB.LCB_GSTNCategory = txtCustGstnCategoryId.Text
            objLCB.LCB_GSTRate = txtCustGSTRate.Text


            If txtCompanyGSTN.Text <> "" And txtCustGSTN.Text <> "" Then
                objLCB.LCB_GSTCustBillStatus = CheckSourceDestinationOfCust(Trim(txtCompanyGSTN.Text), Trim(txtCustGSTN.Text))
            ElseIf txtCompanyGSTN.Text <> "" And txtCustGSTN.Text = "" Then
                objLCB.LCB_GSTCustBillStatus = "Local"
            ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text <> "" Then
                objLCB.LCB_GSTCustBillStatus = "Local"
            ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text = "" Then
                objLCB.LCB_GSTCustBillStatus = "Local"
            End If

            If txtCompanyGSTN.Text <> "" And txtCustGSTN.Text <> "" Then
                objLCB.LCB_State = CheckSourceDestinationState(Trim(txtCompanyGSTN.Text), Trim(txtCustGSTN.Text))
            ElseIf txtCompanyGSTN.Text <> "" And txtCustGSTN.Text = "" Then
                objLCB.LCB_State = CheckSourceDestinationState(Trim(txtCompanyGSTN.Text), (""))
            ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text <> "" Then
                objLCB.LCB_State = CheckSourceDestinationState((""), Trim(txtCustGSTN.Text))
            End If


            'Chart Of Accounts'
            Dim iHead1, iGroup, iSubGroup, iGL, iChartID As Integer
            Dim sPerm As String = ""
            Dim sArray1 As Array
            Dim sName As String = ""

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead1 = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146

            'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
            sName = "Sale Of Product " & objLCB.LCB_State
            txtGLID.Text = objLCB.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
            Else
                iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
            End If


            sName = "Local GST " & objLCB.LCB_GSTRate & " % " & objLCB.LCB_State & " Sale Account"
            txtGLID.Text = objLCB.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", objLCB.LCB_GSTRate)
            End If

            sName = "Inter State GST " & objLCB.LCB_GSTRate & " % " & objLCB.LCB_State & " Sale Account"
            txtGLID.Text = objLCB.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", objLCB.LCB_GSTRate)
            End If

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead1 = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146

            sName = "OUTPUT SGST " & objLCB.LCB_GSTRate / 2 & " % " & objLCB.LCB_State & " Sale Account"
            txtGLID.Text = objLCB.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", objLCB.LCB_GSTRate / 2)
            End If

            sName = "OUTPUT CGST " & objLCB.LCB_GSTRate / 2 & " % " & objLCB.LCB_State & " Sale Account"
            txtGLID.Text = objLCB.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", objLCB.LCB_GSTRate / 2)
            End If

            sName = "OUTPUT IGST " & objLCB.LCB_GSTRate & " % " & objLCB.LCB_State & " Sale Account"
            txtGLID.Text = objLCB.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", objLCB.LCB_GSTRate)
            End If

            '    Next
            'End If

            For i = 0 To grdCustAmountDetails.Rows.Count - 1
                lblTotalAmount = grdCustAmountDetails.Rows(i).FindControl("lblTripAmount")
                lblGSTRate = grdCustAmountDetails.Rows(i).FindControl("lblGSTRate")

                lblGSTAmount = grdCustAmountDetails.Rows(i).FindControl("lblGSTAmount")
                objLCB.LCB_GSTAmount = lblGSTAmount.Text

                lblSGST = grdCustAmountDetails.Rows(i).FindControl("lblSGST")
                objLCB.LCB_SGST = lblSGST.Text

                lblSGSTAmount = grdCustAmountDetails.Rows(i).FindControl("lblSGSTAmount")
                objLCB.LCB_SGSTAmount = lblSGSTAmount.Text

                lblCGST = grdCustAmountDetails.Rows(i).FindControl("lblCGST")
                objLCB.LCB_CGST = lblCGST.Text

                lblCGSTAmount = grdCustAmountDetails.Rows(i).FindControl("lblCGSTAmount")
                objLCB.LCB_CGSTAmount = lblCGSTAmount.Text

                lblIGST = grdCustAmountDetails.Rows(i).FindControl("lblIGST")
                objLCB.LCB_IGST = lblIGST.Text

                lblIGSTAmount = grdCustAmountDetails.Rows(i).FindControl("lblIGSTAmount")
                objLCB.LCB_IGSTAmount = lblIGSTAmount.Text

                lblGrandTotal = grdCustAmountDetails.Rows(i).FindControl("lblTripAmount")
                objLCB.LCB_TotalAmt = lblGrandTotal.Text
            Next

            objLCB.LCB_Delflag = "W"
            objLCB.LCB_CompID = sSession.AccessCodeID
            objLCB.LCB_Status = "C"
            objLCB.LCB_Operation = "C"
            objLCB.LCB_IPAddress = sSession.IPAddress
            objLCB.LCB_CreatedBy = sSession.UserID
            objLCB.LCB_CreatedOn = Date.Today
            objLCB.LCB_ApprovedBy = Nothing
            objLCB.LCB_ApprovedOn = Date.Today
            objLCB.LCB_DeletedBy = Nothing
            objLCB.LCB_DeletedOn = Date.Today
            objLCB.LCB_UpdatedBy = sSession.UserID
            objLCB.LCB_UpdatedOn = Date.Today
            objLCB.LCB_YearID = sSession.YearID

            If Val(txtEscAmt.Text) <> 0 Then
                objLCB.LCB_EscAmt = txtEscAmt.Text
            Else
                objLCB.LCB_EscAmt = 0
            End If
            If Val(txtTotEscAmt.Text) <> 0 Then
                objLCB.LCB_TotalEscAmt = txtTotEscAmt.Text
            Else
                objLCB.LCB_TotalEscAmt = 0
            End If


            Arr = objLCB.SaveCustomerBilling(sSession.AccessCode, objLCB)
            'txtGLID.Text = 0

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                lblCustBillingValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                imgbtnSave.Visible = False 'btnDelete.Visible = True
                'btnSave.Text = "Save"
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
                lblCustBillingValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If  'btnDelete.Visible = True
                imgbtnSave.Visible = False
                ' btnSave.Text = "Update"
                imgbtnWaiting.Visible = True
                lblStatus.Text = "Waiting For Approval"
            End If
            LoadExistingInvoiceNo()
            ddlExistingInvoiceno.SelectedValue = Arr(1)
            ddlExistingInvoiceno_SelectedIndexChanged(sender, e)

            'grdCustAmountDetails.Visible = True
            'grdCustAmountDetails.DataSource = objLCB.BindAmountDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInvoiceno.SelectedValue)
            'grdCustAmountDetails.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub
    Public Function CheckSourceDestinationState(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            'sSource = sBillingAddress.Substring(0, 2)
            'sDestination = sDeliveryAddress.Substring(0, 2)

            'If sSource = sDestination Then
            '    CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
            'Else
            '    CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
            'End If
            If sBillingAddress <> "" And sDeliveryAddress = "" Then
                sSource = sBillingAddress.Substring(0, 2)
                CheckSourceDestinationState = objLCB.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sDeliveryAddress <> "" Then
                sDestination = sDeliveryAddress.Substring(0, 2)
                CheckSourceDestinationState = objLCB.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sDeliveryAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sDeliveryAddress.Substring(0, 2)
                If sSource = sDestination Then
                    CheckSourceDestinationState = objLCB.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    CheckSourceDestinationState = objLCB.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return CheckSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationState")
        End Try
    End Function
    Public Function CheckSourceDestinationOfCust(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            sSource = sBillingAddress.Substring(0, 2)
            sDestination = sDeliveryAddress.Substring(0, 2)

            If sSource = sDestination Then
                CheckSourceDestinationOfCust = "Local"
            Else
                CheckSourceDestinationOfCust = "Inter State"
            End If
            Return CheckSourceDestinationOfCust
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationOfCust")
        End Try
    End Function
    Private Sub grdCustAmountDetails_PreRender(sender As Object, e As EventArgs) Handles grdCustAmountDetails.PreRender
        Dim dt As New DataTable
        Try
            If grdCustAmountDetails.Rows.Count > 0 Then
                grdCustAmountDetails.UseAccessibleHeader = True
                grdCustAmountDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                grdCustAmountDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCustAmountDetails_PreRender")
        End Try
    End Sub

    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            imgbtnSave_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            'lblError.Text = ""
            'ddlExistingInvoiceno.SelectedIndex = 0 : ddlCustomers.SelectedIndex = 0 : ddlRoute.SelectedIndex = 0
            'txtFromDate.Text = "" : txtToDate.Text = "" : txtCustOrderRef.Text = ""
            'txtAggreement.Text = "" : txtCompanyAddress.Text = "" : txtCompanyGSTN.Text = ""
            'txtCustomerAddress.Text = "" : txtCustGSTN.Text = "" : txtCustGstnCategory.Text = ""
            'txtCustGstnCategoryId.Text = "" : txtTotalAmount.Text = "" : txtCustGSTRate.Text = ""
            'txtInvDate.Text = "" : txtInvNo.Text = ""
            'GenerateInvoiceNo()
            'grdCustAmountDetails.DataSource = Nothing
            'grdCustAmountDetails.DataBind()
            'grdCustAmountDetails.Visible = False
            ''    LoadRoute()
            ''   LoadParty()
            ''dgINVDetails.DataSource = Nothing
            ''dgINVDetails.DataBind()
            ''dgINVDetails.Visible = False
            'lblStatus.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Logistics/FrmLgstCustomerBilling.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub ddlExistingInvoiceno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingInvoiceno.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistingInvoiceno.SelectedIndex > 0 Then
                '  dgINVDetails.Visible = True
                grdCustAmountDetails.Visible = True
                BindDetails(ddlExistingInvoiceno.SelectedValue)

                dgINVDetails.DataSource = objLCB.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInvoiceno.SelectedValue)
                dgINVDetails.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingInvoiceno_SelectedIndexChanged")
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
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblGoods, lblUnit, lblOrderedQty As New Label
        Dim iMasterID As Integer
        Dim sStatus As String = ""
        Dim iCustID As Integer
        Dim iZoneID, iRegionID, iAreaID, iBranchID As Integer
        Dim dtPO As New DataTable
        Dim dEscAmt As Double = 0.0, dSgstAmt As Double = 0.0, dCgstAmt As Double = 0.0, dIgstAmt As Double = 0.0, dGstAmt As Double = 0.0
        Try
            lblError.Text = ""
            If ddlExistingInvoiceno.SelectedIndex = 0 Then
                lblError.Text = "Select Existing DispatchNo to Approve."
                lblCustBillingValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlExistingInvoiceno.SelectedIndex > 0 Then
                iMasterID = ddlExistingInvoiceno.SelectedValue
            End If

            If ddlCustomers.SelectedIndex > 0 Then
                iCustID = ddlCustomers.SelectedValue

            End If

            sStatus = objLCB.GetStatusOfInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInvoiceno.SelectedValue, ddlCustomers.SelectedValue)
            If UCase(sStatus) = Trim(UCase("A")) Then
                lblError.Text = "This is Already Approved."
                lblCustBillingValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            ''Creating JE For Sales once we approve'

            'dtPO = objDispatch.GetZone(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            'If dtPO.Rows.Count > 0 Then
            '    iZoneID = dtPO.Rows(0)("SPO_ZoneID")
            '    iRegionID = dtPO.Rows(0)("SPO_RegionID")
            '    iAreaID = dtPO.Rows(0)("SPO_AreaID")
            '    iBranchID = dtPO.Rows(0)("SPO_BranchID")
            'End If

            GetLgstInvGrid(iCustID, iMasterID)

            SaveSalesJEDetails(iMasterID)

            'If (IsDBNull(dgTipDetails.Items(0).Cells(1).Text) = False) And (dgTipDetails.Items(0).Cells(1).Text <> "&nbsp;") Then
            '    dEscAmt = dgTipDetails.Items(0).Cells(1).Text
            'Else
            '    dEscAmt = 0
            'End If
            'If (IsDBNull(dgTipDetails.Items(0).Cells(1).Text) = False) And (dgTipDetails.Items(0).Cells(1).Text <> "&nbsp;") Then
            '    dSgstAmt = dgTipDetails.Items(0).Cells(1).Text
            'Else
            '    dSgstAmt = 0
            'End If
            'If (IsDBNull(dgTipDetails.Items(0).Cells(1).Text) = False) And (dgTipDetails.Items(0).Cells(1).Text <> "&nbsp;") Then
            '    dCgstAmt = dgTipDetails.Items(0).Cells(1).Text
            'Else
            '    dCgstAmt = 0
            'End If
            'If (IsDBNull(dgTipDetails.Items(0).Cells(1).Text) = False) And (dgTipDetails.Items(0).Cells(1).Text <> "&nbsp;") Then
            '    dIgstAmt = dgTipDetails.Items(0).Cells(1).Text
            'Else
            '    dIgstAmt = 0
            'End If
            'If (IsDBNull(dgTipDetails.Items(0).Cells(1).Text) = False) And (dgTipDetails.Items(0).Cells(1).Text <> "&nbsp;") Then
            '    dGstAmt = dgTipDetails.Items(0).Cells(1).Text
            'Else
            '    dGstAmt = 0
            'End If

            '      objLCB.UpdateTripMaster(sSession.AccessCode, sSession.AccessCodeID, ddlExistingInvoiceno.SelectedValue, sSession.YearID, sFromDate, sToDate, ddlRoute.SelectedValue, dEscAmt, dSgstAmt, dCgstAmt, dIgstAmt, dGstAmt)


            ' objLCB.UpdateInvStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingInvoiceno.SelectedValue, sSession.YearID)

            lblError.Text = "Successfully Approved."
            lblCustBillingValidationMsg.Text = "Successfully Approved."
            lblStatus.Text = "Approved."
            imgbtnUpdate.Visible = False
            imgbtnWaiting.Visible = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Public Sub GetLgstInvGrid(ByVal iCustID As Integer, ByVal iMasterID As Integer)
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
        Dim sArrayRoute As Array
        Dim iParty As Integer = 0

        Dim dt1 As New DataTable
        Dim dPartyTotal As Double
        Dim dtGSTRates As New DataTable : Dim sSql As String = ""
        Dim dTotalAmt, dSGSTAmt, dCGSTAmt, dIGSTAmt As Double
        Dim SGST, CGST, IGST As Double
        Dim sTypeOfBill As String = "" : Dim sState As String = ""
        Dim iGSTrate As Integer = 0
        Dim iRouteId As Integer = 0
        Dim sRoute As String = ""
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
            iParty = iCustID


            sTypeOfBill = objDBL.SQLGetDescription(sSession.AccessCode, "Select LCB_GSTCustBillStatus From lgst_customerBilling Where LCB_ID=" & iMasterID & " And LCB_CompID=" & sSession.AccessCodeID & " And LCB_YearID=" & sSession.YearID & " ")
            sState = objDBL.SQLGetDescription(sSession.AccessCode, "Select LCB_State From lgst_customerBilling Where LCB_ID=" & iMasterID & " And LCB_CompID=" & sSession.AccessCodeID & " And LCB_YearID=" & sSession.YearID & " ")
            iGSTrate = objDBL.SQLGetDescription(sSession.AccessCode, "Select LCB_GSTRate From lgst_customerBilling Where LCB_ID=" & iMasterID & " And LCB_CompID=" & sSession.AccessCodeID & " And LCB_YearID=" & sSession.YearID & " ")
            iRouteId = objDBL.SQLGetDescription(sSession.AccessCode, "Select LCB_ID From lgst_customerBilling Where LCB_ID=" & iMasterID & " And LCB_CompID=" & sSession.AccessCodeID & " And LCB_YearID=" & sSession.YearID & " ")

            If iRouteId > 0 Then
                sRoute = objDBL.SQLGetDescription(sSession.AccessCode, "Select LRM_StartDestPlace From lgst_route_master Where LRM_ID=" & iRouteId & " And LRM_CompID=" & sSession.AccessCodeID & " And LRM_YearID=" & sSession.YearID & " and LRM_Delflag='A' ")
                'If sRoute > "" Then
                '    sArrayRoute = sRoute.Split("-")
                '    'dRow("GLCode") = sArray(0)
                '    sRoute = sArrayRoute(0) & "-" & sArrayRoute(1)
                'End If

            End If

            'dtGSTRates.Rows.Add(iGSTrate)
            ''Extra'
            'dtGSTRates.Rows.Add("0")
            'Extra'

            'If dtGSTRates.Rows.Count > 0 Then
            'For k = 0 To dtGSTRates.Rows.Count - 1

            dt1 = objDBL.SQLExecuteDataSet(sSession.AccessCode, "Select * From lgst_customerBilling Where LCB_GSTRate=" & iGSTrate & " And LCB_ID=" & iMasterID & " and LCB_YearID= " & sSession.YearID & " And LCB_CompID=" & sSession.AccessCodeID & " ").Tables(0)
            If dt1.Rows.Count > 0 Then
                    For z = 0 To dt1.Rows.Count - 1
                        dTotalAmt = dTotalAmt + dt1.Rows(z)("LCB_TotalAmt")
                        dSGSTAmt = dSGSTAmt + dt1.Rows(z)("LCB_SGSTAmount")
                        dCGSTAmt = dCGSTAmt + dt1.Rows(z)("LCB_CGSTAmount")
                        dIGSTAmt = dIGSTAmt + dt1.Rows(z)("LCB_IGSTAmount")
                        Dim dtotalAmount = (dTotalAmt + dSGSTAmt + dCGSTAmt + dIGSTAmt)
                        dPartyTotal = dPartyTotal + Convert.ToDecimal(dtotalAmount)
                    Next

                    dRow = dt.NewRow 'Item Name
                    dRow("Id") = 0
                dRow("HeadID") = objLCB.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") '"Sale Of Product " & sState
                dRow("GLID") = objLCB.GetCOAGLID(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") '"Sale Of Product " & sState
                If sTypeOfBill = "Local" Then
                    dRow("SubGLID") = objLCB.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, sRoute) '"Local GST " & iGSTrate & " % " & sState & " Sale Account"
                ElseIf sTypeOfBill = "Inter State" Then
                    dRow("SubGLID") = objLCB.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, sRoute) '"Inter State GST " & iGSTrate & " % " & sState & " Sale Account"
                End If
                    dRow("PaymentID") = 5
                    dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Frieght Earnings" '"Sale Of Material"

                sGL = objLCB.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                    If sGL <> "" Then
                        sArray = sGL.Split("-")
                        dRow("GLCode") = sArray(0)
                        dRow("GLDescription") = sArray(1)
                    End If

                    sSubGL = objLCB.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                    If sSubGL <> "" Then
                        sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    If sArray.Length = 4 Then
                        dRow("SubGLDescription") = sArray(1) & "-" & sArray(2) & "-" & sArray(3)
                    Else
                        dRow("SubGLDescription") = sArray(1) & "-" & sArray(2)
                    End If
                End If

                    dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(dTotalAmt).ToString("#,##0.00") 'dTotalAmt
                dt.Rows.Add(dRow)


                SGST = iGSTrate / 2
                CGST = iGSTrate / 2
                IGST = iGSTrate

                dRow = dt.NewRow 'SGST
                    dRow("Id") = 0
                    dRow("HeadID") = objLCB.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                    dRow("GLID") = objLCB.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                dRow("SubGLID") = objLCB.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output SGST " & SGST & " % " & sState & " Sale Account")

                dRow("PaymentID") = 6
                    dRow("SrNo") = dt.Rows.Count + 1
                    dRow("Type") = "SGST"

                    sGL = objLCB.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                    If sGL <> "" Then
                        sArray = sGL.Split("-")
                        dRow("GLCode") = sArray(0)
                        dRow("GLDescription") = sArray(1)
                    End If

                    sSubGL = objLCB.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                    If sSubGL <> "" Then
                        sArray = sSubGL.Split("-")
                        dRow("SubGL") = sArray(0)
                        dRow("SubGLDescription") = sArray(1)
                    End If

                dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(dSGSTAmt).ToString("#,##0.00") 'dSGSTAmt
                dt.Rows.Add(dRow)

                    dRow = dt.NewRow 'CGST
                    dRow("Id") = 0
                    dRow("HeadID") = objLCB.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                    dRow("GLID") = objLCB.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                    dRow("SubGLID") = objLCB.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output CGST " & CGST & " % " & sState & " Sale Account")
                    dRow("PaymentID") = 7
                    dRow("SrNo") = dt.Rows.Count + 1
                    dRow("Type") = "CGST"

                    sGL = objLCB.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                    If sGL <> "" Then
                        sArray = sGL.Split("-")
                        dRow("GLCode") = sArray(0)
                        dRow("GLDescription") = sArray(1)
                    End If

                    sSubGL = objLCB.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                    If sSubGL <> "" Then
                        sArray = sSubGL.Split("-")
                        dRow("SubGL") = sArray(0)
                        dRow("SubGLDescription") = sArray(1)
                    End If

                dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(dCGSTAmt).ToString("#,##0.00") 'dCGSTAmt
                dt.Rows.Add(dRow)

                    dRow = dt.NewRow 'IGST
                    dRow("Id") = 0
                    dRow("HeadID") = objLCB.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                    dRow("GLID") = objLCB.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                    dRow("SubGLID") = objLCB.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output IGST " & IGST & " % " & sState & " Sale Account")
                    dRow("PaymentID") = 8
                    dRow("SrNo") = dt.Rows.Count + 1
                    dRow("Type") = "IGST"

                    sGL = objLCB.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                    If sGL <> "" Then
                        sArray = sGL.Split("-")
                        dRow("GLCode") = sArray(0)
                        dRow("GLDescription") = sArray(1)
                    End If

                    sSubGL = objLCB.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                    If sSubGL <> "" Then
                        sArray = sSubGL.Split("-")
                        dRow("SubGL") = sArray(0)
                        dRow("SubGLDescription") = sArray(1)
                    End If

                dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(dIGSTAmt).ToString("#,##0.00") ' dIGSTAmt
                dt.Rows.Add(dRow)

                    dTotalAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0 : dIGSTAmt = 0
                End If

                'Next

                dRow = dt.NewRow 'Party/Customer
                dRow("Id") = 0
                dRow("HeadID") = objLCB.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_Head")
                dRow("GLID") = objLCB.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_GL")
                dRow("SubGLID") = objLCB.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "C")
                dRow("PaymentID") = 9
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Party/Customer"

                sGL = objLCB.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

                sSubGL = objLCB.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                If sArray.Length = 3 Then
                    dRow("SubGLDescription") = sArray(1) & "-" & sArray(2)
                Else
                    dRow("SubGLDescription") = sArray(1)
                End If
            End If
            dRow("Debit") = Convert.ToDecimal(dPartyTotal).ToString("#,##0.00") 'dPartyTotal
            dRow("Credit") = Convert.ToDecimal(0).ToString("#,##0.00") '0

            txtBillAmount.Text = dPartyTotal

                dt.Rows.Add(dRow)

            'End If
            dgINVDetails.Visible = True
            dgINVDetails.DataSource = dt
            dgINVDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetDefaultGridPurchase")
        End Try
    End Sub
    Private Function CheckDebitAndCredit() As Integer
        Dim i As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Try
            For i = 0 To dgINVDetails.Items.Count - 1
                If (IsDBNull(dgINVDetails.Items(i).Cells(12).Text) = False) And (dgINVDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    dDebit = dDebit + Convert.ToDouble(dgINVDetails.Items(i).Cells(12).Text)
                End If

                If (IsDBNull(dgINVDetails.Items(i).Cells(13).Text) = False) And (dgINVDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    dCredit = dCredit + Convert.ToDouble(dgINVDetails.Items(i).Cells(13).Text)
                End If
            Next

            If String.Format("{0:0.00}", Convert.ToDecimal(dDebit)) <> String.Format("{0:0.00}", Convert.ToDecimal(dCredit)) Then
                Return 1  ' Debit and Credit amount not Matched
            End If

            If dDebit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(dDebit)) <> String.Format("{0:0.00}", Convert.ToDecimal(txtBillAmount.Text)) Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            Else
                If dDebit <> txtBillAmount.Text Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            End If

            If dCredit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(dCredit)) <> String.Format("{0:0.00}", Convert.ToDecimal(txtBillAmount.Text)) Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            Else
                If dCredit <> txtBillAmount.Text Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckDebitAndCredit")
        End Try
    End Function
    Public Sub SaveSalesJEDetails(ByVal iMasterID As Integer)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Try
            ' iRet = CheckDebitAndCredit()

            If iRet = 1 Then
                lblCustBillingValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 2 Then
                lblCustBillingValidationMsg.Text = "Amount Not Matched with Advance Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 3 Then
                lblCustBillingValidationMsg.Text = "Amount Not Matched with Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 4 Then
                lblCustBillingValidationMsg.Text = "Total Debit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 5 Then
                lblCustBillingValidationMsg.Text = "Total Credit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            objLCB.UpdateInvStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingInvoiceno.SelectedValue, sSession.YearID)

            For i = 0 To dgINVDetails.Items.Count - 1


                objLCB.iATD_TrType = 15


                If (IsDBNull(dgINVDetails.Items(i).Cells(0).Text) = False) And (dgINVDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objLCB.iATD_ID = dgINVDetails.Items(i).Cells(0).Text
                Else
                    objLCB.iATD_ID = 0
                End If

                objLCB.dATD_TransactionDate = Date.ParseExact(txtInvDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) ' DateTime.Today

                objLCB.iATD_BillId = ddlExistingInvoiceno.SelectedValue
                objLCB.iATD_PaymentType = dgINVDetails.Items(i).Cells(4).Text

                If (IsDBNull(dgINVDetails.Items(i).Cells(1).Text) = False) And (dgINVDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objLCB.iATD_Head = dgINVDetails.Items(i).Cells(1).Text
                Else
                    objLCB.iATD_Head = 0
                End If


                If (IsDBNull(dgINVDetails.Items(i).Cells(2).Text) = False) And (dgINVDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objLCB.iATD_GL = dgINVDetails.Items(i).Cells(2).Text
                Else
                    objLCB.iATD_GL = 0
                End If

                If (IsDBNull(dgINVDetails.Items(i).Cells(3).Text) = False) And (dgINVDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objLCB.iATD_SubGL = dgINVDetails.Items(i).Cells(3).Text
                Else
                    objLCB.iATD_SubGL = 0
                End If

                If (IsDBNull(dgINVDetails.Items(i).Cells(12).Text) = False) And (dgINVDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objLCB.dATD_Debit = Convert.ToDouble(dgINVDetails.Items(i).Cells(12).Text)
                Else
                    objLCB.dATD_Debit = 0
                End If

                If (IsDBNull(dgINVDetails.Items(i).Cells(13).Text) = False) And (dgINVDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objLCB.dATD_Credit = Convert.ToDouble(dgINVDetails.Items(i).Cells(13).Text)
                Else
                    objLCB.dATD_Credit = 0
                End If

                If objLCB.dATD_Debit > 0 And objLCB.dATD_Credit = 0 Then
                    objLCB.iATD_DbOrCr = 1 'Debit
                ElseIf objLCB.dATD_Debit = 0 And objLCB.dATD_Credit > 0 Then
                    objLCB.iATD_DbOrCr = 2 'Credit
                End If

                objLCB.iATD_CreatedBy = sSession.UserID
                objLCB.dATD_CreatedOn = DateTime.Today

                objLCB.sATD_Status = "A"
                objLCB.iATD_YearID = sSession.YearID
                objLCB.sATD_Operation = "C"
                objLCB.sATD_IPAddress = sSession.IPAddress

                objLCB.iATD_UpdatedBy = sSession.UserID
                objLCB.dATD_UpdatedOn = DateTime.Today

                objLCB.iATD_CompID = sSession.AccessCodeID

                objLCB.iATD_ZoneID = 2
                objLCB.iATD_RegionID = 3
                objLCB.iATD_AreaID = 4
                objLCB.iATD_BranchID = 5

                objLCB.dATD_OpenDebit = "0.00"
                objLCB.dATD_OpenCredit = "0.00"
                objLCB.dATD_ClosingDebit = "0.00"
                objLCB.dATD_ClosingCredit = "0.00"
                objLCB.iATD_SeqReferenceNum = 0

                objLCB.SaveUpdateTransactionDetails(sSession.AccessCode, objLCB)

            Next

            objLCB.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingInvoiceno.SelectedValue, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)

            lblCustBillingValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            dgINVDetails.DataSource = objLCB.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInvoiceno.SelectedValue)
            dgINVDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveSalesJEDetails")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Logistics/FrmLgstCustBillingDashboard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub ddlRoute_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoute.SelectedIndexChanged
        Dim icount As Integer
        Dim dToDate As String = ""
        Try
            lblError.Text = ""
            If ddlCustomers.SelectedIndex > 0 Then
                icount = objLCB.GetRouteCustCount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomers.SelectedValue, ddlRoute.SelectedValue)
                If icount > 0 Then
                    dToDate = objLCB.GetRouteCustdate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomers.SelectedValue, ddlRoute.SelectedValue)
                    If dToDate <> "" Then
                        txtFromDate.Text = objGen.FormatDtForRDBMS(dToDate, "D")
                    Else
                        txtFromDate.Text = ""
                    End If
                End If
            Else
                lblError.Text = "Select Customer"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRoute_SelectedIndexChanged")
        End Try
    End Sub

End Class
