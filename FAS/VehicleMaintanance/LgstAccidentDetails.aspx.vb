Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class VehicleMaintanance_LgstAccidentDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/LgstAccidentDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Dim objCOA As New clsChartOfAccounts
    Private Shared sCMDSave As String
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objvad As New clsVehicleAccidentDetails
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnAddDet.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try

            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LDB")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCMDSave = "NO" : imgbtnWaiting.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True

                        sCMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If
                txtAccId.Text = 0 : lblID.Text = 0
                BindExistingVehicle() : BindVehicle()
                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iInvID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlExistingVehicleNo.SelectedValue = iInvID
                    'BindDetails(iInvID)
                    ddlExistingVehicleNo_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub BindVehicle()
        Try
            ddlvehilceNo.DataSource = objvad.LoadVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlvehilceNo.DataTextField = "LVAM_RegNo"
            ddlvehilceNo.DataValueField = "LVAM_ID"
            ddlvehilceNo.DataBind()
            ddlvehilceNo.Items.Insert(0, "Select Existing Vehicle")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingVehicle")
            Throw
        End Try
    End Sub
    Protected Sub BindExistingVehicle()
        Try
            ddlExistingVehicleNo.DataSource = objvad.LoadAccidentVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingVehicleNo.DataTextField = "LVAD_RegNo"
            ddlExistingVehicleNo.DataValueField = "LVAD_MasterID"
            ddlExistingVehicleNo.DataBind()
            ddlExistingVehicleNo.Items.Insert(0, "Select Existing Vehicle")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingVehicle")
            Throw
        End Try
    End Sub
    Private Sub ddlExistingVehicleNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingVehicleNo.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistingVehicleNo.SelectedIndex > 0 Then
                BindDetails(ddlExistingVehicleNo.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingVehicleNo_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindDetails(ByVal iVehicleId As Integer)
        Dim dt As New DataTable
        Try
            ddlvehilceNo.SelectedValue = ddlExistingVehicleNo.SelectedValue
            ddlvehilceNo.Enabled = False
            txtAccId.Text = ddlExistingVehicleNo.SelectedValue
            dgAccidentDet.Visible = True
            dgAccidentDet.DataSource = objvad.LoadAccidentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
            dgAccidentDet.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
        End Try
    End Sub

    Private Sub imgbtnAddDet_click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddDet.Click
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlvehilceNo.SelectedIndex > 0 Then
                If txtAccDt.Text = "" Or txtCaseDet.Text = "" Or txtDamageDet.Text = "" Or txtPoliceComplntDet.Text = "" Or txtVehStat.Text = "" Then
                    lblError.Text = "Enter Complain Details"
                    Exit Sub
                End If
                objvad.LVAD_ID = txtAccId.Text
                If ddlvehilceNo.SelectedIndex > 0 Then
                    objvad.LVAD_MasterID = ddlvehilceNo.SelectedValue
                Else
                    objvad.LVAD_MasterID = 0
                End If
                objvad.LVAD_RegNo = ddlvehilceNo.SelectedItem.Text
                objvad.LVAD_AccidentDt = txtAccDt.Text
                objvad.LVAD_DamageDtls = txtDamageDet.Text
                objvad.LVAD_ComplaintDtls = txtPoliceComplntDet.Text

                objvad.LVAD_CaseDtls = txtCaseDet.Text
                objvad.LVAD_VehcileDtls = txtVehStat.Text

                objvad.LVAD_Delflag = "W"
                objvad.LVAD_CompID = sSession.AccessCodeID
                objvad.LVAD_Status = "C"
                objvad.LVAD_Operation = "C"
                objvad.LVAD_IPAddress = sSession.IPAddress
                objvad.LVAD_CreatedBy = sSession.UserID
                objvad.LVAD_CreatedOn = Date.Today
                objvad.LVAD_ApprovedBy = Nothing
                objvad.LVAD_ApprovedOn = Date.Today
                objvad.LVAD_DeletedBy = Nothing
                objvad.LVAD_DeletedOn = Date.Today
                objvad.LVAD_UpdatedBy = sSession.UserID
                objvad.LVAD_UpdatedOn = Date.Today
                objvad.LVAD_YearID = sSession.YearID





                Arr = objvad.SaveLoanMater(sSession.AccessCode, objvad)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    lblStatus.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    If sCMDSave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If
                    imgbtnSave.Visible = False 'btnDelete.Visible = True
                    'btnSave.Text = "Save"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    If sCMDSave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If  'btnDelete.Visible = True
                    imgbtnSave.Visible = False
                    ' btnSave.Text = "Update"
                    imgbtnWaiting.Visible = True
                    lblStatus.Text = "Waiting For Approval"
                End If


                dgAccidentDet.Visible = True
                dgAccidentDet.DataSource = objvad.LoadAccidentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlvehilceNo.SelectedValue, sSession.YearID)
                dgAccidentDet.DataBind()
                txtAccId.Text = 0
                ClearAccDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddDet_click")
        End Try
    End Sub
    Public Sub ClearAccDetails()
        Try
            lblError.Text = ""
            txtAccDt.Text = "" : txtCaseDet.Text = "" : txtDamageDet.Text = ""
            txtPoliceComplntDet.Text = "" : txtVehStat.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAccDetails")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/VehicleMaintanance/LgstAccidentDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub ddlvehilceNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlvehilceNo.SelectedIndexChanged
        Dim icount As Integer
        Try
            lblError.Text = ""
            If ddlvehilceNo.SelectedIndex > 0 Then

                icount = objvad.GetVehCount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlvehilceNo.SelectedValue, ddlvehilceNo.SelectedItem.Text)
                If icount > 0 Then




                    ddlExistingVehicleNo.SelectedValue = ddlvehilceNo.SelectedValue
                    BindDetails(ddlvehilceNo.SelectedValue)
                Else
                    ddlExistingVehicleNo.SelectedIndex = 0
                    dgAccidentDet.DataSource = Nothing
                    dgAccidentDet.DataBind()
                    txtAccId.Text = 0
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlvehilceNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/VehicleMaintanance/LgstAccidentDtlsDashboard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub dgAccidentDet_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAccidentDet.ItemCommand
        Dim dtAccidentDet As New DataTable
        Try
            If e.CommandName = "Edit" Then
                txtAccId.Text = e.Item.Cells(0).Text
                imgbtnAddDet.ImageUrl = "~/Images/Update16.png"
                dtAccidentDet = objvad.GetAccidentDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlvehilceNo.SelectedValue, sSession.YearID)
                If dtAccidentDet.Rows.Count > 0 Then
                    txtAccDt.Text = dtAccidentDet.Rows(0)("LVAD_AccidentDt")
                    txtCaseDet.Text = dtAccidentDet.Rows(0)("LVAD_CaseDtls")
                    txtDamageDet.Text = dtAccidentDet.Rows(0)("LVAD_DamageDtls")
                    txtPoliceComplntDet.Text = dtAccidentDet.Rows(0)("LVAD_ComplaintDtls")
                    txtVehStat.Text = dtAccidentDet.Rows(0)("LVAD_VehcileDtls")
                End If
            ElseIf e.CommandName = "Delete" Then
                objvad.DeleteComplianceValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlvehilceNo.SelectedValue, sSession.YearID)
                dgAccidentDet.DataSource = objvad.LoadAccidentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlvehilceNo.SelectedValue, sSession.YearID)
                dgAccidentDet.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAccidentDet_ItemCommand")
        End Try
    End Sub
End Class
