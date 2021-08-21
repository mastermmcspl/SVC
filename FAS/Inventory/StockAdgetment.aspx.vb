
Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports DatabaseLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing
Partial Class Inventory_StockAdgetment
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Orders/PurchaseOrder"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objPO As New clsPurchaseOrder
    Dim objGin As New ClsGoodsInward
    Dim objInvD As New clsInvenotryDetails
    Dim objClsFASGnrl As New clsFASGeneral
    Dim objGnrlFnction As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails
    Dim objPreturn As New clsPurchaseReturn
    Private Shared sIKBBackStatus As String
    Private objclsModulePermission As New clsModulePermission
    Dim objDb As New DBHelper
    Dim objPOst As New clsStockAdjustment
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Save24.png"
        'imgbtnSave.ImageUrl = "~/Images/Save24.png"
        'imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        '  imgNAdd.ImageUrl = "~/Images/Add24.png"
        'ImgbtnAddNew.ImageUrl = "~/Images/Add24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "STOA")
                imgRefresh.Visible = False : imgbtnAdd.Visible = False : imgbtnWaiting.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgRefresh.Visible = True
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnWaiting.Visible = True
                    End If
                End If
                ' CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                lblDescID.Text = "0"
                txtMasterID.Text = "" : txtHistoryID.Text = ""
                ' ddlExistingOrder.Visible = False
                Me.ddlCommodity.Attributes.Add("onclick", "return ValidateCommodity()")
                Me.txtrate.Attributes.Add("onblur", "return ValidateRate()")
                Me.txtrate.Attributes.Add("onblur", "return CalculateUsingRate()")
                Me.txtQuantity.Attributes.Add("onblur", "return validate()")
                Me.txtQuantity.Attributes.Add("onblur", "return Calculate()")
                'Me.btnSave.Attributes.Add("onclick", "return ValidatePurcahseOrder()")
                LoadExistingPurchaseOrder()
                LoadCommodity()
                loadDescitionStart()
                lblDescID.Visible = False
                GenerateOrderCodeAnddate()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub chkCategory_PreRender(ByVal sender As Object,
   ByVal e As System.EventArgs) Handles chkCategory.PreRender
        For Each item As ListItem In chkCategory.Items
            item.Attributes.Add("title", item.Text)
        Next
    End Sub

    Public Sub GenerateOrderCodeAnddate()
        Try
            txtAdjustedCode.Text = objPOst.GeneratePurchaseOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            txtadjustedDate.Text = objClsFASGnrl.FormatDtForRDBMS(System.DateTime.Now, "D")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objPOst.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "--- Select Commodity ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingPurchaseOrder()
        Try
            ddlExistingOrder.DataSource = objPOst.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingOrder.DataTextField = "sm_Code"
            ddlExistingOrder.DataValueField = "sm_id"
            ddlExistingOrder.DataBind()
            ddlExistingOrder.Items.Insert(0, "--- Existing Purchase Order ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadDescitionStart()
        Try
            chkCategory.DataSource = objPOst.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            chkCategory.DataTextField = "Inv_Code"
            chkCategory.DataValueField = "Inv_ID"
            chkCategory.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            If ddlCommodity.SelectedIndex > 0 Then
                chkCategory.DataSource = objPOst.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
                chkCategory.DataTextField = "Inv_Code"
                chkCategory.DataValueField = "SL_ID"
                chkCategory.DataBind()
            Else
                loadDescitionStart()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Private Function GetPurchaseDetails(ByVal iHistoryId As Integer) As Object
        Dim dt As New DataTable
        Try
            dt = objPOst.GetPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                Else
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub GetOtherDetails(ByVal iHistoryId As Integer)
        Dim dt As New DataTable
        Try
            dt = objPOst.GetOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                Else
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDesciptionDetails()
        Dim dt As New DataTable
        Dim sArray As Array
        Try
            lblError.Text = ""
            txtrate.Text = "" : txtRateAmount.Text = ""
            txtQuantity.Text = ""

            If lblDescID.Text <> "0" Then
                dt = objPOst.LoadStockRateQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text, chkCategory.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "Enter Inventory Master Details"
                    Exit Sub
                End If
                If dt.Rows.Count > 1 Then
                    txtrate.Enabled = True
                    txtHistoryID.Text = dt.Rows(0)("SL_HistoryID").ToString()
                    txtrate.Text = dt.Rows(0)("PurchaseRate").ToString()
                    txteQty.Text = dt.Rows(0)("SL_ClosingBalanceQty").ToString()
                    GetOtherDetails(dt.Rows(0)("SL_HistoryID").ToString())
                    txtEValue.Text = (Convert.ToDecimal(dt.Rows(0)("SL_ClosingBalanceQty").ToString()) * Convert.ToDecimal(dt.Rows(0)("PurchaseRate").ToString()))
                Else
                    txtHistoryID.Text = dt.Rows(0)("SL_HistoryID").ToString()
                    txtrate.Text = dt.Rows(0)("PurchaseRate").ToString()
                    txteQty.Text = dt.Rows(0)("SL_ClosingBalanceQty").ToString()
                    GetOtherDetails(dt.Rows(0)("SL_HistoryID").ToString())
                    txtrate.Enabled = True
                    txtEValue.Text = (Convert.ToDecimal(dt.Rows(0)("SL_ClosingBalanceQty").ToString()) * Convert.ToDecimal(dt.Rows(0)("PurchaseRate").ToString()))
                    If txtHistoryID.Text <> "" Then
                        GetPurchaseDetails(txtHistoryID.Text)
                        GetOtherDetails(txtHistoryID.Text)
                    End If
                End If
                ddlUnit.DataSource = objPOst.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                ddlUnit.DataTextField = "Mas_Desc"
                ddlUnit.DataValueField = "Mas_ID"
                ddlUnit.DataBind()
                ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub ddlExistingOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingOrder.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlExistingOrder.SelectedIndex > 0 Then
                ddlCommodity.SelectedIndex = 0
                chkCategory.Items.Clear()

                ClearAll()
                dgStockAdgestment.DataSource = objPOst.LoadAjustmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                dgStockAdgestment.DataBind()
                lblError.Text = ""
                'dt = objPOst.LoadAjustmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                'If dt.Rows.Count > 0 Then
                '    If IsDBNull(dt.Rows(0)("POM_OrderNo").ToString()) = False Then
                '        txtAdjustedCode.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("POM_OrderNo").ToString())
                '    Else
                '        txtAdjustedCode.Text = ""
                '    End If
                '    If IsDBNull(dt.Rows(0)("POM_OrderDate").ToString()) = False Then
                '        txtadjustedDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POM_OrderDate").ToString(), "D")
                '    Else
                '        txtadjustedDate.Text = ""
                '    End If
                '    If IsDBNull(dt.Rows(0)("POM_ID").ToString()) = False Then
                '        txtMasterID.Text = dt.Rows(0)("POM_ID").ToString()
                '    Else
                '        txtMasterID.Text = 0
                '    End If
                '    If IsDBNull(dt.Rows(0)("POM_Status").ToString()) = False Then
                '        If (dt.Rows(0)("POM_Status") = "W") Then
                '            lblError.Text = "Waiting For approval"
                '        ElseIf dt.Rows(0)("POM_Status") = "A" Then
                '            lblError.Text = "Approved."
                '        Else
                '        End If
                '    End If
                'End If
            Else
                txtAdjustedCode.Text = "" : txtadjustedDate.Text = ""
                GenerateOrderCodeAnddate()
                dgStockAdgestment.DataSource = dt
                dgStockAdgestment.DataBind()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingOrder_SelectedIndexChanged")
        End Try
    End Sub

    Public Sub ClearAll()
        Try
            For i = 0 To chkCategory.Items.Count - 1
                chkCategory.Items(i).Selected = False
            Next
            ddlUnit.Items.Clear()
            txtrate.Text = ""

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
        Dim iPices As Integer
        Try
            If (chkCategory.SelectedValue > 0) Then
                ddlCommodity.SelectedValue = objDb.SQLGetDescription(sSession.AccessCode, "select Inv_Parent from inventory_master where Inv_ID='" & chkCategory.SelectedValue & "'")
            End If
            lblDescID.Text = chkCategory.SelectedValue
            LoadDesciptionDetails()
            iPices = objDb.SQLGetDescription(sSession.AccessCode, "Select INVH_PerPieces From Inventory_master_History Where InvH_ID ='" & txtHistoryID.Text & "' And INVH_CompID=" & sSession.AccessCodeID & " ")
            txtPices.Text = iPices
            ddlUnit.SelectedValue = objDb.SQLGetDescription(sSession.AccessCode, "Select InvH_Unit From Inventory_master_History Where InvH_ID ='" & txtHistoryID.Text & "' And INVH_CompID=" & sSession.AccessCodeID & " ")
            hfTotalPieces.Value = txtPices.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        End Try
    End Sub

    Protected Sub ddlUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnit.SelectedIndexChanged
        Dim iPices As Integer
        Dim altUnit As Integer
        Dim total As Decimal
        Try
            If ddlUnit.SelectedIndex > 0 Then
                If (txtQuantity.Text <> "" And txtrate.Text <> "") Then
                    iPices = objDb.SQLExecuteScalar(sSession.AccessCode, "Select INVH_PerPieces From Inventory_master_History Where InvH_ID ='" & txtHistoryID.Text & "' And INVH_CompID=" & sSession.AccessCodeID & " ")
                    altUnit = objDb.SQLExecuteScalar(sSession.AccessCode, "Select InvH_AlterUnit From Inventory_master_History Where InvH_ID ='" & txtHistoryID.Text & "' And INVH_CompID=" & sSession.AccessCodeID & " ")
                    txtPices.Text = iPices
                    If (ddlUnit.SelectedValue = altUnit) Then
                        total = ((Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(iPices)) * Convert.ToDecimal(txtrate.Text))
                        txtRateAmount.Text = total
                        hfRateAmount.Value = total
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlUnit_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim iMasterID As Integer = 0
        Dim dOrderDate As Date
        Dim dRequiredDate As Date
        Try
            lblError.Text = ""
            dOrderDate = Date.ParseExact(Trim(txtadjustedDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objPOst.sSM_AjustedNo = txtAdjustedCode.Text
            objPOst.sSM_AjustedDate = dOrderDate
            objPOst.iSM_YearID = sSession.YearID
            objPOst.iSM_CreatedBy = sSession.UserID
            iMasterID = objPOst.SaveStockAdgestment(sSession.AccessCode, sSession.AccessCodeID, dOrderDate, objPOst)
            txtMasterID.Text = iMasterID
            objPOst.iSD_MasterID = iMasterID
            objPOst.iSD_Commodity = ddlCommodity.SelectedValue
            objPOst.iSD_DescriptionID = lblDescID.Text
            objPOst.iSD_HistoryID = txtHistoryID.Text
            objPOst.iSD_UnitID = ddlUnit.SelectedValue
            objPOst.iSD_StockID = chkCategory.SelectedValue
            objPOst.dSD_Rate = Trim(txtrate.Text)
            objPOst.sSM_AjustedDate = dOrderDate
            objPOst.sSD_Reason = txtReason.Text
            objPOst.sSd_Type = "E"
            objPOst.sSd_Flag = "E"
            objPOst.sSd_Status = "W"
            If (lblFlag.Text = "Negative") Then
                objPOst.sSd_Status = "N"
            Else
                objPOst.sSd_Status = "P"
            End If
            objPOst.dSD_AjustedQuantity = txtadjusted.Text
            If (hfadjustedAmount.Value = "") Then
                objPOst.dSD_AjustedAmount = 0
            Else
                objPOst.dSD_AjustedAmount = hfadjustedAmount.Value
            End If

            objPOst.dSD_ExistingAmount = txtEValue.Text
            objPOst.dSD_ExistingQty = txteQty.Text
            objPOst.SaveStockAdgestmentDetails(sSession.AccessCode, sSession.AccessCodeID, dRequiredDate, objPOst)
            dgStockAdgestment.DataSource = objPOst.LoadAjustmentDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
            dgStockAdgestment.DataBind()
            LoadExistingPurchaseOrder()
            ddlExistingOrder.SelectedValue = iMasterID
            ClearAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub dgStockAdgestment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgStockAdgestment.RowCommand
        Dim lnkDescription As New LinkButton
        Dim dt As New DataTable
        Dim lblitemID As New Label
        Dim lblcomodityID As New Label
        Dim lblHistoryID As New Label

        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblitemID = DirectCast(clickedRow.FindControl("lblDescriptionID"), Label)
            lblcomodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
            lblHistoryID = DirectCast(clickedRow.FindControl("lblHistoryID"), Label)

            If e.CommandName = "Delete" Then
                objPOst.DeleteOrderValues(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtAdjustedCode.Text, lblitemID.Text)
                dgStockAdgestment.DataSource = objPOst.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
                dgStockAdgestment.DataBind()
                If (dgStockAdgestment.Rows.Count = 0) Then
                    objPOst.DeleteOrderValuesFromMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtAdjustedCode.Text)
                End If
                LoadExistingPurchaseOrder()
            End If
            'lnkDescription = e.Item.FindControl("Goods")
            If (objDb.SQLGetDescription(sSession.AccessCode, "select sm_Status from Stock_Adgestment_Master where sm_ID=" & txtMasterID.Text & "") = "W") Then
                dt = objPOst.LoadPurchaseOderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text, lblcomodityID.Text, lblitemID.Text, lblHistoryID.Text)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("sd_Commodity").ToString()) = False Then
                        ddlCommodity.SelectedValue = dt.Rows(0)("sd_Commodity").ToString()
                        chkCategory.DataSource = objPOst.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
                        chkCategory.DataTextField = "Inv_Code"
                        chkCategory.DataValueField = "Inv_ID"
                        chkCategory.DataBind()
                        chkCategory.SelectedValue = dt.Rows(0)("sd_DescriptionID").ToString()
                        lblDescID.Text = dt.Rows(0)("sd_DescriptionID").ToString()
                    Else
                        ddlCommodity.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("sd_HistoryID").ToString()) = False Then
                        txtHistoryID.Text = dt.Rows(0)("sd_HistoryID").ToString()
                    Else
                        txtHistoryID.Text = "0"
                    End If

                    If IsDBNull(dt.Rows(0)("sd_Unit").ToString()) = False Then
                        ddlUnit.SelectedValue = dt.Rows(0)("sd_Unit").ToString()
                    Else
                        ddlUnit.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("sd_Rate").ToString()) = False Then
                        txtrate.Text = dt.Rows(0)("sd_Rate").ToString()
                    Else
                        txtrate.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("sd_RateAmount").ToString()) = False Then
                        txtRateAmount.Text = dt.Rows(0)("sd_RateAmount").ToString()
                    Else
                        txtRateAmount.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("sd_Quantity").ToString()) = False Then
                        txtQuantity.Text = dt.Rows(0)("sd_Quantity").ToString()
                    Else
                        txtQuantity.Text = ""
                    End If


                    Dim sStatus As String = ""
                    sStatus = objPOst.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                    If sStatus = "A" Then
                    Else
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnApprove_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim sStatus As String = ""
        Dim ibtnCancel As New ImageButton
        Try
            lblError.Text = ""
            If dgStockAdgestment.Rows.Count > 0 Then
                objPOst.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, txtAdjustedCode.Text)
                lblError.Text = "Approved Successfully."
                sStatus = objPOst.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtMasterID.Text)
                If sStatus = "W" Then
                    lblError.Text = "Waiting For approve."
                ElseIf sStatus = "A" Then
                    lblError.Text = "Approved."
                End If
                For i = 0 To dgStockAdgestment.Rows.Count - 1
                    ibtnCancel.Enabled = False
                Next
            Else
                lblError.Text = "Add Items."
                chkCategory.Focus()

                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnApprove_Click")
        End Try
    End Sub
End Class
