Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class VehicleMaintanance_LgstInsuranceDashBoard
    Inherits System.Web.UI.Page
    Private sFormName As String = "VehicleMaintanance/LgstInsuranceDashBoard"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Private Shared sCMAD As String
    Private Shared sCMAP As String
    Private Shared sCMSave As String

    Dim objvim As New clsVehicleInsuranceMaster
    Dim objclsGeneralFunctions As New clsGeneralFunctions

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                GetAmount()
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LGSTDM")
                imgbtnReport.Visible = False : sCMAD = "NO" : sCMAP = "NO" : sCMSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sCMSave = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sCMAP = "YES"
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        sCMAD = "YES"
                    End If
                End If
                BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                'imgbtnReport.Visible = True
                BindSODetails(0, ddlStatus.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Waiting for Approval")
            ddlStatus.Items.Insert(3, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetAmount()
        Dim dDebitAmt As Double = 0.0, dInsuranceAmt As Double = 0.0, dBalanceAmt As Double = 0.0
        Try
            dDebitAmt = objvim.GetDebitAmt(sSession.AccessCode, sSession.AccessCodeID)
            dInsuranceAmt = objvim.GetInsuranceAmt(sSession.AccessCode, sSession.AccessCodeID)
            If dDebitAmt > dInsuranceAmt Then
                dBalanceAmt = Val(dDebitAmt) - Val(dInsuranceAmt)
                lblPaymentMasterValidationMsg.Text = "Add the Insurance Amount of " & dBalanceAmt & " to vehicle." : lblError.Text = "Add the Insurance Amount of " & dBalanceAmt & " to vehicle ."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sMainMaster As String = ""
        Try
            lblError.Text = ""
            sMainMaster = ""
            imgbtnAdd.Visible = True
            If ddlStatus.SelectedIndex = 0 Then
                If sCMAD = "YES" Then
                End If
            ElseIf ddlStatus.SelectedIndex = 1 Then
                If sCMAD = "YES" Then
                    '    imgbtnActivate.Visible = True
                End If
                '    imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                If sCMAP = "YES" Then
                    '  imgbtnWaiting.Visible = True
                End If
                '    imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            Else
                '   imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If
            BindSODetails(0, ddlStatus.SelectedIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindSODetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer)
        Dim dt As New DataTable
        Try
            If ddlStatus.SelectedIndex = 0 Then
                If sCMAD = "YES" Then
                    '   imgbtnDeActivate.Visible = True
                End If
                '    imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 1 Then
                If sCMAD = "YES" Then
                    '     imgbtnActivate.Visible = True
                End If
                '   imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                If sCMAP = "YES" Then
                    '     imgbtnWaiting.Visible = True
                End If
                '   imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            Else
                '  imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If
            dt = objvim.LoadVehicleInsuranceDashDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus)
            GvVehicleMaster.DataSource = dt
            GvVehicleMaster.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSODetails")
        End Try
    End Sub
    Private Sub GvVehicleMaster_PreRender(sender As Object, e As EventArgs) Handles GvVehicleMaster.PreRender
        Dim dt As New DataTable
        Try
            If GvVehicleMaster.Rows.Count > 0 Then
                GvVehicleMaster.UseAccessibleHeader = True
                GvVehicleMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                GvVehicleMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvVehicleMaster_PreRender")
        End Try
    End Sub
    Private Sub GvVehicleMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvVehicleMaster.RowDataBound
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
                    If sCMAD = "YES" Then
                        '     GvVehicleMaster.Columns(7).Visible = True
                    End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    If sCMAD = "YES" Then
                        '      GvVehicleMaster.Columns(7).Visible = True
                    End If
                    'If sCMSave = "YES" Then
                    '    dgParty.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    If sCMAP = "YES" Then
                        '     GvVehicleMaster.Columns(7).Visible = True
                    End If
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvVehicleMaster_RowDataBound")
        End Try
    End Sub
    Private Sub GvVehicleMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvVehicleMaster.RowCommand
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
                Response.Redirect(String.Format("~/VehicleMaintanance/LgstInsuranceDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterID), False) 'GeneralMasterDetails
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDiesel_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try

            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(3))
            End If
            Response.Redirect(String.Format("~/VehicleMaintanance/LgstInsuranceDetails.aspx?StatusID={0}&PID={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    '    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
    '        Dim chkSelect As New CheckBox
    '        Dim iCount As Integer
    '        Dim lblDescID As New Label
    '        'Dim DVdt As New DataView(dt)
    '        Try
    '            lblError.Text = ""
    '            If GvVehicleMaster.Rows.Count = 0 Then
    '                lblError.Text = "No data to Activate"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Activate','', 'info');", True)
    '                Exit Sub
    '            End If
    '            For i = 0 To GvVehicleMaster.Rows.Count - 1
    '                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
    '                If chkSelect.Checked = True Then
    '                    iCount = 1
    '                    GoTo NextSave
    '                End If
    '            Next
    '            If iCount = 0 Then
    '                lblError.Text = "Select to Activate."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate.','', 'info');", True)
    '                Exit Sub
    '            End If
    'NextSave:   For i = 0 To GvVehicleMaster.Rows.Count - 1
    '                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
    '                lblDescID = GvVehicleMaster.Rows(i).FindControl("lblDescID")
    '                If chkSelect.Checked = True Then
    '                    '    objDPM.UpdateDieselMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, sSession.IPAddress, lblDescID.Text)
    '                    lblError.Text = "Successfully Activated."
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
    '                End If
    '            Next

    '            'If ddlStatus.SelectedIndex = 1 Then
    '            'objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, sSession.IPAddress)
    '            'lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
    '            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
    '            'End If
    '            BindSODetails(0, ddlStatus.SelectedIndex)
    '        Catch ex As Exception
    '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
    '        End Try
    '    End Sub
    '    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
    '        Dim chkSelect As New CheckBox
    '        Dim iCount As Integer
    '        Dim lblDescID As New Label
    '        'Dim DVdt As New DataView(dt)Dashboard
    '        Try
    '            lblError.Text = ""
    '            If GvVehicleMaster.Rows.Count = 0 Then
    '                lblError.Text = "No data to De-Activate"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
    '                Exit Sub
    '            End If
    '            For i = 0 To GvVehicleMaster.Rows.Count - 1
    '                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
    '                If chkSelect.Checked = True Then
    '                    iCount = 1
    '                    GoTo NextSave
    '                End If
    '            Next
    '            If iCount = 0 Then
    '                lblError.Text = "Select to De-Activate."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to De-Activate.','', 'info');", True)
    '                Exit Sub
    '            End If
    'NextSave:   For i = 0 To GvVehicleMaster.Rows.Count - 1
    '                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
    '                lblDescID = GvVehicleMaster.Rows(i).FindControl("lblDescID")
    '                If chkSelect.Checked = True Then
    '                    '  objDPM.UpdateDieselMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, sSession.IPAddress, lblDescID.Text)
    '                    lblError.Text = "Successfully De-Activated."
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
    '                End If
    '            Next

    '            'If ddlStatus.SelectedIndex = 0 Then
    '            '    objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, sSession.IPAddress)
    '            '    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
    '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
    '            'End If
    '            BindSODetails(0, ddlStatus.SelectedIndex)
    '        Catch ex As Exception
    '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
    '        End Try
    '    End Sub
    '    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
    '        Dim chkSelect As New CheckBox
    '        Dim iCount As Integer
    '        Dim lblDescID As New Label
    '        'Dim DVdt As New DataView(dt)
    '        Try
    '            lblError.Text = ""
    '            If GvVehicleMaster.Rows.Count = 0 Then
    '                lblError.Text = "No data to Approve"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
    '                Exit Sub
    '            End If
    '            For i = 0 To dgDiesel.Rows.Count - 1
    '                chkSelect = dgDiesel.Rows(i).FindControl("chkSelect")
    '                If chkSelect.Checked = True Then
    '                    iCount = 1
    '                    GoTo NextSave
    '                End If
    '            Next
    '            If iCount = 0 Then
    '                lblError.Text = "Select to Approve."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve.','', 'info');", True)
    '                Exit Sub
    '            End If
    'NextSave:   For i = 0 To dgDiesel.Rows.Count - 1
    '                chkSelect = dgDiesel.Rows(i).FindControl("chkSelect")
    '                lblDescID = dgDiesel.Rows(i).FindControl("lblDescID")
    '                If chkSelect.Checked = True Then
    '                    objDPM.UpdateDieselMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "A", sSession.UserID, sSession.IPAddress, lblDescID.Text)
    '                    lblError.Text = "Successfully Approved."
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
    '                End If
    '            Next

    '            'If ddlStatus.SelectedIndex = 2 Then
    '            '    objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, sSession.IPAddress)
    '            '    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
    '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
    '            'End If
    '            BindSODetails(0, ddlStatus.SelectedIndex)
    '        Catch ex As Exception
    '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
    '        End Try
    '    End Sub
    'Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
    '    Dim chkField As New CheckBox, chkAll As New CheckBox
    '    Dim iIndx As Integer
    '    Try
    '        lblError.Text = ""
    '        chkAll = CType(sender, CheckBox)
    '        If chkAll.Checked = True Then
    '            For iIndx = 0 To dgDiesel.Rows.Count - 1
    '                chkField = dgDiesel.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = True
    '            Next
    '        Else
    '            For iIndx = 0 To dgDiesel.Rows.Count - 1
    '                chkField = dgDiesel.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = False
    '            Next
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
    '    End Try
    'End Sub
    Private Sub GvVehicleMaster_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GvVehicleMaster.RowEditing

    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            '    dt = objvim.LoadVehicleAdditionalDashDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/RPTCustomerMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CustomerMaster" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            'HttpContext.Current.Response.Flush() 'Sends all currently buffered output To the client.
            'HttpContext.Current.Response.SuppressContent = True 'Gets Or sets a value indicating whether To send HTTP content To the client.
            'HttpContext.Current.ApplicationInstance.CompleteRequest() 'Causes ASP.NET To bypass all events And filtering In the HTTP pipeline chain Of execution And directly execute the EndRequest Event.
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            '    dt = objvam.LoadVehicleAdditionalDashDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/RPTCustomerMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CustomerMaster" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            'HttpContext.Current.Response.Flush() 'Sends all currently buffered output To the client.
            'HttpContext.Current.Response.SuppressContent = True 'Gets Or sets a value indicating whether To send HTTP content To the client.
            'HttpContext.Current.ApplicationInstance.CompleteRequest() 'Causes ASP.NET To bypass all events And filtering In the HTTP pipeline chain Of execution And directly execute the EndRequest Event.
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
End Class
