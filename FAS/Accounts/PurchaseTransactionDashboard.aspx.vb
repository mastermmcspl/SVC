Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_PurchaseTransactionDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PurchaseTransactionDashboard"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Dim objPurchase As New clsPurchaseVoucher
    Private Shared sPTAoD As String
    Private Shared sPTAP As String
    Private Shared sPTED As String
    Dim dt As New DataTable
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PT")
                imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : sPTAoD = "NO" : sPTAP = "NO" : sPTED = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        'imgbtnDeActivate.Visible = True
                        sPTAoD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sPTAP = "YES"
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sPTED = "YES"
                    End If
                End If
                BindStatus()

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If

                'imgbtnAdd.Visible = True : imgbtnReport.Visible = True
                ddlStatus_SelectedIndexChanged(sender, e)
                BindPurchaseVoucherDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
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
    Public Sub BindPurchaseVoucherDetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer, ByVal IYearID As Integer)
        Try
            If ddlStatus.SelectedIndex = 0 Then
                If sPTAoD = "YES" Then
                    'imgbtnDeActivate.Visible = True
                Else
                    imgbtnDeActivate.Visible = False
                End If
                imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 1 Then
                If sPTAoD = "YES" Then
                    imgbtnActivate.Visible = True
                Else
                    imgbtnActivate.Visible = False
                End If
                imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                If sPTAP = "YES" Then
                    imgbtnWaiting.Visible = True
                Else
                    imgbtnWaiting.Visible = False
                End If
                imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If
            dt = objPurchase.LoadPurchaseVoucher(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus)
            dgPurchase.DataSource = dt
            dgPurchase.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = True
            'imgbtnReport.Visible = True
            If ddlStatus.SelectedIndex = 0 Then
                If sPTAoD = "YES" Then
                    'imgbtnDeActivate.Visible = True
                Else
                    imgbtnDeActivate.Visible = False
                End If
                imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 1 Then
                If sPTAoD = "YES" Then
                    imgbtnActivate.Visible = True
                Else
                    imgbtnActivate.Visible = False
                End If
                imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                If sPTAP = "YES" Then
                    imgbtnWaiting.Visible = True
                Else
                    imgbtnWaiting.Visible = False
                End If
                imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If
            BindPurchaseVoucherDetails(0, ddlStatus.SelectedIndex, sSession.YearID)

            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            'lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgPurchase.Rows.Count - 1
                    chkField = dgPurchase.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgPurchase.Rows.Count - 1
                    chkField = dgPurchase.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub

    Private Sub dgPurchase_PreRender(sender As Object, e As EventArgs) Handles dgPurchase.PreRender
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

    Private Sub dgPurchase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchase.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgPurchase.Columns(0).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    dgPurchase.Columns(9).Visible = False
                    If sPTAoD = "YES" Then
                        ' dgPurchase.Columns(9).Visible = True
                        dgPurchase.Columns(9).Visible = False
                    End If

                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    imgbtnStatus.Visible = True : imgbtnEdit.Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    dgPurchase.Columns(9).Visible = False
                    If sPTAoD = "YES" Then
                        dgPurchase.Columns(9).Visible = True
                    End If

                    imgbtnStatus.Visible = True
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    dgPurchase.Columns(9).Visible = False
                    If sPTAP = "YES" Then
                        dgPurchase.Columns(9).Visible = True
                    End If

                    imgbtnStatus.Visible = True
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.Visible = False : imgbtnEdit.Visible = False
                End If
            End If


            'dgPurchase.Columns(10).Visible = False
            'If sPTED = "YES" Then
            '    dgPurchase.Columns(10).Visible = True
            'End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowDataBound")
        End Try
    End Sub
    Private Sub dgPurchase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPurchase.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Try
            ' lblError.Text = ""       
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
                Response.Redirect(String.Format("~/Accounts/PurchaseTransaction.aspx?StatusID={0}&MasterID={1}&MasterName={2}", oStatusID, oMasterID, oMasterName), False) 'GeneralMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objPurchase.UpdatePurchaseVoucherStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objPurchase.UpdatePurchaseVoucherStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    Dim sStr As String = ""
                    sStr = objPurchase.GetStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID)
                    If sStr = "S" Then
                        objPurchase.UpdatePurchaseVoucherStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                        'objPurchase.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress)
                        objPurchase.WriteGLTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID)
                        lblError.Text = "Successfully Approved."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                    Else
                        lblError.Text = "This Transaction is not Submitted."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This Transaction is not Submitted','', 'success');", True)
                        Exit Sub
                    End If
                End If
                ddlStatus.SelectedIndex = 0
                BindPurchaseVoucherDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object
        Dim oMasterName As String = ""
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
            Session("dtPurchase") = Nothing
            Response.Redirect(String.Format("~/Accounts/PurchaseTransaction.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgPurchase.Rows.Count = 0 Then
                lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
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
                lblError.Text = "Select to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate','', 'info');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                lblDescID = dgPurchase.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objPurchase.UpdatePurchaseVoucherStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)

                    'DVdt.Sort = "Acc_Purchase_ID"
                    'Dim iIndex As Integer = DVdt.Find(lblDescID.Text)
                    'DVdt(iIndex)("ACC_Purchase_delflag") = "Activated"
                    'dt = DVdt.ToTable

                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            BindPurchaseVoucherDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgPurchase.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
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
                lblError.Text = "Select to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to De-Activate.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                lblDescID = dgPurchase.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then

                    objPurchase.UpdatePurchaseVoucherStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)

                    'DVdt.Sort = "ID"
                    'Dim iIndex As Integer = DVdt.Find(lblDescID.Text)
                    'DVdt(iIndex)("ACC_Purchase_delflag") = "De-Activated"
                    'dt = DVdt.ToTable

                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)

                End If
            Next
            ddlStatus.SelectedIndex = 1
            BindPurchaseVoucherDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgPurchase.Rows.Count = 0 Then
                lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
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
                lblError.Text = "Select to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgPurchase.Rows.Count - 1
                chkSelect = dgPurchase.Rows(i).FindControl("chkSelect")
                lblDescID = dgPurchase.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then

                    objPurchase.UpdatePurchaseVoucherStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    objPurchase.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.YearID, sSession.UserID, sSession.IPAddress)
                    'DVdt.Sort = "ID"
                    'Dim iIndex As Integer = DVdt.Find(lblDescID.Text)
                    'DVdt(iIndex)("ACC_Purchase_delflag") = "Activated"
                    'dt = DVdt.ToTable

                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'info');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            BindPurchaseVoucherDetails(0, ddlStatus.SelectedIndex, sSession.YearID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub dgPurchase_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgPurchase.RowEditing

    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objPurchase.LoadPurchaseVoucher(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                'lblEmpMasterValidationMsg.Text = "No Data."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptPurchaseTransaction.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Purchase Transaction", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=PurchaseTransaction" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objPurchase.LoadPurchaseVoucher(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                'lblEmpMasterValidationMsg.Text = "No Data." : 
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptPurchaseTransaction.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Purchase Transaction", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=PurchaseTransaction" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
End Class


