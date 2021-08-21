Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Masters_FrmLgstDieselMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters/FrmLgstDieselMaster"
    Dim objCSM As New clsCustomer
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Private objDPM As New clsDieselPumpMaster

    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Dim objCOA As New clsChartOfAccounts
    Private Shared sCMDSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sStr As String = ""
        Dim sFormButtons As String = ""
        Try
            RFVDieselName.ErrorMessage = "Enter Diesel/Petrol Pump Name"
            REVMobile.ErrorMessage = "Enter Valid Mobile No."
            RFVMobileNo.ErrorMessage = "Enter Contact Number"
            RFVRegistration.ErrorMessage = "Enter Registration No"
            RFVGSTNo.ErrorMessage = "Enter GSTN No"
            REVGSTNo.ErrorMessage = "Enter Valid GSTN Reg.No."


            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LGSTDM")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnBankSave.Visible = False : sCMDSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        imgbtnBankSave.Visible = True
                        sCMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If

                Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")
                divBankDetails.Visible = True
                txtBankID.Text = 0

                txtGLID.Text = 0



                BindExistingDieselNo()

                imgbtnUpdate.Visible = False
                'imgbtnAdd.Visible = True
                BindState()
                bindCity()
                lblID.Text = "0"


                'Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")

                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iPumpID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlExistingDieselNo.SelectedValue = iPumpID
                    BindDetails(iPumpID)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub BindExistingDieselNo()
        Try
            ddlExistingDieselNo.DataSource = objDPM.LoadExistingDieselRegNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingDieselNo.DataTextField = "LPM_PumpRegNo"
            ddlExistingDieselNo.DataValueField = "LPM_Id"
            ddlExistingDieselNo.DataBind()
            ddlExistingDieselNo.Items.Insert(0, "Select Existing Diesel Pump RegNo")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindState()
        Try
            ddlState.DataSource = objCSM.LoadState(sSession.AccessCode, sSession.AccessCodeID)
            ddlState.DataTextField = "Mas_Desc"
            ddlState.DataValueField = "Mas_Id"
            ddlState.DataBind()
            ddlState.Items.Insert(0, "Select State")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub bindCity()
        Try
            ddlCity.DataSource = objCSM.LoadCity(sSession.AccessCode, sSession.AccessCodeID)
            ddlCity.DataTextField = "Mas_Desc"
            ddlCity.DataValueField = "Mas_Id"
            ddlCity.DataBind()
            ddlCity.Items.Insert(0, "Select City")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDetails(ByVal iDieselPumpID As Integer)
        Dim dt As New DataTable

        Try
            dt = objDPM.LoadDieseMasterlDetails(sSession.AccessCode, sSession.AccessCodeID, iDieselPumpID)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LPM_Id")
                If IsDBNull(dt.Rows(0)("LPM_PumpRegNo")) = False Then
                    txtRegistrationNo.Text = dt.Rows(0)("LPM_PumpRegNo")
                Else
                    txtRegistrationNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPM_PumpName")) = False Then
                    txtDieselName.Text = dt.Rows(0)("LPM_PumpName")
                Else
                    txtDieselName.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPM_ContactPerson")) = False Then
                    txtContactPerson.Text = dt.Rows(0)("LPM_ContactPerson")
                Else
                    txtContactPerson.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPM_MobNo")) = False Then
                    txtMobileNo.Text = dt.Rows(0)("LPM_MobNo")
                Else
                    txtMobileNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPM_Details")) = False Then
                    txtDetails.Text = dt.Rows(0)("LPM_Details")
                Else
                    txtDetails.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPM_GstNo")) = False Then
                    txtGSTNo.Text = dt.Rows(0)("LPM_GstNo")
                Else
                    txtGSTNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPM_Address")) = False Then
                    txtAddress.Text = dt.Rows(0)("LPM_Address")
                Else
                    txtAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LPM_PinCode")) = False Then
                    txtPinCode.Text = dt.Rows(0)("LPM_PinCode")
                Else
                    txtPinCode.Text = ""
                End If


                If IsDBNull(dt.Rows(0)("LPM_City")) = False Then
                    If dt.Rows(0)("LPM_City") > 0 Then
                        ddlCity.SelectedValue = dt.Rows(0)("LPM_City")
                    Else
                        ddlCity.SelectedIndex = 0
                    End If
                Else
                    ddlCity.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LPM_State")) = False Then
                    If dt.Rows(0)("LPM_State") > 0 Then
                        ddlState.SelectedValue = dt.Rows(0)("LPM_State")
                    Else
                        ddlState.SelectedIndex = 0
                    End If
                Else
                    ddlState.Items.Clear()
                End If



                If (dt.Rows(0)("LPM_Delflag") = "W") Then
                    lblError.Text = "Waiting For Approval"
                    'btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LPM_Delflag") = "D") Then
                    ' btnDelete.Text = "ReCall"
                Else
                    'btnDelete.Text = "Delete"
                End If

                imgbtnSave.Visible = False
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                ' BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                If (dt.Rows(0)("LPM_Delflag") = "X") Then
                    lblStatus.Text = "Waiting For Approval(After De-Activate)"
                    imgbtnWaiting.Visible = True
                    imgbtnUpdate.Visible = False
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LPM_Delflag") = "D") Then
                    lblStatus.Text = "De-Activated"
                    imgbtnUpdate.Visible = False
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LPM_Delflag") = "A") Then
                    lblStatus.Text = "Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "Delete"
                End If
                If (dt.Rows(0)("LPM_Delflag") = "Y") Then
                    lblStatus.Text = "Waiting For Approval(After Activate)"
                    imgbtnWaiting.Visible = True
                    imgbtnUpdate.Visible = False
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LPM_Delflag") = "W") Then
                    lblStatus.Text = "Waiting For Approval"
                    imgbtnWaiting.Visible = True
                    imgbtnUpdate.Visible = False
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If

                If IsDBNull(dt.Rows(0)("LPM_SubGL")) = False Then
                    txtGLID.Text = dt.Rows(0)("LPM_SubGL")
                Else
                    txtGLID.Text = 0
                End If


                dgBankDetails.DataSource = objDPM.BindPumpBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingDieselNo.SelectedValue)
                dgBankDetails.DataBind()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sSubGrpcode As String = "", sGLCode As String = ""
        Dim lParentId As Integer = 0, iHead As Integer = 2
        Dim bCheck As Boolean
        Try
            If txtDieselName.Text = "" Or txtContactPerson.Text = "" Or txtMobileNo.Text = "" Or txtRegistrationNo.Text = "" Or txtGSTNo.Text = "" Then
                Exit Sub
            Else
                If ddlExistingDieselNo.SelectedIndex > 0 Then
                Else
                    If txtGSTNo.Text <> "" Then
                        bCheck = objDPM.CheckGSTNODuplicate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtGSTNo.Text))
                        If bCheck = True Then
                            lblError.Text = "This GSTNO. Is already Exist"
                            Exit Sub
                        End If
                    End If
                    If txtDieselName.Text <> "" Then
                        bCheck = objDPM.CheckPumpNameDuplicate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtDieselName.Text))
                        If bCheck = True Then
                            lblError.Text = "This Pump Name Is already Exist"
                            Exit Sub
                        End If
                    End If
                    If txtRegistrationNo.Text <> "" Then
                        bCheck = objDPM.CheckRegNoDuplicate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtRegistrationNo.Text))
                        If bCheck = True Then
                            lblError.Text = "This Registration Number Is already Exist"
                            Exit Sub
                        End If
                    End If
                End If

                objDPM.LPM_ID = lblID.Text
                objDPM.LPM_PumpName = txtDieselName.Text
                objDPM.LPM_PumpRegNo = txtRegistrationNo.Text
                objDPM.LPM_ContactPerson = txtContactPerson.Text
                objDPM.LPM_MobNo = txtMobileNo.Text
                objDPM.LPM_Details = txtDetails.Text
                objDPM.LPM_GstNo = txtGSTNo.Text
                objDPM.LPM_Address = txtAddress.Text

                objDPM.LPM_Pincode = txtPinCode.Text
                If ddlCity.SelectedIndex > 0 Then
                    objDPM.LPM_City = ddlCity.SelectedValue
                Else
                    objDPM.LPM_City = 0
                End If
                If ddlState.SelectedIndex > 0 Then
                    objDPM.LPM_state = ddlState.SelectedValue
                Else
                    objDPM.LPM_state = 0
                End If
                If ddlExistingDieselNo.SelectedIndex > 0 Then
                    objDPM.LPM_Delflag = "A"
                    objDPM.LPM_Status = "A"
                Else
                    objDPM.LPM_Delflag = "W"
                    objDPM.LPM_Status = "C"
                End If
                objDPM.LPM_CompID = sSession.AccessCodeID

                objDPM.LPM_Operation = "C"
                    objDPM.LPM_IPAddress = sSession.IPAddress
                    objDPM.LPM_CreatedBy = sSession.UserID
                    objDPM.LPM_CreatedOn = Date.Today
                    objDPM.LPM_ApprovedBy = Nothing
                    objDPM.LPM_ApprovedOn = Date.Today
                    objDPM.LPM_DeletedBy = Nothing
                    objDPM.LPM_DeletedOn = Date.Today
                    objDPM.LPM_UpdatedBy = sSession.UserID
                    objDPM.LPM_UpdatedOn = Date.Today
                    objDPM.LPM_YearID = sSession.YearID



                Dim sPerm As String = ""
                Dim sArray1 As Array
                sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier")
                sPerm = sPerm.Remove(0, 1)
                sArray1 = sPerm.Split(",")
                iHead = sArray1(0) '1
                objDPM.LPM_Group = sArray1(1) '29
                objDPM.LPM_SubGroup = sArray1(2) '31
                objDPM.LPM_GL = sArray1(3) '146

                'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
                If txtGLID.Text > 0 Then
                    objDPM.LPM_SubGL = CreateChartOfAccounts(Trim(txtDieselName.Text), 3, objDPM.LPM_GL, 4, "Update")
                Else
                    objDPM.LPM_SubGL = CreateChartOfAccounts(Trim(txtDieselName.Text), 3, objDPM.LPM_GL, 4, "Save")
                End If

                If txtGSTNo.Text <> "" Then
                        objDPM.LPM_GstNo = txtGSTNo.Text
                    Else
                        objDPM.LPM_GstNo = ""
                    End If



                    Arr = objDPM.SavePumpDetails(sSession.AccessCode, objDPM)
                    txtGLID.Text = 0

                    If Arr(0) = "2" Then
                        lblError.Text = "Successfully Updated"
                        lblDieselValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = False
                    'End If
                    imgbtnSave.Visible = False 'btnDelete.Visible = True
                        'btnSave.Text = "Save"
                    ElseIf Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved"
                        lblDieselValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        If sCMDSave = "YES" Then
                            imgbtnUpdate.Visible = True
                        End If  'btnDelete.Visible = True
                        imgbtnSave.Visible = False
                        ' btnSave.Text = "Update"
                        lblStatus.Text = "Waiting For Approval"
                    End If
                    BindExistingDieselNo()
                    ddlExistingDieselNo.SelectedValue = Arr(1)
                    ddlExistingDieselNo_SelectedIndexChanged(sender, e)
                End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub

    Private Sub ddlExistingDieselNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingDieselNo.SelectedIndexChanged
        Try
            If ddlExistingDieselNo.SelectedIndex > 0 Then
                BindDetails(ddlExistingDieselNo.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingDieselNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Masters/FrmLgstDieselMasterDashboard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
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
    Public Sub ClearBankDetails()
        Try
            txtAccountNo.Text = "" : txtBankName.Text = "" : txtIFSCCode.Text = "" : txtBranchName.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgBankDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBankDetails.ItemCommand
        Dim dtBank As New DataTable
        Try
            If e.CommandName = "Edit" Then
                txtBankID.Text = e.Item.Cells(0).Text
                imgbtnBankSave.ImageUrl = "~/Images/Update16.png"
                dtBank = objDPM.GetBankDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingDieselNo.SelectedValue)
                If dtBank.Rows.Count > 0 Then
                    txtAccountNo.Text = dtBank.Rows(0)("LPD_AccountNo")
                    txtBankName.Text = dtBank.Rows(0)("LPD_BankName")
                    txtIFSCCode.Text = dtBank.Rows(0)("LPD_IFSC")
                    txtBranchName.Text = dtBank.Rows(0)("LPD_Branch")
                End If
            ElseIf e.CommandName = "Delete" Then
                objDPM.DeleteBankValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingDieselNo.SelectedValue)
                dgBankDetails.DataSource = objDPM.BindPumpBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingDieselNo.SelectedValue)
                dgBankDetails.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgBankDetails_ItemCommand")
        End Try
    End Sub
    Private Sub imgbtnBankSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBankSave.Click
        Dim Arr() As String
        Try
            If ddlExistingDieselNo.SelectedIndex > 0 Then
                If txtAccountNo.Text = "" Or txtBankName.Text = "" Or txtBranchName.Text = "" Or txtIFSCCode.Text = "" Then
                    lblError.Text = "Enter Complete Bank Details"
                    Exit Sub
                Else
                    objDPM.LPD_ID = txtBankID.Text
                    objDPM.LPD_PumpID = ddlExistingDieselNo.SelectedValue
                    objDPM.LPD_AccountNo = txtAccountNo.Text
                    objDPM.LPD_BankName = txtBankName.Text
                    objDPM.LPD_IFSC = txtIFSCCode.Text
                    objDPM.LPD_Branch = txtBranchName.Text
                    objDPM.LPD_DelFlag = "X"
                    objDPM.LPD_Status = "W"
                    objDPM.LPD_CreatedBy = sSession.UserID
                    objDPM.LPD_CreatedOn = Date.Today
                    objDPM.LPD_UpdatedBy = sSession.UserID
                    objDPM.LPD_UpdatedOn = Date.Today
                    objDPM.LPD_CompID = sSession.AccessCodeID
                    objDPM.LPD_YearID = sSession.YearID
                    objDPM.LPD_Operation = "C"
                    objDPM.LPD_IPAddress = sSession.IPAddress
                    Arr = objDPM.SavePumpBankDetails(sSession.AccessCode, objDPM)
                    If Arr(0) = "2" Then
                        lblError.Text = "Successfully Updated"
                        imgbtnBankSave.ImageUrl = "~/Images/Save16.png"
                    ElseIf Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved"
                    End If
                    dgBankDetails.DataSource = objDPM.BindPumpBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingDieselNo.SelectedValue)
                    dgBankDetails.DataBind()
                    txtBankID.Text = 0
                    ClearBankDetails()
                End If

            Else
                    lblError.Text = "Select Existing Diesel/Petrol Pump Registeration No."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBankSave_Click")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            txtGLID.Text = 0 : txtAddress.Text = ""
            If sCMDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            imgbtnUpdate.Visible = False
            'btnSave.Text = "Save"
            ' btnDelete.Text = "Delete"
            ddlExistingDieselNo.SelectedIndex = 0 : lblStatus.Text = ""

            txtRegistrationNo.Text = "" : txtDetails.Text = ""
            txtContactPerson.Text = "" : txtAddress.Text = "" : txtPinCode.Text = ""
            ddlState.SelectedIndex = 0 : txtMobileNo.Text = ""
            txtDieselName.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0
            lblID.Text = "0"
            txtGSTNo.Text = ""
            dgBankDetails.DataSource = Nothing
            dgBankDetails.DataBind()
            'txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
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
            Throw
        End Try
    End Function
    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer, ByVal sStatus As String) As Integer
        Dim sRet As String = ""
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try
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

            'sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            If sStatus = "Save" Then
                sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            ElseIf sStatus = "Update" Then
                objCOA.igl_id = txtGLID.Text
                sRet = objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            End If

            sArray = sRet.Split(",")
            Return sArray(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            lblError.Text = ""
            If ddlExistingDieselNo.SelectedIndex > 0 Then
                objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingDieselNo.SelectedValue, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                lblStatus.Text = "Approved"
                lblError.Text = "Successfully Approved"
                lblDieselValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                imgbtnUpdate.Visible = True : imgbtnWaiting.Visible = False : imgbtnSave.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
