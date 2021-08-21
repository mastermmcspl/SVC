Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_PurchaseSalesJE
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PurchaseSalesJE"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private Shared sPSJEAoD As String
    Private Shared sPSJEAP As String
    Private objclsModulePermission As New clsModulePermission
    Dim objJE As New ClsPurchaseSalesJE
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        'imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        'imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        'imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        'imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnWaiting.Visible = False
                imgbtnAdd.Visible = True
                'imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnReport.Visible = False
                sPSJEAoD = "NO" : sPSJEAP = "NO"
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PSJT")
                'imgbtnReport.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        'imgbtnDeActivate.Visible = True
                        sPSJEAoD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sPSJEAP = "YES"
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        'imgbtnReport.Visible = True
                    End If
                End If
                BindStatus()
                BindPS()
                'ddlPS.SelectedValue = 1

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                'imgbtnAdd.Visible = True
                'imgbtnReport.Visible = True
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
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
            ddlStatus.SelectedIndex = 2
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindPS()
        Try
            ddlPS.Items.Insert(0, "Purchase")
            ddlPS.Items.Insert(1, "Sales")
            ddlPS.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sMainMaster As String = ""
        Try
            lblError.Text = ""
            sMainMaster = ""
            imgbtnAdd.Visible = True
            'imgbtnReport.Visible = True
            'If ddlStatus.SelectedIndex = 0 Then
            '    If sPSJEAoD = "YES" Then
            '        imgbtnDeActivate.Visible = True
            '    Else
            '        imgbtnDeActivate.Visible = False
            '    End If
            '    imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            'ElseIf ddlStatus.SelectedIndex = 1 Then
            '    If sPSJEAoD = "YES" Then
            '        imgbtnActivate.Visible = True
            '    Else
            '        imgbtnActivate.Visible = False
            '    End If
            '    imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            'ElseIf ddlStatus.SelectedIndex = 2 Then
            '    If sPSJEAP = "YES" Then
            '        imgbtnWaiting.Visible = True
            '    Else
            '        imgbtnWaiting.Visible = False
            '    End If
            '    imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            'Else
            '    imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            'End If

            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindJEDetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer, ByVal iPS As Integer)
        Dim dt As New DataTable, dtSR, dtGR As New DataTable
        Dim dtCS, dtCP As New DataTable
        Try
            lblSI.Visible = False : lblSRJE.Visible = False : lblCSJE.Visible = False

            dgJE.DataSource = Nothing
            dgJE.DataBind()

            dgJESR.DataSource = Nothing
            dgJESR.DataBind()

            dgJECS.DataSource = Nothing
            dgJECS.DataBind()

            If iPS = "0" Then 'Purchase

                dt = objJE.LoadPurchaseJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus, "PI")
                dgJE.DataSource = dt
                dgJE.DataBind()
                If dt.Rows.Count > 0 Then
                    lblSI.Text = "Purchase Invoice JE"
                    lblSI.Visible = True
                End If

                dtGR = objJE.LoadPurchaseJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus, "GR")
                dgJESR.DataSource = dtGR
                dgJESR.DataBind()
                If dtGR.Rows.Count > 0 Then
                    lblSRJE.Text = "Goods Return JE"
                    lblSRJE.Visible = True
                End If

                dtCP = objJE.LoadPurchaseJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus, "CP")
                dgJECS.DataSource = dtCP
                dgJECS.DataBind()
                If dtCP.Rows.Count > 0 Then
                    lblCSJE.Text = "Cash Purchase JE"
                    lblCSJE.Visible = True
                End If

            ElseIf iPS = "1" Then 'Sales

                'dt = objJE.LoadSalesJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus)
                'dgJE.DataSource = dt
                'dgJE.DataBind()
                'dtSR = objJE.LoadSalesJournalEntrySR(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus)
                'dgJESR.DataSource = dtSR
                'dgJESR.DataBind()
                dt = objJE.LoadSalesJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus, "SI")
                dgJE.DataSource = dt
                dgJE.DataBind()
                If dt.Rows.Count > 0 Then
                    lblSI.Text = "Sales Invoice JE"
                    lblSI.Visible = True
                End If

                dtSR = objJE.LoadSalesJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus, "SR")
                dgJESR.DataSource = dtSR
                dgJESR.DataBind()
                If dtSR.Rows.Count > 0 Then
                    lblSRJE.Text = "Sales Return JE"
                    lblSRJE.Visible = True
                End If

                dtCS = objJE.LoadSalesJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus, "CS")
                dgJECS.DataSource = dtCS
                dgJECS.DataBind()
                If dtCS.Rows.Count > 0 Then
                    lblCSJE.Text = "Cash Sales JE"
                    lblCSJE.Visible = True
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgJE.Rows.Count - 1
                    chkField = dgJE.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgJE.Rows.Count - 1
                    chkField = dgJE.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Protected Sub chkSelectAllSR_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgJESR.Rows.Count - 1
                    chkField = dgJESR.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgJESR.Rows.Count - 1
                    chkField = dgJESR.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllSR_CheckedChanged")
        End Try
    End Sub
    Protected Sub chkSelectAllCS_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgJECS.Rows.Count - 1
                    chkField = dgJECS.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgJECS.Rows.Count - 1
                    chkField = dgJECS.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllCS_CheckedChanged")
        End Try
    End Sub
    Private Sub dgJE_PreRender(sender As Object, e As EventArgs) Handles dgJE.PreRender
        Dim dt As New DataTable
        Try
            If dgJE.Rows.Count > 0 Then
                dgJE.UseAccessibleHeader = True
                dgJE.HeaderRow.TableSection = TableRowSection.TableHeader
                dgJE.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPayment_PreRender")
        End Try
    End Sub
    Private Sub dgJESR_PreRender(sender As Object, e As EventArgs) Handles dgJESR.PreRender
        Dim dt As New DataTable
        Try
            If dgJESR.Rows.Count > 0 Then
                dgJESR.UseAccessibleHeader = True
                dgJESR.HeaderRow.TableSection = TableRowSection.TableHeader
                dgJESR.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJESR_PreRender")
        End Try
    End Sub
    Private Sub dgJE_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgJE.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgJE.Columns(0).Visible = True : dgJE.Columns(8).Visible = False

                'If ddlStatus.SelectedIndex = 0 Then
                '    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                '    If sPSJEAoD = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJE.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                'End If

                'If ddlStatus.SelectedIndex = 1 Then
                '    If sPSJEAoD = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJE.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                '    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                'End If

                'If ddlStatus.SelectedIndex = 2 Then
                '    If sPSJEAP = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJE.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                '    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                'End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.Visible = False : imgbtnEdit.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJE_RowDataBound")
        End Try
    End Sub
    Private Sub dgJESR_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgJESR.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgJESR.Columns(0).Visible = True : dgJESR.Columns(8).Visible = False

                'If ddlStatus.SelectedIndex = 0 Then
                '    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                '    If sPSJEAoD = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJESR.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                'End If

                'If ddlStatus.SelectedIndex = 1 Then
                '    If sPSJEAoD = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJESR.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                '    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                'End If

                'If ddlStatus.SelectedIndex = 2 Then
                '    If sPSJEAP = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJESR.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                '    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                'End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.Visible = False : imgbtnEdit.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJESR_RowDataBound")
        End Try
    End Sub
    Private Sub dgJE_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgJE.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/Accounts/PurchaseSalesJEDetails.aspx?StatusID={0}&MasterID={1}&sPSStr={2}", oStatusID, oMasterID, ddlPS.SelectedItem.Text), False) 'GeneralMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "SI")
                    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "SI")
                    lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "SI")
                    'objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    objJE.WriteGLTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, ddlPS.SelectedIndex)
                    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJE_RowCommand")
        End Try
    End Sub
    Private Sub dgJESR_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgJESR.RowCommand
        Dim oStatusID As Object, oMasterID As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/Accounts/PurchaseSalesJEDetails.aspx?StatusID={0}&MasterID={1}&sPSStr={2}", oStatusID, oMasterID, ddlPS.SelectedItem.Text), False) 'GeneralMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "SR")
                    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "SR")
                    lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "SR")
                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJE_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try

            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(3))
            End If
            Response.Redirect(String.Format("~/Accounts/PurchaseSalesJEDetails.aspx?StatusID={0}&MasterName={1}&sPSStr={2}", oStatusID, oMasterName, ddlPS.SelectedItem.Text), False)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub ddlPS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPS.SelectedIndexChanged
        Try
            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPS_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub dgJE_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgJE.RowEditing

    End Sub
    '    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
    '        Dim chkSelect As New CheckBox
    '        Dim iCount As Integer
    '        Dim lblDescID As New Label
    '        Try
    '            If dgJE.Rows.Count = 0 Then
    '                lblError.Text = "No data to activate"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
    '                Exit Sub
    '            End If

    '            For i = 0 To dgJE.Rows.Count - 1
    '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
    '                If chkSelect.Checked = True Then
    '                    iCount = 1
    '                    GoTo NextSave
    '                End If
    '            Next
    '            If iCount = 0 Then
    '                lblError.Text = "Select to Activate."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate','', 'info');", True)
    '                Exit Sub
    '            End If

    'NextSave:   For i = 0 To dgJE.Rows.Count - 1
    '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
    '                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
    '                If chkSelect.Checked = True Then
    '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, lblDescID.Text, sSession.IPAddress)
    '                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
    '                    lblError.Text = "Successfully Activated."
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
    '                End If
    '            Next
    '            'If ddlStatus.SelectedIndex = 1 Then
    '            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
    '            '    lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
    '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
    '            'End If
    '            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
    '        Catch ex As Exception
    '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
    '        End Try
    '    End Sub
    '    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
    '        Dim chkSelect As New CheckBox
    '        Dim iCount As Integer
    '        Dim lblDescID As New Label
    '        Try
    '            If dgJE.Rows.Count = 0 Then
    '                lblError.Text = "No data to De-Activate"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
    '                Exit Sub
    '            End If

    '            For i = 0 To dgJE.Rows.Count - 1
    '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
    '                If chkSelect.Checked = True Then
    '                    iCount = 1
    '                    GoTo NextSave
    '                End If
    '            Next
    '            If iCount = 0 Then
    '                lblError.Text = "Select to De-Activate."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to De-Activate.','', 'info');", True)
    '                Exit Sub
    '            End If
    'NextSave:   For i = 0 To dgJE.Rows.Count - 1
    '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
    '                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
    '                If chkSelect.Checked = True Then
    '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, lblDescID.Text, sSession.IPAddress)
    '                    lblError.Text = "Successfully De-Activated."
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
    '                End If
    '            Next
    '            'If ddlStatus.SelectedIndex = 0 Then
    '            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
    '            '    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
    '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
    '            'End If
    '            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
    '        Catch ex As Exception
    '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
    '        End Try
    '    End Sub
    '    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
    '        Dim chkSelect As New CheckBox
    '        Dim iCount As Integer
    '        Dim lblDescID As New Label
    '        Try
    '            If dgJE.Rows.Count = 0 Then
    '                lblError.Text = "No data to Approve"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
    '                Exit Sub
    '            End If

    '            For i = 0 To dgJE.Rows.Count - 1
    '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
    '                If chkSelect.Checked = True Then
    '                    iCount = 1
    '                    GoTo NextSave
    '                End If
    '            Next
    '            If iCount = 0 Then
    '                lblError.Text = "Select to Approve."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve','', 'info');", True)
    '                Exit Sub
    '            End If
    'NextSave:   For i = 0 To dgJE.Rows.Count - 1
    '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
    '                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
    '                If chkSelect.Checked = True Then
    '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, lblDescID.Text, sSession.IPAddress)
    '                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
    '                    lblError.Text = "Successfully Approved."
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
    '                End If
    '            Next

    '            'If ddlStatus.SelectedIndex = 2 Then
    '            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
    '            '    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress)
    '            '    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
    '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
    '            'End If
    '            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
    '        Catch ex As Exception
    '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
    '        End Try
    '    End Sub
    'Preethi Changes
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        '        Dim chkSelect As New CheckBox
        '        Dim iCount As Integer, iCountSR As Integer
        '        Dim lblDescID As New Label
        '        Try
        '            If dgJE.Rows.Count = 0 And dgJESR.Rows.Count = 0 Then
        '                lblError.Text = "No data to activate"
        '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
        '                Exit Sub
        '            End If

        '            For i = 0 To dgJE.Rows.Count - 1
        '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
        '                If chkSelect.Checked = True Then
        '                    iCount = 1
        '                    GoTo NextSave
        '                End If
        '            Next
        '            For i = 0 To dgJESR.Rows.Count - 1
        '                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
        '                If chkSelect.Checked = True Then
        '                    iCountSR = 1
        '                    GoTo NextSaveSR
        '                End If
        '            Next
        '            If iCount = 0 And iCountSR = 0 Then
        '                lblError.Text = "Select to Activate."
        '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate','', 'info');", True)
        '                Exit Sub
        '            End If

        'NextSave:   For i = 0 To dgJE.Rows.Count - 1
        '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
        '                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
        '                If chkSelect.Checked = True Then
        '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SI")
        '                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
        '                    lblError.Text = "Successfully Activated."
        '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
        '                End If
        '            Next
        'NextSaveSR: For i = 0 To dgJESR.Rows.Count - 1
        '                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
        '                lblDescID = dgJESR.Rows(i).FindControl("lblDescID")
        '                If chkSelect.Checked = True Then
        '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SR")
        '                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
        '                    lblError.Text = "Successfully Activated."
        '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
        '                End If
        '            Next
        '            'If ddlStatus.SelectedIndex = 1 Then
        '            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
        '            '    lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
        '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
        '            'End If
        '            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
        '        Catch ex As Exception
        '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        '        End Try
        Dim chkSelect As New CheckBox
        Dim iCount As Integer = 0, iCountSR As Integer = 0, iCountCS As Integer = 0
        Dim lblDescID As New Label
        Try
            If dgJE.Rows.Count = 0 And dgJESR.Rows.Count = 0 And dgJECS.Rows.Count = 0 Then
                lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            For i = 0 To dgJESR.Rows.Count - 1
                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCountSR = 1
                    GoTo NextSaveSR
                End If
            Next
            For i = 0 To dgJECS.Rows.Count - 1
                chkSelect = dgJECS.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCountCS = 1
                    GoTo NextSaveCS
                End If
            Next
            If iCount = 0 And iCountSR = 0 And iCountCS = 0 Then
                lblError.Text = "Select to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate','', 'info');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SI")
                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
