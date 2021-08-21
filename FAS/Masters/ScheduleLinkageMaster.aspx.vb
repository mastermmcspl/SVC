Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Diagnostics
Partial Class Masters_ScheduleLinkageMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters/ScheduleLinkageMaster.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGen As New clsFASGeneral
    Private objclsModulePermission As New clsModulePermission
    Dim objGenFun As New clsGeneralFunctions
    Dim objSL As New clsScheduleLinkage
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Public Function GetLineNumber(ByVal ex As Exception)
        Dim lineNumber As Int32 = 0
        Const lineSearch As String = ":line "
        Dim index = ex.StackTrace.LastIndexOf(lineSearch)
        If index <> -1 Then
            Dim lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length)
            If Int32.TryParse(lineNumberText, lineNumber) Then
            End If
        End If
        Return lineNumber
    End Function
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                loadHead()

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SLM")
                imgbtnReport.Visible = False : btnAdd.Visible = False : btnDelete.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        btnAdd.Visible = True
                        btnDelete.Visible = True
                    End If
                End If

                chkLedger.DataSource = objSL.LoadAllGeneralLedger(sSession.AccessCode, sSession.AccessCodeID)
                chkLedger.DataTextField = "gl_Desc"
                chkLedger.DataValueField = "gl_ID"
                chkLedger.DataBind()
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub loadHead()
        Try
            ddlHead.Items.Add(New ListItem("Head of Accounts", 0))
            ddlHead.Items.Add(New ListItem("Assets", 1))
            ddlHead.Items.Add(New ListItem("Income", 2))
            ddlHead.Items.Add(New ListItem("Liabilities", 4))
            ddlHead.Items.Add(New ListItem("Expenditure", 3))
            ddlHead.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlHead.SelectedIndex > 0 Then
                ddlGroup.DataSource = objSL.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue)
                ddlGroup.DataTextField = "Description"
                ddlGroup.DataValueField = "gl_id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
            Else
                ddlGroup.DataSource = dt
                ddlGroup.DataBind()
                ddlSubGroup.Items.Clear()
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlHead_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlGroup.SelectedIndex > 0 Then
                ddlSubGroup.DataSource = objSL.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue, ddlGroup.SelectedValue)
                ddlSubGroup.DataTextField = "Description"
                ddlSubGroup.DataValueField = "gl_id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select Sub Group")
            Else
                ddlSubGroup.DataSource = dt
                ddlSubGroup.DataBind()
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGroup_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Try
            If ddlSubGroup.SelectedIndex > 0 Then
                'chkLedger.DataSource = objSL.LoadGeneralLedger(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue)
                'chkLedger.DataTextField = "gl_Desc"
                'chkLedger.DataValueField = "gl_ID"
                'chkLedger.DataBind()

                chkLedger.DataSource = objSL.LoadAllGeneralLedger(sSession.AccessCode, sSession.AccessCodeID)
                chkLedger.DataTextField = "gl_Desc"
                chkLedger.DataValueField = "gl_ID"
                chkLedger.DataBind()

                LoadSavedDetails()
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubGroup_SelectedIndexChanged")
        End Try
    End Sub




    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim sInventory As String = ""
        Dim iGroup As Integer = 0, iSubGroup As Integer = 0
        Dim iGL As Integer = 0, i As Integer = 0
        Try
            If txtNote.Text = "" Then
                lblError.Text = "Enter Note Number"
                Exit Sub
            End If
            If ddlGroup.SelectedIndex > 0 Then
                iGroup = ddlGroup.SelectedValue
            Else
                iGroup = 0
            End If

            For i = 0 To chkLedger.Items.Count - 1
                If chkLedger.Items(i).Selected = True Then
                    sInventory = sInventory & "," & chkLedger.Items(i).Value
                End If
            Next
            If sInventory <> "" Then
                If lblUpdate.Text <> "" Then
                    sInventory = sInventory & lblUpdate.Text
                Else
                    sInventory = sInventory & "," & lblUpdate.Text
                End If

                objSL.SaveScheduleLinkageMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, iGroup, ddlSubGroup.SelectedValue, iGL, sInventory, ddlHead.SelectedValue, txtNote.Text, sSession.IPAddress)
                ddlSubGroup_SelectedIndexChanged(sender, e)
            Else
                sInventory = ""
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAdd_Click")
        End Try
    End Sub
    Private Sub LoadSavedDetails()
        Dim sInv As String = ""
        Dim sArray As Array
        Dim i, j As Integer
        Try
            'For j = 0 To chkLedger.Items.Count - 1
            '    chkLedger.Items(j).Selected = False
            '    chkLedger.Items(j).Enabled = True
            'Next

            sInv = objSL.GetSavedLinkageDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            sArray = sInv.Split(",")
            For i = 0 To sArray.Length - 1
                If sArray(i) <> "" Then
                    For j = 0 To chkLedger.Items.Count - 1
                        If chkLedger.Items(j).Value = sArray(i) Then
                            chkLedger.Items(j).Enabled = False
                            GoTo ArrayNext1
                        End If
                    Next
