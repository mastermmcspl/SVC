Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Masters_FrmLgstRouteMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters/FrmLgstRouteMaster"
    Dim objGen As New clsFASGeneral
    Dim objCSM As New clsCustomer
    Private Shared sSession As AllSession
    Private objRM As New clsRouteMaster


    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
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
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnRoutePumpSave.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sStr As String = ""
        Dim sFormButtons As String = ""
        Try

            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LGSTDM")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False  'imgbtnBankSave.Visible = False : sCMDSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        ' imgbtnBankSave.Visible = True
                        sCMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If


                'divBankDetails.Visible = True
                txtPumpID.Text = 0

                txtGLID.Text = 0

                bindVehicleType()

                BindExistingRoute()

                imgbtnUpdate.Visible = False
                'imgbtnAdd.Visible = True
                bindDieselPump()
                bindCity()
                lblID.Text = 0


                Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")

                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iPartyID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlExistingRouteNo.SelectedValue = iPartyID
                    BindDetails(iPartyID)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub bindVehicleType()
        Try
            ddlVehicleType.DataSource = objRM.LoadVehicleType(sSession.AccessCode, sSession.AccessCodeID)
            ddlVehicleType.DataTextField = "Mas_Desc"
            ddlVehicleType.DataValueField = "Mas_Id"
            ddlVehicleType.DataBind()
            ddlVehicleType.Items.Insert(0, "Select Vehicle Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub bindCity()
        Dim dt As New DataTable
        Try
            dt = objCSM.LoadCity(sSession.AccessCode, sSession.AccessCodeID)
            ddlStartPlace.DataSource = dt
            ddlStartPlace.DataTextField = "Mas_Desc"
            ddlStartPlace.DataValueField = "Mas_Id"
            ddlStartPlace.DataBind()
            ddlStartPlace.Items.Insert(0, "Select Start City")

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub bindDieselPump()
        Dim dt As New DataTable
        Try
            dt = objRM.LoadDieselPump(sSession.AccessCode, sSession.AccessCodeID)
            ddlDiesel.DataSource = dt
            ddlDiesel.DataTextField = "LPM_PumpName"
            ddlDiesel.DataValueField = "LPM_Id"
            ddlDiesel.DataBind()
            ddlDiesel.Items.Insert(0, "Select DieselPump Name")

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindExistingRoute()
        Try
            ddlExistingRouteNo.DataSource = objRM.LoadExistingRouteNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingRouteNo.DataTextField = "LRM_StartDestPlace"
            ddlExistingRouteNo.DataValueField = "LRM_Id"
            ddlExistingRouteNo.DataBind()
            ddlExistingRouteNo.Items.Insert(0, "Select Existing Route Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDetails(ByVal iRouteID As Integer)
        Dim dt As New DataTable

        Try
            dt = objRM.LoadRouteMasterlDetails(sSession.AccessCode, sSession.AccessCodeID, iRouteID)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LRM_Id")
                If IsDBNull(dt.Rows(0)("LRM_VehicleType")) = False Then
                    ddlVehicleType.SelectedValue = dt.Rows(0)("LRM_VehicleType")
                Else
                    ddlVehicleType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LRM_StartPlace")) = False Then
                    If dt.Rows(0)("LRM_StartPlace") > 0 Then
                        ddlStartPlace.SelectedValue = dt.Rows(0)("LRM_StartPlace")
                        BindDestination()
                    Else
                        ddlStartPlace.SelectedIndex = 0
                    End If
                Else
                    ddlStartPlace.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LRM_DestPlace")) = False Then
                    If dt.Rows(0)("LRM_DestPlace") > 0 Then
                        ddlDestnPlace.SelectedValue = dt.Rows(0)("LRM_DestPlace")
                    Else
                        ddlDestnPlace.SelectedIndex = 0
                    End If
                Else
                    ddlDestnPlace.SelectedIndex = 0
                End If


                If IsDBNull(dt.Rows(0)("LRM_DistinKms")) = False Then
                    txtDistance.Text = dt.Rows(0)("LRM_DistinKms")
                Else
                    txtDistance.Text = "0.00"
                End If
                If IsDBNull(dt.Rows(0)("LRM_Rate")) = False Then
                    txtRate.Text = dt.Rows(0)("LRM_Rate")
                Else
                    txtRate.Text = "0.00"
                End If
                If IsDBNull(dt.Rows(0)("LRM_DriverAlnceAmt")) = False Then
                    TxtDrvrAlwnceAmt.Text = dt.Rows(0)("LRM_DriverAlnceAmt")
                Else
                    TxtDrvrAlwnceAmt.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LRM_PetrolQty")) = False Then
                    txtPtrlInLtr.Text = dt.Rows(0)("LRM_PetrolQty")
                Else
                    txtPtrlInLtr.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("LRM_AllottedTime")) = False Then
                    txtTimeAlltFrTrip.Text = dt.Rows(0)("LRM_AllottedTime")
                Else
                    txtTimeAlltFrTrip.Text = ""
                End If


                If (dt.Rows(0)("LRM_Delflag") = "W") Then
                    lblError.Text = "Waiting For Approval"
                    'btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LRM_Delflag") = "D") Then
                    ' btnDelete.Text = "ReCall"
                Else
                    'btnDelete.Text = "Delete"
                End If

                imgbtnSave.Visible = False
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                ' BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                If (dt.Rows(0)("LRM_Delflag") = "X") Then
                        lblStatus.Text = "Waiting For Approval(After De-Activate)"
                    imgbtnWaiting.Visible = True
                    imgbtnUpdate.Visible = False
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                    If (dt.Rows(0)("LRM_Delflag") = "D") Then
                    lblStatus.Text = "De-Activated"
                    imgbtnUpdate.Visible = False
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "ReCall"
                End If
                    If (dt.Rows(0)("LRM_Delflag") = "A") Then
                        lblStatus.Text = "Activated"
                        'lblCustomerValidationMsg.Text = lblError.Text
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        ' btnDelete.Text = "Delete"
                    End If
                    If (dt.Rows(0)("LRM_Delflag") = "Y") Then
                        lblStatus.Text = "Waiting For Approval(After Activate)"
                    imgbtnWaiting.Visible = True
                    imgbtnUpdate.Visible = False
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                    If (dt.Rows(0)("LRM_Delflag") = "W") Then
                        lblStatus.Text = "Waiting For Approval"
                    imgbtnWaiting.Visible = True
                    imgbtnUpdate.Visible = False
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If

                    If IsDBNull(dt.Rows(0)("LRM_SubGL")) = False Then
                        txtGLID.Text = dt.Rows(0)("LRM_SubGL")
                    Else
                        txtGLID.Text = 0
                    End If


                    dgPumpDetails.DataSource = objRM.BindRoutePumpDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingRouteNo.SelectedValue)
                    dgPumpDetails.DataBind()

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
            Dim sStartPlace As String = ""
            Dim sDestPlace As String = ""
            Dim sVehicleType As String = ""
            sStartPlace = ddlStartPlace.SelectedItem.ToString()
            sDestPlace = ddlDestnPlace.SelectedItem.ToString()
            sVehicleType = ddlVehicleType.SelectedItem.ToString()
            objRM.LRM_StartDestPlace = sStartPlace & " - " & sDestPlace & " - " & sVehicleType
            If ddlExistingRouteNo.SelectedIndex > 0 Then
            Else

                If ddlVehicleType.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Vehicle Type"
                    Exit Sub
                End If

                If ddlStartPlace.SelectedIndex > 0 Then
                    If ddlStartPlace.SelectedValue <> ddlDestnPlace.SelectedValue Then
                    Else
                        lblError.Text = "Select Different Start and Destination Place."
                        Exit Sub
                    End If
                Else
                    lblError.Text = "Select Start Place"
                    Exit Sub
                End If

                If ddlDestnPlace.SelectedIndex > 0 Then
                    If ddlStartPlace.SelectedValue <> ddlDestnPlace.SelectedValue Then
                    Else
                        lblError.Text = "Select Different Start and Destination Place."
                        Exit Sub
                    End If
                Else
                    lblError.Text = "Select Destination Place"
                    Exit Sub
                End If
                If txtDistance.Text = "" Then
                    lblError.Text = "Enter Distance"
                    Exit Sub
                End If
                If txtPtrlInLtr.Text = "" Then
                    lblError.Text = "Enter Petrol"
                    Exit Sub
                End If

                If ddlStartPlace.SelectedItem.Text = "Miscellaneous" And ddlDestnPlace.SelectedItem.Text = "Trip" Then
                    txtRate.Text = 0
                    TxtDrvrAlwnceAmt.Text = 0
                    txtTimeAlltFrTrip.Text = 0
                Else
                    If txtRate.Text = "" Then
                        lblError.Text = "Enter Rate"
                        Exit Sub
                    End If
                    If TxtDrvrAlwnceAmt.Text = "" Then
                        lblError.Text = "Enter Driver Allowance"
                        Exit Sub
                    End If
                    If txtTimeAlltFrTrip.Text = "" Then
                        lblError.Text = "Enter Allotted Time"
                        Exit Sub
                    End If
                End If


                If objRM.LRM_StartDestPlace <> "" Then
                    bCheck = objRM.CheckRouteNameDuplicate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(objRM.LRM_StartDestPlace))
                    If bCheck = True Then
                        lblError.Text = "This Route name and Vehicle type is already Exist"
                        Exit Sub
                    End If
                End If
            End If



            objRM.LRM_ID = lblID.Text
            objRM.LRM_VehicleType = ddlVehicleType.SelectedValue

            objRM.LRM_DistinKms = txtDistance.Text
            objRM.LRM_PetrolQty = txtPtrlInLtr.Text
            objRM.LRM_AllottedTime = txtTimeAlltFrTrip.Text
            objRM.LRM_Rate = txtRate.Text
            objRM.LRM_DriverAlnceAmt = TxtDrvrAlwnceAmt.Text




            If ddlStartPlace.SelectedIndex > 0 Then
                objRM.LRM_StartPlace = ddlStartPlace.SelectedValue
            Else
                objRM.LRM_StartPlace = 0
            End If
            If ddlDestnPlace.SelectedIndex > 0 Then
                objRM.LRM_DestPlace = ddlDestnPlace.SelectedValue
            Else
                objRM.LRM_DestPlace = 0
            End If

            If ddlExistingRouteNo.SelectedIndex > 0 Then
                objRM.LRM_Delflag = "A"
                objRM.LRM_Status = "A"
            Else
                objRM.LRM_Delflag = "W"
                objRM.LRM_Status = "C"
            End If

            objRM.LRM_CompID = sSession.AccessCodeID

            objRM.LRM_Operation = "C"
            objRM.LRM_IPAddress = sSession.IPAddress
            objRM.LRM_CreatedBy = sSession.UserID
            objRM.LRM_CreatedOn = Date.Today
            objRM.LRM_ApprovedBy = Nothing
            objRM.LRM_ApprovedOn = Date.Today
            objRM.LRM_DeletedBy = Nothing
            objRM.LRM_DeletedOn = Date.Today
            objRM.LRM_UpdatedBy = sSession.UserID
            objRM.LRM_UpdatedOn = Date.Today
            objRM.LRM_YearID = sSession.YearID


            iHead = 2
            objRM.LRM_Group = objRM.GetCOAGroup(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") 'sArray1(1) '29
            objRM.LRM_SubGroup = objRM.GetCOASubGroup(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") ' sArray1(2) '31
            objRM.LRM_GL = objRM.GetCOAGL(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") 'sArray1(3) '146

            If txtGLID.Text > 0 Then
                objRM.LRM_SubGL = CreateChartOfAccounts(Trim(objRM.LRM_StartDestPlace), 3, objRM.LRM_GL, 1, "Update")
            Else
                objRM.LRM_SubGL = CreateChartOfAccounts(Trim(objRM.LRM_StartDestPlace), 3, objRM.LRM_GL, 1, "Save")
            End If



            Arr = objRM.SaveRouteDetails(sSession.AccessCode, objRM)
            txtGLID.Text = 0

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                lblRouteValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                'If sCMDSave = "YES" Then
                imgbtnUpdate.Visible = False
                ' End If
                imgbtnSave.Visible = False 'btnDelete.Visible = True
                'btnSave.Text = "Save"
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
                lblRouteValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If  'btnDelete.Visible = True
                imgbtnSave.Visible = False
                ' btnSave.Text = "Update"
                lblStatus.Text = "Waiting For Approval"
            End If
            'BindMasterDetails()
            BindExistingRoute()
            ddlExistingRouteNo.SelectedValue = Arr(1)
            ddlExistingRouteNo_SelectedIndexChanged(sender, e)
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
    Private Sub ddlExistingRouteNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingRouteNo.SelectedIndexChanged
        Try
            If ddlExistingRouteNo.SelectedIndex > 0 Then
                BindDetails(ddlExistingRouteNo.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingRouteNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Masters/FrmLgstRouteMasterDashboard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
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
    Public Sub ClearPumpDetails()
        Try
            ddlDiesel.SelectedIndex = -1
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgPumpDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPumpDetails.ItemCommand
        Dim dtBank As New DataTable
        Try
            If e.CommandName = "Edit" Then
                txtPumpID.Text = e.Item.Cells(0).Text
                imgbtnRoutePumpSave.ImageUrl = "~/Images/Update16.png"
                dtBank = objRM.GetPumpDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingRouteNo.SelectedValue)
                If dtBank.Rows.Count > 0 Then
                    ddlDiesel.SelectedValue = dtBank.Rows(0)("LRD_PumpID")

                End If
            ElseIf e.CommandName = "Delete" Then
                objRM.DeletePumpValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingRouteNo.SelectedValue)
                dgPumpDetails.DataSource = objRM.BindRoutePumpDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingRouteNo.SelectedValue)
                dgPumpDetails.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPumpDetails_ItemCommand")
        End Try
    End Sub
    Private Sub imgbtnRoutePumpSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRoutePumpSave.Click
        Dim Arr() As String
        Try
            If ddlExistingRouteNo.SelectedIndex > 0 Then

                objRM.LRD_ID = txtPumpID.Text
                objRM.LRD_RouteID = ddlExistingRouteNo.SelectedValue
                objRM.LRD_PumpID = ddlDiesel.SelectedValue
                objRM.LRD_DelFlag = "X"
                objRM.LRD_Status = "W"
                objRM.LRD_CreatedBy = sSession.UserID
                objRM.LRD_CreatedOn = Date.Today
                objRM.LRD_UpdatedBy = sSession.UserID
                objRM.LRD_UpdatedOn = Date.Today
                objRM.LRD_CompID = sSession.AccessCodeID
                objRM.LRD_YearID = sSession.YearID
                objRM.LRD_Operation = "C"
                objRM.LRD_IPAddress = sSession.IPAddress
                Arr = objRM.SaveRoutePumpkDetails(sSession.AccessCode, objRM)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnRoutePumpSave.ImageUrl = "~/Images/Save16.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
                dgPumpDetails.DataSource = objRM.BindRoutePumpDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingRouteNo.SelectedValue)
                dgPumpDetails.DataBind()
                txtPumpID.Text = 0
                ClearPumpDetails()
                bindDieselPump()
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
            txtGLID.Text = 0 : txtRate.Text = "" : txtDistance.Text = "" : txtPtrlInLtr.Text = ""
            If sCMDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            imgbtnUpdate.Visible = False
            'btnSave.Text = "Save"
            ' btnDelete.Text = "Delete"
            ddlExistingRouteNo.SelectedIndex = 0 : lblStatus.Text = ""
            ddlStartPlace.SelectedIndex = 0 : ddlDestnPlace.SelectedIndex = 0
            ddlVehicleType.SelectedIndex = 0 : TxtDrvrAlwnceAmt.Text = "" : txtTimeAlltFrTrip.Text = ""
            lblID.Text = 0
            dgPumpDetails.DataSource = Nothing
            dgPumpDetails.DataBind()
            'txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub ddlStartPlace_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStartPlace.SelectedIndexChanged
        Try
            BindDestination()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub BindDestination()
        Try
            If ddlStartPlace.SelectedIndex > 0 Then
                ddlDestnPlace.DataSource = objRM.LoadDestinationCity(sSession.AccessCode, sSession.AccessCodeID, ddlStartPlace.SelectedValue)
                ddlDestnPlace.DataTextField = "Mas_Desc"
                ddlDestnPlace.DataValueField = "Mas_Id"
                ddlDestnPlace.DataBind()
                ddlDestnPlace.Items.Insert(0, "Select Destination City")
            Else
                lblError.Text = "Select Start City"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            lblError.Text = ""
            If ddlExistingRouteNo.SelectedIndex > 0 Then
                objRM.UpdateRouteMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingRouteNo.SelectedValue, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                lblStatus.Text = "Approved"
                lblError.Text = "Successfully Approved"
                lblRouteValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                imgbtnUpdate.Visible = True : imgbtnWaiting.Visible = False : imgbtnSave.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
