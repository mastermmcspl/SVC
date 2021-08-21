Imports System
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Diagnostics
Imports System.Net.Dns
Imports System.Security.Cryptography
Imports BusinesLayer
Public Class CustomerMaster_CustomerMaster
    Inherits System.Web.UI.Page
    Public Shared iUserID As Integer
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
                gvRequestDetails.DataSource = objclsCustomerDetails.LoadAllCustomerDetails(0)
                gvRequestDetails.DataBind()
            End If
        Catch ex As Exception
            ' Throw
        End Try
    End Sub

    Protected Sub gvRequestDetails_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvRequestDetails.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim imgbtnView As ImageButton = CType(e.Row.FindControl("imgbtnView"), ImageButton)
                imgbtnView.ImageUrl = "~/Images/View16.png"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub gvRequestDetails_PreRender(sender As Object, e As EventArgs) Handles gvRequestDetails.PreRender
        Try
            gvRequestDetails.UseAccessibleHeader = True
            gvRequestDetails.HeaderRow.TableSection = TableRowSection.TableHeader
            gvRequestDetails.FooterRow.TableSection = TableRowSection.TableFooter
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub gvRequestDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRequestDetails.RowCommand
        Dim lblIDPKID As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblIDPKID = DirectCast(clickedRow.FindControl("lblIDPKID"), Label)
            'Response.Redirect(String.Format("CustomerDetails.aspx?CustID=" & lblIDPKID.Text & "&UserID=" & iUserID & ""), False)
            Response.Redirect(String.Format("~/CustomerMaster/CustomerDetails.aspx?CustID=" & lblIDPKID.Text), False)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub ddlPDProductInterest_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPDProductInterest.SelectedIndexChanged
        Try
            If ddlPDProductInterest.SelectedIndex > 0 Then
                gvRequestDetails.DataSource = objclsCustomerDetails.LoadAllCustomerDetails(ddlPDProductInterest.SelectedValue)
            Else
                gvRequestDetails.DataSource = objclsCustomerDetails.LoadAllCustomerDetails(0)
            End If
            gvRequestDetails.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            Response.Redirect("~/CustomerMaster/CustomerDetails.aspx", False)
        Catch ex As Exception

        End Try
    End Sub
End Class