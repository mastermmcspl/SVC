Imports System
Imports System.Data
Imports BusinesLayer
Partial Class Masters_AssetMasters
    Inherits System.Web.UI.Page
    Private sFormName As String = "AssetMAsters"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objAsst As New ClsAssetMaster

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnSave.ImageUrl = "~/Images/Save24.png"
                LoadAssetTypes()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadAssetTypes()
        Dim dt As New DataTable
        Try
            dt = objAsst.LoadAssets(sSession.AccessCode, sSession.AccessCodeID)
            ddlAssetType.DataSource = dt
            ddlAssetType.DataTextField = "GL_Desc"
            ddlAssetType.DataValueField = "GL_ID"
            ddlAssetType.DataBind()
            ddlAssetType.Items.Insert(0, "Select AssetType")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim objAsst As New ClsAssetMaster
        Dim iAstID As Integer = 0
        Dim dt As New DataTable
        Dim Arr() As String
        Try

            If ddlAssetType.SelectedIndex > 0 Then
                iAstID = ddlAssetType.SelectedValue
            Else
                iAstID = 0
            End If

            objAsst.AM_ID = 0
            objAsst.AM_AssetID = iAstID
            objAsst.AM_CreatedBy = sSession.UserID
            objAsst.AM_CreatedOn = DateTime.Today
            objAsst.AM_UpdatedBy = sSession.UserID
            objAsst.AM_UpdatedOn = DateTime.Today
            objAsst.AM_DelFlag = "X"
            objAsst.AM_Status = "W"
            objAsst.AM_YearID = sSession.YearID
            objAsst.AM_CompID = sSession.AccessCodeID
            objAsst.AM_Deprate = txtdeprcnrate.Text
            objAsst.AM_Opeartion = "C"
            objAsst.AM_IPAddress = sSession.IPAddress
            objAsst.AM_ITRate = TxtIncmTax.Text
            Arr = objAsst.SaveAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objAsst)

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                lblGeneralMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
                lblGeneralMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlAssetType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssetType.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlAssetType.SelectedIndex > 0 Then
                dt = objAsst.AssetRetrieve(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlAssetType.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        ddlAssetType.SelectedValue = dt.Rows(i)("AM_AssetID")
                        txtdeprcnrate.Text = dt.Rows(i)("AM_Deprate")
                        If IsDBNull(dt.Rows(0).Item("AM_ITRate")) = False Then
                            TxtIncmTax.Text = dt.Rows(i)("AM_ITRate")
                        Else
                            TxtIncmTax.Text = ""
                            txtdeprcnrate.Text = ""
                        End If
                        imgbtnSave.ImageUrl = "~/Images/Update24.png"
                    Next
                Else
                    TxtIncmTax.Text = ""
                    txtdeprcnrate.Text = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub




    'Protected Sub ddlDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDesc.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Try
    '        lblError.Text = "" : lblGeneralMasterStatus.Text = "" : txtDesc.Text = "" : txtNotes.Text = ""
    '        If ddlDesc.SelectedIndex > 0 Then

    '            If sMasterName = 30 Then
    '                dt = objMaster.GetDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue, sMasterName, 1)
    '                txtdeprcnrate.Visible = True
    '                lblDepRate.Visible = True
    '                txtDesc.Text = ""
    '                txtNotes.Text = ""

    '                txtDesc.Text = ddlDesc.SelectedValue

    '            Else
    '                dt = objMaster.GetDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue, sMasterName, 0)
    '                txtdeprcnrate.Visible = False
    '                lblDepRate.Visible = False
    '                If dt.Rows.Count > 0 Then
    '                    If IsDBNull(dt.Rows(0).Item("Mas_Desc")) = False Then
    '                        txtDesc.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Desc")))
    '                    End If
    '                    If IsDBNull(dt.Rows(0).Item("Mas_Remarks")) = False Then
    '                        txtNotes.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Remarks")))
    '                    End If
    '                    If IsDBNull(dt.Rows(0).Item("Mas_DepRate")) = False Then
    '                        txtdeprcnrate.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_DepRate")))
    '                    End If
    '                    If IsDBNull(dt.Rows(0).Item("Mas_DelFlag")) = False Then
    '                        sGMFlag = dt.Rows(0).Item("Mas_DelFlag")
    '                    End If
    '                End If
    '            End If


    '            'If dt.Rows(0).Item("Mas_master") = 30 Then
    '            '    txtdeprcnrate.Visible = True
    '            '    lblDepRate.Visible = True
    '            'Else
    '            '    txtdeprcnrate.Visible = False
    '            '            lblDepRate.Visible = False
    '            '        End If
    '            '        If dt.Rows.Count > 0 Then
    '            '            If IsDBNull(dt.Rows(0).Item("Mas_Desc")) = False Then
    '            '                txtDesc.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Desc")))
    '            '            End If
    '            '            If IsDBNull(dt.Rows(0).Item("Mas_Remarks")) = False Then
    '            '                txtNotes.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Remarks")))
    '            '            End If
    '            '            If IsDBNull(dt.Rows(0).Item("Mas_DepRate")) = False Then
    '            '                txtdeprcnrate.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_DepRate")))
    '            '            End If
    '            '            If IsDBNull(dt.Rows(0).Item("Mas_DelFlag")) = False Then
    '            '                sGMFlag = dt.Rows(0).Item("Mas_DelFlag")
    '            '            End If
    '            '        End If


    '            If sGMFlag = "W" Then
    '                lblGeneralMasterStatus.Text = "Waiting for Approval"
    '                'If sGMSave = "YES" Then
    '                imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
    '                'End If
    '            ElseIf sGMFlag = "D" Then
    '                lblGeneralMasterStatus.Text = "De-Activated"
    '                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
    '            Else
    '                lblGeneralMasterStatus.Text = "Activated"
    '                ' If sGMSave = "YES" Then
    '                imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
    '                'End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '        'lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        'Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDesc_SelectedIndexChanged")
    '    End Try
    'End Sub
End Class
