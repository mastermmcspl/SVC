Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class VehicleMaintanance_ComplianceDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "VehicleMaintanance_ComplianceDashboard"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASPermission As New clsFASPermission
    Private Shared sSession As AllSession
    Dim objvm As New clsVehicleMaintanance
    Dim objGen As New clsFASGeneral
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sModule As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sModule = objclsFASPermission.GetLoginUserModulePermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, 1)
                If sModule = "False" Then
                    ' Response.Redirect("~/Permissions/SysAdminPermissionModule.aspx", False) 'Permissions/SysAdminPermissionModule
                    Exit Sub
                End If
                loadComplianceType()
                '  BindSODetails(0, ddlStatus.SelectedIndex)
            End If

        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub loadComplianceType()
        Dim dt As New DataTable
        Try
            ddlCompliancetype.Items.Insert(0, "---Select Compliance Type ---")
            ddlCompliancetype.Items.Insert(1, "Battery")
            ddlCompliancetype.Items.Insert(2, "Tyres")
            ddlCompliancetype.Items.Insert(3, "Other Compliance")
            ddlCompliancetype.Items.Insert(4, "Loan Due")
            ddlCompliancetype.Items.Insert(5, "Insurance Due")
            ddlCompliancetype.Items.Insert(6, "All Compliances")
            ddlCompliancetype.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadComplianceType")
        End Try
    End Sub
    Public Sub BindSODetails(ByVal iComplianceType As Integer)
        Dim dt As New DataTable, dtCompliance As New DataTable, dtBattery As New DataTable
        Dim dtLoan As New DataTable, dtInsurance As New DataTable
        Try
            '  If ddlStatus.SelectedIndex = 0 Then
            '  ElseIf ddlStatus.SelectedIndex = 1 Then
            If iComplianceType = 1 Then
                dtBattery = objvm.LoadBatteryCompliance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                gvBatteryComp.Visible = True : lblBattery.Visible = True
                gvBatteryComp.DataSource = dtBattery
                gvBatteryComp.DataBind()
            ElseIf iComplianceType = 2 Then
                dt = objvm.LoadTyreCompliance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvTyreMaster.Visible = True : lblTyre.Visible = True
                GvTyreMaster.DataSource = dt
                GvTyreMaster.DataBind()
            ElseIf iComplianceType = 3 Then
                dtCompliance = objvm.LoadCompliance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                gvCompliance.Visible = True : lblOther.Visible = True
                gvCompliance.DataSource = dtCompliance
                gvCompliance.DataBind()
            ElseIf iComplianceType = 4 Then
                dtBattery = objvm.LoadLoanDue(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvLoan.Visible = True : lblLoan.Visible = True
                GvLoan.DataSource = dtBattery
                GvLoan.DataBind()
            ElseIf iComplianceType = 5 Then
                dtBattery = objvm.LoadInsuranceDue(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvInsurance.Visible = True : lblIns.Visible = True
                GvInsurance.DataSource = dtBattery
                GvInsurance.DataBind()
            ElseIf iComplianceType = 6 Then
                dt = objvm.LoadTyreCompliance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvTyreMaster.Visible = True : lblTyre.Visible = True
                GvTyreMaster.DataSource = dt
                GvTyreMaster.DataBind()

                dtCompliance = objvm.LoadCompliance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                gvCompliance.Visible = True : lblOther.Visible = True
                gvCompliance.DataSource = dtCompliance
                gvCompliance.DataBind()

                dtBattery = objvm.LoadBatteryCompliance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                gvBatteryComp.Visible = True : lblBattery.Visible = True
                gvBatteryComp.DataSource = dtBattery
                gvBatteryComp.DataBind()

                dtInsurance = objvm.LoadInsuranceDue(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvInsurance.Visible = True : lblIns.Visible = True
                GvInsurance.DataSource = dtInsurance
                GvInsurance.DataBind()

                dtLoan = objvm.LoadLoanDue(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvLoan.Visible = True : lblLoan.Visible = True
                GvLoan.DataSource = dtLoan
                GvLoan.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSODetails")
        End Try
    End Sub
    Private Sub GvTyreMaster_PreRender(sender As Object, e As EventArgs) Handles GvTyreMaster.PreRender
        Dim dt As New DataTable
        Try
            If GvTyreMaster.Rows.Count > 0 Then
                GvTyreMaster.UseAccessibleHeader = True
                GvTyreMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                GvTyreMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvTyreMaster_PreRender")
        End Try
    End Sub
    Private Sub gvCompliance_PreRender(sender As Object, e As EventArgs) Handles gvCompliance.PreRender
        Dim dt As New DataTable
        Try
            If gvCompliance.Rows.Count > 0 Then
                gvCompliance.UseAccessibleHeader = True
                gvCompliance.HeaderRow.TableSection = TableRowSection.TableHeader
                gvCompliance.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompliance_PreRender")
        End Try
    End Sub
    Private Sub gvBatteryComp_PreRender(sender As Object, e As EventArgs) Handles gvBatteryComp.PreRender
        Dim dt As New DataTable
        Try
            If gvBatteryComp.Rows.Count > 0 Then
                gvBatteryComp.UseAccessibleHeader = True
                gvBatteryComp.HeaderRow.TableSection = TableRowSection.TableHeader
                gvBatteryComp.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompliance_PreRender")
        End Try
    End Sub
    Private Sub GvInsurance_PreRender(sender As Object, e As EventArgs) Handles GvInsurance.PreRender
        Dim dt As New DataTable
        Try
            If GvInsurance.Rows.Count > 0 Then
                GvInsurance.UseAccessibleHeader = True
                GvInsurance.HeaderRow.TableSection = TableRowSection.TableHeader
                GvInsurance.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvInsurance_PreRender")
        End Try
    End Sub
    Private Sub GvLoan_PreRender(sender As Object, e As EventArgs) Handles GvLoan.PreRender
        Dim dt As New DataTable
        Try
            If GvLoan.Rows.Count > 0 Then
                GvLoan.UseAccessibleHeader = True
                GvLoan.HeaderRow.TableSection = TableRowSection.TableHeader
                GvLoan.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvLoan_PreRender")
        End Try
    End Sub
    Private Sub ddlCompliancetype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompliancetype.SelectedIndexChanged
        Try
            If ddlCompliancetype.SelectedIndex > 0 Then
                gvBatteryComp.Visible = False : lblBattery.Visible = False
                gvCompliance.Visible = False : lblOther.Visible = False
                GvTyreMaster.Visible = False : lblTyre.Visible = False
                GvInsurance.Visible = False : lblIns.Visible = False
                GvLoan.Visible = False : lblLoan.Visible = False
                BindSODetails(ddlCompliancetype.SelectedIndex)
            Else
                lblError.Text = "Select Compliance Type"
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvBatteryComp_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvBatteryComp.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/VehicleMaintanance/LgstComplianceDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterID), False) 'GeneralMasterDetails
            End If
            'If e.CommandName.Equals("Status") Then
            '    If ddlStatus.SelectedIndex = 0 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 1 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 2 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            'BindSODetails(0, ddlStatus.SelectedIndex)
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvBatteryComp_RowCommand")
        End Try
    End Sub
    Private Sub gvBatteryComp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvBatteryComp.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                '    imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                '   GvVehicleMaster.Columns(0).Visible = True
                '  GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    '  If sCMAD = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    '  If sCMAD = "YES" Then
                    '      GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    '   If sCMAP = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    ' End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    '    GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvBatteryComp_RowDataBound")
        End Try
    End Sub
    Private Sub GvTyreMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvTyreMaster.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/VehicleMaintanance/LgstComplianceDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterID), False) 'GeneralMasterDetails
            End If
            'If e.CommandName.Equals("Status") Then
            '    If ddlStatus.SelectedIndex = 0 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 1 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 2 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            'BindSODetails(0, ddlStatus.SelectedIndex)
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvTyreMaster_RowCommand")
        End Try
    End Sub
    Private Sub GvTyreMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvTyreMaster.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                '    imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                '   GvVehicleMaster.Columns(0).Visible = True
                '  GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    '  If sCMAD = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    '  If sCMAD = "YES" Then
                    '      GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    '   If sCMAP = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    ' End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    '    GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvTyreMaster_RowDataBound")
        End Try
    End Sub
    Private Sub gvCompliance_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCompliance.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/VehicleMaintanance/LgstComplianceDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterID), False) 'GeneralMasterDetails
            End If
            'If e.CommandName.Equals("Status") Then
            '    If ddlStatus.SelectedIndex = 0 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 1 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 2 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            'BindSODetails(0, ddlStatus.SelectedIndex)
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompliance_RowCommand")
        End Try
    End Sub
    Private Sub gvCompliance_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCompliance.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                '    imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                '   GvVehicleMaster.Columns(0).Visible = True
                '  GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    '  If sCMAD = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    '  If sCMAD = "YES" Then
                    '      GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    '   If sCMAP = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    ' End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    '    GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompliance_RowDataBound")
        End Try
    End Sub
    Private Sub GvLoan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvLoan.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/VehicleMaintanance/LgstComplianceDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterID), False) 'GeneralMasterDetails
            End If
            'If e.CommandName.Equals("Status") Then
            '    If ddlStatus.SelectedIndex = 0 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 1 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 2 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            'BindSODetails(0, ddlStatus.SelectedIndex)
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvLoan_RowCommand")
        End Try
    End Sub
    Private Sub GvLoan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvLoan.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                '    imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                '   GvVehicleMaster.Columns(0).Visible = True
                '  GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    '  If sCMAD = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    '  If sCMAD = "YES" Then
                    '      GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    '   If sCMAP = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    ' End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    '    GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvLoan_RowDataBound")
        End Try
    End Sub
    Private Sub GvInsurance_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvInsurance.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/VehicleMaintanance/LgstComplianceDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterID), False) 'GeneralMasterDetails
            End If
            'If e.CommandName.Equals("Status") Then
            '    If ddlStatus.SelectedIndex = 0 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 1 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            '    If ddlStatus.SelectedIndex = 2 Then
            '        objDPM.UpdateDieselMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            '    End If
            'BindSODetails(0, ddlStatus.SelectedIndex)
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvInsurance_RowCommand")
        End Try
    End Sub
    Private Sub GvInsurance_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvInsurance.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                '    imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                '   GvVehicleMaster.Columns(0).Visible = True
                '  GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    '  If sCMAD = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    '  If sCMAD = "YES" Then
                    '      GvVehicleMaster.Columns(7).Visible = True
                    'End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    '   If sCMAP = "YES" Then
                    '     GvVehicleMaster.Columns(7).Visible = True
                    ' End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    '    GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvInsurance_RowDataBound")
        End Try
    End Sub
End Class
