Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class VehicleMaintanance_LgstInsuranceDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/LgstInsuranceDetails"
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
    Dim objvim As New clsVehicleInsuranceMaster
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
        imgbtnAddAmt.ImageUrl = "~/Images/Add24.png"
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
                BindExistingVehicle() : BindInsVehicle()
                txtInsId.Text = 0
                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iInvID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlExistingVehicleNo.SelectedValue = iInvID
                    'BindDetails(iInvID)
                    ddlExistingVehicleNo_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch
        End Try
    End Sub
    Protected Sub BindInsVehicle()
        Try
            ddlInsVehicleNo.DataSource = objvim.LoadInsVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlInsVehicleNo.DataTextField = "LVID_RegNo"
            ddlInsVehicleNo.DataValueField = "LVID_MasterID"
            ddlInsVehicleNo.DataBind()
            ddlInsVehicleNo.Items.Insert(0, "Select Vehicle")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindExistingVehicle()
        Try
            ddlExistingVehicleNo.DataSource = objvim.LoadVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingVehicleNo.DataTextField = "LVM_RegNo"
            ddlExistingVehicleNo.DataValueField = "LVM_ID"
            ddlExistingVehicleNo.DataBind()
            ddlExistingVehicleNo.Items.Insert(0, "Select Vehicle")
        Catch ex As Exception
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
        End Try
    End Sub
    Public Sub BindDetails(ByVal iVehicleId As Integer)
        Dim dt As New DataTable
        Try
            dt = objvim.LoadVehicleDetails(sSession.AccessCode, sSession.AccessCodeID, iVehicleId)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LVM_Id")
                If IsDBNull(dt.Rows(0)("LVM_InsuranceType")) = False Then
                    ddlInsuranceType.SelectedIndex = dt.Rows(0)("LVM_InsuranceType")
                Else
                    ddlInsuranceType.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("LVM_Id")) = False Then
                    ddlExistingVehicleNo.SelectedValue = dt.Rows(0)("LVM_Id")
                    ddlExistingVehicleNo.Enabled = False
                Else
                    ddlExistingVehicleNo.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("LVM_InsuranceNo")) = False Then
                    txtPolicyNo.Text = dt.Rows(0)("LVM_InsuranceNo")
                    txtPaymentPolicyNo.Text = dt.Rows(0)("LVM_InsuranceNo")
                    txtPolicyNo.Enabled = True
                Else
                    txtPolicyNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_InsuranceAmt")) = False Then
                    txtPolicyAmt.Text = dt.Rows(0)("LVM_InsuranceAmt")
                    txtPolicyAmt.Enabled = True
                Else
                    txtPolicyAmt.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_InsuranceExpDate")) = False Then
                    txtExpiryDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LVM_InsuranceExpDate").ToString(), "D")
                    txtExpiryDate.Enabled = True
                Else
                    txtExpiryDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_InsuranceDetails")) = False Then
                    TxtPolicyDetails.Text = dt.Rows(0)("LVM_InsuranceDetails")
                    TxtPolicyDetails.Enabled = True
                Else
                    TxtPolicyDetails.Text = ""
                End If
                'If (dt.Rows(0)("LVM_DelFlag") = "W") Then
                '    lblStatus.Text = "Waiting For Approval"
                '    '  txtRegistrationNo.Enabled = False
                '    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                '    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                '    imgbtnWaiting.Visible = True
                'End If
                'If (dt.Rows(0)("LVM_Delflag") = "A") Then
                '    lblStatus.Text = "Approved"
                '    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                '    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                '    imgbtnWaiting.Visible = False
                'End If
                'If (dt.Rows(0)("LVM_Delflag") = "D") Then
                '    lblStatus.Text = "Deactivated"
                '    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                '    imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
                '    imgbtnWaiting.Visible = False
                'End If
                Dim dtInsurance As New DataTable
                dtInsurance = objvim.LoadInsuranceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                If dtInsurance.Rows.Count > 0 Then
                    dgInsuranceDet.Visible = True
                    dgInsuranceDet.DataSource = dtInsurance
                    dgInsuranceDet.DataBind()
                    If IsDBNull(dt.Rows(0)("LVM_ID")) = False Then
                        ddlInsVehicleNo.SelectedValue = dt.Rows(0)("LVM_ID")
                    Else
                        ddlInsVehicleNo.SelectedIndex = 0
                    End If
                Else
                    dgInsuranceDet.DataSource = Nothing
                    dgInsuranceDet.DataBind()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub imgbtnAddAmt_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddAmt.Click
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlExistingVehicleNo.SelectedIndex > 0 Then
                objvim.LVID_ID = txtInsId.Text
                objvim.LVID_MasterID = ddlExistingVehicleNo.SelectedValue
                objvim.LVID_RegNo = ddlExistingVehicleNo.SelectedItem.Text
                objvim.LVID_PolicyNo = txtPaymentPolicyNo.Text
                objvim.LVID_InsCompany = txtInsComp.Text
                objvim.LVID_InsFromDate = txtFromDt.Text
                objvim.LVID_InsToDate = txtToDt.Text
                objvim.LVID_InsPaidDt = txtInsPaidDt.Text
                objvim.LVID_InsPaidAmt = txtInsPaidAmt.Text
                objvim.LVID_InsInterestAmt = txtInterestAmt.Text
                objvim.LVID_TotalAmt = txttotalAmt.Text
                objvim.LVID_Reference = txtReference.Text
                objvim.LVID_DelFlag = "X"
                objvim.LVID_Status = "W"
                objvim.LVID_CreatedBy = sSession.UserID
                objvim.LVID_CreatedOn = Date.Today
                objvim.LVID_UpdatedBy = sSession.UserID
                objvim.LVID_UpdatedOn = Date.Today
                objvim.LVID_CompID = sSession.AccessCodeID
                objvim.LVID_YearID = sSession.YearID
                objvim.LVID_Operation = "C"
                objvim.LVID_IPAddress = sSession.IPAddress
                Arr = objvim.SaveInsuranceDetails(sSession.AccessCode, objvim)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
                dgInsuranceDet.Visible = True
                dgInsuranceDet.DataSource = objvim.LoadInsuranceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                dgInsuranceDet.DataBind()
                txtInsId.Text = 0
                ClearInsuranceDetails()
            Else
                lblError.Text = "Select Existing Vehicle Registeration No."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddAmt_Click")
        End Try
    End Sub
    Public Sub ClearInsuranceDetails()
        Try
            lblError.Text = ""
            txtFromDt.Text = "" : txtToDt.Text = "" : txtInsPaidDt.Text = "" : txtInsPaidAmt.Text = "" : txtInterestAmt.Text = ""
            txttotalAmt.Text = "" : txtReference.Text = "" : txtInsComp.Text = "" : txtPaymentPolicyNo.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/VehicleMaintanance/LgstInsuranceDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/VehicleMaintanance/LgstInsuranceDashBoard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Protected Sub txtPolicyNo_TextChanged(sender As Object, e As EventArgs)
        Try
            txtPaymentPolicyNo.Text = txtPolicyNo.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtPolicyNo_TextChanged")
        End Try
    End Sub

    Protected Sub txtInterestAmt_TextChanged(sender As Object, e As EventArgs)
        Try
            txttotalAmt.Text = Val(txtInsPaidAmt.Text) + Val(txtInterestAmt.Text)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtInterestAmt_TextChanged")
        End Try
    End Sub

    Private Sub ddlInsVehicleNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInsVehicleNo.SelectedIndexChanged
        Try
            BindDetails(ddlInsVehicleNo.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInsVehicleNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub dgInsuranceDet_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgInsuranceDet.ItemCommand
        Dim dtTyreDet As New DataTable
        Try
            If e.CommandName = "Edit" Then
                txtInsId.Text = e.Item.Cells(0).Text
                imgbtnAddAmt.ImageUrl = "~/Images/Update16.png"
                dtTyreDet = objvim.GetInsDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                If dtTyreDet.Rows.Count > 0 Then
                    txtPaymentPolicyNo.Text = dtTyreDet.Rows(0)("LVID_PolicyNo")
                    txtInsComp.Text = dtTyreDet.Rows(0)("LVID_InsCompany")
                    txtFromDt.Text = dtTyreDet.Rows(0)("LVID_InsFromDate")
                    txtToDt.Text = dtTyreDet.Rows(0)("LVID_InsToDate")
                    txtInsPaidDt.Text = dtTyreDet.Rows(0)("LVID_InsPaidDt")
                    txtInsPaidAmt.Text = dtTyreDet.Rows(0)("LVID_InsPaidAmt")
                    txtInterestAmt.Text = dtTyreDet.Rows(0)("LVID_InsInterestAmt")
                    txttotalAmt.Text = dtTyreDet.Rows(0)("LVID_TotalAmt")
                    txtReference.Text = dtTyreDet.Rows(0)("LVID_Reference")
                End If
            ElseIf e.CommandName = "Delete" Then
                objvim.DeleteInsValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                dgInsuranceDet.DataSource = objvim.LoadInsuranceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                dgInsuranceDet.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInsuranceDet_ItemCommand")
        End Try
    End Sub

    'Private Sub txttotalAmt_TextChanged(sender As Object, e As EventArgs) Handles txttotalAmt.TextChanged
    '    Dim dDebitAmt As Double = 0.0
    '    Try
    '        dDebitAmt = objvim.GetDebitAmt(sSession.AccessCode, sSession.AccessCodeID)
    '        If Val(txttotalAmt.Text) > Val(dDebitAmt) Then
    '            lblVehicleValidationMsg.Text = "Total Amount is Greater than" & dDebitAmt : lblError.Text = "Total Amount is Greater than" & dDebitAmt
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txttotalAmt_TextChanged")
    '    End Try
    'End Sub
    Protected Sub txttotalAmt_TextChanged(sender As Object, e As EventArgs)
        Dim dDebitAmt As Double = 0.0, dInsuranceAmt As Double = 0.0, dBalanceAmt As Double = 0.0
        Try
            dDebitAmt = objvim.GetDebitAmt(sSession.AccessCode, sSession.AccessCodeID)
            dInsuranceAmt = objvim.GetInsuranceAmt(sSession.AccessCode, sSession.AccessCodeID)
            If dDebitAmt > dInsuranceAmt Then
                dBalanceAmt = Val(dDebitAmt) - Val(dInsuranceAmt)
                If Val(txttotalAmt.Text) > Val(dBalanceAmt) Then
                    lblVehicleValidationMsg.Text = "Total Amount is Greater than" & dBalanceAmt : lblError.Text = "Total Amount is Greater than" & dBalanceAmt
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                    txttotalAmt.Text = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txttotalAmt_TextChanged")
        End Try
    End Sub
End Class
