Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Inventory_InventoryMasterDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Inventory_InventoryMasterDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Private objclsModulePermission As New clsModulePermission
    Dim objGenFun As New clsGeneralFunctions
    Dim objInv As New clsInvenotryDetails
    Private Shared sSession As AllSession
    Private Shared sIMDSave As String
    Private objDBL As New DatabaseLayer.DBHelper
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Dim sFormButtons As String = ""
        Try
            RFVCommodity.InitialValue = "Select Commodity" : RFVItem.InitialValue = "Select Item"
            RFVddlUnit.InitialValue = "Select Unit of Measurement" : RFVddlAlternative.InitialValue = "Select Alternative Unit"
            RFVCtgry.InitialValue = "Select Price Category" : RFVCtgry.InitialValue = "Select Price Category"
            RFVddlVAT.InitialValue = "Select VAT" : RFVddlCST.InitialValue = "Select CST" : RFVddlExcise.InitialValue = "Select Excise Duty"

            sSession = Session("AllSession")
            If IsPostBack = False Then

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "INMD")
                imgbtnSave.Visible = False : imgbtnAdd.Visible = False : sIMDSave = "NO" : btnAddTaxDetails.Visible = False : btnUpdateTaxDetails.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        sIMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If
                'If sSession.YearID > 0 Then
                '    sStr = sSession.YearName
                '    sArray = sStr.Split("-")
                '    iSYear = sArray(0)
                '    iEYear = sArray(1)

                'dStartDate = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'dEndDate = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                '    MtxtEffeFrom_CalendarExtender.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    MtxtEffeFrom_CalendarExtender.EndDate = New DateTime(iEYear, 3, dEndDate)

                '    MtxtEffeTo_CalendarExtender.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    MtxtEffeTo_CalendarExtender.EndDate = New DateTime(iEYear, 3, dEndDate)

                '    RtxtEffeFrom_CalendarExtender.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    RtxtEffeFrom_CalendarExtender.EndDate = New DateTime(iEYear, 3, dEndDate)

                '    RtxtEffeTo_CalendarExtender.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    RtxtEffeTo_CalendarExtender.EndDate = New DateTime(iEYear, 3, dEndDate)

                '    PtxtEffeFrom_CalendarExtender.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    PtxtEffeFrom_CalendarExtender.EndDate = New DateTime(iEYear, 3, dEndDate)

                '    PtxtEffeTo_CalendarExtender.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    PtxtEffeTo_CalendarExtender.EndDate = New DateTime(iEYear, 3, dEndDate)

                'End If
                'rgvMtxtEffeFrom.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvMtxtEffeFrom.MinimumValue = "" & dStartDate & ""
                'rgvMtxtEffeFrom.MaximumValue = "" & dEndDate & ""

                'rgvMtxtEffeTo.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvMtxtEffeTo.MinimumValue = "" & dStartDate & ""
                'rgvMtxtEffeTo.MaximumValue = "" & dEndDate & ""

                'rgvRtxtEffeFrom.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvRtxtEffeFrom.MinimumValue = "" & dStartDate & ""
                'rgvRtxtEffeFrom.MaximumValue = "" & dEndDate & ""

                'rgvRtxtEffeTo.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvRtxtEffeTo.MinimumValue = "" & dStartDate & ""
                'rgvRtxtEffeTo.MaximumValue = "" & dEndDate & ""

                'rgvPtxtEffeFrom.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvPtxtEffeFrom.MinimumValue = "" & dStartDate & ""
                'rgvPtxtEffeFrom.MaximumValue = "" & dEndDate & ""

                'rgvPtxtEffeTo.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvPtxtEffeTo.MinimumValue = "" & dStartDate & ""
                'rgvPtxtEffeTo.MaximumValue = "" & dEndDate & ""

                txtStartDate.Text = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                txtEndDate.Text = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                imgbtnUpdate.Visible = False

                lblhistoryID.Text = "0"
                LoadUnitOfMeasurement()
                LoadAlternativeUnitOfMeasurement()
                LoadCommodity()
                LoadItem(0)

                LoadVAT()
                LoadCST()
                LoadExciseDuty()

                loadType()
                'Me.imgbtnUpdate.Attributes.Add("OnClick", "return validationAll()")
                'Me.MbtnSave.Attributes.Add("OnClick", "return validate()")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objInv.LoadCommodity(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "INV_Description"
            ddlCommodity.DataValueField = "INV_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadItem(ByVal iCommodityID As Integer)
        'Dim iParentID As Integer
        Try
            'If ddlCommodity.SelectedIndex > 0 Then
            '    iParentID = ddlCommodity.SelectedValue
            'Else
            '    iParentID = 0
            'End If
            ddlItem.DataSource = objInv.LoadItem(sSession.AccessCode, sSession.AccessCodeID, iCommodityID)
            ddlItem.DataTextField = "INV_Code"
            ddlItem.DataValueField = "INV_ID"
            ddlItem.DataBind()
            ddlItem.Items.Insert(0, "Select Item")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadVAT()
        Try
            ddlVAT.DataSource = objInv.LoadVAT(sSession.AccessCode, sSession.AccessCodeID)
            ddlVAT.DataTextField = "Mas_Desc"
            ddlVAT.DataValueField = "Mas_ID"
            ddlVAT.DataBind()
            ddlVAT.Items.Insert(0, "Select VAT")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCST()
        Try
            ddlCST.DataSource = objInv.LoadCST(sSession.AccessCode, sSession.AccessCodeID)
            ddlCST.DataTextField = "Mas_Desc"
            ddlCST.DataValueField = "Mas_ID"
            ddlCST.DataBind()
            ddlCST.Items.Insert(0, "Select CST")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExciseDuty()
        Try
            ddlExcise.DataSource = objInv.LoadExciseDuty(sSession.AccessCode, sSession.AccessCodeID)
            ddlExcise.DataTextField = "Mas_Desc"
            ddlExcise.DataValueField = "Mas_ID"
            ddlExcise.DataBind()
            ddlExcise.Items.Insert(0, "Select Excise Duty")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadUnitOfMeasurement()
        Try
            ddlUnit.DataSource = objInv.LoadUnitOfMeasurement(sSession.AccessCode, sSession.AccessCodeID)
            ddlUnit.DataTextField = "Mas_Desc"
            ddlUnit.DataValueField = "Mas_Id"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, "Select Unit of Measurement")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadAlternativeUnitOfMeasurement()
        Try
            ddlAlternative.DataSource = objInv.LoadUnitOfMeasurement(sSession.AccessCode, sSession.AccessCodeID)
            ddlAlternative.DataTextField = "Mas_Desc"
            ddlAlternative.DataValueField = "Mas_Id"
            ddlAlternative.DataBind()
            ddlAlternative.Items.Insert(0, "Select Alternative Unit")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadType()
        Dim dt As New DataTable
        Try
            ddlCtgry.DataSource = objInv.LoadType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCtgry.DataTextField = "Mas_Desc"
            ddlCtgry.DataValueField = "Mas_ID"
            ddlCtgry.DataBind()
            ddlCtgry.Items.Insert(0, "Select Price Category")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim InvHistryID As Integer = 0
        Dim sComodity As String = ""
        Dim sItem As String = ""
        Dim dt As New DataTable
        Dim Arr() As String

        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            lblError.Text = ""

            If MtxtEffeFrom.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(MtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Effective From (" & MtxtEffeFrom.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    MtxtEffeFrom.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(MtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Effective From (" & MtxtEffeFrom.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    MtxtEffeFrom.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If MtxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(MtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Effective To (" & MtxtEffeTo.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    MtxtEffeTo.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(MtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Effective To (" & MtxtEffeTo.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    MtxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If RtxtEffeFrom.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(RtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Effective From (" & RtxtEffeFrom.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    RtxtEffeFrom.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(RtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Effective From (" & RtxtEffeFrom.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    RtxtEffeFrom.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If RtxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(RtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Effective To (" & RtxtEffeTo.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    RtxtEffeTo.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(RtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Effective To (" & RtxtEffeTo.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    RtxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If PtxtEffeFrom.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(PtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Effective From (" & PtxtEffeFrom.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    PtxtEffeFrom.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(PtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Effective From (" & PtxtEffeFrom.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    PtxtEffeFrom.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If PtxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(PtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Effective To (" & PtxtEffeTo.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    PtxtEffeTo.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(PtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Effective To (" & PtxtEffeTo.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    PtxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If


            'If lblExistMRP.Text > 0 Then
            '    'If item used we are not allowing to update details'
            '    If objInv.CheckMasterINUSE(sSession.AccessCode, sSession.AccessCodeID, 1, lblExistMRP.Text) = False Then

            '    Else
            '        lblError.Text = "Master Data used it can not be Updated."
            '        lblCustomerValidationMsg.Text = lblError.Text
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '        Exit Sub
            '    End If
            '    'If item used we are not allowing to update details'
            'End If

            If lblExistMRP.Text <> "" Then
                objInv.INVH_ID = lblExistMRP.Text
            Else
                objInv.INVH_ID = 0
            End If
            If lblNode.Text = "0" Then
                objInv.INVH_Unit = 0
                objInv.INVH_AlterUnit = 0
                objInv.INVH_INV_ID = 0
            Else
                objInv.INVH_Unit = ddlUnit.SelectedValue
                objInv.INVH_AlterUnit = ddlAlternative.SelectedValue
                objInv.INVH_INV_ID = lblNode.Text
            End If
            objInv.INVH_Flag = "X"
            objInv.InvH_Operation = "C"
            objInv.InvH_IPAddress = sSession.IPAddress
            objInv.INVH_CreatedBy = sSession.UserID
            objInv.INVH_CompID = sSession.AccessCodeID

            If txtMRP.Text = "" Then
                objInv.INVH_MRP = 0
            Else
                objInv.INVH_MRP = objGen.SafeSQL(txtMRP.Text)
            End If
            If txtRetail.Text = "" Then
                objInv.INVH_Retail = 0
            Else
                objInv.INVH_Retail = objGen.SafeSQL(txtRetail.Text)
            End If

            If txtpreDeterminePrice.Text = "" Then
                objInv.INVH_PreDeterminedPrice = 0
            Else
                objInv.INVH_PreDeterminedPrice = objGen.SafeSQL(txtpreDeterminePrice.Text)
            End If

            If MtxtEffeFrom.Text <> "" Then
                objInv.INVH_EffeFrom = DateTime.ParseExact(MtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.INVH_EffeFrom = "01/01/1900"
            End If

            If MtxtEffeTo.Text <> "" Then
                objInv.INVH_EffeTo = Date.ParseExact(Trim(MtxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.INVH_EffeTo = "01/01/1900"
            End If


            If (MtxtEffeTo.Text <> "") And (MtxtEffeTo.Text <> "01/01/1900") Then
                dDate = Date.ParseExact(MtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(MtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'Dim m As Integer
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Effective To Date (" & MtxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & MtxtEffeFrom.Text & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    MtxtEffeTo.Focus()
                    Exit Sub
                End If
            End If

            If RtxtEffeFrom.Text <> "" Then
                objInv.INVH_RetailEffeFrom = DateTime.ParseExact(RtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.INVH_RetailEffeFrom = "01/01/1900"
            End If
            If RtxtEffeTo.Text <> "" Then
                objInv.INVH_RetailEffeTo = Date.ParseExact(Trim(RtxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.INVH_RetailEffeTo = "01/01/1900"
            End If

            If (RtxtEffeTo.Text <> "") And (RtxtEffeTo.Text <> "01/01/1900") Then
                Dim dRFDate, dRTSDate As Date
                dRFDate = Date.ParseExact(RtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dRTSDate = Date.ParseExact(RtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim r As Integer
                r = DateDiff(DateInterval.Day, dRFDate, dRTSDate)
                If r < 0 Then
                    lblError.Text = "Effective To Date (" & RtxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & RtxtEffeFrom.Text & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    RtxtEffeTo.Focus()
                    Exit Sub
                End If
            End If

            If PtxtEffeFrom.Text <> "" Then
                objInv.INVH_PurchaseEffeFrom = DateTime.ParseExact(PtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.INVH_PurchaseEffeFrom = "01/01/1900"
            End If
            If PtxtEffeTo.Text <> "" Then
                objInv.INVH_PurchaseEffeTo = Date.ParseExact(Trim(PtxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.INVH_PurchaseEffeTo = "01/01/1900"
            End If

            If (PtxtEffeTo.Text <> "") And (PtxtEffeTo.Text <> "01/01/1900") Then
                Dim dPFDate, dPTSDate As Date
                dPFDate = Date.ParseExact(PtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dPTSDate = Date.ParseExact(PtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim p As Integer
                p = DateDiff(DateInterval.Day, dPFDate, dPTSDate)
                If p < 0 Then
                    lblError.Text = "Effective To Date (" & PtxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & PtxtEffeFrom.Text & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    PtxtEffeTo.Focus()
                    Exit Sub
                End If
            End If

            If txtPerPieces.Text = "" Then
                objInv.INVH_PerPieces = 0
            Else
                objInv.INVH_PerPieces = objGen.SafeSQL(txtPerPieces.Text)
            End If
            If ddlCtgry.SelectedIndex > 0 Then
                objInv.INVH_CategoryID = ddlCtgry.SelectedValue
            Else
                objInv.INVH_CategoryID = 0
            End If

            If MtxtEffeFrom.Text <> "" And RtxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(MtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(RtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblError.Text = "All Effective From Dates should be equal."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    RtxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If


            If MtxtEffeFrom.Text <> "" And PtxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(MtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(PtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblError.Text = "All Effective From Dates should be equal."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    PtxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If


            If RtxtEffeFrom.Text <> "" And PtxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(RtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(PtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblError.Text = "All Effective From Dates should be equal."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    RtxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If

            Arr = objInv.SaveInventoryDetails(sSession.AccessCode, sSession.AccessCodeID, objInv)
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                lblCustomerValidationMsg.Text = lblError.Text
                imgbtnUpdate.Visible = False
                If sIMDSave = "YES" Then
                    imgbtnSave.Visible = True
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
                lblCustomerValidationMsg.Text = lblError.Text
                imgbtnSave.Visible = False
                If sIMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If

            'loadDetails(0, InvHistryID)
            LoadHistory(lblNode.Text)
            lblExistMRP.Text = "0"
            txtMRP.Text = "" : txtRetail.Text = "" : ddlCtgry.SelectedIndex = 0
            txtpreDeterminePrice.Text = "" : MtxtEffeFrom.Text = "" : MtxtEffeTo.Text = ""
            RtxtEffeFrom.Text = "" : RtxtEffeTo.Text = "" : PtxtEffeFrom.Text = "" : PtxtEffeTo.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdate_Click")
        End Try
    End Sub
    Private Sub LoadHistory(ByVal iItemID As Integer)
        Dim dt As New DataTable
        Try
            'loadDetails(iItemID, iINVHID)
            dt = objInv.BindHistory(sSession.AccessCode, sSession.AccessCodeID, iItemID)
            dgHistory.DataSource = dt
            dgHistory.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadHistory")
        End Try
    End Sub
    Private Sub loadDetails(ByVal iItemID As Integer, ByVal iINVHID As Integer)
        Dim dt As New DataTable
        Try
            dt = objInv.BindDetails(sSession.AccessCode, sSession.AccessCodeID, iItemID, iINVHID)
            If (dt.Rows.Count > 0) Then
                If dt.Rows(0)("InvH_ID").ToString() <> "" Then
                    lblhistoryID.Text = dt.Rows(0)("InvH_ID").ToString()
                Else
                    lblhistoryID.Text = "0"
                End If
                If dt.Rows(0)("InvH_Unit").ToString() <> "" Then
                    ddlUnit.SelectedValue = dt.Rows(0)("InvH_Unit").ToString()
                Else
                    ddlUnit.SelectedIndex = 0
                End If
                ddlUnit.Enabled = False

                If dt.Rows(0)("InvH_AlterUnit").ToString() <> "" Then
                    ddlAlternative.SelectedValue = dt.Rows(0)("InvH_AlterUnit").ToString()
                Else
                    ddlAlternative.SelectedIndex = 0
                End If
                ddlAlternative.Enabled = False

                If dt.Rows(0)("InvH_PerPieces").ToString() <> "" Then
                    txtPerPieces.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("InvH_PerPieces").ToString())
                Else
                    txtPerPieces.Text = ""
                End If
                txtPerPieces.Enabled = False

                If dt.Rows(0)("InvH_MRP").ToString() <> "" Then
                    txtMRP.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("InvH_MRP").ToString())
                Else
                    txtMRP.Text = ""
                End If

                If dt.Rows(0)("InvH_Retail").ToString() <> "" Then
                    txtRetail.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("InvH_Retail").ToString())
                Else
                    txtRetail.Text = ""
                End If

                If dt.Rows(0)("InvH_PreDeterminedPrice").ToString() <> "" Then
                    txtpreDeterminePrice.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("InvH_PreDeterminedPrice").ToString())
                Else
                    txtpreDeterminePrice.Text = ""
                End If

                If (IsDBNull(dt.Rows(0)("INVH_CategoryID")) = False) Then
                    If dt.Rows(0)("INVH_CategoryID") > 0 Then
                        ddlCtgry.SelectedValue = Convert.ToDecimal(dt.Rows(0)("INVH_CategoryID"))
                    End If
                End If

                If dt.Rows(0)("InvH_EffeFrom").ToString() <> "" Then
                    MtxtEffeFrom.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("InvH_EffeFrom").ToString(), "D")
                    If ((MtxtEffeFrom.Text = "01/01/1900") Or (MtxtEffeFrom.Text = "01-01-1900")) Or ((MtxtEffeFrom.Text = "01/01/2000") Or (MtxtEffeFrom.Text = "01-01-2000")) Then
                        MtxtEffeFrom.Text = ""
                    End If
                Else
                    MtxtEffeFrom.Text = ""
                End If
                MtxtEffeFrom.Enabled = True

                If dt.Rows(0)("INVH_RetailEffeFrom").ToString() <> "" Then
                    RtxtEffeFrom.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("INVH_RetailEffeFrom").ToString(), "D")
                    If ((RtxtEffeFrom.Text = "01/01/1900") Or (RtxtEffeFrom.Text = "01-01-1900")) Or ((RtxtEffeFrom.Text = "01/01/2000") Or (RtxtEffeFrom.Text = "01-01-2000")) Then
                        RtxtEffeFrom.Text = ""
                    End If
                Else
                    RtxtEffeFrom.Text = ""
                End If
                RtxtEffeFrom.Enabled = True

                If dt.Rows(0)("INVH_PurchaseEffeFrom").ToString() <> "" Then
                    PtxtEffeFrom.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("INVH_PurchaseEffeFrom").ToString(), "D")
                    If ((PtxtEffeFrom.Text = "01/01/1900") Or (PtxtEffeFrom.Text = "01-01-1900")) Or ((PtxtEffeFrom.Text = "01/01/2000") Or (PtxtEffeFrom.Text = "01-01-2000")) Then
                        PtxtEffeFrom.Text = ""
                    End If
                Else
                    PtxtEffeFrom.Text = ""
                End If
                PtxtEffeFrom.Enabled = True

                If dt.Rows(0)("InvH_EffeTo").ToString() <> "" Then
                    MtxtEffeTo.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("InvH_EffeTo").ToString(), "D")
                    If (MtxtEffeTo.Text = "01/01/1900" Or MtxtEffeTo.Text = "01-01-2000") Then
                        MtxtEffeTo.Text = ""
                    End If
                Else
                    MtxtEffeTo.Text = ""
                End If

                If dt.Rows(0)("INVH_RetailEffeTo").ToString() <> "" Then
                    RtxtEffeTo.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("INVH_RetailEffeTo").ToString(), "D")
                    If (RtxtEffeTo.Text = "01/01/1900" Or RtxtEffeTo.Text = "01-01-2000") Then
                        RtxtEffeTo.Text = ""
                    End If
                Else
                    RtxtEffeTo.Text = ""
                End If

                If dt.Rows(0)("INVH_PurchaseEffeTo").ToString() <> "" Then
                    PtxtEffeTo.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("INVH_PurchaseEffeTo").ToString(), "D")
                    If (PtxtEffeTo.Text = "01/01/1900" Or PtxtEffeTo.Text = "01-01-2000") Then
                        PtxtEffeTo.Text = ""
                    End If
                Else
                    PtxtEffeTo.Text = ""
                End If

                'Extra'
                txtMRP.Enabled = True : txtRetail.Enabled = True : txtpreDeterminePrice.Enabled = True
                MtxtEffeTo.Enabled = True : ddlCtgry.Enabled = True
                RtxtEffeTo.Enabled = True : PtxtEffeTo.Enabled = True
                'Extra'
            Else
                ddlUnit.Enabled = True
                ddlAlternative.Enabled = True
                txtMRP.Enabled = True
                txtPerPieces.Enabled = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Dim iCommodityID As Integer
        Try
            lblError.Text = ""
            If ddlCommodity.SelectedIndex > 0 Then
                iCommodityID = ddlCommodity.SelectedValue
            Else
                iCommodityID = 0
            End If
            LoadItem(iCommodityID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlItem.SelectedIndexChanged
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim lParent As Long
        Dim sNodeDesc As String
        Dim sPath As String = ""
        Dim sCurNodeDesc As String = ""
        'Dim objInv As New clsInventoryMaster.Inventory
        Dim dtINV As New DataTable
        Try
            lblError.Text = ""
            lblNode.Text = ddlItem.SelectedValue

            LoadCommodity()
            ddlCommodity.SelectedValue = objInv.GetBrandID(sSession.AccessCode, sSession.AccessCodeID, lblNode.Text)

            dt = objInv.GetInventoryMasterDetails(sSession.AccessCode, sSession.AccessCodeID, lblNode.Text)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Inv_Code").ToString()) = False Then
                    lblCode.Text = dt.Rows(0)("Inv_Code").ToString() & " - " & dt.Rows(0)("Inv_Description").ToString()
                End If
                If IsDBNull(dt.Rows(0)("Inv_Description").ToString()) = False Then
                    sCurNodeDesc = objGen.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
                End If
                lParent = dt.Rows(0)("Inv_Parent").ToString()
            End If
            'i = lParent
            'For i = 1 To lParent
            '    objInv = objInv.GetPath(sSession.AccessCode, sSession.AccessCodeID, lParent)
            '    If objInv.Inv_Parent <> 0 Or objInv.Inv_Description <> "" Then
            '        lParent = objInv.Inv_Parent
            '        sNodeDesc = objInv.Inv_Description
            '        sPath = sNodeDesc & "/" & sPath
            '    End If
            'Next
            dtINV = objInv.GetPath(sSession.AccessCode, sSession.AccessCodeID, lParent)
            If dtINV.Rows.Count > 0 Then
                lParent = dtINV.Rows(0)("Inv_Parent")
                sNodeDesc = dtINV.Rows(0)("Inv_Description")
                sPath = sNodeDesc & "/" & sPath
            End If

            sNodeDesc = sCurNodeDesc
            sPath = sPath & sNodeDesc
            lblPath.Text = sPath
            dt = objInv.BindHistory(sSession.AccessCode, sSession.AccessCodeID, lblNode.Text)
            dgHistory.DataSource = dt
            dgHistory.DataBind()
            'loadDetails()

            'ddlUnit.SelectedIndex = -1 : ddlAlternative.SelectedIndex = -1
            'ddlExcise.SelectedIndex = 0 : ddlVAT.SelectedIndex = 0 : ddlCST.SelectedIndex = 0
            'txtPerPieces.Text = "" 
            'txtMRP.Text = ""
            'txtpreDeterminePrice.Text = "" : txtRetail.Text = ""
            'MtxtEffeTo.Text = "" : lblExistMRP.Text = "0" : RtxtEffeTo.Text = "" : PtxtEffeTo.Text = ""

            loadDetails(lblNode.Text, 0)
            'lblExistMRP.Text = lblhistoryID.Text
            lblExistMRP.Text = "0"
            txtMRP.Text = "" : txtpreDeterminePrice.Text = "" : txtRetail.Text = ""
            MtxtEffeFrom.Text = "" : RtxtEffeFrom.Text = "" : PtxtEffeFrom.Text = ""
            MtxtEffeTo.Text = "" : RtxtEffeTo.Text = "" : PtxtEffeTo.Text = ""
            ddlCtgry.SelectedIndex = 0

            imgbtnUpdate.Visible = False
            If sIMDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlItem_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub dgHistory_PreRender(sender As Object, e As EventArgs) Handles dgHistory.PreRender
        Dim dt As New DataTable
        Try
            If dgHistory.Rows.Count > 0 Then
                dgHistory.UseAccessibleHeader = True
                dgHistory.HeaderRow.TableSection = TableRowSection.TableHeader
                dgHistory.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgHistory_PreRender")
        End Try
    End Sub
    Private Sub dgHistory_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgHistory.RowCommand
        Dim dt As New DataTable
        Dim lblsID As New Label

        Dim lblID As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Delete" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)

                If objInv.CheckMasterINUSE(sSession.AccessCode, sSession.AccessCodeID, 1, lblID.Text) = False Then
                    objInv.DeleteInventoryValues(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                Else
                    lblError.Text = "Master Data used it can not be deleted."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
            ElseIf e.CommandName = "Tax" Then   'ElseIf e.CommandName = "MRP" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblTAXHistoryID.Text = lblID.Text

                lblTax.Text = ""
                clearTaxDetails()
                GVDetails.DataSource = objInv.BindTaxDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                GVDetails.DataBind()
                If sIMDSave = "YES" Then
                    btnAddTaxDetails.Visible = True
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)

            ElseIf e.CommandName = "EditRow" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)

                'Extra
                If objInv.CheckMasterINUSE(sSession.AccessCode, sSession.AccessCodeID, 1, lblID.Text) = False Then

                Else
                    lblError.Text = "It is used, it can not be Updated."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
                'Extra

                lblExistMRP.Text = lblID.Text

                loadDetails(lblNode.Text, lblID.Text)
                imgbtnSave.Visible = False
                If sIMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                'If objInv.CheckMasterINUSE(sSession.AccessCode, sSession.AccessCodeID, 1, lblID.Text) = False Then
                '    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                '    loadDetails(lblNode.Text, lblID.Text)
                'Else
                '    lblError.Text = "Master Data used it can not be Edit."
                '    lblCustomerValidationMsg.Text = lblError.Text
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                '    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                '    Exit Sub
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgHistory_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            lblError.Text = ""
            imgbtnUpdate_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub dgHistory_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles dgHistory.RowDeleting

    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            imgbtnUpdate.Visible = False
            If sIMDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            lblExistMRP.Text = "0"
            txtpreDeterminePrice.Text = "" : txtRetail.Text = ""
            MtxtEffeTo.Text = "" : ddlCtgry.SelectedIndex = 0 : ddlCtgry.Enabled = True
            lblError.Text = ""
            'If IsDBNull(objDBL.SQLExecuteScalar(sSession.AccessCode, "Select AS_StartDate from Application_Settings")) = False Then
            '    MtxtEffeFrom.Text = objGen.FormatDtForRDBMS(objDBL.SQLExecuteScalar(sSession.AccessCode, "Select AS_StartDate from Application_Settings"), "D")
            '    MtxtEffeFrom.Enabled = False
            'End If
            MtxtEffeFrom.Text = ""
            txtMRP.Text = "" : txtMRP.Enabled = True
            txtRetail.Enabled = True : txtpreDeterminePrice.Enabled = True : MtxtEffeTo.Enabled = True : MtxtEffeFrom.Enabled = True

            RtxtEffeFrom.Text = "" : RtxtEffeTo.Text = "" : PtxtEffeFrom.Text = "" : PtxtEffeTo.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub clearTaxDetails()
        Try
            ddlVAT.SelectedIndex = 0 : VATtxtEffeFrom.Text = "" : VATtxtEffeTo.Text = ""
            ddlCST.SelectedIndex = 0 : CSTtxtEffeFrom.Text = "" : CSTtxtEffeTo.Text = ""
            ddlExcise.SelectedIndex = 0 : ExcisetxtEffeFrom.Text = "" : ExcisetxtEffeTo.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnAddTaxDetails_Click(sender As Object, e As EventArgs) Handles btnAddTaxDetails.Click
        Dim Arr() As String
        Dim dDate, dSDate As Date
        Dim m As Integer
        Try

            ''If item used we are not allowing to update details'
            'If objInv.CheckMasterINUSE(sSession.AccessCode, sSession.AccessCodeID, 1, lblExistMRP.Text) = False Then

            'Else
            '    lblError.Text = "This item is used already Tax Details can not be Updated."
            '    lblCustomerValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '    clearTaxDetails()
            '    Exit Sub
            'End If
            ''If item used we are not allowing to update details'

            objInv.IMT_ID = 0
            'objInv.IMT_MasterID = lblExistMRP.Text
            objInv.IMT_MasterID = lblTAXHistoryID.Text

            If ddlVAT.SelectedIndex > 0 Then
                objInv.IMT_VAT = ddlVAT.SelectedValue
            Else
                objInv.IMT_VAT = 0
            End If
            If VATtxtEffeFrom.Text <> "" Then
                objInv.IMT_EffectiveVATFrom = DateTime.ParseExact(VATtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveVATFrom = "01/01/1900"
            End If
            If VATtxtEffeTo.Text <> "" Then
                objInv.IMT_EffectiveVATTo = Date.ParseExact(Trim(VATtxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveVATTo = "01/01/1900"
            End If

            If VATtxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(VATtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(VATtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblTax.Text = "Effective To Date (" & VATtxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & VATtxtEffeFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    VATtxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If ddlCST.SelectedIndex > 0 Then
                objInv.IMT_CST = ddlCST.SelectedValue
            Else
                objInv.IMT_CST = 0
            End If
            If CSTtxtEffeFrom.Text <> "" Then
                objInv.IMT_EffectiveCSTFrom = DateTime.ParseExact(CSTtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveCSTFrom = "01/01/1900"
            End If
            If CSTtxtEffeTo.Text <> "" Then
                objInv.IMT_EffectiveCSTTo = Date.ParseExact(Trim(CSTtxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveCSTTo = "01/01/1900"
            End If

            If CSTtxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(CSTtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(CSTtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblTax.Text = "Effective To Date (" & CSTtxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & CSTtxtEffeFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    CSTtxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If


            If ddlExcise.SelectedIndex > 0 Then
                objInv.IMT_Excise = ddlExcise.SelectedValue
            Else
                objInv.IMT_Excise = 0
            End If
            If ExcisetxtEffeFrom.Text <> "" Then
                objInv.IMT_EffectiveExciseFrom = DateTime.ParseExact(ExcisetxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveExciseFrom = "01/01/1900"
            End If
            If ExcisetxtEffeTo.Text <> "" Then
                objInv.IMT_EffectiveExciseTo = Date.ParseExact(Trim(ExcisetxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveExciseTo = "01/01/1900"
            End If

            If ExcisetxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(ExcisetxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(ExcisetxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblTax.Text = "Effective To Date (" & ExcisetxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & ExcisetxtEffeFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    ExcisetxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            objInv.IMT_Status = "W"
            objInv.IMT_CompID = sSession.AccessCodeID
            objInv.IMT_CreatedBy = sSession.UserID
            objInv.IMT_CreatedOn = System.DateTime.Now
            objInv.IMT_Operation = "C"
            objInv.IMT_IPAddress = sSession.IPAddress


            If VATtxtEffeFrom.Text <> "" And CSTtxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(VATtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(CSTtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblTax.Text = "All Effective From Dates should be equal."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    CSTtxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If

            If VATtxtEffeFrom.Text <> "" And ExcisetxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(VATtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(ExcisetxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblTax.Text = "All Effective From Dates should be equal."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    ExcisetxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If

            If CSTtxtEffeFrom.Text <> "" And ExcisetxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(CSTtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(ExcisetxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblTax.Text = "All Effective From Dates should be equal."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    CSTtxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If


            Arr = objInv.SaveTAXDetails(sSession.AccessCode, sSession.AccessCodeID, objInv)
            If Arr(0) = "2" Then
                lblTax.Text = "Successfully Updated"
            ElseIf Arr(0) = "3" Then
                lblTax.Text = "Successfully Saved"
            End If
            clearTaxDetails()
            GVDetails.DataSource = objInv.BindTaxDetails(sSession.AccessCode, sSession.AccessCodeID, lblTAXHistoryID.Text)
            GVDetails.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddTaxDetails_Click")
        End Try
    End Sub

    Private Sub GVDetails_PreRender(sender As Object, e As EventArgs) Handles GVDetails.PreRender
        Dim dt As New DataTable
        Try
            If GVDetails.Rows.Count > 0 Then
                GVDetails.UseAccessibleHeader = True
                GVDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                GVDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVDetails_PreRender")
        End Try
    End Sub
    Private Sub GVDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GVDetails.RowCommand
        Dim dt As New DataTable
        Dim lblsID As New Label
        Dim lblTAXID As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "VAT" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblTAXID = DirectCast(clickedRow.FindControl("lblID"), Label)
                btnAddTaxDetails.Visible = False
                If sIMDSave = "YES" Then
                    btnUpdateTaxDetails.Visible = True
                End If
                loadTAXDetails(lblTAXID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)

                If sIMDSave = "YES" Then
                    btnUpdateTaxDetails.Visible = True
                End If
                lblTaxationID.Text = lblTAXID.Text
            Else
                lblTaxationID.Text = "0"
                btnUpdateTaxDetails.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVDetails_RowCommand")
        End Try
    End Sub
    Private Sub loadTAXDetails(ByVal ITD As Integer)
        'Dim dt As New DataTable
        'Try
        '    dt = objInv.BindTaxDetails(sSession.AccessCode, sSession.AccessCodeID, iItemID, iINVHID)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("INVHID")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATEffeFromDate")
            dtTab.Columns.Add("VATEffeToDate")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTEffeFromDate")
            dtTab.Columns.Add("CSTEffeToDate")
            dtTab.Columns.Add("Excise")
            dtTab.Columns.Add("ExciseEffeFromDate")
            dtTab.Columns.Add("ExciseEffeToDate")
            sSql = "Select * from Inventory_Master_TaxDetails Where IMT_ID=" & ITD & " And IMT_CompID=" & sSession.AccessCodeID & ""
            dt = objDBL.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("INVHID") = dt.Rows(i)("IMT_MasterID")
                dr("ID") = dt.Rows(i)("IMT_ID")
                If (dt.Rows(i)("IMT_VAT").ToString() = "") Then
                    ddlVAT.SelectedIndex = 0
                Else
                    ddlVAT.SelectedValue = dt.Rows(i)("IMT_VAT")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATFrom"), "D").ToString() = "01-01-1900") Then
                    VATtxtEffeFrom.Text = ""
                Else
                    VATtxtEffeFrom.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATFrom"), "D")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATTo"), "D").ToString() = "01-01-1900") Then
                    VATtxtEffeTo.Text = ""
                Else
                    VATtxtEffeTo.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATTo"), "D")
                End If

                If (dt.Rows(i)("IMT_CST").ToString() = "") Then
                    ddlCST.SelectedIndex = 0
                Else
                    ddlCST.SelectedValue = dt.Rows(i)("IMT_CST")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTFrom"), "D").ToString() = "01-01-1900") Then
                    CSTtxtEffeFrom.Text = ""
                Else
                    CSTtxtEffeFrom.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTFrom"), "D")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTTo"), "D").ToString() = "01-01-1900") Then
                    CSTtxtEffeTo.Text = ""
                Else
                    CSTtxtEffeTo.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTTo"), "D")
                End If

                If (dt.Rows(i)("IMT_Excise").ToString() = "") Then
                    ddlExcise.SelectedIndex = 0
                Else
                    ddlExcise.SelectedValue = dt.Rows(i)("IMT_Excise")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseFrom"), "D").ToString() = "01-01-1900") Then
                    ExcisetxtEffeFrom.Text = ""
                Else
                    ExcisetxtEffeFrom.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseFrom"), "D")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseTo"), "D").ToString() = "01-01-1900") Then
                    ExcisetxtEffeTo.Text = ""
                Else
                    ExcisetxtEffeTo.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseTo"), "D")
                End If
                '  dtTab.Rows.Add(dr)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnUpdateTaxDetails_Click(sender As Object, e As EventArgs) Handles btnUpdateTaxDetails.Click
        Dim Arr() As String
        Dim dDate, dSDate As Date
        Dim m As Integer
        Try

            If lblTaxationID.Text = "" Then
                lblTax.Text = "Select Tax Details to update."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                Exit Sub
            End If
            If lblTaxationID.Text <> "" Then
                If lblTaxationID.Text = 0 Then
                    lblTax.Text = "Select Tax Details to update."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    Exit Sub
                End If
            End If

            objInv.IMT_ID = lblTaxationID.Text
            objInv.IMT_MasterID = lblTAXHistoryID.Text

            If ddlVAT.SelectedIndex > 0 Then
                objInv.IMT_VAT = ddlVAT.SelectedValue
            Else
                objInv.IMT_VAT = 0
            End If
            If VATtxtEffeFrom.Text <> "" Then
                objInv.IMT_EffectiveVATFrom = DateTime.ParseExact(VATtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveVATFrom = "01/01/1900"
            End If
            If VATtxtEffeTo.Text <> "" Then
                objInv.IMT_EffectiveVATTo = Date.ParseExact(Trim(VATtxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveVATTo = "01/01/1900"
            End If

            If VATtxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(VATtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(VATtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblTax.Text = "Effective To Date (" & VATtxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & VATtxtEffeFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    VATtxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If ddlCST.SelectedIndex > 0 Then
                objInv.IMT_CST = ddlCST.SelectedValue
            Else
                objInv.IMT_CST = 0
            End If
            If CSTtxtEffeFrom.Text <> "" Then
                objInv.IMT_EffectiveCSTFrom = DateTime.ParseExact(CSTtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveCSTFrom = "01/01/1900"
            End If
            If CSTtxtEffeTo.Text <> "" Then
                objInv.IMT_EffectiveCSTTo = Date.ParseExact(Trim(CSTtxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveCSTTo = "01/01/1900"
            End If
            If CSTtxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(CSTtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(CSTtxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblTax.Text = "Effective To Date (" & CSTtxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & CSTtxtEffeFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    CSTtxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If
            If ddlExcise.SelectedIndex > 0 Then
                objInv.IMT_Excise = ddlExcise.SelectedValue
            Else
                objInv.IMT_Excise = 0
            End If
            If ExcisetxtEffeFrom.Text <> "" Then
                objInv.IMT_EffectiveExciseFrom = DateTime.ParseExact(ExcisetxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveExciseFrom = "01/01/1900"
            End If
            If ExcisetxtEffeTo.Text <> "" Then
                objInv.IMT_EffectiveExciseTo = Date.ParseExact(Trim(ExcisetxtEffeTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objInv.IMT_EffectiveExciseTo = "01/01/1900"
            End If
            If ExcisetxtEffeTo.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(ExcisetxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(ExcisetxtEffeTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblTax.Text = "Effective To Date (" & ExcisetxtEffeTo.Text & ") should be Greater than or equal to Effective From Date(" & ExcisetxtEffeFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    ExcisetxtEffeTo.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If
            objInv.IMT_Status = "W"
            objInv.IMT_CompID = sSession.AccessCodeID
            objInv.IMT_CreatedBy = sSession.UserID
            objInv.IMT_CreatedOn = System.DateTime.Now
            objInv.IMT_Operation = "C"
            objInv.IMT_IPAddress = sSession.IPAddress

            If VATtxtEffeFrom.Text <> "" And CSTtxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(VATtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(CSTtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblTax.Text = "All Effective From Dates should be equal."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    CSTtxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If
            If VATtxtEffeFrom.Text <> "" And ExcisetxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(VATtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(ExcisetxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblTax.Text = "All Effective From Dates should be equal."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    ExcisetxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If
            If CSTtxtEffeFrom.Text <> "" And ExcisetxtEffeFrom.Text <> "" Then
                dDate = Date.ParseExact(CSTtxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(ExcisetxtEffeFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Or m > 0 Then
                    lblTax.Text = "All Effective From Dates should be equal."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    CSTtxtEffeFrom.Focus()
                    Exit Sub
                End If
            End If
            Arr = objInv.SaveTAXDetails(sSession.AccessCode, sSession.AccessCodeID, objInv)
            If Arr(0) = "2" Then
                lblTax.Text = "Successfully Updated"
                If sIMDSave = "YES" Then
                    btnAddTaxDetails.Visible = True
                End If

            ElseIf Arr(0) = "3" Then
                lblTax.Text = "Successfully Saved"
            End If
            clearTaxDetails()
            GVDetails.DataSource = objInv.BindTaxDetails(sSession.AccessCode, sSession.AccessCodeID, lblTAXHistoryID.Text)
            GVDetails.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateTaxDetails_Click")
        End Try
    End Sub
End Class
