
Imports System
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Diagnostics
Imports System.Net.Dns
Imports System.Security.Cryptography
Imports BusinesLayer
Public Class CustomerMaster_ServerDetails
    Inherits System.Web.UI.Page
    Private objclsCustomerDetails As New clsCustomerDetails
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                gvServerDetails.DataSource = objclsCustomerDetails.LoadAllserverDetails()
                gvServerDetails.DataBind()
            End If
        Catch ex As Exception
            ' Throw
        End Try
    End Sub
    Protected Sub gvServerDetails_PreRender(sender As Object, e As EventArgs) Handles gvServerDetails.PreRender
        Try
            gvServerDetails.UseAccessibleHeader = True
            gvServerDetails.HeaderRow.TableSection = TableRowSection.TableHeader
            gvServerDetails.FooterRow.TableSection = TableRowSection.TableFooter
        Catch ex As Exception
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            Response.Redirect("~/CustomerMaster/serverDB.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvServerDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvServerDetails.RowCommand
        Dim oAccID As Object
        Dim objGen As New clsFASGeneral
        Dim linkbtn As LinkButton
        Try
            If e.CommandName = "FAS" Then
                Dim clickedItem As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                linkbtn = DirectCast(clickedItem.FindControl("SD_AccessCode"), LinkButton)

                oAccID = HttpUtility.UrlEncode(objGen.EncryptQueryString(linkbtn.Text))
                Response.Redirect(String.Format("~/Loginpage.aspx?AccID={0}", oAccID), False) 'GeneralMasterDetails
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class