Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Drawing
Partial Class Reports_PhysicalReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Reports\PhysicalStockReport.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Private objclsModulePermission As New clsModulePermission
    Dim ObjPReport As New clsPhysicalStockUpdate
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "ISU")
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                End If
                '  CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                LoadCommodity()
                loadgrid()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub dgInward_PreRender(sender As Object, e As EventArgs) Handles dgReport.PreRender
        Dim dt As New DataTable
        Try
            If dgReport.Rows.Count > 0 Then
                dgReport.UseAccessibleHeader = True
                dgReport.HeaderRow.TableSection = TableRowSection.TableHeader
                dgReport.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInward_PreRender")
        End Try
    End Sub

    'Public Sub CheckAuidtPermission(ByVal sNameSpace As String, ByVal iUsrId As Integer)
    '    Dim sbret As String
    '    Dim i As Integer, j As Integer
    '    Dim sArray As String(), sArray1 As String()
    '    Try

    '        'sbret = clsGeneralMaster.CheckUmsPermit(sNameSpace, sSession.AccessCodeID, iUsrId, "FasPSR", "ALL")
    '        If sbret = "False" Or sbret = "" Then
    '            Response.Redirect("../Permissions/MasterPermission.aspx")
    '        Else
    '            sArray = sbret.Split(",")
    '            For i = 0 To sArray.Length - 1
    '                If sArray(i) <> "" Then
    '                    sArray1 = sArray(i).Split(":")
    '                    For j = 0 To sArray1.Length - 1
    '                        Select Case sArray1(0)
    '                            Case "Export"
    '                                btnReport.Enabled = True
    '                        End Select
    '                    Next
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Protected Sub loadgrid()
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCmdty.SelectedValue = "0" Then
                '  lblHead.Text = "Physical Stock as On " & Date.Now.ToString("dd-MM-yyyy")
            Else
                ' lblHead.Text = ddlCmdty.SelectedItem.Text & " Physical Stock as On " & Date.Now.ToString("dd-MM-yyyy")
            End If

            dt = ObjPReport.loadDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCmdty.SelectedValue)
            dgReport.DataSource = dt
            dgReport.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadgrid")
        End Try
    End Sub
    Private Sub LoadCommodity()
        Try
            ddlCmdty.DataSource = ObjPReport.Commodity(sSession.AccessCode, sSession.AccessCodeID)
            ddlCmdty.DataTextField = "Inv_Description"
            ddlCmdty.DataValueField = "Inv_ID"
            ddlCmdty.DataBind()
            ddlCmdty.Items.Insert(0, New ListItem("--- Select Commodity ---", "0"))
        Catch ex As Exception
            'lblErrorUp.Text = ex.Message
            'lblErrorDown.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCommodity")
        End Try
    End Sub
    Private Sub ExportGrid(fileName As String, contentType As String)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", Convert.ToString("attachment;filename=") & fileName)
        Response.Charset = ""
        Response.ContentType = contentType

        Dim sw As New StringWriter()
        Dim HW As New HtmlTextWriter(sw)
        'Panel1.RenderControl(HW)
        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.Close()
        Response.[End]()
    End Sub

    'Protected Sub btnReport_Click(sender As Object, e As ImageClickEventArgs) Handles btnReport.Click
    '    If ddlCmdty.SelectedValue = 0 Then
    '        ExportGrid("StockReport_" & Date.Now.ToString("dd-MM-yyyy") & ".xls", "application/vnd.ms-excel")
    '    Else
    '        ExportGrid("" & ddlCmdty.SelectedItem.Text & "_" & Date.Now.ToString("dd-MM-yyyy") & ".xls", "application/vnd.ms-excel")
    '    End If

    'End Sub
    Protected Sub ddlCmdty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCmdty.SelectedIndexChanged
        loadgrid()
    End Sub
    'Protected Sub dgReport_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgReport.RowDataBound
    '    Try
    '        If e.Item.ItemType = ListItemType.Header Then
    '            e.Item.Font.Bold = True
    '            e.Item.Cells(0).BackColor = ColorTranslator.FromHtml("#87A822") ' e.Item.Cells(0)
    '            e.Item.Cells(1).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(2).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(3).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(4).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(5).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(6).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(7).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(8).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(9).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(10).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(11).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(12).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(13).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.Cells(14).BackColor = ColorTranslator.FromHtml("#87A822")
    '            e.Item.ForeColor = Color.White

    '        End If
    '        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
    '            e.Item.Cells(14).Font.Bold = True
    '            e.Item.Cells(14).BackColor = Color.Gray
    '            e.Item.Cells(14).ForeColor = Color.White

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Protected Sub dgReport_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgReport.RowDataBound
        Dim lblQty As New TextBox
        Dim lblSlQty As New Label
        Dim btnUpdate As New Button
        Dim slID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblQty = CType(e.Row.FindControl("txtTotal"), TextBox)
                lblSlQty = CType(e.Row.FindControl("txtslQty"), Label)
                btnUpdate = CType(e.Row.FindControl("btnUpdate"), Button)
                If lblSlQty.Text.Trim = "" Or lblSlQty.Text.Trim = "&nbsp:" Or lblSlQty.Text.Trim = "0.000" Then
                    lblQty.Enabled = True
                    btnUpdate.Enabled = True
                Else
                    lblQty.Enabled = False
                    btnUpdate.Enabled = False
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgReport_RowDataBound")
        End Try
    End Sub
    Protected Sub dgReport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgReport.SelectedIndexChanged

    End Sub
    Protected Sub dgReport_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgReport.RowCommand
        Dim lblID As New Label
        Dim lblQty As New TextBox
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
            If e.CommandName.Equals("Update1") Then
                lblID = DirectCast(clickedRow.FindControl("lblslID"), Label)
                lblQty = DirectCast(clickedRow.FindControl("txtTotal"), TextBox)
                ObjPReport.UpdateStock(sSession.AccessCode, sSession.AccessCodeID, lblID.Text, lblQty.Text)
                loadgrid()
                lblError.Text = "Updated Successfully"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgReport_RowCommand")
        End Try
    End Sub
    Protected Sub dgReport_RowUpdated(sender As Object, e As GridViewUpdatedEventArgs) Handles dgReport.RowUpdated

    End Sub
End Class