NextSaveSR: For i = 0 To dgJESR.Rows.Count - 1
                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
                lblDescID = dgJESR.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SR")
                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
NextSaveCS: For i = 0 To dgJECS.Rows.Count - 1
                chkSelect = dgJECS.Rows(i).FindControl("chkSelect")
                lblDescID = dgJECS.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, lblDescID.Text, sSession.IPAddress, "CS")
                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
            'If ddlStatus.SelectedIndex = 1 Then
            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
            '    lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            'End If
            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        '        Dim chkSelect As New CheckBox
        '        Dim iCount As Integer, iCountSR As Integer
        '        Dim lblDescID As New Label
        '        Try
        '            If dgJE.Rows.Count = 0 And dgJESR.Rows.Count = 0 Then
        '                lblError.Text = "No data to De-Activate"
        '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
        '                Exit Sub
        '            End If

        '            For i = 0 To dgJE.Rows.Count - 1
        '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
        '                If chkSelect.Checked = True Then
        '                    iCount = 1
        '                    GoTo NextSave
        '                End If
        '            Next
        '            For i = 0 To dgJESR.Rows.Count - 1
        '                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
        '                If chkSelect.Checked = True Then
        '                    iCountSR = 1
        '                    GoTo NextSaveSR
        '                End If
        '            Next
        '            If iCount = 0 And iCountSR = 0 Then
        '                lblError.Text = "Select to De-Activate."
        '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to De-Activate.','', 'info');", True)
        '                Exit Sub
        '            End If
        'NextSave:   For i = 0 To dgJE.Rows.Count - 1
        '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
        '                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
        '                If chkSelect.Checked = True Then
        '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SI")
        '                    lblError.Text = "Successfully De-Activated."
        '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
        '                End If
        '            Next
        'NextSaveSR: For i = 0 To dgJESR.Rows.Count - 1
        '                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
        '                lblDescID = dgJESR.Rows(i).FindControl("lblDescID")
        '                If chkSelect.Checked = True Then
        '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SR")
        '                    lblError.Text = "Successfully De-Activated."
        '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
        '                End If
        '            Next
        '            'If ddlStatus.SelectedIndex = 0 Then
        '            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
        '            '    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
        '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
        '            'End If
        '            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
        '        Catch ex As Exception
        '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        '        End Try
        Dim chkSelect As New CheckBox
        Dim iCount As Integer = 0, iCountSR As Integer = 0, iCountCS As Integer = 0
        Dim lblDescID As New Label
        Try
            If dgJE.Rows.Count = 0 And dgJESR.Rows.Count = 0 And dgJECS.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            For i = 0 To dgJESR.Rows.Count - 1
                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCountSR = 1
                    GoTo NextSaveSR
                End If
            Next
            For i = 0 To dgJECS.Rows.Count - 1
                chkSelect = dgJECS.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCountCS = 1
                    GoTo NextSaveCS
                End If
            Next
            If iCount = 0 And iCountSR = 0 And iCountCS = 0 Then
                lblError.Text = "Select to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to De-Activate.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SI")
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
            Next
