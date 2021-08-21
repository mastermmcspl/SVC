Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_ChallanDetails
    Inherits System.Web.UI.Page

    Private sFormName As String = "Accounts_ChallanDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private ObjclsChequeDetails As New clsChequeDetails
    Dim objGen As New clsFASGeneral
    Private sSession As AllSession
    Public dtTab As DataTable
    Private objclsModulePermission As New clsModulePermission
    Public lblToDate As New Label
    Private Shared sGMBackStatus As String
    Private Shared sCDSave As String
    Private Shared iMasterID As Integer
    Dim dAmount As Double = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterID As String = ""
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnUpdate.Visible = False : imgbtnBack.Visible = True

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PDCR")
                imgbtnSave.Visible = False : sCDSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        sCDSave = "YES"
                    End If
                End If
                sMasterID = Request.QueryString("MasterID")
                BindBankName() : BindChallanDetails(0, ddlBankName.SelectedIndex)

                txtChequeDate.Text = Date.Now.ToString("dd/MM/yyyy")

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindBankName()
        Try
            ddlBankName.Items.Insert(0, "Select")
            ddlBankName.Items.Insert(1, "SBI")
            ddlBankName.Items.Insert(2, "Canara Bank")
            ddlBankName.Items.Insert(3, "ICICI Bank")
            ddlBankName.Items.Insert(4, "Axis Bank")
            ddlBankName.Items.Insert(5, "Vijaya Bank")
            ddlBankName.Items.Insert(6, "Indian Bank")
            ddlBankName.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindBankName")
        End Try
    End Sub
    Public Sub BindChallanDetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer)
        Dim sDate As String
        Dim imasId As Integer
        Try

            sDate = txtChequeDate.Text
            dgChallanDetails.CurrentPageIndex = iPageIndex
            dtTab = ObjclsChequeDetails.LoadChallanDetails(sSession.AccessCode, ddlBankName.SelectedIndex, sDate, imasId)
            Session("dtTab") = dtTab
            If dtTab.Rows.Count > dgChallanDetails.PageSize Then
                dgChallanDetails.AllowPaging = True
            Else
                dgChallanDetails.AllowPaging = False
            End If
            dgChallanDetails.DataSource = dtTab
            dgChallanDetails.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindChallanDetails")
        End Try
    End Sub
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Try
            If ddlBankName.SelectedIndex >= 0 Then
                BindChallanDetails(0, ddlBankName.SelectedIndex) : dgChallanDetails.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BtnOk_Click")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("PostDatedCheDetails.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dtdes As New DataTable
        Dim DVChallandts As New DataView(dtdes)
        Dim TxtContactNo As New TextBox, TxtPanNo As New TextBox
        Dim Arr() As String
        dtdes = Session("dtTab")
        Try

            For i = 0 To dgChallanDetails.Items.Count - 1
                chkSelect = dgChallanDetails.Items(i).FindControl("chkSelect")
                lblDescID = dgChallanDetails.Items(i).FindControl("lblACMID")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblPostDatedCheDetails.Text = "Select to save Challan Details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASPostDatedCheDetails').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgChallanDetails.Items.Count - 1
                chkSelect = dgChallanDetails.Items(i).FindControl("chkSelect")
                lblDescID = dgChallanDetails.Items(i).FindControl("lblACMID")
                TxtContactNo = dgChallanDetails.Items(i).FindControl("TxtContactNo")
                TxtPanNo = dgChallanDetails.Items(i).FindControl("TxtPanNo")
                If chkSelect.Checked = True Then
                    ObjclsChequeDetails.iACD_MasterID = lblDescID.Text
                    ObjclsChequeDetails.iACD_BankID = ddlBankName.SelectedIndex
                    ObjclsChequeDetails.sACD_ContactNo = TxtContactNo.Text
                    ObjclsChequeDetails.sACD_PANNo = TxtPanNo.Text
                    ObjclsChequeDetails.sACD_Status = "P"
                    ObjclsChequeDetails.iACD_CompID = sSession.AccessCodeID
                    ObjclsChequeDetails.iACD_YearID = sSession.YearID
                    ObjclsChequeDetails.iACD_CreatedBy = sSession.UserID

                    Arr = ObjclsChequeDetails.SaveChallanDetails(ObjclsChequeDetails, sSession.AccessCode)
                    ObjclsChequeDetails.UpdateDelflag(sSession.AccessCode, ObjclsChequeDetails.iACD_MasterID)
                    dtdes = DVChallandts.ToTable

                    lblPostDatedCheDetails.Text = "Successfully Saved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPostDatedCheDetails').modal('show');", True)

                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub

    Private Sub dgChallanDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgChallanDetails.ItemDataBound
        Try
            lblError.Text = ""
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then


                If e.Item.Cells(8).Text <> "" Then
                    dAmount = 0.0
                    dAmount = dAmount + Convert.ToDouble(e.Item.Cells(8).Text)

                    e.Item.Cells(8).Text = Convert.ToDecimal(dAmount).ToString("#,##0.00")
                    e.Item.Cells(8).Font.Bold = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgChallanDetails_ItemDataBound")
        End Try
    End Sub
    Private Sub ddlBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankName.SelectedIndexChanged
        Try
            BindChallanDetails(0, ddlBankName.SelectedIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBankName_SelectedIndexChanged")
        End Try
    End Sub
End Class
