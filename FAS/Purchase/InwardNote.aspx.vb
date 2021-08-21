Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports System.IO
Imports DatabaseLayer
Partial Class Purchase_InwardNote
    Inherits System.Web.UI.Page
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objPO As New clsPurchaseOrder
    Dim objGIN As New ClsGoodsInward
    Dim sSession As New AllSession
    Dim objFasGnrl As New clsFASGeneral
    Dim objGnrlFnctn As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails
    Dim objclsModulePermission As New clsModulePermission
    Dim objDb As New DBHelper
    Private Shared sFormName As String = "Purchase/InwardNote"
    Private dtTab As New DataTable
    Private Shared sIKBBackStatus As String
    Private Shared sCurrentMonthID As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgNAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Dim iDefaultBranch As Integer
        Try
            sSession = Session("AllSession")
            RFEinwardNo.InitialValue = 0
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "GIN")
                imgbtnWaiting.Visible = False : imgbtnAdd.Visible = False : imgRefresh.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
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

                RFVddlOrderNo.InitialValue = "Select Order No" : RFVddlOrderNo.ErrorMessage = "Select Order No."

                Me.txtAccepted.Attributes.Add("onblur", "return CalculateFromVat()")
                Me.txtDiscount.Attributes.Add("onblur", "return CalculateDiscount()")
                Me.ddlVat.Attributes.Add("onclick", "return CalculateFromVat()")
                Me.ddlCst.Attributes.Add("onclick", "return CalculateFromCST()")
                'txtInvoiceDate.Text = Date.Today
                GenerateOrderCodeAnddate()

                iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                LoadOrderNo(iDefaultBranch)
                LoadVAT()
                LoadCST()
                LoadNBrand()
                LoadSuppliers()
                BindTypeOfSale()
                BindCategoryOfSale()
                BuildTable()
                Session("UpdateTab") = dtTab
                LoadExistingInwardNo()
                Dim iAID As String = "" : Dim iDashBoard As String = ""
                iDashBoard = Request.QueryString("sStrID")

                If iDashBoard = "1" Then
                    iAID = objFasGnrl.DecryptQueryString(Request.QueryString("AID"))
                    If iAID <> "AddNew" Then
                        'ExistingAllocationNo(0)
                        ddlExistingInwardNo.SelectedValue = objFasGnrl.DecryptQueryString(Request.QueryString("AID"))
                        ddlExistingInwardNo_SelectedIndexChanged(sender, e)
                    Else
                    End If
                ElseIf iDashBoard = "0" Then
                End If
                'If iDashBoard <> "" Then
                '    Dim iAllocateID As String = ""
                '    iAllocateID = objFasGnrl.DecryptQueryString(Request.QueryString("AllocationID"))
                '    If iAllocateID <> "" Then
                '        '      lblReAllocateID.Text = iAllocateID
                '        ddlExistingInwardNo.SelectedValue = objFasGnrl.DecryptQueryString(Request.QueryString("AllocationID"))
                '        ddlExistingInwardNo_SelectedIndexChanged(sender, e)
                '    End If
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadNBrand()
        Try
            ddlNBrand.DataSource = objPO.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlNBrand.DataTextField = "Inv_Description"
            ddlNBrand.DataValueField = "Inv_ID"
            ddlNBrand.DataBind()
            ddlNBrand.Items.Insert(0, "Select Brand")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingInwardNo()
        Try
            ddlExistingInwardNo.DataSource = objGIN.LoadExistingInwardNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingInwardNo.DataTextField = "PGM_GIN_Number"
            ddlExistingInwardNo.DataValueField = "PGM_ID"
            ddlExistingInwardNo.DataBind()
            ddlExistingInwardNo.Items.Insert(0, "Existing Inward No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSuppliers()
        Try
            ddlSupplier.DataSource = objPO.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplier.DataTextField = "CSM_Name"
            ddlSupplier.DataValueField = "CSM_ID"
            ddlSupplier.DataBind()
            ddlSupplier.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadOrderNo(ByVal iBranch As Integer)
        Try
            ddlOrderNo.DataSource = objGIN.LoadOurRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBranch)
            ddlOrderNo.DataTextField = "POM_OrderNo"
            ddlOrderNo.DataValueField = "POM_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "Select Order No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCodeAnddate()
        Try
            txtOrderCode.Text = objGIN.GenerateInwardCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadExistDocRefNo(ByVal iPONo As Integer)
        Try
            ddlExistingDocRef.DataSource = objGIN.LoadExistDocRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPONo)
            ddlExistingDocRef.DataTextField = "PRM_DocumentRefNo"
            ddlExistingDocRef.DataValueField = "PRM_ID"
            ddlExistingDocRef.DataBind()
            ddlExistingDocRef.Items.Insert(0, "--- Existing Doc Ref No ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            lblStatus.Text = ""
            oStatus = HttpUtility.UrlEncode(objFasGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/Purchase/InwardNoteMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Protected Sub ddlExistingInwardNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingInwardNo.SelectedIndexChanged
        Try
            Clear()
            dgInward.DataSource = Nothing
            dgInward.DataBind()
            lblError.Text = ""
            lblStatus.Text = ""
            txtDocRefNo.Visible = True
            ddlExistingDocRef.Enabled = True
            If ddlExistingInwardNo.SelectedIndex > 0 Then
                ViewEnable()
                'LoadExistDocRefNo(ddlOrderNo.SelectedValue)
                'ExistingInwardDetails()
                LoadExistingInwardDetails()
                'LoadOrderDetails(ddlOrderNo.SelectedValue)
                dgInward.DataSource = objGIN.LoadInwardDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInwardNo.SelectedValue)
                dgInward.DataBind()
                If (dgInward.Rows.Count > 0) Then
                    dgInward.Visible = True
                Else
                    dgInward.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingInwardNo_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadOrderDetails(ByVal iPONo As Integer)
        Dim dtable As New DataTable
        Try
            dtable = objGIN.OrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPONo)
            If (dtable.Rows.Count > 0) Then
                For i = 0 To dtable.Rows.Count - 1
                    txtOrderDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("POM_OrderDate"), "D")
                    ddlSupplier.SelectedValue = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("POM_Supplier"))

                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOrderDetails")
        End Try
    End Sub
    Protected Sub ddlOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrderNo.SelectedIndexChanged
        Try
            Clear()
            dgInward.DataSource = Nothing
            dgInward.DataBind()
            lblError.Text = ""
            lblStatus.Text = ""
            ddlExistingInwardNo.SelectedIndex = 0
            txtDocRefNo.Visible = True
            ddlExistingDocRef.Enabled = True
            If ddlOrderNo.SelectedIndex > 0 Then
                ViewEnable()
                GenerateOrderCodeAnddate()
                LoadExistDocRefNo(ddlOrderNo.SelectedValue)
                LoadOrderDetails(ddlOrderNo.SelectedValue)
                dtTab = objGIN.GetPODetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                Session("UpdateTab") = dtTab
                dgInward.DataSource = dtTab
                dgInward.DataBind()
                If (dgInward.Rows.Count > 0) Then
                    dgInward.Visible = True
                Else
                    dgInward.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlOrderNo_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlNBrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNBrand.SelectedIndexChanged
        Try
            If ddlNBrand.SelectedIndex > 0 Then
                ddlNItems.DataSource = objPO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlNBrand.SelectedValue)
                ddlNItems.DataTextField = "Inv_Code"
                ddlNItems.DataValueField = "Inv_ID"
                ddlNItems.DataBind()
                ddlNItems.Items.Insert(0, "Select Item")
            Else
                loadDescitionStart()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlNBrand_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub loadRegular()
        Try
            ddlCst.Items.Clear()

            ddlCst.DataSource = objInvntry.LoadVAT(sSession.AccessCode, sSession.AccessCodeID)
            ddlCst.DataTextField = "Mas_Desc"
            ddlCst.DataValueField = "Mas_ID"
            ddlCst.DataBind()
            ddlCst.Items.Insert(0, "--- Select CST ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub LoadVAT()
        Try
            ddlVat.DataSource = objInvntry.LoadVAT(sSession.AccessCode, sSession.AccessCodeID)
            ddlVat.DataTextField = "Mas_Desc"
            ddlVat.DataValueField = "Mas_ID"
            ddlVat.DataBind()
            ddlVat.Items.Insert(0, "--- Select VAT ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCST()
        Try
            ddlCst.DataSource = objInvntry.LoadCST(sSession.AccessCode, sSession.AccessCodeID)
            ddlCst.DataTextField = "Mas_Desc"
            ddlCst.DataValueField = "Mas_ID"
            ddlCst.DataBind()
            ddlCst.Items.Insert(0, "--- Select CST ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetOtherDetails(ByVal iHistoryId As Integer)
        Dim dt As New DataTable
        Try
            dt = objPO.GetOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                    txtExcise.Text = objFasGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                Else
                    txtExcise.Text = "0"
                End If
                If (ddlCstCtgry.SelectedValue = 1) Then
                    ddlVat.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    Else
                        ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCstCtgry.SelectedValue = 2) Then
                    ddlVat.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0)("InvH_CST").ToString()) = False Then
                        ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 15, dt.Rows(0)("InvH_CST").ToString())
                    Else
                        ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCstCtgry.SelectedValue = 3) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                    ddlCst.Enabled = False
                    ddlVat.Enabled = False
                ElseIf (ddlCstCtgry.SelectedValue = 4) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                    ddlCst.Enabled = False
                    ddlVat.Enabled = False
                ElseIf (ddlCstCtgry.SelectedValue = 5) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                    ddlCst.Enabled = False
                    ddlVat.Enabled = False
                Else
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        ddlVat.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetOtherDetails")
        End Try
    End Sub

    Protected Sub ddlNItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNItems.SelectedIndexChanged
        Dim altPices As Integer
        Try
            If (ddlNItems.SelectedValue > 0) Then
                ddlNBrand.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, ddlNItems.SelectedValue)
            End If
            lblDescID.Text = ddlNItems.SelectedValue
            LoadDesciptionDetails()
            altPices = objPO.GetAlterNatePiceValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
            txtPices.Text = altPices
            ddlNUnit.SelectedValue = objPO.GetUnitsValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
            hfTotalPieces.Value = txtPices.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlNItems_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindTypeOfSale()
        Try
            ddlTypeOfSale.Items.Add(New ListItem("Select Type Of Sale", 0))
            ddlTypeOfSale.Items.Add(New ListItem("Local", 1))
            ddlTypeOfSale.Items.Add(New ListItem("Inter State", 2))
            ddlTypeOfSale.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindCategoryOfSale()
        Try
            ddlCstCtgry.Items.Add(New ListItem("Select Type Of Sale", 0))
            ddlCstCtgry.Items.Add(New ListItem("Regular", 1))
            ddlCstCtgry.Items.Add(New ListItem("2 % CST", 2))
            ddlCstCtgry.Items.Add(New ListItem("H Form", 3))
            ddlCstCtgry.Items.Add(New ListItem("I Form", 4))
            ddlCstCtgry.Items.Add(New ListItem("F Form", 5))
            ddlCstCtgry.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDesciptionDetails()
        Dim dt As New DataTable
        Dim sArray As Array
        Try
            If lblDescID.Text <> "0" Then
                dt = objPO.CheckDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text, txtOrderDate.Text)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "Enter Details in Inventory Master Details"
                    Exit Sub
                End If
                If dt.Rows.Count > 1 Then
                    ddlRate.DataSource = dt
                    ddlRate.DataTextField = "INVH_PreDeterminedPrice"
                    ddlRate.DataValueField = "InvH_ID"
                    ddlRate.DataBind()
                    ddlRate.Enabled = True : txtRate.Enabled = False
                    txtHistoryID.Text = ddlRate.SelectedValue
                    sArray = ddlRate.SelectedItem.Text.Split("-")
                    txtRate.Text = sArray(0)
                    GetOtherDetails(txtHistoryID.Text)
                Else
                    sArray = dt.Rows(0)(1).ToString().Split("-")
                    txtRate.Text = sArray(0)
                    txtHistoryID.Text = dt.Rows(0)(0).ToString()
                    ddlRate.Enabled = False : txtRate.Enabled = True
                    If txtHistoryID.Text <> "" Then
                        GetPurchaseDetails(txtHistoryID.Text)
                        GetOtherDetails(txtHistoryID.Text)
                    End If
                End If
                ddlNUnit.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                ddlNUnit.DataTextField = "Mas_Desc"
                ddlNUnit.DataValueField = "Mas_ID"
                ddlNUnit.DataBind()
                ddlNUnit.Items.Insert(0, "--- Unit of Measurement ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDesciptionDetails")
        End Try
    End Sub

    Private Function GetPurchaseDetails(ByVal iHistoryId As Integer) As Object
        Dim dt As New DataTable
        Try
            dt = objPO.GetPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                'If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                '    txtExcise.Text = clsTRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                'End If

                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                    txtExcise.Text = objFasGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                Else
                    txtExcise.Text = "0"
                End If

                If (ddlCstCtgry.SelectedValue = 1) Then
                    ddlVat.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    Else
                        ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCstCtgry.SelectedValue = 2) Then
                    If IsDBNull(dt.Rows(0)("InvH_CST").ToString()) = False Then
                        ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 15, dt.Rows(0)("InvH_CST").ToString())
                    Else
                        ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCstCtgry.SelectedValue = 3) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                ElseIf (ddlCstCtgry.SelectedValue = 4) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                ElseIf (ddlCstCtgry.SelectedValue = 5) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                Else
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        'txtVat.Text = clsTRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("InvH_Vat").ToString())
                        ddlVat.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetPurchaseDetails")
        End Try
    End Function
    Private Sub loadDescitionStart()
        Try
            ddlNItems.DataSource = objPO.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            ddlNItems.DataTextField = "Inv_Code"
            ddlNItems.DataValueField = "Inv_ID"
            ddlNItems.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub dgInward_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgInward.RowDataBound
        Dim txtRecivedQty As New TextBox
        Dim lblOderedQty As New Label
        Dim txtAcceptedQty As New TextBox
        Dim txtRejectedQty As New TextBox
        Dim txtExcssQty As New TextBox
        Dim txtMdate As New TextBox
        Dim txtEdate As New TextBox
        Dim lblPending As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                txtRecivedQty = e.Row.FindControl("txtReceivedQty")
                lblOderedQty = e.Row.FindControl("lblOrderQty")
                txtAcceptedQty = e.Row.FindControl("txtAcceptedQty")
                txtRejectedQty = e.Row.FindControl("txtRejected")
                txtExcssQty = e.Row.FindControl("txtExcessQty")
                lblPending = e.Row.FindControl("lblPending")
                txtMdate = e.Row.FindControl("txtMdate")
                txtEdate = e.Row.FindControl("txtEdate")

                If ddlExistingInwardNo.SelectedIndex > 0 Then
                    txtRecivedQty.Enabled = False : txtAcceptedQty.Enabled = False : txtRejectedQty.Enabled = False : txtExcssQty.Enabled = False
                End If

                txtEdate.Attributes.Add("Onblur", "javascript:return CheckEDate('" & txtMdate.ClientID & "','" & txtEdate.ClientID & "')")
                txtMdate.Attributes.Add("Onblur", "javascript:return CheckMDate('" & txtMdate.ClientID & "')")
                txtRejectedQty.Attributes.Add("Onblur", "javascript:return ValidateStringOrNot('" & txtRecivedQty.ClientID & "','" & txtRejectedQty.ClientID & "','" & txtExcssQty.ClientID & "','" & txtAcceptedQty.ClientID & "','" & txtAcceptedQty.ClientID & "')")
                txtExcssQty.Attributes.Add("Onblur", "javascript:return CheckExcess('" & txtExcssQty.ClientID & "')")
                txtRecivedQty.Attributes.Add("Onblur", "javascript:return ConfirmMessage('" & lblPending.ClientID & "','" & txtRecivedQty.ClientID & "','" & txtAcceptedQty.ClientID & "','" & txtRejectedQty.ClientID & "','" & txtExcssQty.ClientID & "','" & lblPending.ClientID & "')")
                txtAcceptedQty.Attributes.Add("Onblur", "javascript:return Amount('" & lblPending.ClientID & "','" & txtAcceptedQty.ClientID & "','" & txtRecivedQty.ClientID & "','" & txtRejectedQty.ClientID & "','" & txtExcssQty.ClientID & "')")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInward_RowDataBound")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim Arr() As String
        Dim ObjGoods As New ClsGoodsInward
        Dim iMasterID As Integer
        Dim sCurrentMonth As String = "", sYear As String = "", sStaus As String = "", sStatus As String = "", Check As String
        Dim ddlUnit As New DropDownList
        Dim lblMRP As New Label
        Dim lblPending As New Label
        Dim lblOrderedQuantity As New Label, lblComodityId As New Label, lblDescription As Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label, UnitsID As New Label
        Dim lblReceivedQuantity As New TextBox
        Dim txtBatchNumber As New TextBox
        Dim lblAcceptedQuantity As New TextBox
        Dim lblRejectedQuantity As New TextBox
        Dim lblRejectedQuantityExcess As New TextBox
        Dim lblRemarks As New TextBox
        Dim txtManufactureDate As New TextBox
        Dim txtExpireDate As New TextBox
        Dim lblBatchNo As New TextBox
        Dim lblRate As New TextBox
        Dim row As GridViewRow
        Dim dDate, dSDate As Date : Dim m As Integer

        Dim objGoodsReturn As New clsGoodsReturn
        Dim purchaseReturnNo As String = "" : Dim dtMaster As New DataTable
        Try
            lblStatus.Text = ""

            ''Goods Return No Generation'
            'purchaseReturnNo = objGoodsReturn.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            ''Goods Return No Generation'

            If txtInvoiceDate.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Invoice Date (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Invoice Date (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            lblError.Text = ""
            If (txtDocRefNo.Text = "") Then
                lblError.Text = "Enter Document reference"
                ' btnSave.Visible = True
                Exit Sub
            Else

                If (objGIN.CheckVerifiedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, txtDocRefNo.Text)) Then
                    lblError.Text = "Already verified "
                    Exit Sub
                End If
            End If
            Check = objGIN.CheckInwardedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, txtDocRefNo.Text)
            If (Check = True) Then
                lblError.Text = "Document reference no already exist"
            Else
                If dgInward.Rows.Count > 0 Then
                    sCurrentMonthID = objGIN.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnctn.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnctn.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    ObjGoods.PGM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ObjGoods.PGM_DocRefNo = objFasGnrl.SafeSQL(txtDocRefNo.Text)
                    ObjGoods.PGM_CreatedBy = sSession.UserID
                    ObjGoods.PGM_ESugamNo = objFasGnrl.SafeSQL(txtESugamNo.Text)
                    ObjGoods.PGM_Supplier = objGIN.GetSupplierName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    If (ddlExistingDocRef.SelectedIndex > 0) Then
                        ObjGoods.PGM_DocRefNo = ddlExistingDocRef.SelectedItem.Text
                    Else
                        ObjGoods.PGM_DocRefNo = objFasGnrl.SafeSQL(txtDocRefNo.Text)
                    End If
                    ObjGoods.GIND_DCNO = txtDcNo.Text
                    ObjGoods.PGM_ID = 0
                    ObjGoods.PGM_CompID = sSession.AccessCodeID
                    ObjGoods.PGM_CrBy = sSession.UserID
                    '  ObjGoods.PGM_CrOn = DateTime.Today
                    ObjGoods.PGM_Status = "W"
                    ObjGoods.PGM_DelFlag = "X"
                    ObjGoods.PGM_YearID = sSession.YearID
                    ObjGoods.PGM_Operation = "C"
                    ObjGoods.PGM_IPAddress = sSession.IPAddress
                    ObjGoods.PGM_Gin_Number = objFasGnrl.SafeSQL(txtOrderCode.Text)
                    '  ObjGoods.PGM_ModeOfShiping = ddlModeOfShipping.SelectedItem.Text 'lblMShiping.Text
                    ObjGoods.PGM_InvoiceDate = Date.ParseExact(Trim(txtInvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ObjGoods.PGM_ESugamNo = objFasGnrl.SafeSQL(txtESugamNo.Text)
                    ObjGoods.PGM_OrderID = ddlOrderNo.SelectedValue

                    ObjGoods.PGM_BatchNo = 0
                    ObjGoods.PGM_BaseName = 0
                    ObjGoods.PGM_OrderNo = ""

                    Arr = objGIN.SaveMaster(sSession.AccessCode, ObjGoods, 0)
                    iMasterID = Arr(1)
                    For i = 0 To dgInward.Rows.Count - 1
                        lblReceivedQuantity = dgInward.Rows(i).FindControl("txtReceivedQty")
                        If (lblReceivedQuantity.Text = "") Then
                            lblReceivedQuantity.Text = 0
                        End If
                        If (Convert.ToDecimal(lblReceivedQuantity.Text) > 0) Then
                            ObjGoods.PGD_MasterID = iMasterID
                            lblMRP = dgInward.Rows(i).FindControl("lblMrp")
                            If lblMRP.Text <> "" Then
                                ObjGoods.PGD_MRP = lblMRP.Text
                            Else
                                ObjGoods.PGD_MRP = 0
                            End If

                            lblDescription = dgInward.Rows(i).FindControl("lblDescription")

                            lblUnitId = dgInward.Rows(i).FindControl("lblUnitId")
                            If lblUnitId.Text <> "" Then
                                ObjGoods.PGD_UnitID = lblUnitId.Text 'objGIN.GetUnitID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescription.Text)
                            Else
                                ObjGoods.PGD_UnitID = 0
                            End If

                            lblHistoryID = dgInward.Rows(i).FindControl("lblHistoryID")
                            If lblHistoryID.Text <> "" Then
                                ObjGoods.PGD_HistoryID = lblHistoryID.Text
                            Else
                                ObjGoods.PGD_HistoryID = 0
                            End If

                            lblItemId = dgInward.Rows(i).FindControl("lblItemId")
                            If lblItemId.Text <> "" Then
                                ObjGoods.PGD_DescriptionID = lblItemId.Text
                            End If
                            lblComodityId = dgInward.Rows(i).FindControl("lblComodityId")
                            If lblComodityId.Text <> "" Then
                                ObjGoods.PGD_CommodityID = lblComodityId.Text
                                'bjGIN.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblComodityId.Text)
                            End If

                            lblOrderedQuantity = dgInward.Rows(i).FindControl("lblOrderQty")
                            If lblOrderedQuantity.Text <> "" Then
                                ObjGoods.PGD_OrderQnt = lblOrderedQuantity.Text
                            Else
                                ObjGoods.PGD_OrderQnt = 0
                            End If

                            If lblReceivedQuantity.Text <> "" Then
                                ObjGoods.PGD_ReceivedQnt = lblReceivedQuantity.Text
                            Else
                                ObjGoods.PGD_ReceivedQnt = 0
                            End If
                            lblAcceptedQuantity = dgInward.Rows(i).FindControl("txtAcceptedQty")
                            If lblAcceptedQuantity.Text <> "" Then
                                ObjGoods.PGD_Accepted = lblAcceptedQuantity.Text
                            Else
                                ObjGoods.PGD_Accepted = 0
                            End If
                            lblRejectedQuantity = dgInward.Rows(i).FindControl("txtRejected")
                            If lblRejectedQuantity.Text <> "" Then
                                ObjGoods.PGD_RejectedQnt = lblRejectedQuantity.Text
                            Else
                                ObjGoods.PGD_RejectedQnt = 0
                            End If
                            lblRejectedQuantityExcess = dgInward.Rows(i).FindControl("txtExcessQty")
                            If lblRejectedQuantityExcess.Text <> "" Then
                                ObjGoods.PGD_Excess = lblRejectedQuantityExcess.Text
                                ObjGoods.PGD_Delflag = "E"
                            Else
                                ObjGoods.PGD_Excess = 0
                                ObjGoods.PGD_Delflag = "A"
                            End If
                            txtManufactureDate = dgInward.Rows(i).FindControl("txtMdate")
                            If txtManufactureDate.Text <> "" Then
                                ObjGoods.PGD_ManufactureDate = Date.ParseExact(Trim(txtManufactureDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Else
                                ObjGoods.PGD_ManufactureDate = "01/01/1900"
                            End If
                            txtExpireDate = dgInward.Rows(i).FindControl("txtEdate")
                            If txtExpireDate.Text <> "" Then
                                ObjGoods.PGD_ExpireDate = Date.ParseExact(Trim(txtExpireDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Else
                                ObjGoods.PGD_ExpireDate = "01/01/1900"
                            End If

                            lblPending = dgInward.Rows(i).FindControl("lblPending")

                            If lblPending.Text <> "" Then
                                ObjGoods.PGD_PendingItem = lblPending.Text
                            End If
                            ObjGoods.PGD_CompID = sSession.AccessCodeID
                            ObjGoods.PGD_Status = "W"
                            ObjGoods.PGD_Operation = "C"
                            ObjGoods.PGD_IPAddress = sSession.IPAddress
                            txtBatchNumber = dgInward.Rows(i).FindControl("txtBatchNumber")
                            If (txtBatchNumber.Text <> "") Then
                                ObjGoods.GIND_BatchNo = txtBatchNumber.Text
                            Else
                                ObjGoods.GIND_BatchNo = " "
                            End If
                            ObjGoods.PGD_OrderID = ddlOrderNo.SelectedValue
                            Arr = objGIN.SaveMasterDetails(sSession.AccessCode, ObjGoods)


                            ''Goods Return'
                            'If ObjGoods.PGD_RejectedQnt > 0 Then

                            '    'Dim historyID = objGreturn.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, GRDescriptionID)
                            '    dtMaster = ObjGoods.LoadPRDetails(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue, lblComodityId.Text, lblItemId.Text, lblHistoryID.Text)
                            '    Dim totRateAmount As Double : Dim totChargeAmount As Double
                            '    If dtMaster.Rows.Count > 0 Then

                            '        For j = 0 To dtMaster.Rows.Count - 1

                            '            If IsDBNull(dtMaster.Rows(j)("POD_Discount")) = False Then
                            '                ObjGoods.sPOD_Discount = dtMaster.Rows(j)("POD_Discount")
                            '            End If
                            '            If IsDBNull(dtMaster.Rows(j)("POD_GSTRate")) = False Then
                            '                ObjGoods.sPOD_GSTRate = dtMaster.Rows(j)("POD_GSTRate")
                            '            End If
                            '            If IsDBNull(dtMaster.Rows(j)("POD_GST_ID")) = False Then
                            '                ObjGoods.sPOD_GSTId = dtMaster.Rows(j)("POD_GST_ID")
                            '            End If
                            '        Next
                            '        'txtQuantity.Text = objGreturn.GetQuantity(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, ddlCommodity.SelectedValue, chkCategory.SelectedValue, historyID)
                            '        'hfsStateCode.Value = objGreturn.GetStateCode(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
                            '        totChargeAmount = ObjGoods.GetTotChargeAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                            '        totRateAmount = ObjGoods.GetTotRateAmount(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue, lblComodityId.Text, lblItemId.Text, lblHistoryID.Text)
                            '        ObjGoods.iRateAmount = Format(lblRejectedQuantity.Text * lblMRP.Text, "0.00")
                            '        ObjGoods.iDiscountAmount = Format((ObjGoods.iRateAmount * ObjGoods.sPOD_Discount) / 100, "0.00")
                            '        'ObjGoods.iCharges = Format((ObjGoods.iRateAmount * totChargeAmount) / totRateAmount, "0.00")
                            '        ObjGoods.iCharges = "0.00"
                            '        ObjGoods.iAmount = Format((ObjGoods.iRateAmount - ObjGoods.iDiscountAmount) + ObjGoods.iCharges, "0.00")
                            '        ObjGoods.iGSTAmount = Format(ObjGoods.sPOD_GSTRate * (ObjGoods.iRateAmount - ObjGoods.iDiscountAmount + ObjGoods.iCharges) / 100, "0.00")
                            '        ObjGoods.iTotalAmount = Format((ObjGoods.iRateAmount - ObjGoods.iDiscountAmount) + ObjGoods.iCharges + ObjGoods.iGSTAmount, "0.00")
                            '    End If

                            '    Dim GINInvID = objGIN.GetGINInvID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, txtDocRefNo.Text)

                            '    objGIN.SaveReturnMaster(sSession.AccessCode, sSession.YearID, ObjGoods, purchaseReturnNo, GINInvID)
                            '    Dim iPRMiD = objGIN.GetPRMiD(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, GINInvID, ObjGoods)
                            '    objGIN.SaveReturnDetails(sSession.AccessCode, ObjGoods, iPRMiD)
                            '    lblUserMasterDetailsValidationMsg.Text = "Goods Return created for this Purchase Order. Purchase Return Number is " & purchaseReturnNo
                            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                            'End If

                            ''Goods Return'
                        End If

                    Next
                    If Arr(0) = "2" Then
                        lblStatus.Text = "Successfully Updated"
                    ElseIf Arr(0) = "3" Then
                        lblStatus.Text = "Successfully Saved"
                    End If
                    LoadExistingInwardNo()
                    ddlExistingInwardNo.SelectedValue = iMasterID
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub ddlTypeOfSale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTypeOfSale.SelectedIndexChanged
        Try
            ' ClearAll()
            LoadCST()
            LoadVAT()
            If (ddlTypeOfSale.SelectedValue = 2) Then
                ddlCstCtgry.Enabled = True
                ddlVat.Enabled = False
                ddlCst.Enabled = True
            Else
                ddlCstCtgry.Enabled = False
                ddlVat.Enabled = True
                ddlCst.Enabled = False
                ddlCstCtgry.SelectedIndex = 0
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTypeOfSale_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlCstCtgry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCstCtgry.SelectedIndexChanged
        Try
            If (ddlCstCtgry.SelectedValue = 1) Then
                loadRegular()
            ElseIf (ddlCstCtgry.SelectedValue = 2) Then
                LoadCST()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCstCtgry_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub dgInward_PreRender(sender As Object, e As EventArgs) Handles dgInward.PreRender
        Dim dt As New DataTable
        Try
            If dgInward.Rows.Count > 0 Then
                dgInward.UseAccessibleHeader = True
                dgInward.HeaderRow.TableSection = TableRowSection.TableHeader
                dgInward.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInward_PreRender")
        End Try
    End Sub
    Private Sub imgNAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgNAdd.Click
        Dim dRequiredDate As Date
        Dim mDate As Date
        Dim EDate As Date
        Dim drNew As DataRow
        Try
            If Not Session("UpdateTab") Is Nothing Then
                dtTab = Session("UpdateTab")
            Else
                BuildTable()
            End If
            drNew = dtTab.NewRow
            objPO.sPOM_OrderNo = ddlOrderNo.SelectedValue
            objPO.iPOM_Supplier = 1
            objPO.sPOM_InwardNo = txtOrderCode.Text
            If (txtDocRefNo.Text <> "") Then
                objPO.POM_DocRef = txtDocRefNo.Text
            Else
                objPO.POM_DocRef = ddlExistingDocRef.SelectedItem.Text
            End If
            objPO.iPOM_ModeOfShipping = 1
            objPO.iPOM_CreatedBy = sSession.UserID
            objPO.iPOM_YearID = sSession.YearID
            txtMasterID.Text = ddlOrderNo.SelectedValue
            objPO.sPOD_FrieghtAmount = 0
            objPO.sPOD_Frieght = 0
            objPO.iPOD_MasterID = ddlOrderNo.SelectedValue
            objPO.iPOD_Commodity = ddlNBrand.SelectedValue

            drNew("ComodityId") = objPO.iPOD_Commodity
            objPO.iPOD_DescriptionID = ddlNItems.SelectedValue
            drNew("Comodity") = objDb.SQLGetDescription(sSession.AccessCode, "Select Inv_Description From Inventory_Master Where INV_ID='" & objPO.iPOD_Commodity & "' and Inv_CompID =" & sSession.AccessCodeID & "")
            drNew("Units") = objDb.SQLGetDescription(sSession.AccessCode, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & ddlNUnit.SelectedValue & " ")
            drNew("Descriptions") = objDb.SQLGetDescription(sSession.AccessCode, "Select Inv_Description From Inventory_Master Where INV_ID='" & objPO.iPOD_DescriptionID & "' and Inv_CompID =" & sSession.AccessCodeID & "")
            drNew("ItemId") = objPO.iPOD_DescriptionID
            If (txtRate.Text <> "") Then
                objPO.iPOD_HistoryID = txtHistoryID.Text

                objPO.sPOD_Rate = txtRate.Text
                drNew("Mrp") = objPO.sPOD_Rate
                drNew("HistoryId") = objPO.iPOD_HistoryID
            Else
                objPO.iPOD_HistoryID = ddlRate.SelectedValue
                objPO.sPOD_Rate = ddlRate.SelectedItem.Text
                drNew("Mrp") = objPO.sPOD_Rate
                drNew("HistoryId") = objPO.iPOD_HistoryID
            End If

            objPO.iPOD_Unit = ddlNUnit.SelectedValue
            drNew("UnitId") = objPO.iPOD_Unit
            If hfRejectedQty.Value <> "" Then
                objPO.POD_Rejected = Request.Form(hfRejectedQty.UniqueID)
                drNew("RejectedQty") = objPO.POD_Rejected
            Else
                objPO.POD_Rejected = "0"
                drNew("RejectedQty") = objPO.POD_Rejected
            End If

            If txtQuantity.Text <> "" Then
                objPO.POD_ReceivedQty = txtQuantity.Text
                objPO.POD_OrderedQty = txtQuantity.Text
                drNew("ReceivedQty") = objPO.POD_ReceivedQty
                drNew("OrderedQty") = objPO.POD_ReceivedQty
            Else
                objPO.POD_Rejected = 0
                objPO.POD_OrderedQty = 0
                drNew("ReceivedQty") = objPO.POD_ReceivedQty
                drNew("OrderedQty") = objPO.POD_ReceivedQty
            End If
            If txtAccepted.Text <> "" Then
                objPO.POD_Accepted = txtAccepted.Text
                drNew("AccpetedQty") = objPO.POD_Accepted
            Else
                objPO.POD_Accepted = "0"
                drNew("AccpetedQty") = objPO.POD_Accepted
            End If
            If hfRateAmount.Value <> "" Then
                objPO.sPOD_RateAmount = Request.Form(hfRateAmount.UniqueID)
            Else
                objPO.sPOD_RateAmount = txtRateAmount.Text
            End If
            objPO.sPOD_Quantity = txtQuantity.Text
            objPO.sPOD_Discount = txtDiscount.Text
            If hfDiscountAmount.Value <> "" Then
                objPO.sPOD_DiscountAmount = Request.Form(hfDiscountAmount.UniqueID)
            Else
                objPO.sPOD_DiscountAmount = txtDiscountAmount.Text
            End If
            objPO.sPOD_Excise = txtExcise.Text
            If hfExciseAmount.Value <> "" Then
                objPO.sPOD_ExciseAmount = Request.Form(hfExciseAmount.UniqueID)
            Else
                objPO.sPOD_ExciseAmount = txtExciseAmount.Text
            End If
            objPO.sPOD_VAT = ddlVat.SelectedValue
            If hfVatAmount.Value <> "" Then
                objPO.sPOD_VATAmount = Request.Form(hfVatAmount.UniqueID)
            Else
                objPO.sPOD_VATAmount = txtVatAmount.Text
            End If
            objPO.sPOD_CST = 0
            If hfCSTAmount.Value <> "" Then

                objPO.sPOD_CSTAmount = Request.Form(hfCSTAmount.UniqueID)
            Else
                objPO.sPOD_CSTAmount = txtCSTAmount.Text
            End If
            'If txtRequiredDate.Text <> "" Then
            '    dRequiredDate = Date.ParseExact(Trim(txtRequiredDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'Else
            dRequiredDate = "01/01/1900"
            'End If
            If txtMdateNew.Text <> "" Then
                mDate = Date.ParseExact(Trim(txtMdateNew.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                drNew("MDate") = objFasGnrl.FormatDtForRDBMS(txtMdateNew.Text, "D")
            Else
                mDate = "01/01/1900"
                drNew("MDate") = objFasGnrl.FormatDtForRDBMS(mDate, "D")
            End If
            If txtEdateNew.Text <> "" Then
                EDate = Date.ParseExact(Trim(txtEdateNew.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                drNew("Edate") = objFasGnrl.FormatDtForRDBMS(txtEdateNew.Text, "D")
            Else
                EDate = "01/01/1900"
                drNew("Edate") = objFasGnrl.FormatDtForRDBMS(EDate, "D")
            End If
            If hfTotalAmount.Value <> "" Then
                objPO.sPOD_TotalAmount = Request.Form(hfTotalAmount.UniqueID)
            Else
                objPO.sPOD_TotalAmount = txtTotalAmount.Text
            End If
            drNew("BatchNumber") = ""
            drNew("PendingItem") = 0
            drNew("ExcessQty") = ""
            objPO.SavePurchaseOrderDetails(sSession.AccessCode, sSession.AccessCodeID, dRequiredDate, objPO, 0)
            objGIN.SaveNewOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRequiredDate, mDate, EDate, objPO)
            txtExciseAmount.Text = ""
            dtTab.Rows.Add(drNew)
            Session("UpdateTab") = dtTab
            dgInward.DataSource = dtTab
            dgInward.DataBind()
            ClearAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgNAdd_Click")
        End Try
    End Sub
    Public Sub ClearAll()
        Try
            ddlNBrand.SelectedIndex = 0 ': ddlTypeOfSale.SelectedIndex = 0
            ddlNItems.Items.Clear() : ddlNUnit.Items.Clear() : ddlRate.Items.Clear()
            txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""
            ddlVat.SelectedIndex = 0 : txtCSTAmount.Text = "" : ddlCst.SelectedIndex = 0 : txtVatAmount.Text = ""
            txtExcise.Text = "" : txtTotalAmount.Text = "" : txtAccepted.Text = "" : txtDiscount.Text = ""
            ' ddlCstCtgry.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function BuildTable() As DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("BatchNumber", GetType(String))
            dtTab.Columns.Add(dc)
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ViewEnable()
        Try
            txtESugamNo.Enabled = True : txtInvoiceDate.Enabled = True : txtDocRefNo.Enabled = True : ddlSupplier.Enabled = True
            txtDcNo.Enabled = True : txtDocRefNo.Enabled = True
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub ViewDisable()
        Try
            txtESugamNo.Enabled = False : txtInvoiceDate.Enabled = False : txtDocRefNo.Enabled = False : ddlSupplier.Enabled = False
            txtDcNo.Enabled = False : txtDocRefNo.Enabled = False

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub ddlExistingDocRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingDocRef.SelectedIndexChanged
        Dim dt As New DataTable
        Dim inwardNo As String
        Try
            lblStatus.Text = ""
            If (ddlExistingDocRef.SelectedIndex > 0) Then
                txtDocRefNo.Text = objFasGnrl.ReplaceSafeSQL(ddlExistingDocRef.SelectedItem.Text)
                If (objGIN.CheckGinExistOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingDocRef.SelectedItem.Text, ddlOrderNo.SelectedValue)) Then
                    inwardNo = objGIN.ExistingInward(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingDocRef.SelectedItem.Text, ddlOrderNo.SelectedValue)
                    lblStatus.Text = "Alraedy Invoice Crated For:-" & txtDocRefNo.Text & " System Generated Number Is:-" & inwardNo & ""
                    dgInward.DataSource = Nothing
                    dgInward.DataBind()
                    txtDocRefNo.Text = ""
                    ddlExistingDocRef.SelectedIndex = 0
                    ViewDisable()
                Else

                    dt = objGIN.GetPODetailsFromRegister(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlExistingDocRef.SelectedValue)
                    dgInward.DataSource = dt
                    dgInward.DataBind()
                    ViewEnable()
                End If
            Else
                txtDocRefNo.Visible = True
            End If
            If (dgInward.Rows.Count > 0) Then
                dgInward.Visible = True
            Else
                dgInward.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingDocRef_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadExistingInwardDetails()
        Dim dtable As New DataTable
        Try
            dtable = objGIN.ExistingInwardDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInwardNo.SelectedValue)
            If (dtable.Rows.Count > 0) Then
                For i = 0 To dtable.Rows.Count - 1
                    txtDocRefNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_DocumentRefNo"))
                    txtInvoiceDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("PGM_InvoiceDate"), "D")
                    txtESugamNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_ESugamNo"))
                    txtOrderCode.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_Gin_Number"))
                    txtOrderDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("PGM_OrderDate"), "D")
                    ddlOrderNo.SelectedValue = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_OrderID"))
                    txtDcNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_DcNo"))
                    ddlSupplier.SelectedValue = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_Supplier"))
                    txtESugamNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_ESugamNo"))
                    lblStatus.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_Status"))
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingInwardDetails")
        End Try
    End Sub
    Public Sub Clear()
        Try
            ddlNBrand.SelectedIndex = 0
            ddlNItems.Items.Clear() : ddlNUnit.Items.Clear() : ddlRate.Items.Clear()
            txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""  'txtRequiredDate.Text = ""
            txtCSTAmount.Text = "" : ddlVat.Items.Clear() : ddlCst.Items.Clear() : txtVatAmount.Text = ""
            txtExcise.Text = "" : txtTotalAmount.Text = "" : txtAccepted.Text = "" : txtDiscount.Text = ""
            txtDocRefNo.Text = "" : txtInvoiceDate.Text = "" : txtESugamNo.Text = ""
            ddlSupplier.SelectedIndex = 0 : ddlExistingDocRef.Items.Clear() : txtDcNo.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try
            If (ddlExistingInwardNo.SelectedIndex > 0) Then

                'Goods Return'
                Dim purchaseReturnNo As String : Dim dtMaster As New DataTable : Dim ObjGoods As New ClsGoodsInward : Dim objGoodsReturn As New clsGoodsReturn
                Dim lblRejectedQuantity As New TextBox : Dim lblComodityId As New Label : Dim lblItemId As New Label : Dim lblHistoryID As New Label
                Dim lblMRP As New Label : Dim lblUnitId As New Label : Dim lblRejectedQuantityExcess As New TextBox
                purchaseReturnNo = objGoodsReturn.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)

                For i = 0 To dgInward.Rows.Count - 1
                    lblRejectedQuantity = dgInward.Rows(i).FindControl("txtRejected")
                    If lblRejectedQuantity.Text <> "" Then
                        ObjGoods.PGD_RejectedQnt = lblRejectedQuantity.Text
                    Else
                        ObjGoods.PGD_RejectedQnt = 0
                    End If

                    If ObjGoods.PGD_RejectedQnt = 0 Then
                        lblRejectedQuantityExcess = dgInward.Rows(i).FindControl("txtExcessQty")
                        If lblRejectedQuantityExcess.Text <> "" Then
                            ObjGoods.PGD_RejectedQnt = lblRejectedQuantityExcess.Text
                        Else
                            ObjGoods.PGD_RejectedQnt = 0
                        End If
                    End If

                    lblComodityId = dgInward.Rows(i).FindControl("lblComodityId")
                    If lblComodityId.Text <> "" Then
                        ObjGoods.PGD_CommodityID = lblComodityId.Text
                        'bjGIN.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblComodityId.Text)
                    End If
                    lblItemId = dgInward.Rows(i).FindControl("lblItemId")
                    If lblItemId.Text <> "" Then
                        ObjGoods.PGD_DescriptionID = lblItemId.Text
                    End If
                    lblHistoryID = dgInward.Rows(i).FindControl("lblHistoryID")
                    If lblHistoryID.Text <> "" Then
                        ObjGoods.PGD_HistoryID = lblHistoryID.Text
                    Else
                        ObjGoods.PGD_DescriptionID = 0
                    End If
                    lblMRP = dgInward.Rows(i).FindControl("lblMrp")
                    If lblMRP.Text <> "" Then
                        ObjGoods.PGD_MRP = lblMRP.Text
                    Else
                        ObjGoods.PGD_MRP = 0
                    End If
                    lblUnitId = dgInward.Rows(i).FindControl("lblUnitId")
                    If lblUnitId.Text <> "" Then
                        ObjGoods.PGD_UnitID = lblUnitId.Text
                    Else
                        ObjGoods.PGD_UnitID = 0
                    End If
                    ObjGoods.PGD_OrderID = ddlOrderNo.SelectedValue
                    ObjGoods.PGM_DocRefNo = objFasGnrl.SafeSQL(txtDocRefNo.Text)
                    ObjGoods.PGD_CompID = sSession.AccessCodeID
                    ObjGoods.PGM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ObjGoods.PGM_Supplier = objGIN.GetSupplierName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    ObjGoods.PGM_YearID = sSession.YearID
                    ObjGoods.PGM_CrBy = sSession.UserID
                    ObjGoods.PGM_IPAddress = sSession.IPAddress
                    If ObjGoods.PGD_RejectedQnt > 0 Then
                        'Dim historyID = objGreturn.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, GRDescriptionID)
                        dtMaster = ObjGoods.LoadPRDetails(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue, lblComodityId.Text, lblItemId.Text, lblHistoryID.Text)
                        Dim totRateAmount As Double : Dim totChargeAmount As Double
                        If dtMaster.Rows.Count > 0 Then
                            For j = 0 To dtMaster.Rows.Count - 1
                                If IsDBNull(dtMaster.Rows(j)("POD_Discount")) = False Then
                                    ObjGoods.sPOD_Discount = dtMaster.Rows(j)("POD_Discount")
                                End If
                                If IsDBNull(dtMaster.Rows(j)("POD_GSTRate")) = False Then
                                    ObjGoods.sPOD_GSTRate = dtMaster.Rows(j)("POD_GSTRate")
                                End If
                                If IsDBNull(dtMaster.Rows(j)("POD_GST_ID")) = False Then
                                    ObjGoods.sPOD_GSTId = dtMaster.Rows(j)("POD_GST_ID")
                                End If
                            Next
                            'txtQuantity.Text = objGreturn.GetQuantity(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, ddlCommodity.SelectedValue, chkCategory.SelectedValue, historyID)
                            'hfsStateCode.Value = objGreturn.GetStateCode(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
                            totChargeAmount = ObjGoods.GetTotChargeAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                            totRateAmount = ObjGoods.GetTotRateAmount(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue, lblComodityId.Text, lblItemId.Text, lblHistoryID.Text)
                            ObjGoods.iRateAmount = Format(ObjGoods.PGD_RejectedQnt * lblMRP.Text, "0.00")
                            ObjGoods.iDiscountAmount = Format((ObjGoods.iRateAmount * ObjGoods.sPOD_Discount) / 100, "0.00")
                            'ObjGoods.iCharges = Format((ObjGoods.iRateAmount * totChargeAmount) / totRateAmount, "0.00")
                            ObjGoods.iCharges = "0.00"
                            ObjGoods.iAmount = Format((ObjGoods.iRateAmount - ObjGoods.iDiscountAmount) + ObjGoods.iCharges, "0.00")
                            ObjGoods.iGSTAmount = Format(ObjGoods.sPOD_GSTRate * (ObjGoods.iRateAmount - ObjGoods.iDiscountAmount + ObjGoods.iCharges) / 100, "0.00")
                            ObjGoods.iTotalAmount = Format((ObjGoods.iRateAmount - ObjGoods.iDiscountAmount) + ObjGoods.iCharges + ObjGoods.iGSTAmount, "0.00")
                        End If

                        Dim GINInvID = objGIN.GetGINInvID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, txtDocRefNo.Text)

                        objGIN.SaveReturnMaster(sSession.AccessCode, sSession.YearID, ObjGoods, purchaseReturnNo, GINInvID)
                        Dim iPRMiD = objGIN.GetPRMiD(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, GINInvID, ObjGoods)
                        objGIN.SaveReturnDetails(sSession.AccessCode, ObjGoods, iPRMiD)
                        lblUserMasterDetailsValidationMsg.Text = "Goods Return created for this Purchase Order. Purchase Return Number is " & purchaseReturnNo
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)

                    End If
                Next
                'Goods Return'

                objGIN.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlExistingInwardNo.SelectedValue)
                '*** Commented Bcz Single Item Getting Added twice ***'
                'MakeTransactioPI()
                'MakeTransactionPR()
                '*** Commented Bcz Single Item Getting Added twice ***'
                lblStatus.Text = "Sucessfully Approved"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Public Sub MakeTransactioPI()
        Dim ObjGoods As New ClsGoodsInward
        Dim Arr() As String
        Dim sCurrentMonth As String = "", sYear As String = "", sCheckAcceptedQTY As String = "", sStr As String = ""
        Dim ddlUnit As New DropDownList
        Dim lblAcceptedQuantity As New TextBox
        Dim lblOrderedQuentity As New Label
        Dim lblReceivedQuentity As New TextBox
        Dim lblExcessQuentity As New TextBox
        Dim lblRemarks As New TextBox
        Dim lblRate As New TextBox
        Dim lblMRP As New Label
        Dim lblComodityId As New Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label
        Dim j As Integer
        Dim ssql As String = ""
        Try
            For j = 0 To dgInward.Rows.Count - 1
                lblReceivedQuentity = dgInward.Rows(j).FindControl("txtReceivedQty")
                If (lblReceivedQuentity.Text = "") Then
                    lblReceivedQuentity.Text = 0
                End If
                If (Convert.ToDecimal(lblReceivedQuentity.Text) > 0) Then
                    sCurrentMonthID = objGIN.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnctn.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnctn.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                    lblOrderedQuentity = dgInward.Rows(j).FindControl("lblOrderQty")

                    lblMRP = dgInward.Rows(j).FindControl("lblMRP")
                    lblOrderedQuentity = dgInward.Rows(j).FindControl("lblOrderQty")
                    If lblOrderedQuentity.Text <> "" Then
                        ObjGoods.PGD_OrderQnt = lblOrderedQuentity.Text
                    Else
                        ObjGoods.PGD_OrderQnt = 0
                    End If
                    lblReceivedQuentity = dgInward.Rows(j).FindControl("txtReceivedQty")
                    If lblReceivedQuentity.Text <> "" Then
                        ObjGoods.PGD_ReceivedQnt = lblReceivedQuentity.Text
                    Else
                        ObjGoods.PGD_ReceivedQnt = 0
                    End If
                    lblAcceptedQuantity = dgInward.Rows(j).FindControl("txtAcceptedQty")
                    If lblAcceptedQuantity.Text <> "" Then
                        ObjGoods.PGD_Accepted = lblAcceptedQuantity.Text
                    Else
                        ObjGoods.PGD_Accepted = 0
                    End If
                    lblExcessQuentity = dgInward.Rows(j).FindControl("txtExcessQty")
                    If lblExcessQuentity.Text <> "" Then
                        ObjGoods.PGD_Excess = lblExcessQuentity.Text
                        ObjGoods.PGD_Status = "W"
                    Else
                        ObjGoods.PGD_Excess = 0
                        ObjGoods.PGD_Status = "A"
                    End If
                    ObjGoods.PGM_Gin_Number = ddlExistingInwardNo.SelectedValue

                    lblComodityId = dgInward.Rows(j).FindControl("lblComodityId")
                    If lblComodityId.Text <> "" Then
                        ObjGoods.PGD_CommodityID = lblComodityId.Text
                    Else
                        ObjGoods.PGD_CommodityID = 0
                    End If
                    ObjGoods.PGD_CompID = sSession.AccessCodeID
                    lblHistoryID = dgInward.Rows(j).FindControl("lblHistoryID")
                    If lblHistoryID.Text <> "" Then
                        ObjGoods.PGD_HistoryID = lblHistoryID.Text
                    Else
                        ObjGoods.PGD_HistoryID = 0
                    End If

                    ObjGoods.PGD_OrderID = ddlOrderNo.SelectedValue
                    lblUnitId = dgInward.Rows(j).FindControl("lblUnitId")
                    If lblUnitId.Text <> "" Then
                        ObjGoods.PGD_UnitID = lblUnitId.Text
                    Else
                        ObjGoods.PGD_UnitID = 0
                    End If

                    lblItemId = dgInward.Rows(j).FindControl("lblItemId")
                    If lblItemId.Text <> "" Then
                        ObjGoods.PGD_DescriptionID = lblItemId.Text
                    Else
                        ObjGoods.PGD_DescriptionID = 0
                    End If

                    lblMRP = dgInward.Rows(j).FindControl("lblMRP")
                    If lblMRP.Text <> "" Then
                        ObjGoods.PGD_MRP = lblMRP.Text
                    Else
                        ObjGoods.PGD_MRP = 0
                    End If
                    If txtDocRefNo.Text <> "" Then
                        ObjGoods.PGM_DocRefNo = txtDocRefNo.Text
                    Else
                        ObjGoods.PGM_DocRefNo = 0
                    End If


                    Arr = objGIN.SaveTransactionInvoiceDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ObjGoods)
                    If Arr(0) = "2" Then
                        lblStatus.Text = "Successfully Approved"
                    ElseIf Arr(0) = "3" Then
                        lblStatus.Text = "Successfully Approved"
                    End If
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MakeTransactioPI")
        End Try
    End Sub

    'Public Sub MakeTransactionExcess()
    '    Dim ObjGoods As New ClsGoodsInward
    '    'Dim objOrder As New clsCustomerOrder
    '    Dim sCurrentMonth As String = "", sYear As String = "", sCheckAcceptedQTY As String = "", sStr As String = ""
    '    Dim ddlUnit As New DropDownList
    '    Dim lblAcceptedQuantity As New TextBox
    '    Dim lblOrderedQuentity As New Label
    '    Dim lblReceivedQuentity As New TextBox
    '    Dim lblExcessQuentity As New TextBox
    '    Dim lblRemarks As New TextBox
    '    Dim lblRate As New TextBox
    '    Dim lblMRP As New Label
    '    Dim j As Integer
    '    Dim ssql As String = ""
    '    Dim iMax As Integer = 0, excessQnt As Integer
    '    Try
    '        For j = 0 To dgInward.Rows.Count - 1

    '            lblOrderedQuentity = dgInward.Rows(j).FindControl("lblOrderQty")
    '            lblReceivedQuentity = dgInward.Rows(j).FindControl("txtReceivedQty")
    '            lblExcessQuentity = dgInwardDetails.Items(j).FindControl("txtExcessQty")
    '            lblAcceptedQuantity = dgInwardDetails.Items(j).FindControl("txtAcceptedQty")
    '            If (lblExcessQuentity.Text = "") Then
    '                excessQnt = 0
    '            Else
    '                excessQnt = lblExcessQuentity.Text
    '            End If
    '            If (excessQnt > 0) Then
    '                iMax = clsTRACeGeneral.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Purchase_Invoice_Excess", "PIE_ID", "PIE_CompID")
    '                ssql = "" : ssql = "Insert into Purchase_Invoice_Excess(PIE_ID,PIE_CommodityID,"
    '                ssql = ssql & "PIE_HistoryID,PIE_Rate,PIE_RateAmount,"
    '                ssql = ssql & "PIE_Quantity,PIE_Discount,PIE_DiscountAmount,PIE_Excise,"
    '                ssql = ssql & "PIE_ExciseAmount,PIE_VAT,PIE_VATAmount,"
    '                ssql = ssql & "PIE_TotalAmount,PIE_CompID,PIE_UnitID,PIE_Rejected,PIE_AcceptQty,PIE_DocRef,PIE_OrderID,PIE_Description,PIE_GINID,PIE_OrderedQty,PIE_RecivedQty,PIE_ManufactureDate,PIE_ExpireDate,PIE_Status,PIE_Delflag)"
    '                ssql = ssql & "Values(" & iMax & "," & dgInwardDetails.Items(j).Cells(1).Text & ","
    '                ssql = ssql & "" & dgInwardDetails.Items(j).Cells(2).Text & ",'" & dgInwardDetails.Items(j).Cells(7).Text & "','" & dgInwardDetails.Items(j).Cells(7).Text & "',"
    '                ssql = ssql & "'" & lblReceivedQuentity.Text & "','" & lblReceivedQuentity.Text & "','" & lblReceivedQuentity.Text & "','" & lblReceivedQuentity.Text & "',"
    '                ssql = ssql & "'" & lblReceivedQuentity.Text & "','" & lblReceivedQuentity.Text & "','" & lblReceivedQuentity.Text & "',"
    '                ssql = ssql & "'" & lblReceivedQuentity.Text & "'," & sSession.AccessCodeID & ",'" & lblAcceptedQuantity.Text & "','" & txtDocRefNo.Text & "','" & ddlOrderNo.SelectedValue & "','" & ddlExistingInwardNo.SelectedValue & "','" & dgInwardDetails.Items(j).Cells(1).Text & "','" & lblOrderedQuentity.Text & "','" & lblReceivedQuentity.Text & "'," & lblReceivedQuentity.Text & "," & lblReceivedQuentity.Text & ",'" & Date.Today & "','" & Date.Today & "','E','W')"
    '                DBHelper.ExecuteNoNQuery(sSession.AccessCode, ssql)
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub MakeTransactionPR()
        Dim ObjGoods As New ClsGoodsInward
        Dim Arr() As String
        Dim sCurrentMonth As String = "", sYear As String = "", sCheckAcceptedQTY As String = "", sStr As String = "", ssql As String = ""
        Dim j As Integer
        Dim ddlUnit As New DropDownList
        Dim lblAcceptedQuantity As New TextBox
        Dim lblOrderedQuentity As New Label
        Dim lblReceivedQuentity As New TextBox
        Dim lblRejectedQty As New TextBox
        Dim lblComodityId As New Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label
        Dim lblRemarks As New TextBox
        Dim lblRate As New TextBox
        Dim lblMRP As New Label
        Try
            For j = 0 To dgInward.Rows.Count - 1
                lblReceivedQuentity = dgInward.Rows(j).FindControl("txtReceivedQty")
                If (lblReceivedQuentity.Text = "") Then
                    lblReceivedQuentity.Text = 0
                End If

                If (Convert.ToDecimal(lblReceivedQuentity.Text) > 0) Then
                    sCurrentMonthID = objGIN.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnctn.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnctn.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    lblOrderedQuentity = dgInward.Rows(j).FindControl("lblOrderQty")
                    'lblReceivedQuentity = dgInward.Rows(j).FindControl("txtReceivedQty")
                    lblRejectedQty = dgInward.Rows(j).FindControl("txtRejected")
                    lblMRP = dgInward.Rows(j).FindControl("lblMRP")
                    lblOrderedQuentity = dgInward.Rows(j).FindControl("lblOrderQty")
                    If lblOrderedQuentity.Text <> "" Then
                        ObjGoods.PGD_OrderQnt = lblOrderedQuentity.Text
                    Else
                        ObjGoods.PGD_OrderQnt = 0
                    End If
                    lblReceivedQuentity = dgInward.Rows(j).FindControl("txtReceivedQty")
                    If lblReceivedQuentity.Text <> "" Then
                        ObjGoods.PGD_ReceivedQnt = lblReceivedQuentity.Text
                    Else
                        ObjGoods.PGD_ReceivedQnt = 0
                    End If
                    lblAcceptedQuantity = dgInward.Rows(j).FindControl("txtAcceptedQty")
                    If lblAcceptedQuantity.Text <> "" Then
                        ObjGoods.PGD_Accepted = lblAcceptedQuantity.Text
                    Else
                        ObjGoods.PGD_Accepted = 0
                    End If
                    If lblRejectedQty.Text <> "" Then
                        ObjGoods.PGD_RejectedQnt = lblRejectedQty.Text
                    Else
                        ObjGoods.PGD_RejectedQnt = 0
                    End If
                    lblComodityId = dgInward.Rows(j).FindControl("lblComodityId")
                    If lblComodityId.Text <> "" Then
                        ObjGoods.PGD_CommodityID = lblComodityId.Text
                    Else
                        ObjGoods.PGD_CommodityID = 0
                    End If
                    ObjGoods.PGD_CompID = sSession.AccessCodeID
                    lblHistoryID = dgInward.Rows(j).FindControl("lblHistoryID")
                    If lblHistoryID.Text <> "" Then
                        ObjGoods.PGD_HistoryID = lblHistoryID.Text
                    Else
                        ObjGoods.PGD_HistoryID = 0
                    End If
                    ObjGoods.PGD_OrderID = ddlOrderNo.SelectedValue

                    lblUnitId = dgInward.Rows(j).FindControl("lblUnitId")
                    If lblUnitId.Text <> "" Then
                        ObjGoods.PGD_UnitID = lblUnitId.Text
                    Else
                        ObjGoods.PGD_UnitID = 0
                    End If
                    lblItemId = dgInward.Rows(j).FindControl("lblItemId")
                    If lblItemId.Text <> "" Then
                        ObjGoods.PGD_DescriptionID = lblItemId.Text
                    Else
                        ObjGoods.PGD_DescriptionID = 0
                    End If
                    lblMRP = dgInward.Rows(j).FindControl("lblMRP")
                    If lblMRP.Text <> "" Then
                        ObjGoods.PGD_MRP = lblMRP.Text
                    Else
                        ObjGoods.PGD_MRP = 0
                    End If

                    If txtDocRefNo.Text <> "" Then
                        ObjGoods.PGM_DocRefNo = txtDocRefNo.Text
                    Else
                        ObjGoods.PGM_DocRefNo = 0
                    End If


                    ObjGoods.PGD_Status = "w"
                    ObjGoods.PGD_OrderID = ddlOrderNo.SelectedValue
                    ObjGoods.PGM_ID = ddlExistingInwardNo.SelectedValue
                    Arr = objGIN.SaveTransactionReturnsDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ObjGoods)
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MakeTransactionPR")
        End Try
    End Sub
    Private Sub imgRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgRefresh.Click
        Try
            ddlNBrand.SelectedIndex = 0
            ddlNItems.Items.Clear() : ddlNUnit.Items.Clear() : ddlRate.Items.Clear()
            txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""  'txtRequiredDate.Text = ""
            txtCSTAmount.Text = "" : ddlVat.Items.Clear() : ddlCst.Items.Clear() : txtVatAmount.Text = ""
            txtExcise.Text = "" : txtTotalAmount.Text = "" : txtAccepted.Text = "" : txtDiscount.Text = ""
            txtDocRefNo.Text = "" : txtInvoiceDate.Text = "" : txtESugamNo.Text = ""
            ddlSupplier.SelectedIndex = 0 : ddlExistingDocRef.Items.Clear() : txtDcNo.Text = ""
            dgInward.DataSource = Nothing
            dgInward.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgRefresh_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Command(sender As Object, e As CommandEventArgs) Handles imgbtnAdd.Command

    End Sub
    Private Sub dgInward_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgInward.RowCommand
        Dim chkSelect As New CheckBox
        Dim txtReceivedQty As New TextBox
        Dim txtAcceptedQty As New TextBox
        Dim txtRejected As New TextBox
        Dim txtExcessQty As New TextBox
        Dim lblComodityId As New Label
        Dim dt As New DataTable
        Try

            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblComodityId = DirectCast(clickedRow.FindControl("lblComodityId"), Label)
            If e.CommandName = "Edit" Then

                txtAcceptedQty = DirectCast(clickedRow.FindControl("txtAcceptedQty"), TextBox)
                txtReceivedQty = DirectCast(clickedRow.FindControl("txtReceivedQty"), TextBox)
                txtRejected = DirectCast(clickedRow.FindControl("txtRejected"), TextBox)
                txtExcessQty = DirectCast(clickedRow.FindControl("txtExcessQty"), TextBox)

                txtReceivedQty.Enabled = True
                txtAcceptedQty.Enabled = True
                txtRejected.Enabled = True
                txtExcessQty.Enabled = True

                txtReceivedQty.Text = String.Empty
                txtAcceptedQty.Text = String.Empty
                txtRejected.Text = String.Empty
                txtExcessQty.Text = String.Empty
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgInward_RowCommand")
        End Try
    End Sub
    Private Sub dgInward_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgInward.RowEditing
        Try

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
