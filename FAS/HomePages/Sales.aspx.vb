Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class HomePages_Sales
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PurchaseSalesJE"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objclsFASPermission As New clsFASPermission
    Dim objDDM As New clsSalesDashboard
    Private Shared lblPOID As New Label
    Private Shared lblGNID As New Label
    Private Shared lblINID As New Label
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnAdd.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FASPRNM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",ADD,") = True Then
                '        imgbtnAdd.Visible = True
                '    End If
                'End If
                BindDetails(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindDetails(ByVal iPageIndex As Integer)
        Dim dt As New DataTable
        Try
            dt = objDDM.LoadRegistryOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgDD.DataSource = dt
            dgDD.DataBind()

            dt = objDDM.LoadBillRegistryOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgBD.DataSource = dt
            dgBD.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
        End Try
    End Sub
    Private Sub dgDD_PreRender(sender As Object, e As EventArgs) Handles dgDD.PreRender
        Dim dt As New DataTable
        Try
            If dgDD.Rows.Count > 0 Then
                dgDD.UseAccessibleHeader = True
                dgDD.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDD.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDD_PreRender")
        End Try
    End Sub
    Private Sub dgBD_PreRender(sender As Object, e As EventArgs) Handles dgBD.PreRender
        Dim dt As New DataTable
        Try
            If dgBD.Rows.Count > 0 Then
                dgBD.UseAccessibleHeader = True
                dgBD.HeaderRow.TableSection = TableRowSection.TableHeader
                dgBD.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgBD_PreRender")
        End Try
    End Sub
End Class
