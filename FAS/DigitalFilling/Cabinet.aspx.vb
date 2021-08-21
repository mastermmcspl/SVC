Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class DigitalFilling_Cabinet
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_Cabinet"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objclsFASGeneral As New clsFASGeneral
    Dim objclsTRACeKnowledge As New clsTRACeKnowledge
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Dim objclsCabinet As New clsCabinet
    Public dtCab As DataTable
    Private Shared iCBN_NODE As Integer = 0
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

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                '//preeti
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "CB")
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

                ' imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False

                imgbtnAdd.Attributes.Add("OnClick", "$('#myModal').modal('show');return false;")
                BindStatus() : BindCabinet()
                RFVCabName.ControlToValidate = "txtCabName" : RFVCabName.ErrorMessage = "Enter Cabinet Name."
                REVCabName.ErrorMessage = "Cabinet Name exceeded maximum size(max 100 characters)." : REVCabName.ValidationExpression = "^[\s\S]{0,100}$"
                REVCabNotes.ErrorMessage = "Cabinet Notes exceeded maximum size(max 255 characters)." : REVCabNotes.ValidationExpression = "^[\s\S]{0,255}$"

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If

                ddlStatus_SelectedIndexChanged(sender, e)
                imgbtnAdd.Visible = True : imgbtnReport.Visible = True
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
    Public Sub BindCabinet()
        Try
            dtCab = objclsCabinet.LoadCabinetGrid(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            Session("dtCab") = dtCab
            dgCabinet.DataSource = dtCab
            dgCabinet.DataBind()
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
                For iIndx = 0 To dgCabinet.Rows.Count - 1
                    chkField = dgCabinet.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgCabinet.Rows.Count - 1
                    chkField = dgCabinet.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub dgCabinet_PreRender(sender As Object, e As EventArgs) Handles dgCabinet.PreRender
        Dim dt As New DataTable
        Try
            If dgCabinet.Rows.Count > 0 Then
                dgCabinet.UseAccessibleHeader = True
                dgCabinet.HeaderRow.TableSection = TableRowSection.TableHeader
                dgCabinet.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCabinet_PreRender")
        End Try
    End Sub
    Private Sub dgCabinet_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgCabinet.RowCommand
        Dim chkSelectAll As New CheckBox
        Dim lblCBN_NODE As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Dim oDescID As New Object, oCBN_NODE As New Object, oBackID As New Object
        Dim dt As New DataTable()

        Try
            lblError.Text = "" : sMainMaster = ""
            If e.CommandName.Equals("EditRow") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)

                btnDescSave.Visible = False : btnDescUpdate.Visible = True
                oDescID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblCBN_NODE.Text)))
                BindCabDetails(Val(lblCBN_NODE.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
            If e.CommandName = "Status" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                If ddlStatus.SelectedIndex = 0 Then
                    objclsCabinet.UpdateStatus(sSession.AccessCode, "D", lblCBN_NODE.Text, "D")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsCabinet.UpdateStatus(sSession.AccessCode, "A", lblCBN_NODE.Text, "A")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objclsCabinet.UpdateStatus(sSession.AccessCode, "W", lblCBN_NODE.Text, "A")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                BindCabinet()
            End If
            If e.CommandName = "SelectCabinet" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                oCBN_NODE = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblCBN_NODE.Text)))
                oBackID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(1))
                Response.Redirect(String.Format("~/DigitalFilling/SubCabinet.aspx?CabinetID={0}&BackID={1}", oCBN_NODE, oBackID), False) 'DigitalFiling/SubCabinet               
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCabinet_ItemCommand")
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            lblModelError.Text = "" : btnDescSave.Visible = True : btnDescUpdate.Visible = False
            txtCabName.Text = "" : txtCabNotes.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNew_Click")
        End Try
    End Sub
    Private Sub dgCabinet_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgCabinet.RowDataBound
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lblCBNID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                dgCabinet.Columns(0).Visible = True
                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgCabinet.Columns(7).Visible = True : dgCabinet.Columns(8).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgCabinet.Columns(7).Visible = True : dgCabinet.Columns(8).Visible = False
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgCabinet.Columns(7).Visible = True : dgCabinet.Columns(8).Visible = False
                End If
                If ddlStatus.SelectedIndex = 3 Then
                    dgCabinet.Columns(0).Visible = False : dgCabinet.Columns(7).Visible = False : dgCabinet.Columns(8).Visible = False
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCabinet_RowDataBound")
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False : imgbtnAdd.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                imgbtnAdd.Visible = True : imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnAdd.Visible = True : imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnAdd.Visible = True : imgbtnWaiting.Visible = True 'Waiting for Approval
            End If
            BindCabinet()
            If dtCab.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No Data to Display" : lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindCabDetails(ByVal iSrNo As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsCabinet.LoadCabinetGrid(sSession.AccessCode, iSrNo, ddlStatus.SelectedIndex)

            iCBN_NODE = dt.Rows(0)("CBN_NODE")
            txtCabName.Text = ""
            If IsDBNull(dt.Rows(0)("CBN_NAME")) = False Then
                txtCabName.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_NAME"))
            End If
            txtCabNotes.Text = ""
            If IsDBNull(dt.Rows(0)("CBN_Note")) = False Then
                txtCabNotes.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_Note"))
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtDepartment As New DataTable
        Dim DVDepartment As New DataView(dtDepartment)
        dtDepartment = Session("dtDep")
        Try
            lblError.Text = ""
            If dgCabinet.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to activate" : lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
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

