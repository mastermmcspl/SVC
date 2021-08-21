Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Masters_AgentsForeignExchange
    Inherits System.Web.UI.Page
    Private Shared sSession As AllSession
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared objclsForeignExchangeAgents As New clsForeignExchangeAgents
    Private sFormName As String = "AgentsForeignExchange"
    Private Shared dtAgencyMaster As DataTable
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
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                ddlStatus_SelectedIndexChanged(sender, e)
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
    Private Function BindAllAgencyDeatils() As DataTable
        Dim dt As New DataTable
        Dim sSearchText As String = "", sStatus As String = "", sFrom As String = "", sTo As String = ""
        Try
            imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 Then
                sStatus = "De-Activated"
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 Then
                sStatus = "Waiting for Approval"
                imgbtnWaiting.Visible = True 'Waiting for Approval         
            End If
            dtAgencyMaster = objclsForeignExchangeAgents.LoadAgentsForeignExchangeDashboard(sSession.AccessCode, sSession.AccessCodeID)
            gvAgencyMaster.DataSource = Nothing
            gvAgencyMaster.DataBind()
            If ddlStatus.SelectedIndex <= 2 Then
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtAgencyMaster)
                DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                DVZRBADetails.Sort = "AgencyName ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            Else
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtAgencyMaster)
                DVZRBADetails.Sort = "AgencyName ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            End If
            gvAgencyMaster.DataSource = dt
            gvAgencyMaster.DataBind()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            Response.Redirect(String.Format("~/Masters/AgentsForeignExchangeDetails.aspx"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblID As New Label
        Dim DVAgencyStatus As New DataView(dtAgencyMaster)
        Try
            lblError.Text = ""
            If gvAgencyMaster.Rows.Count = 0 Then
                lblValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvAgencyMaster.Rows.Count - 1
                chkSelect = gvAgencyMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblValidationMsg.Text = "Select Function to Activate." : lblError.Text = "Select Function to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvAgencyMaster.Rows.Count - 1
                chkSelect = gvAgencyMaster.Rows(i).FindControl("chkSelect")
                lblID = gvAgencyMaster.Rows(i).FindControl("lblID")
                If chkSelect.Checked = True Then
                    objclsForeignExchangeAgents.ApproveAgencyStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "Activated")
                    DVAgencyStatus.Sort = "ID"
                    Dim iIndex As Integer = DVAgencyStatus.Find(lblID.Text)
                    DVAgencyStatus(iIndex)("Status") = "Activated"
                    dtAgencyMaster = DVAgencyStatus.ToTable
                End If
            Next
            BindAllAgencyDeatils()
            lblValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Master", "Activated", lblID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer = 0
        Dim lblID As New Label
        Dim DVAgencyStatus As New DataView(dtAgencyMaster)
        Try
            lblError.Text = ""
            If gvAgencyMaster.Rows.Count = 0 Then
                lblValidationMsg.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvAgencyMaster.Rows.Count - 1
                chkSelect = gvAgencyMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblValidationMsg.Text = "Select Function to De-Activate." : lblError.Text = "Select Function to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvAgencyMaster.Rows.Count - 1
                chkSelect = gvAgencyMaster.Rows(i).FindControl("chkSelect")
                lblID = gvAgencyMaster.Rows(i).FindControl("lblID")
                If chkSelect.Checked = True Then
                    objclsForeignExchangeAgents.ApproveAgencyStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "DeActivated")
                    DVAgencyStatus.Sort = "ID"
                    Dim iIndex As Integer = DVAgencyStatus.Find(lblID.Text)
                    DVAgencyStatus(iIndex)("Status") = "De-Activated"
                    dtAgencyMaster = DVAgencyStatus.ToTable
                End If
            Next
            BindAllAgencyDeatils()
            lblValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Master", "De-Activated", lblID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblID As New Label
        Dim DVAgencyStatus As New DataView(dtAgencyMaster)
        Try
            lblError.Text = ""
            If gvAgencyMaster.Rows.Count = 0 Then
                lblValidationMsg.Text = "No data to Approve." : lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvAgencyMaster.Rows.Count - 1
                chkSelect = gvAgencyMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblValidationMsg.Text = "Select Function to Approve." : lblError.Text = "Select Function to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvAgencyMaster.Rows.Count - 1
                chkSelect = gvAgencyMaster.Rows(i).FindControl("chkSelect")
                lblID = gvAgencyMaster.Rows(i).FindControl("lblID")
                If chkSelect.Checked = True Then
                    objclsForeignExchangeAgents.ApproveAgencyStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "Created")
                    DVAgencyStatus.Sort = "ID"
                    Dim iIndex As Integer = DVAgencyStatus.Find(lblID.Text)
                    DVAgencyStatus(iIndex)("Status") = "Activated"
                    dtAgencyMaster = DVAgencyStatus.ToTable
                End If
            Next
            BindAllAgencyDeatils()
            lblValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Master", "Approved", lblID.Text, "", 0, "", sSession.IPAddress)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub gvAgencyMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAgencyMaster.RowDataBound
        Try
            Dim imgbtnStatus As ImageButton, imgbtnEdit As ImageButton
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png" : imgbtnStatus.ToolTip = "Edit"

                gvAgencyMaster.Columns(8).Visible = False
                gvAgencyMaster.Columns(9).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    gvAgencyMaster.Columns(8).Visible = True
                    gvAgencyMaster.Columns(9).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    gvAgencyMaster.Columns(8).Visible = True
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    gvAgencyMaster.Columns(8).Visible = True
                    gvAgencyMaster.Columns(9).Visible = True
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    gvAgencyMaster.Columns(0).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAgencyMaster_RowDataBound")
        End Try
    End Sub
    Private Sub gvAgencyMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAgencyMaster.RowCommand
        Dim lblID As New Label
        Dim oID As Object
        Dim DVAgecnyADA As New DataView(dtAgencyMaster)
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
            If e.CommandName = "EditRow" Then
                oID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblID.Text)))
                Response.Redirect(String.Format("~/Masters/AgentsForeignExchangeDetails.aspx?ID={0}", oID), False) 'AgentsForeignExchangeDetails
            End If
            If e.CommandName = "Status" Then
                If ddlStatus.SelectedIndex = 0 Then
                    objclsForeignExchangeAgents.ApproveAgencyStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "DeActivated")
                    DVAgecnyADA.Sort = "ID"
                    Dim iIndex As Integer = DVAgecnyADA.Find(lblID.Text)
                    DVAgecnyADA(iIndex)("Status") = "De-Activated"
                    dtAgencyMaster = DVAgecnyADA.ToTable
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Master", "De-Activated", lblID.Text, "", 0, "", sSession.IPAddress)
                    lblValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsForeignExchangeAgents.ApproveAgencyStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "Activated")
                    DVAgecnyADA.Sort = "ID"
                    Dim iIndex As Integer = DVAgecnyADA.Find(lblID.Text)
                    DVAgecnyADA(iIndex)("Status") = "Activated"
                    dtAgencyMaster = DVAgecnyADA.ToTable
                    lblValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Master", "Activated", lblID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objclsForeignExchangeAgents.ApproveAgencyStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblID.Text, sSession.IPAddress, "Created")
                    DVAgecnyADA.Sort = "ID"
                    Dim iIndex As Integer = DVAgecnyADA.Find(lblID.Text)
                    DVAgecnyADA(iIndex)("Status") = "Activated"
                    dtAgencyMaster = DVAgecnyADA.ToTable
                    lblValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Master", "Approved", lblID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                End If
                BindAllAgencyDeatils()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAgencyMaster_RowCommand")
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            BindAllAgencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvAgencyMaster.Rows.Count - 1
                    chkField = gvAgencyMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvAgencyMaster.Rows.Count - 1
                    chkField = gvAgencyMaster.Rows(iIndx).FindControl("chkSelect")
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
            dt = BindAllAgencyDeatils()
            If dt.Rows.Count = 0 Then
                lblValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/AgentsForeignExchange.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=AgentsForeignExchange" + ".pdf")
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
            dt = BindAllAgencyDeatils()
            If dt.Rows.Count = 0 Then
                lblValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/AgentsForeignExchange.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Foriegn Exchange Agents Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=AgentsForeignExchange" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub gvAgencyMaster_PreRender(sender As Object, e As EventArgs) Handles gvAgencyMaster.PreRender
        Try
            If gvAgencyMaster.Rows.Count > 0 Then
                gvAgencyMaster.UseAccessibleHeader = True
                gvAgencyMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAgencyMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAgencyMaster_PreRender")
        End Try
    End Sub
End Class
