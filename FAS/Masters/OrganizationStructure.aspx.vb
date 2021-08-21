Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class Masters_OrganizationStructure
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_OrganizationStructure"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsFASGeneral
    Private objclsOrgStructure As New clsOrgStructure
    Private objclsCheckMasterIsInUse As New clsCheckMasterIsInUse
    Private objclsGRACePermission As New clsFASPermission

    Private Shared sSession As AllSession
    Private Shared sOrgSave As String
    Private Shared sOrgAD As String
    Private objclsModulePermission As New clsModulePermission
    Private Shared sOrgFlag As String
    Private Shared dtOrgStructureReport As DataTable
    Private Shared iIsInBranchLevel As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "ORGS")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                imgbtnWaiting.Visible = False : imgbtnReport.Visible = False
                sOrgSave = "NO" : sOrgAD = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        imgbtnUpdate.Visible = True
                        sOrgSave = "YES"
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        imgbtnActivate.Visible = True
                        imgbtnDeActivate.Visible = True
                        sOrgAD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnWaiting.Visible = True
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If
                'imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                'imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
                'lblSalesUnitCode.Visible = False : txtSalesUnitCode.Visible = False : lblBranchCode.Visible = False : txtBranchCode.Visible = False
                'sOrgSave = "NO" : sOrgAD = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasOS", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnAdd.Visible = True : imgbtnSave.Visible = True
                '        sOrgSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Activate/De-Activate,") = True Then
                '        sOrgAD = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If

                txtParentID.Text = 1 : txtCurrentID.Text = 1 : txtDepthID.Text = 0 : iIsInBranchLevel = 0
                BindSearch()

                'Name
                RFVName.ValidationGroup = True
                RFVName.ValidationGroup = "Validate" : RFVName.ControlToValidate = "txtName" : RFVName.ErrorMessage = "Enter Name."
                REVName.ValidationGroup = "Validate" : REVName.ControlToValidate = "txtName"
                REVName.ValidationExpression = "^(.{0,1000})$" : REVName.ErrorMessage = "Name exceeded maximum size(max 1000 character)."

                'Note
                REVNote.ValidationGroup = True
                REVNote.ValidationGroup = "Validate" : REVNote.ControlToValidate = "txtNote"
                REVNote.ValidationExpression = "^(.{0,2000})$" : REVNote.ErrorMessage = "Note exceeded maximum size(max 2000 character)."
                'IRDA Code
                RFVIRDACode.ValidationGroup = True
                RFVIRDACode.ValidationGroup = "Validate" : RFVIRDACode.ControlToValidate = "txtIRDACode" : RFVIRDACode.ErrorMessage = "Enter Org Code."
                REVIRDACode.ValidationGroup = "Validate" : REVIRDACode.ControlToValidate = "txtIRDACode"
                REVIRDACode.ValidationExpression = "^(.{0,25})$" : REVIRDACode.ErrorMessage = "Org Code exceeded maximum size(max 25 character)."
                'Sales Unit Code
                RFVSalesUnitCode.ValidationGroup = False
                RFVSalesUnitCode.ValidationGroup = "Validate" : RFVSalesUnitCode.ControlToValidate = "txtSalesUnitCode" : RFVSalesUnitCode.ErrorMessage = "Enter Sales Unit Code."
                REVSalesUnitCode.ValidationGroup = "Validate" : REVSalesUnitCode.ControlToValidate = "txtSalesUnitCode"
                REVSalesUnitCode.ValidationExpression = "^(.{0,25})$" : REVSalesUnitCode.ErrorMessage = "Sales Unit Code exceeded maximum size(max 25 character)."
                'Branch Code
                RFVBranchCode.ValidationGroup = False
                RFVBranchCode.ValidationGroup = "Validate" : RFVBranchCode.ControlToValidate = "txtBranchCode" : RFVBranchCode.ErrorMessage = "Enter Branch Code."
                REVBranchCode.ValidationGroup = "Validate" : REVBranchCode.ControlToValidate = "txtBranchCode"
                REVBranchCode.ValidationExpression = "^(.{0,25})$" : REVBranchCode.ErrorMessage = "Branch Code exceeded maximum size(max 25 character)."
                LoadTreeView()
                LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindSearch()
        Try
            ddlSearch.Items.Insert(0, "Select")
            ddlSearch.Items.Insert(1, "IRDA Code")
            ddlSearch.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            txtIRDACode.Enabled = True : txtIRDACode.Text = ""
            txtName.Enabled = True : txtName.Text = "" : txtNote.Text = "" : lblCurrentStatus.Text = ""
            txtCurrentID.Text = 0 : txtSaveOrUpdate.Text = 0
            lblName.Text = "* " & txtOrgStrNextLvlName.Text & " Name"
            RFVName.ErrorMessage = "Enter " & txtOrgStrNextLvlName.Text & " Name."
            REVName.ErrorMessage = txtOrgStrNextLvlName.Text & " Name exceeded maximum size(max 1000 character)."
            If sOrgSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False
            imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
            RFVSalesUnitCode.ValidationGroup = False : RFVBranchCode.ValidationGroup = False
            RFVSalesUnitCode.ValidationGroup = "Validate" : RFVBranchCode.ValidationGroup = "Validate"
            RFVSalesUnitCode.ErrorMessage = "" : RFVBranchCode.ErrorMessage = ""
            If objclsOrgStructure.CheckOrgLevel(sSession.AccessCode, sSession.AccessCodeID, Val(txtDepthID.Text) + 2) = True Then
                iIsInBranchLevel = 1
                RFVSalesUnitCode.ValidationGroup = True : RFVBranchCode.ValidationGroup = True
                RFVSalesUnitCode.ValidationGroup = "Validate" : RFVBranchCode.ValidationGroup = "Validate"
                RFVSalesUnitCode.ErrorMessage = "Enter Sales Unit Code." : RFVBranchCode.ErrorMessage = "Enter Branch Code."
                lblSalesUnitCode.Visible = True : txtSalesUnitCode.Visible = True
                lblBranchCode.Visible = True : txtBranchCode.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub LoadOrgStructureDetails(ByVal iParentID As Integer, ByVal iCurrentID As Integer, ByVal iDepthID As Integer)
        Dim sPath As String = ""
        Dim i As Integer
        Try
            txtSaveOrUpdate.Text = 0 : iIsInBranchLevel = 0
            If iDepthID > 0 Then
                txtSaveOrUpdate.Text = 1
            End If
            txtIRDACode.Enabled = True
            txtOrgStrNextLvlName.Text = objclsOrgStructure.GetOrgStructureLevels(sSession.AccessCode, sSession.AccessCodeID, iDepthID)
            If txtOrgStrNextLvlName.Text <> "" Then
                imgbtnAdd.ToolTip = "Create " & txtOrgStrNextLvlName.Text
            End If

            txtOrgStrCurrentLvlName.Text = objclsOrgStructure.GetOrgStructureLevels(sSession.AccessCode, sSession.AccessCodeID, iDepthID - 1)
            If txtOrgStrCurrentLvlName.Text <> "" Then
                lblName.Text = "* " & txtOrgStrCurrentLvlName.Text & " Name"
                imgbtnSearch.ToolTip = "New "
            End If
            'LoadSelected Org Details
            objclsOrgStructure = objclsOrgStructure.GetOrgStructureDetails(sSession.AccessCode, sSession.AccessCodeID, iCurrentID)
            txtIRDACode.Text = objclsGRACeGeneral.ReplaceSafeSQL(objclsOrgStructure.sOrgIRDAcode)
            txtIRDACode.Enabled = False
            txtName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objclsOrgStructure.sOrgName)
            If iDepthID = 0 Then
                txtName.Enabled = False
                txtIRDACode.Enabled = False
            Else
                txtName.Enabled = True
                txtIRDACode.Enabled = True
            End If
            txtNote.Text = objclsGRACeGeneral.ReplaceSafeSQL(objclsOrgStructure.sOrgNote)
            txtIRDACode.Text = objclsGRACeGeneral.ReplaceSafeSQL(objclsOrgStructure.sOrgIRDAcode)
            txtName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objclsOrgStructure.sOrgName)
            txtNote.Text = objclsGRACeGeneral.ReplaceSafeSQL(objclsOrgStructure.sOrgNote)
            txtSalesUnitCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(objclsOrgStructure.sOrgSalesUnitCode)
            txtBranchCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(objclsOrgStructure.sOrgBranchCode)
            If IsDBNull(objclsOrgStructure.sOrgDelflag) = False Then
                sOrgFlag = objclsOrgStructure.sOrgDelflag
                If objclsOrgStructure.sOrgDelflag = "W" Then
                    lblCurrentStatus.Text = "Waiting for Approval"
                    imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnAdd.Visible = False : imgbtnSave.Visible = False
                    If sOrgAD = "YES" Then
                        imgbtnWaiting.Visible = True
                    End If
                    If sOrgSave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If
                ElseIf objclsOrgStructure.sOrgDelflag = "D" Then
                    lblCurrentStatus.Text = "De-Activated"
                    imgbtnAdd.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                    If sOrgAD = "YES" Then
                        imgbtnActivate.Visible = True
                    End If
                Else
                    lblCurrentStatus.Text = "Activated"
                    imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False : imgbtnSave.Visible = False
                    If sOrgSave = "YES" Then
                        imgbtnAdd.Visible = True
                    End If
                    If sOrgAD = "YES" Then
                        imgbtnDeActivate.Visible = True
                    End If
                    If sOrgSave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If
                End If
            End If
            Dim iPathParentID As Integer = txtParentID.Text
            If Val(txtDepthID.Text) > 0 Then
                For i = 0 To txtDepthID.Text
                    objclsOrgStructure = objclsOrgStructure.LoadParent(sSession.AccessCode, sSession.AccessCodeID, iPathParentID)
                    If objclsOrgStructure.iOrgParent <> 0 Or objclsOrgStructure.sOrgName <> "" Then
                        iPathParentID = objclsOrgStructure.iOrgParent
                        sPath = objclsOrgStructure.sOrgName & "/" & sPath
                    End If
                Next
            Else
                sPath = txtName.Text
            End If
            If sPath.EndsWith("/") = True Then
                sPath = sPath.Remove(Len(sPath) - 1, 1)
            End If
            lblPath.Text = sPath
            RFVSalesUnitCode.ValidationGroup = False : RFVBranchCode.ValidationGroup = False
            RFVSalesUnitCode.ValidationGroup = "Validate" : RFVBranchCode.ValidationGroup = "Validate"
            RFVSalesUnitCode.ErrorMessage = "" : RFVBranchCode.ErrorMessage = ""
            lblSalesUnitCode.Visible = False : txtSalesUnitCode.Visible = False
            lblBranchCode.Visible = False : txtBranchCode.Visible = False
            If objclsOrgStructure.CheckOrgLevel(sSession.AccessCode, sSession.AccessCodeID, iDepthID + 1) = True Then
                iIsInBranchLevel = 1
                imgbtnAdd.Visible = False
                lblSalesUnitCode.Visible = True : txtSalesUnitCode.Visible = True : lblBranchCode.Visible = True : txtBranchCode.Visible = True
                'Sales Unit Code
                RFVSalesUnitCode.ValidationGroup = True
                RFVSalesUnitCode.ValidationGroup = "Validate" : RFVSalesUnitCode.ControlToValidate = "txtSalesUnitCode" : RFVSalesUnitCode.ErrorMessage = "Enter Sales Unit Code."
                REVSalesUnitCode.ValidationGroup = "Validate" : REVSalesUnitCode.ControlToValidate = "txtSalesUnitCode"
                REVSalesUnitCode.ValidationExpression = "^(.{0,25})$" : REVSalesUnitCode.ErrorMessage = "Sales Unit Code exceeded maximum size(max 25 character)."
                'Branch Code
                RFVBranchCode.ValidationGroup = True
                RFVBranchCode.ValidationGroup = "Validate" : RFVBranchCode.ControlToValidate = "txtBranchCode" : RFVBranchCode.ErrorMessage = "Enter Branch Code."
                REVBranchCode.ValidationGroup = "Validate" : REVBranchCode.ControlToValidate = "txtBranchCode"
                REVBranchCode.ValidationExpression = "^(.{0,25})$" : REVBranchCode.ErrorMessage = "Branch Code exceeded maximum size(max 25 character)."
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadTreeView()
        Dim dt As New DataTable
        Try
            dtOrgStructureReport = objclsOrgStructure.LoadOrgStructureReport(sSession.AccessCode, sSession.AccessCodeID)
            dt = objclsOrgStructure.LoadOrgStructure(sSession.AccessCode, sSession.AccessCodeID, 0)
            TVOrgStructure.DataSource = dt
            TVOrgStructure.DataKeyField = "Org_Node"
            TVOrgStructure.DataParentField = "Org_Parent"
            TVOrgStructure.DataTextField = "Org_Name"
            TVOrgStructure.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub TVOrgStructure_NodeClick(ByVal sender As Object, ByVal args As PowerUp.Web.UI.WebTree.TreeNodeEventArgs) Handles TVOrgStructure.NodeClick
        Try
            chkboxSetBranch.Checked = False
            lblError.Text = ""
            txtParentID.Text = args.Node.DataKey
            txtCurrentID.Text = args.Node.DataKey
            txtDepthID.Text = args.Node.Depth
            LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
            txtName.Focus()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "TVOrgStructure_NodeClick")
        End Try
    End Sub
    Private Sub TVOrgStructure_NodePopulate(ByVal sender As Object, ByVal args As PowerUp.Web.UI.WebTree.TreeNodeEventArgs) Handles TVOrgStructure.NodePopulate
        Dim dt As New DataTable
        Try
            dt = objclsOrgStructure.LoadOrgStructure(sSession.AccessCode, sSession.AccessCodeID, args.Node.DataKey)
            TVOrgStructure.DataKeyField = "Org_Node"
            TVOrgStructure.DataParentField = "Org_Parent"
            TVOrgStructure.DataTextField = "Org_Name"
            args.Node.Populate(dt)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "TVOrgStructure_NodePopulate")
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As EventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim iParentID As Integer, iCurrentID As Integer, iDepthID As Integer
        Try
            lblError.Text = ""
            iParentID = Val(txtParentID.Text) : iCurrentID = Val(txtCurrentID.Text)
            If iCurrentID = 0 Then
                iDepthID = Val(txtDepthID.Text) + 1
            Else
                iDepthID = Val(txtDepthID.Text)
            End If
            If lblIRDACode.Text.Trim = "" Then
                lblValidationMsgOrgStructurer.Text = "Enter Org Code." : lblError.Text = "Enter Org Code."
                txtIRDACode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
            If (txtIRDACode.Text.Trim).Length > 25 Then
                lblValidationMsgOrgStructurer.Text = "Org Code exceeded maximum size(max 25 characters)." : lblError.Text = "Org Code exceeded maximum size(max 25 characters)."
                txtIRDACode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
            If iIsInBranchLevel = 1 Then
                If txtSalesUnitCode.Text.Trim = "" Then
                    lblValidationMsgOrgStructurer.Text = "Enter Sales Unit Code." : lblError.Text = "Enter Sales Unit Code."
                    txtSalesUnitCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
                If (txtSalesUnitCode.Text.Trim).Length > 25 Then
                    lblValidationMsgOrgStructurer.Text = "Sales Unit Code exceeded maximum size(max 25 characters)." : lblError.Text = "Sales Unit Code exceeded maximum size(max 25 characters)."
                    txtSalesUnitCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
                If txtBranchCode.Text.Trim = "" Then
                    lblValidationMsgOrgStructurer.Text = "Enter Branch Code." : lblError.Text = "Enter Branch Code."
                    txtBranchCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
                If (txtBranchCode.Text.Trim).Length > 25 Then
                    lblValidationMsgOrgStructurer.Text = "Branch Code exceeded maximum size(max 25 characters)." : lblError.Text = "Branch Code exceeded maximum size(max 25 characters)."
                    txtBranchCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
            End If
            If txtName.Text.Trim = "" Then
                lblValidationMsgOrgStructurer.Text = "Enter Name." : lblError.Text = "Enter Name."
                txtName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
            If (txtName.Text.Trim).Length > 1000 Then
                lblValidationMsgOrgStructurer.Text = "Name exceeded maximum size(max 1000 characters)." : lblError.Text = "Name exceeded maximum size(max 1000 characters)."
                txtName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If

            If txtNote.Text.Trim <> "" Then
                If (txtNote.Text.Trim).Length > 2000 Then
                    lblValidationMsgOrgStructurer.Text = "Notes exceeded maximum size(max 2000 characters)." : lblError.Text = "Notes exceeded maximum size(max 2000 characters)."
                    txtNote.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
            End If

            If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(txtIRDACode.Text.Trim)), iCurrentID) = True Then
                lblValidationMsgOrgStructurer.Text = "This IRDA Code already exists." : lblError.Text = "This IRDA Code already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If

            If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(txtName.Text.Trim)), iParentID, iCurrentID) = True Then
                lblValidationMsgOrgStructurer.Text = "This Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                lblError.Text = "This Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
            objclsOrgStructure.iOrgnode = iCurrentID
            objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(txtIRDACode.Text)
            If iIsInBranchLevel = 1 Then
                objclsOrgStructure.sOrgSalesUnitCode = objclsGRACeGeneral.SafeSQL(txtSalesUnitCode.Text)
                objclsOrgStructure.sOrgBranchCode = objclsGRACeGeneral.SafeSQL(txtBranchCode.Text)
            Else
                objclsOrgStructure.sOrgSalesUnitCode = ""
                objclsOrgStructure.sOrgBranchCode = ""
            End If
            objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(txtName.Text)
            objclsOrgStructure.iOrgAppStrength = 0
            objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(txtNote.Text)
            objclsOrgStructure.iOrgParent = iParentID
            objclsOrgStructure.iOrgLevelCode = iDepthID
            objclsOrgStructure.sOrgDelflag = "W"
            objclsOrgStructure.sOrgStatus = "C"
            objclsOrgStructure.iOrgCreatedBy = sSession.UserID
            objclsOrgStructure.dOrgCreatedOn = Date.Today
            objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
            'objclsOrgStructure.iOrg_Default = 0

            Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
            txtParentID.Text = Arr(1) : txtCurrentID.Text = Arr(1) : txtDepthID.Text = iDepthID
            LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
            LoadTreeView()
            If Arr(0) = "3" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Organisation Structure", "Saved", Arr(1), txtName.Text.Trim, 0, "", sSession.IPAddress)
                lblValidationMsgOrgStructurer.Text = "Successfully Saved & Waiting for Approval." : lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationOrgStructurer').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As EventArgs) Handles imgbtnUpdate.Click
        Dim Arr() As String
        Dim iParentID As Integer, iCurrentID As Integer, iDepthID As Integer
        Try
            lblError.Text = ""
            iParentID = Val(txtParentID.Text) : iCurrentID = Val(txtCurrentID.Text)
            If iCurrentID = 0 Then
                iDepthID = Val(txtDepthID.Text) + 1
            Else
                iDepthID = Val(txtDepthID.Text)
            End If
            If txtIRDACode.Text.Trim = "" Then
                lblValidationMsgOrgStructurer.Text = "Enter Org Code." : lblError.Text = "Enter Org Code."
                txtIRDACode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
            If (txtIRDACode.Text.Trim).Length > 25 Then
                lblValidationMsgOrgStructurer.Text = "Org Code exceeded maximum size(max 25 characters)." : lblError.Text = "Org Code exceeded maximum size(max 25 characters)."
                txtIRDACode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
            If iIsInBranchLevel = 1 Then
                If txtSalesUnitCode.Text.Trim = "" Then
                    lblValidationMsgOrgStructurer.Text = "Enter Sales Unit Code." : lblError.Text = "Enter Sales Unit Code."
                    txtSalesUnitCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
                If (txtSalesUnitCode.Text.Trim).Length > 25 Then
                    lblValidationMsgOrgStructurer.Text = "Sales Unit Code exceeded maximum size(max 25 characters)." : lblError.Text = "Sales Unit Code exceeded maximum size(max 25 characters)."
                    txtSalesUnitCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
                If txtBranchCode.Text.Trim = "" Then
                    lblValidationMsgOrgStructurer.Text = "Enter Branch Code." : lblError.Text = "Enter Branch Code."
                    txtBranchCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
                If (txtBranchCode.Text.Trim).Length > 25 Then
                    lblValidationMsgOrgStructurer.Text = "Branch Code exceeded maximum size(max 25 characters)." : lblError.Text = "Branch Code exceeded maximum size(max 25 characters)."
                    txtBranchCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
            End If
            If txtName.Text.Trim = "" Then
                lblValidationMsgOrgStructurer.Text = "Enter Name." : lblError.Text = "Enter Name."
                txtName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
            If (txtName.Text.Trim).Length > 1000 Then
                lblValidationMsgOrgStructurer.Text = "Name exceeded maximum size(max 1000 characters)." : lblError.Text = "Name exceeded maximum size(max 1000 characters)."
                txtName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If

            If txtNote.Text.Trim <> "" Then
                If (txtNote.Text.Trim).Length > 2000 Then
                    lblValidationMsgOrgStructurer.Text = "Notes exceeded maximum size(max 2000 characters)." : lblError.Text = "Notes exceeded maximum size(max 2000 characters)."
                    txtNote.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
            End If

            If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(txtIRDACode.Text.Trim)), iCurrentID) = True Then
                lblValidationMsgOrgStructurer.Text = "This IRDA Code already exists." : lblError.Text = "This IRDA Code already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If

            If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(txtName.Text.Trim)), iParentID, iCurrentID) = True Then
                lblValidationMsgOrgStructurer.Text = "This Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                lblError.Text = "This Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If

            objclsOrgStructure.iOrgnode = iCurrentID
            objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(txtIRDACode.Text)
            If iIsInBranchLevel = 1 Then
                objclsOrgStructure.sOrgSalesUnitCode = objclsGRACeGeneral.SafeSQL(txtSalesUnitCode.Text)
                objclsOrgStructure.sOrgBranchCode = objclsGRACeGeneral.SafeSQL(txtBranchCode.Text)
            Else
                objclsOrgStructure.sOrgSalesUnitCode = ""
                objclsOrgStructure.sOrgBranchCode = ""
            End If
            objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(txtName.Text)
            objclsOrgStructure.iOrgAppStrength = 0
            objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(txtNote.Text)
            objclsOrgStructure.iOrgParent = iParentID
            objclsOrgStructure.iOrgLevelCode = iDepthID
            objclsOrgStructure.sOrgDelflag = "A"
            objclsOrgStructure.sOrgStatus = "C"
            objclsOrgStructure.iOrgCreatedBy = sSession.UserID
            objclsOrgStructure.dOrgCreatedOn = Date.Today
            objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
            Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
            txtParentID.Text = Arr(1) : txtCurrentID.Text = Arr(1) : txtDepthID.Text = iDepthID
            LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
            LoadTreeView()
            If Arr(0) = "2" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Organisation Structure", "Updated", Arr(1), txtName.Text.Trim, 0, "", sSession.IPAddress)
                If sOrgFlag = "W" Then
                    lblValidationMsgOrgStructurer.Text = "Successfully Updated & Waiting for Approval." : lblError.Text = "Successfully Updated & Waiting for Approval."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationOrgStructurer').modal('show');", True)
                Else
                    lblValidationMsgOrgStructurer.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationOrgStructurer').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Try
            lblError.Text = ""
            If Val(txtCurrentID.Text) > 0 Then
                If sOrgFlag = "D" Then
                    If objclsOrgStructure.CheckParentElementOrgStructure(sSession.AccessCode, sSession.AccessCodeID, Val(txtCurrentID.Text), "A") = False Then
                        objclsOrgStructure.DeActivateOrgStructureDetails(sSession.AccessCode, sSession.AccessCodeID, Val(txtCurrentID.Text), sSession.UserID, sSession.IPAddress, "A")
                        LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Organisation Structure", "Activated", txtCurrentID.Text, txtName.Text, 0, "", sSession.IPAddress)
                        lblValidationMsgOrgStructurer.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Else
                        lblValidationMsgOrgStructurer.Text = "Activate parent element first." : lblError.Text = "Activate parent element first."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    End If
                End If
            Else
                lblValidationMsgOrgStructurer.Text = "Select Zone/Region/Area/Branch." : lblError.Text = "Select Zone/Region/Area/Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Try
            lblError.Text = ""
            If Val(txtCurrentID.Text) > 0 Then
                If objclsCheckMasterIsInUse.CheckOrganizationStructureIsInUse(sSession.AccessCode, sSession.AccessCodeID, txtCurrentID.Text) = True Then
                    lblValidationMsgOrgStructurer.Text = "Already tag to some User, can't be De-Activated." : lblError.Text = "Already tag to some User, can't be De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Exit Sub
                End If
                If sOrgFlag = "A" Then
                    If objclsOrgStructure.CheckChildElementOrgStructure(sSession.AccessCode, sSession.AccessCodeID, Val(txtCurrentID.Text), "D") = False Then
                        objclsOrgStructure.DeActivateOrgStructureDetails(sSession.AccessCode, sSession.AccessCodeID, Val(txtCurrentID.Text), sSession.UserID, sSession.IPAddress, "D")
                        LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Organisation Structure", "De-Activated", txtCurrentID.Text, txtName.Text, 0, "", sSession.IPAddress)
                        lblValidationMsgOrgStructurer.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Else
                        lblValidationMsgOrgStructurer.Text = "De-Activate all child element first." : lblError.Text = "De-Activate all child element first."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    End If
                End If
            Else
                lblValidationMsgOrgStructurer.Text = "Select Zone/Region/Area/Branch." : lblError.Text = "Select Zone/Region/Area/Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            lblError.Text = ""
            If Val(txtCurrentID.Text) > 0 Then
                If sOrgFlag = "W" Then
                    If objclsOrgStructure.CheckParentElementOrgStructure(sSession.AccessCode, sSession.AccessCodeID, Val(txtCurrentID.Text), "W") = False Then
                        objclsOrgStructure.DeActivateOrgStructureDetails(sSession.AccessCode, sSession.AccessCodeID, Val(txtCurrentID.Text), sSession.UserID, sSession.IPAddress, "W")
                        LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Organisation Structure", "Approved", txtCurrentID.Text, txtName.Text, 0, "", sSession.IPAddress)
                        lblValidationMsgOrgStructurer.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationOrgStructurer').modal('show');", True)
                    Else
                        lblValidationMsgOrgStructurer.Text = "Approve all parent element first." : lblError.Text = "Approve all parent element first."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                    End If
                End If
            Else
                lblValidationMsgOrgStructurer.Text = "Select Zone/Region/Area/Branch." : lblError.Text = "Select Zone/Region/Area/Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Try
            lblError.Text = ""
            If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(txtSearch.Text), 0) = True Then
                txtParentID.Text = objclsOrgStructure.GetCurrentNodeParentDeptID(sSession.AccessCode, sSession.AccessCodeID, UCase(txtSearch.Text), "PARENT")
                txtCurrentID.Text = objclsOrgStructure.GetCurrentNodeParentDeptID(sSession.AccessCode, sSession.AccessCodeID, UCase(txtSearch.Text), "NODE")
                txtDepthID.Text = objclsOrgStructure.GetCurrentNodeParentDeptID(sSession.AccessCode, sSession.AccessCodeID, UCase(txtSearch.Text), "LEVEL")
                LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
                txtName.Focus()
            Else
                lblValidationMsgOrgStructurer.Text = "Invalid IRDA Code." : lblError.Text = "Invalid IRDA Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationOrgStructurer').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Try
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtOrgStructureReport)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/OrganizationStructure.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Organisation Structure", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=OrganisationStructure" + ".pdf")
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
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtOrgStructureReport)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/OrganizationStructure.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Organisation Structure", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=OrganisationStructure" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub chkboxSetBranch_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxSetBranch.CheckedChanged
        Try
            If txtDepthID.Text = 4 Then
                objclsOrgStructure.UpdateDefault(sSession.AccessCode, sSession.AccessCodeID, txtCurrentID.Text)
                lblError.Text = "Default has been set."
            Else
                lblError.Text = "Default can be set only for Branch."
                chkboxSetBranch.Checked = False
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
