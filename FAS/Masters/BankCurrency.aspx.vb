Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Masters_BankCurrency
    Inherits System.Web.UI.Page
    Private Shared sSession As AllSession
    Private objSettings As New clsApplicationSettings
    Private Shared dtCurrencyMaster As DataTable
    Private Shared objclsBankCurrency As New clsBankCurrency
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private sFormName As String = "BankCurrency"
    Private Shared iPkID As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                BindStatus() : BindBank()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("BankID") IsNot Nothing Then
                    ddlBankName.SelectedValue = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("BankID")))
                    ddlBankName_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Waiting for Approval")
            ddlStatus.Items.Insert(3, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
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
    Private Function LoadAllBankCurrencyDeatils() As DataTable
        Dim dt As New DataTable
        Dim sSearchText As String = "", sStatus As String = "", sFrom As String = "", sTo As String = ""
        Dim iOperateOn As Integer = 0, iBank As Integer
        Try
            imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 Then
                sStatus = "De-Activated"
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 Then
                sStatus = "Waiting for Approval"
                imgbtnWaiting.Visible = True 'Waiting for Approval         
            End If
            If (ddlBankName.SelectedIndex > 0) Then
                iBank = ddlBankName.SelectedValue
            End If
            If (ddlToCurrency.SelectedIndex > 0) Then
                iOperateOn = ddlToCurrency.SelectedValue
            End If
            If txtStartDate.Text <> "" Then
                sFrom = txtStartDate.Text
            End If
            If txtEndDate.Text <> "" Then
                sTo = txtEndDate.Text
            End If
            dtCurrencyMaster = objclsBankCurrency.LoadBankCurrencyDashboard(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iBank, iOperateOn, sFrom, sTo)
            gvcurrencyMaster.DataSource = Nothing
            If ddlStatus.SelectedIndex <= 2 Then
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtCurrencyMaster)
                DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                DVZRBADetails.Sort = "Currency ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            Else
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtCurrencyMaster)
                DVZRBADetails.Sort = "Currency ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            End If
            gvcurrencyMaster.DataSource = dt
            gvcurrencyMaster.DataBind()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Sub ddlToCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlToCurrency.SelectedIndexChanged
        Dim sFrom As String = "", sTo As String = ""
        Try
            lblError.Text = ""
            iPkID = 0
            If (ddlToCurrency.SelectedIndex = 0) Then
                txtStartDate.Text = "" : txtEndDate.Text = ""
            End If
            LoadAllBankCurrencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlToCurrency_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub gvcurrencyMaster_PreRender(sender As Object, e As EventArgs) Handles gvcurrencyMaster.PreRender
        Try
            If gvcurrencyMaster.Rows.Count > 0 Then
                gvcurrencyMaster.UseAccessibleHeader = True
                gvcurrencyMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvcurrencyMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvcurrencyMaster_PreRender")
        End Try
    End Sub
    Private Sub gvcurrencyMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvcurrencyMaster.RowDataBound
        Dim imgbtnEdit As New ImageButton
        Dim lblStatus As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim imgbtnStatus As ImageButton = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    If txtStartDate.Text <> "" Or txtEndDate.Text <> "" Then
                        gvcurrencyMaster.Columns(0).Visible = False
                        gvcurrencyMaster.Columns(13).Visible = False
                        gvcurrencyMaster.Columns(14).Visible = False
                    Else
                        gvcurrencyMaster.Columns(13).Visible = True
                        gvcurrencyMaster.Columns(14).Visible = False
                    End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    If txtStartDate.Text <> "" Or txtEndDate.Text <> "" Then
                        gvcurrencyMaster.Columns(0).Visible = False
                        gvcurrencyMaster.Columns(13).Visible = False
                        gvcurrencyMaster.Columns(14).Visible = False
                    Else
                        gvcurrencyMaster.Columns(13).Visible = True
                        gvcurrencyMaster.Columns(14).Visible = False
                    End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    If txtStartDate.Text <> "" Or txtEndDate.Text <> "" Then
                        gvcurrencyMaster.Columns(0).Visible = False
                        gvcurrencyMaster.Columns(13).Visible = False
                        gvcurrencyMaster.Columns(14).Visible = False
                    Else
                        gvcurrencyMaster.Columns(13).Visible = True
                        gvcurrencyMaster.Columns(14).Visible = True
                    End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    gvcurrencyMaster.Columns(0).Visible = False
                    gvcurrencyMaster.Columns(13).Visible = False
                    gvcurrencyMaster.Columns(14).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvcurrencyMaster_RowDataBound")
        End Try
    End Sub
    Private Sub gvcurrencyMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvcurrencyMaster.RowCommand
        Dim lblID As New Label, lblDate As New Label, lblCurrID As New Label, lblCurID As New Label
        Dim oID As Object, oBankID As New Object, oCurr As New Object, oCur As New Object
        Dim DVZRBADetails As New DataView(dtCurrencyMaster)
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
            lblDate = DirectCast(clickedRow.FindControl("lblDate"), Label)
            lblCurID = DirectCast(clickedRow.FindControl("lblCurID"), Label)
            lblCurrID = DirectCast(clickedRow.FindControl("lblCurrID"), Label)
            If e.CommandName.Equals("EditData") Then
                oID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblID.Text)))
                oBankID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(ddlBankName.SelectedValue))
                oCurr = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(lblCurrID.Text))
                Response.Redirect(String.Format("~/Masters/BankCurrencyDetails.aspx?BankID={0}&ToCurr={1}&ID={2}", oBankID, oCurr, oID), False) 'BankCurrencyDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    If objclsBankCurrency.CheckDataIsInUse(sSession.AccessCode, sSession.AccessCodeID, lblDate.Text, ddlBankName.SelectedValue, lblCurID.Text, lblCurrID.Text) = True Then
                        lblValidationMsg.Text = "Already used in Currency Master, can't be De-Activate" : lblError.Text = "Already used in Currency Master, can't be De-Activate"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                        Exit Sub
                    End If
                    objclsBankCurrency.BankCurrencyApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "DeActivated")
                    DVZRBADetails.Sort = "ID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblID.Text)
                    DVZRBADetails(iIndex)("Status") = "De-Activated"
                    dtCurrencyMaster = DVZRBADetails.ToTable
                    lblValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "De-Activated", lblID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsBankCurrency.BankCurrencyApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "Activated")
                    DVZRBADetails.Sort = "ID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtCurrencyMaster = DVZRBADetails.ToTable
                    lblValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "Activated", lblID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then 'Waiting for Approval
                    objclsBankCurrency.BankCurrencyApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "Created")
                    DVZRBADetails.Sort = "ID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtCurrencyMaster = DVZRBADetails.ToTable
                    lblValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "Approved", lblID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
                LoadAllBankCurrencyDeatils()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvcurrencyMaster_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Dim dASDate As Date, dAEDate As Date
        Dim iOperateOn As Integer = 0
        Dim sFrom As String = "", sTo As String = ""
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlBankName.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Bank." : lblError.Text = "Select Bank."
                txtStartDate.Text = "" : txtEndDate.Text = ""
                ddlBankName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtStartDate.Text.Trim = "" Then
                lblValidationMsg.Text = "Select From Date." : lblError.Text = "Select From Date."
                txtStartDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            Else
                Try
                    dASDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblValidationMsg.Text = "Enter valid From Date." : lblError.Text = "Enter valid From Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    txtStartDate.Focus()
                    Exit Sub
                End Try
            End If

            If txtEndDate.Text.Trim = "" Then
                lblValidationMsg.Text = "Select To Date." : lblError.Text = "Select To Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                txtEndDate.Focus()
                Exit Sub
            Else
                Try
                    dAEDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblValidationMsg.Text = "Enter valid To Date." : lblError.Text = "Enter valid To Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    txtEndDate.Focus()
                    Exit Sub
                End Try
            End If

            Dim dSSDate As Date, dSCDate As Date
            dSSDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSCDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dSSDate > dSCDate Then
                txtEndDate.Focus()
                lblValidationMsg.Text = "To Date (" & txtEndDate.Text & ") should be greater than or equal to From Date (" & txtStartDate.Text & ")."
                lblError.Text = "To Date (" & txtEndDate.Text & ") should be greater than or equal to From Date (" & txtStartDate.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            LoadAllBankCurrencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
    Private Sub ddlBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankName.SelectedIndexChanged
        Try
            ddlToCurrency.Items.Clear() : txtStartDate.Text = "" : txtEndDate.Text = ""
            gvcurrencyMaster.DataSource = Nothing
            gvcurrencyMaster.DataBind()
            If ddlBankName.SelectedIndex > 0 Then
                LoadCurrencyTypeDB()
                LoadAllBankCurrencyDeatils()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBankName_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oBankID As New Object, oCurrID As Object
        Try
            If ddlBankName.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Bank." : lblError.Text = "Select Bank."
                ddlBankName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            Else
                oBankID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(ddlBankName.SelectedValue))
                If ddlToCurrency.SelectedIndex > 0 Then
                    oCurrID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(ddlToCurrency.SelectedValue))
                    Response.Redirect(String.Format("~/Masters/BankCurrencyDetails.aspx?BankID={0}&ToCurr={1}", oBankID, oCurrID), False) 'BankCurrencyDetails
                Else
                    Response.Redirect(String.Format("~/Masters/BankCurrencyDetails.aspx?BankID={0}", oBankID), False) 'BankCurrencyDetails
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            ddlBankName_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtCurrencyMaster)
        Try
            lblError.Text = ""
            If gvcurrencyMaster.Rows.Count = 0 Then
                lblValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvcurrencyMaster.Rows.Count - 1
                chkSelect = gvcurrencyMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblValidationMsg.Text = "Select Data to Activate." : lblError.Text = "Select Data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvcurrencyMaster.Rows.Count - 1
                chkSelect = gvcurrencyMaster.Rows(i).FindControl("chkSelect")
                lblID = gvcurrencyMaster.Rows(i).FindControl("lblID")
                If chkSelect.Checked = True Then
                    objclsBankCurrency.BankCurrencyApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "Activated")
                    DVZRBADetails.Sort = "ID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtCurrencyMaster = DVZRBADetails.ToTable
                End If
            Next
            lblValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "Activated", lblID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            LoadAllBankCurrencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer
        Dim lblID As New Label, lblDate As Label, lblCurID As New Label, lblCurrID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtCurrencyMaster)
        Try
            lblError.Text = ""
            If gvcurrencyMaster.Rows.Count = 0 Then
                lblValidationMsg.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvcurrencyMaster.Rows.Count - 1
                chkSelect = gvcurrencyMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblValidationMsg.Text = "Select Data to De-Activate." : lblError.Text = "Select Data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvcurrencyMaster.Rows.Count - 1
                chkSelect = gvcurrencyMaster.Rows(i).FindControl("chkSelect")
                lblID = gvcurrencyMaster.Rows(i).FindControl("lblID")
                lblDate = gvcurrencyMaster.Rows(i).FindControl("lblDate")
                lblCurID = gvcurrencyMaster.Rows(i).FindControl("lblCurID")
                lblCurrID = gvcurrencyMaster.Rows(i).FindControl("lblCurrID")
                If chkSelect.Checked = True Then
                    If objclsBankCurrency.CheckDataIsInUse(sSession.AccessCode, sSession.AccessCodeID, lblDate.Text, ddlBankName.SelectedValue, lblCurID.Text, lblCurrID.Text) = False Then
                        objclsBankCurrency.BankCurrencyApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "DeActivated")
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "De-Activated", lblID.Text, "", 0, "", sSession.IPAddress)
                        DVZRBADetails.Sort = "ID"
                        Dim iIndex As Integer = DVZRBADetails.Find(lblID.Text)
                        DVZRBADetails(iIndex)("Status") = "De-Activated"
                        dtCurrencyMaster = DVZRBADetails.ToTable
                    Else
                        iCheck = 1
                    End If
                End If
            Next
            If iCheck = 0 Then
                lblValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "De-Activated", lblID.Text, "", 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            Else
                lblValidationMsg.Text = "Already used in Currency Master, can't be De-Activate." : lblError.Text = "Already used in Currency Master, can't be De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            LoadAllBankCurrencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtCurrencyMaster)
        Try
            lblError.Text = ""
            If gvcurrencyMaster.Rows.Count = 0 Then
                lblValidationMsg.Text = "No data to Approve." : lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvcurrencyMaster.Rows.Count - 1
                chkSelect = gvcurrencyMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblValidationMsg.Text = "Select Data to Approve." : lblError.Text = "Select Data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvcurrencyMaster.Rows.Count - 1
                chkSelect = gvcurrencyMaster.Rows(i).FindControl("chkSelect")
                lblID = gvcurrencyMaster.Rows(i).FindControl("lblID")
                If chkSelect.Checked = True Then
                    objclsBankCurrency.BankCurrencyApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "Created")
                    DVZRBADetails.Sort = "ID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtCurrencyMaster = DVZRBADetails.ToTable
                End If
            Next
            lblValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "Approved", lblID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            LoadAllBankCurrencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvcurrencyMaster.Rows.Count - 1
                    chkField = gvcurrencyMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvcurrencyMaster.Rows.Count - 1
                    chkField = gvcurrencyMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            If ddlBankName.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Bank." : lblError.Text = "Select Bank."
                ddlBankName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            dt = LoadAllBankCurrencyDeatils()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/BankCurrencyMatser.rdlc")

            Dim BankName As ReportParameter() = New ReportParameter() {New ReportParameter("BankName", ddlBankName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(BankName)

            If ddlToCurrency.SelectedIndex > 0 Then
                Dim ToCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("ToCurrency", ddlToCurrency.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(ToCurrency)
            Else
                Dim ToCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("ToCurrency", " ")}
                ReportViewer1.LocalReport.SetParameters(ToCurrency)
            End If

            If txtStartDate.Text <> "" Then
                Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", txtStartDate.Text)}
                ReportViewer1.LocalReport.SetParameters(FromDate)
            Else
                Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", " ")}
                ReportViewer1.LocalReport.SetParameters(FromDate)
            End If

            If txtEndDate.Text <> "" Then
                Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", txtEndDate.Text)}
                ReportViewer1.LocalReport.SetParameters(ToDate)
            Else
                Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", " ")}
                ReportViewer1.LocalReport.SetParameters(ToDate)
            End If
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=BankCurrencyMatser" + ".pdf")
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
        Dim dt As New DataTable
        Dim iOperateOn As Integer = 0
        Dim sFrom As String = "", sTo As String = ""
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            If ddlBankName.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Bank." : lblError.Text = "Select Bank."
                ddlBankName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            dt = LoadAllBankCurrencyDeatils()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/BankCurrencyMatser.rdlc")

            Dim BankName As ReportParameter() = New ReportParameter() {New ReportParameter("BankName", ddlBankName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(BankName)

            If ddlToCurrency.SelectedIndex > 0 Then
                Dim ToCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("ToCurrency", ddlToCurrency.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(ToCurrency)
            Else
                Dim ToCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("ToCurrency", " ")}
                ReportViewer1.LocalReport.SetParameters(ToCurrency)
            End If

            If txtStartDate.Text <> "" Then
                Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", txtStartDate.Text)}
                ReportViewer1.LocalReport.SetParameters(FromDate)
            Else
                Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", " ")}
                ReportViewer1.LocalReport.SetParameters(FromDate)
            End If

            If txtEndDate.Text <> "" Then
                Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", txtEndDate.Text)}
                ReportViewer1.LocalReport.SetParameters(ToDate)
            Else
                Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", " ")}
                ReportViewer1.LocalReport.SetParameters(ToDate)
            End If
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Bank Currency Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=BankCurrencyMatser" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
End Class
