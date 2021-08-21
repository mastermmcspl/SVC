Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class Masters_EmployeeMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_EmployeeMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsFASGeneral
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private objclsCheckMasterIsInUse As New clsCheckMasterIsInUse
    Private objclsFASPermission As New clsFASPermission
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Private Shared sEMPSave As String
    Private Shared sEMPAD As String
    Private Shared sEMPAP As String
    Private Shared sEMPBL As String
    Private Shared dtEmpDetails As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/AddUser24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnUnLock.ImageUrl = "~/Images/Unlock24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnUnBlock.ImageUrl = "~/Images/CheckedUser24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "EUM")
                imgbtnReport.Visible = False : sEMPAD = "NO" : sEMPAP = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        sEMPAD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sEMPAP = "YES"
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If
                'imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                'imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                'imgbtnUnLock.Visible = False : imgbtnUnBlock.Visible = False
                'sEMPSave = "NO" : sEMPAD = "NO" : sEMPBL = "NO"
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasUC", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnAdd.Visible = True
                '        sEMPSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Active/DeActive,") = True Then
                '        sEMPAD = "YES"
                '    End If
                'End If

                BindSearchDDL() : BindStatus()

                RFVSearch.ErrorMessage = "Select Search by."
                RFVSearch.InitialValue = "Select"

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                dtEmpDetails = objclsEmployeeMaster.LoadAllEmpDetails(sSession.AccessCode, sSession.AccessCodeID)
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindSearchDDL()
        Try
            ddlSearch.Items.Insert(0, "Select")
            ddlSearch.Items.Insert(1, "SAP Code")
            ddlSearch.Items.Insert(2, "Employee Name")
            ddlSearch.Items.Insert(3, "Designation")
            ddlSearch.Items.Insert(4, "Role")
            ddlSearch.Items.Insert(5, "Module")
            ddlSearch.Items.Insert(6, "Zone")
            ddlSearch.Items.Insert(7, "Region")
            ddlSearch.Items.Insert(8, "Area")
            ddlSearch.Items.Insert(9, "Branch")
            ddlSearch.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Locked")
            ddlStatus.Items.Insert(3, "Blocked")
            ddlStatus.Items.Insert(4, "Waiting for Approval")
            ddlStatus.Items.Insert(5, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oEmpID As New Object, oStatusID As New Object
        Try
            lblError.Text = ""
            oEmpID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Or ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 4 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(4))
            ElseIf ddlStatus.SelectedIndex = 5 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(5))
            End If
            Response.Redirect(String.Format("~/Masters/EmployeeMasterDetails.aspx?EmpID={0}&StatusID={1}", oEmpID, oStatusID), False) 'EmployeeMasterDetails
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            LoadAllEmpDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadAllEmpDeatils(ByVal iPageIndex As Integer, ByVal sPageType As String, ByVal sIsReport As String) As DataTable
        Dim dt As New DataTable
        Dim sSearchText As String = "", sStatus As String = ""
        Try
            imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnUnLock.Visible = False : imgbtnUnBlock.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"
                If sEMPAD = "YES" Then
                    imgbtnDeActivate.Visible = True 'Activate
                End If
            ElseIf ddlStatus.SelectedIndex = 1 Then
                    sStatus = "De-Activated"
                    If sEMPAD = "YES" Then
                        imgbtnActivate.Visible = True 'De-Activate
                    End If
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    sStatus = "Lock"
                    If sEMPAD = "YES" Then
                        imgbtnUnLock.Visible = True 'Lock
                    End If
                ElseIf ddlStatus.SelectedIndex = 3 Then
                    sStatus = "Block"
                If sEMPAD = "YES" Then
                    imgbtnUnBlock.Visible = True 'Block
                End If
            ElseIf ddlStatus.SelectedIndex = 4 Then
                    sStatus = "Waiting for Approval"
                If sEMPAP = "YES" Then
                    imgbtnWaiting.Visible = True 'Waiting for Approval
                End If
            End If
                sSearchText = objclsGRACeGeneral.SafeSQL(txtSearch.Text.Trim)
            If ddlSearch.SelectedIndex > 0 And sSearchText <> "" Then
                If ddlSearch.SelectedIndex = 1 Then 'SAP Code
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    If sIsReport = "YES" Then
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                        End If
                    Else
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "SAPCode Like '" & sSearchText & "%' And Status='" & sStatus & "'"
                        Else
                            DVZRBADetails.RowFilter = "SAPCode like '" & sSearchText & "%'"
                        End If
                    End If
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                ElseIf ddlSearch.SelectedIndex = 2 Then 'Employee Name
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    If sIsReport = "YES" Then
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                        End If
                    Else
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "EmployeeName like '" & sSearchText & "%' And Status='" & sStatus & "'"
                        Else
                            DVZRBADetails.RowFilter = "EmployeeName like '" & sSearchText & "%'"
                        End If
                    End If
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                ElseIf ddlSearch.SelectedIndex = 3 Then 'Designation
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    If sIsReport = "YES" Then
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                        End If
                    Else
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Designation like '" & sSearchText & "%' And Status='" & sStatus & "'"
                        Else
                            DVZRBADetails.RowFilter = "Designation like '" & sSearchText & "%'"
                        End If
                    End If
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                ElseIf ddlSearch.SelectedIndex = 4 Or ddlSearch.SelectedIndex = 5 Then 'Role & Module
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    If sIsReport = "YES" Then
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                        End If
                    Else
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Module like '%" & sSearchText & "%' And Status='" & sStatus & "'"
                        Else
                            DVZRBADetails.RowFilter = "Module like '%" & sSearchText & "%'"
                        End If
                    End If
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                ElseIf ddlSearch.SelectedIndex = 6 Then 'Zone
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    If sIsReport = "YES" Then
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                        End If
                    Else
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Zone like '" & sSearchText & "%' And Status='" & sStatus & "'"
                        Else
                            DVZRBADetails.RowFilter = "Zone like '" & sSearchText & "%'"
                        End If
                    End If
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                ElseIf ddlSearch.SelectedIndex = 7 Then 'Region
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    If sIsReport = "YES" Then
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                        End If
                    Else
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Region like '" & sSearchText & "%' And Status='" & sStatus & "'"
                        Else
                            DVZRBADetails.RowFilter = "Region like '" & sSearchText & "%'"
                        End If
                    End If
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                ElseIf ddlSearch.SelectedIndex = 8 Then 'Area
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    If sIsReport = "YES" Then
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                        End If
                    Else
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Area like '" & sSearchText & "%' And Status='" & sStatus & "'"
                        Else
                            DVZRBADetails.RowFilter = "Area like '" & sSearchText & "%'"
                        End If
                    End If
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                ElseIf ddlSearch.SelectedIndex = 9 Then 'Branch
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    If sIsReport = "YES" Then
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                        End If
                    Else
                        If ddlStatus.SelectedIndex <= 4 Then
                            DVZRBADetails.RowFilter = "Branch like '" & sSearchText & "%' And Status='" & sStatus & "'"
                        Else
                            DVZRBADetails.RowFilter = "Branch like '" & sSearchText & "%'"
                        End If
                    End If
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                End If
            Else
                If ddlStatus.SelectedIndex <= 4 Then
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                Else
                    dt = Nothing
                    Dim DVZRBADetails As New DataView(dtEmpDetails)
                    DVZRBADetails.Sort = "EmployeeName ASC"
                    dt = DVZRBADetails.ToTable
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dt.Rows(i)("SrNo") = i + 1
                        Next
                        dt.AcceptChanges()
                    End If
                End If
            End If
            dgEmployeeDetails.DataSource = dt
            dgEmployeeDetails.DataBind()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllEmpDeatils")
        End Try
    End Function
    Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Try
            lblError.Text = ""
            LoadAllEmpDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
    Private Sub dgEmployeeDetails_PreRender(sender As Object, e As EventArgs) Handles dgEmployeeDetails.PreRender
        Dim dt As New DataTable
        Try
            If dgEmployeeDetails.Rows.Count > 0 Then
                dgEmployeeDetails.UseAccessibleHeader = True
                dgEmployeeDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                dgEmployeeDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgEmployeeDetails_PreRender")
        End Try
    End Sub
    Private Sub dgEmployeeDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgEmployeeDetails.RowDataBound
        Dim imgbtnStatus As ImageButton, imgbtnedit As ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnedit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnedit.ImageUrl = "~/Images/Edit16.png"

                If sEMPAD = "YES" Then
                    dgEmployeeDetails.Columns(0).Visible = True
                End If
                dgEmployeeDetails.Columns(14).Visible = False
                dgEmployeeDetails.Columns(15).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    If sEMPAD = "YES" Then
                        dgEmployeeDetails.Columns(14).Visible = True
                    End If
                    If sEMPSave = "YES" Then
                        dgEmployeeDetails.Columns(15).Visible = True
                    End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    If sEMPAD = "YES" Then
                        dgEmployeeDetails.Columns(14).Visible = True
                    End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Unlock16.png" : imgbtnStatus.ToolTip = "Unlock"
                    If sEMPAD = "YES" Then
                        dgEmployeeDetails.Columns(14).Visible = True
                    End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.ImageUrl = "~/Images/CheckedUser16.png" : imgbtnStatus.ToolTip = "Unblock"
                    If sEMPAD = "YES" Then
                        dgEmployeeDetails.Columns(14).Visible = True
                    End If
                End If

                If ddlStatus.SelectedIndex = 4 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    If sEMPAD = "YES" Then
                        dgEmployeeDetails.Columns(14).Visible = True
                    End If
                    If sEMPSave = "YES" Then
                        dgEmployeeDetails.Columns(15).Visible = True
                    End If
                End If

                If ddlStatus.SelectedIndex = 5 Then
                    dgEmployeeDetails.Columns(0).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgEmployeeDetails_RowDataBound")
        End Try
    End Sub
    Private Sub dgEmployeeDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgEmployeeDetails.RowCommand
        Dim lblEmpID As New Label
        Dim oEmpID As Object, oStatusID As Object
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""

            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblEmpID = DirectCast(clickedRow.FindControl("lblEmpID"), Label)

            If e.CommandName.Equals("Edit") Then
                oEmpID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblEmpID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Or ddlStatus.SelectedIndex = 3 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 4 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(4))
                Else
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                End If
                Response.Redirect(String.Format("~/Masters/EmployeeMasterDetails.aspx?EmpID={0}&StatusID={1}", oEmpID, oStatusID), False) 'EmployeeMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    'If objclsCheckMasterIsInUse.CheckEmployeeNameIsInUse(sSession.AccessCode, sSession.AccessCodeID, lblEmpID.Text) = True Then
                    '    lblEmpMasterValidationMsg.Text = "Already tag to some User, can't be De-Activate" : lblError.Text = "Already tag to some User, can't be De-Activate"
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                    '    Exit Sub
                    'End If
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "DeActivated")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "De-Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "De-Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Activated")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then 'Unlock
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnLock")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Unlocked." : lblError.Text = "Successfully Unlocked."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Unlocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 3 Then 'Unblock
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnBlock")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Unblocked." : lblError.Text = "Successfully Unblocked."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Unblocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If

                If ddlStatus.SelectedIndex = 4 Then 'Waiting for Approval
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Created")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Approved", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
                LoadAllEmpDeatils(0, "True", "NO")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgEmployeeDetails_RowCommand")
        End Try
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If dgEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Employee to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Employee to Activate.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = dgEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Activated")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = "" : ddlStatus.SelectedIndex = 0
            LoadAllEmpDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If dgEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Employee to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Employee to De-Activate.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = dgEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "DeActivated")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "De-Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
            Next

            ddlSearch.SelectedIndex = 0 : txtSearch.Text = "" : ddlStatus.SelectedIndex = 1
            LoadAllEmpDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnUnBlock_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUnBlock.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If dgEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to Unblock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Unblock.','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Employee to Unblock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Employee to Unblock.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = dgEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnBlock")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblError.Text = "Successfully Unblocked."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Unblocked','', 'success');", True)
                End If
            Next
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            LoadAllEmpDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUnBlock_Click")
        End Try
    End Sub
    Protected Sub imgbtnUnLock_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUnLock.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If dgEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to Unlock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Unlock.','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Employee to Unlock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Employee to Unlock.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = dgEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnLock")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblError.Text = "Successfully Unlocked."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Unlocked.','', 'success');", True)
                End If
            Next
            'lblEmpMasterValidationMsg.Text = "Successfully Unlocked." : lblError.Text = "Successfully Unlocked."
            'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Unlocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            LoadAllEmpDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUnLock_Click")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If dgEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve.','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Employee to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Employee to Approve.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgEmployeeDetails.Rows.Count - 1
                chkSelect = dgEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = dgEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Created")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved.','', 'success');", True)
                End If
            Next
            'lblEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Approved", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = "" : ddlStatus.SelectedIndex = 0
            LoadAllEmpDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgEmployeeDetails.Rows.Count - 1
                    chkField = dgEmployeeDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgEmployeeDetails.Rows.Count - 1
                    chkField = dgEmployeeDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = LoadAllEmpDeatils(0, "True", "YES")
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data.','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/RPTUserMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "User Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=UserMaster" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = LoadAllEmpDeatils(0, "True", "YES")
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data.','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/RPTUserMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "User Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=UserMaster" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub dgEmployeeDetails_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgEmployeeDetails.RowEditing

    End Sub
End Class
