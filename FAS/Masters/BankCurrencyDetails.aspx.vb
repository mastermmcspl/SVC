Imports System
Imports System.Data
Imports BusinesLayer
Partial Class Masters_BankCurrencyDetails
    Inherits System.Web.UI.Page
    Private Shared sSession As AllSession
    Private objSettings As New clsApplicationSettings
    Private Shared dt As DataTable
    Private Shared objclsBankCurrency As New clsBankCurrency
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private sFormName As String = "BankCurrencyMasterDetails"
    Private Shared iPkID As Integer = 0
    Private Shared iBankID As Integer = 0
    Private Shared iStatus As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iCurr As Integer = 0
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadCurrencyTypeDB()
                LoadCurrencyType()
                GetAppSettings()
                If Request.QueryString("BankID") IsNot Nothing Then
                    iBankID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("BankID")))
                    If iBankID > 0 Then
                        lblBankName.Text = objclsBankCurrency.GetBankName(sSession.AccessCode, sSession.AccessCodeID, iBankID)
                    End If
                End If
                If Request.QueryString("ToCurr") IsNot Nothing Then
                    iCurr = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ToCurr")))
                    Dim liCurr As ListItem = ddlToCurrency.Items.FindByValue(iCurr)
                    If IsNothing(liCurr) = False Then
                        ddlToCurrency.SelectedValue = iCurr
                    End If
                End If
                If Request.QueryString("ID") IsNot Nothing Then
                    iStatus = 2
                    iPkID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ID")))
                End If
                ddlToCurrency_SelectedIndexChanged(sender, e)
                RFVddlToCurrency.ErrorMessage = "Select Currency." : RFVddlToCurrency.InitialValue = "Select Currency"
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
    Protected Sub ddlToCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlToCurrency.SelectedIndexChanged
        Dim sFrom As String = "", sTo As String = ""
        Dim iOperateOn As Integer = 0
        Try
            lblError.Text = "" : iPkID = 0
            If (ddlToCurrency.SelectedIndex > 0) Then
                iOperateOn = ddlToCurrency.SelectedValue
            Else
                txtTTbuy.Text = 0 : txtTTsell.Text = 0 : txtBuy.Text = 0 : txtSell.Text = 0 : iPkID = 0
            End If
            dt = objclsBankCurrency.LoadBankCurrencyDetails(sSession.AccessCode, sSession.AccessCodeID, iBankID, iOperateOn, iPkID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0).Item("BCM_DelFlag")) = False Then
                    If dt.Rows(0).Item("BCM_DelFlag") = "A" Then
                        imgbtnAdd.Visible = False
                    End If
                    If dt.Rows(0).Item("BCM_DelFlag") = "W" Then
                        imgbtnAdd.Visible = True
                    End If
                    If dt.Rows(0).Item("BCM_DelFlag") = "D" Then
                        imgbtnAdd.Visible = False
                    End If
                End If
                If IsDBNull(dt.Rows(0).Item("BCM_PKID")) = False Then
                    iPkID = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0).Item("BCM_PKID").ToString())
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
                iPkID = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlToCurrency_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim objstrBankCurrencyMaster As New strBankCurrencyMaster
        Dim arr As Array
        Dim iType As Integer = 0
        Dim sFrom As String = "", sTo As String = ""
        Try
            lblError.Text = ""
            If ddlToCurrency.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Currency." : lblError.Text = "Select Currency."
                ddlToCurrency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            End If
            If ddlToCurrency.SelectedIndex > 0 Then
                If iPkID = 0 Then
                    iPkID = objclsBankCurrency.LoadBankCurrencyPKID(sSession.AccessCode, sSession.AccessCodeID, iBankID, ddlToCurrency.SelectedValue)
                End If
                objstrBankCurrencyMaster.iBCM_PKID = iPkID
                objstrBankCurrencyMaster.sBCM_Currency = ddlfromcurrency.SelectedValue
                objstrBankCurrencyMaster.sBCM_OperateOn = ddlToCurrency.SelectedValue
                objstrBankCurrencyMaster.sBCM_Date = DateTime.Now.ToString("dd/MM/yyyy")
                objstrBankCurrencyMaster.sBCM_IPAddress = sSession.IPAddress
                objstrBankCurrencyMaster.sBCM_CreatedBy = sSession.UserID
                objstrBankCurrencyMaster.sBCM_Time = objclsBankCurrency.GetCurrentTime(sSession.AccessCode)
                objstrBankCurrencyMaster.dBCM_TTBUY = objclsFASGeneral.SafeSQL(txtTTbuy.Text.Trim)
                objstrBankCurrencyMaster.dBCM_TTSell = objclsFASGeneral.SafeSQL(txtTTsell.Text.Trim)
                objstrBankCurrencyMaster.dBCM_BUY = objclsFASGeneral.SafeSQL(txtBuy.Text.Trim)
                objstrBankCurrencyMaster.dBCM_Sell = objclsFASGeneral.SafeSQL(txtSell.Text.Trim)
                objstrBankCurrencyMaster.iBCM_BankID = iBankID
                objstrBankCurrencyMaster.iBCM_CompID = sSession.AccessCodeID
                arr = objclsBankCurrency.SaveBankCurrencyMaster(sSession.AccessCode, objstrBankCurrencyMaster)
                iPkID = arr(1)
                iStatus = 2
                If arr(0) = 3 Then
                    lblValidationMsg.Text = "Successfully Saved & Waiting for Approval." : lblError.Text = "Successfully Saved & Waiting for Approval."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master Details", "Saved", iPkID, "", 0, "", sSession.IPAddress)
                Else
                    lblValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master Details", "Updated", iPkID, "", 0, "", sSession.IPAddress)
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
            lblError.Text = "" : txtTTbuy.Text = 0 : txtTTsell.Text = 0 : txtBuy.Text = 0 : txtSell.Text = 0 : iPkID = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object, oBankID As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(iStatus)))
            oBankID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(iBankID)))
            Response.Redirect(String.Format("~/Masters/BankCurrency.aspx?StatusID={0}&BankID={1}", oStatus, oBankID), False) 'Masters/EmployeeMaster
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
End Class
