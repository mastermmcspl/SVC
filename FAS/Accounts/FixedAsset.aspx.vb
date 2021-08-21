Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_FixedAsset
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_EmployeeMaster"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objFA As New clsFixedAsset
    Private objclsModulePermission As New clsModulePermission
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FASS")
                imgbtnAdd.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                imgbtnUnBlock.Visible = False : imgbtnUnLock.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        imgbtnActivate.Visible = True
                        imgbtnDeActivate.Visible = True
                        imgbtnUnBlock.Visible = True
                        imgbtnUnLock.Visible = True
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnWaiting.Visible = True
                    End If
                End If

                BindBranch()
                gvFixedAsset.DataSource = objFA.BindFixedAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                gvFixedAsset.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub BindBranch()
        Try
            ddlBranch.DataSource = objFA.LoadBranch(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranch.DataValueField = "CUSTB_ID"
            ddlBranch.DataTextField = "CUSTB_NAME"
            ddlBranch.DataBind()
            ddlBranch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub gvFixedAsset_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFixedAsset.RowDataBound
        Dim FinancialYear As String = ""
        Dim aYear As Array
        Dim r As Integer
        Dim c As Integer
        Try

            If e.Row.RowType = DataControlRowType.Header Then

                FinancialYear = sSession.YearName
                aYear = FinancialYear.Split("-")

                e.Row.Cells(0).Text = "GL"
                e.Row.Cells(1).Text = "glHead"
                e.Row.Cells(2).Text = "Description"
                e.Row.Cells(3).Text = "NoteNo"
                e.Row.Cells(4).Text = "As at" & "</br>" & "01 - 04 - " & aYear(0) & ""
                e.Row.Cells(5).Text = "Additions"
                e.Row.Cells(6).Text = "Deductions/Adjustments"
                e.Row.Cells(7).Text = "As at" & "</br>" & "31-03-" & aYear(1) & ""
                e.Row.Cells(8).Text = "As at" & "</br>" & "01-04-" & aYear(0) & ""
                e.Row.Cells(9).Text = "For the Year"
                e.Row.Cells(10).Text = "Deduction/Adjustments"
                e.Row.Cells(11).Text = "Upto" & "</br>" & "31-03-" & aYear(1) & ""
                e.Row.Cells(12).Text = "As at" & "</br>" & "31-03-" & aYear(1) & ""
                e.Row.Cells(13).Text = "As at" & "</br>" & "31-03-" & aYear(0) & ""

                If ddlSelect.SelectedValue = 2 Then
                    e.Row.Cells(7).Visible = False
                    'e.Row.Cells(4).Visible = False
                    'e.Row.Cells(5).Attributes.Add("colspan", "3")
                End If

            End If

            For r = 0 To gvFixedAsset.Rows.Count - 1
                For c = 0 To gvFixedAsset.Columns.Count - 1

                    If gvFixedAsset.Rows(r).Cells(c).Text.Trim = "Sub Total" Then
                        gvFixedAsset.Rows(r).Cells(2).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(3).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(4).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(5).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(6).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(7).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(8).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(9).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(10).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(11).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(12).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(13).BackColor = Drawing.Color.PapayaWhip
                    End If

                    If gvFixedAsset.Rows(r).Cells(c).Text.Trim = "TOTAL" Then
                        gvFixedAsset.Rows(r).Cells(2).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(3).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(4).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(5).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(6).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(7).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(8).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(9).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(10).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(11).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(12).BackColor = Drawing.Color.PapayaWhip
                        gvFixedAsset.Rows(r).Cells(13).BackColor = Drawing.Color.PapayaWhip
                    End If

                    If gvFixedAsset.Rows(r).Cells(c).Text.Trim = "Sub Total" Or gvFixedAsset.Rows(r).Cells(c).Text.Trim = "TOTAL" Or gvFixedAsset.Rows(r).Cells(c).Text.Trim = "Tangible Assets" Or gvFixedAsset.Rows(r).Cells(c).Text.Trim = "Intangible Assets" Then
                        gvFixedAsset.Rows(r).Cells(c).Font.Bold = True
                    End If
                Next
            Next

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFixedAsset_RowDataBound")
        End Try
    End Sub
    Private Sub gvFixedAsset_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvFixedAsset.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim HeaderGrid As GridView = DirectCast(sender, GridView)
                Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

                If ddlSelect.SelectedValue = 2 Then
                    Dim HeaderCellD As New TableCell()
                    HeaderCellD.Text = " "
                    HeaderCellD.ColumnSpan = 1
                    HeaderGridRow.Cells.Add(HeaderCellD)

                    Dim HeaderCellN As New TableCell()
                    HeaderCellN.Text = " "
                    HeaderCellN.ColumnSpan = 1
                    HeaderGridRow.Cells.Add(HeaderCellN)

                    Dim HeaderCell As New TableCell()
                    HeaderCell.Text = "Gross Block"
                    HeaderCell.ColumnSpan = 4
                    HeaderGridRow.Cells.Add(HeaderCell)

                    HeaderCell = New TableCell()
                    HeaderCell.Text = "Depreciation/Amortisation"
                    HeaderCell.ColumnSpan = 3
                    HeaderGridRow.Cells.Add(HeaderCell)

                    HeaderCell = New TableCell()
                    HeaderCell.Text = "Net Block"
                    HeaderCell.ColumnSpan = 2
                    HeaderGridRow.Cells.Add(HeaderCell)

                Else
                    Dim HeaderCellD As New TableCell()
                    HeaderCellD.Text = " "
                    HeaderCellD.ColumnSpan = 1
                    HeaderGridRow.Cells.Add(HeaderCellD)

                    Dim HeaderCellN As New TableCell()
                    HeaderCellN.Text = " "
                    HeaderCellN.ColumnSpan = 1
                    HeaderGridRow.Cells.Add(HeaderCellN)

                    Dim HeaderCell As New TableCell()
                    HeaderCell.Text = "Gross Block"
                    HeaderCell.ColumnSpan = 4
                    HeaderGridRow.Cells.Add(HeaderCell)
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center

                    HeaderCell = New TableCell()
                    HeaderCell.Text = "Depreciation/Amortisation"
                    HeaderCell.ColumnSpan = 4
                    HeaderGridRow.Cells.Add(HeaderCell)
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center

                    HeaderCell = New TableCell()
                    HeaderCell.Text = "Net Block"
                    HeaderCell.ColumnSpan = 2
                    HeaderGridRow.Cells.Add(HeaderCell)
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center

                End If
                gvFixedAsset.Controls(0).Controls.AddAt(0, HeaderGridRow)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFixedAsset_RowCreated")
        End Try
    End Sub
    Private Sub ddlSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelect.SelectedIndexChanged
        Try
            gvFixedAsset.DataSource = objFA.BindFixedAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            gvFixedAsset.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSelect_SelectedIndexChanged")
        End Try
    End Sub
End Class
