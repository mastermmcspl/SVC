Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Sales_SalesPartyDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PurchaseSalesJE"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objSPD As New ClsSalesPartyDashboard
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
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                imgbtnReport.Visible = True
                BindSODetails(0, ddlStatus.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
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

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sMainMaster As String = ""
        Try
            lblError.Text = ""
            sMainMaster = ""
            imgbtnAdd.Visible = True
            imgbtnReport.Visible = True
            If ddlStatus.SelectedIndex = 0 Then
                ' imgbtnDeActivate.Visible = True : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 1 Then
                ' imgbtnActivate.Visible = True : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                ' imgbtnWaiting.Visible = True : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            Else
                '  imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If

            BindSODetails(0, ddlStatus.SelectedIndex)
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindSODetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer)
        Dim dt As New DataTable
        Try
            dt = objSPD.LoadSalesParty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus)
            dgParty.DataSource = dt
            dgParty.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgParty_PreRender(sender As Object, e As EventArgs) Handles dgParty.PreRender
        Dim dt As New DataTable
        Try
            If dgParty.Rows.Count > 0 Then
                dgParty.UseAccessibleHeader = True
                dgParty.HeaderRow.TableSection = TableRowSection.TableHeader
                dgParty.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgParty_PreRender")
        End Try
    End Sub
    Private Sub dgParty_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgParty.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgParty.Columns(0).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    imgbtnStatus.Visible = True : imgbtnEdit.Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.Visible = True
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.Visible = True
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.Visible = False : imgbtnEdit.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPayment_ItemDataBound")
        End Try
    End Sub
    Private Sub dgParty_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgParty.RowCommand
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
                Response.Redirect(String.Format("~/Sales/SalesPartyMaster.aspx?StatusID={0}&PID={1}", oStatusID, oMasterID), False) 'GeneralMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objSPD.UpdatePartyMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objSPD.UpdatePartyMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objSPD.UpdatePartyMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                BindSODetails(0, ddlStatus.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
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
            Response.Redirect(String.Format("~/Sales/SalesPartyMaster.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Try
            If ddlStatus.SelectedIndex = 1 Then
                objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, sSession.IPAddress)
                lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            End If
            BindSODetails(0, ddlStatus.SelectedIndex)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Try
            If ddlStatus.SelectedIndex = 0 Then
                objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, sSession.IPAddress)
                lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            End If
            BindSODetails(0, ddlStatus.SelectedIndex)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            If ddlStatus.SelectedIndex = 2 Then
                objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, sSession.IPAddress)
                lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            End If
            BindSODetails(0, ddlStatus.SelectedIndex)
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
                For iIndx = 0 To dgParty.Rows.Count - 1
                    chkField = dgParty.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgParty.Rows.Count - 1
                    chkField = dgParty.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub dgParty_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgParty.RowEditing

    End Sub
End Class