NextSaveSR: For i = 0 To dgJESR.Rows.Count - 1
                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
                lblDescID = dgJESR.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SR")
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
            Next
NextSaveCS: For i = 0 To dgJECS.Rows.Count - 1
                chkSelect = dgJECS.Rows(i).FindControl("chkSelect")
                lblDescID = dgJECS.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, lblDescID.Text, sSession.IPAddress, "CS")
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
            Next
            'If ddlStatus.SelectedIndex = 0 Then
            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
            '    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            'End If
            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        '        Dim chkSelect As New CheckBox
        '        Dim iCount As Integer, iCountSR As Integer
        '        Dim lblDescID As New Label
        '        Try
        '            If dgJE.Rows.Count = 0 And dgJESR.Rows.Count = 0 Then
        '                lblError.Text = "No data to Approve"
        '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
        '                Exit Sub
        '            End If

        '            For i = 0 To dgJE.Rows.Count - 1
        '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
        '                If chkSelect.Checked = True Then
        '                    iCount = 1
        '                    GoTo NextSave
        '                End If
        '            Next
        '            For i = 0 To dgJESR.Rows.Count - 1
        '                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
        '                If chkSelect.Checked = True Then
        '                    iCountSR = 1
        '                    GoTo NextSaveSR
        '                End If
        '            Next
        '            If iCount = 0 Then
        '                lblError.Text = "Select to Approve."
        '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve','', 'info');", True)
        '                Exit Sub
        '            End If
        'NextSave:   For i = 0 To dgJE.Rows.Count - 1
        '                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
        '                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
        '                If chkSelect.Checked = True Then
        '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SI")
        '                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
        '                    lblError.Text = "Successfully Approved."
        '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
        '                End If
        '            Next
        'NextSaveSR: For i = 0 To dgJESR.Rows.Count - 1
        '                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
        '                lblDescID = dgJESR.Rows(i).FindControl("lblDescID")
        '                If chkSelect.Checked = True Then
        '                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SR")
        '                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
        '                    lblError.Text = "Successfully Approved."
        '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
        '                End If
        '            Next
        '            'If ddlStatus.SelectedIndex = 2 Then
        '            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
        '            '    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress)
        '            '    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
        '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
        '            'End If
        '            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
        '        Catch ex As Exception
        '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        '        End Try
        Dim chkSelect As New CheckBox
        Dim iCount As Integer = 0, iCountSR As Integer = 0, iCountCS As Integer = 0
        Dim lblDescID As New Label
        Try
            If dgJE.Rows.Count = 0 And dgJESR.Rows.Count = 0 And dgJECS.Rows.Count = 0 Then
                lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            For i = 0 To dgJESR.Rows.Count - 1
                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCountSR = 1
                    GoTo NextSaveSR
                End If
            Next
            For i = 0 To dgJECS.Rows.Count - 1
                chkSelect = dgJECS.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCountCS = 1
                    GoTo NextSaveCS
                End If
            Next
            If iCount = 0 And iCountSR = 0 And iCountCS = 0 Then
                lblError.Text = "Select to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SI")
                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
            Next
