Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Accounts_FixedAssetJEEntry
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts_FixedAssetJEEntry"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Dim objFAJE As New clsFixedAssetJournalEntry
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FAJE")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                ibtnDbAdd.Visible = False : ibtnCrAdd.Visible = False
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
                        imgbtnUpdate.Visible = True
                        ibtnDbAdd.Visible = True : ibtnCrAdd.Visible = True
                    End If
                End If

                GenerateOrderCode()
                LoadExistingVoucher()
                LoadParty()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub GenerateOrderCode()
        Try
            txtTransactionNo.Text = objFAJE.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCode")
        End Try
    End Sub
    Private Sub LoadExistingVoucher()
        Try
            ddlExistPayment.DataSource = objFAJE.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            ddlExistPayment.DataTextField = "AFJ_TransactionNo"
            ddlExistPayment.DataValueField = "AFJ_ID"
            ddlExistPayment.DataBind()
            ddlExistPayment.Items.Insert(0, "--- Existing Payment Voucher ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadParty()
        Try
            ddlParty.DataSource = objFAJE.LoadParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataValueField = "APM_ID"
            ddlParty.DataTextField = "Name"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "--- Select Party ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Try
            If ddlParty.SelectedIndex > 0 Then
                ddlLocation.DataSource = objFAJE.LoadLocations(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                ddlLocation.DataTextField = "Name"
                ddlLocation.DataValueField = "Mas_ID"
                ddlLocation.DataBind()
                ddlLocation.Items.Insert(0, "--- Select Locations ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddldbHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbHead.SelectedIndexChanged
        Try
            If ddldbHead.SelectedIndex > 0 Then
                ddldbGL.DataSource = objFAJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbHead.SelectedValue)
                ddldbGL.DataTextField = "GlDesc"
                ddldbGL.DataValueField = "gl_Id"
                ddldbGL.DataBind()
                ddldbGL.Items.Insert(0, "--- Select GL Code ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddldbGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbGL.SelectedIndexChanged
        Try
            If ddldbGL.SelectedIndex > 0 Then
                ddldbsUbGL.DataSource = objFAJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbGL.SelectedValue)
                ddldbsUbGL.DataTextField = "GlDesc"
                ddldbsUbGL.DataValueField = "gl_Id"
                ddldbsUbGL.DataBind()
                ddldbsUbGL.Items.Insert(0, "--- Select SubGL Code ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCrHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrHead.SelectedIndexChanged
        Try
            If ddlCrHead.SelectedIndex > 0 Then
                ddlCrGL.DataSource = objFAJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedValue)
                ddlCrGL.DataTextField = "GlDesc"
                ddlCrGL.DataValueField = "gl_Id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "--- Select GL Code ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCrGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrGL.SelectedIndexChanged
        Try
            If ddlCrGL.SelectedIndex > 0 Then
                ddlCrSubGL.DataSource = objFAJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue)
                ddlCrSubGL.DataTextField = "GlDesc"
                ddlCrSubGL.DataValueField = "gl_Id"
                ddlCrSubGL.DataBind()
                ddlCrSubGL.Items.Insert(0, "--- Select SubGL Code ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim objFAJE As New clsFixedAssetJournalEntry
        Dim Arr() As String
        Dim dt As New DataTable
        Dim iMasterID As Integer
        Dim iAFHID As Integer = 0
        Try
            If ddlExistPayment.SelectedIndex > 0 Then
                iAFHID = ddlExistPayment.SelectedValue
            Else
                iAFHID = 0
            End If
            objFAJE.AFJ_ID = iAFHID
            objFAJE.AFJ_TransactionNo = txtTransactionNo.Text
            objFAJE.AFJ_Amount = txtAmount.Text
            objFAJE.AFJ_Narration = txtNarration.Text
            objFAJE.AFJ_Block = ddlFixedAssetBlock.SelectedValue

            If ddlParty.SelectedIndex > 0 Then
                objFAJE.AFJ_Party = ddlParty.SelectedValue
            Else
                objFAJE.AFJ_Party = 0
            End If
            If ddlLocation.SelectedIndex > 0 Then
                objFAJE.AFJ_Location = ddlLocation.SelectedValue
            Else
                objFAJE.AFJ_Location = 0
            End If

            objFAJE.AFJ_Status = "W"
            objFAJE.AFJ_CreatedBy = sSession.UserID
            objFAJE.AFJ_CreatedOn = DateTime.Today
            objFAJE.AFJ_YearID = sSession.YearID
            objFAJE.AFJ_CompID = sSession.AccessCodeID
            objFAJE.AFJ_Operation = "C"
            objFAJE.AFJ_IPAddress = sSession.IPAddress

            Arr = objFAJE.SaveFixedAssetJE(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objFAJE)
            dt = Session("UpdateTab")
            iMasterID = Arr(1)

            objFAJE.AFJD_MasterID = iMasterID

            If dgAccount.Items.Count > 0 Then
                For i = 0 To dgAccount.Items.Count - 1
                    objFAJE.AFJD_Head = dgAccount.Items(i).Cells(1).Text
                    objFAJE.AFJD_GL = dgAccount.Items(i).Cells(2).Text
                    objFAJE.AFJD_SubGL = dgAccount.Items(i).Cells(4).Text
                    objFAJE.AFJD_Debit = dgAccount.Items(i).Cells(7).Text
                    objFAJE.AFJD_Credit = dgAccount.Items(i).Cells(8).Text
                    objFAJE.AFJD_YearID = sSession.YearID
                    objFAJE.AFJD_CompID = sSession.AccessCodeID
                    objFAJE.AFJD_Status = "W"
                    objFAJE.AFJD_Operation = "C"
                    objFAJE.AFJD_IPAddress = sSession.IPAddress

                    Arr = objFAJE.SaveFixedAssetJEDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objFAJE)
                Next

                If Arr(0) = "2" Then
                    lblCustomerValidationMsg.Text = "Successfully Updated"
                ElseIf Arr(0) = "3" Then
                    lblCustomerValidationMsg.Text = "Successfully Saved"
                End If

            End If
            Clear()
            LoadExistingVoucher()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub ibtnDbAdd_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnDbAdd.Click
        Try
            dgAccount.DataSource = BindGrid()
            dgAccount.DataBind()
            ddldbHead.SelectedIndex = 0 : ddldbGL.SelectedIndex = 0 : ddldbsUbGL.SelectedIndex = 0 : txtDbAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ibtnDbAdd_Click")
        End Try
    End Sub
    Private Function BindGrid() As DataTable
        Dim dtTab As New DataTable
        Dim dRow As DataRow
        Try

            dtTab.Columns.Add("Sl.No")
            dtTab.Columns.Add("Head")
            dtTab.Columns.Add("GLCode")
            dtTab.Columns.Add("GLDescription")
            dtTab.Columns.Add("SubGLCode")
            dtTab.Columns.Add("SubGLDescription")
            dtTab.Columns.Add("DebitAmt(Dr.)")
            dtTab.Columns.Add("CreditAmt(Cr.)")
            dtTab.Columns.Add("Balance")
            dtTab.Columns.Add("OpeningBalance")
            dtTab.Columns.Add("PaymentType")

            dRow = dtTab.NewRow()
            dRow("Sl.No") = 1
            dRow("Head") = ddldbHead.SelectedValue
            dRow("GLCode") = ddldbGL.SelectedValue
            dRow("GLDescription") = ddldbGL.SelectedItem.Text
            dRow("SubGLCode") = ddldbsUbGL.SelectedValue
            dRow("SubGLDescription") = ddldbsUbGL.SelectedItem.Text
            dRow("DebitAmt(Dr.)") = txtDbAmount.Text
            dRow("CreditAmt(Cr.)") = 0
            dRow("Balance") = ""
            dRow("OpeningBalance") = ""
            dRow("PaymentType") = ""
            dtTab.Rows.Add(dRow)

            Session("GridDetails") = dtTab
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BindGridCr() As DataTable
        Dim dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dtTab = Session("GridDetails")

            dRow = dtTab.NewRow()
            dRow("Sl.No") = dtTab.Rows.Count + 1
            dRow("Head") = ddlCrHead.SelectedValue
            dRow("GLCode") = ddlCrGL.SelectedValue
            dRow("GLDescription") = ddlCrGL.SelectedItem.Text
            dRow("SubGLCode") = ddlCrSubGL.SelectedValue
            dRow("SubGLDescription") = ddlCrSubGL.SelectedItem.Text
            dRow("DebitAmt(Dr.)") = 0
            dRow("CreditAmt(Cr.)") = txtCrAmount.Text
            dRow("Balance") = ""
            dRow("OpeningBalance") = ""
            dRow("PaymentType") = ""
            dtTab.Rows.Add(dRow)

            Session("GridDetails") = dtTab
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub ibtnCrAdd_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnCrAdd.Click
        Try
            dgAccount.DataSource = BindGridCr()
            dgAccount.DataBind()
            ddlCrHead.SelectedIndex = 0 : ddlCrGL.SelectedIndex = 0 : ddlCrSubGL.SelectedIndex = 0 : txtCrAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ibtnCrAdd_Click")
        End Try
    End Sub
    Private Sub ddlExistPayment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistPayment.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Try
            If ddlExistPayment.SelectedIndex > 0 Then
                dtMaster = objFAJE.GetMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPayment.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        txtTransactionNo.Text = dtMaster.Rows(i)("AFJ_TransactionNo")
                        ddlParty.SelectedValue = dtMaster.Rows(i)("AFJ_Party")
                        ddlLocation.SelectedValue = dtMaster.Rows(i)("AFJ_Location")
                        ddlFixedAssetBlock.SelectedValue = dtMaster.Rows(i)("AFJ_Block")
                        txtAmount.Text = dtMaster.Rows(i)("AFJ_Amount")
                        txtNarration.Text = dtMaster.Rows(i)("AFJ_Narration")
                    Next
                    dgAccount.DataSource = objFAJE.BindDetailsGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPayment.SelectedValue)
                    dgAccount.DataBind()
                End If
            Else
                Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistPayment_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            Clear()
            ddlExistPayment.SelectedIndex = 0
            GenerateOrderCode()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub Clear()
        Try
            lblError.Text = ""
            txtTransactionNo.Text = "" : ddlParty.SelectedIndex = 0 : ddlLocation.Items.Clear() : ddlFixedAssetBlock.SelectedIndex = 0 : txtAmount.Text = ""
            ddldbHead.SelectedIndex = 0 : ddldbGL.Items.Clear() : ddldbsUbGL.Items.Clear() : txtDbAmount.Text = ""
            ddlCrHead.SelectedIndex = 0 : ddlCrGL.Items.Clear() : ddlCrSubGL.Items.Clear() : txtCrAmount.Text = ""
            txtNarration.Text = ""
            dgAccount.DataSource = Nothing
            dgAccount.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgAccount_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAccount.ItemCommand
        Try
            If e.CommandName = "Edit" Then

            ElseIf e.CommandName = "Delete" Then

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAccount_ItemCommand")
        End Try
    End Sub
End Class
