Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer

Partial Class Purchase_GoodsReturnMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Purchase_GoodsReturnMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsFASGeneral
    Dim objGMasterreturn As New clsGoodsReturnMaster
    Private objclsCheckMasterIsInUse As New clsCheckMasterIsInUse
    Private objclsFASPermission As New clsFASPermission
    Private Shared sSession As AllSession
    Private Shared sGRSave As String
    Private Shared sEMPAD As String
    Private Shared sEMPBL As String
    Dim objclsModulePermission As New clsModulePermission
    Private Shared dtGrDetails As New DataTable

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

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "GR")
                imgbtnAdd.Visible = True : imgbtnReport.Visible = False : sGRSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sGRSave = "YES"
                    End If
                End If
                BindStatus()

                RFVSearch.ErrorMessage = "Select Search by."
                RFVSearch.InitialValue = "Select"
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                dtGrDetails = objGMasterReturn.LoadAllReturnDetails(sSession.AccessCode, sSession.AccessCodeID)
                dgPurchase.DataSource = dtGrDetails
                dgPurchase.DataBind()
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "Waiting For Approval")
            ddlStatus.Items.Insert(2, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub dgPurchase_PreRender(sender As Object, e As EventArgs) Handles dgPurchase.PreRender
        Dim dt As New DataTable
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

    Protected Sub dgPurchase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchase.RowDataBound
        Dim imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgPurchase.Columns(0).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnEdit.Visible = True
                    imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                    imgbtnEdit.ToolTip = "Edit"
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnEdit.Visible = True
                    imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                    imgbtnEdit.ToolTip = "Edit"
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnEdit.Visible = True
                    imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                    imgbtnEdit.ToolTip = "Edit"
                End If

                dgPurchase.Columns(7).Visible = False
                If sGRSave = "YES" Then
                    dgPurchase.Columns(7).Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowDataBound")
        End Try
    End Sub

    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oEmpID As New Object, oStatusID As New Object
        Try
            lblError.Text = ""
            oEmpID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                'ElseIf ddlStatus.SelectedIndex = 2 Or ddlStatus.SelectedIndex = 3 Then
                '    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(4))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(5))
            End If
            'Response.Redirect(String.Format("~/Purchase/GoodsReturn.aspx?EmpID={0}&StatusID={1}", oEmpID, oStatusID), False) 'Purchase Order Details
            Response.Redirect(String.Format("~/Purchase/GoodsReturn.aspx?EmpID={0}&StatusID={1}", oEmpID, oStatusID), False) 'Purchase Order Details
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Protected Sub dgPurchase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPurchase.RowCommand
        Dim lblOid As New Label
        Dim dt As New DataTable
        Dim dgvDetails As New DataView(dtGrDetails)
        Try
            lblError.Text = ""

            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblOid = DirectCast(clickedRow.FindControl("lblOiD"), Label)
            If e.CommandName.Equals("EditRow") Then
                Response.Redirect(String.Format("~/Purchase/GoodsReturn.aspx?AID={0}&sStrID={1}", lblOid.Text, 1), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowCommand")
        End Try
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            ' LoadAllEmpDeatils(0, "True", "NO")
            LoadAllEmpDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadAllEmpDeatils() As DataTable
        Dim dt As New DataTable
        Dim sSearchText As String = "", sStatus As String = ""
        Try
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"

            ElseIf ddlStatus.SelectedIndex = 1 Then
                sStatus = "Waiting for Approval"
            End If

            If ddlStatus.SelectedIndex <= 1 Then
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtGrDetails)
                DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                DVZRBADetails.Sort = "POnO ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            Else
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtGrDetails)
                DVZRBADetails.Sort = "POnO ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            End If
            dgPurchase.DataSource = dt
            dgPurchase.DataBind()
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllEmpDeatils")
        End Try
    End Function

    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objGMasterreturn.LoadGoodsReturn(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/PurchaseMaster/rptGoodsReturnMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Purchase Order Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=GoodsReturn" + ".pdf")
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
            dt = objGMasterreturn.LoadGoodsReturn(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/PurchaseMaster/rptGoodsReturnMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Purchase Order Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=GoodsReturn" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
End Class
