Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Logistics_FrmLgstTRDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters/FrmLgstTRDashboard"
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper


    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Dim objCOA As New clsChartOfAccounts
    Dim objRM As New clsRouteMaster
    Dim objVM As New clsVehicleMaster
    Dim objDM As New clsDriverMaster
    Dim objTG As New clsTripGeneration
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
        imgbtnAddHopOn.ImageUrl = "~/Images/Add24.png"
        ImgbtnDieselSave.ImageUrl = "~/Images/Add24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sStr As String = ""
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LTGF")
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
                    If sFormButtons.Contains(",Exception") = True Then
                        txtPtrlinLtrs.Enabled = True '                
                        '    lblOwner.Text = "Access taken by owner" : lblOwner.Visible = True
                    End If
                End If

                txtGLID.Text = 0
                bindVehicleType()
                BindRoute()
                BindExistingVehicleNo()
                BindStartCustomer()
                BindDestinationCustomer()
                BindExistingDriver()
                'BindExistingTripGenerationNo()
                ddlTripStatus.SelectedIndex = 1

                imgbtnUpdate.Visible = False

                lblID.Text = "0"
                txtHopOnID.Text = 0
                txtDieselId.Text = 0
                BindExistingTGNo()

                'Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")

                'Dim sPID As String = ""
                'sPID = Request.QueryString("PID")
                'If sPID <> "" Then
                '    Dim iTGID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                '    ddlExistingTripDetails.SelectedValue = iTGID
                '    'BindDetails(iTGID)
                '    ddlExistingTripDetails_SelectedIndexChanged(sender, e)
                'End If
                Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")
                '   txtTransactionNo.Text = objTG.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                Dim sPID As String = ""
                sPID = sSession.pkoid
                If sPID <> "" Then
                    Dim iTGID As Integer = sSession.pkoid
                    ddlExistingTripDetails.SelectedValue = iTGID
                    BindDetails(iTGID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub bindVehicleType()
        Try
            ddlVehicleType.DataSource = objTG.LoadVehicleType(sSession.AccessCode, sSession.AccessCodeID)
            ddlVehicleType.DataTextField = "Mas_Desc"
            ddlVehicleType.DataValueField = "Mas_Id"
            ddlVehicleType.DataBind()
            ddlVehicleType.Items.Insert(0, "Select Vehicle Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindExistingTGNo()
        Try
            lblError.Text = ""
            ddlExistingTripDetails.DataSource = objTG.LoadExistingTripGenNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingTripDetails.DataTextField = "LTGM_TransactionNo"
            ddlExistingTripDetails.DataValueField = "LTGM_ID"
            ddlExistingTripDetails.DataBind()
            ddlExistingTripDetails.Items.Insert(0, "Select Existing Trip No.")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindDieselPump()
        Try
            ddlDieselPump.DataSource = objTG.LoadDieselPump(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlRoute.SelectedValue)
            ddlDieselPump.DataTextField = "LPM_PumpName"
            ddlDieselPump.DataValueField = "LPM_ID"
            ddlDieselPump.DataBind()
            ddlDieselPump.Items.Insert(0, "Select Diesel/Petrol Pump name.")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindRoute()
        Try
            ddlRoute.DataSource = objTG.LoadExistingRouteNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlRoute.DataTextField = "LRM_StartDestPlace"
            ddlRoute.DataValueField = "LRM_Id"
            ddlRoute.DataBind()
            ddlRoute.Items.Insert(0, "Select Route Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindRouteAll()
        Try
            ddlRoute.DataSource = objTG.LoadExistingRouteAll(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlRoute.DataTextField = "LRM_StartDestPlace"
            ddlRoute.DataValueField = "LRM_Id"
            ddlRoute.DataBind()
            ddlRoute.Items.Insert(0, "Select Route Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindExistingVehicleNo()
        Try
            ddlVehicleNo.DataSource = objVM.LoadExistingVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlVehicleNo.DataTextField = "LVM_RegNo"
            ddlVehicleNo.DataValueField = "LVM_ID"
            ddlVehicleNo.DataBind()
            ddlVehicleNo.Items.Insert(0, "Select Vehicle No.")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindVehicleNo()
        Try
            ddlVehicleNo.DataSource = objVM.LoadVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlVehicleNo.DataTextField = "LVM_RegNo"
            ddlVehicleNo.DataValueField = "LVM_ID"
            ddlVehicleNo.DataBind()
            ddlVehicleNo.Items.Insert(0, "Select Vehicle No.")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindStartCustomer()
        Try
            ddlStartCustomer.DataSource = objTG.BindStartCustomer(sSession.AccessCode, sSession.AccessCodeID)
            ddlStartCustomer.DataTextField = "Cust_Name"
            ddlStartCustomer.DataValueField = "Cust_ID"
            ddlStartCustomer.DataBind()
            ddlStartCustomer.Items.Insert(0, "Select Start Customer")
            ddlStartCustomer.SelectedIndex = 1
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDestinationCustomer()
        Try
            ddlDestinCustomer.DataSource = objTG.BindDestinationCustomer(sSession.AccessCode, sSession.AccessCodeID)
            ddlDestinCustomer.DataTextField = "BM_Name"
            ddlDestinCustomer.DataValueField = "BM_ID"
            ddlDestinCustomer.DataBind()
            ddlDestinCustomer.Items.Insert(0, "Select Destination Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlVehicleType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVehicleType.SelectedIndexChanged
        lblError.Text = ""
        'Dim dtRouteDetails As New DataTable
        'Try
        '    dtRouteDetails = objTG.GetRouteMasterDetails(sSession.AccessCode, ddlRoute.SelectedValue, ddlVehicleType.SelectedIndex)
        '    If dtRouteDetails.Rows.Count > 0 Then

        '        BindDieselPump()

        '        If IsDBNull(dtRouteDetails.Rows(0)("LRM_Rate")) = False Then
        '            txtRate.Text = dtRouteDetails.Rows(0)("LRM_Rate")
        '        Else
        '            txtRate.Text = ""
        '        End If
        '        If IsDBNull(dtRouteDetails.Rows(0)("LRM_DistinKms")) = False Then
        '            TxtDstanceKM.Text = dtRouteDetails.Rows(0)("LRM_DistinKms")
        '        Else
        '            TxtDstanceKM.Text = ""
        '        End If
        '        If IsDBNull(dtRouteDetails.Rows(0)("LRM_DriverAlnceAmt")) = False Then
        '            txtelgblAmt.Text = dtRouteDetails.Rows(0)("LRM_DriverAlnceAmt")
        '        Else
        '            txtelgblAmt.Text = ""
        '        End If
        '        If IsDBNull(dtRouteDetails.Rows(0)("LRM_StartPlace")) = False Then
        '            txtStartCity.Text = objTG.LoadCity(sSession.AccessCode, sSession.AccessCodeID, dtRouteDetails.Rows(0)("LRM_StartPlace"))
        '        Else
        '            txtStartCity.Text = ""
        '        End If
        '        If IsDBNull(dtRouteDetails.Rows(0)("LRM_DestPlace")) = False Then
        '            txtDestinCity.Text = objTG.LoadCity(sSession.AccessCode, sSession.AccessCodeID, dtRouteDetails.Rows(0)("LRM_DestPlace"))
        '        Else
        '            txtDestinCity.Text = ""
        '        End If

        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlVehicleType_SelectedIndexChanged")
        'End Try
    End Sub

    Private Sub ddlStartCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStartCustomer.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlStartCustomer.SelectedIndex > 0 Then
                ddlDestinCustomer.DataSource = objTG.BindDestinationCustomer(sSession.AccessCode, sSession.AccessCodeID)
                ddlDestinCustomer.DataTextField = "BM_Name"
                ddlDestinCustomer.DataValueField = "BM_ID"
                ddlDestinCustomer.DataBind()
                ddlDestinCustomer.Items.Insert(0, "Select Destination Customer")
            Else
                lblError.Text = "Select Start Customer"
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStartCustomer_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub BindExistingDriver()
        Try
            lblError.Text = ""
            ddlDriver.DataSource = objDM.LoadExistingDriver(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlDriver.DataTextField = "LDM_DriverName"
            ddlDriver.DataValueField = "LDM_ID"
            ddlDriver.DataBind()
            ddlDriver.Items.Insert(0, "Select Driver")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindDriver()
        Try

            ddlDriver.DataSource = objDM.LoadDriver(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlDriver.DataTextField = "LDM_DriverName"
            ddlDriver.DataValueField = "LDM_ID"
            ddlDriver.DataBind()
            ddlDriver.Items.Insert(0, "Select Driver")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDetails(ByVal iTGID As Integer)
        Dim dt As New DataTable
        Dim iCustCompanyType As Integer = 0
        Try
            lblError.Text = ""
            dt = objTG.LoadTGMasterlDetails(sSession.AccessCode, sSession.AccessCodeID, iTGID, sSession.YearID)
            If dt.Rows.Count > 0 Then
                BindVehicleNo()
                BindDriver()
                lblID.Text = dt.Rows(0)("LTGM_ID")
                If IsDBNull(dt.Rows(0)("LTGM_RouteID")) = False Then
                    If dt.Rows(0)("LTGM_RouteID") > 0 Then
                        BindRouteAll()
                        ddlRoute.SelectedValue = dt.Rows(0)("LTGM_RouteID")
                    Else
                        ddlRoute.SelectedIndex = 0
                    End If
                Else
                    ddlRoute.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LTGM_TransactionNo")) = False Then
                    txtTransactionNo.Text = dt.Rows(0)("LTGM_TransactionNo")
                Else
                    txtTransactionNo.Text = ""
                End If

                BindDieselPump()

                If IsDBNull(dt.Rows(0)("LTGM_VehicleType")) = False Then
                    ddlVehicleType.SelectedValue = dt.Rows(0)("LTGM_VehicleType")
                Else
                    ddlVehicleType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LTGM_TripStatus")) = False Then
                    ddlTripStatus.SelectedIndex = dt.Rows(0)("LTGM_TripStatus")
                Else
                    ddlTripStatus.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LTGM_VehivleNo")) = False Then
                    If dt.Rows(0)("LTGM_VehivleNo") > 0 Then
                        ddlVehicleNo.SelectedValue = dt.Rows(0)("LTGM_VehivleNo")
                    Else
                        ddlVehicleNo.SelectedIndex = 0
                    End If
                Else
                    ddlVehicleNo.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LTGM_StartCity")) = False Then
                    txtStartCity.Text = dt.Rows(0)("LTGM_StartCity")
                Else
                    txtStartCity.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_DestinationCity")) = False Then
                    txtDestinCity.Text = dt.Rows(0)("LTGM_DestinationCity")
                Else
                    txtDestinCity.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_DistanceinKms")) = False Then
                    TxtDstanceKM.Text = dt.Rows(0)("LTGM_DistanceinKms")
                Else
                    TxtDstanceKM.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_Rate")) = False Then
                    txtRate.Text = dt.Rows(0)("LTGM_Rate")
                Else
                    txtRate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("LTGM_PetrolQty")) = False Then
                    txtPtrlinLtrs.Text = dt.Rows(0)("LTGM_PetrolQty")
                Else
                    txtPtrlinLtrs.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("LTGM_StartCustomer")) = False Then
                    If dt.Rows(0)("LTGM_StartCustomer") > 0 Then
                        ddlStartCustomer.SelectedValue = dt.Rows(0)("LTGM_StartCustomer")
                    Else
                        ddlStartCustomer.SelectedIndex = 0
                    End If
                Else
                    ddlStartCustomer.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LTGM_DestinationCustomer")) = False Then
                    If dt.Rows(0)("LTGM_DestinationCustomer") > 0 Then
                        ddlDestinCustomer.SelectedValue = dt.Rows(0)("LTGM_DestinationCustomer")
                    Else
                        ddlDestinCustomer.SelectedIndex = 0
                    End If
                Else
                    ddlDestinCustomer.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LTGM_SVCNo")) = False Then
                    txtSVCNo.Text = dt.Rows(0)("LTGM_SVCNo")
                Else
                    txtSVCNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_ClientRefNo")) = False Then
                    txtCRN.Text = dt.Rows(0)("LTGM_ClientRefNo")
                Else
                    txtCRN.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("LTGM_Driver")) = False Then
                    If dt.Rows(0)("LTGM_Driver") > 0 Then
                        ddlDriver.SelectedValue = dt.Rows(0)("LTGM_Driver")
                    Else
                        ddlDriver.SelectedIndex = 0
                    End If
                Else
                    ddlDriver.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("LTGM_DriverAmount")) = False Then
                    txtelgblAmt.Text = dt.Rows(0)("LTGM_DriverAmount")
                    If Val(txtelgblAmt.Text) = 0 Then
                        txtAdvncsPaidDriver.Text = 0
                    End If
                Else
                    txtelgblAmt.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_AllottedTime")) = False Then
                    txtTimeAlltdFrTrip.Text = dt.Rows(0)("LTGM_AllottedTime")
                Else
                    txtTimeAlltdFrTrip.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_TripTakenTime")) = False Then
                    txtTimeTknTrip.Text = dt.Rows(0)("LTGM_TripTakenTime")
                Else
                    txtTimeTknTrip.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_TimeStatus")) = False Then
                    lbltripTimeStatus.Text = dt.Rows(0)("LTGM_TimeStatus")
                Else
                    lbltripTimeStatus.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_Remarks")) = False Then
                    txtRemarks.Text = dt.Rows(0)("LTGM_Remarks")
                Else
                    txtRemarks.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_StartTime")) = False Then
                    txtStartTime.Text = dt.Rows(0)("LTGM_StartTime")
                Else
                    txtStartTime.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("LTGM_StopTime")) = False Then
                    txtStopTime.Text = dt.Rows(0)("LTGM_StopTime")
                Else
                    txtStopTime.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_EWayBillNo")) = False Then
                    txtEwayBillNo.Text = dt.Rows(0)("LTGM_EWayBillNo")
                Else
                    txtEwayBillNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_StartDate")) = False Then
                    txtStartDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LTGM_StartDate").ToString(), "D")
                Else
                    txtStartDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("LTGM_StopDate")) = False Then
                    If dt.Rows(0)("LTGM_StopDate") = "1900-01-01" Then
                        txtStopDate.Text = ""
                    Else
                        txtStopDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("LTGM_StopDate").ToString(), "D")
                    End If
                Else
                    txtStopDate.Text = ""
                End If
                If dt.Rows(0)("LTGM_MRStart") <> 0 Then
                    txtMRStart.Text = dt.Rows(0)("LTGM_MRStart")
                Else
                    txtMRStart.Text = 0
                End If
                If dt.Rows(0)("LTGM_MREnd") <> 0 Then
                    txtMREnd.Text = dt.Rows(0)("LTGM_MREnd")
                Else
                    txtMREnd.Text = ""
                End If
                lblMeterStatus.Text = dt.Rows(0)("LTGM_MRStatus")

                If IsDBNull(dt.Rows(0)("LTGM_CompanyAddress")) = False Then
                    txtCompanyAddress.Text = dt.Rows(0)("LTGM_CompanyAddress")
                Else
                    txtCompanyAddress.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("LTGM_CompanyGSTNRegNo")) = False Then
                    txtCompanyGSTN.Text = dt.Rows(0)("LTGM_CompanyGSTNRegNo")
                Else
                    txtCompanyGSTN.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_CustomerAddress")) = False Then
                    txtCustomerAddress.Text = dt.Rows(0)("LTGM_CustomerAddress")
                Else
                    txtCustomerAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LTGM_CustomerGSTNRegNo")) = False Then
                    txtCustGSTN.Text = dt.Rows(0)("LTGM_CustomerGSTNRegNo")
                Else
                    txtCustGSTN.Text = ""
                End If
                iCustCompanyType = dt.Rows(0)("LTGM_GSTNCategory")
                txtCustGstnCategoryId.Text = iCustCompanyType
                Dim description As String
                description = objTG.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, iCustCompanyType)
                If UCase(description) = UCase("RCM DEALER") Then
                    txtCustGSTRate.Text = 0
                    txtCustGstnCategory.Text = "RCM DEALER"
                End If
                If UCase(description) = UCase("NORMAL GST DEALER") Then
                    txtCustGSTRate.Text = 12
                    txtCustGstnCategory.Text = "NORMAL GST DEALER"
                End If
                If UCase(description) = UCase("REDUCED GST DEALER") Then
                    txtCustGSTRate.Text = 5
                    txtCustGstnCategory.Text = "REDUCED GST DEALER"
                End If


                If (dt.Rows(0)("LTGM_Delflag") = "W") Then
                    lblError.Text = "Waiting For Approval"
                    'btnDelete.Text = "ReCall"
                    dgDiesel.Enabled = True : dgHopOn.Enabled = True
                    imgbtnAddHopOn.Enabled = True : ImgbtnDieselSave.Enabled = True
                End If
                If (dt.Rows(0)("LTGM_Delflag") = "D") Then
                    ' btnDelete.Text = "ReCall"
                Else
                    'btnDelete.Text = "Delete"
                End If

                imgbtnSave.Visible = False
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                ' BindStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                If (dt.Rows(0)("LTGM_Delflag") = "X") Then
                    lblStatus.Text = "Waiting For Approval(After De-Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LTGM_Delflag") = "D") Then
                    lblStatus.Text = "De-Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "ReCall"
                End If
                If (dt.Rows(0)("LTGM_Delflag") = "A") Then
                    lblStatus.Text = "Activated"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ' btnDelete.Text = "Delete"
                    imgbtnWaiting.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False
                    dgDiesel.Enabled = False : dgHopOn.Enabled = False
                    imgbtnAddHopOn.Enabled = False : ImgbtnDieselSave.Enabled = False
                End If
                If (dt.Rows(0)("LTGM_Delflag") = "Y") Then
                    lblStatus.Text = "Waiting For Approval(After Activate)"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                If (dt.Rows(0)("LTGM_Delflag") = "W") Then
                    lblStatus.Text = "Waiting For Approval"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    imgbtnWaiting.Visible = True
                End If

                'If IsDBNull(dt.Rows(0)("LPM_SubGL")) = False Then
                '    txtGLID.Text = dt.Rows(0)("LPM_SubGL")
                'Else
                '    txtGLID.Text = 0
                'End If

                Dim dtHopOn As New DataTable, dtDiesel As New DataTable, dtTransDetails As New DataTable

                dtHopOn = objTG.BindHopOnDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                If dtHopOn.Rows.Count > 0 Then
                    dgHopOn.Visible = True
                    dgHopOn.DataSource = dtHopOn
                    dgHopOn.DataBind()
                Else
                    dgHopOn.Visible = False
                End If

                dtDiesel = objTG.BindDieselPumpDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                If dtDiesel.Rows.Count > 0 Then
                    dgDiesel.Visible = True
                    dgDiesel.DataSource = dtDiesel
                    dgDiesel.DataBind()
                Else
                    dgDiesel.Visible = False
                End If

                dtTransDetails = objTG.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingTripDetails.SelectedValue)
                If dtTransDetails.Rows.Count > 0 Then
                    dgINVDetails.Visible = True
                    dgINVDetails.DataSource = dtTransDetails
                    dgINVDetails.DataBind()
                Else
                    dgINVDetails.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
        End Try
    End Sub
    Public Function CheckSourceDestinationOfCust(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            lblError.Text = ""
            sSource = sBillingAddress.Substring(0, 2)
            sDestination = sDeliveryAddress.Substring(0, 2)

            If sSource = sDestination Then
                CheckSourceDestinationOfCust = "Local"
            Else
                CheckSourceDestinationOfCust = "Inter State"
            End If
            Return CheckSourceDestinationOfCust
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationOfCust")
        End Try
    End Function
    Public Function CheckSourceDestinationState(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            lblError.Text = ""
            'sSource = sBillingAddress.Substring(0, 2)
            'sDestination = sDeliveryAddress.Substring(0, 2)

            'If sSource = sDestination Then
            '    CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
            'Else
            '    CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
            'End If
            If sBillingAddress <> "" And sDeliveryAddress = "" Then
                sSource = sBillingAddress.Substring(0, 2)
                CheckSourceDestinationState = objTG.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sDeliveryAddress <> "" Then
                sDestination = sDeliveryAddress.Substring(0, 2)
                CheckSourceDestinationState = objTG.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sDeliveryAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sDeliveryAddress.Substring(0, 2)
                If sSource = sDestination Then
                    CheckSourceDestinationState = objTG.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    CheckSourceDestinationState = objTG.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return CheckSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationState")
        End Try
    End Function
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
            Throw
        End Try
    End Function
    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer, ByVal sStatus As String, ByVal sReason As String) As Integer
        Dim sRet As String = ""
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try
            objCOA.igl_id = 0
            objCOA.igl_head = iHead
            objCOA.igl_Parent = iParent
            objCOA.sgl_glcode = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, iAccHead, iParent)
            objCOA.sgl_Desc = objGen.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(sReason)
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
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sSubGrpcode As String = "", sGLCode As String = ""
        Dim lParentId As Integer = 0, iHead As Integer = 2
        Dim bCheck As Boolean
        Dim sBillStatus As String
        Dim dDate, dSDate As Date
        Dim dFinalVal As Double = 0.0
        Dim dToDate, dSToDate As Date
        Dim m As Integer
        Try
            lblError.Text = ""
            If (txtStartDate.Text <> "") Then
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Start Date (" & txtStartDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    txtStartDate.Focus()
                    Exit Sub
                End If
                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Start Date (" & txtStartDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    txtStartDate.Focus()
                    Exit Sub
                End If
            End If
            If (txtStartDate.Text <> "" And txtStopDate.Text <> "") Then
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Start Date (" & txtStartDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    txtStartDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Start Date (" & txtStartDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    txtStartDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'

                'Cheque Date Comparision'
                dToDate = Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSToDate = Date.ParseExact(txtStopDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dToDate, dSToDate)
                If m < 0 Then
                    lblError.Text = "Stop Date (" & txtStopDate.Text & ") should be Greater than or equal to Start Date(" & txtStartDate.Text & ")."
                    txtStopDate.Focus()
                    Exit Sub
                End If

                'dToDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'dSToDate = Date.ParseExact(txtStopDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'm = DateDiff(DateInterval.Day, dToDate, dSToDate)
                'If m > 0 Then
                '    lblError.Text = "Stop Date Date (" & txtStopDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                '    txtStopDate.Focus()
                '    Exit Sub
                'End If
                'Cheque Date Comparision'


                'dDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'dSDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'm = DateDiff(DateInterval.Day, dDate, dSDate)
                'If m < 0 Then
                '    lblError.Text = "To Date (" & txtToDate.Text & ") should be Greater than From Date(" & txtFromDate.Text & ")."
                '    txtToDate.Focus()
                '    Exit Sub
                'End If
            End If
            If ddlExistingTripDetails.SelectedIndex > 0 Then
            Else
                If txtStartCity.Text <> "" Then
                Else
                    lblError.Text = "Enter Start City"
                    txtStartCity.Focus()
                    Exit Sub
                End If
                If txtDestinCity.Text <> "" Then
                Else
                    lblError.Text = "Enter Destination City"
                    txtDestinCity.Focus()
                    Exit Sub
                End If
                If ddlRoute.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Route"
                    ddlRoute.Focus()
                    Exit Sub
                End If
                If ddlVehicleType.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Vehicle Type"
                    ddlVehicleType.Focus()
                    Exit Sub
                End If
                If ddlVehicleNo.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Vehicle No"
                    ddlVehicleNo.Focus()
                    Exit Sub
                End If
                If ddlStartCustomer.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Start Customer"
                    ddlStartCustomer.Focus()
                    Exit Sub
                End If
                If ddlDestinCustomer.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Destination Customer"
                    ddlDestinCustomer.Focus()
                    Exit Sub
                End If
                If ddlDriver.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Driver"
                    ddlDriver.Focus()
                    Exit Sub
                End If

                If txtelgblAmt.Text <> "" Then
                Else
                    lblError.Text = "Enter Driver Amount"
                    txtelgblAmt.Focus()
                    Exit Sub
                End If
                If ddlTripStatus.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select Trip Status"
                    ddlTripStatus.Focus()
                    Exit Sub
                End If
                If txtCRN.Text <> "" Then
                Else
                    lblError.Text = "Enter Client Ref No."
                    txtCRN.Focus()
                    Exit Sub
                End If
                If txtStartDate.Text <> "" Then
                Else
                    lblError.Text = "Select Start Date"
                    txtStartDate.Focus()
                    Exit Sub
                End If
                If txtStartTime.Text <> "" Then
                Else
                    lblError.Text = "Enter Start Time"
                    txtStartTime.Focus()
                    Exit Sub
                End If
                If txtTimeAlltdFrTrip.Text <> "" Then
                Else
                    lblError.Text = "Enter Alloted time"
                    txtTimeAlltdFrTrip.Focus()
                    Exit Sub
                End If
                If txtMRStart.Text <> "" Then
                Else
                    lblError.Text = "Enter Meter Reading Starting Point"
                    txtMRStart.Focus()
                    Exit Sub
                End If
            End If

            If txtCompanyGSTN.Text <> "" And txtCustGSTN.Text <> "" Then
                sBillStatus = CheckSourceDestinationOfCust(Trim(txtCompanyGSTN.Text), Trim(txtCustGSTN.Text))
            ElseIf txtCompanyGSTN.Text <> "" And txtCustGSTN.Text = "" Then
                sBillStatus = "Local"
            ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text <> "" Then
                sBillStatus = "Local"
            ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text = "" Then
                sBillStatus = "Local"
            End If
            Dim dTotalTripAmt As Double = 0.0, dGSTRate As Double = 0.0, dTotalAmount As Double = 0.0
            Dim dGstAmount As Double = 0.0
            Dim dSGST As Double = 0.0, dCGST As Double = 0.0, dIGST As Double = 0.0
            Dim dSGSTAmount As Double = 0.0, dCGSTAmount As Double = 0.0, dIGSTAmount As Double = 0.0
            If txtRate.Text <> "" Then
                dTotalTripAmt = txtRate.Text
            Else
                lblError.Text = "Enter Trip Rate"
                Exit Sub
            End If
            dGSTRate = txtCustGSTRate.Text
            dTotalAmount = dTotalTripAmt * dGSTRate
            dGstAmount = (dTotalAmount / 100)
            If sBillStatus = "Local" Then
                dSGST = (dGSTRate / 2)
                dSGSTAmount = (dGstAmount / 2)
                dCGST = (dGSTRate / 2)
                dCGSTAmount = (dGstAmount / 2)
                '   drow("IGST") = 0
                dIGST = 0
                dIGSTAmount = 0
            ElseIf sBillStatus = "Inter State" Then
                dSGSTAmount = 0
                '  drow("CGST") = 0
                '  drow("CGSTAmount") = 0
                '   drow("IGST") = drow("GSTRate")
                dSGST = 0
                dSGSTAmount = 0
                dCGST = 0
                dCGSTAmount = 0
                dIGST = dGSTRate
                dIGSTAmount = dGstAmount
            End If
            ' drow("GrandTotal") = (drow("TotalTripAmount") + dGstAmount)

            objTG.LTGM_CompanyAddress = txtCompanyAddress.Text
            objTG.LTGM_CompanyGSTNRegNo = txtCompanyGSTN.Text

            objTG.LTGM_CustomerAddress = txtCustomerAddress.Text
            objTG.LTGM_CustomerGSTNRegNo = txtCustGSTN.Text

            objTG.LTGM_GSTNCategory = txtCustGstnCategoryId.Text
            objTG.LTGM_GSTRate = txtCustGSTRate.Text


            If txtCompanyGSTN.Text <> "" And txtCustGSTN.Text <> "" Then
                objTG.LTGM_GSTCustBillStatus = CheckSourceDestinationOfCust(Trim(txtCompanyGSTN.Text), Trim(txtCustGSTN.Text))
            ElseIf txtCompanyGSTN.Text <> "" And txtCustGSTN.Text = "" Then
                objTG.LTGM_GSTCustBillStatus = "Local"
            ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text <> "" Then
                objTG.LTGM_GSTCustBillStatus = "Local"
            ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text = "" Then
                objTG.LTGM_GSTCustBillStatus = "Local"
            End If

            If txtCompanyGSTN.Text <> "" And txtCustGSTN.Text <> "" Then
                objTG.LTGM_State = CheckSourceDestinationState(Trim(txtCompanyGSTN.Text), Trim(txtCustGSTN.Text))
            ElseIf txtCompanyGSTN.Text <> "" And txtCustGSTN.Text = "" Then
                objTG.LTGM_State = CheckSourceDestinationState(Trim(txtCompanyGSTN.Text), (""))
            ElseIf txtCompanyGSTN.Text = "" And txtCustGSTN.Text <> "" Then
                objTG.LTGM_State = CheckSourceDestinationState((""), Trim(txtCustGSTN.Text))
            End If


            'Chart Of Accounts'
            Dim iHead1, iGroup, iSubGroup, iGL, iChartID As Integer
            Dim sPerm As String = ""
            Dim sArray1 As Array
            Dim sName As String = ""

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead1 = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146

            'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
            sName = "Sale Of Product " & objTG.LTGM_State
            txtGLID.Text = objTG.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
            Else
                iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
            End If


            sName = "Local GST " & objTG.LTGM_GSTRate & " % " & objTG.LTGM_State & " Sale Account"
            txtGLID.Text = objTG.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", objTG.LTGM_GSTRate)
            End If

            sName = "Inter State GST " & objTG.LTGM_GSTRate & " % " & objTG.LTGM_State & " Sale Account"
            txtGLID.Text = objTG.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", objTG.LTGM_GSTRate)
            End If

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead1 = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146

            sName = "OUTPUT SGST " & objTG.LTGM_GSTRate / 2 & " % " & objTG.LTGM_State & " Sale Account"
            txtGLID.Text = objTG.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", objTG.LTGM_GSTRate / 2)
            End If

            sName = "OUTPUT CGST " & objTG.LTGM_GSTRate / 2 & " % " & objTG.LTGM_State & " Sale Account"
            txtGLID.Text = objTG.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", objTG.LTGM_GSTRate / 2)
            End If

            sName = "OUTPUT IGST " & objTG.LTGM_GSTRate & " % " & objTG.LTGM_State & " Sale Account"
            txtGLID.Text = objTG.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
            Else
                CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", objTG.LTGM_GSTRate)
            End If




            objTG.LTGM_GSTAmount = dGstAmount

            objTG.LTGM_SGST = dSGST
            objTG.LTGM_SGSTAmount = dSGSTAmount
            objTG.LTGM_CGST = dCGST
            objTG.LTGM_CGSTAmount = dCGSTAmount
            objTG.LTGM_IGST = dIGST
            objTG.LTGM_IGSTAmount = dIGSTAmount



            objTG.LTGM_ID = lblID.Text
            If ddlRoute.SelectedIndex > 0 Then
                objTG.LTGM_RouteID = ddlRoute.SelectedValue
            Else
                objTG.LTGM_RouteID = 0
            End If
            If ddlExistingTripDetails.SelectedIndex > 0 Then
                objTG.LTGM_TransactionNo = txtTransactionNo.Text
            Else
                txtTransactionNo.Text = objTG.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                objTG.LTGM_TransactionNo = txtTransactionNo.Text
            End If
            If ddlVehicleType.SelectedIndex > 0 Then
                objTG.LTGM_VehicleType = ddlVehicleType.SelectedValue
            Else
                objTG.LTGM_VehicleType = 0
            End If

            If ddlTripStatus.SelectedIndex > 0 Then
                objTG.LTGM_TripStatus = ddlTripStatus.SelectedIndex
            Else
                objTG.LTGM_TripStatus = 0
            End If

            If ddlVehicleNo.SelectedIndex > 0 Then
                objTG.LTGM_VehicleNo = ddlVehicleNo.SelectedValue
            Else
                objTG.LTGM_VehicleNo = 0
            End If

            If ddlStartCustomer.SelectedIndex > 0 Then
                objTG.LTGM_StartCustomer = ddlStartCustomer.SelectedValue
            Else
                objTG.LTGM_StartCustomer = 0
            End If

            If ddlDestinCustomer.SelectedIndex > 0 Then
                objTG.LTGM_DestinationCustomer = ddlDestinCustomer.SelectedValue
            Else
                objTG.LTGM_DestinationCustomer = 0
            End If

            If txtSVCNo.Text = "" Then
                objTG.LTGM_SVCNo = "00"
            Else
                objTG.LTGM_SVCNo = txtSVCNo.Text
            End If

            objTG.LTGM_ClientRefNo = txtCRN.Text

            If ddlDriver.SelectedIndex > 0 Then
                objTG.LTGM_Driver = ddlDriver.SelectedValue
            Else
                objTG.LTGM_Driver = 0
            End If


            objTG.LTGM_StartCity = txtStartCity.Text
            objTG.LTGM_DestinationCity = txtDestinCity.Text
            objTG.LTGM_DistanceinKms = TxtDstanceKM.Text
            objTG.LTGM_Rate = txtRate.Text
            objTG.LTGM_PetrolQty = txtPtrlinLtrs.Text
            objTG.LTGM_DriverAmount = txtelgblAmt.Text


            objTG.LTGM_AllottedTime = txtTimeAlltdFrTrip.Text
            objTG.LTGM_TripTakenTime = txtTimeTknTrip.Text
            If ddlTripStatus.SelectedIndex = 2 Then
                If txtTimeAlltdFrTrip.Text <> "" And txtTimeTknTrip.Text <> "" Then
                    If Val(txtTimeAlltdFrTrip.Text) > Val(txtTimeTknTrip.Text) Then
                        objTG.LTGM_TimeStatus = "Early"
                    ElseIf Val(txtTimeAlltdFrTrip.Text) = 0 And Val(txtTimeTknTrip.Text) = 0 Then
                        objTG.LTGM_TimeStatus = "MiscellaneousTrip"
                    ElseIf Val(txtTimeAlltdFrTrip.Text) = Val(txtTimeTknTrip.Text) Then
                        objTG.LTGM_TimeStatus = "OnTime"
                    ElseIf Val(txtTimeAlltdFrTrip.Text) < Val(txtTimeTknTrip.Text) Then
                        objTG.LTGM_TimeStatus = "Delay"
                    End If
                Else
                    objTG.LTGM_TimeStatus = ""
                End If
            Else
                objTG.LTGM_TimeStatus = ""
            End If



            objTG.LTGM_Remarks = txtRemarks.Text
            objTG.LTGM_StartTime = txtStartTime.Text
            objTG.LTGM_StopTime = txtStopTime.Text
            objTG.LTGM_StartDate = Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If txtStopDate.Text = "" Then
                objTG.LTGM_StopDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objTG.LTGM_StopDate = Date.ParseExact(txtStopDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If


            objTG.LTGM_EWayBillNo = txtEwayBillNo.Text

            If txtMRStart.Text = "" Then
                objTG.LTGM_MRStart = 0
            Else
                objTG.LTGM_MRStart = txtMRStart.Text
            End If

            If txtMREnd.Text = "" Then
                objTG.LTGM_MREnd = 0
            Else
                objTG.LTGM_MREnd = txtMREnd.Text
            End If
            'If lblMeterStatus.Text = "" Then
            '    objTG.LTGM_MRStatus = ""
            'Else
            '    objTG.LTGM_MRStatus = lblMeterStatus.Text
            'End If
            If ddlTripStatus.SelectedIndex = 2 Then
                If txtMRStart.Text = "" Then
                    lblError.Text = "Set Meter reading start point"
                    Exit Sub
                Else
                    If txtMREnd.Text = "" Then
                        objTG.LTGM_MRStatus = ""
                    Else
                        dFinalVal = Val(txtMREnd.Text) - Val(txtMRStart.Text)
                        If dFinalVal > Val(TxtDstanceKM.Text) Then
                            objTG.LTGM_MRStatus = "Access"
                        ElseIf dFinalVal = Val(TxtDstanceKM.Text) Then
                            objTG.LTGM_MRStatus = "Same"
                        Else
                            objTG.LTGM_MRStatus = "Lack"
                        End If
                    End If

                End If
            Else
                objTG.LTGM_MRStatus = ""
            End If
            objTG.LTGM_Delflag = "W"
            objTG.LTGM_CompID = sSession.AccessCodeID
            objTG.LTGM_Status = "C"
            objTG.LTGM_Operation = "C"
            objTG.LTGM_IPAddress = sSession.IPAddress
            objTG.LTGM_CreatedBy = sSession.UserID
            objTG.LTGM_CreatedOn = Date.Today
            objTG.LTGM_ApprovedBy = Nothing
            objTG.LTGM_ApprovedOn = Date.Today
            objTG.LTGM_DeletedBy = Nothing
            objTG.LTGM_DeletedOn = Date.Today
            objTG.LTGM_UpdatedBy = sSession.UserID
            objTG.LTGM_UpdatedOn = Date.Today
            objTG.LTGM_YearID = sSession.YearID



            'Dim sPerm As String = ""
            'Dim sArray1 As Array
            'sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer")
            'sPerm = sPerm.Remove(0, 1)
            'sArray1 = sPerm.Split(",")

            'iHead = sArray1(0) '1
            'objCSM.BM_Group = sArray1(1) '29
            'objCSM.BM_SubGroup = sArray1(2) '31
            'objCSM.BM_GL = sArray1(3) '146

            'If txtGLID.Text > 0 Then
            '    objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 1, "Update")
            'Else
            '    objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 1, "Save")
            'End If

            objTG.LTGM_EscAmt = 0

            Arr = objTG.SaveTripGenerationDetails(sSession.AccessCode, objTG)
            txtGLID.Text = 0

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                lblTGValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                imgbtnSave.Visible = False 'btnDelete.Visible = True
                'btnSave.Text = "Save"
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
                lblTGValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                If sCMDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If  'btnDelete.Visible = True
                imgbtnSave.Visible = False
                ' btnSave.Text = "Update"
                imgbtnWaiting.Visible = True
                lblStatus.Text = "Waiting For Approval"
            End If
            BindExistingTGNo()
            ddlExistingTripDetails.SelectedValue = Arr(1)
            ddlExistingTripDetails_SelectedIndexChanged(sender, e)
            'BindMasterDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub

    Private Sub ddlExistingTripDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingTripDetails.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistingTripDetails.SelectedIndex > 0 Then
                BindDetails(ddlExistingTripDetails.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingTripDetails_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            Response.Redirect(String.Format("~/Logistics/FrmLgstTripGenDashboard.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
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
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            lblStatus.Text = "" : lblError.Text = ""
            If ddlExistingTripDetails.SelectedIndex > 0 Then
                If ddlTripStatus.SelectedIndex = 1 Then
                    lblError.Text = "Select Trip Status End... Before Approve Trip Details"
                    ddlTripStatus.Focus()
                    Exit Sub
                End If
                If txtStartCity.Text = "Miscellaneous" And txtDestinCity.Text = "Trip" Then
                    txtTimeTknTrip.Text = 0
                Else
                    If txtTimeTknTrip.Text = "" Then
                        lblError.Text = "Enter Time taken by trip"
                        txtTimeTknTrip.Focus()
                        Exit Sub
                    End If
                End If

                If txtStopDate.Text = "" Then
                    lblError.Text = "Select Stop Date"
                    txtStopDate.Focus()
                    Exit Sub
                End If
                If txtStopTime.Text = "" Then
                    lblError.Text = "Select Stop Time"
                    txtStartTime.Focus()
                    Exit Sub
                End If
                If txtMREnd.Text = "" Then
                    lblError.Text = "Enter Meter Reading end Point"
                    txtStartTime.Focus()
                    Exit Sub
                End If
                If Val(txtMRStart.Text) > Val(txtMREnd.Text) Then
                    lblError.Text = "End Point Meter Reading Must be Greater than Start Point."
                    txtMREnd.Text = "" : txtMREnd.Focus()
                    Exit Sub
                End If
                imgbtnUpdate_Click(sender, e)


                GetLgstInvGrid(ddlDestinCustomer.SelectedValue, ddlExistingTripDetails.SelectedValue)

                SaveSalesJEDetails(ddlDestinCustomer.SelectedValue)




            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Public Sub ClearHopOnDetails()
        Try
            lblError.Text = ""
            txtInTime.Text = "" : txtOutTime.Text = "" : txtHopOnRemark.Text = ""
            txtInDate.Text = "" : txtOutDate.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgHopOn_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgHopOn.ItemCommand
        Dim dtBank As New DataTable
        Try
            If e.CommandName = "Edit" Then
                txtHopOnID.Text = e.Item.Cells(0).Text
                imgbtnAddHopOn.ImageUrl = "~/Images/Update16.png"
                dtBank = objTG.GetHopOnDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                If dtBank.Rows.Count > 0 Then
                    txtInTime.Text = dtBank.Rows(0)("LTGHD_InTime")
                    txtOutTime.Text = dtBank.Rows(0)("LTGHD_OutTime")
                    txtHopOnRemark.Text = dtBank.Rows(0)("LTGHD_HopOnDetails")
                    txtInDate.Text = objGen.FormatDtForRDBMS(dtBank.Rows(0)("LTGHD_InDate").ToString(), "D")
                    txtOutDate.Text = objGen.FormatDtForRDBMS(dtBank.Rows(0)("LTGHD_OutDate").ToString(), "D")
                End If
            ElseIf e.CommandName = "Delete" Then
                objTG.DeleteHopOnValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                dgHopOn.DataSource = objTG.BindHopOnDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                dgHopOn.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgHopOn_ItemCommand")
        End Try
    End Sub
    Private Sub imgbtnAddHopOn_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddHopOn.Click
        Dim Arr() As String
        Dim dDate, dSDate As Date : Dim m As Integer

        Try
            lblError.Text = ""
            If ddlExistingTripDetails.SelectedIndex > 0 Then

                If txtInDate.Text <> "" Then
                    'Cheque Date Comparision'
                    dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtInDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m < 0 Then
                        lblError.Text = "In Date (" & txtInDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                        txtInDate.Focus()
                        Exit Sub
                    End If

                    dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtInDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m > 0 Then
                        lblError.Text = "In Date (" & txtInDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                        txtInDate.Focus()
                        Exit Sub
                    End If
                    'Cheque Date Comparision'
                End If
                If txtOutDate.Text <> "" Then
                    'Cheque Date Comparision'
                    dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtOutDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m < 0 Then
                        lblError.Text = "Out Date (" & txtOutDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                        txtOutDate.Focus()
                        Exit Sub
                    End If

                    dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtOutDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m > 0 Then
                        lblError.Text = "Out Date (" & txtOutDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                        txtOutDate.Focus()
                        Exit Sub
                    End If
                    'Cheque Date Comparision'
                End If

                objTG.LTGHD_ID = txtHopOnID.Text
                objTG.LTGHD_TripID = ddlExistingTripDetails.SelectedValue
                objTG.LTGHD_InTime = txtInTime.Text
                objTG.LTGHD_OutTime = txtOutTime.Text
                objTG.LTGHD_HopOnDetails = txtHopOnRemark.Text

                If txtInDate.Text = "" Then
                    objTG.LTGHD_InDate = Date.ParseExact("1900-01-01", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    objTG.LTGHD_InDate = Date.ParseExact(txtInDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                If txtOutDate.Text = "" Then
                    objTG.LTGHD_OutDate = Date.ParseExact("1900-01-01", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    objTG.LTGHD_OutDate = Date.ParseExact(txtOutDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If


                objTG.LTGHD_DelFlag = "X"
                objTG.LTGHD_Status = "W"
                objTG.LTGHD_CreatedBy = sSession.UserID
                objTG.LTGHD_CreatedOn = Date.Today
                objTG.LTGHD_UpdatedBy = sSession.UserID
                objTG.LTGHD_UpdatedOn = Date.Today
                objTG.LTGHD_CompID = sSession.AccessCodeID
                objTG.LTGHD_YearID = sSession.YearID
                objTG.LTGHD_Operation = "C"
                objTG.LTGHD_IPAddress = sSession.IPAddress
                Arr = objTG.SaveTGHopOnDetails(sSession.AccessCode, objTG)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnAddHopOn.ImageUrl = "~/Images/Save16.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
                dgHopOn.Visible = True
                dgHopOn.DataSource = objTG.BindHopOnDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                dgHopOn.DataBind()
                txtHopOnID.Text = 0
                ClearHopOnDetails()
            Else
                lblError.Text = "Select Existing Diesel/Petrol Pump Registeration No."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBankSave_Click")
        End Try
    End Sub

    Public Sub ClearDieselPumpDetails()
        Try
            lblError.Text = ""
            txtDieselAmount.Text = "" : txtDieselinLtrs.Text = "" : ddlDieselPump.SelectedIndex = 0 : txtDieselDt.Text = ""
            txtRatePrLtr.Text = "" : txtAdvncsPaidDriver.Text = "" : txtOtherExp.Text = "" : txtOtherRemarks.Text = ""
            txtOilinLtr.Text = "" : txtOilRate.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgDiesel_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDiesel.ItemCommand
        Dim dtBank As New DataTable
        Dim imgbtnView As New ImageButton
        Try
            If e.CommandName = "Edit" Then
                txtDieselId.Text = e.Item.Cells(0).Text
                imgbtnAddHopOn.ImageUrl = "~/Images/Update16.png"
                dtBank = objTG.GetDieselPumpDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                If dtBank.Rows.Count > 0 Then
                    ddlDieselPump.SelectedValue = dtBank.Rows(0)("LTGDD_PumpID")
                    txtDieselDt.Text = dtBank.Rows(0)("LTGDD_IndDate")
                    txtDieselinLtrs.Text = dtBank.Rows(0)("LTGDD_DieselinLtrs")
                    txtDieselAmount.Text = dtBank.Rows(0)("LTGDD_DieselAmount")
                    txtRatePrLtr.Text = dtBank.Rows(0)("LTGDD_DieselRatePerltr")
                    txtAdvncsPaidDriver.Text = dtBank.Rows(0)("LTGDD_DriverAdvancGvnByPump")

                    txtOtherRemarks.Text = dtBank.Rows(0)("LTGDD_Remarks")

                    txtOilRate.Text = dtBank.Rows(0)("LTGDD_OilAmountInLtr")
                    If dtBank.Rows(0)("LTGDD_OtherExpenses") = 0 Then
                        txtOtherExp.Text = ""
                    Else
                        txtOtherExp.Text = dtBank.Rows(0)("LTGDD_OtherExpenses")
                    End If
                    If dtBank.Rows(0)("LTGDD_DieselRatePerltr") = 0 Then
                        txtRatePrLtr.Text = ""
                    Else
                        txtRatePrLtr.Text = dtBank.Rows(0)("LTGDD_DieselRatePerltr")
                    End If
                    If dtBank.Rows(0)("LTGDD_OilInltr") = 0 Then
                        txtOilinLtr.Text = ""
                    Else
                        txtOilinLtr.Text = dtBank.Rows(0)("LTGDD_OilInltr")
                    End If
                    objTG.DeleteDieselPumpValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                    dgDiesel.DataSource = objTG.BindDieselPumpDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                    dgDiesel.DataBind()
                End If
            ElseIf e.CommandName = "Report" Then
                imgbtnView.ImageUrl = "~/Images/View16.png"
                dtBank = objTG.GetDieselPumpDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                Print(dtBank.Rows(0)("LTGDD_ID"), e.Item.Cells(1).Text)
            ElseIf e.CommandName = "Delete" Then
                objTG.DeleteDieselPumpValue(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                dgDiesel.DataSource = objTG.BindDieselPumpDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                dgDiesel.DataBind()
                lblError.Text = "Successfully Deleted."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDiesel_ItemCommand")
        End Try
    End Sub
    Private Sub ImgbtnDieselSave_Click(sender As Object, e As ImageClickEventArgs) Handles ImgbtnDieselSave.Click
        Dim Arr() As String
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            lblError.Text = ""
            If ddlExistingTripDetails.SelectedIndex > 0 Then

                If txtDieselDt.Text <> "" Then
                    'Cheque Date Comparision'
                    dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtDieselDt.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m < 0 Then
                        lblError.Text = "Indent Date (" & txtDieselDt.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                        txtDieselDt.Focus()
                        Exit Sub
                    End If

                    dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtDieselDt.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m > 0 Then
                        lblError.Text = "Indent Date (" & txtDieselDt.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                        txtDieselDt.Focus()
                        Exit Sub
                    End If
                    'Cheque Date Comparision'
                End If


                If ddlDieselPump.SelectedIndex < 1 Or txtDieselinLtrs.Text = "" Or txtRatePrLtr.Text = "" Or txtDieselAmount.Text = "" Or txtDieselDt.Text = "" Then
                    lblError.Text = "Enter Pump Details."
                    Exit Sub
                Else
                    If txtStartCity.Text = "Miscellaneous" And txtDestinCity.Text = "Trip" Then
                    Else
                        If txtAdvncsPaidDriver.Text = "" Then
                            lblError.Text = "Enter Pump Details."
                            Exit Sub
                        End If
                    End If
                    objTG.LTGDD_ID = txtDieselId.Text
                    objTG.LTGDD_TripID = ddlExistingTripDetails.SelectedValue
                    objTG.LTGDD_PumpId = ddlDieselPump.SelectedValue

                    objTG.LTGDD_IndDate = Date.ParseExact(txtDieselDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objTG.LTGDD_DieselinLtrs = txtDieselinLtrs.Text
                    objTG.LTGDD_DieselAmount = txtDieselAmount.Text
                    objTG.LTGDD_DieselRatePerltr = txtRatePrLtr.Text
                    If txtAdvncsPaidDriver.Text = "" Then
                        objTG.LTGDD_DriverAdvancGvnByPump = 0
                    Else
                        objTG.LTGDD_DriverAdvancGvnByPump = txtAdvncsPaidDriver.Text
                    End If
                    If Val(txtOtherExp.Text) = 0 Then
                        objTG.LTGDD_OtherExpenses = 0
                    Else
                        objTG.LTGDD_OtherExpenses = txtOtherExp.Text
                    End If
                    If Val(txtOilinLtr.Text) = 0 Then
                        objTG.LTGDD_OilInltr = 0
                    Else
                        objTG.LTGDD_OilInltr = txtOilinLtr.Text
                    End If
                    If Val(txtOilRate.Text) = 0 Then
                        objTG.LTGDD_OilAmountInLtr = 0
                    Else
                        objTG.LTGDD_OilAmountInLtr = txtOilRate.Text
                    End If
                    objTG.LTGDD_Remarks = txtOtherRemarks.Text
                    objTG.LTGDD_DelFlag = "X"
                    objTG.LTGDD_Status = "W"
                    objTG.LTGDD_CreatedBy = sSession.UserID
                    objTG.LTGDD_CreatedOn = Date.Today
                    objTG.LTGDD_UpdatedBy = sSession.UserID
                    objTG.LTGDD_UpdatedOn = Date.Today
                    objTG.LTGDD_CompID = sSession.AccessCodeID
                    objTG.LTGDD_YearID = sSession.YearID
                    objTG.LTGDD_Operation = "C"
                    objTG.LTGDD_IPAddress = sSession.IPAddress
                    Arr = objTG.SaveTGDieselPumpDetails(sSession.AccessCode, objTG)
                    If Arr(0) = "2" Then
                        lblError.Text = "Successfully Updated"
                        ImgbtnDieselSave.ImageUrl = "~/Images/Save16.png"
                    ElseIf Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved"
                    End If
                    dgDiesel.Visible = True
                    dgDiesel.DataSource = objTG.BindDieselPumpDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.YearID)
                    dgDiesel.DataBind()
                    txtDieselId.Text = 0
                    ClearDieselPumpDetails()
                End If
            Else
                lblError.Text = "Select Existing Diesel/Petrol Pump Registeration No."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgbtnDieselSave_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            'lblError.Text = ""
            'txtGLID.Text = 0 : txtRate.Text = "" : txtPtrlinLtrs.Text = "" : txtelgblAmt.Text = "" : TxtDstanceKM.Text = "" : txtStartCity.Text = "" : txtDestinCity.Text = ""
            'If sCMDSave = "YES" Then
            '    imgbtnSave.Visible = True
            'End If
            'imgbtnUpdate.Visible = False
            ''btnSave.Text = "Save"
            '' btnDelete.Text = "Delete"
            'txtTransactionNo.Text = objTG.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'txtStartTime.Text = "" : txtStopTime.Text = "" : txtStartDate.Text = "" : txtStopDate.Text = ""

            'ddlExistingTripDetails.SelectedIndex = 0 : lblStatus.Text = ""
            'ddlStartCustomer.SelectedIndex = 1 : ddlDestinCustomer.SelectedIndex = 0
            'ddlRoute.SelectedIndex = 0 : ddlVehicleNo.SelectedIndex = 0 : ddlTripStatus.SelectedIndex = 1

            'txtInTime.Text = "" : txtOutTime.Text = "" : txtHopOnRemark.Text = "" : txtRemarks.Text = "" : txtAdvncsPaidDriver.Text = ""
            'txtEwayBillNo.Text = ""
            'txtDieselAmount.Text = "" : txtDieselinLtrs.Text = "" : ddlDieselPump.SelectedIndex = 0

            'txtRatePrLtr.Text = "" : txtAdvncsPaidDriver.Text = ""
            'txtInDate.Text = "" : txtOutDate.Text = ""
            'txtTimeAlltdFrTrip.Text = "" : txtTimeTknTrip.Text = ""
            'lbltripTimeStatus.Text = ""
            'txtSVCNo.Text = "" : txtCRN.Text = ""

            'BindExistingDriver() : BindExistingVehicleNo() : BindRoute()
            'ddlDriver.SelectedIndex = 0 : ddlVehicleNo.SelectedIndex = 0
            'lblID.Text = "0"
            'dgDiesel.Enabled = True : dgHopOn.Enabled = True
            'imgbtnAddHopOn.Enabled = True : ImgbtnDieselSave.Enabled = True
            'txtMREnd.Text = "" : txtMRStart.Text = "" : txtOtherExp.Text = "" : txtOtherRemarks.Text = ""
            'dgHopOn.DataSource = Nothing
            'dgHopOn.DataBind()

            'dgDiesel.DataSource = Nothing
            'dgDiesel.DataBind()
            ''txtSupplierCode.Text = objCSM.GeneratePartyCode(sSession.AccessCode, sSession.AccessCodeID, "")

            'dgINVDetails.DataSource = Nothing
            'dgINVDetails.DataBind()
            'dgINVDetails.Visible = False
            'dgHopOn.Visible = False : dgDiesel.Visible = False : dgINVDetails.Visible = False

            'lblMeterStatus.Text = "" : lbltripTimeStatus.Text = ""
            'ddlVehicleType.SelectedIndex = 0

            lblError.Text = ""
            '  oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ' Response.Redirect(String.Format("~/Logistics/FrmLgstTripGeneration.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
            Response.Redirect("~/Logistics/FrmLgstTripGeneration.aspx", False)
            sSession.Statusid = 0
            sSession.pkoid = 0
            Session("AllSession") = sSession
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub ddlRoute_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoute.SelectedIndexChanged
        Dim dtRouteDetails As New DataTable
        Try
            lblError.Text = ""

            dtRouteDetails = objTG.GetRouteMasterDetails(sSession.AccessCode, ddlRoute.SelectedValue)
            If dtRouteDetails.Rows.Count > 0 Then

                BindDieselPump()
                bindVehicleType()

                If IsDBNull(dtRouteDetails.Rows(0)("LRM_VehicleType")) = False Then
                    ddlVehicleType.SelectedValue = dtRouteDetails.Rows(0)("LRM_VehicleType")
                Else
                    ddlVehicleType.SelectedIndex = 0
                End If

                If IsDBNull(dtRouteDetails.Rows(0)("LRM_Rate")) = False Then
                    txtRate.Text = dtRouteDetails.Rows(0)("LRM_Rate")
                Else
                    txtRate.Text = ""
                End If

                If IsDBNull(dtRouteDetails.Rows(0)("LRM_PetrolQty")) = False Then
                    txtPtrlinLtrs.Text = dtRouteDetails.Rows(0)("LRM_PetrolQty")
                Else
                    txtPtrlinLtrs.Text = ""
                End If

                If IsDBNull(dtRouteDetails.Rows(0)("LRM_DistinKms")) = False Then
                    TxtDstanceKM.Text = dtRouteDetails.Rows(0)("LRM_DistinKms")
                Else
                    TxtDstanceKM.Text = ""
                End If
                If IsDBNull(dtRouteDetails.Rows(0)("LRM_DriverAlnceAmt")) = False Then
                    txtelgblAmt.Text = dtRouteDetails.Rows(0)("LRM_DriverAlnceAmt")
                    If Val(txtelgblAmt.Text) = 0 Then
                        txtAdvncsPaidDriver.Text = 0 ': txtAdvncsPaidDriver.Enabled = False
                    End If
                Else
                    txtelgblAmt.Text = ""
                End If
                If IsDBNull(dtRouteDetails.Rows(0)("LRM_StartPlace")) = False Then
                    txtStartCity.Text = objTG.LoadCity(sSession.AccessCode, sSession.AccessCodeID, dtRouteDetails.Rows(0)("LRM_StartPlace"))
                Else
                    txtStartCity.Text = ""
                End If
                If IsDBNull(dtRouteDetails.Rows(0)("LRM_DestPlace")) = False Then
                    txtDestinCity.Text = objTG.LoadCity(sSession.AccessCode, sSession.AccessCodeID, dtRouteDetails.Rows(0)("LRM_DestPlace"))
                Else
                    txtDestinCity.Text = ""
                End If

                If IsDBNull(dtRouteDetails.Rows(0)("LRM_AllottedTime")) = False Then
                    txtTimeAlltdFrTrip.Text = dtRouteDetails.Rows(0)("LRM_AllottedTime")
                    If txtTimeAlltdFrTrip.Text = 0 Then
                        txtTimeTknTrip.Text = 0
                    End If
                Else
                    txtTimeAlltdFrTrip.Text = ""
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRoute_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub txtRatePrLtr_TextChanged(sender As Object, e As EventArgs)
        Try
            lblError.Text = ""
            If ddlDieselPump.SelectedIndex > 0 Then
                If txtDieselinLtrs.Text = "" Or txtDieselinLtrs.Text > 0 Or txtRatePrLtr.Text = "" Or txtRatePrLtr.Text > 0 Then
                    txtDieselAmount.Text = txtDieselinLtrs.Text * txtRatePrLtr.Text
                    txtDieselAmount.Focus()
                    If txtOilRate.Text <> "" Then
                        txtDieselAmount.Text = Val(txtDieselAmount.Text) + (Val(txtOilinLtr.Text) * Val(txtOilRate.Text))
                    End If
                Else
                    lblError.Text = "Enter Valid amount."
                    txtRatePrLtr.Focus()
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Existing Diesel/Petrol Pump Registeration No."
                txtRatePrLtr.Focus()
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Protected Sub txtTimeTknTrip_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        lblError.Text = ""
    '        If txtTimeAlltdFrTrip.Text <> "" Or txtTimeTknTrip.Text <> "" Then
    '            If txtTimeAlltdFrTrip.Text < txtTimeTknTrip.Text Then
    '                lbltripTimeStatus.Text = "Early"
    '            ElseIf txtTimeAlltdFrTrip.Text = txtTimeTknTrip.Text Then
    '                lbltripTimeStatus.Text = "OnTime"
    '            ElseIf txtTimeAlltdFrTrip.Text > txtTimeTknTrip.Text Then
    '                lbltripTimeStatus.Text = "Delay"
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub txtAdvncsPaidDriver_TextChanged(sender As Object, e As EventArgs)
        Dim dDriverAllowance As Double
        Dim dDriverAdvance As Double
        Dim dTotalAmount As Double
        Dim dDrivertoalAmt As Double
        Try
            lblError.Text = ""
            dDriverAdvance = txtAdvncsPaidDriver.Text
            If dgDiesel.Items.Count > 0 Then
                dTotalAmount = objTG.GettotalDriverAdvance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingTripDetails.SelectedValue)
                dDriverAllowance = txtelgblAmt.Text
                dDrivertoalAmt = dDriverAllowance - dTotalAmount
                If dDriverAdvance > dDrivertoalAmt Then
                    lblError.Text = "Total Amount for this route is Exceed"
                    txtAdvncsPaidDriver.Text = ""
                    txtAdvncsPaidDriver.Focus()
                    Exit Sub
                End If
                ImgbtnDieselSave.Focus()
            Else
                dDriverAllowance = txtelgblAmt.Text
                If dDriverAdvance > dDriverAllowance Then
                    lblError.Text = "Advance Amount is Greater than Allotted amount of driver"
                    txtAdvncsPaidDriver.Text = ""
                    txtAdvncsPaidDriver.Focus()
                    Exit Sub
                End If
                ImgbtnDieselSave.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtDieselinLtrs_TextChanged(sender As Object, e As EventArgs)
        Dim dTotalPetrol As Double
        Dim dNoofLiters As Double
        Dim dtotalDiesel As Double
        Dim dTotalNoOfPetrolInltrs As Double
        Try
            lblError.Text = ""
            dTotalPetrol = txtPtrlinLtrs.Text 'objTG.GettotalPetrolinLtrs(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingTripDetails.SelectedValue)
            dNoofLiters = txtDieselinLtrs.Text
            If dgDiesel.Items.Count > 0 Then
                dTotalNoOfPetrolInltrs = objTG.GettotalNoOfPetrolInltrs(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingTripDetails.SelectedValue)

                dTotalPetrol = txtPtrlinLtrs.Text
                dtotalDiesel = dTotalPetrol - dTotalNoOfPetrolInltrs

                If dNoofLiters > dtotalDiesel Then
                    lblError.Text = "No. of Diesel liters is greater than total diesel alloted for this route"
                    txtDieselinLtrs.Text = ""
                    txtDieselinLtrs.Focus()
                    Exit Sub
                End If
                txtRatePrLtr.Focus()
            Else
                dTotalPetrol = txtPtrlinLtrs.Text
                dNoofLiters = txtDieselinLtrs.Text

                If dNoofLiters > dTotalPetrol Then
                    lblError.Text = "No. of Diesel liters is greater than total diesel alloted for this route!!!!"
                    txtDieselinLtrs.Text = ""
                    txtDieselinLtrs.Focus()
                    Exit Sub
                End If
                If txtRatePrLtr.Text <> "" Then
                    txtDieselAmount.Text = Val(txtDieselinLtrs.Text) * Val(txtRatePrLtr.Text)
                Else
                    txtRatePrLtr.Focus()
                End If
                If txtOilRate.Text <> "" Then
                    txtDieselAmount.Text = Val(txtDieselAmount.Text) + (Val(txtOilinLtr.Text) * Val(txtOilRate.Text))
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlDestinCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDestinCustomer.SelectedIndexChanged
        Dim dtCustomer As New DataTable
        Dim dtCompany As New DataTable
        Dim iCustCompanyType As Integer
        Dim sCustCompanyType As String
        Try
            If ddlDestinCustomer.SelectedIndex > 0 Then
                txtCustGstnCategoryId.Text = 0
                dtCustomer = objTG.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDestinCustomer.SelectedValue)
                If dtCustomer.Rows.Count > 0 Then
                    If IsDBNull(dtCustomer.Rows(0)("BM_GSTNRegNo")) = False Then
                        txtCustGSTN.Text = dtCustomer.Rows(0)("BM_GSTNRegNo")
                    Else
                        txtCustGSTN.Text = ""
                    End If

                    If IsDBNull(dtCustomer.Rows(0)("BM_Address1")) = False Then
                        txtCustomerAddress.Text = dtCustomer.Rows(0)("BM_Address1")
                    Else
                        txtCustomerAddress.Text = ""
                    End If

                    iCustCompanyType = dtCustomer.Rows(0)("BM_GSTNCategory")
                    txtCustGstnCategoryId.Text = iCustCompanyType
                    Dim description As String
                    description = objTG.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, iCustCompanyType)
                    If UCase(description) = UCase("RCM DEALER") Then
                        txtCustGSTRate.Text = 0
                        txtCustGstnCategory.Text = "RCM DEALER"
                    End If
                    If UCase(description) = UCase("NORMAL GST DEALER") Then
                        txtCustGSTRate.Text = 12
                        txtCustGstnCategory.Text = "NORMAL GST DEALER"
                    End If
                    If UCase(description) = UCase("REDUCED GST DEALER") Then
                        txtCustGSTRate.Text = 5
                        txtCustGstnCategory.Text = "REDUCED GST DEALER"
                    End If

                    'ddlDBOtherHead.SelectedIndex = objTG.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dtCustomer.Rows(0)("BM_GL").ToString())
                    'ddldbOtherHead_SelectedIndexChanged(sender, e)

                    'ddlDbOtherGL.SelectedValue = dtCustomer.Rows(0)("BM_GL").ToString()
                    'ddlDBOtherGL_SelectedIndexChanged(sender, e)

                    'If dtCustomer.Rows(0)("BM_SubGL").ToString() = "0" Then
                    '    ddlDbOtherSubGL.SelectedIndex = -1
                    'Else
                    '    ddlDbOtherSubGL.SelectedValue = dtCustomer.Rows(0)("BM_SubGL").ToString()
                    '    'ddlDBOtherSubGL_SelectedIndexChanged(sender, e)
                    'End If

                End If

                dtCompany = objTG.GetCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
                If dtCompany.Rows.Count > 0 Then
                    If IsDBNull(dtCompany.Rows(0)("Cust_FinalNo")) = False Then
                        txtCompanyGSTN.Text = dtCompany.Rows(0)("Cust_FinalNo")
                    Else
                        txtCompanyGSTN.Text = ""
                    End If

                    If IsDBNull(dtCompany.Rows(0)("Cust_comm_address")) = False Then
                        txtCompanyAddress.Text = dtCompany.Rows(0)("Cust_comm_address")
                    Else
                        txtCompanyAddress.Text = ""
                    End If
                End If
            Else
                'lblScode.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDestinCustomer_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub GetLgstInvGrid(ByVal iCustID As Integer, ByVal iMasterID As Integer)
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
        Dim sArrayRoute As Array
        Dim iParty As Integer = 0

        Dim dt1 As New DataTable
        Dim dPartyTotal As Double
        Dim dDieselTotal As Double
        Dim dtGSTRates As New DataTable : Dim sSql As String = ""
        Dim dTotalAmt, dSGSTAmt, dCGSTAmt, dIGSTAmt As Double
        Dim SGST, CGST, IGST As Double
        Dim sTypeOfBill As String = "" : Dim sState As String = ""
        Dim iGSTrate As Integer = 0
        Dim iRouteId As Integer = 0
        Dim sRoute As String = ""
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("PaymentID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Type")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")

            'iParty = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            iParty = iCustID


            sTypeOfBill = objDBL.SQLGetDescription(sSession.AccessCode, "Select LTGM_GSTCustBillStatus From Lgst_TripGeneration_Master Where LTGM_ID=" & iMasterID & " And LTGM_CompID=" & sSession.AccessCodeID & " And LTGM_YearID=" & sSession.YearID & " ")
            sState = objDBL.SQLGetDescription(sSession.AccessCode, "Select LTGM_State From Lgst_TripGeneration_Master Where LTGM_ID=" & iMasterID & " And LTGM_CompID=" & sSession.AccessCodeID & " And LTGM_YearID=" & sSession.YearID & " ")
            iGSTrate = objDBL.SQLGetDescription(sSession.AccessCode, "Select LTGM_GSTRate From Lgst_TripGeneration_Master Where LTGM_ID=" & iMasterID & " And LTGM_CompID=" & sSession.AccessCodeID & " And LTGM_YearID=" & sSession.YearID & " ")
            iRouteId = objDBL.SQLGetDescription(sSession.AccessCode, "Select LTGM_RouteID From Lgst_TripGeneration_Master Where LTGM_ID=" & iMasterID & " And LTGM_CompID=" & sSession.AccessCodeID & " And LTGM_YearID=" & sSession.YearID & " ")

            If iRouteId > 0 Then
                sRoute = objDBL.SQLGetDescription(sSession.AccessCode, "Select LRM_StartDestPlace From lgst_route_master Where LRM_ID=" & iRouteId & " And LRM_CompID=" & sSession.AccessCodeID & "  and LRM_Delflag='A' ")
                'If sRoute > "" Then
                '    sArrayRoute = sRoute.Split("-")
                '    'dRow("GLCode") = sArray(0)
                '    sRoute = sArrayRoute(0) & "-" & sArrayRoute(1)
                'End If

            End If

            'dtGSTRates.Rows.Add(iGSTrate)
            ''Extra'
            'dtGSTRates.Rows.Add("0")
            'Extra'

            'If dtGSTRates.Rows.Count > 0 Then
            'For k = 0 To dtGSTRates.Rows.Count - 1

            dt1 = objDBL.SQLExecuteDataSet(sSession.AccessCode, "Select * From Lgst_TripGeneration_Master Where  LTGM_YearID=" & sSession.YearID & " and LTGM_GSTRate=" & iGSTrate & " And LTGM_ID=" & iMasterID & " And LTGM_CompID=" & sSession.AccessCodeID & " ").Tables(0)
            If dt1.Rows.Count > 0 Then
                For z = 0 To dt1.Rows.Count - 1
                    dTotalAmt = dTotalAmt + dt1.Rows(z)("LTGM_Rate")
                    dSGSTAmt = dSGSTAmt + dt1.Rows(z)("LTGM_SGSTAmount")
                    dCGSTAmt = dCGSTAmt + dt1.Rows(z)("LTGM_CGSTAmount")
                    dIGSTAmt = dIGSTAmt + dt1.Rows(z)("LTGM_IGSTAmount")
                    Dim dtotalAmount = (dTotalAmt + dSGSTAmt + dCGSTAmt + dIGSTAmt)
                    dPartyTotal = dPartyTotal + Convert.ToDecimal(dtotalAmount)
                Next

                dRow = dt.NewRow 'Item Name
                dRow("Id") = 0
                dRow("HeadID") = objTG.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") '"Sale Of Product " & sState
                dRow("GLID") = objTG.GetCOAGLID(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") '"Sale Of Product " & sState
                If sTypeOfBill = "Local" Then
                    dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, sRoute) '"Local GST " & iGSTrate & " % " & sState & " Sale Account"
                ElseIf sTypeOfBill = "Inter State" Then
                    dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, sRoute) '"Inter State GST " & iGSTrate & " % " & sState & " Sale Account"
                End If
                dRow("PaymentID") = 5
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Frieght Earnings" '"Sale Of Material"

                sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objTG.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    If sArray.Length = 4 Then
                        dRow("SubGLDescription") = sArray(1) & "-" & sArray(2) & "-" & sArray(3)
                    Else
                        dRow("SubGLDescription") = sArray(1) & "-" & sArray(2)
                    End If
                End If

                dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(dTotalAmt).ToString("#,##0.00") 'dTotalAmt
                dt.Rows.Add(dRow)

                dRow = dt.NewRow 'Escallated amount
                dRow("Id") = 0
                dRow("HeadID") = objTG.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") '"Sale Of Product " & sState
                dRow("GLID") = objTG.GetCOAGLID(sSession.AccessCode, sSession.AccessCodeID, "Frieght Earnings") '"Sale Of Product " & sState
                If sTypeOfBill = "Local" Then
                    dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, sRoute) '"Local GST " & iGSTrate & " % " & sState & " Sale Account"
                ElseIf sTypeOfBill = "Inter State" Then
                    dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, sRoute) '"Inter State GST " & iGSTrate & " % " & sState & " Sale Account"
                End If
                dRow("PaymentID") = 20
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Frieght Earnings" '"Sale Of Material"

                sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objTG.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    If sArray.Length = 4 Then
                        dRow("SubGLDescription") = sArray(1) & "-" & sArray(2) & "-" & sArray(3)
                    Else
                        dRow("SubGLDescription") = sArray(1) & "-" & sArray(2)
                    End If
                End If

                dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(0).ToString("#,##0.00") 'dTotalAmt
                dt.Rows.Add(dRow)



                SGST = iGSTrate / 2
                CGST = iGSTrate / 2
                IGST = iGSTrate

                dRow = dt.NewRow 'SGST
                dRow("Id") = 0
                dRow("HeadID") = objTG.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                dRow("GLID") = objTG.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output SGST " & SGST & " % " & sState & " Sale Account")

                dRow("PaymentID") = 6
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "SGST"

                sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objTG.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    dRow("SubGLDescription") = sArray(1)
                End If

                dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(dSGSTAmt).ToString("#,##0.00") 'dSGSTAmt
                dt.Rows.Add(dRow)

                dRow = dt.NewRow 'CGST
                dRow("Id") = 0
                dRow("HeadID") = objTG.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                dRow("GLID") = objTG.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output CGST " & CGST & " % " & sState & " Sale Account")
                dRow("PaymentID") = 7
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "CGST"

                sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objTG.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    dRow("SubGLDescription") = sArray(1)
                End If

                dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(dCGSTAmt).ToString("#,##0.00") 'dCGSTAmt
                dt.Rows.Add(dRow)

                dRow = dt.NewRow 'IGST
                dRow("Id") = 0
                dRow("HeadID") = objTG.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                dRow("GLID") = objTG.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output IGST " & IGST & " % " & sState & " Sale Account")
                dRow("PaymentID") = 8
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "IGST"

                sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objTG.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    dRow("SubGLDescription") = sArray(1)
                End If

                dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
                dRow("Credit") = Convert.ToDecimal(dIGSTAmt).ToString("#,##0.00") ' dIGSTAmt
                dt.Rows.Add(dRow)

                dTotalAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0 : dIGSTAmt = 0
            End If


            dRow = dt.NewRow 'Party/Customer
            dRow("Id") = 0
            dRow("HeadID") = objTG.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_Head")
            dRow("GLID") = objTG.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_GL")
            dRow("SubGLID") = objTG.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "C")
            dRow("PaymentID") = 9
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Party/Customer"

            sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objTG.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                If sArray.Length = 3 Then
                    dRow("SubGLDescription") = sArray(1) & "-" & sArray(2)
                Else
                    dRow("SubGLDescription") = sArray(1)
                End If
            End If
            dRow("Debit") = Convert.ToDecimal(dPartyTotal).ToString("#,##0.00") 'dPartyTotal
            dRow("Credit") = Convert.ToDecimal(0).ToString("#,##0.00") '0

            'txtBillAmount.Text = dPartyTotal

            dt.Rows.Add(dRow)


            If dgDiesel.Items.Count > 0 Then

                For i = 0 To dgDiesel.Items.Count - 1
                    dDieselTotal = 0
                    iParty = dgDiesel.Items(i).Cells(12).Text
                    'If (IsDBNull(dgDiesel.Items(0).Cells(0).Text) = False) And (dgDiesel.Items(0).Cells(0).Text <> "&nbsp;") Then
                    '    iParty = dPartyTotal + dgDiesel.Items(0).Cells(0).Text
                    'Else
                    '    iParty = 0
                    'End If
                    Dim dOtheramnt As Double = 0.0
                    If (IsDBNull(dgDiesel.Items(i).Cells(4).Text) = False) And (dgDiesel.Items(i).Cells(4).Text <> "&nbsp;") Then
                        If (IsDBNull(dgDiesel.Items(i).Cells(4).Text) = False) And (dgDiesel.Items(i).Cells(4).Text <> "&nbsp;") Then
                            dOtheramnt = Val(dgDiesel.Items(i).Cells(8).Text)
                        End If
                        dDieselTotal = dDieselTotal + Val(dgDiesel.Items(i).Cells(6).Text) + Val(dgDiesel.Items(i).Cells(7).Text + dOtheramnt)
                    Else
                        dDieselTotal = 0
                    End If



                    dRow = dt.NewRow 'Diesel PumpName
                    dRow("Id") = 0
                    dRow("HeadID") = objTG.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
                    dRow("GLID") = objTG.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
                    dRow("SubGLID") = objTG.GetDieselSubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                    dRow("PaymentID") = 16
                    dRow("SrNo") = dt.Rows.Count + 1
                    dRow("Type") = "Pump Name"

                    sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                    If sGL <> "" Then
                        sArray = sGL.Split("-")
                        dRow("GLCode") = sArray(0)
                        dRow("GLDescription") = sArray(1)
                    End If

                    sSubGL = objTG.GetDieselSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                    If sSubGL <> "" Then
                        sArray = sSubGL.Split("-")
                        dRow("SubGL") = sArray(0)
                        dRow("SubGLDescription") = sArray(1)
                    End If



                    dRow("Debit") = Convert.ToDecimal(0).ToString("#,##0.00") ' 0
                    dRow("Credit") = Convert.ToDecimal(dDieselTotal).ToString("#,##0.00") ' dPartyTotal



                    dt.Rows.Add(dRow)
                Next
                '     txtBillAmount.Text = dPartyTotal + dDieselTotal
            End If

            ' Pump Bill
            dRow = dt.NewRow 'Diesel
            dRow("Id") = 0
            dRow("HeadID") = objTG.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Fuel Expense")
            dRow("GLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Purchase of Diesel")
            dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Diesel Expenses")
            dRow("PaymentID") = 17
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Purchase of Diesel"

            sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objTG.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            Dim dPurchaseDieselTotal As Double = "0.00"
            If dgDiesel.Items.Count > 0 Then

                For i = 0 To dgDiesel.Items.Count - 1
                    If (IsDBNull(dgDiesel.Items(i).Cells(6).Text) = False) And (dgDiesel.Items(i).Cells(6).Text <> "&nbsp;") Then
                        dPurchaseDieselTotal = dPurchaseDieselTotal + (Val(dgDiesel.Items(i).Cells(6).Text))
                    Else
                        dPurchaseDieselTotal = 0
                    End If
                Next
            End If

            dRow("Debit") = Convert.ToDecimal(dPurchaseDieselTotal).ToString("#,##0.00") 'dPartyTotal
            dRow("Credit") = Convert.ToDecimal(0).ToString("#,##0.00") '0
            dt.Rows.Add(dRow)

            ' Driver Bill
            Dim dDriverAmt As Double = 0.0, dDriveTotAmt As Double = 0.0
            iParty = ddlDriver.SelectedValue
            If dgDiesel.Items.Count > 0 Then
                For i = 0 To dgDiesel.Items.Count - 1
                    If (IsDBNull(dgDiesel.Items(i).Cells(7).Text) = False) And (dgDiesel.Items(i).Cells(7).Text <> "&nbsp;") Then
                        dDriverAmt = dDriverAmt + Val(dgDiesel.Items(i).Cells(7).Text)
                    Else
                        dDriverAmt = 0
                    End If
                Next
            End If
            dRow = dt.NewRow 'Driver
            dRow("Id") = 0
            dRow("HeadID") = objTG.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Short time Loan & Advance")
            dRow("GLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Loan & Advance")
            dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Driver Advance")
            dRow("PaymentID") = 18
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Driver Advance"

            sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objTG.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If

            dRow("Debit") = Convert.ToDecimal(dDriverAmt).ToString("#,##0.00") '0
            dRow("Credit") = Convert.ToDecimal(0).ToString("#,##0.00") 'dPartyTotal
            dt.Rows.Add(dRow)

            dRow = dt.NewRow  'Other Expenses
            Dim dVehivlelAmt As Double = 0.0 ', dDriverDieselAmt As Double = 0.0
            iParty = ddlDriver.SelectedValue
            If dgDiesel.Items.Count > 0 Then
                For i = 0 To dgDiesel.Items.Count - 1
                    If (IsDBNull(dgDiesel.Items(i).Cells(8).Text) = False) And (dgDiesel.Items(i).Cells(8).Text <> "&nbsp;") Then
                        dVehivlelAmt = dVehivlelAmt + Val(dgDiesel.Items(i).Cells(8).Text)
                    End If
                Next
            End If
            dRow = dt.NewRow 'vehicle Expenses
            dRow("Id") = 0
            dRow("HeadID") = objTG.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Short time Loan & Advance")
            dRow("GLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Loan & Advance")
            dRow("SubGLID") = objTG.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Other Advance")
            dRow("PaymentID") = 19
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Other advance"

            sGL = objTG.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objTG.GetOtherSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "Vehicle Expenses")
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If



            dRow("Debit") = Convert.ToDecimal(dVehivlelAmt).ToString("#,##0.00") ' dDriverDieselAmt
            dRow("Credit") = Convert.ToDecimal(0).ToString("#,##0.00") ' 0



            dt.Rows.Add(dRow)

            txtBillAmount.Text = dPartyTotal + dPurchaseDieselTotal + dDriverAmt + dVehivlelAmt


            dgINVDetails.DataSource = dt
            dgINVDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetDefaultGridPurchase")
        End Try
    End Sub
    Public Sub SaveSalesJEDetails(ByVal iMasterID As Integer)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Try
            iRet = CheckDebitAndCredit()

            If iRet = 1 Then
                lblTGValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 2 Then
                lblTGValidationMsg.Text = "Amount Not Matched with Advance Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 3 Then
                lblTGValidationMsg.Text = "Amount Not Matched with Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 4 Then
                lblTGValidationMsg.Text = "Total Debit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 5 Then
                lblTGValidationMsg.Text = "Total Credit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            ' objTG.UpdateTGStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, sSession.UserID, sSession.IPAddress, sSession.YearID)

            For i = 0 To dgINVDetails.Items.Count - 1
                objTG.iATD_TrType = 15


                If (IsDBNull(dgINVDetails.Items(i).Cells(0).Text) = False) And (dgINVDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objTG.iATD_ID = dgINVDetails.Items(i).Cells(0).Text
                Else
                    objTG.iATD_ID = 0
                End If

                objTG.dATD_TransactionDate = Date.ParseExact(txtStopDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) ' DateTime.Today

                objTG.iATD_BillId = ddlExistingTripDetails.SelectedValue
                objTG.iATD_PaymentType = dgINVDetails.Items(i).Cells(4).Text

                If (IsDBNull(dgINVDetails.Items(i).Cells(1).Text) = False) And (dgINVDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objTG.iATD_Head = dgINVDetails.Items(i).Cells(1).Text
                Else
                    objTG.iATD_Head = 0
                End If


                If (IsDBNull(dgINVDetails.Items(i).Cells(2).Text) = False) And (dgINVDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objTG.iATD_GL = dgINVDetails.Items(i).Cells(2).Text
                Else
                    objTG.iATD_GL = 0
                End If

                If (IsDBNull(dgINVDetails.Items(i).Cells(3).Text) = False) And (dgINVDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objTG.iATD_SubGL = dgINVDetails.Items(i).Cells(3).Text
                Else
                    objTG.iATD_SubGL = 0
                End If

                If (IsDBNull(dgINVDetails.Items(i).Cells(12).Text) = False) And (dgINVDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objTG.dATD_Debit = Convert.ToDouble(dgINVDetails.Items(i).Cells(12).Text)
                Else
                    objTG.dATD_Debit = 0
                End If

                If (IsDBNull(dgINVDetails.Items(i).Cells(13).Text) = False) And (dgINVDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objTG.dATD_Credit = Convert.ToDouble(dgINVDetails.Items(i).Cells(13).Text)
                Else
                    objTG.dATD_Credit = 0
                End If

                If objTG.dATD_Debit > 0 And objTG.dATD_Credit = 0 Then
                    objTG.iATD_DbOrCr = 1 'Debit
                ElseIf objTG.dATD_Debit = 0 And objTG.dATD_Credit > 0 Then
                    objTG.iATD_DbOrCr = 2 'Credit
                End If

                objTG.iATD_CreatedBy = sSession.UserID
                objTG.dATD_CreatedOn = DateTime.Today

                objTG.sATD_Status = "A"
                objTG.iATD_YearID = sSession.YearID
                objTG.sATD_Operation = "C"
                objTG.sATD_IPAddress = sSession.IPAddress

                objTG.iATD_UpdatedBy = sSession.UserID
                objTG.dATD_UpdatedOn = DateTime.Today

                objTG.iATD_CompID = sSession.AccessCodeID

                objTG.iATD_ZoneID = 2
                objTG.iATD_RegionID = 3
                objTG.iATD_AreaID = 4
                objTG.iATD_BranchID = 5

                objTG.dATD_OpenDebit = "0.00"
                objTG.dATD_OpenCredit = "0.00"
                objTG.dATD_ClosingDebit = "0.00"
                objTG.dATD_ClosingCredit = "0.00"
                objTG.iATD_SeqReferenceNum = 0

                objTG.SaveUpdateTransactionDetails(sSession.AccessCode, objTG)

            Next

            objTG.UpdateTripMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTripDetails.SelectedValue, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)

            lblStatus.Text = "Approved"
            lblError.Text = "Successfully Approved"
            lblTGValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False
            dgDiesel.Enabled = False : dgHopOn.Enabled = False
            imgbtnAddHopOn.Enabled = False : ImgbtnDieselSave.Enabled = False

            Dim dtTransdet As New DataTable
            dtTransdet = objTG.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingTripDetails.SelectedValue)
            If dtTransdet.Rows.Count > 0 Then
                dgINVDetails.Visible = True
                dgINVDetails.DataSource = dtTransdet
                dgINVDetails.DataBind()
            Else
                dgINVDetails.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveSalesJEDetails")
        End Try
    End Sub
    Private Function CheckDebitAndCredit() As Integer
        Dim i As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Try
            For i = 0 To dgINVDetails.Items.Count - 1
                If (IsDBNull(dgINVDetails.Items(i).Cells(12).Text) = False) And (dgINVDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    dDebit = dDebit + Convert.ToDouble(dgINVDetails.Items(i).Cells(12).Text)
                End If

                If (IsDBNull(dgINVDetails.Items(i).Cells(13).Text) = False) And (dgINVDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    dCredit = dCredit + Convert.ToDouble(dgINVDetails.Items(i).Cells(13).Text)
                End If
            Next

            If String.Format("{0:0.00}", Convert.ToDecimal(dDebit)) <> String.Format("{0:0.00}", Convert.ToDecimal(dCredit)) Then
                Return 1  ' Debit and Credit amount not Matched
            End If

            If dDebit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(dDebit)) <> String.Format("{0:0.00}", Convert.ToDecimal(txtBillAmount.Text)) Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            Else
                If dDebit <> txtBillAmount.Text Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            End If

            If dCredit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(dCredit)) <> String.Format("{0:0.00}", Convert.ToDecimal(txtBillAmount.Text)) Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            Else
                If dCredit <> txtBillAmount.Text Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckDebitAndCredit")
        End Try
    End Function
    Private Sub Print(ByVal id As Integer, ByVal iIndNo As Integer)
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim Amount As Double = 0.0
        Dim CashPaid As Double = 0.0 : Dim OtherExp As Double = 0.0
        Dim TAmount As Double = 0.0
        Dim OilLtr As Double = 0.0, oilAmt As Double = 0.0, oilTotal As Double = 0.0
        Dim diselLtr As Double = 0.0, diselAMt As Double = 0.0, dieselAmt As Double = 0.0
        Try
            ReportViewer1.Reset()
            dt1 = objTG.GetAccessCode(sSession.AccessCode)
            dt = objTG.LoadDieselPumpDetails(sSession.AccessCode, sSession.AccessCodeID, id, ddlExistingTripDetails.SelectedValue, sSession.YearID)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/LgstTripGeneration.rdlc")

            Dim sCompName As ReportParameter() = {New ReportParameter("sCompName", dt1.Rows(0).Item("Sad_CMS_Name").ToString())}
            ReportViewer1.LocalReport.SetParameters(sCompName)

            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlDestinCustomer.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)

            Dim TripNo As ReportParameter() = New ReportParameter() {New ReportParameter("TripNo", ddlExistingTripDetails.SelectedItem.Text & "-" & iIndNo)}
            ReportViewer1.LocalReport.SetParameters(TripNo)

            Dim RouteName As ReportParameter() = New ReportParameter() {New ReportParameter("RouteName", ddlRoute.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(RouteName)

            Dim VehicleNo As ReportParameter() = New ReportParameter() {New ReportParameter("VehicleNo", ddlVehicleNo.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(VehicleNo)

            Dim Dte As ReportParameter() = New ReportParameter() {New ReportParameter("Dte", txtStartDate.Text)}
            ReportViewer1.LocalReport.SetParameters(Dte)

            CashPaid = dt.Rows(0)("AdvancePaidToDriver")
            Amount = dt.Rows(0)("Amount")
            OtherExp = dt.Rows(0)("OtherExpense")
            TAmount = CashPaid + Amount + OtherExp
            Dim Total As ReportParameter() = {New ReportParameter("Total", String.Format("{0:0.00}", Convert.ToDecimal(TAmount)))}
            ReportViewer1.LocalReport.SetParameters(Total)
            OilLtr = dt.Rows(0)("OilInLtr")
            oilAmt = dt.Rows(0)("OilRateLtr")
            oilTotal = OilLtr * oilAmt

            Dim oilTotalAmt As ReportParameter() = {New ReportParameter("oilTotalAmt", String.Format("{0:0.00}", Convert.ToDecimal(oilTotal)))}
            ReportViewer1.LocalReport.SetParameters(oilTotalAmt)
            diselAMt = dt.Rows(0)("DieselinLtrs")
            diselLtr = dt.Rows(0)("RateperLtrs")
            dieselAmt = diselAMt * diselLtr
            Dim dieselTotalAMt As ReportParameter() = {New ReportParameter("dieselTotalAMt", String.Format("{0:0.00}", Convert.ToDecimal(dieselAmt)))}
            ReportViewer1.LocalReport.SetParameters(dieselTotalAMt)

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType

            Response.AddHeader("content-disposition", "attachment; filename=LgstTripGeneration" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Private Sub dgDiesel_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDiesel.ItemDataBound
        Dim imgbtnView As New ImageButton
        Try
            If e.Item.ItemType = DataControlRowType.DataRow Then
                imgbtnView = CType(e.Item.FindControl("imgbtnView"), ImageButton)
                imgbtnView.ImageUrl = "~/Images/View16.png"

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDiesel_ItemDataBound")
        End Try
    End Sub

    Private Sub ddlVehicleNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVehicleNo.SelectedIndexChanged
        Try
            If ddlRoute.SelectedIndex > 0 Then
                txtMRStart.Text = objTG.GetMeterReading(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlVehicleNo.SelectedValue)
            Else
                lblError.Text = "Select Route first"
                ddlRoute.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlVehicleNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub txtMREnd_TextChanged(sender As Object, e As EventArgs) Handles txtMREnd.TextChanged
        Dim dFinalVal As Double = 0.0
        Try
            lblError.Text = ""
            If txtMRStart.Text = "" Then
                lblError.Text = "Set Meter reading start point"
                Exit Sub
            Else
                If Val(txtMRStart.Text) > Val(txtMREnd.Text) Then
                    lblError.Text = "End Point Meter Reading Must be Greater than Start Point."
                    txtMREnd.Text = "" : txtMREnd.Focus()
                    Exit Sub
                End If
                'dFinalVal = Val(txtMREnd.Text) - Val(txtMRStart.Text)
                'If dFinalVal > Val(TxtDstanceKM.Text) Then
                '    lblMeterStatus.Text = "Access"
                'ElseIf dFinalVal = Val(TxtDstanceKM.Text) Then
                '    lblMeterStatus.Text = "Same"
                'Else
                '    lblMeterStatus.Text = "Lack"
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtMREnd_TextChanged")
        End Try
    End Sub

    Private Sub txtStopDate_TextChanged(sender As Object, e As EventArgs) Handles txtStopDate.TextChanged
        Dim dToDate As String = ""
        Dim dDate As New Date, dSDate As New Date, dmDate As New Date
        Dim m As Integer = 0
        Try
            If txtStopDate.Text <> "" Then
                If txtStartDate.Text <> "" Then
                    If txtStartTime.Text <> "" Then
                        'If txtStartDate.Text < txtStopDate.Text Then
                        '    txtStopDate.Text = ""
                        '    lblError.Text = "Stop date " & (txtStartDate.Text) & " should be greater than start date " & (txtStartDate.Text)
                        '    lblTGValidationMsg.Text = lblError.Text
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        '    Exit Sub
                        'End If
                        dDate = Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        dSDate = Date.ParseExact(txtStopDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        m = DateDiff(DateInterval.Day, dSDate, dDate)
                        If m > 0 Then


                            lblError.Text = "Stop date " & (txtStopDate.Text) & " should be greater than start date " & (txtStartDate.Text)
                            lblTGValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            txtStopDate.Text = ""
                            Exit Sub
                        End If
                        If ddlDestinCustomer.SelectedIndex > 0 Then
                            dToDate = objTG.GetCustomerBilldate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDestinCustomer.SelectedValue, ddlRoute.SelectedValue)
                            If dToDate <> "" Then
                                dmDate = objGen.FormatDtForRDBMS(dToDate, "D")
                                dSDate = Date.ParseExact(txtStopDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                                m = DateDiff(DateInterval.Day, dSDate, dmDate)
                                If m > 0 Then
                                    lblError.Text = "Already invoice raised to this customer, End the trip after " & dToDate
                                    lblTGValidationMsg.Text = lblError.Text
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                    ddlDestinCustomer.Focus()
                                    txtStopDate.Text = ""
                                    Exit Sub
                                Else
                                End If
                            End If
                        Else
                            lblError.Text = "Select Service given to Customer"
                            txtStopDate.Text = ""
                            Exit Sub
                        End If
                        If ddlDriver.SelectedIndex > 0 Then
                            dToDate = ""
                            dToDate = objTG.GetDriverBilldate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDriver.SelectedValue)
                            If dToDate <> "" Then
                                dmDate = objGen.FormatDtForRDBMS(dToDate, "D")
                                dSDate = Date.ParseExact(txtStopDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                                m = DateDiff(DateInterval.Day, dSDate, dmDate)
                                If m > 0 Then
                                    lblError.Text = "Already invoice raised to this Driver, End the trip after " & dToDate
                                    lblTGValidationMsg.Text = lblError.Text
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                    ddlDriver.Focus() : txtStopDate.Text = ""
                                    Exit Sub
                                Else
                                End If
                            End If
                        Else
                            lblError.Text = "Select Driver for the trip"
                            txtStopDate.Text = ""
                            Exit Sub
                        End If
                        If dgDiesel.Items.Count > 0 Then
                            dToDate = ""
                            For i = 0 To dgDiesel.Items.Count - 1
                                dToDate = objTG.GetPumpBilldate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dgDiesel.Items(i).Cells(12).Text)
                                If dToDate <> "" Then
                                    dToDate = objGen.FormatDtForRDBMS(dToDate, "D")
                                    dSDate = Date.ParseExact(txtStopDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                                    m = DateDiff(DateInterval.Day, dSDate, dmDate)
                                    If m > 0 Then
                                        lblError.Text = "Already invoice raised to this" & dgDiesel.Items(i).Cells(2).Text & " pump. End the trip after " & dToDate
                                        lblTGValidationMsg.Text = lblError.Text
                                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                                        txtStopDate.Text = ""
                                        Exit Sub
                                    Else
                                    End If
                                End If
                            Next
                        End If



                    Else
                        lblError.Text = "Enter Start Time"
                        txtStopDate.Text = ""
                        Exit Sub
                    End If
                Else
                    lblError.Text = "Enter Start Date"
                    txtStopDate.Text = ""
                    Exit Sub
                End If
            Else
                lblError.Text = "Enter Stop Time"
                txtStopDate.Text = "" : txtStartTime.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtStopDate_TextChanged")
        End Try
    End Sub

    Private Sub txtStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtStartDate.TextChanged
        Dim hDate As New Date
        Try
            '  hDate = txtStartDate.Text.ToString.m

            '    hDate.MaxLength = txtStopDate.Text
            'Dim selectedFromDate As DateTime = DateTime.Parse(txtStartDate.Text.ToString())
            'Dim maxDate As DateTime = selectedFromDate.AddMonths(3)
            'ccStopDate.MaximumValue = maxDate.ToString()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtStartDate_TextChanged")
        End Try
    End Sub
    Protected Sub txtOilRate_TextChanged(sender As Object, e As EventArgs)
        Dim OilVal As Double = 0.0
        Try
            If Val(txtOilinLtr.Text) <> 0 Then
                If Val(txtOilRate.Text) <> 0 Then
                    OilVal = Val(txtOilinLtr.Text) * Val(txtOilRate.Text)
                    txtDieselAmount.Text = (Val(txtDieselinLtrs.Text) * Val(txtRatePrLtr.Text)) + OilVal
                Else
                    lblError.Text = "Enter Oil Rate"
                    txtOilRate.Text = ""
                    Exit Sub
                End If
            Else
                lblError.Text = "Enter Oil"
                txtOilRate.Text = ""
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
