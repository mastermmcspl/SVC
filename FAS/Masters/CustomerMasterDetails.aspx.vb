Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Masters_CustomerMasterDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters/CustomerMasters"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Dim objCSM As New clsCustomer
    Dim objCOA As New clsChartOfAccounts
    Private Shared sCMDSave As String
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
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
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "CUSM")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnBankSave.Visible = False : btnStatutoryAdd.Visible = False : sCMDSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        imgbtnBankSave.Visible = True : btnStatutoryAdd.Visible = True
                        sCMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If

                divBankDetails.Visible = True
                txtBankID.Text = 0
                'BindGSTNCategory()
                BindCompanyType()

                txtGLID.Text = 0
                divcollapseTAXDetails.Visible = True
                BindExistingCustomer()

                imgbtnUpdate.Visible = False
                'imgbtnAdd.Visible = True
                BindState()
                bindCity()
                BindGeneralCategory()
                lblID.Text = "0"

                '* Commented *'
                'sStr = Request.QueryString("Status")
                'If sStr = "SO" Then
                '    txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, sStr)
                'Else
                '    txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")
                'End If
                '* Commented *'

                'Me.imgbtnSave.Attributes.Add("OnClick", "return validation()")
                'btnStatutoryAdd.Attributes.Add("onclick", "javascript:return validationStatutoryRef();")
                Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")

                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iPartyID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlExistingCustomer.SelectedValue = iPartyID
                    BindDetails(iPartyID)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindCompanyType()
        Try
            ddlCompanyType.DataSource = objCSM.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyType.DataTextField = "Mas_Desc"
            ddlCompanyType.DataValueField = "Mas_Id"
            ddlCompanyType.DataBind()
            ddlCompanyType.Items.Insert(0, "Select Company Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindExistingCustomer()
        Try
            ddlExistingCustomer.DataSource = objCSM.LoadExistingCustomer(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingCustomer.DataTextField = "BM_Name"
            ddlExistingCustomer.DataValueField = "BM_Id"
            ddlExistingCustomer.DataBind()
            ddlExistingCustomer.Items.Insert(0, "Select Existing Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindGeneralCategory()
        Try
            ddlCategory.DataSource = objCSM.LoadGeneralcategory(sSession.AccessCode, sSession.AccessCodeID)
            ddlCategory.DataTextField = "Mas_Desc"
            ddlCategory.DataValueField = "Mas_Id"
            ddlCategory.DataBind()
            ddlCategory.Items.Insert(0, "Select Category")
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
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sSubGrpcode As String = "", sGLCode As String = ""
        Dim lParentId As Integer = 0, iHead As Integer = 2
        Dim bCheck As Boolean
        Try
            If ddlExistingCustomer.SelectedIndex > 0 Then
            Else
                If txtGSTNRegNo.Text <> "" Then
                    bCheck = objCSM.CheckGSTNDuplicate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtGSTNRegNo.Text))
                    If bCheck = True Then
                        lblError.Text = "This GSTN Reg.No is already Exist"
                        Exit Sub
                    End If
                End If
            End If

            objCSM.CSM_ID = lblID.Text
            objCSM.CSM_IndType = 0
            objCSM.CSM_Name = txtSupplierName.Text
            objCSM.CSM_Code = txtSupplierCode.Text
            objCSM.CSM_Inventry = 0
            objCSM.CSM_ContactPerson = txtConatctPerson.Text
            objCSM.CSM_EmailID = txtEmail.Text
            objCSM.CSM_MobileNo = txtMobile.Text
            objCSM.CSM_LandLineNo = txtLandLine.Text
            objCSM.CSM_Fax = txtFax.Text
            objCSM.CSM_Address = txtAddress.Text

            objCSM.CSM_Address1 = txtAddress1.Text
            objCSM.CSM_Address2 = txtAddress2.Text
            objCSM.CSM_Address3 = txtAddress3.Text

            objCSM.CSM_Pincode = txtPinCode.Text
            If ddlCity.SelectedIndex > 0 Then
                objCSM.CSM_City = ddlCity.SelectedValue
            Else
                objCSM.CSM_City = 0
            End If
            If ddlState.SelectedIndex > 0 Then
                objCSM.CSM_State = ddlState.SelectedValue
            Else
                objCSM.CSM_State = 0
            End If
            objCSM.CSM_Delflag = "W"
            objCSM.CSM_CompID = sSession.AccessCodeID
            objCSM.CSM_Status = "C"
            'objCSM.CSM_Operation = "C"
            objCSM.CSM_IPAddress = sSession.IPAddress
            objCSM.CSM_CreatedBy = sSession.UserID
            objCSM.CSM_CreatedOn = Date.Today
            objCSM.CSM_ApprovedBy = Nothing
            objCSM.CSM_ApprovedOn = Date.Today
            objCSM.CSM_DeletedBy = Nothing
            objCSM.CSM_DeletedOn = Date.Today
            objCSM.CSM_UpdatedBy = sSession.UserID
            objCSM.CSM_UpdatedOn = Date.Today
            objCSM.CSM_YearID = sSession.YearID

            If ddlCategory.SelectedIndex > 0 Then
                objCSM.BM_GenCategory = ddlCategory.SelectedValue
            Else
                objCSM.BM_GenCategory = 0
            End If

            Dim sPerm As String = ""
            Dim sArray1 As Array
            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")

            iHead = sArray1(0) '1
            objCSM.BM_Group = sArray1(1) '29
            objCSM.BM_SubGroup = sArray1(2) '31
            objCSM.BM_GL = sArray1(3) '146

            If txtGLID.Text > 0 Then
                objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 1, "Update")
            Else
                objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 1, "Save")
            End If

            If txtGSTNRegNo.Text <> "" Then
                objCSM.BM_GSTNRegNo = txtGSTNRegNo.Text
            Else
                objCSM.BM_GSTNRegNo = ""
            End If

            If ddlCompanyType.SelectedIndex > 0 Then
                objCSM.BM_CompanyType = ddlCompanyType.SelectedValue
            Else
                objCSM.BM_CompanyType = 0
            End If

            If ddlGSTCategory.SelectedIndex > 0 Then
                objCSM.BM_GSTNCategory = ddlGSTCategory.SelectedValue
            Else
                objCSM.BM_GSTNCategory = 0
            End If

            Arr = objCSM.SavePartyDetails(sSession.AccessCode, objCSM)
            txtGLID.Text = 0

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                imgbtnSave.Visible = False 'btnDelete.Visible = True
                'btnSave.Text = "Save"
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If  'btnDelete.Visible = True
                imgbtnSave.Visible = False
                ' btnSave.Text = "Update"
                lblStatus.Text = "Waiting For Approval"
            End If
            'BindMasterDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDetailsSetttings")
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
    Private Sub BindStatutoryReferencesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer)
        Dim dt As New DataTable
        Try
            dt = objCSM.LoadGridStatutoryReferencesDetails(sNameSpace, iCompID, iPartyID)
            dgOtherDetails.DataSource = dt
            dgOtherDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatutoryReferencesDetails")
        End Try
    End Sub

    'Private Sub BindMasterDetails()
    '    Dim dt As New DataTable
    '    Try
    '        dt = objCSM.LoadGridMasterDetails(sSession.AccessCode, sSession.AccessCodeID)
    '        grdMaster.DataSource = dt
    '        grdMaster.DataBind()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Private Sub btnStatutoryAdd_Click(sender As Object, e As EventArgs) Handles btnStatutoryAdd.Click
        Dim iID As Integer
        Dim sStr As String = ""
        Try
            If lblID.Text <> "" Then
                'sStr = objCSM.GetPartyStatus(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                'If sStr = "A" Then
                iID = objCSM.GetCSMStatutoryNameValueID(sSession.AccessCode, sSession.AccessCodeID, txtStatutoryName.Text, lblID.Text)
                If lblID.Text > 0 Then
                    If txtStatutoryName.Text <> "" And txtStatutoryValue.Text <> "" Then
                        lblError.Text = ""
                        objCSM.SaveCSMStatutoryNameValue(sSession.AccessCode, sSession.AccessCodeID, txtStatutoryName.Text, txtStatutoryValue.Text, iID, lblID.Text)
                        txtStatutoryName.Text = "" : txtStatutoryValue.Text = ""
                    End If
                Else
                    lblError.Text = "Select Existing Customer."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
                BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                'ElseIf sStr = "D" Then
                'lblError.Text = "The party has been deleted." : lblErrorDown.Text = "The party has been deleted."
                '    Exit Sub
                'Else
                'lblError.Text = "Approve the party." : lblErrorDown.Text = "Approve the party."
                '    Exit Sub
                'End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnStatutoryAdd_Click")
        End Try
    End Sub

    Private Sub dgOtherDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgOtherDetails.RowCommand
        Dim dt As New DataTable
        Dim lblsID As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Delete" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblsID = DirectCast(clickedRow.FindControl("lblsID"), Label)
                objCSM.DeleteCSMStatutoryNameValue(sSession.AccessCode, sSession.AccessCodeID, lblsID.Text)
            End If
            BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgOtherDetails_RowCommand")
        End Try
    End Sub
    'Private Sub grdMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdMaster.RowCommand
    '    Dim dt As New DataTable
    '    Dim lblCSM_ID As New Label
    '    Try
    '        lblError.Text = ""
    '        ' btnSave.Text = "Update"
    '        imgbtnSave.Visible = False : imgbtnUpdate.Visible = True  'btnDelete.Visible = True
    '        Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
    '        lblCSM_ID = DirectCast(clickedRow.FindControl("lblCSM_ID"), Label)

    '        BindDetails(lblCSM_ID.Text)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdMaster_ItemCommand")
    '    End Try
    'End Sub
    Public Sub BindDetails(ByVal iPartyID As Integer)
        Dim dt As New DataTable

        Try
            dt = objCSM.LoadPartyDetails(sSession.AccessCode, sSession.AccessCodeID, iPartyID)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("BM_Id")
                If IsDBNull(dt.Rows(0)("BM_Name")) = False Then
                    txtSupplierName.Text = dt.Rows(0)("BM_Name")
                Else
                    txtSupplierName.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("BM_Code")) = False Then
                    txtSupplierCode.Text = dt.Rows(0)("BM_Code")
                Else
                    txtSupplierCode.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("BM_ContactPerson")) = False Then
                    txtConatctPerson.Text = dt.Rows(0)("BM_ContactPerson")
                Else
                    txtConatctPerson.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("BM_EmailID")) = False Then
                    txtEmail.Text = dt.Rows(0)("BM_EmailID")
                Else
                    txtEmail.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("BM_MobileNo")) = False Then
                    txtMobile.Text = dt.Rows(0)("BM_MobileNo")
                Else
                    txtMobile.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("BM_LandLineNo")) = False Then
                    txtLandLine.Text = dt.Rows(0)("BM_LandLineNo")
                Else
                    txtLandLine.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("BM_Fax")) = False Then
                    txtFax.Text = dt.Rows(0)("BM_Fax")
                Else
                    txtFax.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("BM_Address")) = False Then
                    txtAddress.Text = dt.Rows(0)("BM_Address")
                Else
                    txtAddress.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BM_Address1")) = False Then
                    txtAddress1.Text = dt.Rows(0)("BM_Address1")
                Else
                    txtAddress1.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BM_Address2")) = False Then
                    txtAddress2.Text = dt.Rows(0)("BM_Address2")
                Else
                    txtAddress2.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BM_Address3")) = False Then
                    txtAddress3.Text = dt.Rows(0)("BM_Address3")
                Else
                    txtAddress3.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BM_Pincode")) = False Then
                    txtPinCode.Text = dt.Rows(0)("BM_Pincode")
                Else
                    txtPinCode.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BM_City")) = False Then
                    If dt.Rows(0)("BM_City") > 0 Then
                        ddlCity.SelectedValue = dt.Rows(0)("BM_City")
                    Else
                        ddlCity.SelectedIndex = 0
                    End If
                Else
                    ddlCity.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("BM_State")) = False Then
                    If dt.Rows(0)("BM_State") > 0 Then
                        ddlState.SelectedValue = dt.Rows(0)("BM_State")
                    Else
                        ddlState.SelectedIndex = 0
                    End If
                Else
                    ddlState.Items.Clear()
                End If

                If IsDBNull(dt.Rows(0)("BM_GenCategory")) = False Then
                    If dt.Rows(0)("BM_GenCategory") > 0 Then
                        ddlCategory.SelectedValue = dt.Rows(0)("BM_GenCategory")
                    Else
                        ddlCategory.SelectedIndex = 0
                    End If
                Else
                    ddlCategory.SelectedIndex = 0
                End If

                If (dt.Rows(0)("BM_Delflag") = "W") Then
                    lblError.Text = "Waiting For Approval"
                    'btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("BM_Delflag") = "D") Then
                    ' btnDelete.Text = "ReCall"
                Else
                    'btnDelete.Text = "Delete"
                End If

                imgbtnSave.Visible = False
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                If (dt.Rows(0)("BM_Delflag") = "X") Then
                    lblStatus.Text = "Waiting For Approval(After De-Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("BM_Delflag") = "D") Then
                    lblStatus.Text = "De-Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("BM_Delflag") = "A") Then
                    lblStatus.Text = "Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "Delete"
                End If
                If (dt.Rows(0)("BM_Delflag") = "Y") Then
                    lblStatus.Text = "Waiting For Approval(After Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("BM_Delflag") = "W") Then
                    lblStatus.Text = "Waiting For Approval"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If

                If IsDBNull(dt.Rows(0)("BM_SubGL")) = False Then
                    txtGLID.Text = dt.Rows(0)("BM_SubGL")
                Else
                    txtGLID.Text = 0
                End If

                If IsDBNull(dt.Rows(0)("BM_GSTNRegNo")) = False Then
                    txtGSTNRegNo.Text = dt.Rows(0)("BM_GSTNRegNo")
                Else
                    txtGSTNRegNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("BM_CompanyType")) = False Then
                    If dt.Rows(0)("BM_CompanyType") > 0 Then
                        ddlCompanyType.SelectedValue = dt.Rows(0)("BM_CompanyType")
                    Else
                        ddlCompanyType.SelectedIndex = 0
                    End If
                Else
                    ddlCompanyType.SelectedIndex = 0
                End If

                'If IsDBNull(dt.Rows(0)("BM_GSTNCategory")) = False Then
                '    If dt.Rows(0)("BM_GSTNCategory") > 0 Then
                '        BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                '        ddlGSTCategory.SelectedValue = dt.Rows(0)("BM_GSTNCategory")
                '    Else
                '        ddlGSTCategory.Items.Clear()
                '    End If
                'Else
                '    ddlGSTCategory.Items.Clear()
                'End If
                Dim taxcategory As String

                If IsDBNull(dt.Rows(0)("BM_GSTNCategory")) = False Then
                    If dt.Rows(0)("BM_GSTNCategory") > 0 Then
                        BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                        ddlGSTCategory.SelectedValue = dt.Rows(0)("BM_GSTNCategory")

                        taxcategory = objCSM.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)

                        If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                            txtGSTNRegNo.Enabled = False
                        Else
                            txtGSTNRegNo.Enabled = True
                        End If

                    Else
                        ddlGSTCategory.Items.Clear()
                    End If
                Else
                    ddlGSTCategory.Items.Clear()
                End If

                If UCase(ddlCategory.SelectedItem.Text) = UCase("Customer") Then
                    ddlGSTCategory.Enabled = False : ddlCompanyType.Enabled = False : txtGSTNRegNo.Enabled = False
                Else
                    ddlGSTCategory.Enabled = True : ddlCompanyType.Enabled = True : txtGSTNRegNo.Enabled = True
                End If

                dgBankDetails.DataSource = objCSM.BindBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingCustomer.SelectedValue)
                dgBankDetails.DataBind()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            txtGLID.Text = 0 : txtAddress1.Text = ""
            If sCMDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            imgbtnUpdate.Visible = False
            'btnSave.Text = "Save"
            ' btnDelete.Text = "Delete"
            ddlExistingCustomer.SelectedIndex = 0 : lblStatus.Text = ""
            ddlCategory.SelectedIndex = 0
            txtSupplierName.Text = "" : txtSupplierCode.Text = ""
            txtConatctPerson.Text = "" : txtAddress.Text = "" : txtPinCode.Text = "" : txtAddress1.Text = "" : txtAddress2.Text = "" : txtAddress3.Text = ""
            ddlState.SelectedIndex = 0 : txtEmail.Text = "" : txtMobile.Text = "" : txtLandLine.Text = ""
            txtFax.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0
            lblID.Text = "0"
            txtSupplierCode.Text = ""
            'txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")
            ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.Items.Clear() : txtGSTNRegNo.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    'Private Sub grdMaster_PreRender(sender As Object, e As EventArgs) Handles grdMaster.PreRender
    '    Dim dt As New DataTable
    '    Try
    '        If grdMaster.Rows.Count > 0 Then
    '            grdMaster.UseAccessibleHeader = True
    '            grdMaster.HeaderRow.TableSection = TableRowSection.TableHeader
    '            grdMaster.FooterRow.TableSection = TableRowSection.TableFooter
    '        End If
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_PreRender")
    '    End Try
    'End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            imgbtnSave_Click(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    '    Try
    '        If (btnDelete.Text = "Delete") Then

    '            If (Convert.ToInt32(lblID.Text) > 0) Then
    '                objCSM.DeletePartyMaster(sSession.AccessCode, sSession.AccessCodeID, lblID.Text, sSession.UserID)
    '                lblError.Text = "Deleted SuccessFully"
    '                lblCustomerValidationMsg.Text = lblError.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '                BindMasterDetails()
    '                txtSupplierName.Text = "" : txtSupplierCode.Text = ""
    '                txtConatctPerson.Text = "" : txtAddress.Text = "" : txtPinCode.Text = ""
    '                ddlState.SelectedIndex = 0 : txtEmail.Text = "" : txtMobile.Text = "" : txtLandLine.Text = ""
    '                txtFax.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0
    '                lblID.Text = "0"
    '            Else
    '                lblError.Text = "Select The Customer To Delete"
    '                lblCustomerValidationMsg.Text = lblError.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '            End If
    '        Else

    '            If (Convert.ToInt32(lblID.Text) > 0) Then
    '                objCSM.ReCallPartyMaster(sSession.AccessCode, sSession.AccessCodeID, lblID.Text, sSession.UserID)
    '                lblError.Text = "ReCalled SuccessFully"
    '                lblCustomerValidationMsg.Text = lblError.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '                BindMasterDetails()
    '                txtSupplierName.Text = "" : txtSupplierCode.Text = ""
    '                txtConatctPerson.Text = "" : txtAddress.Text = "" : txtPinCode.Text = ""
    '                ddlState.SelectedIndex = 0 : txtEmail.Text = "" : txtMobile.Text = "" : txtLandLine.Text = ""
    '                txtFax.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0
    '                lblID.Text = "0"
    '            Else
    '                lblError.Text = "Select The Customer To Delete"
    '                lblCustomerValidationMsg.Text = lblError.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message

    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDelete.Click")
    '    End Try
    'End Sub
    'Private Sub imgbtnCreateVAT_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCreateVAT.Click
    '    Try
    '        If lblID.Text > 0 Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
    '        Else
    '            lblError.Text = "Select the customer"
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub dgOtherDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgOtherDetails.RowDataBound
        Try
            dgOtherDetails.Columns(3).Visible = False
            If sCMDSave = "YES" Then
                dgOtherDetails.Columns(3).Visible = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dgOtherDetails_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles dgOtherDetails.RowDeleted

    End Sub

    Private Sub dgOtherDetails_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles dgOtherDetails.RowDeleting

    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Masters/CustomerMaster.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub dgOtherDetails_PreRender(sender As Object, e As EventArgs) Handles dgOtherDetails.PreRender
        Dim dt As New DataTable
        Try
            If dgOtherDetails.Rows.Count > 0 Then
                dgOtherDetails.UseAccessibleHeader = True
                dgOtherDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                dgOtherDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgOtherDetails_PreRender")
        End Try
    End Sub
    Private Sub ddlExistingCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingCustomer.SelectedIndexChanged
        Try
            If ddlExistingCustomer.SelectedIndex > 0 Then
                BindDetails(ddlExistingCustomer.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingCustomer_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub BindGSTNCategory(ByVal sCompanyType As String)
        Dim dt As New DataTable
        Try
            dt = objCSM.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategory.DataSource = dt
            ddlGSTCategory.DataTextField = "GC_GSTCategory"
            ddlGSTCategory.DataValueField = "GC_Id"
            ddlGSTCategory.DataBind()
            ddlGSTCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCompanyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompanyType.SelectedIndexChanged
        Try
            If ddlCompanyType.SelectedIndex > 0 Then
                BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
            Else
                ddlGSTCategory.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCompanyType_SelectedIndexChanged")
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
                dtBank = objCSM.GetBankDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingCustomer.SelectedValue)
                If dtBank.Rows.Count > 0 Then
                    txtAccountNo.Text = dtBank.Rows(0)("CBD_AccountNo")
                    txtBankName.Text = dtBank.Rows(0)("CBD_BankName")
                    txtIFSCCode.Text = dtBank.Rows(0)("CBD_IFSC")
                    txtBranchName.Text = dtBank.Rows(0)("CBD_Branch")
                End If
            ElseIf e.CommandName = "Delete" Then
                objCSM.DeleteBankValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingCustomer.SelectedValue)
                dgBankDetails.DataSource = objCSM.BindBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingCustomer.SelectedValue)
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
            If ddlExistingCustomer.SelectedIndex > 0 Then
                objCSM.CBD_ID = txtBankID.Text
                objCSM.CBD_Customer_ID = ddlExistingCustomer.SelectedValue
                objCSM.CBD_AccountNo = txtAccountNo.Text
                objCSM.CBD_BankName = txtBankName.Text
                objCSM.CBD_IFSC = txtIFSCCode.Text
                objCSM.CBD_Branch = txtBranchName.Text
                objCSM.CBD_DelFlag = "X"
                objCSM.CBD_Status = "W"
                objCSM.CBD_CreatedBy = sSession.UserID
                objCSM.CBD_CreatedOn = Date.Today
                objCSM.CBD_UpdatedBy = sSession.UserID
                objCSM.CBD_UpdatedOn = Date.Today
                objCSM.CBD_CompID = sSession.AccessCodeID
                objCSM.CBD_YearID = sSession.YearID
                objCSM.CBD_Operation = "C"
                objCSM.CBD_IPAddress = sSession.IPAddress
                Arr = objCSM.SaveBankDetails(sSession.AccessCode, objCSM)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnBankSave.ImageUrl = "~/Images/Save16.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
                dgBankDetails.DataSource = objCSM.BindBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingCustomer.SelectedValue)
                dgBankDetails.DataBind()
                txtBankID.Text = 0
                ClearBankDetails()
            Else
                lblError.Text = "Select Existing Customer"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBankSave_Click")
        End Try
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        'Try
        '    If UCase(ddlCategory.SelectedItem.Text) = UCase("Customer") Then
        '        txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "SO")
        '        ddlGSTCategory.Enabled = False : ddlCompanyType.Enabled = False : txtGSTNRegNo.Enabled = False
        '    Else
        '        txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")
        '        ddlGSTCategory.Enabled = True : ddlCompanyType.Enabled = True : txtGSTNRegNo.Enabled = True
        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCategory_SelectedIndexChanged")
        'End Try
        Try
            If ddlExistingCustomer.SelectedIndex > 0 Then
                If UCase(ddlCategory.SelectedItem.Text) = UCase("Customer") Then
                    ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.Items.Clear() : txtGSTNRegNo.Text = ""
                    ddlGSTCategory.Enabled = False : ddlCompanyType.Enabled = False : txtGSTNRegNo.Enabled = False
                Else
                    ddlGSTCategory.Enabled = True : ddlCompanyType.Enabled = True
                End If
            Else
                If UCase(ddlCategory.SelectedItem.Text) = UCase("Customer") Then
                    txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "SO")
                    ddlGSTCategory.Enabled = False : ddlCompanyType.Enabled = False : txtGSTNRegNo.Enabled = False
                Else
                    txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")
                    ddlGSTCategory.Enabled = True : ddlCompanyType.Enabled = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCategory_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlGSTCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGSTCategory.SelectedIndexChanged
        Try
            If UCase(ddlGSTCategory.SelectedItem.Text) = UCase("UNRIGISTERED DEALER") Then
                txtGSTNRegNo.Enabled = False
                txtGSTNRegNo.Text = ""
            Else
                txtGSTNRegNo.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGSTCategory_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub dgBankDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgBankDetails.ItemDataBound
        Try
            dgBankDetails.Columns(6).Visible = False
            If sCMDSave = "YES" Then
                dgBankDetails.Columns(6).Visible = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class