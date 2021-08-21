Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class DigitalFilling_Folders
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFilling_Folders"
    Private objclsGRACeGeneral As New clsFASGeneral
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objclsFASGeneral As New clsFASGeneral
    Dim objclsSubCabinet As New clsSubCabinet
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objclsFolders As New clsFolders
    Private Shared iFol_Id As Integer = 0
    Public dtFol As DataTable
    Private Shared iCabinetID As Integer = 0
    Private Shared iSubCabID As Integer = 0
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
                ' imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                '//Preeti             
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FL")
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
                RFVFolName.ControlToValidate = "txtFolName" : RFVFolName.ErrorMessage = "Enter Folder Name."
                REVFolName.ErrorMessage = "Folder Name exceeded maximum size(max 100 characters)." : REVFolName.ValidationExpression = "^[\s\S]{0,100}$"
                REVFolNotes.ErrorMessage = "Folder Notes exceeded maximum size(max 255 characters)." : REVFolNotes.ValidationExpression = "^[\s\S]{0,255}$"
                BindStatus() : BindexistingCabinet()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                ddlStatus_SelectedIndexChanged(sender, e)

                If Request.QueryString("CabinetID") IsNot Nothing Then
                    iCabinetID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CabinetID")))
                    ddlCabinet.SelectedValue = iCabinetID
                    ddlCabinet_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("SubCabID") IsNot Nothing Then
                    iSubCabID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SubCabID")))
                    ddlSubCabinet.SelectedValue = iSubCabID
                    ddlSubCabinet_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("BackID") IsNot Nothing Then
                    imgbtnBack.Visible = True
                    objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("BackID")))
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
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
            ddlCabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindexistingSubCab()
        Try
            ddlSubCabinet.DataSource = objclsFolders.LoadSubCab(sSession.AccessCode, ddlCabinet.SelectedValue)
            ddlSubCabinet.DataTextField = "CBN_NAME"
            ddlSubCabinet.DataValueField = "CBN_NODE"
            ddlSubCabinet.DataBind()
            ddlSubCabinet.Items.Insert(0, "Select Sub Cabinet")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Try
            lblError.Text = "" : ddlSubCabinet.Items.Clear()
            If ddlCabinet.SelectedIndex > 0 Then
                lblCabinetName.Text = ddlCabinet.SelectedItem.Text
                BindexistingSubCab()
                If ddlSubCabinet.SelectedIndex > 0 Then
                    BindFolders(0, ddlStatus.SelectedIndex, ddlSubCabinet.SelectedValue)
                Else
                    BindFolders(0, ddlStatus.SelectedIndex, 0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlSubCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubCabinet.SelectedIndexChanged
        Dim objclsTRACeKnowledge As New clsTRACeKnowledge
        Try
            lblError.Text = ""
            If ddlSubCabinet.SelectedIndex > 0 Then
                lblSubCabinetName.Text = ddlSubCabinet.SelectedItem.Text
                imgbtnAdd.Visible = True : imgbtnReport.Visible = True
                BindFolders(0, ddlStatus.SelectedIndex, ddlSubCabinet.SelectedValue)
            Else
                imgbtnAdd.Visible = False
                BindFolders(0, ddlStatus.SelectedIndex, 0)
            End If
            If ddlStatus.SelectedIndex = 0 Then
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
            End If
            If dtFol.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No Data to Display" : lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubCabinet_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindFolders(ByVal iPageIndex As Integer, ByVal iStatus As Integer, ByVal iSubCabId As Integer)
        Try
            dtFol = objclsFolders.LoadFolders(sSession.AccessCode, iStatus, iSubCabId)
            Session("dtFol") = dtFol
            dgFolders.DataSource = dtFol
            dgFolders.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindFolders")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgFolders.Rows.Count - 1
                    chkField = dgFolders.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgFolders.Rows.Count - 1
                    chkField = dgFolders.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub dgFolders_PreRender(sender As Object, e As EventArgs) Handles dgFolders.PreRender
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If dgFolders.Rows.Count > 0 Then
                dgFolders.UseAccessibleHeader = True
                dgFolders.HeaderRow.TableSection = TableRowSection.TableHeader
                dgFolders.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgFolders_PreRender")
        End Try
    End Sub
    Private Sub dgFolders_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgFolders.RowDataBound
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lnkDocuments As New LinkButton
        Dim lblDocumentsID As New Label
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                lblDocumentsID = CType(e.Row.FindControl("lblDocumentsID"), Label)
                lnkDocuments = CType(e.Row.FindControl("lnkDocumentsID"), LinkButton)
                If lblDocumentsID.Text = "0" Then
                    lblDocumentsID.Visible = True
                    lnkDocuments.Visible = False
                Else
                    lnkDocuments.Visible = True
                End If
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                dgFolders.Columns(0).Visible = True
                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgFolders.Columns(7).Visible = True : dgFolders.Columns(8).Visible = True
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgFolders.Columns(7).Visible = True : dgFolders.Columns(8).Visible = False
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgFolders.Columns(7).Visible = True : dgFolders.Columns(8).Visible = False
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgFolders.Columns(0).Visible = False : dgFolders.Columns(7).Visible = False : dgFolders.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgFolders_ItemDataBound")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblFOL_FOLID As New Label
        Dim dtFolder As New DataTable
        Dim DVFolder As New DataView(dtFolder)
        dtFolder = Session("dtFld")
        Try
            lblError.Text = ""
            If dgFolders.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to activate" : lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
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

NextSave:   For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                lblFOL_FOLID = dgFolders.Rows(i).FindControl("lblFOL_FOLID")
                If chkSelect.Checked = True Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, "A", lblFOL_FOLID.Text, "A")

                    dtFolder = DVFolder.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            BindFolders(0, ddlStatus.SelectedIndex, 0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblFOL_FOLID As New Label
        Dim dtFolder As New DataTable
        Dim DVFolder As New DataView(dtFolder)
        dtFolder = Session("dtFld")
        Try
            lblError.Text = ""
            If dgFolders.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to deactivate" : lblError.Text = "No data to deactivate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
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
NextSave:   For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                lblFOL_FOLID = dgFolders.Rows(i).FindControl("lblFOL_FOLID")
                If chkSelect.Checked = True Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, "D", lblFOL_FOLID.Text, "D")
                    dtFolder = DVFolder.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Deactivated." : lblError.Text = "Successfully Deactivated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            BindFolders(0, ddlStatus.SelectedIndex, 0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblFOL_FOLID As New Label
        Dim dtFolder As New DataTable
        Dim DVFolder As New DataView(dtFolder)
        dtFolder = Session("dtFld")
        Try
            lblError.Text = ""
            If dgFolders.Rows.Count = 0 Then
                lblCabinetEmpMasterValidationMsg.Text = "No data to Approve" : lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
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
NextSave:   For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                lblFOL_FOLID = dgFolders.Rows(i).FindControl("lblFOL_FOLID")
                If chkSelect.Checked = True Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, "W", lblFOL_FOLID.Text, "A")
                    dtFolder = DVFolder.ToTable
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            BindFolders(0, ddlStatus.SelectedIndex, 0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub dgFolders_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgFolders.RowCommand
        Dim chkSelectAll As New CheckBox
        Dim lblFOL_FOLID As New Label, lblDescName As New Label, lblPGE_CABINET As New Label, lblPGE_SubCABINET As New Label, lblPGE_FOLDER As New Label
        Dim sMainMaster As String
        Dim oDescID As New Object
        Dim dt As New DataTable()
        Dim oPGE_CABINET As New Object, oPGE_SubCABINET As New Object, oPGE_FOLDER As New Object

        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Try
            lblError.Text = "" : sMainMaster = ""

            If e.CommandName = "EditRow" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblFOL_FOLID = DirectCast(clickedRow.FindControl("lblFOL_FOLID"), Label)
                btnDescSave.Visible = False : btnDescUpdate.Visible = True
                oDescID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblFOL_FOLID.Text)))
                BindFolderDetails(Val(lblFOL_FOLID.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
            If e.CommandName = "Status" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblFOL_FOLID = DirectCast(clickedRow.FindControl("lblFOL_FOLID"), Label)
                If ddlStatus.SelectedIndex = 0 Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, "D", lblFOL_FOLID.Text, "D")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, "A", lblFOL_FOLID.Text, "A")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, "W", lblFOL_FOLID.Text, "A")
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                BindFolders(0, ddlStatus.SelectedIndex, 0)
            End If
            If e.CommandName = "Document" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblPGE_CABINET = DirectCast(clickedRow.FindControl("lblPGE_CABINET"), Label)
                lblPGE_FOLDER = DirectCast(clickedRow.FindControl("lblPGE_FOLDER"), Label)

                dt = objclsFolders.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCabinet.SelectedValue, lblPGE_CABINET.Text, lblPGE_FOLDER.Text)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sSelectedChecksIDs = sSelectedChecksIDs & "," & dt.Rows(i)("PGE_BASENAME")
                    Next
                End If

                If sSelectedChecksIDs.StartsWith(",") Then
                    sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                End If
                If sSelectedChecksIDs.EndsWith(",") Then
                    sSelectedChecksIDs = sSelectedChecksIDs.Remove(Len(sSelectedChecksIDs) - 1, 1)
                End If

                oSelectedCabID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(ddlCabinet.SelectedValue))
                oSelectedSubCabID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(lblPGE_CABINET.Text))
                oSelectedFolID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(lblPGE_FOLDER.Text))
                oSelectedChecksIDs = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedChecksIDs))
                oSelectedIndexID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(0))

                Response.Redirect(String.Format("~/Viewer/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={7}&SelectedDescID={8}&SelectedFrmtID={9}&SelectedCrByID={10}&SelectedIndexID={11}", "", "", oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, "", "", "", "", "", oSelectedIndexID), False)

                'oPGE_CABINET = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblPGE_CABINET.Text)))
                'oPGE_SubCABINET = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(0)))
                'oPGE_FOLDER = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblPGE_FOLDER.Text)))
                'Response.Redirect(String.Format("~/Search/Search.aspx?PGE_CABINET={0}&PGE_SUBCABINET={1}&PGE_FOLDER={2}", oPGE_CABINET, oPGE_SubCABINET, oPGE_FOLDER, False))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgFolders_ItemCommand")
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
            End If
            If ddlSubCabinet.SelectedIndex > 0 Then
                BindFolders(0, ddlStatus.SelectedIndex, ddlSubCabinet.SelectedValue)
            Else
                BindFolders(0, ddlStatus.SelectedIndex, 0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            lblError.Text = "" : lblModelError.Text = "" : btnDescSave.Visible = True
            txtFolName.Text = "" : txtFolNotes.Text = "" : btnDescUpdate.Visible = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNew_Click")
        End Try
    End Sub
    Public Sub BindFolderDetails(ByVal iSrNo As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsFolders.LoadFolderDetails(sSession.AccessCode, iSrNo)
            iFol_Id = dt.Rows(0)("Fol_FolId")
            txtFolName.Text = ""
            If IsDBNull(dt.Rows(0)("Fol_NAME")) = False Then
                txtFolName.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0)("Fol_NAME"))
            End If
            txtFolNotes.Text = ""
            If IsDBNull(dt.Rows(0)("Fol_Notes")) = False Then
                txtFolNotes.Text = objclsFASGeneral.ReplaceSafeSQL(dt.Rows(0)("Fol_Notes"))
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click
        Dim Arr() As String
        Dim iRet As Integer
        Try
            lblModelError.Text = "" : lblError.Text = ""
            If ddlCabinet.SelectedIndex = 0 Then
                lblError.Text = "Select Cabinet."
                Exit Sub
            End If
            If ddlSubCabinet.SelectedIndex = 0 Then
                lblError.Text = "Select Sub Cabinet."
                Exit Sub
            End If

            iRet = objclsFolders.CheckFoldersName(sSession.AccessCode, objclsFASGeneral.SafeSQL(txtFolName.Text), ddlSubCabinet.SelectedValue, iFol_Id)
            If iRet = 0 Then
                If IsDBNull(txtFolName.Text) = False Then
                    objclsFolders.sFol_Name = objclsFASGeneral.SafeSQL(txtFolName.Text)
                Else
                    objclsFolders.sFol_Name = ""
                End If
                If IsDBNull(txtFolNotes.Text) = False Then
                    objclsFolders.sFol_Notes = objclsFASGeneral.SafeSQL(txtFolNotes.Text)
                Else
                    objclsFolders.sFol_Notes = ""
                End If
                objclsFolders.iFol_Id = 0
                objclsFolders.iFol_Cab = ddlSubCabinet.SelectedValue
                objclsFolders.sFol_Status = "W"
                objclsFolders.iFol_Crby = sSession.UserID
                objclsFolders.iFol_Pagecount = 0
                objclsFolders.sFol_Operation = "I"
                objclsFolders.iFol_OppBy = sSession.UserID
                Arr = objclsFolders.SaveFolderDetails(sSession.AccessCode, objclsFolders)
                objclsFolders.UpdateFolderCount(sSession.AccessCode, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Folder", "Saved", ddlSubCabinet.SelectedValue, ddlSubCabinet.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblModelError.Text = "Successfully Saved and Waiting for Approval."
            Else
                lblModelError.Text = "Folder Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtFolName.Focus()
                Exit Sub
            End If
            ddlStatus.SelectedIndex = 2
            BindFolders(0, ddlStatus.SelectedIndex, ddlSubCabinet.SelectedValue)
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
            lblModelError.Text = "" : lblError.Text = ""
            If ddlCabinet.SelectedIndex > 0 Then
                lblError.Text = "Select Cabinet."
                Exit Sub
            End If
            If ddlSubCabinet.SelectedIndex > 0 Then
                lblError.Text = "Select Sub Cabinet."
                Exit Sub
            End If
            iRet = objclsFolders.CheckFoldersName(sSession.AccessCode, objclsFASGeneral.SafeSQL(txtFolName.Text), ddlSubCabinet.SelectedValue, iFol_Id)
            If iRet = 0 Then
                If IsDBNull(txtFolName.Text) = False Then
                    objclsFolders.sFol_Name = objclsFASGeneral.SafeSQL(txtFolName.Text)
                Else
                    objclsFolders.sFol_Name = ""
                End If
                If IsDBNull(txtFolNotes.Text) = False Then
                    objclsFolders.sFol_Notes = objclsFASGeneral.SafeSQL(txtFolNotes.Text)
                Else
                    objclsFolders.sFol_Notes = ""
                End If
                objclsFolders.iFol_Id = iFol_Id
                objclsFolders.iFol_Cab = ddlSubCabinet.SelectedValue
                objclsFolders.sFol_Status = "A"
                objclsFolders.iFol_Crby = sSession.UserID
                objclsFolders.iFol_Pagecount = 0
                objclsFolders.sFol_Operation = "U"
                objclsFolders.iFol_OppBy = sSession.UserID
                objclsFolders.UpdateFolderCount(sSession.AccessCode, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue)
                Arr = objclsFolders.SaveFolderDetails(sSession.AccessCode, objclsFolders)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Folder", "Updated", ddlSubCabinet.SelectedValue, ddlSubCabinet.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblModelError.Text = "Successfully Updated."
            Else
                lblModelError.Text = "Folder Name already exists."
                txtFolName.Focus()
            End If
            BindFolders(0, ddlStatus.SelectedIndex, ddlSubCabinet.SelectedValue)
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
            dt = objclsFolders.LoadFolders(sSession.AccessCode, ddlStatus.SelectedIndex, ddlSubCabinet.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFiling/Folders.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Folders" + ".xls")
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
            dt = objclsFolders.LoadFolders(sSession.AccessCode, ddlStatus.SelectedIndex, ddlSubCabinet.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFiling/Folders.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Folders" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
End Class
