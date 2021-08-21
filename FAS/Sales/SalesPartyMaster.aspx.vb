Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Sales_SalesPartyMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_SalesOrder"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objCSM As New ClsSalesPartyMaster
    Dim objCOA As New clsChartOfAccounts
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
        imgbtnCreateVAT.ImageUrl = "~/Images/Add24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sStr As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnUpdate.Visible = False
                'btnDelete.Visible = False
                BindState()
                bindCity()
                BindMasterDetails()
                BindGeneralCategory()
                lblID.Text = "0"

                sStr = Request.QueryString("Status")
                If sStr = "SO" Then
                    txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, sStr)
                Else
                    txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")
                End If
                'Me.imgbtnSave.Attributes.Add("OnClick", "return validation()")
                'btnStatutoryAdd.Attributes.Add("onclick", "javascript:return validationStatutoryRef();")
                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iPartyID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    BindDetails(iPartyID)
                End If

            End If
        Catch ex As Exception
            lblErrorUp.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
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
        Try
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

            objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 1)

            Arr = objCSM.SavePartyDetails(sSession.AccessCode, objCSM)

            If Arr(0) = "2" Then
                lblErrorUp.Text = "Successfully Updated"
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                imgbtnUpdate.Visible = False
                imgbtnSave.Visible = True 'btnDelete.Visible = True
                'btnSave.Text = "Save"
            ElseIf Arr(0) = "3" Then
                lblErrorUp.Text = "Successfully Saved"
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                imgbtnUpdate.Visible = True  'btnDelete.Visible = True
                imgbtnSave.Visible = False
                ' btnSave.Text = "Update"
            End If
            BindMasterDetails()
        Catch ex As Exception
            lblErrorUp.Text = ex.Message

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
            Throw
        End Try
    End Function
    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer) As Integer
        Dim sRet As String
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
            sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            sArray = sRet.Split(",")
            Return sArray(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub BindStatutoryReferencesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer)
        Dim dt As New DataTable
        Try
            dt = objCSM.LoadGridStatutoryReferencesDetails(sNameSpace, iCompID, iPartyID)
            dgOtherDetails.DataSource = dt
            dgOtherDetails.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub BindMasterDetails()
        Dim dt As New DataTable
        Try
            dt = objCSM.LoadGridMasterDetails(sSession.AccessCode, sSession.AccessCodeID)
            grdMaster.DataSource = dt
            grdMaster.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

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
                        lblErrorUp.Text = ""
                        objCSM.SaveCSMStatutoryNameValue(sSession.AccessCode, sSession.AccessCodeID, txtStatutoryName.Text, txtStatutoryValue.Text, iID, lblID.Text)
                        txtStatutoryName.Text = "" : txtStatutoryValue.Text = ""
                    End If
                Else
                    lblErrorUp.Text = "Select Party Hyper Link."
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
                BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                'ElseIf sStr = "D" Then
                'lblErrorUp.Text = "The party has been deleted." : lblErrorDown.Text = "The party has been deleted."
                '    Exit Sub
                'Else
                'lblErrorUp.Text = "Approve the party." : lblErrorDown.Text = "Approve the party."
                '    Exit Sub
                'End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblErrorUp.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnStatutoryAdd_Click")
        End Try
    End Sub

    Private Sub dgOtherDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgOtherDetails.RowCommand
        Dim dt As New DataTable
        Dim lblsID As New Label
        Try
            lblErrorUp.Text = ""
            If e.CommandName = "Delete" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblsID = DirectCast(clickedRow.FindControl("lblsID"), Label)
                objCSM.DeleteCSMStatutoryNameValue(sSession.AccessCode, sSession.AccessCodeID, lblsID.Text)
            End If
            BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblErrorUp.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgOtherDetails_RowCommand")
        End Try
    End Sub
    Private Sub grdMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdMaster.RowCommand
        Dim dt As New DataTable
        Dim lblCSM_ID As New Label
        Try
            lblErrorUp.Text = ""
            ' btnSave.Text = "Update"
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True  'btnDelete.Visible = True
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            lblCSM_ID = DirectCast(clickedRow.FindControl("lblCSM_ID"), Label)

            BindDetails(lblCSM_ID.Text)
        Catch ex As Exception
            lblErrorUp.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdMaster_ItemCommand")
        End Try
    End Sub
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
                    lblErrorUp.Text = "Waiting For Approval"
                    'btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("BM_Delflag") = "D") Then
                    ' btnDelete.Text = "ReCall"
                Else
                    'btnDelete.Text = "Delete"
                End If

                BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                If (dt.Rows(0)("BM_Delflag") = "X") Then
                    lblErrorUp.Text = "Waiting For Approval(After De-Activate)"
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("BM_Delflag") = "D") Then
                    lblErrorUp.Text = "De-Activate"
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("BM_Delflag") = "A") Then
                    lblErrorUp.Text = "Approved"
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "Delete"
                End If
                If (dt.Rows(0)("BM_Delflag") = "Y") Then
                    lblErrorUp.Text = "Waiting For Approval(After Activate)"
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("BM_Delflag") = "W") Then
                    lblErrorUp.Text = "Waiting For Approval"
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblErrorUp.Text = ""
            imgbtnSave.Visible = True
            'btnSave.Text = "Save"
            ' btnDelete.Text = "Delete"
            ddlCategory.SelectedIndex = 0
            txtSupplierName.Text = "" : txtSupplierCode.Text = ""
            txtConatctPerson.Text = "" : txtAddress.Text = "" : txtPinCode.Text = "" : txtAddress1.Text = "" : txtAddress2.Text = "" : txtAddress3.Text = ""
            ddlState.SelectedIndex = 0 : txtEmail.Text = "" : txtMobile.Text = "" : txtLandLine.Text = ""
            txtFax.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0
            lblID.Text = "0"
            txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")
        Catch ex As Exception
            lblErrorUp.Text = ex.Message

            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNew_Click")
        End Try
    End Sub
    Private Sub grdMaster_PreRender(sender As Object, e As EventArgs) Handles grdMaster.PreRender
        Dim dt As New DataTable
        Try
            If grdMaster.Rows.Count > 0 Then
                grdMaster.UseAccessibleHeader = True
                grdMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                grdMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_PreRender")
        End Try
    End Sub
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
    '                lblErrorUp.Text = "Deleted SuccessFully"
    '                lblCustomerValidationMsg.Text = lblErrorUp.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '                BindMasterDetails()
    '                txtSupplierName.Text = "" : txtSupplierCode.Text = ""
    '                txtConatctPerson.Text = "" : txtAddress.Text = "" : txtPinCode.Text = ""
    '                ddlState.SelectedIndex = 0 : txtEmail.Text = "" : txtMobile.Text = "" : txtLandLine.Text = ""
    '                txtFax.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0
    '                lblID.Text = "0"
    '            Else
    '                lblErrorUp.Text = "Select The Customer To Delete"
    '                lblCustomerValidationMsg.Text = lblErrorUp.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '            End If
    '        Else

    '            If (Convert.ToInt32(lblID.Text) > 0) Then
    '                objCSM.ReCallPartyMaster(sSession.AccessCode, sSession.AccessCodeID, lblID.Text, sSession.UserID)
    '                lblErrorUp.Text = "ReCalled SuccessFully"
    '                lblCustomerValidationMsg.Text = lblErrorUp.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '                BindMasterDetails()
    '                txtSupplierName.Text = "" : txtSupplierCode.Text = ""
    '                txtConatctPerson.Text = "" : txtAddress.Text = "" : txtPinCode.Text = ""
    '                ddlState.SelectedIndex = 0 : txtEmail.Text = "" : txtMobile.Text = "" : txtLandLine.Text = ""
    '                txtFax.Text = "" : ddlCity.SelectedIndex = 0 : ddlState.SelectedIndex = 0
    '                lblID.Text = "0"
    '            Else
    '                lblErrorUp.Text = "Select The Customer To Delete"
    '                lblCustomerValidationMsg.Text = lblErrorUp.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message

    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDelete.Click")
    '    End Try
    'End Sub
    Private Sub imgbtnCreateVAT_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCreateVAT.Click
        Try
            If lblID.Text > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            Else
                lblErrorUp.Text = "Select the customer"
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgOtherDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgOtherDetails.RowDataBound
        Try

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dgOtherDetails_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles dgOtherDetails.RowDeleted

    End Sub

    Private Sub dgOtherDetails_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles dgOtherDetails.RowDeleting

    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblErrorUp.Text = ""
            Response.Redirect(String.Format("SalesPartyDashboard.aspx?"), False)
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
End Class
