Imports System
Imports System.Data
Imports BusinesLayer
Partial Class DigitalFilling_NormalScan
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFilling_NormalScan"
    Private Shared sSession As AllSession
    Private objErrorClass As New Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsNormalScan As New clsNormalScan
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                gvScannedImages.DataSource = objclsNormalScan.BindNormalScannedImages(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Server.MapPath("."), "\NormalScan\")
                gvScannedImages.DataBind()
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/DigitalFilling/NormalScanDetails.aspx"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub gvScannedImages_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvScannedImages.RowDataBound
        Dim imgbtnView As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnView = CType(e.Row.FindControl("imgbtnView"), ImageButton)
                imgbtnView.ImageUrl = "~/Images/View16.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvScannedImages_RowDataBound")
        End Try
    End Sub
    Private Sub gvScannedImages_PreRender(sender As Object, e As EventArgs) Handles gvScannedImages.PreRender
        Try
            If gvScannedImages.Rows.Count > 0 Then
                gvScannedImages.UseAccessibleHeader = True
                gvScannedImages.HeaderRow.TableSection = TableRowSection.TableHeader
                gvScannedImages.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvScannedImages_PreRender")
        End Try
    End Sub
    Private Sub gvScannedImages_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvScannedImages.RowEditing
    End Sub
    Private Sub gvScannedImages_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvScannedImages.RowCommand
        Dim lblFolderName As New Label
        Dim oFolderName As New Object
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblFolderName = DirectCast(clickedRow.FindControl("lblFolderName"), Label)

            If e.CommandName.Equals("View") Then
                oFolderName = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(lblFolderName.Text))
                Response.Redirect(String.Format("~/DigitalFilling/NormalScanDetails.aspx?Folder={0}", oFolderName), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvScannedImages_RowCommand")
        End Try
    End Sub
End Class
