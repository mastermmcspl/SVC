Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Text.RegularExpressions
Imports System.Web.Services
Imports System.Net
Imports System.Web.Script.Services
Imports Microsoft.Reporting.WebForms
Imports System.Web.Script.Serialization
Partial Class Masters_CurrencyMasterDetails
    Inherits System.Web.UI.Page
    Private Shared sSession As AllSession
    Private objSettings As New clsApplicationSettings
    Private Shared dt As DataTable
    Private Shared objclsCurrencyMaster As New clsCurrencyMaster
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private sFormName As String = "CurrencyMasterDetails"
    Shared dtAgency As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iCurr As Integer = 0, iToCurr As Integer = 0, iBankID As Integer = 0, iCheck As Integer = 0
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'iCheck = objclsCurrencyMaster.CheckBankWeekOff(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'If iCheck > 0 Then
                '    imgbtnAdd.Visible = False : imgbtnRefresh.Visible = False : imgbtnReport.Visible = False
                '    lblValidationMsg.Text = "Exchange rates are not available on Weekends(Saturday & Sunday) & Bank Holidays."
                '    lblError.Text = "Exchange rates are not available on Weekends(Saturday & Sunday) & Bank Holidays."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                '    Exit Sub
                'Else
                imgbtnAdd.Visible = True : imgbtnRefresh.Visible = True : imgbtnReport.Visible = True
                'End If
                BindBank()
                RFVTTbuy.ErrorMessage = "Enter TT Buy Rate."
                REVTTbuy.ErrorMessage = "Enter valid TT Buy Rate." : REVTTbuy.ValidationExpression = "((\d+)((\.\d{1,5})?))$"
                RFVTTSell.ErrorMessage = "Enter TT Buy Sell."
                REVTTSell.ErrorMessage = "Enter valid TT Sell Rate." : REVTTSell.ValidationExpression = "((\d+)((\.\d{1,5})?))$"
                RFVBuy.ErrorMessage = "Enter Buy Rate."
                REVBuy.ErrorMessage = "Enter valid Buy Rate." : REVBuy.ValidationExpression = "((\d+)((\.\d{1,5})?))$"
                RFVSell.ErrorMessage = "Enter Buy Sell."
                REVSell.ErrorMessage = "Enter valid Sell Rate." : REVSell.ValidationExpression = "((\d+)((\.\d{1,5})?))$"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindBank()
        Dim dt As New DataTable
        Try
            dt = objclsGeneralFunctions.LoadBanksName(sSession.AccessCode, sSession.AccessCodeID)
            ddlBankName.DataSource = dt
            ddlBankName.DataTextField = "Description"
            ddlBankName.DataValueField = "gl_id"
            ddlBankName.DataBind()
            ddlBankName.Items.Insert(0, "Select Bank")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCurrencyTypeDB()
        Dim dt As New DataTable
        Try
            dt = objSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlToCurrency.DataSource = dt
            ddlToCurrency.DataTextField = "CUR_CODE"
            ddlToCurrency.DataValueField = "CUR_ID"
            ddlToCurrency.DataBind()
            ddlToCurrency.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCurrencyType()
        Dim dt As New DataTable
        Try
            dt = objSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlfromcurrency.DataSource = dt
            ddlfromcurrency.DataTextField = "CUR_CODE"
            ddlfromcurrency.DataValueField = "CUR_ID"
            ddlfromcurrency.DataBind()
            ddlfromcurrency.Items.Insert(0, "Select Currency")
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
                        ddlfromcurrency.SelectedValue = dt.Rows(i)("sad_Config_Value")
                    End If
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankName.SelectedIndexChanged
        Try
            LoadCurrencyTypeDB()
            GetAppSettings()
            LoadCurrencyType()
            txtTTbuy.Text = 0 : txtTTsell.Text = 0 : txtBuy.Text = 0 : txtSell.Text = 0
            txtWTTbuy.Text = 0 : txtWTTsell.Text = 0 : txtWBuy.Text = 0 : txtWSell.Text = 0
            rboBank.Checked = False : rboWeb.Checked = False : rboAgency.Checked = False : imgbtnAdd.Visible = True
            gvAgencycurrencyMaster.DataSource = Nothing
            gvAgencycurrencyMaster.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBankName_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlfromcurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlfromcurrency.SelectedIndexChanged
        Try
            txtTTbuy.Text = 0 : txtTTsell.Text = 0 : txtBuy.Text = 0 : txtSell.Text = 0
            txtWTTbuy.Text = 0 : txtWTTsell.Text = 0 : txtWBuy.Text = 0 : txtWSell.Text = 0
            rboBank.Checked = False : rboWeb.Checked = False
            If ddlBankName.SelectedIndex > 0 And ddlfromcurrency.SelectedIndex > 0 And ddlToCurrency.SelectedIndex > 0 Then
                ddlToCurrency_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBankName_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlToCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlToCurrency.SelectedIndexChanged
        Dim sFrom As String = "", sTo As String = ""
        Dim iOperateOn As Integer = 0, iCurr As Integer = 0, iBankID As Integer
        Dim Web As New WebClient()
        Dim amount As Integer = 1
        Dim fromCurrency As String = "", ToCurrency As String = ""
        Dim dtTable As DataTable
        Dim chkSelect As New CheckBox
        Dim lblID As New Label
        Try
            lblError.Text = ""
            If ddlBankName.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Bank." : lblError.Text = "Select Bank."
                ddlBankName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlfromcurrency.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Base Currency." : lblError.Text = "Select Base Currency."
                ddlfromcurrency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlBankName.SelectedIndex > 0 Then
                iBankID = ddlBankName.SelectedValue
            End If
            If ddlfromcurrency.SelectedIndex > 0 Then
                iCurr = ddlfromcurrency.SelectedValue
            End If

            If ddlToCurrency.SelectedIndex > 0 Then
                iOperateOn = ddlToCurrency.SelectedValue
                '*********************************** Web ***********************************
                fromCurrency = ddlfromcurrency.SelectedItem.Text.Remove(3)
                ToCurrency = ddlToCurrency.SelectedItem.Text.Remove(3)
                Try
                    Dim url As String = String.Format("http://www.google.co.in/ig/calculator?h1=en&q={2}{0}%3D%3F{1}", fromCurrency, ToCurrency, txtExchangeRate.Text)
                    'Dim url As String = String.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency, ToCurrency)
                    Dim response As String = Web.DownloadString(url)
                    Dim values As String() = Regex.Split(response, ",")
                    Dim Rate As Decimal = System.Convert.ToDecimal(values(1))
                    txtWTTbuy.Text = Rate * amount
                    txtWTTsell.Text = Rate * amount
                    txtWBuy.Text = Rate * amount
                    txtWSell.Text = Rate * amount
                    txtWTTbuy.Enabled = False
                    txtWTTsell.Enabled = False
                    txtWBuy.Enabled = False
                    txtWSell.Enabled = False
                Catch ex As Exception
                    'lblValidationMsg.Text = "This site has been Closed.So refer & Enter from OANDA Currency Converter."
                    lblError.Text = "This site has been Closed.So refer & Enter from OANDA Currency Converter."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    txtWTTbuy.Enabled = True
                    txtWTTsell.Enabled = True
                    txtWBuy.Enabled = True
                    txtWSell.Enabled = True

                    txtWTTbuy.Text = 0
                    txtWTTsell.Text = 0
                    txtWBuy.Text = 0
                    txtWSell.Text = 0
                End Try
            Else
                txtTTbuy.Text = 0 : txtTTsell.Text = 0 : txtBuy.Text = 0 : txtSell.Text = 0 : rboBank.Checked = False : rboWeb.Checked = False
            End If
            If iBankID > 0 Then
                dt = objclsCurrencyMaster.LoadBankCurrencyDetails(sSession.AccessCode, sSession.AccessCodeID, iBankID, iCurr, iOperateOn)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("BCM_DelFlag")) = False Then
                        If dt.Rows(0).Item("BCM_DelFlag") = "A" Then
                            imgbtnAdd.Visible = True : imgbtnReport.Visible = True
                        End If
                        If dt.Rows(0).Item("BCM_DelFlag") = "W" Then
                            imgbtnAdd.Visible = False : imgbtnReport.Visible = False
                            lblValidationMsg.Text = "The Data is Waiting for Approval." : lblError.Text = "The Data is Waiting for Approval."
                            ddlBankName.Focus()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                            Exit Sub
                        End If
                        If dt.Rows(0).Item("BCM_DelFlag") = "D" Then
                            imgbtnAdd.Visible = False : imgbtnReport.Visible = False
                            lblValidationMsg.Text = "The Data has been De-Activated." : lblError.Text = "The Data has been De-Activated."
                            ddlBankName.Focus()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If
                    If IsDBNull(dt.Rows(0).Item("BCM_BankID")) = False Then
                        ddlBankName.SelectedValue = dt.Rows(0).Item("BCM_BankID")
                    End If

                    txtTTbuy.Text = 0
                    If IsDBNull(dt.Rows(0).Item("BCM_TTBuy")) = False Then
                        txtTTbuy.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0).Item("BCM_TTBuy").ToString())
                    End If

                    txtTTsell.Text = 0
                    If IsDBNull(dt.Rows(0).Item("BCM_TTSell")) = False Then
                        txtTTsell.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0).Item("BCM_TTSell").ToString())
                    End If

                    txtBuy.Text = 0
                    If IsDBNull(dt.Rows(0).Item("BCM_Buy")) = False Then
                        txtBuy.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0).Item("BCM_Buy").ToString())
                    End If

                    txtSell.Text = 0
                    If IsDBNull(dt.Rows(0).Item("BCM_Sell")) = False Then
                        txtSell.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0).Item("BCM_Sell").ToString())
                    End If
                Else
                    lblValidationMsg.Text = "Ensure that card rates are available for the day."
                    lblError.Text = "Ensure that card rates are available for the day."
                    txtTTbuy.Text = 0 : txtTTsell.Text = 0 : txtBuy.Text = 0 : txtSell.Text = 0
                    imgbtnAdd.Visible = False
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            dtAgency = objclsCurrencyMaster.LoadAgencyDetails(sSession.AccessCode, sSession.AccessCodeID, iCurr, iOperateOn)
            gvAgencycurrencyMaster.DataSource = dtAgency
            gvAgencycurrencyMaster.DataBind()
            dtTable = objclsCurrencyMaster.LoadCurrencyDetails(sSession.AccessCode, sSession.AccessCodeID, iCurr, iOperateOn)
            If dtTable.Rows.Count > 0 Then
                lblError.Text = ""
                'lblValidationMsg.Text = ""
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalValidation').modal('hide');", True)
                If IsDBNull(dtTable.Rows(0).Item("CM_Type")) = False Then
                    If dtTable.Rows(0).Item("CM_Type") = "0" Then
                        rboBank.Checked = True
                        If IsDBNull(dtTable.Rows(0).Item("CM_BankID")) = False Then
                            If dtTable.Rows(0).Item("CM_BankID") = "0" Then
                                ddlBankName.SelectedIndex = 0
                            Else
                                ddlBankName.SelectedValue = dtTable.Rows(0).Item("CM_BankID")
                            End If
                        End If
                        txtTTbuy.Text = 0
                        If IsDBNull(dtTable.Rows(0).Item("CM_TTBuy")) = False Then
                            txtTTbuy.Text = objclsFASGeneral.ReplaceSafeSQL(dtTable.Rows(0).Item("CM_TTBuy").ToString())
                        End If

                        txtTTsell.Text = 0
                        If IsDBNull(dtTable.Rows(0).Item("CM_TTSell")) = False Then
                            txtTTsell.Text = objclsFASGeneral.ReplaceSafeSQL(dtTable.Rows(0).Item("CM_TTSell").ToString())
                        End If

                        txtBuy.Text = 0
                        If IsDBNull(dtTable.Rows(0).Item("CM_Buy")) = False Then
                            txtBuy.Text = objclsFASGeneral.ReplaceSafeSQL(dtTable.Rows(0).Item("CM_Buy").ToString())
                        End If

                        txtSell.Text = 0
                        If IsDBNull(dtTable.Rows(0).Item("CM_Sell")) = False Then
                            txtSell.Text = objclsFASGeneral.ReplaceSafeSQL(dtTable.Rows(0).Item("CM_Sell").ToString())
                        End If
                    ElseIf dtTable.Rows(0).Item("CM_Type") = "1" Then
                        rboWeb.Checked = True
                        txtWTTbuy.Text = 0
                        If IsDBNull(dtTable.Rows(0).Item("CM_TTBuy")) = False Then
                            txtWTTbuy.Text = objclsFASGeneral.ReplaceSafeSQL(dtTable.Rows(0).Item("CM_TTBuy").ToString())
                        End If

                        txtWTTsell.Text = 0
                        If IsDBNull(dtTable.Rows(0).Item("CM_TTSell")) = False Then
                            txtWTTsell.Text = objclsFASGeneral.ReplaceSafeSQL(dtTable.Rows(0).Item("CM_TTSell").ToString())
                        End If

                        txtWBuy.Text = 0
                        If IsDBNull(dtTable.Rows(0).Item("CM_Buy")) = False Then
                            txtWBuy.Text = objclsFASGeneral.ReplaceSafeSQL(dtTable.Rows(0).Item("CM_Buy").ToString())
                        End If

                        txtWSell.Text = 0
                        If IsDBNull(dtTable.Rows(0).Item("CM_Sell")) = False Then
                            txtWSell.Text = objclsFASGeneral.ReplaceSafeSQL(dtTable.Rows(0).Item("CM_Sell").ToString())
                        End If
                    ElseIf dtTable.Rows(0).Item("CM_Type") = "2" Then
                        rboAgency.Checked = True
                        If dtAgency.Rows.Count > 0 Then
                            If IsDBNull(dtTable.Rows(0).Item("CM_BankID")) = False Then
                                If dtTable.Rows(0).Item("CM_BankID") = "0" Then
                                Else
                                    For i = 0 To gvAgencycurrencyMaster.Rows.Count - 1
                                        chkSelect = gvAgencycurrencyMaster.Rows(i).FindControl("chkSelect")
                                        lblID = gvAgencycurrencyMaster.Rows(i).FindControl("lblFEID")
                                        If lblID.Text = dtTable.Rows(0).Item("CM_BankID") Then
                                            chkSelect.Checked = True
                                        Else
                                            chkSelect.Checked = False
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If
                If IsDBNull(dtTable.Rows(0).Item("CM_DelFlag")) = False Then
                    imgbtnAdd.Visible = False : imgbtnReport.Visible = True
                End If
            Else
                imgbtnAdd.Visible = True : imgbtnReport.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlToCurrency_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim objstrCurrencyMaster As New strCurrencyMaster
        Dim arr As Array
        Dim iType As Integer = 0, iCount As Integer = 0
        Dim sFrom As String = "", sTo As String = ""
        Dim iOperateOn As Integer = 0, iCurr As Integer = 0, iBankID As Integer
        Dim chkSelect As New CheckBox
        Dim lblID As New Label, lblFEID As New Label, lblTTBuy As New Label, lblTTSell As New Label, lblTBuy As New Label, lblTSell As New Label
        Try
        lblError.Text = ""
            If rboBank.Checked = False And rboWeb.Checked = False And rboAgency.Checked = False Then
                lblValidationMsg.Text = "Select either Bank or Web or Agency." : lblError.Text = "Select either Bank or Web or Agency."
                rboBank.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlBankName.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Bank." : lblError.Text = "Select Bank."
                ddlBankName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlfromcurrency.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Base Currency." : lblError.Text = "Select Base Currency."
                ddlfromcurrency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlToCurrency.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Currency." : lblError.Text = "Select Currency."
                ddlToCurrency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If rboAgency.Checked = True Then
                If gvAgencycurrencyMaster.Rows.Count = 0 Then
                    lblError.Text = "No data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data.','', 'info');", True)
                    Exit Sub
                End If
                For i = 0 To gvAgencycurrencyMaster.Rows.Count - 1
                    chkSelect = gvAgencycurrencyMaster.Rows(i).FindControl("chkSelect")
                    If chkSelect.Checked = True Then
                        iCount = 1
                        GoTo NextSave
                    End If
                Next
                If iCount = 0 Then
                    lblError.Text = "Select Agecny."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Agecny.','', 'info');", True)
                    Exit Sub
                End If
            End If
