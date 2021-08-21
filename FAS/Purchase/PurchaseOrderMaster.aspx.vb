Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class Purchase_PurchaseOrderMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Purchase_PurchaseOrderMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsFASGeneral
    Private objPo As New clsPurchaseOrder
    Private objclsCheckMasterIsInUse As New clsCheckMasterIsInUse
    Private objclsModulePermission As New clsModulePermission
    Private objclsFASPermission As New clsFASPermission
    Private Shared sSession As AllSession
    Private Shared sEMPSave As String
    Private Shared sEMPAD As String
    Private Shared sEMPAP As String
    Private Shared sEMPBL As String
    Private Shared dtPoDetails As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnUnLock.ImageUrl = "~/Images/Unlock24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnUnBlock.ImageUrl = "~/Images/CheckedUser24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnWaiting.Visible = True

                imgbtnUnLock.Visible = True : imgbtnUnBlock.Visible = True
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PO")
                imgbtnReport.Visible = False : sEMPAD = "NO" : sEMPAP = "NO" : sEMPSave = "NO" : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        sEMPAD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sEMPAP = "YES"
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sEMPSave = "YES"
                    End If
                    'If sFormButtons = ",View,New,SaveOrUpdate,Report," Then
                    '    imgbtnReport.Visible = True
                    'End If
                End If
                'sEMPSave = "NO" : sEMPAD = "YES" : sEMPBL = "NO"

                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FASPOM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",ADD,") = True Then
                '        imgbtnAdd.Visible = True
                '    End If
                'End If
                BindSearchDDL() : BindStatus()

                RFVSearch.ErrorMessage = "Select Search by."
                RFVSearch.InitialValue = "Select"
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                dtPoDetails = objPo.LoadAllPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID)
                dgPurchase.DataSource = dtPoDetails
                dgPurchase.DataBind()
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
            'ddlSearch.Items.Insert(1, "SAP Code")
            'ddlSearch.Items.Insert(2, "Employee Name")
            'ddlSearch.Items.Insert(3, "Designation")
            'ddlSearch.Items.Insert(4, "Role")
            'ddlSearch.Items.Insert(5, "Module")
            'ddlSearch.Items.Insert(6, "Zone")
            'ddlSearch.Items.Insert(7, "Region")
            'ddlSearch.Items.Insert(8, "Area")
            'ddlSearch.Items.Insert(9, "Branch")
            ddlSearch.SelectedIndex = 0
        Catch ex As Exception
            Throw
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
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oEmpID As New Object, oStatusID As New Object
        Try
            lblError.Text = ""
            oEmpID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                'ElseIf ddlStatus.SelectedIndex = 2 Or ddlStatus.SelectedIndex = 3 Then
                '    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(4))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(5))
            End If
            Response.Redirect(String.Format("~/Purchase/PurchaseOrder.aspx?EmpID={0}&StatusID={1}", oEmpID, oStatusID), False) 'Purchase Order Details
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            ' LoadAllEmpDeatils(0, "True", "NO")
            LoadAllEmpDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadAllEmpDeatils() As DataTable
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
                sStatus = "Waiting for Approval"
                If sEMPAP = "YES" Then
                    imgbtnWaiting.Visible = True 'Waiting for Approval
                End If
            End If

            If ddlStatus.SelectedIndex <= 2 Then
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtPoDetails)
                DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                DVZRBADetails.Sort = "POnO ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            Else
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtPoDetails)
                DVZRBADetails.Sort = "POnO ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            End If
            dgPurchase.DataSource = dt
            dgPurchase.DataBind()
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllEmpDeatils")
        End Try
    End Function
    Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Try
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objPo.LoadPurchaseOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/PurchaseMaster/rptPurchaseOrderMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Purchase Order Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=PurchaseOrderMaster" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Protected Sub dgPurchase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchase.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgPurchase.Columns(0).Visible = True : dgPurchase.Columns(8).Visible = False : dgPurchase.Columns(9).Visible = True : imgbtnEdit.Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.Visible = False
                    imgbtnStatus.ToolTip = "De-Activate"
                    If sEMPAD = "YES" Then
                        dgPurchase.Columns(8).Visible = True
                        imgbtnStatus.Visible = True
                        imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png"
                        imgbtnEdit.Visible = True
                    End If
                    'If sEMPSave = "YES" Then
                    '    dgPurchase.Columns(9).Visible = True
                    '    imgbtnStatus.Visible = True
                    '    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png"
                    '    imgbtnEdit.Visible = True
                    'End If

                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.Visible = False
                    If sEMPAD = "YES" Then
                        dgPurchase.Columns(8).Visible = True
                        imgbtnStatus.Visible = True
                        imgbtnStatus.ImageUrl = "~/Images/Activate16.png"
                        imgbtnStatus.ToolTip = "Activate"
                    End If
                End If

                'If ddlStatus.SelectedIndex = 2 Then
                '    imgbtnStatus.Visible = True
                '    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Activate"
                'End If
                'If ddlStatus.SelectedIndex = 3 Then
                '    imgbtnStatus.Visible = True
                '    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                'End If
                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.Visible = False
                    'imgbtnWaiting.Visible = False
                    If sEMPAP = "YES" Then
                        dgPurchase.Columns(8).Visible = True
                        'imgbtnWaiting.Visible = True
                        imgbtnStatus.Visible = True
                        imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png"
                        imgbtnStatus.ToolTip = "Waiting for Approval"
                    End If
                    'If sEMPSave = "YES" Then
                    '    dgPurchase.Columns(9).Visible = True
                    '    imgbtnEdit.Visible = True
                    'End If
                End If
                If ddlStatus.SelectedIndex = 3 Then
                    dgPurchase.Columns(9).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowDataBound")
        End Try
    End Sub
    Protected Sub dgPurchase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPurchase.RowCommand
        Dim lblOid As New Label
        Dim oMasterID As Object
        Dim oEmpID As Object, oStatusID As Object
        Dim dt As New DataTable
        Dim dgvDetails As New DataView(dtPoDetails)
        Try
            lblError.Text = ""

            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblOid = DirectCast(clickedRow.FindControl("lblOiD"), Label)
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    If objclsCheckMasterIsInUse.CheckPurchaseOrderInUse(sSession.AccessCode, sSession.AccessCodeID, lblOid.Text) = True Then
                        lblEmpMasterValidationMsg.Text = "Already tag to some User, can't be De-Activate" : lblError.Text = "Already tag to some User, can't be De-Activate"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                        Exit Sub
                    End If
                    objPo.UpdatePurchaseMasterStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblOid.Text, sSession.IPAddress, "DeActivated")
                    dgvDetails.Sort = "PoID"
                    Dim iIndex As Integer = dgvDetails.Find(lblOid.Text)
                    dgvDetails(iIndex)("Status") = "De-Activated"
                    dtPoDetails = dgvDetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "De-Activated", lblOid.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objPo.UpdatePurchaseMasterStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblOid.Text, sSession.IPAddress, "A")
                    dgvDetails.Sort = "PoID"
                    Dim iIndex As Integer = dgvDetails.Find(lblOid.Text)
                    dgvDetails(iIndex)("Status") = "Activated"
                    dtPoDetails = dgvDetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Activated", lblOid.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If

                If ddlStatus.SelectedIndex = 2 Then 'Waiting for Approval
                    objPo.UpdatePurchaseMasterStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblOid.Text, sSession.IPAddress, "Created")
                    dgvDetails.Sort = "PoID"
                    Dim iIndex As Integer = dgvDetails.Find(lblOid.Text)
                    dgvDetails(iIndex)("Status") = "Activated"
                    dtPoDetails = dgvDetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If

                LoadAllEmpDeatils()
                ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            End If
            If e.CommandName.Equals("EditRow") Then
                'oMasterID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblOid.Text)))
                Response.Redirect(String.Format("~/Purchase/PurchaseOrder.aspx?AID={0}&sStrID={1}", lblOid.Text, 1), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowCommand")
        End Try
    End Sub
    Protected Sub dgPurchase_PreRender(sender As Object, e As EventArgs) Handles dgPurchase.PreRender
        Dim dt As New DataTable
        Try
            If dgPurchase.Rows.Count > 0 Then
                dgPurchase.UseAccessibleHeader = True
                dgPurchase.HeaderRow.TableSection = TableRowSection.TableHeader
                dgPurchase.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_PreRender")
        End Try
    End Sub
    Protected Sub dgPurchase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgPurchase.SelectedIndexChanged

    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objPo.LoadPurchaseOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/PurchaseMaster/rptPurchaseOrderMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Purchase Order Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=PurchaseOrderMaster" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblOiD As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If dgPurchase.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblEmpMasterValidationMsg.Text = "Select Name to Activate." : lblError.Text = "Select Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                lblOiD = dgPurchase.Rows(i).FindControl("lblOiD")
                If chkSelect.Checked = True Then
                    objPo.UpdatePurchaseMasterStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblOiD.Text, sSession.IPAddress, "A")

                    'LoadAllEmpDeatils()

                End If

            Next
            dtPoDetails = objPo.LoadAllPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID)
            dgPurchase.DataSource = dtPoDetails
            dgPurchase.DataBind()
            ddlStatus_SelectedIndexChanged(sender, e)
            lblEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblOiD As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If dgPurchase.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblEmpMasterValidationMsg.Text = "Select Name to De-Activate." : lblError.Text = "Select Name to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                lblOiD = dgPurchase.Rows(i).FindControl("lblOiD")

                If chkSelect.Checked = True Then
                    If objclsCheckMasterIsInUse.CheckPurchaseOrderInUse(sSession.AccessCode, sSession.AccessCodeID, lblOiD.Text) = True Then
                        'lblEmpMasterValidationMsg.Text = "Already tag to some User, can't be De-Activate" : lblError.Text = "Already tag to some User, can't be De-Activate"
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                        'Exit Sub
                    Else
                        objPo.UpdatePurchaseMasterStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblOiD.Text, sSession.IPAddress, "DeActivated")

                        'LoadAllEmpDeatils()

                    End If
                End If
            Next
            dtPoDetails = objPo.LoadAllPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID)
            dgPurchase.DataSource = dtPoDetails
            dgPurchase.DataBind()
            ddlStatus_SelectedIndexChanged(sender, e)
            lblEmpMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblOiD As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If dgPurchase.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblEmpMasterValidationMsg.Text = "Select Name to Approve." : lblError.Text = "Select Name to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                lblOiD = dgPurchase.Rows(i).FindControl("lblOiD")
                If chkSelect.Checked = True Then
                    objPo.UpdatePurchaseMasterStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblOiD.Text, sSession.IPAddress, "Created")

                    'LoadAllEmpDeatils()

                End If

            Next
            dtPoDetails = objPo.LoadAllPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID)
            dgPurchase.DataSource = dtPoDetails
            dgPurchase.DataBind()
            ddlStatus_SelectedIndexChanged(sender, e)
            lblEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
End Class
