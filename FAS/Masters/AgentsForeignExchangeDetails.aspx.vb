Imports System
Imports System.Data
Imports BusinesLayer
Partial Class Masters_AgentsForeignExchangeDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_AgentsForeignExchangeDetails"
    Dim objGen As New clsFASGeneral
    Private objSettings As New clsApplicationSettings
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objclsFASGeneral As New clsFASGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Dim objclsForeignExchangeAgents As New clsForeignExchangeAgents
    Private Shared iPKID As Integer = 0
    Private Shared sStatus As String
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
                BindAgency() : BindAllDropDown() : BindCategory() : BindIssueRiskType()
                If Request.QueryString("ID") IsNot Nothing Then
                    iPKID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ID")))
                    ddlExistingAgency.SelectedValue = iPKID
                    ddlExistingAgency_SelectedIndexChanged(sender, e)
                End If
                RFVAgencyName.ErrorMessage = "Enter Agency Name."
                REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                RFVAddress.ErrorMessage = "Enter Address."
                REVPostalCode.ValidationExpression = "[0-9]{6}" : REVPostalCode.ErrorMessage = "Enter Valid Postal Code."
                RFVCity.InitialValue = "Select City" : RFVCity.ErrorMessage = "Select City"
                RFVState.InitialValue = "Select State" : RFVState.ErrorMessage = "Select State"
                RFVCountry.InitialValue = "Select Billing Country" : RFVCountry.ErrorMessage = "Select Billing Country"
                REVMob.ValidationExpression = "[0-9]{10}" : REVMob.ErrorMessage = "Enter Valid Mobile No."
                REVTelphone.ValidationExpression = "[0-9]{10}" : REVTelphone.ErrorMessage = "Enter Valid Telephone no."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindAllDropDown()
        Dim dtCity As New DataTable
        Dim dtState As New DataTable
        Dim dtCountry As New DataTable
        Try
            dtCity = objclsForeignExchangeAgents.LoadCity(sSession.AccessCode, sSession.AccessCodeID)
            ddlCity.DataSource = dtCity
            ddlCity.DataTextField = "Mas_Desc"
            ddlCity.DataValueField = "Mas_Id"
            ddlCity.DataBind()
            ddlCity.Items.Insert(0, "Select City")

            dtState = objclsForeignExchangeAgents.LoadState(sSession.AccessCode, sSession.AccessCodeID)
            ddlState.DataSource = dtState
            ddlState.DataTextField = "Mas_Desc"
            ddlState.DataValueField = "Mas_Id"
            ddlState.DataBind()
            ddlState.Items.Insert(0, "Select State")

            dtCountry = objclsForeignExchangeAgents.LoadCountry(sSession.AccessCode, sSession.AccessCodeID)
            ddlCountry.DataSource = dtCountry
            ddlCountry.DataTextField = "Mas_Desc"
            ddlCountry.DataValueField = "Mas_Id"
            ddlCountry.DataBind()
            ddlCountry.Items.Insert(0, "Select Country")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindCategory()
        Dim dt As New DataTable
        Try
            dt = objclsForeignExchangeAgents.LoadCategory(sSession.AccessCode, sSession.AccessCodeID, "TRADER")
            ddlGSTNCategory.DataSource = dt
            ddlGSTNCategory.DataTextField = "GC_GSTCategory"
            ddlGSTNCategory.DataValueField = "GC_Id"
            ddlGSTNCategory.DataBind()
            ddlGSTNCategory.Items.Insert(0, "Select GSTNCategory")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindAgency()
        Dim dt As New DataTable
        Try
            dt = objclsForeignExchangeAgents.BindAgencyDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlExistingAgency.DataSource = dt
            ddlExistingAgency.DataTextField = "FE_AgencyName"
            ddlExistingAgency.DataValueField = "FE_ID"
            ddlExistingAgency.DataBind()
            ddlExistingAgency.Items.Insert(0, "Select Agency")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindIssueRiskType()
        Try
            ChkCurrency.DataSource = objSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ChkCurrency.DataTextField = "CUR_CODE"
            ChkCurrency.DataValueField = "CUR_ID"
            ChkCurrency.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindAgencyDetails()
        Dim dt As New DataTable, dtCurr As New DataTable
        Dim sCurrency As String = ""
        Try
            ClearAll()
            If ddlExistingAgency.SelectedIndex > 0 Then
                dt = objclsForeignExchangeAgents.LoadAgencyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingAgency.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("FE_AgencyName").ToString()) = False Then
                        txtAgencyName.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_AgencyName").ToString())
                    End If
                    If IsDBNull(dt.Rows(0).Item("FE_ContactName").ToString()) = False Then
                        txtContactPerson.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_ContactName").ToString())
                    End If

                    If IsDBNull(dt.Rows(0).Item("FE_MobileNo").ToString()) = False Then
                        txtMobNo.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_MobileNo").ToString())
                    End If
                    If IsDBNull(dt.Rows(0).Item("FE_Address").ToString()) = False Then
                        txtAddress.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_Address").ToString())
                    End If

                    ddlCountry.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("FE_Country").ToString()) = False Then
                        ddlCountry.SelectedValue = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_Country").ToString())
                    End If
                    If IsDBNull(dt.Rows(0).Item("FE_FAX").ToString()) = False Then
                        txtFax.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_FAX").ToString())
                    End If
                    If IsDBNull(dt.Rows(0).Item("FE_PostalCode").ToString()) = False Then
                        txtPostalCode.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_PostalCode").ToString())
                    End If
                    ddlGSTNCategory.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("FE_GSTNCategory").ToString()) = False Then
                        ddlGSTNCategory.SelectedValue = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_GSTNCategory").ToString())
                    End If

                    If IsDBNull(dt.Rows(0).Item("FE_PhoneNo").ToString()) = False Then
                        txtTelphone.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_PhoneNo").ToString())
                    End If
                    If IsDBNull(dt.Rows(0).Item("FE_GSTNRegNO").ToString()) = False Then
                        txtGSTNRegNo.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_GSTNRegNO").ToString())
                    End If
                    ddlCity.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("FE_City").ToString()) = False Then
                        ddlCity.SelectedValue = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_City").ToString())
                    End If
                    ddlState.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("FE_State").ToString()) = False Then
                        ddlState.SelectedValue = dt.Rows(0).Item("FE_State").ToString()
                    End If

                    If IsDBNull(dt.Rows(0).Item("FE_Website").ToString()) = False Then
                        txtWebsite.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_Website").ToString())
                    End If

                    If IsDBNull(dt.Rows(0).Item("FE_EMail").ToString()) = False Then
                        txtEmail.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_EMail").ToString())
                    End If

                    If IsDBNull(dt.Rows(0).Item("FE_Bank").ToString()) = False Then
                        txtBankName.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_Bank").ToString())
                    End If

                    If IsDBNull(dt.Rows(0).Item("FE_ACCNO").ToString()) = False Then
                        txtAccNo.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_ACCNO").ToString())
                    End If

                    If IsDBNull(dt.Rows(0).Item("FE_IFSC").ToString()) = False Then
                        txtIFSCode.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_IFSC").ToString())
                    End If

                    If IsDBNull(dt.Rows(0).Item("FE_BranchName").ToString()) = False Then
                        txtBranchName.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("FE_BranchName").ToString())
                    End If
                    If IsDBNull(dt.Rows(0).Item("FE_DelFlag")) = False Then
                        sStatus = dt.Rows(0).Item("FE_DelFlag").ToString()
                        If dt.Rows(0).Item("FE_DelFlag").ToString() = "W" Then
                            lblError.Text = "Waiting for Approval"
                            imgbtnAdd.Visible = True
                        ElseIf dt.Rows(0).Item("FE_DelFlag").ToString() = "D" Then
                            lblError.Text = "De-Activated"
                            imgbtnAdd.Visible = False
                        Else
                            imgbtnAdd.Visible = True
                        End If
                    End If
                End If
                sCurrency = GetSelectedUsersRiskType()
                If sCurrency.Length > 0 Then
                    For j = 0 To ChkCurrency.Items.Count - 1
                        If sCurrency.Contains("," & ChkCurrency.Items(j).Value & ",") = True Then
                            ChkCurrency.Items(j).Selected = True
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function GetSelectedUsersRiskType() As String
        Dim i As Integer
        Dim sCurrency As String = ""
        Dim dtCurr As New DataTable
        Try
            dtCurr = objclsForeignExchangeAgents.LoadAgencyCurrencyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingAgency.SelectedValue)
            If dtCurr.Rows.Count > 0 Then
                For i = 0 To dtCurr.Rows.Count - 1
                    sCurrency = sCurrency & "," & dtCurr.Rows(i).Item("FEA_Currency")
                Next
                sCurrency = sCurrency & ","
            End If
            Return sCurrency
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub ddlExistingAgency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingAgency.SelectedIndexChanged
        Try
            If ddlExistingAgency.SelectedIndex > 0 Then
                BindAgencyDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingAgency_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            ddlExistingAgency.SelectedIndex = 0
            ClearAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub
    Private Sub ClearAll()
        txtAgencyName.Text = "" : txtContactPerson.Text = "" : txtMobNo.Text = ""
        txtAddress.Text = "" : ddlCountry.SelectedIndex = 0 : txtFax.Text = "" : txtPostalCode.Text = "" : ddlGSTNCategory.SelectedIndex = 0
        txtTelphone.Text = "" : txtGSTNRegNo.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0 : txtWebsite.Text = "" : txtEmail.Text = ""
        txtBankName.Text = "" : txtAccNo.Text = "" : txtIFSCode.Text = "" : txtBranchName.Text = "" : ChkCurrency.SelectedIndex = -1
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim Arr() As String, Arr1() As String
        Dim iPkid As Integer
        Try
            If ChkCurrency.SelectedIndex = -1 Then
                lblError.Text = "Select Currency."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Currency.','', 'success');", True)
                Exit Sub
            End If
            If ddlExistingAgency.SelectedIndex > 0 Then
                objclsForeignExchangeAgents.FE_ID = ddlExistingAgency.SelectedValue
            Else
                objclsForeignExchangeAgents.FE_ID = 0
                iPkid = objclsForeignExchangeAgents.GetAgencyID(sSession.AccessCode, sSession.AccessCodeID, objclsFASGeneral.SafeSQL(txtAgencyName.Text.Trim))
                If iPkid > 0 Then
                    lblError.Text = "This Agency Name Already Exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This Agency Name Already Exists.','', 'success');", True)
                    Exit Sub
                End If
            End If
            objclsForeignExchangeAgents.FE_AgencyName = objclsFASGeneral.SafeSQL(txtAgencyName.Text.Trim)
            objclsForeignExchangeAgents.FE_ContactName = objclsFASGeneral.SafeSQL(txtContactPerson.Text.Trim)
            objclsForeignExchangeAgents.FE_PhoneNo = objclsFASGeneral.SafeSQL(txtTelphone.Text.Trim)
            objclsForeignExchangeAgents.FE_MobileNo = objclsFASGeneral.SafeSQL(txtMobNo.Text.Trim)
            objclsForeignExchangeAgents.FE_Address = objclsFASGeneral.SafeSQL(txtAddress.Text.Trim)
            objclsForeignExchangeAgents.FE_PostalCode = objclsFASGeneral.SafeSQL(txtPostalCode.Text.Trim)
            objclsForeignExchangeAgents.FE_City = ddlCity.SelectedValue
            objclsForeignExchangeAgents.FE_State = ddlState.SelectedValue
            objclsForeignExchangeAgents.FE_Country = ddlCountry.SelectedValue
            objclsForeignExchangeAgents.FE_FAX = objclsFASGeneral.SafeSQL(txtFax.Text.Trim)
            objclsForeignExchangeAgents.FE_GSTNCategory = ddlGSTNCategory.SelectedValue
            objclsForeignExchangeAgents.FE_GSTNRegNO = objclsFASGeneral.SafeSQL(txtGSTNRegNo.Text.Trim)
            objclsForeignExchangeAgents.FE_Website = objclsFASGeneral.SafeSQL(txtWebsite.Text.Trim)
            objclsForeignExchangeAgents.FE_EMail = objclsFASGeneral.SafeSQL(txtEmail.Text.Trim)
            objclsForeignExchangeAgents.FE_Bank = objclsFASGeneral.SafeSQL(txtBankName.Text.Trim)
            objclsForeignExchangeAgents.FE_ACCNO = objclsFASGeneral.SafeSQL(txtAccNo.Text.Trim)
            objclsForeignExchangeAgents.FE_IFSC = objclsFASGeneral.SafeSQL(txtIFSCode.Text.Trim)
            objclsForeignExchangeAgents.FE_BranchName = objclsFASGeneral.SafeSQL(txtBranchName.Text.Trim)
            objclsForeignExchangeAgents.FE_CRBY = sSession.UserID
            objclsForeignExchangeAgents.FE_UpdatedBy = sSession.UserID
            objclsForeignExchangeAgents.FE_IPAddress = sSession.IPAddress
            objclsForeignExchangeAgents.FE_CompID = sSession.AccessCodeID
            Arr = objclsForeignExchangeAgents.SaveAgentsDetails(sSession.AccessCode, objclsForeignExchangeAgents)
            If ChkCurrency.Items.Count > 0 Then
                objclsForeignExchangeAgents.DeleteAgencyCurrency(sSession.AccessCode, sSession.AccessCodeID, Arr(1))
                For i = 0 To ChkCurrency.Items.Count - 1
                    If ChkCurrency.Items(i).Selected Then
                        objclsForeignExchangeAgents.FEA_ID = 0
                        objclsForeignExchangeAgents.FEA_FEID = Arr(1)
                        objclsForeignExchangeAgents.FEA_Currency = ChkCurrency.Items(i).Value
                        objclsForeignExchangeAgents.FEA_CRBY = sSession.UserID
                        objclsForeignExchangeAgents.FEA_CompID = sSession.AccessCodeID
                        Arr1 = objclsForeignExchangeAgents.SaveAgentsCurrencyDetails(sSession.AccessCode, objclsForeignExchangeAgents)
                    End If
                Next
            End If
            BindAgency()
            ddlExistingAgency.SelectedValue = Arr(1)
            ddlExistingAgency_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Details", "Updated", Arr(1), "", 0, "", sSession.IPAddress)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval.','', 'success');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Details", "Saved", Arr(1), "", 0, "", sSession.IPAddress)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Dim iStatus As Integer = 0
        Try
            lblError.Text = ""
            If sStatus = "W" Then
                iStatus = 2
            ElseIf sStatus = "D" Then
                iStatus = 1
            Else
                iStatus = 0
            End If
            oStatus = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(iStatus)))
            Response.Redirect(String.Format("~/Masters/AgentsForeignExchange.aspx?StatusID={0}", oStatus), False) 'Masters/AgentsForeignExchange
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
End Class

