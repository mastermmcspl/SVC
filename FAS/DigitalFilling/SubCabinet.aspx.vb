Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class DigitalFilling_SubCabinet
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_SubCabinet"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objclsFASGeneral As New clsFASGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Dim objclsSubCabinet As New clsSubCabinet
    Public dtSubCab As DataTable
    Private Shared iCBN_NODE As Integer = 0
    Private Shared iCabinetID As Integer = 0
    Private objclsModulePermission As New clsModulePermission
    Private Shared sPTAoD As String
    Private Shared sPTAP As String
    Private Shared sPTED As String

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
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                '//Preeti
                ' imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SC")
                imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False
                imgbtnDeActivate.Visible = False : sPTAoD = "NO" : sPTAP = "NO" : sPTED = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/DigitalFilingPermission.aspx", False) 'Permissions/DigitalFilingPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        imgbtnDeActivate.Visible = True
                        sPTAoD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sPTAP = "YES"
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sPTED = "YES"
                    End If
                End If

                imgbtnAdd.Attributes.Add("OnClick", "$('#myModal').modal('show');return false;")

                RFVSubCabName.ControlToValidate = "txtSubCabName" : RFVSubCabName.ErrorMessage = "Enter Sub-Cabinet Name."
                REVSubCabName.ErrorMessage = "Sub-Cabinet Name exceeded maximum size(max 100 characters)." : REVSubCabName.ValidationExpression = "^[\s\S]{0,100}$"
                REVSubCabNotes.ErrorMessage = "Sub-Cabinet Notes exceeded maximum size(max 255 characters)." : REVSubCabNotes.ValidationExpression = "^[\s\S]{0,255}$"
                BindStatus() : BindexistingCabinet()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("CabinetID") IsNot Nothing Then
                    iCabinetID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CabinetID")))
                    ddlCabinet.SelectedValue = iCabinetID
                    ddlCabinet_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("BackID") IsNot Nothing Then
                    imgbtnBack.Visible = True
                    objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("BackID")))
                End If

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Add(New ListItem("Activated", 0))
            ddlStatus.Items.Add(New ListItem("De-Activated", 1))
            ddlStatus.Items.Add(New ListItem("Waiting for Approval", 2))
            ddlStatus.Items.Add(New ListItem("All", 3))
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindexistingCabinet()
        Try
            ddlCabinet.DataSource = objclsSubCabinet.LoadCabinet(sSession.AccessCode, "")
            ddlCabinet.DataTextField = "CBN_NAME"
            ddlCabinet.DataValueField = "CBN_NODE"
            ddlCabinet.DataBind()
            ddlCabinet.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindSubCabinet(ByVal iCabinetID As Integer)
        Try
            lblError.Text = ""
            If iCabinetID > 0 Then
                dtSubCab = objclsSubCabinet.LoadSubCabGrid(sSession.AccessCode, 0, ddlStatus.SelectedIndex, iCabinetID)
                Session("dtSubCab") = dtSubCab
                dgSubCabinet.DataSource = dtSubCab
                dgSubCabinet.DataBind()
            ElseIf ddlCabinet.SelectedIndex > 0 Then
                dtSubCab = objclsSubCabinet.LoadSubCabGrid(sSession.AccessCode, 0, ddlStatus.SelectedIndex, ddlCabinet.SelectedValue)
                Session("dtSubCab") = dtSubCab
                dgSubCabinet.DataSource = dtSubCab
                dgSubCabinet.DataBind()
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
                For iIndx = 0 To dgSubCabinet.Rows.Count - 1
                    chkField = dgSubCabinet.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgSubCabinet.Rows.Count - 1
                    chkField = dgSubCabinet.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub dgSubCabinet_PreRender(sender As Object, e As EventArgs) Handles dgSubCabinet.PreRender
        Dim dt As New DataTable
        Try
            If dgSubCabinet.Rows.Count > 0 Then
                dgSubCabinet.UseAccessibleHeader = True
                dgSubCabinet.HeaderRow.TableSection = TableRowSection.TableHeader
                dgSubCabinet.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSubCabinet_PreRender")
        End Try
    End Sub
    Private Sub dgSubCabinet_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgSubCabinet.RowDataBound
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lblCBNID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                dgSubCabinet.Columns(0).Visible = True
                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgSubCabinet.Columns(6).Visible = True : dgSubCabinet.Columns(7).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgSubCabinet.Columns(6).Visible = True : dgSubCabinet.Columns(7).Visible = False
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgSubCabinet.Columns(6).Visible = True : dgSubCabinet.Columns(7).Visible = False
                End If
                If ddlStatus.SelectedIndex = 3 Then
                    dgSubCabinet.Columns(0).Visible = False : dgSubCabinet.Columns(6).Visible = False : dgSubCabinet.Columns(7).Visible = False
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSubCabinet_RowDataBound")
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            If ddlStatus.SelectedIndex = 0 And ddlCabinet.SelectedIndex > 0 Then
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 And ddlCabinet.SelectedIndex > 0 Then
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 And ddlCabinet.SelectedIndex > 0 Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
            End If

            If ddlCabinet.SelectedIndex > 0 Then
                BindSubCabinet(ddlCabinet.SelectedValue)
            Else
                BindSubCabinet(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtSubCab As New DataTable
        Dim DVSubCab As New DataView(dtSubCab)
        dtSubCab = Session("dtSubDep")
        Try
            lblError.Text = ""
            If dgSubCabinet.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to activate" : lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "Select to Activate." : lblError.Text = "Select to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgSubCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objclsSubCabinet.UpdateStatus(sSession.AccessCode, "A", lblCBN_NODE.Text, "A")

                    dtSubCab = DVSubCab.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            If ddlCabinet.SelectedIndex > 0 Then
                BindSubCabinet(ddlCabinet.SelectedValue)
            Else
                BindSubCabinet(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtSubCab As New DataTable
        Dim DVSubCab As New DataView(dtSubCab)
        dtSubCab = Session("dtSubCab")
        Try
            lblError.Text = ""
            If dgSubCabinet.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to deactivate" : lblError.Text = "No data to deactivate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "Select to Deactivate." : lblError.Text = "Select to Deactivate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgSubCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objclsSubCabinet.UpdateStatus(sSession.AccessCode, "D", lblCBN_NODE.Text, "D")
                    dtSubCab = DVSubCab.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Deactivated." : lblError.Text = "Successfully Deactivated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            If ddlCabinet.SelectedIndex > 0 Then
                BindSubCabinet(ddlCabinet.SelectedValue)
            Else
                BindSubCabinet(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtSubCab As New DataTable
        Dim DVSubCab As New DataView(dtSubCab)
        dtSubCab = Session("dtSubCab")
        Try
            lblError.Text = ""
            If dgSubCabinet.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to Approve" : lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "Select to Approve." : lblError.Text = "Select to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgSubCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objclsSubCabinet.UpdateStatus(sSession.AccessCode, "W", lblCBN_NODE.Text, "A")
                    dtSubCab = DVSubCab.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            If ddlCabinet.SelectedIndex > 0 Then
                BindSubCabinet(ddlCabinet.SelectedValue)
            Else
                BindSubCabinet(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub dgSubCabinet_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgSubCabinet.RowCommand
        Dim chkSelectAll As New CheckBox
        Dim lblCBN_NODE As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Dim oSubCabID As New Object, oCabID As New Object, oBackID As New Object
        Dim dt As New DataTable()
        Try
            lblError.Text = "" : sMainMaster = ""

            If e.CommandName.Equals("EditRow") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                btnDescSave.Visible = False : btnDescUpdate.Visible = True
                oSubCabID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblCBN_NODE.Text)))
                BindSubCabDetails(Val(lblCBN_NODE.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
            If e.CommandName = "Status" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                If ddlStatus.SelectedIndex = 0 Then
                    objclsSubCabinet.UpdateStatus(sSession.AccessCode, "D", lblCBN_NODE.Text, "D")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsSubCabinet.UpdateStatus(sSession.AccessCode, "A", lblCBN_NODE.Text, "A")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objclsSubCabinet.UpdateStatus(sSession.AccessCode, "W", lblCBN_NODE.Text, "A")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                If ddlCabinet.SelectedIndex > 0 Then
                    BindSubCabinet(ddlCabinet.SelectedValue)
                Else
                    BindSubCabinet(0)
                End If
            End If
            If e.CommandName = "SelectSubCabinet" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                oCabID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(iCabinetID))
                oSubCabID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblCBN_NODE.Text)))
                oBackID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(1))
                Response.Redirect(String.Format("~/DigitalFilling/Folders.aspx?CabinetID={0}&SubCabID={1}&BackID={2}", oCabID, oSubCabID, oBackID), False) 'DigitalFiling/Folder             
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCabinet_ItemCommand")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            If ddlCabinet.SelectedIndex > 0 Then
                lblError.Text = "" : btnDescSave.Visible = True
                txtSubCabName.Text = "" : txtSubCabNotes.Text = ""
            Else
                lblCabinetEmpMasterValidationMsg.Text = "Select Cabinet"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            lblError.Text = ""
            lblModelError.Text = "" : btnDescSave.Visible = True : btnDescUpdate.Visible = False
            txtSubCabName.Text = "" : txtSubCabNotes.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNew_Click")
        End Try
    End Sub
    Public Sub BindSubCabDetails(ByVal iSrNo As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsSubCabinet.LoadSubCabGrid(sSession.AccessCode, iSrNo, ddlStatus.SelectedIndex, ddlCabinet.SelectedValue)
            iCBN_NODE = dt.Rows(0)("CBN_NODE")
            txtSubCabName.Text = ""
            If IsDBNull(dt.Rows(0)("CBN_NAME")) = False Then
                txtSubCabName.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_NAME"))
            End If
            txtSubCabNotes.Text = ""
            If IsDBNull(dt.Rows(0)("CBN_Note")) = False Then
                txtSubCabNotes.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_Note"))
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Dim objclsTRACeKnowledge As New clsTRACeKnowledge
        Try
            BindSubCabinet(0)
            If ddlCabinet.SelectedIndex > 0 Then
                imgbtnAdd.Visible = True : imgbtnReport.Visible = True
                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnDeActivate.Visible = True 'Activate
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    imgbtnActivate.Visible = True 'De-Activate
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    imgbtnWaiting.Visible = True 'Waiting for Approval
                End If
                lblCabinetName.Text = ddlCabinet.SelectedItem.Text
                BindSubCabinet(ddlCabinet.SelectedValue)
            Else
                imgbtnAdd.Visible = False
            End If
            If dtSubCab.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No Data to Display" : lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click
        Dim Arr() As String
        Dim iRet As Integer
        Try
            lblModelError.Text = ""
            iRet = objclsSubCabinet.CheckSubCabName(sSession.AccessCode, objclsFASGeneral.SafeSQL(txtSubCabName.Text), ddlCabinet.SelectedValue, 0)
            If iRet = 0 Then
                If IsDBNull(txtSubCabName.Text) = False Then
                    objclsSubCabinet.sCBN_Name = objclsFASGeneral.SafeSQL(txtSubCabName.Text)
                Else
                    objclsSubCabinet.sCBN_Name = ""
                End If
                If IsDBNull(txtSubCabNotes.Text) = False Then
                    objclsSubCabinet.sCBN_Note = objclsFASGeneral.SafeSQL(txtSubCabNotes.Text)
                Else
                    objclsSubCabinet.sCBN_Note = ""
                End If
                objclsSubCabinet.iCBN_ParGrp = 0
                objclsSubCabinet.iCBN_NODE = 0
                objclsSubCabinet.sCBN_Delstatus = "W"
                objclsSubCabinet.iCBN_PARENT = ddlCabinet.SelectedValue
                objclsSubCabinet.iCBN_USERGROUP = "0"
                objclsSubCabinet.iCBN_PERMISSION = "0"
                objclsSubCabinet.iCBN_USERID = sSession.UserID
                objclsSubCabinet.sCBN_Operation = "X"
                objclsSubCabinet.iCBN_SCCount = "0"
                objclsSubCabinet.iCBN_FolCount = "0"
                Arr = objclsSubCabinet.SaveSubCabDetails(sSession.AccessCode, objclsSubCabinet)
                objclsSubCabinet.UpdateSubCabDetails(sSession.AccessCode, 0, ddlCabinet.SelectedValue)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Sub Cabinet", "Saved", ddlCabinet.SelectedValue, ddlCabinet.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblModelError.Text = "Successfully Saved and Waiting for Approval."
            Else
                lblModelError.Text = "Sub Cabinet Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtSubCabName.Focus()
                Exit Sub
            End If
            ddlStatus.SelectedIndex = 2
            If ddlCabinet.SelectedIndex > 0 Then
                BindSubCabinet(ddlCabinet.SelectedValue)
            Else
                BindSubCabinet(0)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescSave_Click")
        End Try
    End Sub
    Protected Sub btnDescUpdate_Click(sender As Object, e As EventArgs) Handles btnDescUpdate.Click
        Dim Arr() As String
        Dim iRet As Integer
        Try
            lblModelError.Text = ""
            iRet = objclsSubCabinet.CheckSubCabName(sSession.AccessCode, objclsFASGeneral.ReplaceSafeSQL(txtSubCabName.Text), ddlCabinet.SelectedValue, iCBN_NODE)
            If iRet = 0 Then
                If IsDBNull(txtSubCabName.Text) = False Then
                    objclsSubCabinet.sCBN_Name = objclsFASGeneral.SafeSQL(txtSubCabName.Text)
                Else
                    objclsSubCabinet.sCBN_Name = ""
                End If
                If IsDBNull(txtSubCabNotes.Text) = False Then
                    objclsSubCabinet.sCBN_Note = objclsFASGeneral.SafeSQL(txtSubCabNotes.Text)
                Else
                    objclsSubCabinet.sCBN_Note = ""
                End If
                objclsSubCabinet.iCBN_ParGrp = 0
                objclsSubCabinet.iCBN_NODE = iCBN_NODE
                objclsSubCabinet.iCBN_PARENT = ddlCabinet.SelectedValue
                objclsSubCabinet.sCBN_Delstatus = "A"
                objclsSubCabinet.iCBN_USERGROUP = "0"
                objclsSubCabinet.iCBN_PERMISSION = "0"
                objclsSubCabinet.iCBN_USERID = sSession.UserID
                objclsSubCabinet.sCBN_Operation = "X"
                Arr = objclsSubCabinet.SaveSubCabDetails(sSession.AccessCode, objclsSubCabinet)
                objclsSubCabinet.UpdateSubCabDetails(sSession.AccessCode, 0, ddlCabinet.SelectedValue)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Sub Cabinet", "Updated", ddlCabinet.SelectedValue, ddlCabinet.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblModelError.Text = "Successfully Updated."
            Else
                lblModelError.Text = "Sub Cabinet Name already exists."
                txtSubCabName.Focus()
            End If
            If ddlCabinet.SelectedIndex > 0 Then
                BindSubCabinet(ddlCabinet.SelectedValue)
            Else
                BindSubCabinet(0)
            End If
            btnDescSave.Visible = False : btnDescUpdate.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescUpdate_Click")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/DigitalFilling/Cabinet.aspx"), False) 'Cabinet     
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            ReportViewer1.Reset()
            dt = objclsSubCabinet.LoadSubCabGrid(sSession.AccessCode, 0, ddlStatus.SelectedIndex, ddlCabinet.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFiling/SubCabinet.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=SubCabinet" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            ReportViewer1.Reset()
            dt = objclsSubCabinet.LoadSubCabGrid(sSession.AccessCode, 0, ddlStatus.SelectedIndex, ddlCabinet.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFiling/SubCabinet.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=SubCabinet" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
End Class
