Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Masters_SupplierMasterDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters/SupplierMasterDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objCSM As New clsSupplierMaster
    Dim objCOA As New clsChartOfAccounts
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSMDSave As String
    Dim objSupplier As New clsSupplierMaster

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
        Dim sMasterType As String = ""
        Dim sFormButtons As String = ""
        Dim sMasterID As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SM")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : sSMDSave = "NO"
                btnStatutoryAdd.Visible = False : imgbtnBankSave.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        btnStatutoryAdd.Visible = True : imgbtnBankSave.Visible = True
                        sSMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If

                divBankDetails.Visible = True
                txtBankID.Text = 0

                BindCompanyType()
                'BindGSTNCategory()

                txtGLID.Text = 0
                imgbtnUpdate.Visible = False
                LoadExistingSupplier() : BindState() : bindCity()
                sStr = Request.QueryString("Status")

                'RFVGSTNRegNo.ErrorMessage = "Enter GSTN Reg.No"
                RFvddlCompanyType.InitialValue = "Select Company Type" : RFvddlCompanyType.ErrorMessage = "Select Company Type"
                RFVddlGSTCategory.InitialValue = "Select" : RFVddlGSTCategory.ErrorMessage = "Select GSTN Category"

                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    ddlExistSuppliers.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExistSuppliers_SelectedIndexChanged(sender, e)
                Else
                    lblStatus.Text = "Not Started"
                    If sStr = "SO" Then
                        txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, sStr)
                    Else
                        txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindCompanyType()
        Try
            ddlCompanyType.DataSource = objSupplier.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyType.DataTextField = "Mas_Desc"
            ddlCompanyType.DataValueField = "Mas_Id"
            ddlCompanyType.DataBind()
            ddlCompanyType.Items.Insert(0, "Select Company Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingSupplier()
        Try
            ddlExistSuppliers.DataSource = objSupplier.LoadExistingSupplier(sSession.AccessCode, sSession.AccessCodeID, 0)
            ddlExistSuppliers.DataTextField = "CSM_Name"
            ddlExistSuppliers.DataValueField = "CSM_ID"
            ddlExistSuppliers.DataBind()
            ddlExistSuppliers.Items.Insert(0, "Existing Supplier")
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
            lblError.Text = ""
            If ddlGSTCategory.SelectedItem.Text = UCase("COMPOSITION DEALER") Or ddlGSTCategory.SelectedItem.Text = UCase("RIGISTERED DEALER") Then
                If txtGSTNRegNo.Text = "" Then
                    lblError.Text = "Enter GSTN Reg.No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter GSTN Reg.No','', 'success');", True)
                    Exit Sub
                End If
            End If

            If ddlExistSuppliers.SelectedIndex > 0 Then
            Else
                If txtGSTNRegNo.Text <> "" Then
                    bCheck = objCSM.CheckGSTNDuplicate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtGSTNRegNo.Text))
                    If bCheck = True Then
                        lblError.Text = "This GSTN Reg.No is already Exist"
                        Exit Sub
                    End If
                End If
            End If

            If ddlExistSuppliers.SelectedIndex > 0 Then
                objCSM.CSM_ID = ddlExistSuppliers.SelectedValue
            Else
                objCSM.CSM_ID = 0
            End If

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
            objCSM.CSM_ProductDescription = txtpDescription.Text
            objCSM.CSM_Address1 = txtAddress1.Text
            objCSM.CSM_Address2 = txtAddress2.Text
            objCSM.CSM_Address3 = txtAddress3.Text
            objCSM.CSM_Operation = "W"
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
            objCSM.CSM_Status = "W"
            objCSM.CSM_IPAddress = sSession.IPAddress
            objCSM.CSM_CreatedBy = sSession.UserID
            objCSM.CSM_CreatedOn = Date.Today
            objCSM.CSM_UpdatedBy = sSession.UserID
            objCSM.CSM_UpdatedOn = Date.Today
            objCSM.CSM_YearID = sSession.YearID


            Dim sPerm As String = ""
            Dim sArray1 As Array
            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            objCSM.BM_Group = sArray1(1) '29
            objCSM.BM_SubGroup = sArray1(2) '31
            objCSM.BM_GL = sArray1(3) '146

            'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
            If txtGLID.Text > 0 Then
                objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4, "Update")
            Else
                objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4, "Save")
            End If

            If txtGSTNRegNo.Text <> "" Then
                objCSM.CSM_GSTNRegNo = txtGSTNRegNo.Text
            Else
                objCSM.CSM_GSTNRegNo = ""
            End If

            If ddlCompanyType.SelectedIndex > 0 Then
                objCSM.CSM_CompanyType = ddlCompanyType.SelectedValue
            Else
                objCSM.CSM_CompanyType = 0
            End If

            If ddlGSTCategory.SelectedIndex > 0 Then
                objCSM.CSM_GSTNCategory = ddlGSTCategory.SelectedValue
            Else
                objCSM.CSM_GSTNCategory = 0
            End If

            Arr = objCSM.SaveCumsterSupplierDetails(sSession.AccessCode, objCSM)
            txtGLID.Text = 0

            'If (ddlGroup.SelectedIndex > 0) Then
            '    sSubGrpcode = objDBL.SQLExecuteScalar(sSession.AccessCode, "Select gl_Glcode from chart_of_Accounts where gl_id = " & ddlGroup.SelectedValue & " and gl_CompID = " & sSession.AccessCodeID & "")
            '    sGLCode = objCOA.GenerateGLCode(sSession.AccessCode, sSession.AccessCodeID, 1, sSubGrpcode)
            '    lParentId = Convert.ToInt32(objCSM.getParentId(sSession.AccessCode, sSession.AccessCodeID, sSubGrpcode))
            '    objCOA.igl_head = 1
            'End If

            'If (ddlSubGroup.SelectedIndex > 0) Then
            '    sSubGrpcode = objDBL.SQLExecuteScalar(sSession.AccessCode, "Select gl_Glcode from chart_of_Accounts where gl_id = " & ddlSubGroup.SelectedValue & " and gl_CompID = " & sSession.AccessCodeID & "")
            '    sGLCode = objCOA.GenerateGLCode(sSession.AccessCode, sSession.AccessCodeID, 1, sSubGrpcode)
            '    lParentId = Convert.ToInt32(objCSM.getParentId(sSession.AccessCode, sSession.AccessCodeID, sSubGrpcode))
            '    objCOA.igl_head = 2
            'End If

            'If (ddlGL.SelectedIndex > 0) Then
            '    sSubGrpcode = objDBL.SQLExecuteScalar(sSession.AccessCode, "Select gl_Glcode from chart_of_Accounts where gl_id = " & ddlGL.SelectedValue & " and gl_CompID = " & sSession.AccessCodeID & "")
            '    sGLCode = objCOA.GenerateGLCode(sSession.AccessCode, sSession.AccessCodeID, 1, sSubGrpcode)
            '    lParentId = Convert.ToInt32(objCSM.getParentId(sSession.AccessCode, sSession.AccessCodeID, sSubGrpcode))
            '    objCOA.igl_head = 3
            'End If

            'objCOA.sgl_glcode = sGLCode
            'objCOA.igl_id = 0
            'objCOA.igl_Parent = lParentId
            'objCOA.sgl_Desc = txtSupplierName.Text

            'objCOA.sgl_reason_Creation = txtSupplierName.Text
            ''objCOA.igl_subglexist = 0
            'objCOA.sgl_Delflag = "C"
            'objCOA.igl_AccHead = 1
            'objCOA.igl_Crby = sSession.UserID
            ''objCOA.igl_SortOrder = 0
            'objCOA.igl_CompId = sSession.AccessCodeID
            'objCOA.sgl_Status = "N"
            'objCOA.sgl_AccType = "C"
            ''objCOA.igl_orderby = 0
            'objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            'objCSM.SavePartyToChartOfAccount(sSession.AccessCode, sSession.AccessCodeID, ddlCategory.SelectedValue, txtSupplierName.Text)          

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
                imgbtnUpdate.Visible = False
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                lblStatus.Text = "Waiting for Approval"
                If sSMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
            End If
            LoadExistingSupplier()
            ddlExistSuppliers.SelectedValue = Arr(1)
            ddlExistSuppliers_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
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
    Private Sub BindStatutoryReferencesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer)
        Try
            dgOtherDetails.DataSource = objCSM.LoadGridStatutoryReferencesDetails(sNameSpace, iCompID, iPartyID)
            dgOtherDetails.DataBind()
        Catch ex As Exception
            Throw
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
    Private Sub btnStatutoryAdd_Click(sender As Object, e As EventArgs) Handles btnStatutoryAdd.Click
        Dim iID As Integer
        Try
            iID = objCSM.GetCSMStatutoryNameValueID(sSession.AccessCode, sSession.AccessCodeID, txtStatutoryName.Text, ddlExistSuppliers.SelectedValue)
            If txtStatutoryName.Text <> "" And txtStatutoryValue.Text <> "" Then
                objCSM.SaveCSMStatutoryNameValue(sSession.AccessCode, sSession.AccessCodeID, txtStatutoryName.Text, txtStatutoryValue.Text, iID, ddlExistSuppliers.SelectedValue)
                txtStatutoryName.Text = "" : txtStatutoryValue.Text = ""
            End If
            BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSuppliers.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnStatutoryAdd_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            txtGLID.Text = 0 : txtAddress1.Text = ""
            lblError.Text = "" : ddlExistSuppliers.SelectedIndex = 0
            txtSupplierName.Text = "" : txtSupplierCode.Text = ""
            txtConatctPerson.Text = "" : txtAddress.Text = "" : txtPinCode.Text = ""
            ddlState.SelectedIndex = 0 : txtEmail.Text = "" : txtMobile.Text = "" : txtLandLine.Text = ""
            txtFax.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0
            txtpDescription.Text = "" : lblStatus.Text = "Not Started"
            If sSMDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            imgbtnUpdate.Visible = False
            txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")
            txtGSTNRegNo.Text = "" : ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.Items.Clear()
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
    Public Sub BindSupplierDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer)
        Dim dt As New DataTable
        Try
            If ddlExistSuppliers.SelectedIndex > 0 Then
                dt = objCSM.LoadPartyDetails(sSession.AccessCode, sSession.AccessCodeID, iSupplier)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("CSM_Name")) = False Then
                        txtSupplierName.Text = dt.Rows(0)("CSM_Name")
                    Else
                        txtSupplierName.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_Code")) = False Then
                        txtSupplierCode.Text = dt.Rows(0)("CSM_Code")
                    Else
                        txtSupplierCode.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_ContactPerson")) = False Then
                        txtConatctPerson.Text = dt.Rows(0)("CSM_ContactPerson")
                    Else
                        txtConatctPerson.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_EmailID")) = False Then
                        txtEmail.Text = dt.Rows(0)("CSM_EmailID")
                    Else
                        txtEmail.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_MobileNo")) = False Then
                        txtMobile.Text = dt.Rows(0)("CSM_MobileNo")
                    Else
                        txtMobile.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_LandLineNo")) = False Then
                        txtLandLine.Text = dt.Rows(0)("CSM_LandLineNo")
                    Else
                        txtLandLine.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_Fax")) = False Then
                        txtFax.Text = dt.Rows(0)("CSM_Fax")
                    Else
                        txtFax.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_Address")) = False Then
                        txtAddress.Text = dt.Rows(0)("CSM_Address")
                    Else
                        txtAddress.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_Address1")) = False Then
                        txtAddress1.Text = dt.Rows(0)("CSM_Address1")
                    Else
                        txtAddress1.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_Address2")) = False Then
                        txtAddress2.Text = dt.Rows(0)("CSM_Address2")
                    Else
                        txtAddress2.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_Address3")) = False Then
                        txtAddress3.Text = dt.Rows(0)("CSM_Address3")
                    Else
                        txtAddress3.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_Pincode")) = False Then
                        txtPinCode.Text = dt.Rows(0)("CSM_Pincode")
                    Else
                        txtPinCode.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_City")) = False Then
                        If dt.Rows(0)("CSM_City") > 0 Then
                            ddlCity.SelectedValue = dt.Rows(0)("CSM_City")
                        Else
                            ddlCity.SelectedIndex = 0
                        End If
                    Else
                        ddlCity.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_State")) = False Then
                        If dt.Rows(0)("CSM_State") > 0 Then
                            ddlState.SelectedValue = dt.Rows(0)("CSM_State")
                        Else
                            ddlState.SelectedIndex = 0
                        End If
                    Else
                        ddlState.Items.Clear()
                    End If

                    If dt.Rows(0)("CSM_DelFlag").ToString() = "W" Then
                        lblStatus.Text = "Waiting for Approval"
                    ElseIf dt.Rows(0)("CSM_DelFlag").ToString() = "D" Then
                        lblStatus.Text = "De-Activated"
                    ElseIf dt.Rows(0)("CSM_DelFlag").ToString() = "A" Then
                        lblStatus.Text = "Activated"
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_SubGL")) = False Then
                        txtGLID.Text = dt.Rows(0)("CSM_SubGL")
                    Else
                        txtGLID.Text = 0
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_GSTNRegNo")) = False Then
                        txtGSTNRegNo.Text = dt.Rows(0)("CSM_GSTNRegNo")
                    Else
                        txtGSTNRegNo.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("CSM_CompanyType")) = False Then
                        If dt.Rows(0)("CSM_CompanyType") > 0 Then
                            ddlCompanyType.SelectedValue = dt.Rows(0)("CSM_CompanyType")
                        Else
                            ddlCompanyType.SelectedIndex = 0
                        End If
                    Else
                        ddlCompanyType.SelectedIndex = 0
                    End If

                    'If IsDBNull(dt.Rows(0)("CSM_GSTNCategory")) = False Then
                    '    If dt.Rows(0)("CSM_GSTNCategory") > 0 Then
                    '        BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    '        ddlGSTCategory.SelectedValue = dt.Rows(0)("CSM_GSTNCategory")
                    '    Else
                    '        ddlGSTCategory.SelectedIndex = 0
                    '    End If
                    'Else
                    '    ddlGSTCategory.SelectedIndex = 0
                    'End If
                    If IsDBNull(dt.Rows(0)("CSM_GSTNCategory")) = False Then
                        If dt.Rows(0)("CSM_GSTNCategory") > 0 Then
                            BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                            ddlGSTCategory.SelectedValue = dt.Rows(0)("CSM_GSTNCategory")
                            Dim taxcategory As String
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

                    dgBankDetails.DataSource = objCSM.BindBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSuppliers.SelectedValue)
                    dgBankDetails.DataBind()
                    ' BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExistSuppliers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistSuppliers.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistSuppliers.SelectedIndex > 0 Then
                BindSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSuppliers.SelectedValue)
                BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSuppliers.SelectedValue)
                imgbtnSave.Visible = False : divcollapseVAT.Visible = True
                If sSMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
            Else
                If sSMDSave = "YES" Then
                    imgbtnSave.Visible = True
                End If
                imgbtnUpdate.Visible = False : divcollapseVAT.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistSuppliers_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object
        Try
            lblError.Text = ""

            If lblStatus.Text = "Waiting for Approval" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf lblStatus.Text = "De-Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf lblStatus.Text = "Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Not Started" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            End If

            Response.Redirect(String.Format("~/Masters/SupplierMaster.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub dgOtherDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgOtherDetails.ItemDataBound
        Dim imgbtnDelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            lblError.Text = ""
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDelete = CType(e.Item.FindControl("imgbtnDelete"), ImageButton)
                imgbtnDelete.ImageUrl = "~/Images/Trash16.png"
                dgOtherDetails.Columns(3).Visible = False
                If sSMDSave = "YES" Then
                    dgOtherDetails.Columns(3).Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgOtherDetails_ItemDataBound")
        End Try
    End Sub

    Private Sub dgOtherDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgOtherDetails.ItemCommand
        Try
            If e.CommandName = "Delete" Then
                objCSM.DeleteCSMStatutoryNameValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text)
            End If
            BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSuppliers.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgOtherDetails_ItemCommand")
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
                dtBank = objCSM.GetBankDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistSuppliers.SelectedValue)
                If dtBank.Rows.Count > 0 Then
                    txtAccountNo.Text = dtBank.Rows(0)("SBD_AccountNo")
                    txtBankName.Text = dtBank.Rows(0)("SBD_BankName")
                    txtIFSCCode.Text = dtBank.Rows(0)("SBD_IFSC")
                    txtBranchName.Text = dtBank.Rows(0)("SBD_Branch")
                End If
            ElseIf e.CommandName = "Delete" Then
                objCSM.DeleteBankValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistSuppliers.SelectedValue)
                dgBankDetails.DataSource = objCSM.BindBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSuppliers.SelectedValue)
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
            If ddlExistSuppliers.SelectedIndex > 0 Then
                objCSM.SBD_ID = txtBankID.Text
                objCSM.SBD_Supplier_ID = ddlExistSuppliers.SelectedValue
                objCSM.SBD_AccountNo = txtAccountNo.Text
                objCSM.SBD_BankName = txtBankName.Text
                objCSM.SBD_IFSC = txtIFSCCode.Text
                objCSM.SBD_Branch = txtBranchName.Text
                objCSM.SBD_DelFlag = "X"
                objCSM.SBD_Status = "W"
                objCSM.SBD_CreatedBy = sSession.UserID
                objCSM.SBD_CreatedOn = Date.Today
                objCSM.SBD_UpdatedBy = sSession.UserID
                objCSM.SBD_UpdatedOn = Date.Today
                objCSM.SBD_CompID = sSession.AccessCodeID
                objCSM.SBD_YearID = sSession.YearID
                objCSM.SBD_Operation = "C"
                objCSM.SBD_IPAddress = sSession.IPAddress
                Arr = objCSM.SaveBankDetails(sSession.AccessCode, objCSM)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnBankSave.ImageUrl = "~/Images/Save16.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
                dgBankDetails.DataSource = objCSM.BindBankDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSuppliers.SelectedValue)
                dgBankDetails.DataBind()
                txtBankID.Text = 0
                ClearBankDetails()
            Else
                lblError.Text = "Select Existing Supplier"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBankSave_Click")
        End Try
    End Sub
    Private Sub ddlGSTCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGSTCategory.SelectedIndexChanged
        'Dim taxcategory As String
        Try
            'taxcategory = objCSM.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
            If UCase(ddlGSTCategory.SelectedItem.Text) = UCase("UNRIGISTERED DEALER") Then
                txtGSTNRegNo.Enabled = False
                txtGSTNRegNo.Text = ""
            Else
                txtGSTNRegNo.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgBankDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgBankDetails.ItemDataBound
        Try
            dgBankDetails.Columns(6).Visible = False
            If sSMDSave = "YES" Then
                dgBankDetails.Columns(6).Visible = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Private Sub dgOtherDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgOtherDetails.RowCommand
    '    Dim dt As New DataTable
    '    Dim lblsID As New Label
    '    Try
    '        lblError.Text = ""
    '        If e.CommandName = "Delete" Then
    '            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
    '            lblsID = DirectCast(clickedRow.FindControl("lblsID"), Label)
    '            objCSM.DeleteCSMStatutoryNameValue(sSession.AccessCode, sSession.AccessCodeID, lblsID.Text)
    '        End If
    '        ' BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgOtherDetails_RowCommand")
    '    End Try
    'End Sub
End Class
