Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class RemoteData_DataCaptureMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "RemoteData/DataCaptureMaster"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private Shared sPSJEAoD As String
    Private Shared sPSJEAP As String
    Private objclsModulePermission As New clsModulePermission
    Dim objDCM As New ClsDataCaptureMaster
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
                BindDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindDetails()
        Dim dt As New DataTable
        Try
            dt = objDCM.LoadMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgJE.DataSource = dt
            dgJE.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
        End Try
    End Sub
    Private Sub dgJE_PreRender(sender As Object, e As EventArgs) Handles dgJE.PreRender
        Dim dt As New DataTable
        Try
            If dgJE.Rows.Count > 0 Then
                dgJE.UseAccessibleHeader = True
                dgJE.HeaderRow.TableSection = TableRowSection.TableHeader
                dgJE.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPayment_PreRender")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            Response.Redirect("~/RemoteData/DataCapture.aspx?")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub dgJE_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgJE.RowCommand
        Dim oMasterID As Object
        Dim lblDescID As New Label, lblCompanyID As New Label, lblTrTypeID As New Label
        Dim sMainMaster As String
        Dim clickedRow As GridViewRow
        Dim lblTrType As New Label
        Try
            lblError.Text = "" : sMainMaster = ""

            If e.CommandName.Equals("Edit") Then
                clickedRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

                lblCompanyID = DirectCast(clickedRow.FindControl("lblCompanyID"), Label)
                lblTrTypeID = DirectCast(clickedRow.FindControl("lblTrTypeID"), Label)

                lblTrType = DirectCast(clickedRow.FindControl("lblTrType"), Label)

                'oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/RemoteData/DataCapture.aspx?BatchID={0}&CustomerID={1}&TrTypeID={2}", lblDescID.Text, lblCompanyID.Text, lblTrTypeID.Text), False) 'GeneralMasterDetails
            ElseIf e.CommandName = "Select" Then   'ElseIf e.CommandName = "MRP" Then

                clickedRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

                lblCompanyID = DirectCast(clickedRow.FindControl("lblCompanyID"), Label)
                lblTrTypeID = DirectCast(clickedRow.FindControl("lblTrTypeID"), Label)

                lblTrType = DirectCast(clickedRow.FindControl("lblTrType"), Label)

                If lblTrType.Text = "Purchase" Or lblTrType.Text = "Cash Purchase" Then
                    GVDetails.DataSource = objDCM.BindPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Sales" Or lblTrType.Text = "Cash Sales" Then
                    GVDetails.DataSource = objDCM.BindSalesDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Payment" Then
                    GVDetails.DataSource = objDCM.BindPaymentDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Receipt" Then
                    GVDetails.DataSource = objDCM.BindReceiptDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Petty Cash" Then
                    GVDetails.DataSource = objDCM.BindPettyCashDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Journal Entry" Then
                    GVDetails.DataSource = objDCM.BindJEDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "GIN" Then
                    GVDetails.DataSource = objDCM.BindGINDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Purchase Return" Then
                    GVDetails.DataSource = objDCM.BindPurchaseReturnDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Sales Dispatch" Then
                    GVDetails.DataSource = objDCM.BindSalesDispatchDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Sales Invoice" Then
                    GVDetails.DataSource = objDCM.BindSalesInvoiceDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()

                ElseIf lblTrType.Text = "Sales Return" Then
                    GVDetails.DataSource = objDCM.BindSalesReturnDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    GVDetails.DataBind()
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub dgJE_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgJE.RowEditing

    End Sub
    Private Sub GVDetails_PreRender(sender As Object, e As EventArgs) Handles GVDetails.PreRender
        Try
            If GVDetails.Rows.Count > 0 Then
                GVDetails.UseAccessibleHeader = True
                GVDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                GVDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVDetails_PreRender")
        End Try
    End Sub
End Class
