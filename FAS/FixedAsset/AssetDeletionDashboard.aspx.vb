Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class FixedAsset_DltnDashBoard
    Inherits System.Web.UI.Page
    Private sFormName As String = "AssetDltnDashboard"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared sAssetID As String
    Private Shared sPTAP As String
    Private Shared sPTED As String
    Dim objAsstTrndel As New ClsAssetTransactionDeletion
    Dim objGen As New clsFASGeneral
    'Dim dtDetails As New DataTable
    Private Shared dtDetails As New DataTable

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If

                dtDetails = objAsstTrndel.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvAsserDetails.DataSource = dtDetails
                GvAsserDetails.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Waiting for Approval")
            ddlStatus.Items.Insert(1, "Deleted")
            ddlStatus.Items.Insert(2, "Transfered/Repair")
            ddlStatus.Items.Insert(3, "Reactivated")
            ddlStatus.Items.Insert(4, "All")
            ddlStatus.SelectedIndex = 4
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                dt = objAsstTrndel.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W")
                GvAsserDetails.DataSource = dt
                GvAsserDetails.DataBind()
            ElseIf ddlStatus.SelectedIndex = 1 Then
                dt = objAsstTrndel.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D")
                GvAsserDetails.DataSource = dt
                GvAsserDetails.DataBind()
            ElseIf ddlStatus.SelectedIndex = 2 Then
                dt = objAsstTrndel.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "TR")
                GvAsserDetails.DataSource = dt
                GvAsserDetails.DataBind()
            ElseIf ddlStatus.SelectedIndex = 3 Then
                dt = objAsstTrndel.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "RS")
                GvAsserDetails.DataSource = dt
                GvAsserDetails.DataBind()
            ElseIf ddlStatus.SelectedIndex = 4 Then
                dt = objAsstTrndel.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvAsserDetails.DataSource = dt
                GvAsserDetails.DataBind()
            End If
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
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
            Session("dtDetails") = Nothing
            Response.Redirect(String.Format("~/FixedAsset/AssetTransactionDeletion.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub GvAsserDetails_PreRender(sender As Object, e As EventArgs) Handles GvAsserDetails.PreRender
        Try
            If GvAsserDetails.Rows.Count > 0 Then
                GvAsserDetails.UseAccessibleHeader = True
                GvAsserDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                GvAsserDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAsserDetails_PreRender")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged1(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To GvAsserDetails.Rows.Count - 1
                    chkField = GvAsserDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To GvAsserDetails.Rows.Count - 1
                    chkField = GvAsserDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged1")
        End Try
    End Sub

    Private Sub GvAsserDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvAsserDetails.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                GvAsserDetails.Columns(0).Visible = True
                imgbtnStatus.ImageUrl = "~/Images/Activate16.png"
                If ddlStatus.SelectedIndex = 0 Then
                    GvAsserDetails.Columns(8).Visible = True
                Else
                    GvAsserDetails.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAsserDetails_RowDataBound")
        End Try
    End Sub

    Private Sub GvAsserDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvAsserDetails.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)
            ' lblDescName = DirectCast(clickedRow.FindControl("lblDescName"), Label)
            If e.CommandName.Equals("Edit1") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                'oMasterName = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescName.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                '  oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/FixedAsset/AssetTransactionDeletion.aspx?MasterID={0}", oMasterID), False)
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    Dim iDeltbl As New DataSet
                    Dim iDelQtyTot As Integer = 0
                    iDeltbl = objAsstTrndel.GetDelId(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    iDelQtyTot = Val(iDeltbl.Tables(0).Rows(0)("AFAD_Quantity").ToString) + Val(iDeltbl.Tables(0).Rows(0)("AFAD_AssetDelQuantity").ToString)
                    If iDeltbl.Tables(0).Rows(0)("AFAD_AssetDelID") = 2 Or iDeltbl.Tables(0).Rows(0)("AFAD_AssetDelID") = 6 Then
                        objAsstTrndel.UpdateDeletionStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "RS", iDelQtyTot)
                    Else
                        objAsstTrndel.UpdateDeletionStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", 0)
                    End If
                    lblError.Text = "Successfully Activated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                    End If
                    ddlStatus.SelectedIndex = 0
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAsserDetails_RowCommand")
        End Try
    End Sub
    Private Sub GvAsserDetails_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GvAsserDetails.RowDeleting

    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To GvAsserDetails.Rows.Count - 1
                    chkField = GvAsserDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To GvAsserDetails.Rows.Count - 1
                    chkField = GvAsserDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objAsstTrndel.GetDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblDltnValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalasserAddnValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetAddDashBoard.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=AssetAdditionDashBoard" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub

    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objAsstTrndel.GetDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblDltnValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalasserAddnValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetAddDashBoard.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=AssetAdditionDashBoard" + ".PDF")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
End Class
