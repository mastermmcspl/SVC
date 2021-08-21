Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class FixedAsset_AssetRegister
    Inherits System.Web.UI.Page

    Private sFormName As String = "AssetRegister"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Dim objAstReg As New ClsAssetRegister
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub pageload(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                loadAssetType()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "pageload")
        End Try
    End Sub

    Public Sub BindAssetRegister(ByVal AFAM_AssetType As String)
        Dim dt As New DataTable

        Try
            dt = objAstReg.LoadAssetRegister(sSession.AccessCode, sSession.AccessCodeID, ddlpAstype.SelectedValue, sSession.YearID)
            dgRegister.DataSource = dt
            dgRegister.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAssetRegister")
        End Try
    End Sub
    Public Sub loadAssetType()
        Dim dt As New DataTable
        Try
            dt = objAstReg.loadAssetType(sSession.AccessCode, sSession.AccessCodeID)
            ddlpAstype.DataTextField = "GL_Desc"
            ddlpAstype.DataValueField = "GL_ID"
            ddlpAstype.DataSource = dt
            ddlpAstype.DataBind()
            ddlpAstype.Items.Insert(0, "SelectAssetType")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType")
        End Try
    End Sub

    Private Sub ddlpAstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlpAstype.SelectedIndexChanged
        Dim var As Integer = 0
        Try

            If ddlpAstype.SelectedIndex > 0 Then
                BindAssetRegister(ddlpAstype.SelectedValue)
            Else
                dgRegister.DataSource = Nothing
                dgRegister.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlpAstype_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub dgRegister_PreRender(sender As Object, e As EventArgs) Handles dgRegister.PreRender
        Try
            If dgRegister.Rows.Count > 0 Then
                dgRegister.UseAccessibleHeader = True
                dgRegister.HeaderRow.TableSection = TableRowSection.TableHeader
                dgRegister.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgRegister_PreRender")
        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = objAstReg.LoadAssetRegister(sSession.AccessCode, sSession.AccessCodeID, ddlpAstype.SelectedValue, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("assetRegDataSet", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetRegReport.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "fixedasset", "AssetRegReport", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=AssetRegister" + ".xls")
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
            lblError.Text = ""
            dt = objAstReg.LoadAssetRegister(sSession.AccessCode, sSession.AccessCodeID, ddlpAstype.SelectedValue, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("assetRegDataSet", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetRegReport.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "fixedasset", "AssetRegReport", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=AssetRegister" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub

    Private Sub dgRegister_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgRegister.RowCommand
        Dim ID As Object
        Dim AssetID As Object
        Dim lblID, lblAssetID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
            lblAssetID = DirectCast(clickedRow.FindControl("lblAssetID"), Label)

            If e.CommandName.Equals("EditFREG") Then
                ID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblID.Text)))
                AssetID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblAssetID.Text)))
                Response.Redirect(String.Format("~/FixedAsset/AssetMaster.aspx?AFAM_ID={0}&AFAM_AssetType={1}", ID, AssetID), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgRegister_RowCommand")
        End Try
    End Sub

    Private Sub dgRegister_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgRegister.RowDataBound
        Dim imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgRegister_RowDataBound")
        End Try
    End Sub

End Class
