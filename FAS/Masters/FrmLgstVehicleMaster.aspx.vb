Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Masters_FrmLgstVehicleMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/DriverMasters"
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
    Dim objVehicleMas As New clsVehicleMaster
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
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            'Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")
            RFVRegistrationNo.ErrorMessage = "Enter Vehicle Registration No"
            REVRegistrationNo.ErrorMessage = "Enter valid registration no"
            RFVChassisNo.ErrorMessage = "Enter Chassis No."
            RFVEngineNo.ErrorMessage = "Enter Engine"
            RFVVehicleType.InitialValue = "Select Vehicle Type"
            RFVOwnerName.ErrorMessage = "Enter Owner Name"
            RFVInsuranceType.InitialValue = "Select Insurance Type"
            RFVPolicyNo.ErrorMessage = "Enter Insurance no"
            RFVPolicyAmt.ErrorMessage = "Enter Policy Amount"
            RFVExpiryDate.ErrorMessage = "Enter expiry date"
            RFVPolicyDetails.ErrorMessage = "Enter policy details"
            sSession = Session("AllSession")
            If IsPostBack = False Then
                lblID.Text = "0"
                BindExistingVehicle()
                bindVehicleType()
                Dim sAssetRefNo As String = ""
                sAssetRefNo = Request.QueryString("MasterID")

                If sAssetRefNo <> "" Then
                    sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LGSTVM")
                    imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCMDSave = "NO"
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
                    ddlExistingVehicleNo.SelectedValue = objGen.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                    sAssetRefNo = objGen.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                    ddlExistingVehicleNo_SelectedIndexChanged(sender, e)
                End If

            End If
        Catch
        End Try
    End Sub
    Protected Sub bindVehicleType()
        Try
            ddlVehicleType.DataSource = objVehicleMas.LoadVehicleType(sSession.AccessCode, sSession.AccessCodeID)
            ddlVehicleType.DataTextField = "Mas_Desc"
            ddlVehicleType.DataValueField = "Mas_Id"
            ddlVehicleType.DataBind()
            ddlVehicleType.Items.Insert(0, "Select Vehicle Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindExistingVehicle()
        Try
            ddlExistingVehicleNo.DataSource = objVehicleMas.LoadVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingVehicleNo.DataTextField = "LVM_RegNo"
            ddlExistingVehicleNo.DataValueField = "LVM_ID"
            ddlExistingVehicleNo.DataBind()
            ddlExistingVehicleNo.Items.Insert(0, "Select Existing Vehicle")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sSubGrpcode As String = "", sGLCode As String = ""
        Dim lParentId As Integer = 0, iHead As Integer = 2
        Dim bCheck As Boolean
        Try
            lblError.Text = ""
            If ddlVehicleType.SelectedIndex > 0 Then
                If ddlExistingVehicleNo.SelectedIndex > 0 Then
                Else
                    If txtRegistrationNo.Text <> "" Then
                        bCheck = objVehicleMas.CheckDuplicateRegNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtRegistrationNo.Text))
                        If bCheck = True Then
                            lblError.Text = "This Vehicle.No is already Exist"
                            Exit Sub
                        End If
                    End If
                End If
                objVehicleMas.LVM_ID = lblID.Text
                objVehicleMas.LVM_RegNo = txtRegistrationNo.Text
                objVehicleMas.LVM_ChassisNo = txtChassisNo.Text
                objVehicleMas.LVM_EngineNo = txtEngineNo.Text
                objVehicleMas.LVM_VehicleType = ddlVehicleType.SelectedValue
                objVehicleMas.LVM_OwnerName = txtOwnerName.Text
                objVehicleMas.LVM_ServiceCntrDtls = txtServiceCenter.Text
                objVehicleMas.LVM_VehicleDetails = txtDetails.Text
                objVehicleMas.LVM_InsuranceType = ddlInsuranceType.SelectedIndex
                objVehicleMas.LVM_InsuranceNo = txtPolicyNo.Text
                objVehicleMas.LVM_InsuranceAmt = txtPolicyAmt.Text
                objVehicleMas.LVM_InsuranceExpDate = Date.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objVehicleMas.LVM_InsuranceDetails = TxtPolicyDetails.Text
                If ddlExistingVehicleNo.SelectedIndex > 0 Then
                    objVehicleMas.LVM_Delflag = "A"
                Else
                    objVehicleMas.LVM_Delflag = "W"
                End If
                objVehicleMas.LVM_Status = "W"
                objVehicleMas.LVM_CreatedBy = sSession.UserID
                objVehicleMas.LVM_CreatedOn = Date.Today
                objVehicleMas.LVM_ApprovedBy = Nothing
                objVehicleMas.LVM_ApprovedOn = Date.Today
                objVehicleMas.LVM_DeletedBy = Nothing
                objVehicleMas.LVM_DeletedOn = Date.Today
                objVehicleMas.LVM_UpdatedBy = sSession.UserID
                objVehicleMas.LVM_UpdatedOn = Date.Today
                objVehicleMas.LVM_RecalldBy = Nothing


                'Dim sPerm As String = ""
                'Dim sArray1 As Array
                'sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Vehicle", "Vehicle")
                'sPerm = sPerm.Remove(0, 1)
                'sArray1 = sPerm.Split(",")

                'iHead = sArray1(0) '1
                'objVehicleMas.LVM_Group = sArray1(1) '29
                'objVehicleMas.LVM_SubGroup = sArray1(2) '31
                'objVehicleMas.LVM_GL = sArray1(3) '146

                'If txtGLID.Text > 0 Then
                '    objVehicleMas.LVM_SubGL = CreateChartOfAccounts(Trim(txtRegistrationNo.Text), 3, objVehicleMas.LVM_GL, 1, "Update")
                'Else
                '    objVehicleMas.LVM_SubGL = CreateChartOfAccounts(Trim(txtRegistrationNo.Text), 3, objVehicleMas.LVM_GL, 1, "Save")
                'End If

                objVehicleMas.LVM_Group = 0
                objVehicleMas.LVM_SubGroup = 0
                objVehicleMas.LVM_GL = 0
                objVehicleMas.LVM_SubGL = 0
                objVehicleMas.LVM_CompID = sSession.AccessCodeID
                objVehicleMas.LVM_IPAddress = sSession.IPAddress
                objVehicleMas.LVM_YearID = sSession.YearID

                objVehicleMas.LVM_Operation = "W"





                Arr = objVehicleMas.SaveVehicleDetails(sSession.AccessCode, objVehicleMas)
                txtGLID.Text = 0

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    lblVehicleValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblVehicleValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                BindExistingVehicle()
                ddlExistingVehicleNo.SelectedValue = Arr(1)
                ddlExistingVehicleNo_SelectedIndexChanged(sender, e)
            Else
                lblError.Text = "Select Vehicle Type"
                lblVehicleValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
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
            dt = objVehicleMas.LoadVehicleDetails(sSession.AccessCode, sSession.AccessCodeID, iVehicleId)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LVM_Id")
                If IsDBNull(dt.Rows(0)("LVM_RegNo")) = False Then
                    txtRegistrationNo.Text = dt.Rows(0)("LVM_RegNo")
                Else
                    txtRegistrationNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_ChassisNo")) = False Then
                    txtChassisNo.Text = dt.Rows(0)("LVM_ChassisNo")
                Else
                    txtChassisNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_EngineNo")) = False Then
                    txtEngineNo.Text = dt.Rows(0)("LVM_EngineNo")
                Else
                    txtEngineNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_VehicleType")) = False Then
                    ddlVehicleType.SelectedValue = dt.Rows(0)("LVM_VehicleType")
                Else
                    ddlVehicleType.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("LVM_OwnerName")) = False Then
                    txtOwnerName.Text = dt.Rows(0)("LVM_OwnerName")
                Else
                    txtOwnerName.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_ServiceCntrDtls")) = False Then
                    txtServiceCenter.Text = dt.Rows(0)("LVM_ServiceCntrDtls")
                Else
                    txtServiceCenter.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_VehicleDetails")) = False Then
                    txtDetails.Text = dt.Rows(0)("LVM_VehicleDetails")
                Else
                    txtDetails.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_VehicleDetails")) = False Then
                    txtDetails.Text = dt.Rows(0)("LVM_VehicleDetails")
                Else
                    txtDetails.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_InsuranceType")) = False Then
                    ddlInsuranceType.SelectedIndex = dt.Rows(0)("LVM_InsuranceType")
                Else
                    ddlInsuranceType.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("LVM_InsuranceNo")) = False Then
                    txtPolicyNo.Text = dt.Rows(0)("LVM_InsuranceNo")
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
                    If (dt.Rows(0)("LVM_DelFlag") = "W") Then
                    lblStatus.Text = "Waiting For Approval"
                    txtRegistrationNo.Enabled = False
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                    imgbtnWaiting.Visible = True
                End If
                If (dt.Rows(0)("LVM_Delflag") = "A") Then
                    lblStatus.Text = "Approved"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                    imgbtnWaiting.Visible = False
                End If
                If (dt.Rows(0)("LVM_Delflag") = "D") Then
                    lblStatus.Text = "Deactivated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                    imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
                    imgbtnWaiting.Visible = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            lblError.Text = ""
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Acc_Head").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Head").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_Group").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Group").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGroup").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGroup").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_GL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_GL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                Return sPerm
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDetailsSetttings")
        End Try
    End Function
    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer, ByVal sStatus As String) As Integer
        Dim sRet As String = ""
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try
            lblError.Text = ""
            objCOA.igl_id = 0
            objCOA.igl_head = iHead
            objCOA.igl_Parent = iParent
            objCOA.sgl_glcode = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, iAccHead, iParent)
            objCOA.sgl_Desc = objGen.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(sName)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = iAccHead
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "A"
            objCOA.sgl_IPAddress = sSession.IPAddress

            If sStatus = "Save" Then
                sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            ElseIf sStatus = "Update" Then
                objCOA.igl_id = txtGLID.Text
                sRet = objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            End If
            sArray = sRet.Split(",")
            Return sArray(0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CreateChartOfAccounts")
        End Try
    End Function
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/Masters/FrmLgstVehicleDashBoard.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub ddlInsuranceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInsuranceType.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlInsuranceType.SelectedIndex > 0 Then
                txtPolicyNo.Enabled = True : txtPolicyAmt.Enabled = True : txtExpiryDate.Enabled = True
                TxtPolicyDetails.Enabled = True
            Else
                txtPolicyNo.Enabled = False : txtPolicyAmt.Enabled = False : txtExpiryDate.Enabled = False
                TxtPolicyDetails.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInsuranceType_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = "" : lblStatus.Text = ""
            lblID.Text = "0"
            ddlExistingVehicleNo.SelectedIndex = 0
            txtRegistrationNo.Text = "" : txtChassisNo.Text = "" : txtEngineNo.Text = ""
            ddlVehicleType.SelectedIndex = 0 : txtOwnerName.Text = "" : ddlInsuranceType.SelectedIndex = 0
            txtPolicyNo.Text = "" : txtPolicyAmt.Text = "" : txtExpiryDate.Text = "" : TxtPolicyDetails.Text = ""
            txtServiceCenter.Text = "" : txtDetails.Text = "" : txtRegistrationNo.Enabled = True
            imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnSave.Visible = True : imgbtnAttachment.Visible = False
            lblBadgeCount.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            imgbtnSave_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub

    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            lblError.Text = ""
            If ddlExistingVehicleNo.SelectedIndex > 0 Then
                objVehicleMas.UpdateVehicleStatus(sSession.AccessCode, sSession.AccessCodeID, lblID.Text, sSession.UserID, sSession.IPAddress, sSession.YearID)
                lblStatus.Text = "Approved"
                lblError.Text = "Successfully Approved"
                lblVehicleValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                imgbtnUpdate.Visible = True : imgbtnWaiting.Visible = False : imgbtnSave.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