ArrayNext1:
                End If
            Next

            If ddlHead.SelectedIndex > 0 Then

                sInv = objSL.GetSavedInventoryDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlHead.SelectedValue)
                sArray = sInv.Split(",")
                For i = 0 To sArray.Length - 1
                    If sArray(i) <> "" Then
                        For j = 0 To chkLedger.Items.Count - 1
                            If chkLedger.Items(j).Value = sArray(i) Then
                                chkLedger.Items(j).Enabled = False
                                chkLedger.Items(j).Attributes.Add("style", "Color: Red")
                                GoTo ArrayNext
                            End If
                        Next
ArrayNext:
                    End If
                Next
                GetSavedGeneralLedger(ddlSubGroup.SelectedValue)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetSavedGeneralLedger(ByVal iSubGroup As Integer)
        Dim dt As New DataTable
        Dim iGroup As Integer = 0
        Dim iGL As Integer = 0, i As Integer = 0
        Try

            lblUpdate.Text = objSL.GetSavedGLS(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iGroup, iSubGroup, iGL, ddlHead.SelectedValue)
            txtNote.Text = objSL.GetNoteNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iGroup, iSubGroup, iGL, ddlHead.SelectedValue)
            dt = objSL.GetSavedGLDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iGroup, iSubGroup, iGL, ddlHead.SelectedValue)

            If dt.Rows.Count > 0 Then
                lstGL.DataSource = dt
                lstGL.DataTextField = "gl_Desc"
                lstGL.DataValueField = "gl_ID"
                lstGL.DataBind()
            Else
                lstGL.DataSource = dt
                lstGL.DataBind()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim i As Integer = 0, iRet As Integer = 0
        Dim sInventory As String = ""
        Dim iGroup As Integer = 0, iSubGroup As Integer = 0
        Dim iGL As Integer = 0
        Try
            For i = 0 To lstGL.Items.Count - 1
                If lstGL.Items(i).Selected = True Then
                    iRet = 1
                    GoTo ArrayNext
                End If
            Next

ArrayNext:  If iRet = 0 Then
                lblError.Text = "Select the Item to Delete"
                Exit Sub
            End If

            For i = 0 To lstGL.Items.Count - 1
                If lstGL.Items(i).Selected = False Then
                    sInventory = sInventory & "," & lstGL.Items(i).Value
                End If
            Next
            If sInventory <> "" Then
                sInventory = sInventory & ","
            Else
                sInventory = ""
            End If

            If ddlGroup.SelectedIndex > 0 Then
                iGroup = ddlGroup.SelectedValue
            Else
                iGroup = 0
            End If

            objSL.DeleteGeneralLedger(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iGroup, ddlSubGroup.SelectedValue, iGL, ddlHead.SelectedValue, sInventory, sSession.IPAddress)
            'LoadSavedDetails()
            ddlSubGroup_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDelete_Click")
        End Try
    End Sub
End Class
