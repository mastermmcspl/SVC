Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Sales_Allocation
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_Allocation"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objAllocate As New clsAllocateSalesOrder
    Private Shared sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Dim objPO As New clsPurchaseOrder
    Dim iDefaultBranch As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        'imgbtnSave.ImageUrl = "~/Images/Save24.png"
        'imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        'imgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnBack.ImageUrl = "~/Images/BackWard24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim txtorderqty As New TextBox
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "ALLO")
                imgbtnSave.Visible = False : imgbtnAdd.Visible = False : ibtnInsert.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        ibtnInsert.Visible = True
                    End If
                End If
                'imgbtnSave.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasAll", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True
                '    End If
                'End If

                'imgbtnUpdate.Visible = False
                'imgbtnApprove.Visible = False
                pnlGrand.Visible = False
                'CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                Me.btnReject.Attributes.Add("OnClick", "return validateRemarks()")

                lblStatus.Text = ""
                LoadParty()
                ddlParty.Enabled = False

                'LoadExistingAllocateCode(0)
                iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                ExistingAllocationNo(0)

                GenerateOrderCode()
                LoadOrderNo(iDefaultBranch)

                'Me.btnPlaceOrder.Attributes.Add("OnClick", "return validatePlaceOrder()")
                Me.imgbtnSave.Attributes.Add("OnClick", "return validatePlaceOrder()")
                'Me.imgbtnApprove.Attributes.Add("OnClick", "return validateApprove()")
                Me.txtGrandDiscount.Attributes.Add("onblur", "return CalculateGrandDiscount()")

                lblReAllocateID.Text = 0

                Dim iAID As String = "" : Dim iDashBoard As String = ""
                iDashBoard = Request.QueryString("sStrID")
                If iDashBoard = "1" Then
                    iAID = objGen.DecryptQueryString(Request.QueryString("AID"))
                    If iAID <> "AddNew" Then
                        'ExistingAllocationNo(0)
                        ddlSearch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("AID"))
                        ddlSearch_SelectedIndexChanged(sender, e)
                    Else

                    End If
                ElseIf iDashBoard = "0" Then

                End If

                If iDashBoard = "" Then
                    Dim iAllocateID As String = ""
                    iAllocateID = objGen.DecryptQueryString(Request.QueryString("AllocationID"))
                    If iAllocateID <> "" Then
                        lblReAllocateID.Text = iAllocateID
                        ddlSearch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("AllocationID"))
                        ddlSearch_SelectedIndexChanged(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub GenerateOrderCode()
        Try
            txtOrderCode.Text = objAllocate.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCode")
        End Try
    End Sub
    Public Sub LoadOrderNo(ByVal iBranch As Integer)
        Try
            ddlOrderNo.DataSource = objAllocate.LoadOrderNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBranch)
            ddlOrderNo.DataTextField = "SPO_OrderCode"
            ddlOrderNo.DataValueField = "SPO_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "Select Order No")
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOrderNo")
        End Try
    End Sub
    Public Sub LoadParty()
        Try
            'ddlParty.DataSource = objAllocate.LoadParty(sSession.AccessCode, sSession.AccessCodeID)
            'ddlParty.DataTextField = "ACM_Name"
            'ddlParty.DataValueField = "ACM_ID"
            'ddlParty.DataBind()
            'ddlParty.Items.Insert(0, "--- Select Party ---")

            ddlParty.DataSource = objAllocate.LoadParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "BM_Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Party")
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadParty")
        End Try
    End Sub
    Public Sub CalculateGrandDiscount()
        Dim dTotal As Double
        Dim lblNetAmount As New Label
        Dim dGrandDiscount, dGrandDiscountAmt, dGrandTotal, dGrandTotalAmt As Double
        Try
            If dgAllocate.Rows.Count > 0 Then
                For i = 0 To dgAllocate.Rows.Count - 1
                    lblNetAmount = dgAllocate.Rows(i).FindControl("lblNetAmount")
                    dTotal = dTotal + lblNetAmount.Text
                Next
            End If

            txtGrandTotal.Text = dTotal
            If txtGrandDiscount.Text <> "" Then
                txtGrandDiscountAmt.Text = (txtGrandTotal.Text * txtGrandDiscount.Text) / 100
                txtGrandTotalAmt.Text = dTotal - txtGrandDiscountAmt.Text
            Else
                txtGrandTotalAmt.Text = txtGrandTotal.Text
            End If

            If txtGrandDiscount.Text <> "" Then
                dGrandDiscount = txtGrandDiscount.Text
            End If
            If txtGrandDiscountAmt.Text <> "" Then
                dGrandDiscountAmt = txtGrandDiscountAmt.Text
            End If
            If txtGrandTotal.Text <> "" Then
                dGrandTotal = txtGrandTotal.Text
            End If
            If txtGrandTotalAmt.Text <> "" Then
                dGrandTotalAmt = txtGrandTotalAmt.Text
            End If

            If ddlSearch.SelectedIndex > 0 Then
                objAllocate.SaveGrandTotalToOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, ddlSearch.SelectedValue, dGrandDiscount, dGrandDiscountAmt, dGrandTotal, dGrandTotalAmt)
                lblErrorUp.Text = "Saved Successfully."
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If

        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateGrandDiscount")
        End Try
    End Sub
    Private Sub ibtnInsert_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnInsert.Click
        Dim dGrandDiscount, dGrandDiscountAmt, dGrandTotal, dGrandTotalAmt As Double
        Try
            If ddlOrderNo.SelectedIndex > 0 Then
                If txtGrandDiscount.Text <> "" Then
                    dGrandDiscount = txtGrandDiscount.Text
                End If
                If txtGrandDiscountAmt.Text <> "" Then
                    dGrandDiscountAmt = txtGrandDiscountAmt.Text
                End If
                If txtGrandTotal.Text <> "" Then
                    dGrandTotal = txtGrandTotal.Text
                End If
                If txtGrandTotalAmt.Text <> "" Then
                    dGrandTotalAmt = txtGrandTotalAmt.Text
                End If

                objAllocate.SaveGrandTotalToOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, ddlOrderNo.SelectedValue, dGrandDiscount, dGrandDiscountAmt, dGrandTotal, dGrandTotalAmt)
                lblErrorUp.Text = "Saved Successfully."
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                'txtGrandTotal.Text = "" : txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotalAmt.Text = ""
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ibtnInsert_Click")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim objAllocate As New clsAllocateSalesOrder
        Dim Arr() As String, OrderNo As String = ""
        Dim lblopeningbalance As New Label
        Dim lblClosingStockValue As New Label
        Dim lblMrp As New Label
        Dim lbltotalamount As New Label
        Dim txtplacedqty As New TextBox
        Dim txtdiscount As New TextBox
        Dim iMasterID As Integer = 0, sId As Integer = 0, AllocateDetailsID As Integer = 0
        Dim iMRP As Decimal = 0, iDiscount As Decimal = 0, iDiscountAmt As Decimal = 0, itotal As Decimal = 0
        Dim iopeningbal As Integer = 0
        Dim ordqty As Integer = 0, closingstock As Integer = 0
        Dim dtTab As New DataTable
        Dim sYear As String = ""
        Dim sStatus As String = ""
        Dim VATAmount As Double

        Dim sItemINVVat As String = "" : Dim sBasicAmount As Double
        Dim sVATAMT As Double

        Dim lbltotalamt, lblNetAmount, lblClosingStock As New Label
        Dim lblExiceAmount As New TextBox
        Dim lblPendingQty As New Label

        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblOrderQuantity, lblOrderedAmount, lblPRODiscount, lblPRODiscountAmount, lblPROTotalAmount As New Label
        Dim bCheck As String = ""
        Try

            lblErrorUp.Text = ""
            If ddlOrderNo.SelectedIndex > 0 Then

                If ddlSearch.SelectedIndex > 0 Then
                    bCheck = objAllocate.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                    If bCheck = True Then
                        lblErrorUp.Text = "Selected Allocation No has been dispatched, it can not be edit."
                        lblCustomerValidationMsg.Text = lblErrorUp.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        'imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                        'btnReject.Enabled = False
                        Exit Sub
                    End If
                End If

                objAllocate.SAM_ID = sId
                objAllocate.SAM_OrderNo = objGen.SafeSQL(Trim(ddlOrderNo.SelectedValue))
                objAllocate.SAM_Party = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
                objAllocate.SAM_Remarks = objGen.SafeSQL(Trim(txtRemarks.Text))
                objAllocate.SAM_Status = "W"
                objAllocate.SAM_CompID = sSession.AccessCodeID
                objAllocate.SAM_YearID = sSession.YearID
                objAllocate.SAM_CreatedBy = sSession.UserID
                objAllocate.SAM_Operation = "C"
                objAllocate.SAM_IPAddress = sSession.IPAddress

                If txtGrandDiscount.Text <> "" Then
                    objAllocate.SAM_GrandDiscount = txtGrandDiscount.Text
                End If
                If txtGrandDiscountAmt.Text <> "" Then
                    objAllocate.SAM_GrandDiscountAmt = txtGrandDiscountAmt.Text
                End If
                If txtGrandTotal.Text <> "" Then
                    objAllocate.SAM_GrandTotal = txtGrandTotal.Text
                End If
                If txtGrandTotalAmt.Text <> "" Then
                    objAllocate.SAM_GrandTotalAmt = txtGrandTotalAmt.Text
                End If
                objAllocate.SAM_DispatchFlag = 0

                If txtOrderCode.Text <> "" Then
                    objAllocate.SAM_Code = txtOrderCode.Text
                End If
                Arr = objAllocate.SaveAllocateOrder(sSession.AccessCode, objAllocate)
                iMasterID = Arr(1)

                If dgAllocate.Rows.Count > 0 Then
                    For i = 0 To dgAllocate.Rows.Count - 1
                        txtplacedqty = dgAllocate.Rows(i).FindControl("txtplacedqty")
                        If (txtplacedqty.Text <> String.Empty) Then 'If Qty is entered
                            If (txtplacedqty.Text > 0) Then

                                lblCommodityID = dgAllocate.Rows(i).FindControl("lblCommodityID")
                                lblItemID = dgAllocate.Rows(i).FindControl("lblItemID")
                                lblHistoryID = dgAllocate.Rows(i).FindControl("lblHistoryID")
                                lblUnitID = dgAllocate.Rows(i).FindControl("lblUnitID")

                                objAllocate.SAD_Commodity = objGen.SafeSQL(lblCommodityID.Text)
                                objAllocate.SAD_DescID = objGen.SafeSQL(lblItemID.Text)
                                objAllocate.SAD_HisotryID = objGen.SafeSQL(lblHistoryID.Text)
                                objAllocate.SAD_UnitID = objGen.SafeSQL(lblUnitID.Text)

                                lblopeningbalance = dgAllocate.Rows(i).FindControl("lblAvailableStock")
                                objAllocate.SAD_OpeningBal = objGen.SafeSQL(lblopeningbalance.Text)

                                lblMrp = dgAllocate.Rows(i).FindControl("lblMRP")
                                objAllocate.SAD_MRP = objGen.SafeSQL(lblMrp.Text)
                                iMRP = Convert.ToDecimal(lblMrp.Text)

                                lblOrderQuantity = dgAllocate.Rows(i).FindControl("lblOrderQuantity")
                                lblOrderedAmount = dgAllocate.Rows(i).FindControl("lblOrderedAmount")
                                lblPRODiscount = dgAllocate.Rows(i).FindControl("lblPRODiscount")
                                lblPRODiscountAmount = dgAllocate.Rows(i).FindControl("lblPRODiscountAmount")
                                lblPROTotalAmount = dgAllocate.Rows(i).FindControl("lblPROTotalAmount")

                                objAllocate.SAD_OrderQnt = objGen.SafeSQL(lblOrderQuantity.Text)
                                objAllocate.SAD_OrderAmount = objGen.SafeSQL(lblOrderedAmount.Text)

                                objAllocate.SAD_Discount = objGen.SafeSQL(lblPRODiscount.Text)
                                objAllocate.SAD_DiscountAmount = objGen.SafeSQL(lblPRODiscountAmount.Text)

                                objAllocate.SAD_TotalAmount = objGen.SafeSQL(lblPROTotalAmount.Text)

                                txtplacedqty = dgAllocate.Rows(i).FindControl("txtplacedqty")
                                If txtplacedqty.Text <> "" Then
                                    objAllocate.SAD_PlacedQnt = objGen.SafeSQL(txtplacedqty.Text)
                                Else
                                    objAllocate.SAD_PlacedQnt = 0
                                End If

                                'lbltotalamt = dgAllocate.Items(i).FindControl("lblTotal")
                                'objAllocate.SAD_PlacedQntAmount = lbltotalamt.Text

                                objAllocate.SAD_PlacedQntAmount = ((lblMrp.Text) * (objAllocate.SAD_PlacedQnt))

                                lblPendingQty = dgAllocate.Rows(i).FindControl("lblPendingQty")

                                objAllocate.SAD_PlacedDiscount = 0
                                objAllocate.SAD_PlacedDiscountAmount = 0
                                objAllocate.SAD_VAT = 0
                                objAllocate.SAD_VATAmount = 0
                                objAllocate.SAD_CST = 0
                                objAllocate.SAD_CSTAmount = 0
                                objAllocate.SAD_Exice = 0
                                objAllocate.SAD_ExiceAmount = 0

                                If txtplacedqty.Text <> "" Then
                                    objAllocate.SAD_PlacedQntAmount = ((lblMrp.Text) * (objAllocate.SAD_PlacedQnt))
                                    objAllocate.SAD_PlacedTotalAmount = ((lblMrp.Text) * (objAllocate.SAD_PlacedQnt))
                                End If

                                objAllocate.SAD_ClosingBal = (lblopeningbalance.Text - objAllocate.SAD_PlacedQnt)

                                'objAllocate.SAD_PendingQty = (lblPendingQty.Text - objAllocate.SAD_PlacedQnt)
                                objAllocate.SAD_PendingQty = (lblOrderQuantity.Text - objAllocate.SAD_PlacedQnt)

                                objAllocate.SAD_CompID = sSession.AccessCodeID
                                objAllocate.SAD_YearID = sSession.YearID
                                objAllocate.SAD_MasterID = iMasterID
                                objAllocate.SAD_Operation = "C"
                                objAllocate.SAD_IPAddress = sSession.IPAddress
                                Arr = objAllocate.SaveAllocateDetails(sSession.AccessCode, objAllocate)

                            End If
                        End If
                    Next

                    'LoadExistingAllocateCode(ddlOrderNo.SelectedValue)
                    ExistingAllocationNo(ddlOrderNo.SelectedValue)

                    'ddlOrderNo_SelectedIndexChanged(sender, e)

                    ddlSearch.SelectedValue = iMasterID
                    ddlSearch_SelectedIndexChanged(sender, e)
                    'GrandDiscount'
                    CalculateGrandDiscount()
                    'GrandDiscount'
                    'imgbtnSave.Visible = False
                    If Arr(0) = "2" Then
                        lblErrorUp.Text = "Successfully Updated"
                        lblCustomerValidationMsg.Text = lblErrorUp.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

                    ElseIf Arr(0) = "3" Then
                        lblErrorUp.Text = "Successfully Saved"
                        lblCustomerValidationMsg.Text = lblErrorUp.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

                    End If
                End If
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Public Sub ExistingAllocationNo(ByVal iOrderID As Integer)
        Dim dt As New DataTable
        Try
            iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
            dt = objAllocate.BindAllocationCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, iDefaultBranch)
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "SAM_Code"
            ddlSearch.DataValueField = "SAM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Allocation No")
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ExistingAllocationNo")
        End Try
    End Sub
    Public Sub LoadExistingAllocateCode(ByVal iOrderID As Integer)
        Dim dt As New DataTable
        Try
            dt = objAllocate.BindExistingCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "SAM_Code"
            ddlSearch.DataValueField = "SAM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Allocation No")
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingAllocateCode")
        End Try
    End Sub
    Private Sub ddlOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrderNo.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Dim bCheck As String = ""
        Dim iTotal As Double
        Dim iGrandTotal As Double
        Dim dtGetAllocateMasterData As New DataTable
        Dim dtOrderMaster As New DataTable
        Dim txtplacedqty As New TextBox
        Try
            lblErrorUp.Text = ""
            txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotal.Text = "" : txtGrandTotalAmt.Text = ""
            If ddlOrderNo.SelectedIndex > 0 Then
                'LoadExistingAllocateCode(ddlOrderNo.SelectedValue)
                ExistingAllocationNo(ddlOrderNo.SelectedValue)
                bCheck = objAllocate.CheckForAllocation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                If bCheck = True Then
                    dtGetAllocateMasterData = objAllocate.GetAllocateMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlOrderNo.SelectedValue)
                    If dtGetAllocateMasterData.Rows.Count > 0 Then
                        For m = 0 To dtGetAllocateMasterData.Rows.Count - 1
                            ddlParty.SelectedValue = dtGetAllocateMasterData.Rows(m)("SAM_Party")
                            txtRemarks.Text = ""
                            txtCode.Value = objAllocate.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)

                            txtGrandDiscount.Text = ""
                            txtGrandDiscountAmt.Text = ""
                            txtGrandTotal.Text = ""
                            txtGrandTotalAmt.Text = ""
                        Next
                    End If

                    dgAllocate.DataSource = Nothing
                    dgAllocate.DataBind()

                    dt = objAllocate.BindGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlOrderNo.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        dgAllocate.DataSource = dt
                        dgAllocate.DataBind()
                        DisableAll()
                    Else
                        lblErrorUp.Text = "All Items under this order no has been allocated, No pending qty to allocate."
                        lblCustomerValidationMsg.Text = lblErrorUp.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                Else
                    dtOrderMaster = objAllocate.GetOrderMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    If dtOrderMaster.Rows.Count > 0 Then
                        For s = 0 To dtOrderMaster.Rows.Count - 1
                            ddlParty.SelectedValue = dtOrderMaster.Rows(s)("SPO_PartyName")
                            txtRemarks.Text = ""
                            txtCode.Value = objAllocate.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)

                            If IsDBNull(dtOrderMaster.Rows(s)("SPO_GrandDiscount")) = False Then
                                txtGrandDiscount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dtOrderMaster.Rows(s)("SPO_GrandDiscount")))
                            Else
                                txtGrandDiscount.Text = ""
                            End If
                            If IsDBNull(dtOrderMaster.Rows(s)("SPO_GrandDiscountAmt")) = False Then
                                txtGrandDiscountAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(dtOrderMaster.Rows(s)("SPO_GrandDiscountAmt")))
                            Else
                                txtGrandDiscountAmt.Text = ""
                            End If
                            If IsDBNull(dtOrderMaster.Rows(s)("SPO_GrandTotal")) = False Then
                                txtGrandTotal.Text = String.Format("{0:0.00}", Convert.ToDecimal(dtOrderMaster.Rows(s)("SPO_GrandTotal")))
                            Else
                                txtGrandTotal.Text = ""
                            End If
                            If IsDBNull(dtOrderMaster.Rows(s)("SPO_GrandTotalAmt")) = False Then
                                txtGrandTotalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(dtOrderMaster.Rows(s)("SPO_GrandTotalAmt")))
                            Else
                                txtGrandTotalAmt.Text = ""
                            End If
                        Next
                    End If

                    dt = objAllocate.BindProFormaGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    dgAllocate.DataSource = dt
                    dgAllocate.DataBind()

                End If

                pnlGrand.Visible = True

                For i = 0 To dgAllocate.Rows.Count - 1
                    txtplacedqty = dgAllocate.Rows(i).Cells(13).FindControl("txtplacedqty")
                    txtplacedqty.Enabled = True
                Next
            Else
                dgAllocate.Visible = False
                ddlParty.Items.Clear()
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlOrderNo_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub DisableAll()
        Dim txtplacedqty As New TextBox
        Try
            For i = 0 To dgAllocate.Rows.Count - 1
                txtplacedqty = dgAllocate.Rows(i).Cells(13).FindControl("txtplacedqty")
                txtplacedqty.Enabled = True
            Next
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DisableAll")
        End Try
    End Sub
    Public Sub EnableAll()
        Dim txtplacedqty As New TextBox
        Try
            For i = 0 To dgAllocate.Rows.Count - 1
                txtplacedqty = dgAllocate.Rows(i).Cells(13).FindControl("txtplacedqty")
                txtplacedqty.Enabled = True
            Next
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "EnableAll")
        End Try
    End Sub
    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Dim sStatus As String = ""
        Dim iCommodityID As Integer : Dim iItemID As Integer : Dim iHistoryID As Integer
        Dim dt As New DataTable

        Dim txtplacedqty As New TextBox
        Try
            lblErrorUp.Text = ""
            If lblStatus.Text = "Ready To allocate" Then
                lblErrorUp.Text = "Save it before you Reject."
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            Else
                If lblStatus.Text = "Rejected By approver" Then
                    lblErrorUp.Text = "It is Already Rejected By approver."
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                Else
                    If dgAllocate.Rows.Count > 0 Then
                        For i = 0 To dgAllocate.Rows.Count - 1
                            iCommodityID = dgAllocate.Rows(i).Cells(1).Text
                            iItemID = dgAllocate.Rows(i).Cells(2).Text
                            iHistoryID = dgAllocate.Rows(i).Cells(3).Text

                            txtplacedqty = dgAllocate.Rows(i).FindControl("txtplacedqty")
                            If (txtplacedqty.Text <> String.Empty) Then 'If Qty is entered
                                objAllocate.RejectMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, txtRemarks.Text, iCommodityID, iItemID, iHistoryID, txtplacedqty.Text)
                            End If

                        Next
                    End If
                    lblErrorUp.Text = "Rejected Successfully."
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    dt = objAllocate.BindAllocatedGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue, ddlOrderNo.SelectedValue)
                    dgAllocate.DataSource = dt
                    dgAllocate.DataBind()
                    sStatus = objAllocate.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                    If sStatus = "W" Then
                        lblStatus.Text = "Waiting For approve."
                    ElseIf sStatus = "A" Then
                        lblStatus.Text = "Approved."
                    ElseIf sStatus = "R" Then
                        lblStatus.Text = "Rejected By approver"
                    Else
                        lblStatus.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnReject_Click")
        End Try
    End Sub
    Public Sub GetPartiesForThisItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderNo As Integer, ByVal iItemId As Integer)
        Dim dtTab As New DataTable
        Try
            dtTab = objAllocate.GetDetailsPartiesForThisItem(sNameSpace, iCompID, iOrderNo, iItemId)
            dgPartyAllocation.DataSource = dtTab
            dgPartyAllocation.DataBind()
            Session("Party") = dtTab
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetPartiesForThisItem")
        End Try
    End Sub
    Protected Sub ddlSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearch.SelectedIndexChanged
        Dim dtGetAllocateMasterData, dt As New DataTable
        Dim sStatus As String = ""
        Dim bCheck As String = ""
        Try
            lblErrorUp.Text = ""
            'imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
            'imgbtnApprove.Visible = True
            imgbtnSave.ImageUrl = "~/Images/Update24.png"
            imgbtnSave.ToolTip = "Update"

            If ddlSearch.SelectedIndex > 0 Then
                dtGetAllocateMasterData = objAllocate.GetAllocateMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue, 0)
                If dtGetAllocateMasterData.Rows.Count > 0 Then
                    For m = 0 To dtGetAllocateMasterData.Rows.Count - 1
                        If IsDBNull(dtGetAllocateMasterData.Rows(m)("SAM_Code")) = False Then
                            txtOrderCode.Text = dtGetAllocateMasterData.Rows(m)("SAM_Code")
                        Else
                            txtOrderCode.Text = ""
                        End If
                        If IsDBNull(dtGetAllocateMasterData.Rows(m)("SAM_OrderNo")) = False Then
                            ddlOrderNo.SelectedValue = dtGetAllocateMasterData.Rows(m)("SAM_OrderNo")
                        Else
                            ddlOrderNo.SelectedIndex = 0
                        End If

                        ddlParty.SelectedValue = dtGetAllocateMasterData.Rows(m)("SAM_Party")
                        txtRemarks.Text = dtGetAllocateMasterData.Rows(m)("SAM_Remarks")
                        txtCode.Value = objAllocate.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)

                        If IsDBNull(dtGetAllocateMasterData.Rows(m)("SAM_GrandDiscount")) = False Then
                            txtGrandDiscount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dtGetAllocateMasterData.Rows(m)("SAM_GrandDiscount")))
                        Else
                            txtGrandDiscount.Text = ""
                        End If
                        If IsDBNull(dtGetAllocateMasterData.Rows(m)("SAM_GrandDiscountAmt")) = False Then
                            txtGrandDiscountAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(dtGetAllocateMasterData.Rows(m)("SAM_GrandDiscountAmt")))
                        Else
                            txtGrandDiscountAmt.Text = ""
                        End If
                        If IsDBNull(dtGetAllocateMasterData.Rows(m)("SAM_GrandTotal")) = False Then
                            txtGrandTotal.Text = String.Format("{0:0.00}", Convert.ToDecimal(dtGetAllocateMasterData.Rows(m)("SAM_GrandTotal")))
                        Else
                            txtGrandTotal.Text = ""
                        End If
                        If IsDBNull(dtGetAllocateMasterData.Rows(m)("SAM_GrandTotalAmt")) = False Then
                            txtGrandTotalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(dtGetAllocateMasterData.Rows(m)("SAM_GrandTotalAmt")))
                        Else
                            txtGrandTotalAmt.Text = ""
                        End If
                    Next
                End If

                dt = objAllocate.BindAllocatedGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue, ddlOrderNo.SelectedValue)
                dgAllocate.DataSource = dt
                dgAllocate.DataBind()

                pnlGrand.Visible = True

                sStatus = objAllocate.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If (sStatus = "W") Then
                    lblStatus.Text = "Waiting for approval"
                    DisableAll()
                ElseIf (sStatus = "A") Then
                    lblStatus.Text = "Approved"
                    DisableAll()
                ElseIf (sStatus = "R") Then
                    lblStatus.Text = "Rejected By approver"
                    DisableAll()
                Else
                    lblStatus.Text = "Ready To allocate"
                    DisableAll()
                End If

                bCheck = objAllocate.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If bCheck = True Then
                    lblErrorUp.Text = "Selected Allocation No has been dispatched, it can not be edit."
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    'btnPlaceOrder.Enabled = False
                    'imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                    'btnApprove.Enabled = False
                    'imgbtnApprove.Visible = False
                    'btnReject.Enabled = False
                    Exit Sub
                End If
            Else

                Clear()
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSearch_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub Clear()
        Try
            lblErrorUp.Text = "" : ddlSearch.SelectedIndex = 0
            txtOrderCode.Text = "" : lblStatus.Text = "" : ddlOrderNo.SelectedIndex = 0 : ddlParty.SelectedIndex = 0
            txtRemarks.Text = ""
            dgAllocate.DataSource = Nothing
            dgAllocate.DataBind()
            GenerateOrderCode()

            txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotal.Text = "" : txtGrandTotalAmt.Text = ""
            pnlGrand.Visible = False
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Clear")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            imgbtnSave.ImageUrl = "~/Images/Save24.png"
            imgbtnSave.ToolTip = "Save"
            Clear()
            ' btnPlaceOrder.Enabled = True 
            'imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            'btnApprove.Enabled = True
            'imgbtnApprove.Visible = False
            'btnReject.Enabled = True
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub dgPartyAllocation_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgPartyAllocation.PageIndexChanged
        Dim dt As New DataTable
        Dim sSearch As String = ""
        Try
            lblErrorUp.Text = ""
            dgPartyAllocation.PageSize = e.NewPageIndex
            dt = Session("Party")
            If dt.Rows.Count > dgPartyAllocation.PageSize Then
                dgPartyAllocation.AllowPaging = True
            Else
                dgPartyAllocation.AllowPaging = False
            End If
            dgPartyAllocation.DataSource = dt
            dgPartyAllocation.DataBind()
            dgPartyAllocation.Visible = True
        Catch ex As Exception
            If ((ex.Message = "Invalid CurrentPageIndex value. It must be >= 0 and < the PageCount.") _
                AndAlso (dgPartyAllocation.PageSize > 0)) Then
                dgPartyAllocation.PageSize = 0
                dgPartyAllocation.DataBind()
            Else
                Throw ex
            End If
            Components.AppException.LogError(Session("NameSpace"), ex.Message, sFormName, "dgPartyAllocation_PageIndexChanged")
        End Try
    End Sub
    'Private Sub dgAllocate_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAllocate.ItemCommand
    '    Dim iItemID As Integer
    '    Try
    '        If e.CommandName = "Select" Then
    '            iItemID = e.Item.Cells(2).Text
    '            GetPartiesForThisItem(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue, iItemID)
    '            mpYN.Show()
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub dgAllocate_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgAllocate.RowCommand
        Dim iItemID As Integer : Dim lblItemID As New Label
        Try
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)

                iItemID = lblItemID.Text
                GetPartiesForThisItem(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue, iItemID)
                'mpYN.Show()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAllocate_RowCommand")
        End Try
    End Sub
    Private Sub dgAllocate_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgAllocate.RowDataBound
        Dim lblavailablestock As New Label
        Dim txtPlacedQty As New TextBox
        Dim lblClosingStock As New Label
        Dim lblMRP As New Label
        Dim lblTotal As New Label
        Dim lblNetAmount As New Label
        Dim dtDiscount, dtVAT, dtCST, dtExice As New DataTable
        Dim iOrderNo, IItemID, iHistoryID As Integer
        Dim dtProDetails, dtAllocateDetails As New DataTable
        Dim bCheck As String = ""
        Dim iVAT, iCST As Integer
        Dim lblPendingQty As New Label
        Dim iAllocateID As Integer
        Dim iCommodityID As Integer : Dim sCheck As String = ""

        Dim lblCommodityID As New Label : Dim lblItemID As New Label : Dim lblHistoryID As New Label : Dim lblOrderQuantity As New Label

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If ddlSearch.SelectedIndex > 0 Then
                    iAllocateID = ddlSearch.SelectedValue
                Else
                    iAllocateID = 0
                End If
                iOrderNo = ddlOrderNo.SelectedValue

                lblCommodityID = e.Row.FindControl("lblCommodityID")
                lblItemID = e.Row.FindControl("lblItemID")
                lblHistoryID = e.Row.FindControl("lblHistoryID")

                iCommodityID = lblCommodityID.Text
                IItemID = lblItemID.Text
                iHistoryID = lblHistoryID.Text

                lblavailablestock = e.Row.FindControl("lblAvailableStock")
                txtPlacedQty = e.Row.FindControl("txtplacedqty")
                lblClosingStock = e.Row.FindControl("lblClosingStock")
                lblMRP = e.Row.FindControl("lblMRP")
                'txtDiscount = e.Item.FindControl("txtDiscount")
                lblTotal = e.Row.FindControl("lblTotal")

                lblNetAmount = e.Row.FindControl("lblNetAmount")

                lblPendingQty = e.Row.FindControl("lblPendingQty")

                lblOrderQuantity = e.Row.FindControl("lblOrderQuantity")

                If lblReAllocateID.Text > 0 Then
                    dtAllocateDetails = objAllocate.GetAllocatedOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo, iCommodityID, IItemID, iHistoryID, iAllocateID)
                    If dtAllocateDetails.Rows.Count > 0 Then
                        For m = 0 To dtAllocateDetails.Rows.Count - 1
                            txtPlacedQty.Text = dtAllocateDetails.Rows(m)("SAD_PlacedQnt")
                            lblTotal.Text = dtAllocateDetails.Rows(m)("SAD_PlacedQntAmount")
                            lblNetAmount.Text = dtAllocateDetails.Rows(m)("SAD_PlacedTotalAmount")
                            lblClosingStock.Text = dtAllocateDetails.Rows(m)("SAD_ClosingBal")
                        Next
                    End If
                Else
                    bCheck = objAllocate.CheckForAllocation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    If bCheck = True Then
                        If iAllocateID > 0 Then
                            'dtAllocateDetails = objAllocate.GetAllocatedOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo, iCommodityID, IItemID, iHistoryID, iAllocateID)
                            'If dtAllocateDetails.Rows.Count > 0 Then
                            '    For m = 0 To dtAllocateDetails.Rows.Count - 1
                            '        txtPlacedQty.Text = dtAllocateDetails.Rows(m)("SAD_PlacedQnt")
                            '        lblTotal.Text = dtAllocateDetails.Rows(m)("SAD_PlacedQntAmount")
                            '        lblNetAmount.Text = dtAllocateDetails.Rows(m)("SAD_PlacedTotalAmount")
                            '        lblClosingStock.Text = dtAllocateDetails.Rows(m)("SAD_ClosingBal")
                            '    Next
                            'End If
                        Else
                            sCheck = objAllocate.CheckForAllocationItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, iCommodityID, IItemID, iHistoryID)
                            If sCheck = "True" Then
                                dtAllocateDetails = objAllocate.GetAllocatedOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo, iCommodityID, IItemID, iHistoryID, 0)
                                If dtAllocateDetails.Rows.Count > 0 Then
                                    For m = 0 To dtAllocateDetails.Rows.Count - 1
                                        txtPlacedQty.Text = ""
                                        lblTotal.Text = ""
                                        lblNetAmount.Text = ""
                                        lblClosingStock.Text = ""
                                    Next
                                End If
                            Else
                                dtProDetails = objAllocate.GetPROFormaOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo, iCommodityID, IItemID, iHistoryID)
                                If dtProDetails.Rows.Count > 0 Then
                                    For m = 0 To dtProDetails.Rows.Count - 1
                                        txtPlacedQty.Text = dtProDetails.Rows(m)("SPOD_Quantity")
                                        lblTotal.Text = dtProDetails.Rows(m)("SPOD_RateAmount")
                                        lblNetAmount.Text = dtProDetails.Rows(m)("SPOD_TotalAmount")
                                        lblClosingStock.Text = lblavailablestock.Text - txtPlacedQty.Text
                                    Next
                                End If
                            End If
                        End If
                    Else
                        'dtProDetails = objAllocate.GetPROFormaOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo, iCommodityID, IItemID, iHistoryID)
                        'If dtProDetails.Rows.Count > 0 Then
                        '    For m = 0 To dtProDetails.Rows.Count - 1
                        '        txtPlacedQty.Text = dtProDetails.Rows(m)("SPOD_Quantity")
                        '        lblTotal.Text = dtProDetails.Rows(m)("SPOD_RateAmount")
                        '        lblNetAmount.Text = dtProDetails.Rows(m)("SPOD_TotalAmount")
                        '        lblClosingStock.Text = lblavailablestock.Text - txtPlacedQty.Text
                        '    Next
                        'End If
                    End If
                End If

                txtPlacedQty.Attributes.Add("Onblur", "javascript:return GetClosingStock('" & lblavailablestock.ClientID & "','" & txtPlacedQty.ClientID & "','" & lblClosingStock.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & lblOrderQuantity.ClientID & "','" & lblNetAmount.ClientID & "','" & lblPendingQty.ClientID & "')")

                If lblPendingQty.Text = "0" Then
                    lblErrorUp.Text = "Pending qty is 0, No qty to allocate."
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    'btnPlaceOrder.Enabled = False
                    imgbtnSave.Enabled = False
                Else
                    ' btnPlaceOrder.Enabled = True
                    imgbtnSave.Enabled = True
                    'btnApprove.Enabled = True
                    'imgbtnApprove.Visible = True
                    btnReject.Enabled = True
                End If
            Else

            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAllocate_RowDataBound")
        End Try
    End Sub
    Private Sub dgAllocate_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles dgAllocate.RowCancelingEdit

    End Sub
    Private Sub dgAllocate_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgAllocate.RowEditing

    End Sub
    'Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
    '    Try
    '        imgbtnSave_Click(sender, e)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub dgAllocate_PreRender(sender As Object, e As EventArgs) Handles dgAllocate.PreRender
        Dim dt As New DataTable
        Try
            If dgAllocate.Rows.Count > 0 Then
                dgAllocate.UseAccessibleHeader = True
                dgAllocate.HeaderRow.TableSection = TableRowSection.TableHeader
                dgAllocate.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAllocate_PreRender")
        End Try
    End Sub
    'Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
    '    Dim sStatus As String = ""
    '    Try
    '        lblErrorUpUp.Text = ""
    '        If lblStatus.Text = "Ready To allocate" Then
    '            lblErrorUpUp.Text = "Save it before you Approve."
    '            lblCustomerValidationMsg.Text = lblErrorUpUp.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            Exit Sub
    '        Else
    '            If lblStatus.Text = "Approved" Then
    '                lblErrorUpUp.Text = "It is Already Approved."
    '                lblCustomerValidationMsg.Text = lblErrorUpUp.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '                Exit Sub
    '            ElseIf lblStatus.Text = "Rejected By approver" Then
    '                lblErrorUpUp.Text = "It is Already Rejected By approver,it can not be approve."
    '                lblCustomerValidationMsg.Text = lblErrorUpUp.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '                Exit Sub
    '            Else
    '                objAllocate.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlSearch.SelectedValue)
    '                lblErrorUpUp.Text = "Approved Successfully."
    '                lblCustomerValidationMsg.Text = lblErrorUpUp.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '                sStatus = objAllocate.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
    '                If sStatus = "W" Then
    '                    lblStatus.Text = "Waiting For approve."
    '                ElseIf sStatus = "A" Then
    '                    lblStatus.Text = "Approved."
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblErrorUpUp.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
    '    End Try
    'End Sub
    Private Sub dgPartyAllocation_PreRender(sender As Object, e As EventArgs) Handles dgPartyAllocation.PreRender
        Dim dt As New DataTable
        Try
            If dgPartyAllocation.Rows.Count > 0 Then
                dgPartyAllocation.UseAccessibleHeader = True
                dgPartyAllocation.HeaderRow.TableSection = TableRowSection.TableHeader
                dgPartyAllocation.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPartyAllocation_PreRender")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/Sales/AllocationMaster.aspx?"), False)
        Catch ex As Exception
            lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
End Class
