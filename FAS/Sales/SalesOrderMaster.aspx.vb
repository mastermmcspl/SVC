Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Sales_SalesOrderMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PurchaseSalesJE"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objSO As New ClsSalesOrderMaster
    Dim objclsFASPermission As New clsFASPermission
    Private Shared sSOSave As String
    Private objclsModulePermission As New clsModulePermission
    Dim objclsGeneralFunctions As New clsGeneralFunctions
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
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SO")
                imgbtnReport.Visible = False : sSOSave = "NO"
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
                        sSOSave = "YES"
                    End If
                End If
                'imgbtnAdd.Visible = False : imgbtnReport.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FASSOM", 1)
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

                BindSODetails(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindSODetails(ByVal iPageIndex As Integer)
        Dim dt As New DataTable
        Try
            dt = objSO.LoadSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgSO.DataSource = dt
            dgSO.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSODetails")
        End Try
    End Sub
    Private Sub dgSO_PreRender(sender As Object, e As EventArgs) Handles dgSO.PreRender
        Dim dt As New DataTable
        Try
            If dgSO.Rows.Count > 0 Then
                dgSO.UseAccessibleHeader = True
                dgSO.HeaderRow.TableSection = TableRowSection.TableHeader
                dgSO.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSO_PreRender")
        End Try
    End Sub
    Private Sub dgSO_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgSO.RowCommand
        Dim oMasterID As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblID"), Label)

            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/Sales/SalesOrder.aspx?SOID={0}", oMasterID), False) 'GeneralMasterDetails
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSO_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/Sales/SalesOrder.aspx?SOID={0}", ""), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub dgSO_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgSO.RowEditing

    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgSO.Rows.Count - 1
                    chkField = dgSO.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgSO.Rows.Count - 1
                    chkField = dgSO.Rows(iIndx).FindControl("chkSelect")
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
            dt = objSO.LoadSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/SalesMaster/RPTSalesOrderMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Sales Order Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=SalesOrderMaster" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            'HttpContext.Current.Response.Flush() 'Sends all currently buffered output To the client.
            'HttpContext.Current.Response.SuppressContent = True 'Gets Or sets a value indicating whether To send HTTP content To the client.
            'HttpContext.Current.ApplicationInstance.CompleteRequest() 'Causes ASP.NET To bypass all events And filtering In the HTTP pipeline chain Of execution And directly execute the EndRequest Event.
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objSO.LoadSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/SalesMaster/RPTSalesOrderMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Sales Order Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=SalesOrderMaster" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            'HttpContext.Current.Response.Flush() 'Sends all currently buffered output To the client.
            'HttpContext.Current.Response.SuppressContent = True 'Gets Or sets a value indicating whether To send HTTP content To the client.
            'HttpContext.Current.ApplicationInstance.CompleteRequest() 'Causes ASP.NET To bypass all events And filtering In the HTTP pipeline chain Of execution And directly execute the EndRequest Event.
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub

    Private Sub dgSO_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgSO.RowDataBound
        'Try
        '    dgSO.Columns(6).Visible = False
        '    If sSOSave = "YES" Then
        '        dgSO.Columns(6).Visible = True
        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSO_RowDataBound")
        'End Try
    End Sub
    'Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
    '    Dim mimeType As String = Nothing
    '    Dim dt As New DataTable
    '    Try
    '        lblError.Text = ""
    '        dt = objSO.LoadSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
    '        If dt.Rows.Count = 0 Then
    '            lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
    '            Exit Sub
    '        End If
    '        ReportViewer1.Reset()
    '        Dim rds As New ReportDataSource("DataSet1", dt)
    '        ReportViewer1.LocalReport.DataSources.Add(rds)
    '        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/SalesMaster/RPTSalesOrderMaster.rdlc")

    '        ReportViewer1.LocalReport.Refresh()
    '        Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
    '        Response.Buffer = True
    '        Response.Clear()
    '        Response.ContentType = mimeType

    '        Response.AddHeader("content-disposition", "attachment; filename=SalesOrderMaster" + ".xls")
    '        Response.BinaryWrite(RptViewer)
    '        Response.Flush()
    '        Response.End()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
    '    End Try
    'End Sub
    'Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
    '    Dim mimeType As String = Nothing
    '    Dim dt As New DataTable
    '    Try
    '        lblError.Text = ""
    '        dt = objSO.LoadSalesOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
    '        If dt.Rows.Count = 0 Then
    '            lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
    '            Exit Sub
    '        End If
    '        ReportViewer1.Reset()
    '        Dim rds As New ReportDataSource("DataSet1", dt)
    '        ReportViewer1.LocalReport.DataSources.Add(rds)
    '        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/SalesMaster/RPTSalesOrderMaster.rdlc")

    '        ReportViewer1.LocalReport.Refresh()
    '        Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
    '        Response.Buffer = True
    '        Response.Clear()
    '        Response.ContentType = mimeType

    '        Response.AddHeader("content-disposition", "attachment; filename=SalesOrderMaster" + ".pdf")
    '        Response.BinaryWrite(RptViewer)
    '        Response.Flush()
    '        Response.End()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
    '    End Try
    'End Sub
End Class
