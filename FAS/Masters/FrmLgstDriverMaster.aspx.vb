Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Masters_FrmLgstDriverMaster
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
    Dim objDriverMas As New clsDriverMaster

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
            RFVDriverName.ErrorMessage = "Enter Driver Name"
            RFVLicenseNo.ErrorMessage = "Enter License No"
            REVAdharNo.ErrorMessage = "Enter Aadhar No"
            REVAdharNo.ErrorMessage = "Enter valid Aadhar No"
            RFVContactNo.ErrorMessage = "Enter Contact No"
            REVContactNo.ErrorMessage = "Enter valid contact no"
            'RFVCity.ErrorMessage = "Enter City"
            'RFVPinCode.ErrorMessage = "Enter Pincode"
            'RFVAddress.ErrorMessage = "Enter Address"
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LGSTDrM")
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
                lblID.Text = "0"

                txtGLID.Text = 0

                BindExistingDriver()
                Dim sAssetRefNo As String = ""
                sAssetRefNo = Request.QueryString("MasterID")
                If sAssetRefNo <> "" Then
                    ddlExistingDriver.SelectedValue = objGen.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                    sAssetRefNo = objGen.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                    ddlExistingDriver_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch
        End Try
    End Sub
    Protected Sub BindExistingDriver()
        Try
            lblStatus.Text = ""
            ddlExistingDriver.DataSource = objDriverMas.LoadDriver(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingDriver.DataTextField = "LDM_DriverName"
            ddlExistingDriver.DataValueField = "LDM_ID"
            ddlExistingDriver.DataBind()
            ddlExistingDriver.Items.Insert(0, "Select Existing Driver")
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
            lblStatus.Text = ""
            If txtDriverName.Text = "" Or txtAdharNo.Text = "" Or txtLicenseNo.Text = "" Then
                Exit Sub
            Else
                If ddlExistingDriver.SelectedIndex > 0 Then
                Else
                    If txtAdharNo.Text <> "" Then
                        bCheck = objDriverMas.CheckDuplicateAadharNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtAdharNo.Text))
                        If bCheck = True Then
                            lblError.Text = "This Adhar.No is already Exist"
                            Exit Sub
                        End If
                    End If

                    If txtLicenseNo.Text <> "" Then
                        bCheck = objDriverMas.CheckDuplicateLicenseNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtLicenseNo.Text))
                        If bCheck = True Then
                            lblError.Text = "This License.No is already Exist"
                            Exit Sub
                        End If
                    End If
                End If

                objDriverMas.LDM_ID = lblID.Text
                objDriverMas.LDM_DriverName = txtDriverName.Text
                objDriverMas.LDM_LicenseNo = txtLicenseNo.Text
                objDriverMas.LDM_AadharNo = txtAdharNo.Text
                objDriverMas.LDM_ContactNo = TxtContactNo.Text
                objDriverMas.LDM_City = txtCity.Text
                objDriverMas.LDM_PinCode = txtPinCode.Text
                objDriverMas.LDM_InsuranceType = ddlInsuranceType.SelectedIndex
                objDriverMas.LDM_InsuranceNo = txtPolicyNo.Text
                If txtPolicyAmt.Text = "" Then
                    objDriverMas.LDM_InsuranceAmt = 0
                Else
                    objDriverMas.LDM_InsuranceAmt = txtPolicyAmt.Text
                End If

                If txtExpiryDate.Text = "" Then
                    objDriverMas.LDM_InsuranceExpDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    objDriverMas.LDM_InsuranceExpDate = Date.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If

                objDriverMas.LDM_InsuranceDetails = TxtPolicyDetails.Text
                objDriverMas.LDM_Address = txtAddress.Text
                If ddlExistingDriver.SelectedIndex > 0 Then
                    objDriverMas.LDM_Delflag = "A"
                Else
                    objDriverMas.LDM_Delflag = "W"
                End If
                objDriverMas.LDM_Status = "W"
                objDriverMas.LDM_CreatedBy = sSession.UserID
                objDriverMas.LDM_CreatedOn = Date.Today
                objDriverMas.LDM_ApprovedBy = Nothing
                objDriverMas.LDM_ApprovedOn = Date.Today
                objDriverMas.LDM_DeletedBy = Nothing
                objDriverMas.LDM_DeletedOn = Date.Today
                objDriverMas.LDM_UpdatedBy = sSession.UserID
                objDriverMas.LDM_UpdatedOn = Date.Today
                objDriverMas.LDM_RecalldBy = Nothing

                Dim sDriverNameLicenseNo As String = ""

                sDriverNameLicenseNo = objDriverMas.LDM_DriverName & " - " & objDriverMas.LDM_LicenseNo


                iHead = 4
                objDriverMas.LDM_Group = objDriverMas.GetCOAGroup(sSession.AccessCode, sSession.AccessCodeID, "Expenditure") 'sArray1(1) '29
                objDriverMas.LDM_SubGroup = objDriverMas.GetCOASubGroup(sSession.AccessCode, sSession.AccessCodeID, "Other Expenses") ' sArray1(2) '31
                objDriverMas.LDM_GL = objDriverMas.GetCOAGL(sSession.AccessCode, sSession.AccessCodeID, "Driver Expenses") 'sArray1(3) '146

                If txtGLID.Text > 0 Then
                    objDriverMas.LDM_SubGL = CreateChartOfAccounts(Trim(sDriverNameLicenseNo), 3, objDriverMas.LDM_GL, 1, "Update")
                Else
                    objDriverMas.LDM_SubGL = CreateChartOfAccounts(Trim(sDriverNameLicenseNo), 3, objDriverMas.LDM_GL, 1, "Save")
                End If


                objDriverMas.LDM_CompID = sSession.AccessCodeID
                objDriverMas.LDM_IPAddress = sSession.IPAddress
                objDriverMas.LDM_YearID = sSession.YearID
                objDriverMas.LDM_Operation = "W"

                Arr = objDriverMas.SaveDriverDetails(sSession.AccessCode, objDriverMas)
                txtGLID.Text = 0

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    lblDriverValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblDriverValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                BindExistingDriver()
                ddlExistingDriver.SelectedValue = Arr(1)
                ddlExistingDriver_SelectedIndexChanged(sender, e)
            End If

        Catch
        End Try
    End Sub
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

    Private Sub ddlExistingDriver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingDriver.SelectedIndexChanged
        Try
            lblStatus.Text = ""
            If ddlExistingDriver.SelectedIndex > 0 Then
                BindDetails(ddlExistingDriver.SelectedValue)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub BindDetails(ByVal iDriverID As Integer)
        Dim dt As New DataTable
        Try
            lblStatus.Text = ""
            dt = objDriverMas.LoadDriverDetails(sSession.AccessCode, sSession.AccessCodeID, iDriverID)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LDM_Id")
                If IsDBNull(dt.Rows(0)("LDM_DriverName")) = False Then
                    txtDriverName.Text = dt.Rows(0)("LDM_DriverName")
                Else
                    txtDriverName.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_LicenseNo")) = False Then
                    txtLicenseNo.Text = dt.Rows(0)("LDM_LicenseNo")
                Else
                    txtLicenseNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_AadharNo")) = False Then
                    txtAdharNo.Text = dt.Rows(0)("LDM_AadharNo")
                Else
                    txtAdharNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_ContactNo")) = False Then
                    TxtContactNo.Text = dt.Rows(0)("LDM_ContactNo")
                Else
                    TxtContactNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_City")) = False Then
                    txtCity.Text = dt.Rows(0)("LDM_City")
                Else
                    txtCity.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_PinCode")) = False Then
                    txtPinCode.Text = dt.Rows(0)("LDM_PinCode")
                Else
                    txtPinCode.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_InsuranceType")) = False Then
                    ddlInsuranceType.SelectedIndex = dt.Rows(0)("LDM_InsuranceType")
                Else
                    ddlInsuranceType.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("LDM_InsuranceNo")) = False Then
                    txtPolicyNo.Text = dt.Rows(0)("LDM_InsuranceNo")
                    txtPolicyNo.Enabled = True
                Else
                    txtPolicyNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_InsuranceAmt")) = False Then
                    If dt.Rows(0)("LDM_InsuranceAmt") = 0 Then
                        txtPolicyAmt.Text = ""
                        txtPolicyAmt.Enabled = False
                    Else
                        txtPolicyAmt.Text = dt.Rows(0)("LDM_InsuranceAmt")
                        txtPolicyAmt.Enabled = True
                    End If
                Else
                    txtPolicyAmt.Text = ""
                End If
                objGen.FormatDtForRDBMS(dt.Rows(0)("LDM_InsuranceExpDate").ToString(), "D")
                If IsDBNull(dt.Rows(0)("LDM_InsuranceExpDate")) = False Then
                    If dt.Rows(0)("LDM_InsuranceExpDate") = "01/01/1900" Then
                        txtExpiryDate.Text = ""
                    Else
                        txtExpiryDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LDM_InsuranceExpDate").ToString(), "D")
                        txtExpiryDate.Enabled = True
                    End If
                Else
                    txtExpiryDate.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_InsuranceDetails")) = False Then
                    TxtPolicyDetails.Text = dt.Rows(0)("LDM_InsuranceDetails")
                    TxtPolicyDetails.Enabled = True
                Else
                    TxtPolicyDetails.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LDM_Address")) = False Then
                    txtAddress.Text = dt.Rows(0)("LDM_Address")
                Else
                    txtAddress.Text = ""
                End If

                If (dt.Rows(0)("LDM_DelFlag") = "W") Then
                    lblStatus.Text = "Waiting For Approval"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                    imgbtnWaiting.Visible = True
                End If
                If (dt.Rows(0)("LDM_Delflag") = "A") Then
                    lblStatus.Text = "Approved"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True : imgbtnWaiting.Visible = False
                    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                End If
                If (dt.Rows(0)("LDM_Delflag") = "D") Then
                    lblStatus.Text = "Deactivated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True : imgbtnWaiting.Visible = False
                    imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
                End If

                If IsDBNull(dt.Rows(0)("LDM_SubGL")) = False Then
                    txtGLID.Text = dt.Rows(0)("LDM_SubGL")
                Else
                    txtGLID.Text = 0
                End If


            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub ddlInsuranceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInsuranceType.SelectedIndexChanged
        Try
            lblStatus.Text = ""
            If ddlInsuranceType.SelectedIndex > 0 Then
                txtPolicyNo.Enabled = True : txtPolicyAmt.Enabled = True
                txtExpiryDate.Enabled = True : TxtPolicyDetails.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            lblStatus.Text = ""
            imgbtnSave_Click(sender, e)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblStatus.Text = ""
            Response.Redirect(String.Format("~/Masters/FrmLgstDriverDashBoard.aspx?"), False)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            lblStatus.Text = ""
            If ddlExistingDriver.SelectedIndex > 0 Then
                objDriverMas.UpdateDriverStatus(sSession.AccessCode, sSession.AccessCodeID, lblID.Text, sSession.UserID, sSession.IPAddress, sSession.YearID)
                imgbtnUpdate.Visible = True
                lblStatus.Text = "Approved"
                lblError.Text = "Successfully Approved"
                lblDriverValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                imgbtnWaiting.Visible = False : imgbtnUpdate.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblStatus.Text = ""
            ddlExistingDriver.SelectedIndex = 0 : txtGLID.Text = 0
            txtDriverName.Text = "" : txtLicenseNo.Text = "" : txtAdharNo.Text = ""
            TxtContactNo.Text = "" : txtCity.Text = "" : txtPinCode.Text = ""
            ddlInsuranceType.SelectedIndex = 0 : txtPolicyNo.Text = "" : txtPolicyAmt.Text = "" : txtExpiryDate.Text = ""
            txtAddress.Text = ""
            lblID.Text = "0" : imgbtnSave.Visible = True : imgbtnWaiting.Visible = False : imgbtnUpdate.Visible = False
            imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
        Catch ex As Exception

        End Try
    End Sub
End Class
