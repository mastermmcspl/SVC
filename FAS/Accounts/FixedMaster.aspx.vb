Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_FixedMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts_FixedMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Dim objFAM As New clsFixedAssetMaster
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sSession = Session("AllSession")
                If IsPostBack = False Then
                    sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FAM")
                    imgbtnAdd.Visible = False : imgbtnSave.Visible = False
                    If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                        Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                        Exit Sub
                    Else
                        If sFormButtons.Contains(",View,") = True Then
                        End If
                        If sFormButtons.Contains(",New,") = True Then
                            imgbtnAdd.Visible = True
                        End If
                        If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                            imgbtnSave.Visible = True
                        End If
                    End If
                    LoadCADesc()
                    BindGrid()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadCADesc()
        Dim dt As New DataTable
        Try
            dt = objFAM.BindCADesc(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlglDesc.DataSource = dt
            ddlglDesc.DataTextField = "gl_Desc"
            ddlglDesc.DataValueField = "gl_ID"
            ddlglDesc.DataBind()
            ddlglDesc.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Try
            objFAM.FAM_ID = 0
            objFAM.FAM_DespreaciationType = ddlDepreciationType.SelectedValue
            objFAM.FAM_glDescID = ddlglDesc.SelectedValue
            objFAM.FAM_DepRate = txtDepreciationRate.Text
            objFAM.FAM_Status = "W"
            objFAM.FAM_CompID = sSession.AccessCodeID
            objFAM.FAM_YearID = sSession.YearID

            objFAM.FAM_CreatedBy = sSession.UserID
            objFAM.FAM_CreatedOn = DateTime.Today

            objFAM.FAM_UpdatedBy = sSession.UserID
            objFAM.FAM_UpdatedOn = DateTime.Today

            objFAM.FAM_Operation = "C"
            objFAM.FAM_IPAddress = sSession.IPAddress

            Arr = objFAM.SaveFixedMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objFAM)
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
            End If
            BindGrid()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Public Sub BindGrid()
        Dim dt As New DataTable
        Try
            dt = objFAM.GetGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgDetails.DataSource = dt
            dgDetails.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgDetails_PreRender(sender As Object, e As EventArgs) Handles dgDetails.PreRender
        Dim dt As New DataTable
        Try
            If dgDetails.Rows.Count > 0 Then
                dgDetails.UseAccessibleHeader = True
                dgDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDetails_PreRender")
        End Try
    End Sub
End Class
