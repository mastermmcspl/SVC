Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Sales_DispatchDetailsMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PurchaseSalesJE"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objDDM As New ClsDispatchDetailsMaster
    Private objclsModulePermission As New clsModulePermission
    Private Shared sDDMSave As String
    Dim objclsFASPermission As New clsFASPermission
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "INVF")
                imgbtnReport.Visible = False : sDDMSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sDDMSave = "YES"
                    End If
                End If
                'imgbtnAdd.Visible = False : imgbtnReport.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasDDM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
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
        Dim dt, dt1 As New DataTable
        Try
            dt = objDDM.LoadSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgDD.DataSource = dt
            dgDD.DataBind()

            dt1 = objDDM.LoadCashSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgCash.DataSource = dt1
            dgCash.DataBind()
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
    Private Sub dgDD_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgDD.RowCommand
        Dim oMasterID As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/Sales/DispatchDetails.aspx?DID={0}", oMasterID), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDD_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/Sales/DispatchDetails.aspx?DID={0}", ""), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub dgDD_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgDD.RowEditing

    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgDD.Rows.Count - 1
                    chkField = dgDD.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgDD.Rows.Count - 1
                    chkField = dgDD.Rows(iIndx).FindControl("chkSelect")
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
            lblError.Text = ""
            dt = objDDM.LoadSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/SalesMaster/RPTDispatchDetailsMaster.rdlc")

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType

            Response.AddHeader("content-disposition", "attachment; filename=DispatchDetailsMaster" + ".xls")
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
            dt = objDDM.LoadSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/SalesMaster/RPTDispatchDetailsMaster.rdlc")

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType

            Response.AddHeader("content-disposition", "attachment; filename=DispatchDetailsMaster" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Private Sub dgCash_PreRender(sender As Object, e As EventArgs) Handles dgCash.PreRender
        Dim dt As New DataTable
        Try
            If dgCash.Rows.Count > 0 Then
                dgCash.UseAccessibleHeader = True
                dgCash.HeaderRow.TableSection = TableRowSection.TableHeader
                dgCash.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCash_PreRender")
        End Try
    End Sub
    Private Sub dgCash_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgCash.RowCommand
        Dim oMasterID As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/Sales/DispatchDetails.aspx?DID={0}", oMasterID), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCash_RowCommand")
        End Try
    End Sub

    Private Sub dgCash_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgCash.RowEditing

    End Sub

    Private Sub dgDD_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDD.RowDataBound
        'Try
        '    dgDD.Columns(6).Visible = False
        '    If sDDMSave = "YES" Then
        '        dgDD.Columns(6).Visible = True
        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDD_RowDataBound")
        'End Try
    End Sub
End Class
