Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Accounts_OpeningBalance
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/OpeningBalance.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objOP As New clsOpeningBalance
    Private Shared sOBSave As String
    Private objclsModulePermission As New clsModulePermission
    Dim dDebit As Double = 0, dCredit As Double = 0
    Dim dSubGLDebit As Double = 0, dSubGLCredit As Double = 0
    Dim dBalance As Double = 0
    Private Shared dtOB As DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Dim iManual As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)
                ddlAccBrnch.SelectedValue = objOP.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                ddlAccBrnch_SelectedIndexChanged(sender, e)

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "OB")
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnReport.Visible = False : sOBSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/AccountPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnUpdate.Visible = True
                        sOBSave = "YES"
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If

                iManual = objOP.GetManual(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If iManual = 2 Then
                    lblMsg.Text = "Opening Balance Has been uploaded using Excel,here it can not be edited."
                    imgbtnUpdate.Enabled = False
                End If

                RFVStartDate.ControlToValidate = "txtStartDate" : RFVStartDate.ErrorMessage = "Select Opening Balance As On."
                REVStartDate.ErrorMessage = "Enter valid Date." : REVStartDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                LoadOpeningBalance(0, 0, 0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objOP.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "--- Select Zone ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccZone.SelectedIndexChanged
        Try
            If ddlAccZone.SelectedIndex > 0 Then
                LoadRegion(ddlAccZone.SelectedValue)
            Else
                ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objOP.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "--- Select Region ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccRgn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccRgn.SelectedIndexChanged
        Try
            If ddlAccRgn.SelectedIndex > 0 Then
                LoadArea(ddlAccRgn.SelectedValue)
            Else
                ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objOP.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "--- Select Area ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccArea.SelectedIndexChanged
        Try
            If ddlAccArea.SelectedIndex > 0 Then
                LoadAccBrnch(ddlAccArea.SelectedValue)
            Else
                ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objOP.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlHead.SelectedIndex > 0 Then
                ddlGroup.DataSource = objOP.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue)
                ddlGroup.DataTextField = "Description"
                ddlGroup.DataValueField = "gl_id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
                LoadOpeningBalance(ddlHead.SelectedValue, 0, 0)
            Else
                ddlGroup.DataSource = dt
                ddlGroup.DataBind()

                ddlSubGroup.DataSource = dt
                ddlSubGroup.DataBind()
                LoadOpeningBalance(0, 0, 0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlHead_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlGroup.SelectedIndex > 0 Then
                imgbtnSave.Visible = False
                If sOBSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If

                ddlSubGroup.DataSource = objOP.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)
                ddlSubGroup.DataTextField = "Description"
                ddlSubGroup.DataValueField = "gl_id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select Sub Group")
                LoadOpeningBalance(ddlHead.SelectedValue, ddlGroup.SelectedValue, 0)
            Else
                ddlSubGroup.DataSource = dt
                ddlSubGroup.DataBind()
                LoadOpeningBalance(ddlHead.SelectedValue, 0, 0)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Try
            If ddlSubGroup.SelectedIndex > 0 Then
                imgbtnSave.Visible = False
                If sOBSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                LoadOpeningBalance(ddlHead.SelectedValue, ddlGroup.SelectedValue, ddlSubGroup.SelectedValue)
            Else
                LoadOpeningBalance(ddlHead.SelectedValue, ddlGroup.SelectedValue, 0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubGroup_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadOpeningBalance(ByVal iHead As Integer, ByVal iGL As Integer, ByVal iSubGl As Integer)
        Try
            dtOB = objOP.LoadGrdGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHead, iGL, iSubGl)
            grdGL.DataSource = dtOB
            grdGL.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub grdGL_PreRender(sender As Object, e As EventArgs) Handles grdGL.PreRender
        Dim dt As New DataTable
        Try
            If grdGL.Rows.Count > 0 Then
                grdGL.UseAccessibleHeader = True
                grdGL.HeaderRow.TableSection = TableRowSection.TableHeader
                grdGL.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdGL_PreRender")
        End Try
    End Sub
    'Private Sub grdGL_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdGL.RowDataBound
    '    Dim TxtDeb As New TextBox
    '    Dim txtDbtTotal As New TextBox
    '    Dim txtDrGtotal As New TextBox

    '    Dim TxtCre As New TextBox
    '    Dim txtCrTotal As New TextBox
    '    Dim txtCrGtotal As New TextBox
    '    Dim lblHead As New Label : Dim TxtSubGLDeb As New TextBox : Dim TxtSubGLCre As New TextBox
    '    Dim txtSubGLDrGtotal As New TextBox : Dim txtSubGLCrGtotal As New TextBox

    '    Dim lblGLID As New Label
    '    Try

    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            lblGLID = e.Row.FindControl("lblGLID")

    '            TxtDeb = e.Row.FindControl("TxtDeb")
    '            If TxtDeb.Text <> "" Then
    '                dDebit = dDebit + Convert.ToDouble(TxtDeb.Text)
    '            End If

    '            TxtCre = e.Row.FindControl("TxtCre")
    '            If TxtCre.Text <> "" Then
    '                dCredit = dCredit + Convert.ToDouble(TxtCre.Text)
    '            End If

    '            If e.Row.Cells(7).Text <> "" Then
    '                'Dim d As Double = e.Row.Cells(4).Text.Substring(e.Row.Cells(4).Text.Length - 2, 1)
    '                dBalance = dBalance + Convert.ToDouble(e.Row.Cells(7).Text.Remove(e.Row.Cells(7).Text.Length - 2, 2))
    '            End If

    '            lblHead = e.Row.FindControl("lblHead")
    '            TxtSubGLDeb = e.Row.FindControl("TxtSubGLDeb")
    '            TxtSubGLCre = e.Row.FindControl("TxtSubGLCre")
    '            If lblHead.Text = 2 Then
    '                TxtSubGLDeb.Visible = False : TxtSubGLCre.Visible = False
    '                TxtDeb.Text = Convert.ToDecimal(objOP.GetTotalSubGLDebit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblGLID.Text)).ToString("#,##0.00")
    '                TxtCre.Text = Convert.ToDecimal(objOP.GetTotalSubGLCredit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblGLID.Text)).ToString("#,##0.00")
    '            End If
    '            If lblHead.Text = 3 Then
    '                TxtDeb.Visible = False : TxtCre.Visible = False
    '            End If

    '            If TxtSubGLDeb.Text <> "" Then
    '                dSubGLDebit = dSubGLDebit + Convert.ToDouble(TxtSubGLDeb.Text)
    '            End If

    '            If TxtSubGLCre.Text <> "" Then
    '                dSubGLCredit = dSubGLCredit + Convert.ToDouble(TxtSubGLCre.Text)
    '            End If

    '        End If

    '        If e.Row.RowType = DataControlRowType.Footer Then

    '            txtDrGtotal = e.Row.FindControl("txtDrGtotal")
    '            txtDrGtotal.Text = Convert.ToDecimal(dDebit).ToString("#,##0.00")

    '            txtCrGtotal = e.Row.FindControl("txtCrGtotal")
    '            txtCrGtotal.Text = Convert.ToDecimal(dCredit).ToString("#,##0.00")

    '            txtSubGLDrGtotal = e.Row.FindControl("txtSubGLDrGtotal")
    '            txtSubGLDrGtotal.Text = Convert.ToDecimal(dSubGLDebit).ToString("#,##0.00")

    '            txtSubGLCrGtotal = e.Row.FindControl("txtSubGLCrGtotal")
    '            txtSubGLCrGtotal.Text = Convert.ToDecimal(dSubGLCredit).ToString("#,##0.00")

    '            e.Row.Cells(7).Text = Convert.ToDecimal(dBalance).ToString("#,##0.00")
    '            e.Row.Cells(7).Font.Bold = True
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdGL_RowDataBound")
    '    End Try
    'End Sub
    Private Sub grdGL_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdGL.RowDataBound
        Dim TxtDeb As New TextBox
        Dim txtDbtTotal As New TextBox
        Dim txtDrGtotal As New TextBox

        Dim TxtCre As New TextBox
        Dim txtCrTotal As New TextBox
        Dim txtCrGtotal As New TextBox
        Dim lblHead As New Label : Dim TxtSubGLDeb As New TextBox : Dim TxtSubGLCre As New TextBox
        Dim txtSubGLDrGtotal As New TextBox : Dim txtSubGLCrGtotal As New TextBox

        Dim lblGLID As New Label
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                lblGLID = e.Row.FindControl("lblGLID")

                TxtDeb = e.Row.FindControl("TxtDeb")
                'If TxtDeb.Text <> "" Then
                '    dDebit = dDebit + Convert.ToDouble(TxtDeb.Text)
                'End If

                TxtCre = e.Row.FindControl("TxtCre")
                'If TxtCre.Text <> "" Then
                '    dCredit = dCredit + Convert.ToDouble(TxtCre.Text)
                'End If

                'If e.Row.Cells(7).Text <> "" Then
                '    'Dim d As Double = e.Row.Cells(4).Text.Substring(e.Row.Cells(4).Text.Length - 2, 1)
                '    dBalance = dBalance + Convert.ToDouble(e.Row.Cells(7).Text.Remove(e.Row.Cells(7).Text.Length - 2, 2))
                'End If

                lblHead = e.Row.FindControl("lblHead")
                TxtSubGLDeb = e.Row.FindControl("TxtSubGLDeb")
                TxtSubGLCre = e.Row.FindControl("TxtSubGLCre")
                If lblHead.Text = 2 Then
                    TxtSubGLDeb.Visible = False : TxtSubGLCre.Visible = False
                    '*** Commented Bcz rounding of ***'
                    'TxtDeb.Text = Convert.ToDecimal(objOP.GetTotalSubGLDebit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblGLID.Text)).ToString("#,##0.00")
                    'TxtCre.Text = Convert.ToDecimal(objOP.GetTotalSubGLCredit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblGLID.Text)).ToString("#,##0.00")
                    'dBalance = Convert.ToDecimal(TxtDeb.Text).ToString("#,##0.00") - Convert.ToDecimal(TxtCre.Text).ToString("#,##0.00")
                    'e.Row.Cells(7).Text = Convert.ToDecimal(dBalance).ToString("#,##0.00")
                    '*** Commented Bcz rounding of ***'

                    TxtDeb.Text = objOP.GetTotalSubGLDebit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblGLID.Text)
                    TxtCre.Text = objOP.GetTotalSubGLCredit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblGLID.Text)
                    dBalance = (TxtDeb.Text - TxtCre.Text)
                    e.Row.Cells(7).Text = dBalance
                End If
                If TxtDeb.Text <> "" Then
                    dDebit = dDebit + Convert.ToDecimal(TxtDeb.Text)
                    'TxtDeb.Text = dDebit
                End If
                If TxtCre.Text <> "" Then
                    dCredit = dCredit + Convert.ToDecimal(TxtCre.Text)
                    ' TxtCre.Text = dCredit
                End If
                If lblHead.Text = 3 Then
                    TxtDeb.Visible = False : TxtCre.Visible = False
                End If

                If TxtSubGLDeb.Text <> "" Then
                    dSubGLDebit = dSubGLDebit + Convert.ToDouble(TxtSubGLDeb.Text)
                End If

                If TxtSubGLCre.Text <> "" Then
                    dSubGLCredit = dSubGLCredit + Convert.ToDouble(TxtSubGLCre.Text)
                End If

                If e.Row.Cells(7).Text <> "" Then
                    'Dim d As Double = e.Row.Cells(4).Text.Substring(e.Row.Cells(4).Text.Length - 2, 1)
                    ' dBalance = dBalance + Convert.ToDouble(e.Row.Cells(7).Text.Remove(e.Row.Cells(7).Text.Length - 2, 2))
                End If
            End If

            If e.Row.RowType = DataControlRowType.Footer Then

                txtDrGtotal = e.Row.FindControl("txtDrGtotal")
                txtDrGtotal.Text = Convert.ToDecimal(dDebit)

                txtCrGtotal = e.Row.FindControl("txtCrGtotal")
                txtCrGtotal.Text = Convert.ToDecimal(dCredit)

                txtSubGLDrGtotal = e.Row.FindControl("txtSubGLDrGtotal")
                txtSubGLDrGtotal.Text = Convert.ToDecimal(dSubGLDebit)

                txtSubGLCrGtotal = e.Row.FindControl("txtSubGLCrGtotal")
                txtSubGLCrGtotal.Text = Convert.ToDecimal(dSubGLCredit)

                e.Row.Cells(7).Text = Convert.ToDecimal(dDebit) - Convert.ToDecimal(dCredit)
                'e.Row.Cells(7).Text = Convert.ToDouble(txtDrGtotal.Text) - Convert.ToDouble(txtCrGtotal.Text)
                'e.Row.Cells(7).Text = Convert.ToDecimal(dBalance).ToString("#,##0.00")
                e.Row.Cells(7).Font.Bold = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdGL_RowDataBound")
        End Try
    End Sub

    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim i As Integer
        Dim TxtDeb As New TextBox
        Dim TxtCre As New TextBox
        Dim Arr() As String
        Dim objclsExcel As New clsExcelUpload
        Dim lblgrdGL As New Label, lblGlId As New Label, lblAccHead As New Label
        Dim iManual As Integer
        Dim lblHead As New Label : Dim TxtSubGLDeb As New TextBox : Dim TxtSubGLCre As New TextBox
        Try
            iManual = objOP.GetManual(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If iManual = 2 Then
                lblMsg.Text = "Opening Balance Has been uploaded using Excel,Opening Balance through form can not be Updated."
                Exit Sub
            End If

            If ddlAccZone.SelectedIndex = 0 Then
                lblError.Text = "Select Zone"
                Exit Sub
            End If
            If ddlAccRgn.SelectedIndex = 0 Then
                lblError.Text = "Select Region"
                Exit Sub
            End If
            If ddlAccArea.SelectedIndex = 0 Then
                lblError.Text = "Select Area"
                Exit Sub
            End If
            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch"
                Exit Sub
            End If

            'Remove Data'
            objclsExcel.RemoveOpBal(sSession.AccessCode, sSession.AccessCodeID)
            'Remove Data'

            For i = 0 To grdGL.Rows.Count - 1

                lblHead = grdGL.Rows(i).FindControl("lblHead")

                lblgrdGL = grdGL.Rows(i).FindControl("lblGLCode")
                objOP.sOpn_GLCode = lblgrdGL.Text

                lblGlId = grdGL.Rows(i).FindControl("lblGLID")
                objOP.iOpn_GlId = lblGlId.Text

                lblAccHead = grdGL.Rows(i).FindControl("lblAccHead")
                objOP.iOpn_AccHead = lblAccHead.Text

                If lblHead.Text = 2 Then    'GL
                    TxtDeb = grdGL.Rows(i).FindControl("TxtDeb")
                    If TxtDeb.Text.Length = 0 Then
                        objOP.dOpn_DebitAmt = "0.00"
                    Else
                        objOP.dOpn_DebitAmt = TxtDeb.Text
                    End If

                    TxtCre = grdGL.Rows(i).FindControl("TxtCre")
                    If TxtCre.Text.Length = 0 Then
                        objOP.dOpn_CreditAmount = "0.00"
                    Else
                        objOP.dOpn_CreditAmount = TxtCre.Text
                    End If
                ElseIf lblHead.Text = 3 Then    'SubGL
                    TxtSubGLDeb = grdGL.Rows(i).FindControl("TxtSubGLDeb")
                    If TxtSubGLDeb.Text.Length = 0 Then
                        objOP.dOpn_DebitAmt = "0.00"
                    Else
                        objOP.dOpn_DebitAmt = TxtSubGLDeb.Text
                    End If

                    TxtSubGLCre = grdGL.Rows(i).FindControl("TxtSubGLCre")
                    If TxtSubGLCre.Text.Length = 0 Then
                        objOP.dOpn_CreditAmount = "0.00"
                    Else
                        objOP.dOpn_CreditAmount = TxtSubGLCre.Text
                    End If
                End If

                objOP.dOpn_Date = objGen.FormatDtForRDBMS(txtStartDate.Text, "D")
                objOP.iOpn_YearId = sSession.YearID
                objOP.iOpn_CompId = sSession.AccessCodeID
                objOP.sOpn_IPAddress = sSession.IPAddress
                objOP.iOpn_Manual = 1

                objOP.iOpn_ZoneID = ddlAccZone.SelectedValue
                objOP.iOpn_RegionID = ddlAccRgn.SelectedValue
                objOP.iOpn_AreaID = ddlAccArea.SelectedValue
                objOP.iOpn_BranchID = ddlAccBrnch.SelectedValue

                Arr = objOP.SaveOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objOP)
            Next

            lblOpeningBalanceMsg.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            If (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex = 0) And (ddlSubGroup.SelectedIndex = -1) Then
                LoadOpeningBalance(ddlHead.SelectedValue, 0, 0)
            ElseIf (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex > 0) And (ddlSubGroup.SelectedIndex = -1) Then
                LoadOpeningBalance(ddlHead.SelectedValue, ddlGroup.SelectedValue, 0)
            ElseIf (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex > 0) And (ddlSubGroup.SelectedIndex = 0) Then
                LoadOpeningBalance(ddlHead.SelectedValue, ddlGroup.SelectedValue, 0)
            ElseIf (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex > 0) And (ddlSubGroup.SelectedIndex > 0) Then
                LoadOpeningBalance(ddlHead.SelectedValue, ddlGroup.SelectedValue, ddlSubGroup.SelectedValue)
            Else
                LoadOpeningBalance(0, 0, 0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = dtOB
            If dt.Rows.Count = 0 Then
                lblOpeningBalanceMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RPTOpBalForm.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objGenFun.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Opening Balance", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=Opening Balance" + ".xls")
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
        'Try
        '    ExportoExcelOpeningBalance(dtOB)
        'Catch ex As Exception
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        'End Try
    End Sub
    Public Sub ExportoExcelOpeningBalance(ByVal dt1 As DataTable)
        Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim dt As System.Data.DataTable
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0, rowIndex As Integer = 0
        Dim sPath As String, strFileNameFullPath As String, strFileNamePath As String, sExcelFileName As String
        Dim i As Integer
        Try
            If dt1.Rows.Count > 0 Then
                dt = dt1
                sPath = Server.MapPath("../") & "SampleExcels\OpeningBalance.xlsx"
                wBook = excel.Workbooks.Add(sPath)
                wSheet = wBook.ActiveSheet()
                For i = 2 To 6
                    colIndex = colIndex + 1
                    excel.Cells(1, colIndex) = dt.Columns(i).ColumnName
                    excel.Cells(1, colIndex).Font.Bold = True
                Next
                For Each dr In dt.Rows
                    rowIndex = rowIndex + 1
                    colIndex = 0
                    For i = 2 To 6
                        colIndex = colIndex + 1
                        excel.Cells(rowIndex + 1, colIndex) = dr(dt.Columns(i).ColumnName)
                    Next
                Next
                wSheet.Columns.AutoFit()
                sExcelFileName = "OpeningBalance.xlsx"
                strFileNamePath = objGenFun.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
                If strFileNamePath.EndsWith("\") = False Then
                    strFileNameFullPath = strFileNamePath & "\" & sExcelFileName
                Else
                    strFileNameFullPath = strFileNamePath & sExcelFileName
                End If

                Dim blnFileOpen As Boolean = False
                Try
                    If System.IO.File.Exists(strFileNameFullPath) Then
                        System.IO.File.Delete(strFileNameFullPath)
                    End If
                    Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileNameFullPath)
                    fileTemp.Close()
                Catch ex As Exception
                    blnFileOpen = False
                End Try
                If System.IO.File.Exists(strFileNameFullPath) Then
                    System.IO.File.Delete(strFileNameFullPath)
                End If
                wBook.SaveAs(strFileNameFullPath)
                wBook.Close()
                excel.Quit()
                excel = Nothing
                DownloadFile(strFileNameFullPath)
            Else
                'lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub DownloadFile(ByVal pstrFileNameAndPath As String)
        Dim Extn As String, pstrContentType As String, sFileName As String, sFullName As String
        Dim myFileInfo As IO.FileInfo
        Dim StartPos As Long = 0, FileSize As Long, EndPos As Long
        Try
            If IO.File.Exists(pstrFileNameAndPath) Then
                myFileInfo = New IO.FileInfo(pstrFileNameAndPath)
                FileSize = myFileInfo.Length
                EndPos = FileSize
                Web.HttpContext.Current.Response.Clear()
                Web.HttpContext.Current.Response.ClearHeaders()
                Web.HttpContext.Current.Response.ClearContent()
                Extn = objGen.GetFileExt(pstrFileNameAndPath)
                sFileName = System.IO.Path.GetFileNameWithoutExtension(pstrFileNameAndPath)
                sFullName = sFileName & "." & Extn
                pstrContentType = "application/x-msexcel"
                Dim Range As String = Web.HttpContext.Current.Request.Headers("Range")
                If Not ((Range Is Nothing) Or (Range = "")) Then
                    Dim StartEnd As Array = Range.Substring(Range.LastIndexOf("=") + 1).Split("-")
                    If Not StartEnd(0) = "" Then
                        StartPos = CType(StartEnd(0), Long)
                    End If
                    If StartEnd.GetUpperBound(0) >= 1 And Not StartEnd(1) = "" Then
                        EndPos = CType(StartEnd(1), Long)
                    Else
                        EndPos = FileSize - StartPos
                    End If
                    If EndPos > FileSize Then
                        EndPos = FileSize - StartPos
                    End If
                    System.Web.HttpContext.Current.Response.StatusCode = 206
                    System.Web.HttpContext.Current.Response.StatusDescription = "Partial Content"
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Range", "bytes " & StartPos & "-" & EndPos & "/" & FileSize)
                End If
                System.Web.HttpContext.Current.Response.ContentType = pstrContentType
                System.Web.HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename=" & sFullName & "")
                System.Web.HttpContext.Current.Response.WriteFile(Server.HtmlEncode(pstrFileNameAndPath), StartPos, EndPos)
                System.Web.HttpContext.Current.Response.Flush()
                System.Web.HttpContext.Current.Response.End()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = dtOB
            If dt.Rows.Count = 0 Then
                lblOpeningBalanceMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalPaymentValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RPTOpBalForm.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objGenFun.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Opening Balance", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=Opening Balance" + ".pdf")
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
    Private Sub grdGL_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdGL.RowCommand
        Dim lblID As New Label
        Try
            If e.CommandName = "Select" Then   'ElseIf e.CommandName = "MRP" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblGLID"), Label)

                GVDetails.DataSource = objOP.BindBreakUPDetails(sSession.AccessCode, sSession.AccessCodeID, lblID.Text)
                GVDetails.DataBind()

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdGL_RowCommand")
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                If ddlAccBrnch.SelectedIndex > 0 Then
                    iParent = objOP.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                    ddlAccArea.SelectedValue = iParent
                End If
                If ddlAccArea.SelectedIndex > 0 Then
                    iParent = objOP.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                    ddlAccRgn.SelectedValue = iParent
                End If
                If ddlAccRgn.SelectedIndex > 0 Then
                    iParent = objOP.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                    ddlAccZone.SelectedValue = iParent
                End If
            Else
                ddlAccArea.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccZone.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw
        End Try
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
