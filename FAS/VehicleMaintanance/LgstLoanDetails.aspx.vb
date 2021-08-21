Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class VehicleMaintanance_VehOtherDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/LgstLoanDetails"
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
    Dim objvlm As New clsVehicleLoanMaster
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
                txtLoanId.Text = 0 : lblID.Text = 0
                BindExistingVehicle() : LoadBanksName() : BindLoanVehicle()
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
    Protected Sub BindExistingVehicle()
        Try
            ddlExistingVehicleNo.DataSource = objvlm.LoadVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingVehicleNo.DataTextField = "LVAM_RegNo"
            ddlExistingVehicleNo.DataValueField = "LVAM_ID"
            ddlExistingVehicleNo.DataBind()
            ddlExistingVehicleNo.Items.Insert(0, "Select Existing Vehicle")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingVehicle")
            Throw
        End Try
    End Sub
    Protected Sub BindLoanVehicle()
        Try
            ddlLoanReg.DataSource = objvlm.LoadLoanVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlLoanReg.DataTextField = "LVLM_RegNo"
            ddlLoanReg.DataValueField = "LVLM_MasterID"
            ddlLoanReg.DataBind()
            ddlLoanReg.Items.Insert(0, "Select Vehicle")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingVehicle")
            Throw
        End Try
    End Sub
    Public Sub LoadBanksName()
        Dim dt As New DataTable
        Try
            dt = objvlm.LoadBanksName(sSession.AccessCode, sSession.AccessCodeID)
            ddlBank.DataTextField = "Description"
            ddlBank.DataValueField = "gl_id"
            ddlBank.DataSource = dt
            ddlBank.DataBind()
            ddlBank.Items.Insert(0, "Select Bank")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadBanksName")
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
            dt = objvlm.LoadVehicleDetails(sSession.AccessCode, sSession.AccessCodeID, iVehicleId)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LVLM_ID")
                If IsDBNull(dt.Rows(0)("LVLM_MasterID")) = False Then
                    ddlExistingVehicleNo.SelectedValue = dt.Rows(0)("LVLM_MasterID")
                    ddlExistingVehicleNo.Enabled = False
                Else
                    ddlExistingVehicleNo.SelectedValue = 0
                    ddlExistingVehicleNo.Enabled = True
                End If
                If IsDBNull(dt.Rows(0)("LVLM_MasterID")) = False Then
                    BindLoanVehicle()
                    ddlLoanReg.SelectedValue = dt.Rows(0)("LVLM_MasterID")
                Else
                    ddlLoanReg.SelectedValue = 0
                End If
                If IsDBNull(dt.Rows(0)("LVLM_LoanAmount")) = False Then
                    txtLoanAmt.Text = String.Format("{0:0.0}", Convert.ToDecimal(dt.Rows(0).Item("LVLM_LoanAmount").ToString()))
                Else
                    txtLoanAmt.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVLM_LoanAccNo")) = False Then
                    txtLoanAccNo.Text = dt.Rows(0)("LVLM_LoanAccNo")
                Else
                    txtLoanAccNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVLM_BankName")) = False Then
                    ddlBank.SelectedValue = dt.Rows(0)("LVLM_BankName")
                Else
                    ddlBank.SelectedValue = 0
                End If
                If IsDBNull(dt.Rows(0)("LVLM_BranchName")) = False Then
                    txtBranchName.Text = dt.Rows(0)("LVLM_BranchName")
                Else
                    txtBranchName.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVLM_LoanDate")) = False Then
                    txtDateofLoanRec.Text = dt.Rows(0)("LVLM_LoanDate")
                Else
                    txtDateofLoanRec.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVLM_LoanDueDate")) = False Then
                    txtInstllmntDueDate.Text = dt.Rows(0)("LVLM_LoanDueDate")
                Else
                    txtInstllmntDueDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVLM_InstallmentAmt")) = False Then
                    txtInstallmntAmt.Text = String.Format("{0:0.0}", Convert.ToDecimal(dt.Rows(0).Item("LVLM_InstallmentAmt").ToString()))
                    '  txtInstallmntAmt_TextChanged(send)
                    txtInstlmntPaidAmt.Text = String.Format("{0:0.0}", Convert.ToDecimal(dt.Rows(0).Item("LVLM_InstallmentAmt").ToString()))
                Else
                    txtInstallmntAmt.Text = ""
                End If
                If (dt.Rows(0)("LVLM_DelFlag") = "W") Then
                    lblStatus.Text = "Waiting For Approval"
                    '  txtRegistrationNo.Enabled = False
                    '   imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    '   imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                    '   imgbtnWaiting.Visible = True
                End If
                If (dt.Rows(0)("LVLM_Delflag") = "A") Then
                    lblStatus.Text = "Approved"
                    '    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    '    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                    '   imgbtnWaiting.Visible = False
                End If
                If (dt.Rows(0)("LVLM_Delflag") = "D") Then
                    lblStatus.Text = "Deactivated"
                    '   imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                    '   imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
                    '   imgbtnWaiting.Visible = False
                End If
                dgInstallmentDet.Visible = True
                dgInstallmentDet.DataSource = objvlm.LoadInstallmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                dgInstallmentDet.DataBind()
            Else
                dgInstallmentDet.Visible = False
                dgInstallmentDet.DataSource = Nothing
                dgInstallmentDet.DataBind()
                ddlLoanReg.SelectedIndex = 0 : ddlExistingVehicleNo.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
        End Try
    End Sub

    Private Sub txtInstallmntAmt_TextChanged(sender As Object, e As EventArgs) Handles txtInstallmntAmt.TextChanged
        Try
            txtInstlmntPaidAmt.Text = txtInstallmntAmt.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtInstallmntAmt_TextChanged")
        End Try
    End Sub

    Private Sub imgbtnAddAmt_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddAmt.Click
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlExistingVehicleNo.SelectedIndex > 0 Then
                objvlm.LVLD_ID = txtLoanId.Text
                objvlm.LVLD_MasterID = ddlExistingVehicleNo.SelectedValue
                objvlm.LVLD_RegNo = ddlExistingVehicleNo.SelectedItem.Text
                objvlm.LVLD_LoanInsDueDate = txtInstllmntDueDate.Text
                objvlm.LVLD_InstallmentPaidDt = txtInstlmntPaidDt.Text
                objvlm.LVLD_InstallmentPaidAmt = txtInstlmntPaidAmt.Text
                objvlm.LVLD_InstallmentInterestAmt = txtInterestAmt.Text
                objvlm.LVLD_TotalAmt = txttotalAmt.Text
                objvlm.LVLD_Reference = txtReference.Text
                objvlm.LVLD_DelFlag = "X"
                objvlm.LVLD_Status = "W"
                objvlm.LVLD_CreatedBy = sSession.UserID
                objvlm.LVLD_CreatedOn = Date.Today
                objvlm.LVLD_UpdatedBy = sSession.UserID
                objvlm.LVLD_UpdatedOn = Date.Today
                objvlm.LVLD_CompID = sSession.AccessCodeID
                objvlm.LVLD_YearID = sSession.YearID
                objvlm.LVLD_Operation = "C"
                objvlm.LVLD_IPAddress = sSession.IPAddress
                Arr = objvlm.SaveLoanDetails(sSession.AccessCode, objvlm)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    '  ImgbtnDieselSave.ImageUrl = "~/Images/Save16.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
                dgInstallmentDet.Visible = True
                dgInstallmentDet.DataSource = objvlm.LoadInstallmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                dgInstallmentDet.DataBind()
                txtLoanId.Text = 0
                ClearLoanDetails()
            Else
                lblError.Text = "Select Existing Vehicle Registeration No."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddAmt_Click")
        End Try
    End Sub
    Public Sub ClearLoanDetails()
        Try
            lblError.Text = ""
            txtInstllmntDueDate.Text = "" : txtInstlmntPaidDt.Text = "" : txtInterestAmt.Text = ""
            txttotalAmt.Text = "" : txtReference.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlExistingVehicleNo.SelectedIndex > 0 Then
                objvlm.LVLM_ID = lblID.Text
                If ddlExistingVehicleNo.SelectedIndex > 0 Then
                    objvlm.LVLM_MasterID = ddlExistingVehicleNo.SelectedValue
                Else
                    objvlm.LVLM_MasterID = 0
                End If
                objvlm.LVLM_RegNo = ddlExistingVehicleNo.SelectedItem.Text
                objvlm.LVLM_LoanAmount = txtLoanAmt.Text
                objvlm.LVLM_LoanAccNo = txtLoanAccNo.Text
                objvlm.LVLM_BankName = ddlBank.SelectedValue
                objvlm.LVLM_BranchName = txtBranchName.Text
                objvlm.LVLM_LoanDate = txtDateofLoanRec.Text
                objvlm.LVLM_LoanDueDate = txtInstllmntDueDate.Text
                objvlm.LVLM_InstallmentAmt = txtInstallmntAmt.Text

                objvlm.LVLM_Delflag = "W"
                objvlm.LVLM_CompID = sSession.AccessCodeID
                objvlm.LVLM_Status = "C"
                objvlm.LVLM_Operation = "C"
                objvlm.LVLM_IPAddress = sSession.IPAddress
                objvlm.LVLM_CreatedBy = sSession.UserID
                objvlm.LVLM_CreatedOn = Date.Today
                objvlm.LVLM_ApprovedBy = Nothing
                objvlm.LVLM_ApprovedOn = Date.Today
                objvlm.LVLM_DeletedBy = Nothing
                objvlm.LVLM_DeletedOn = Date.Today
                objvlm.LVLM_UpdatedBy = sSession.UserID
                objvlm.LVLM_UpdatedOn = Date.Today
                objvlm.LVLM_YearID = sSession.YearID





                Arr = objvlm.SaveLoanMater(sSession.AccessCode, objvlm)
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


                '  ddlVehicleNo.SelectedValue = Arr(1)
                ddlExistingVehicleNo_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/VehicleMaintanance/LgstLoanDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
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
            Response.Redirect(String.Format("~/VehicleMaintanance/LgstLoanDashboard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Protected Sub txtInterestAmt_TextChanged(sender As Object, e As EventArgs)
        Try
            txttotalAmt.Text = Val(txtInstallmntAmt.Text) + Val(txtInterestAmt.Text)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtInterestAmt_TextChanged")
        End Try
    End Sub

    Private Sub dgInstallmentDet_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgInstallmentDet.ItemCommand
        Dim dtInstallmentDet As New DataTable
        Try
            If e.CommandName = "Edit" Then
                txtLoanId.Text = e.Item.Cells(0).Text
                imgbtnAddAmt.ImageUrl = "~/Images/Update16.png"
                dtInstallmentDet = objvlm.GetLoanDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                If dtInstallmentDet.Rows.Count > 0 Then
                    txtInstllmntDueDate.Text = dtInstallmentDet.Rows(0)("LVLD_LoanDueDate")
                    txtInstlmntPaidDt.Text = dtInstallmentDet.Rows(0)("LVLD_InstallmentPaidDt")
                    txtInstlmntPaidAmt.Text = dtInstallmentDet.Rows(0)("LVLD_InstallmentPaidAmt")
                    txtInterestAmt.Text = dtInstallmentDet.Rows(0)("LVLD_InstallmentInterestAmt")
                    txttotalAmt.Text = dtInstallmentDet.Rows(0)("LVLD_TotalAmt")
                    txtReference.Text = dtInstallmentDet.Rows(0)("LVLD_Reference")
                End If
            ElseIf e.CommandName = "Delete" Then
                objvlm.DeleteLoanValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                dgInstallmentDet.DataSource = objvlm.LoadInstallmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingVehicleNo.SelectedValue, sSession.YearID)
                dgInstallmentDet.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInstallmentDet_ItemCommand")
        End Try
    End Sub

    Private Sub ddlLoanReg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLoanReg.SelectedIndexChanged
        Try
            BindDetails(ddlLoanReg.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLoanReg_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub txttotalAmt_TextChanged(sender As Object, e As EventArgs)
        Dim dDebitAmt As Double = 0.0, dInsuranceAmt As Double = 0.0, dBalanceAmt As Double = 0.0
        Try
            dDebitAmt = objvlm.GetDebitAmt(sSession.AccessCode, sSession.AccessCodeID)
            dInsuranceAmt = objvlm.GetInsuranceAmt(sSession.AccessCode, sSession.AccessCodeID)
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