NextSave:   If ddlBankName.SelectedIndex > 0 Then
                iBankID = ddlBankName.SelectedValue
            End If
            If ddlfromcurrency.SelectedIndex > 0 Then
                iCurr = ddlfromcurrency.SelectedValue
            End If

            If ddlToCurrency.SelectedIndex > 0 Then
                iOperateOn = ddlToCurrency.SelectedValue
            Else
                txtTTbuy.Text = 0 : txtTTsell.Text = 0 : txtBuy.Text = 0 : txtSell.Text = 0
            End If
            If ddlToCurrency.SelectedIndex > 0 Then
                objstrCurrencyMaster.iCM_PKID = 0
                objstrCurrencyMaster.sCM_Currency = ddlfromcurrency.SelectedValue
                objstrCurrencyMaster.sCM_OperateOn = ddlToCurrency.SelectedValue
                objstrCurrencyMaster.sCM_Date = DateTime.Now.ToString("dd/MM/yyyy")
                objstrCurrencyMaster.sCM_IPAddress = sSession.IPAddress
                objstrCurrencyMaster.sCM_CreatedBy = sSession.UserID
                objstrCurrencyMaster.sCM_Time = objclsCurrencyMaster.GetCurrentTime(sSession.AccessCode)
                If rboBank.Checked = True Then
                    objstrCurrencyMaster.iCM_Type = 0
                    objstrCurrencyMaster.dCM_TTBUY = objclsFASGeneral.SafeSQL(txtTTbuy.Text.Trim)
                    objstrCurrencyMaster.dCM_TTSell = objclsFASGeneral.SafeSQL(txtTTsell.Text.Trim)
                    objstrCurrencyMaster.dCM_BUY = objclsFASGeneral.SafeSQL(txtBuy.Text.Trim)
                    objstrCurrencyMaster.dCM_Sell = objclsFASGeneral.SafeSQL(txtSell.Text.Trim)
                    objstrCurrencyMaster.iCM_BankID = iBankID
                    For i = 0 To gvAgencycurrencyMaster.Rows.Count - 1
                        Dim chkBx As CheckBox = CType(gvAgencycurrencyMaster.Rows(i).FindControl("chkSelect"), CheckBox)
                        chkBx.Checked = False
                    Next
                ElseIf rboWeb.Checked = True Then
                    objstrCurrencyMaster.iCM_Type = 1
                    objstrCurrencyMaster.dCM_TTBUY = objclsFASGeneral.SafeSQL(txtWTTbuy.Text.Trim)
                    objstrCurrencyMaster.dCM_TTSell = objclsFASGeneral.SafeSQL(txtWTTsell.Text.Trim)
                    objstrCurrencyMaster.dCM_BUY = objclsFASGeneral.SafeSQL(txtWBuy.Text.Trim)
                    objstrCurrencyMaster.dCM_Sell = objclsFASGeneral.SafeSQL(txtWSell.Text.Trim)
                    objstrCurrencyMaster.iCM_BankID = iBankID
                    For i = 0 To gvAgencycurrencyMaster.Rows.Count - 1
                        Dim chkBx As CheckBox = CType(gvAgencycurrencyMaster.Rows(i).FindControl("chkSelect"), CheckBox)
                        chkBx.Checked = False
                    Next
                ElseIf rboAgency.Checked = True Then
                    objstrCurrencyMaster.iCM_Type = 2
                    For i = 0 To gvAgencycurrencyMaster.Rows.Count - 1
                        chkSelect = gvAgencycurrencyMaster.Rows(i).FindControl("chkSelect")
                        lblID = gvAgencycurrencyMaster.Rows(i).FindControl("lblID")
                        lblFEID = gvAgencycurrencyMaster.Rows(i).FindControl("lblFEID")
                        lblTTBuy = gvAgencycurrencyMaster.Rows(i).FindControl("lblTTBuy")
                        lblTTSell = gvAgencycurrencyMaster.Rows(i).FindControl("lblTTSell")
                        lblTBuy = gvAgencycurrencyMaster.Rows(i).FindControl("lblTBuy")
                        lblTSell = gvAgencycurrencyMaster.Rows(i).FindControl("lblTSell")
                        If chkSelect.Checked = True Then
                            objstrCurrencyMaster.dCM_TTBUY = objclsFASGeneral.SafeSQL(lblTTBuy.Text.Trim)
                            objstrCurrencyMaster.dCM_TTSell = objclsFASGeneral.SafeSQL(lblTTSell.Text.Trim)
                            objstrCurrencyMaster.dCM_BUY = objclsFASGeneral.SafeSQL(lblTBuy.Text.Trim)
                            objstrCurrencyMaster.dCM_Sell = objclsFASGeneral.SafeSQL(lblTSell.Text.Trim)
                            objstrCurrencyMaster.iCM_BankID = lblFEID.Text
                        End If
                    Next
                End If
                objstrCurrencyMaster.iCM_CompID = sSession.AccessCodeID
                arr = objclsCurrencyMaster.SaveCurrencyMaster(sSession.AccessCode, objstrCurrencyMaster)
                If arr(0) = 3 Then
                    lblValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Currency Master Details", "Saved", arr(1), "", 0, "", sSession.IPAddress)
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            lblError.Text = "" : ddlBankName.SelectedIndex = 0
            ddlBankName_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/Masters/CurrencyMasterDashboard.aspx"), False) 'Masters/EmployeeMaster
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub gvAgencycurrencyMaster_PreRender(sender As Object, e As EventArgs) Handles gvAgencycurrencyMaster.PreRender
        Try
            If gvAgencycurrencyMaster.Rows.Count > 0 Then
                gvAgencycurrencyMaster.UseAccessibleHeader = True
                gvAgencycurrencyMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAgencycurrencyMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAgencycurrencyMaster_PreRender")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            If ddlBankName.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Bank." : lblError.Text = "Select Bank."
                ddlBankName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlfromcurrency.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Base Currency." : lblError.Text = "Select Base Currency."
                ddlfromcurrency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlToCurrency.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Currency." : lblError.Text = "Select Currency."
                ddlToCurrency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dtAgency)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/CurrencyMatserDetails.rdlc")

            If ddlBankName.SelectedIndex > 0 Then
                Dim BankName As ReportParameter() = New ReportParameter() {New ReportParameter("BankName", ddlBankName.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(BankName)
            Else
                Dim BankName As ReportParameter() = New ReportParameter() {New ReportParameter("BankName", " ")}
                ReportViewer1.LocalReport.SetParameters(BankName)
            End If

            Dim BaseCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("BaseCurrency", ddlfromcurrency.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(BaseCurrency)

            Dim Currency As ReportParameter() = New ReportParameter() {New ReportParameter("Currency", ddlToCurrency.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Currency)

            Dim UnitCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("UnitCurrency", txtExchangeRate.Text)}
            ReportViewer1.LocalReport.SetParameters(UnitCurrency)

            Dim TTBuy As ReportParameter() = New ReportParameter() {New ReportParameter("TTBuy", txtTTbuy.Text)}
            ReportViewer1.LocalReport.SetParameters(TTBuy)

            Dim TTSell As ReportParameter() = New ReportParameter() {New ReportParameter("TTSell", txtTTsell.Text)}
            ReportViewer1.LocalReport.SetParameters(TTSell)

            Dim TCBuy As ReportParameter() = New ReportParameter() {New ReportParameter("TCBuy", txtBuy.Text)}
            ReportViewer1.LocalReport.SetParameters(TCBuy)

            Dim TCSell As ReportParameter() = New ReportParameter() {New ReportParameter("TCSell", txtSell.Text)}
            ReportViewer1.LocalReport.SetParameters(TCSell)

            Dim WTTBuy As ReportParameter() = New ReportParameter() {New ReportParameter("WTTBuy", txtWTTbuy.Text)}
            ReportViewer1.LocalReport.SetParameters(WTTBuy)

            Dim WTTSell As ReportParameter() = New ReportParameter() {New ReportParameter("WTTSell", txtWTTsell.Text)}
            ReportViewer1.LocalReport.SetParameters(WTTSell)

            Dim WTCBuy As ReportParameter() = New ReportParameter() {New ReportParameter("WTCBuy", txtWBuy.Text)}
            ReportViewer1.LocalReport.SetParameters(WTCBuy)

            Dim WTCSell As ReportParameter() = New ReportParameter() {New ReportParameter("WTCSell", txtWSell.Text)}
            ReportViewer1.LocalReport.SetParameters(WTCSell)

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Currency Master Details", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CurrencyMatserDetails" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            If ddlBankName.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Bank." : lblError.Text = "Select Bank."
                ddlBankName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlfromcurrency.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Base Currency." : lblError.Text = "Select Base Currency."
                ddlfromcurrency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlToCurrency.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Currency." : lblError.Text = "Select Currency."
                ddlToCurrency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dtAgency)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/CurrencyMatserDetails.rdlc")

            If ddlBankName.SelectedIndex > 0 Then
                Dim BankName As ReportParameter() = New ReportParameter() {New ReportParameter("BankName", ddlBankName.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(BankName)
            Else
                Dim BankName As ReportParameter() = New ReportParameter() {New ReportParameter("BankName", " ")}
                ReportViewer1.LocalReport.SetParameters(BankName)
            End If

            Dim BaseCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("BaseCurrency", ddlfromcurrency.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(BaseCurrency)

            Dim Currency As ReportParameter() = New ReportParameter() {New ReportParameter("Currency", ddlToCurrency.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Currency)

            Dim UnitCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("UnitCurrency", txtExchangeRate.Text)}
            ReportViewer1.LocalReport.SetParameters(UnitCurrency)

            Dim TTBuy As ReportParameter() = New ReportParameter() {New ReportParameter("TTBuy", txtTTbuy.Text)}
            ReportViewer1.LocalReport.SetParameters(TTBuy)

            Dim TTSell As ReportParameter() = New ReportParameter() {New ReportParameter("TTSell", txtTTsell.Text)}
            ReportViewer1.LocalReport.SetParameters(TTSell)

            Dim TCBuy As ReportParameter() = New ReportParameter() {New ReportParameter("TCBuy", txtBuy.Text)}
            ReportViewer1.LocalReport.SetParameters(TCBuy)

            Dim TCSell As ReportParameter() = New ReportParameter() {New ReportParameter("TCSell", txtSell.Text)}
            ReportViewer1.LocalReport.SetParameters(TCSell)

            Dim WTTBuy As ReportParameter() = New ReportParameter() {New ReportParameter("WTTBuy", txtWTTbuy.Text)}
            ReportViewer1.LocalReport.SetParameters(WTTBuy)

            Dim WTTSell As ReportParameter() = New ReportParameter() {New ReportParameter("WTTSell", txtWTTsell.Text)}
            ReportViewer1.LocalReport.SetParameters(WTTSell)

            Dim WTCBuy As ReportParameter() = New ReportParameter() {New ReportParameter("WTCBuy", txtWBuy.Text)}
            ReportViewer1.LocalReport.SetParameters(WTCBuy)

            Dim WTCSell As ReportParameter() = New ReportParameter() {New ReportParameter("WTCSell", txtWSell.Text)}
            ReportViewer1.LocalReport.SetParameters(WTCSell)

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Currency Master Details", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CurrencyMatserDetails" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Protected Sub ChkSelect_OnCheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Try
            lblError.Text = ""
            Dim activeCheckBox As CheckBox = CType(sender, CheckBox)
            For Each rw As GridViewRow In gvAgencycurrencyMaster.Rows
                Dim chkBx As CheckBox = CType(rw.FindControl("chkSelect"), CheckBox)
                If chkBx IsNot activeCheckBox Then
                    chkBx.Checked = False
                Else
                    chkBx.Checked = True
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
End Class
