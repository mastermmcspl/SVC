Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Imports System.Drawing
Partial Class Reports_AccountsStock
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Reports\AccountsStock.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Private objclsModulePermission As New clsModulePermission
    Dim ObjPReport As New ClsAccountStock
    Dim objDB As New DBHelper
    Private objAccSetting As New clsAccountSetting
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PHR")
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                End If
                '  CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                LoadAccBrnch(0)
                LoadCommodity()
                loadgrid()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        'imgbtnReport.Src = "~/Images/Download24.png"
        'imgbtnBack.ImageUrl = "~/Images/Backward24.png"
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
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub loadgrid()
        Dim dt As New DataTable
        Dim iCommodity, iBranch As Integer
        Try
            If ddlCmdty.SelectedIndex > 0 Then
                iCommodity = ddlCmdty.SelectedValue
            Else
                iCommodity = 0
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranch = ddlAccBrnch.SelectedValue
            Else
                iBranch = 0
            End If
            dt = ObjPReport.loadDetails(sSession.AccessCode, sSession.AccessCodeID, iCommodity, iBranch)
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
        'If e.rows.ItemType = ListItemType.Header Then
        '    e.Item.Font.Bold = True
        '    e.Item.Cells(0).BackColor = ColorTranslator.FromHtml("#87A822") ' e.Item.Cells(0)
        '    e.Item.Cells(1).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(2).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(3).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(4).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(5).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(6).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(7).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(8).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(9).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(10).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(11).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(12).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(13).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.Cells(14).BackColor = ColorTranslator.FromHtml("#87A822")
        '    e.Item.ForeColor = Color.White

        'End If
        'If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
        '    e.Item.Cells(14).Font.Bold = True
        '    e.Item.Cells(14).BackColor = Color.Gray
        '    e.Item.Cells(14).ForeColor = Color.White

        'End If
    End Sub
    Protected Sub dgReport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgReport.SelectedIndexChanged

    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Try
            loadgrid()
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