NextSave:   For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objclsCabinet.UpdateStatus(sSession.AccessCode, "A", lblCBN_NODE.Text, "A")

                    dtCab = DVDepartment.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            BindCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtCabinet As New DataTable
        Dim DVDepartment As New DataView(dtCabinet)
        dtCabinet = Session("dtCab")
        Try
            lblError.Text = ""
            If dgCabinet.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to deactivate" : lblError.Text = "No data to deactivate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
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
NextSave:   For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objclsCabinet.UpdateStatus(sSession.AccessCode, "D", lblCBN_NODE.Text, "D")
                    dtCab = DVDepartment.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Deactivated." : lblError.Text = "Successfully Deactivated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            BindCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtCabinet As New DataTable
        Dim DVDepartment As New DataView(dtCabinet)
        dtCabinet = Session("dtCab")
        Try
            lblError.Text = ""
            If dgCabinet.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to Approve" : lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
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
NextSave:   For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objclsCabinet.UpdateStatus(sSession.AccessCode, "W", lblCBN_NODE.Text, "A")
                    dtCab = DVDepartment.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            BindCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Protected Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click
        Dim Arr() As String
        Dim iRet As Integer
        Try
            lblModelError.Text = ""
            iRet = objclsCabinet.CheckCabName(sSession.AccessCode, objclsFASGeneral.SafeSQL(txtCabName.Text), 0)

            If iRet = 0 Then

                If IsDBNull(txtCabName.Text) = False Then
                    objclsCabinet.sCBN_Name = objclsFASGeneral.SafeSQL(txtCabName.Text)
                Else
                    objclsCabinet.sCBN_Name = ""
                End If

                If IsDBNull(txtCabNotes.Text) = False Then
                    objclsCabinet.sCBN_Note = objclsFASGeneral.SafeSQL(txtCabNotes.Text)
                Else
                    objclsCabinet.sCBN_Note = ""
                End If
                objclsCabinet.iCBN_ParGrp = 0
                objclsCabinet.iCBN_NODE = 0
                objclsCabinet.sCBN_Delstatus = "W"
                objclsCabinet.iCBN_PARENT = "-1"
                objclsCabinet.iCBN_USERGROUP = "0"
                objclsCabinet.iCBN_PERMISSION = "0"
                objclsCabinet.iCBN_USERID = sSession.UserID
                objclsCabinet.sCBN_Operation = "X"
                objclsCabinet.iCBN_SCCount = "0"
                objclsCabinet.iCBN_FolCount = "0"
                Arr = objclsCabinet.SaveCabDetails(sSession.AccessCode, objclsCabinet)
                lblModelError.Text = "Successfully Saved and Waiting for Approval."

            Else
                lblModelError.Text = "Cabinet Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtCabName.Focus()
                Exit Sub
            End If
            ddlStatus.SelectedIndex = 2
            BindCabinet()
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Cabinet", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescSave_Click")
        End Try
    End Sub
    Protected Sub btnDescUpdate_Click(sender As Object, e As EventArgs) Handles btnDescUpdate.Click
        Dim Arr() As String
        Dim iRet As Integer
        Dim sPermission As String = ""
        Dim iCreate As Integer = 0, iModify As Integer = 0
        Try
            lblModelError.Text = ""
            iRet = objclsCabinet.CheckCabName(sSession.AccessCode, objclsFASGeneral.ReplaceSafeSQL(txtCabName.Text), iCBN_NODE)

            If iRet = 0 Then

                If IsDBNull(txtCabName.Text) = False Then
                    objclsCabinet.sCBN_Name = objclsFASGeneral.SafeSQL(txtCabName.Text)
                Else
                    objclsCabinet.sCBN_Name = ""
                End If

                If IsDBNull(txtCabNotes.Text) = False Then
                    objclsCabinet.sCBN_Note = objclsFASGeneral.SafeSQL(txtCabNotes.Text)
                Else
                    objclsCabinet.sCBN_Note = ""
                End If
                objclsCabinet.iCBN_ParGrp = 0
                objclsCabinet.iCBN_NODE = iCBN_NODE
                objclsCabinet.sCBN_Delstatus = "A"
                objclsCabinet.iCBN_PARENT = "-1"
                objclsCabinet.iCBN_USERGROUP = "0"
                objclsCabinet.iCBN_PERMISSION = "0"
                objclsCabinet.iCBN_USERID = sSession.UserID
                objclsCabinet.sCBN_Operation = "X"
                Arr = objclsCabinet.SaveCabDetails(sSession.AccessCode, objclsCabinet)
                objclsCabinet.UpdateCabDetails(sSession.AccessCode, 0, iCBN_NODE)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Cabinet", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                lblModelError.Text = "Successfully Updated."
            Else
                lblModelError.Text = "Cabinet Name already exists."
                txtCabName.Focus()
            End If
            BindCabinet()
            btnDescSave.Visible = False : btnDescUpdate.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescUpdate_Click")
        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            ReportViewer1.Reset()
            dt = objclsCabinet.LoadCabinetGrid(sSession.AccessCode, 0, 0)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFiling/Cabinet.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Cabinet" + ".xls")
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
            dt = objclsCabinet.LoadCabinetGrid(sSession.AccessCode, 0, 0)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFiling/Cabinet.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Cabinet" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub


End Class