NextSaveSR: For i = 0 To dgJESR.Rows.Count - 1
                chkSelect = dgJESR.Rows(i).FindControl("chkSelect")
                lblDescID = dgJESR.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, lblDescID.Text, sSession.IPAddress, "SR")
                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
            Next
NextSaveCS: For i = 0 To dgJECS.Rows.Count - 1
                chkSelect = dgJECS.Rows(i).FindControl("chkSelect")
                lblDescID = dgJECS.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, lblDescID.Text, sSession.IPAddress, "CS")
                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
            Next

            'If ddlStatus.SelectedIndex = 2 Then
            '    objJE.UpdateJEMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress)
            '    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress)
            '    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            'End If
            BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub

    Private Sub dgJESR_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgJESR.RowEditing

    End Sub

    Private Sub dgJECS_PreRender(sender As Object, e As EventArgs) Handles dgJECS.PreRender
        Dim dt As New DataTable
        Try
            If dgJECS.Rows.Count > 0 Then
                dgJECS.UseAccessibleHeader = True
                dgJECS.HeaderRow.TableSection = TableRowSection.TableHeader
                dgJECS.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJECS_PreRender")
        End Try
    End Sub

    Private Sub dgJECS_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgJECS.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgJECS.Columns(0).Visible = True : dgJECS.Columns(8).Visible = False

                'If ddlStatus.SelectedIndex = 0 Then
                '    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                '    If sPSJEAoD = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJECS.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                'End If

                'If ddlStatus.SelectedIndex = 1 Then
                '    If sPSJEAoD = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJECS.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                '    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                'End If

                'If ddlStatus.SelectedIndex = 2 Then
                '    If sPSJEAP = "YES" Then
                '        imgbtnStatus.Visible = True
                '        dgJECS.Columns(8).Visible = True
                '    End If
                '    imgbtnEdit.Visible = True
                '    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                'End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.Visible = False : imgbtnEdit.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJECS_RowDataBound")
        End Try
    End Sub

    Private Sub dgJECS_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgJECS.RowCommand
        Dim oStatusID As Object, oMasterID As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/Accounts/PurchaseSalesJEDetails.aspx?StatusID={0}&MasterID={1}&sPSStr={2}", oStatusID, oMasterID, ddlPS.SelectedItem.Text), False) 'GeneralMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "CS")
                    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "CS")
                    lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, ddlPS.SelectedIndex, sSession.IPAddress, sSession.YearID, "CS")
                    objJE.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlPS.SelectedIndex)
                    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlPS.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJECS_RowCommand")
        End Try
    End Sub
    Private Sub dgJECS_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgJECS.RowEditing

    End Sub
End Class
