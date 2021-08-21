Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_PettyCashTransaction
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PettyCashTransaction"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Dim objPC As New clsPettyCash
    Private Shared sPCTAoD As String
    Private Shared sPCTAP As String
    Private Shared sPCTED As String
    Dim dt As New DataTable

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PCT")
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False
                imgbtnDeActivate.Visible = False : sPCTAoD = "NO" : sPCTAP = "NO" : sPCTED = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        ' imgbtnDeActivate.Visible = True
                        sPCTAoD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sPCTAP = "YES"
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sPCTED = "YES"
                    End If
                End If

                BindStatus()

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                imgbtnAdd.Visible = True
                'imgbtnReport.Visible = True
                BindPettyCashDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
                ddlStatus_SelectedIndexChanged(sender, e)
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
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""

            imgbtnAdd.Visible = True
            imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            'imgbtnReport.Visible = True
            If ddlStatus.SelectedIndex = 0 Then
                ' imgbtnDeActivate.Visible = True
                imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnActivate.Visible = True : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnWaiting.Visible = True : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If

            BindPettyCashDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindPettyCashDetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer, ByVal iYearID As Integer)
        Try
            If ddlStatus.SelectedIndex = 0 Then
                If sPCTAoD = "YES" Then
                    ' imgbtnDeActivate.Visible = True 'Activate
                Else
                    imgbtnDeActivate.Visible = False 'Activate
                End If
                imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 1 Then
                If sPCTAoD = "YES" Then
                    imgbtnActivate.Visible = True 'De-Activate
                Else
                    imgbtnActivate.Visible = False 'De-Activate
                End If
                imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                If sPCTAP = "YES" Then
                    imgbtnWaiting.Visible = True 'Waiting for Approval
                Else
                    imgbtnWaiting.Visible = False 'Waiting for Approval
                End If
                imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If
            dt = objPC.LoadPettyCash(sSession.AccessCode, sSession.AccessCodeID, iStatus, sSession.YearID)
            dgPettyCash.DataSource = dt
            dgPettyCash.DataBind()
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
                For iIndx = 0 To dgPettyCash.Rows.Count - 1
                    chkField = dgPettyCash.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgPettyCash.Rows.Count - 1
                    chkField = dgPettyCash.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub dgPettyCash_PreRender(sender As Object, e As EventArgs) Handles dgPettyCash.PreRender
        Try
            If dgPettyCash.Rows.Count > 0 Then
                dgPettyCash.UseAccessibleHeader = True
                dgPettyCash.HeaderRow.TableSection = TableRowSection.TableHeader
                dgPettyCash.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPettyCash_PreRender")
        End Try
    End Sub
    Private Sub dgPettyCash_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPettyCash.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                'If sSGMAD = "YES" Then
                dgPettyCash.Columns(0).Visible = True
                'End If
                dgPettyCash.Columns(5).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    dgPettyCash.Columns(8).Visible = False
                    If sPCTAoD = "YES" Then
                        dgPettyCash.Columns(8).Visible = True
                        dgPettyCash.Columns(10).Visible = False
                    End If

                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'If sSGMAD = "YES" Then
                    dgPettyCash.Columns(4).Visible = True
                    'End If
                    'If sSGMSave = "YES" Then
                    dgPettyCash.Columns(5).Visible = True : dgPettyCash.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    dgPettyCash.Columns(8).Visible = False
                    If sPCTAoD = "YES" Then
                        dgPettyCash.Columns(8).Visible = True
                    End If

                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'If sSGMAD = "YES" Then
                    dgPettyCash.Columns(4).Visible = True
                    dgPettyCash.Columns(5).Visible = True : dgPettyCash.Columns(9).Visible = False
                    ' End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    dgPettyCash.Columns(8).Visible = False
                    If sPCTAP = "YES" Then
                        dgPettyCash.Columns(8).Visible = True
                    End If

                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'If sSGMAD = "YES" Then
                    dgPettyCash.Columns(4).Visible = True
                    'End If
                    'If sSGMSave = "YES" Then
                    dgPettyCash.Columns(5).Visible = True : dgPettyCash.Columns(9).Visible = False
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgPettyCash.Columns(0).Visible = False : dgPettyCash.Columns(5).Visible = False : dgPettyCash.Columns(9).Visible = False

                End If
            End If

            dgPettyCash.Columns(9).Visible = False
            If sPCTED = "YES" Then
                dgPettyCash.Columns(9).Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPettyCash_RowDataBound")
        End Try
    End Sub
    Private Sub dgPettyCash_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPettyCash.RowCommand
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
                Response.Redirect(String.Format("~/Accounts/PettyCashTransactionDetails.aspx?StatusID={0}&MasterID={1}&MasterName={2}", oStatusID, oMasterID, oMasterName), False) 'GeneralMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objPC.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objPC.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objPC.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    objPC.WriteGLTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID)
                    'objPC.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress)
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                End If
                ddlStatus.SelectedIndex = 0
                BindPettyCashDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPettyCash_RowCommand")
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

            Response.Redirect(String.Format("~/Accounts/PettyCashTransactionDetails.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgPettyCash.Rows.Count = 0 Then
                lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgPettyCash.Rows.Count - 1
                chkSelect = dgPettyCash.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate','', 'info');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgPettyCash.Rows.Count - 1
                chkSelect = dgPettyCash.Rows(i).FindControl("chkSelect")
                lblDescID = dgPettyCash.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objPC.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            BindPettyCashDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgPettyCash.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgPettyCash.Rows.Count - 1
                chkSelect = dgPettyCash.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to De-Activate.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgPettyCash.Rows.Count - 1
                chkSelect = dgPettyCash.Rows(i).FindControl("chkSelect")
                lblDescID = dgPettyCash.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objPC.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 1
            BindPettyCashDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgPettyCash.Rows.Count = 0 Then
                lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgPettyCash.Rows.Count - 1
                chkSelect = dgPettyCash.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgPettyCash.Rows.Count - 1
                chkSelect = dgPettyCash.Rows(i).FindControl("chkSelect")
                lblDescID = dgPettyCash.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objPC.UpdatePaymentMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    'objPC.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress)
                    objPC.WriteGLTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID)
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'info');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            BindPettyCashDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub dgPettyCash_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgPettyCash.RowEditing

    End Sub
End Class
