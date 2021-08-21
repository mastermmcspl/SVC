Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_BillDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PurchaseSalesJE"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objclsFASPermission As New clsFASPermission
    Private objclsModulePermission As New clsModulePermission
    Dim objBDM As New clsBillDashboard
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
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "BD")
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If
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
            dt = objBDM.LoadRegistryOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgBD.DataSource = dt
            dgBD.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails")
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
    Private Sub dgBD_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgBD.RowCommand
        Dim lblDescID As New Label, lblDescName As New Label, lblPOID As New Label, lblGNID As New Label, lblINID As New Label
        Dim sMainMaster As String
        Dim dt As New DataTable
        Dim iInvoiceNo As Integer = 0
        Try
            lblError.Text = "" : sMainMaster = ""
            'If e.CommandName.Equals("Edit") Then
            '    Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            '    lblDescID = DirectCast(clickedRow.FindControl("lblID"), Label)
            '    oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
            '    Response.Redirect(String.Format("~/Purchase/PurchaseInvoiceEntry.aspx?AID={0}&sStrID={1}", oMasterID, 1), False)
            'End If
            If e.CommandName.Equals("ShowPO") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblPOID = DirectCast(clickedRow.FindControl("lblPOID"), Label)
                dt = objBDM.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, lblPOID.Text)
                dgPurchase.DataSource = dt
                dgPurchase.DataBind()
                dgInward.DataSource = Nothing
                dgInward.DataBind()
                dgViewPI.DataSource = Nothing
                dgViewPI.DataBind()
            End If
            If e.CommandName.Equals("ShowGIN") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblGNID = DirectCast(clickedRow.FindControl("lblGNID"), Label)
                dt = objBDM.LoadInwardDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblGNID.Text)
                dgInward.DataSource = dt
                dgInward.DataBind()
                dgPurchase.DataSource = Nothing
                dgPurchase.DataBind()
                dgViewPI.DataSource = Nothing
                dgViewPI.DataBind()
            End If
            If e.CommandName.Equals("ShowIN") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblINID = DirectCast(clickedRow.FindControl("lblINID"), Label)
                If lblINID.Text <> "" Then
                    iInvoiceNo = lblINID.Text
                Else
                    lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                    dgViewPI.DataSource = Nothing
                    dgViewPI.DataBind()
                    dgPurchase.DataSource = Nothing
                    dgPurchase.DataBind()
                    dgInward.DataSource = Nothing
                    dgInward.DataBind()
                    Exit Sub
                End If
                dt = objBDM.BindInvoiceDetails(sSession.AccessCode, sSession.AccessCodeID, iInvoiceNo)
                dgViewPI.DataSource = dt
                dgViewPI.DataBind()
                dgPurchase.DataSource = Nothing
                dgPurchase.DataBind()
                dgInward.DataSource = Nothing
                dgInward.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgBD_RowCommand")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = objBDM.LoadRegistryOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RPTBillDashboard.rdlc")

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType

            Response.AddHeader("content-disposition", "attachment; filename=BillDashboard" + ".pdf")
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
            lblError.Text = ""
            dt = objBDM.LoadRegistryOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RPTBillDashboard.rdlc")

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType

            Response.AddHeader("content-disposition", "attachment; filename=BillDashboard" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Protected Sub dgPurchase_PreRender(sender As Object, e As EventArgs) Handles dgPurchase.PreRender
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
    Protected Sub dgInward_PreRender(sender As Object, e As EventArgs) Handles dgInward.PreRender
        Try
            If dgInward.Rows.Count > 0 Then
                dgInward.UseAccessibleHeader = True
                dgInward.HeaderRow.TableSection = TableRowSection.TableHeader
                dgInward.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInward_PreRender")
        End Try
    End Sub
    Protected Sub dgViewPI_PreRender(sender As Object, e As EventArgs) Handles dgViewPI.PreRender
        Try
            If dgViewPI.Rows.Count > 0 Then
                dgViewPI.UseAccessibleHeader = True
                dgViewPI.HeaderRow.TableSection = TableRowSection.TableHeader
                dgViewPI.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgViewPI_PreRender")
        End Try
    End Sub
End Class
