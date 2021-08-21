Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class VehicleMaintanance_VehicleAddtnlMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/LgstAddtnlMaster"
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
    Dim objvam As New clsVehicleAddtlnMaster

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
        imgbtnAddTyre.ImageUrl = "~/Images/Add24.png"
        imgbtnCompliances.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            'Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")
            '  RFVChassisNo.ErrorMessage = "Enter Chassis No."
            '  RFVEngineNo.ErrorMessage = "Enter Engine"
            RFVVehicleType.InitialValue = "Select Vehicle Type"
            RFVOwnerName.ErrorMessage = "Enter Owner Name"
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
                lblID.Text = "0"
                txtTyrelId.Text = 0 : txtTyreAddId.Text = 0
                txtComplianceId.Text = 0 : txtComplianceAddId.Text = 0
                BindExistingVehicle()
                bindVehicleType() : BindExistingAdditionalVehicle()
                bindComplianceType()
                'Dim sAssetRefNo As String = ""
                'sAssetRefNo = Request.QueryString("MasterID")
                'If sAssetRefNo <> "" Then
                '    sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LGSTVM")
                '    imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCMDSave = "NO"
                '    If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                '        Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                '        Exit Sub
                '    Else
                '        If sFormButtons.Contains(",View,") = True Then
                '        End If
                '        If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                '            imgbtnSave.Visible = True
                '            sCMDSave = "YES"
                '        End If
                '        If sFormButtons.Contains(",New,") = True Then
                '            imgbtnAdd.Visible = True
                '        End If
                '    End If
                '    ddlVehicleNo.SelectedValue = objGen.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                '    sAssetRefNo = objGen.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                '    ddlVehicleNo_SelectedIndexChanged(sender, e)
                'End If

                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iInvID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlexistingVehicleNo.SelectedValue = iInvID
                    'BindDetails(iInvID)
                    ddlexistingVehicleNo_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch
        End Try
    End Sub
    Protected Sub bindComplianceType()
        Try
            ddlCompliance.DataSource = objvam.LoadComplianceType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompliance.DataTextField = "Mas_Desc"
            ddlCompliance.DataValueField = "Mas_Id"
            ddlCompliance.DataBind()
            ddlCompliance.Items.Insert(0, "Select Compliance Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub bindVehicleType()
        Try
            ddlVehicleType.DataSource = objvam.LoadVehicleType(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlVehicleNo.DataSource = objvam.LoadVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlVehicleNo.DataTextField = "LVM_RegNo"
            ddlVehicleNo.DataValueField = "LVM_ID"
            ddlVehicleNo.DataBind()
            ddlVehicleNo.Items.Insert(0, "Select Vehicle")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindExistingAdditionalVehicle()
        Try
            ddlexistingVehicleNo.DataSource = objvam.LoadAdditionalVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlexistingVehicleNo.DataTextField = "LVAM_RegNo"
            ddlexistingVehicleNo.DataValueField = "LVAM_MasterID"
            ddlexistingVehicleNo.DataBind()
            ddlexistingVehicleNo.Items.Insert(0, "Select Existing Vehicle")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlVehicleNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVehicleNo.SelectedIndexChanged
        Dim icount As Integer
        Try
            lblError.Text = ""
            If ddlVehicleNo.SelectedIndex > 0 Then
                '  icount = objvam.GetRouteCustCount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlVehicleNo.SelectedValue, ddlVehicleNo.SelectedItem.Text)
                '   If icount > 0 Then
                '  BindAllDetails(ddlVehicleNo.SelectedValue)
                '  Else
                BindDetails(ddlVehicleNo.SelectedValue)
                '  End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub BindDetails(ByVal iVehicleId As Integer)
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Try
            dt = objvam.LoadVehicleDetails(sSession.AccessCode, sSession.AccessCodeID, iVehicleId)
            If dt.Rows.Count > 0 Then

                If IsDBNull(dt.Rows(0)("LVM_RegNo")) = False Then
                    txtRegistrationNo.Text = dt.Rows(0)("LVM_RegNo")
                Else
                    txtRegistrationNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_ChassisNo")) = False Then
                    '     txtChassisNo.Text = dt.Rows(0)("LVM_ChassisNo")
                Else
                    '    txtChassisNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_EngineNo")) = False Then
                    '   txtEngineNo.Text = dt.Rows(0)("LVM_EngineNo")
                Else
                    '  txtEngineNo.Text = ""
                End If
                If ddlexistingVehicleNo.SelectedIndex > 0 Then
                    ddlVehicleNo.Enabled = False
                    If IsDBNull(dt.Rows(0)("LVM_ID")) = False Then
                        ddlVehicleNo.SelectedValue = dt.Rows(0)("LVM_ID")
                    Else
                        ddlVehicleNo.SelectedIndex = 0
                    End If
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
                        '    txtServiceCenter.Text = dt.Rows(0)("LVM_ServiceCntrDtls")
                    Else
                        '    txtServiceCenter.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("LVM_VehicleDetails")) = False Then
                        '     txtDetails.Text = dt.Rows(0)("LVM_VehicleDetails")
                    Else
                        '    txtDetails.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("LVM_VehicleDetails")) = False Then
                        '   txtDetails.Text = dt.Rows(0)("LVM_VehicleDetails")
                    Else
                        '    txtDetails.Text = ""
                    End If

                    txtMtrRdng.Text = objvam.GetMeterReading(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlVehicleNo.SelectedValue)

                    dt1 = objvam.LoadVehicleAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlVehicleNo.SelectedValue, txtRegistrationNo.Text)
                If dt1.Rows.Count > 0 Then
                    lblID.Text = dt1.Rows(0)("LVAM_ID")
                    txtTyreAddId.Text = dt1.Rows(0)("LVAM_ID")
                    txtComplianceAddId.Text = dt1.Rows(0)("LVAM_ID")
                    If IsDBNull(dt1.Rows(0)("LVAM_VehiclePurchaseDate")) = False Then
                        txtDateofPurchase.Text = dt1.Rows(0)("LVAM_VehiclePurchaseDate")
                    Else
                        txtDateofPurchase.Text = ""
                    End If
                    If IsDBNull(dt1.Rows(0)("LVAM_VehicleInvoiceNo")) = False Then
                        txtInvNo.Text = dt1.Rows(0)("LVAM_VehicleInvoiceNo")
                    Else
                        txtInvNo.Text = ""
                    End If
                    If IsDBNull(dt1.Rows(0)("LVAM_VehicleAmt")) = False Then
                        txtVehVal.Text = dt1.Rows(0)("LVAM_VehicleAmt")
                    Else
                        txtVehVal.Text = ""
                    End If
                    If IsDBNull(dt1.Rows(0)("LVAM_MasterID")) = False Then
                        ddlexistingVehicleNo.SelectedValue = dt1.Rows(0)("LVAM_MasterID")
                    Else
                        ddlexistingVehicleNo.SelectedIndex = 0
                    End If

                    If IsDBNull(dt1.Rows(0)("LVAM_VehicleDealer")) = False Then
                        txtDealer.Text = dt1.Rows(0)("LVAM_VehicleDealer")
                    Else
                        txtDealer.Text = ""
                    End If
                    If IsDBNull(dt1.Rows(0)("LVAM_VehicleManufacturer")) = False Then
                        txtManfcturer.Text = dt1.Rows(0)("LVAM_VehicleManufacturer")
                    Else
                        txtManfcturer.Text = ""
                    End If
                    If IsDBNull(dt1.Rows(0)("LVAM_DepreciationAmt")) = False Then
                        txtDeprctnVal.Text = dt1.Rows(0)("LVAM_DepreciationAmt")
                    Else
                        txtDeprctnVal.Text = ""
                    End If
                    If IsDBNull(dt1.Rows(0)("LVAM_BatteryNo")) = False Then
                        txtBattryNo.Text = dt1.Rows(0)("LVAM_BatteryNo")
                    Else
                        txtBattryNo.Text = ""
                    End If
                    If IsDBNull(dt1.Rows(0)("LVAM_BatteryFrequency")) = False Then
                        txtBattryFreq.Text = dt1.Rows(0)("LVAM_BatteryFrequency")
                    Else
                        txtBattryFreq.Text = ""
                    End If

                    If (dt1.Rows(0)("LVAM_DelFlag") = "W") Then
                        lblStatus.Text = "Waiting For Approval"
                        txtRegistrationNo.Enabled = False
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                        '  imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                        imgbtnWaiting.Visible = True
                        dgCompliance.Enabled = True : dgTyreDet.Enabled = True
                    End If
                    If (dt1.Rows(0)("LVAM_Delflag") = "A") Then
                        lblStatus.Text = "Approved"
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False
                        dgCompliance.Enabled = False : dgTyreDet.Enabled = False
                        imgbtnAddTyre.Visible = False : imgbtnCompliances.Visible = False
                    End If
                    If (dt1.Rows(0)("LVAM_Delflag") = "D") Then
                        lblStatus.Text = "Deactivated"
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                        '   imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
                        imgbtnWaiting.Visible = False
                    End If
                Else
                    ClearVehcileDetails()
                End If
                Dim dtTyre As New DataTable, dtCompliance As New DataTable
                dtTyre = objvam.LoadTyreDetails(sSession.AccessCode, sSession.AccessCodeID, ddlVehicleNo.SelectedValue, sSession.YearID)
                If dtTyre.Rows.Count > 0 Then
                    dgTyreDet.Visible = True : dgTyreDet.Enabled = True
                    dgTyreDet.DataSource = dtTyre
                    dgTyreDet.DataBind()
                Else
                    dgTyreDet.DataSource = Nothing
                    dgTyreDet.DataBind()
                End If
                dtCompliance = objvam.LoadComplianceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlVehicleNo.SelectedValue, sSession.YearID)
                If dtCompliance.Rows.Count > 0 Then
                    dgCompliance.Visible = True : dgTyreDet.Enabled = True
                    dgCompliance.DataSource = dtCompliance
                    dgCompliance.DataBind()
                Else
                    dgCompliance.DataSource = Nothing
                    dgCompliance.DataBind()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub ClearVehcileDetails()
        Try
            lblError.Text = ""
            txtDateofPurchase.Text = "" : txtInvNo.Text = "" : txtVehVal.Text = "" : txtDealer.Text = ""
            txtManfcturer.Text = "" : txtDeprctnVal.Text = "" : txtBattryNo.Text = "" : txtBattryFreq.Text = ""
            ddlexistingVehicleNo.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/VehicleMaintanance/LgstAddtnlDashboard.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = "" : lblStatus.Text = ""
            lblID.Text = "0"
            ddlVehicleNo.SelectedIndex = 0
            txtTyrelId.Text = 0 : txtTyreAddId.Text = 0
            txtComplianceId.Text = 0 : txtComplianceAddId.Text = 0
            txtRegistrationNo.Text = "" : ddlVehicleType.SelectedIndex = 0 : txtOwnerName.Text = ""
            txtRegistrationNo.Enabled = True
            imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnSave.Visible = True : imgbtnAttachment.Visible = False
            lblBadgeCount.Visible = False : ddlVehicleNo.Enabled = True : ddlexistingVehicleNo.SelectedIndex = 0
            dgCompliance.Enabled = True : dgTyreDet.Enabled = True
            imgbtnAddTyre.Visible = True : imgbtnCompliances.Visible = True
            txtDateofPurchase.Text = "" : txtMtrRdng.Text = "" : txtInvNo.Text = "" : txtVehVal.Text = ""
            txtDealer.Text = "" : txtManfcturer.Text = "" : txtDeprctnVal.Text = "" : txtBattryNo.Text = "" : txtBattryFreq.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub imgbtnAddTyre_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddTyre.Click
        Dim Arr() As String
        lblError.Text = ""
        Try
            lblError.Text = ""
            If ddlexistingVehicleNo.SelectedIndex > 0 Then
                objvam.LVTM_ID = txtTyrelId.Text
                objvam.LVTM_MasterID = txtTyreAddId.Text
                objvam.LVTM_AddtlnVehicleID = ddlexistingVehicleNo.SelectedValue
                objvam.LVTM_TyreSLNo = txtTyreNo.Text
                objvam.LVTM_TyreFreq = txtTyreFreq.Text



                objvam.LVTM_DelFlag = "X"
                objvam.LVTM_Status = "W"
                objvam.LVTM_CreatedBy = sSession.UserID
                objvam.LVTM_CreatedOn = Date.Today
                objvam.LVTM_UpdatedBy = sSession.UserID
                objvam.LVTM_UpdatedOn = Date.Today
                objvam.LVTM_CompID = sSession.AccessCodeID
                objvam.LVTM_YearID = sSession.YearID
                objvam.LVTM_Operation = "C"
                objvam.LVTM_IPAddress = sSession.IPAddress
                Arr = objvam.SaveTyreDetails(sSession.AccessCode, objvam)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    '    imgbtnAddHopOn.ImageUrl = "~/Images/Save16.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
                dgTyreDet.Visible = True
                dgTyreDet.DataSource = objvam.LoadTyreDetails(sSession.AccessCode, sSession.AccessCodeID, ddlVehicleNo.SelectedValue, sSession.YearID)
                dgTyreDet.DataBind()
                txtTyrelId.Text = 0
            Else
                lblError.Text = "Select Existing Vehicle Registeration No."
                Exit Sub
            End If
            dgTyreDet.Visible = True
            '    dgTyreDet.DataSource = objvam.BindHopOnDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.YearID)
            dgTyreDet.DataBind()
            txtTyrelId.Text = 0
            ClearTyreDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddTyre_Click")
        End Try
    End Sub
    Public Sub ClearTyreDetails()
        Try
            lblError.Text = ""
            txtTyreNo.Text = "" : txtTyreFreq.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlVehicleNo.SelectedIndex > 0 Then
                objvam.LVAM_ID = lblID.Text
                If ddlVehicleNo.SelectedIndex > 0 Then
                    objvam.LVAM_MasterID = ddlVehicleNo.SelectedValue
                Else
                    objvam.LVAM_MasterID = 0
                End If
                objvam.LVAM_RegNo = txtRegistrationNo.Text
                objvam.LVAM_VehiclePurchaseDate = txtDateofPurchase.Text
                objvam.LVAM_TotalMeterValue = txtMtrRdng.Text
                objvam.LVAM_VehicleInvoiceNo = txtInvNo.Text

                objvam.LVAM_VehicleAmt = txtVehVal.Text
                objvam.LVAM_VehicleDealer = txtDealer.Text
                objvam.LVAM_VehicleManufacturer = txtManfcturer.Text
                objvam.LVAM_DepreciationAmt = txtDeprctnVal.Text
                objvam.LVAM_BatteryNo = txtBattryNo.Text
                objvam.LVAM_BatteryFreq = txtBattryFreq.Text


                objvam.LVAM_Delflag = "W"
                objvam.LVAM_CompID = sSession.AccessCodeID
                objvam.LVAM_Status = "C"
                objvam.LVAM_Operation = "C"
                objvam.LVAM_IPAddress = sSession.IPAddress
                objvam.LVAM_CreatedBy = sSession.UserID
                objvam.LVAM_CreatedOn = Date.Today
                objvam.LVAM_ApprovedBy = Nothing
                objvam.LVAM_ApprovedOn = Date.Today
                objvam.LVAM_DeletedBy = Nothing
                objvam.LVAM_DeletedOn = Date.Today
                objvam.LVAM_UpdatedBy = sSession.UserID
                objvam.LVAM_UpdatedOn = Date.Today
                objvam.LVAM_YearID = sSession.YearID





                Arr = objvam.SaveVehicleDetails(sSession.AccessCode, objvam)
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
                BindExistingAdditionalVehicle()

                '  ddlVehicleNo.SelectedValue = Arr(1)
                ddlVehicleNo_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub

    Private Sub imgbtnCompliances_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCompliances.Click
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlexistingVehicleNo.SelectedIndex > 0 Then
                objvam.LVCM_ID = txtComplianceId.Text
                objvam.LVCM_MasterID = ddlVehicleNo.SelectedValue
                objvam.LVCM_AddtlnVehicleID = txtComplianceAddId.Text
                objvam.LVCM_ComplianceID = ddlCompliance.SelectedValue
                If txtFreqinKM.Text = "" Then
                    objvam.LVCM_ComplianceFreqInKM = 0
                Else
                    objvam.LVCM_ComplianceFreqInKM = txtFreqinKM.Text
                End If
                If txtFreqinYears.Text = "" Then
                    objvam.LVCM_ComplianceFreqInYear = 0
                Else
                    objvam.LVCM_ComplianceFreqInYear = txtFreqinYears.Text
                End If
                objvam.LVCM_DelFlag = "X"
                objvam.LVCM_Status = "W"
                    objvam.LVCM_CreatedBy = sSession.UserID
                    objvam.LVCM_CreatedOn = Date.Today
                    objvam.LVCM_UpdatedBy = sSession.UserID
                    objvam.LVCM_UpdatedOn = Date.Today
                    objvam.LVCM_CompID = sSession.AccessCodeID
                    objvam.LVCM_YearID = sSession.YearID
                    objvam.LVCM_Operation = "C"
                    objvam.LVCM_IPAddress = sSession.IPAddress
                    Arr = objvam.SaveComplianceDetails(sSession.AccessCode, objvam)
                    If Arr(0) = "2" Then
                        lblError.Text = "Successfully Updated"
                        '  ImgbtnDieselSave.ImageUrl = "~/Images/Save16.png"
                    ElseIf Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved"
                    End If
                    dgCompliance.Visible = True
                    dgCompliance.DataSource = objvam.LoadComplianceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlVehicleNo.SelectedValue, sSession.YearID)
                    dgCompliance.DataBind()
                    txtComplianceId.Text = 0
                    ClearComplianceDetails()
                Else
                    lblError.Text = "Select Existing Vehicle Registeration No."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnCompliances_Click")
        End Try
    End Sub
    Public Sub ClearComplianceDetails()
        Try
            lblError.Text = ""
            ddlCompliance.SelectedIndex = 0 : txtFreqinKM.Text = "" : txtFreqinYears.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dgTyreDet_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgTyreDet.ItemCommand
        Dim dtTyreDet As New DataTable
        Try
            If e.CommandName = "Edit" Then
                txtTyrelId.Text = e.Item.Cells(0).Text
                imgbtnAddTyre.ImageUrl = "~/Images/Update16.png"
                dtTyreDet = objvam.GetTyreDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlVehicleNo.SelectedValue, sSession.YearID)
                If dtTyreDet.Rows.Count > 0 Then
                    txtTyreNo.Text = dtTyreDet.Rows(0)("LVTM_TyreSLNo")
                    txtTyreFreq.Text = dtTyreDet.Rows(0)("LVTM_TyreFreq")
                End If
            ElseIf e.CommandName = "Delete" Then
                objvam.DeleteTyreValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlVehicleNo.SelectedValue, sSession.YearID)
                dgTyreDet.DataSource = objvam.LoadTyreDetails(sSession.AccessCode, sSession.AccessCodeID, ddlVehicleNo.SelectedValue, sSession.YearID)
                dgTyreDet.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgTyreDet_ItemCommand")
        End Try
    End Sub

    Private Sub dgCompliance_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgCompliance.ItemCommand
        Dim dtComplianceDet As New DataTable
        Try
            If e.CommandName = "Edit" Then
                txtComplianceId.Text = e.Item.Cells(0).Text
                imgbtnCompliances.ImageUrl = "~/Images/Update16.png"
                dtComplianceDet = objvam.GetComplianceDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlVehicleNo.SelectedValue, sSession.YearID)
                If dtComplianceDet.Rows.Count > 0 Then
                    ddlCompliance.SelectedValue = dtComplianceDet.Rows(0)("LVCM_ComplianceID")
                    txtFreqinKM.Text = dtComplianceDet.Rows(0)("LVCM_ComplianceFreqInKM")
                    txtFreqinYears.Text = dtComplianceDet.Rows(0)("LVCM_ComplianceFreqInYear")
                End If
            ElseIf e.CommandName = "Delete" Then
                objvam.DeleteComplianceValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlVehicleNo.SelectedValue, sSession.YearID)
                dgCompliance.DataSource = objvam.LoadComplianceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlVehicleNo.SelectedValue, sSession.YearID)
                dgCompliance.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCompliance_ItemCommand")
        End Try
    End Sub

    Private Sub ddlexistingVehicleNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlexistingVehicleNo.SelectedIndexChanged
        Try
            BindDetails(ddlexistingVehicleNo.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlexistingVehicleNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            If ddlexistingVehicleNo.SelectedIndex > 0 Then
                imgbtnSave_Click(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub

    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            If ddlexistingVehicleNo.SelectedIndex > 0 Then
                objvam.UpdateAdditionalVehicle(sSession.AccessCode, sSession.AccessCodeID, ddlexistingVehicleNo.SelectedValue, sSession.YearID)
                lblError.Text = "Successfully Approved"
                lblStatus.Text = "Successfully Approved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False
                dgCompliance.Enabled = False : dgTyreDet.Enabled = False
                imgbtnAddTyre.Visible = False : imgbtnCompliances.Visible = False

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
End Class
