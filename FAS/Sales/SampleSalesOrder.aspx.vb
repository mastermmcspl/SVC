Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Sales_SampleSalesOrder
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_SalesOrder"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Private objclsModulePermission As New clsModulePermission
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objSampleSales As New ClsSampleSalesOrder
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnNew.ImageUrl = "~/Images/Reresh24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnDelete.ImageUrl = "~/Images/Trash24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SSO")
                imgbtnNew.Visible = False : imgbtnAdd.Visible = False : imgbtnUpdate.Visible = False :: imgbtnDelete.Visible = False
                'imgbtnReport.Visible = False 
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnNew.Visible = True
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        'imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnUpdate.Visible = True
                        imgbtnAdd.Visible = True
                        imgbtnDelete.Visible = True
                    End If
                End If
                'imgbtnAdd.Visible = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasSSO", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnAdd.Visible = True : imgbtnUpdate.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Delete,") = True Then
                '        imgbtnDelete.Visible = True
                '    End If
                'End If

                'imgbtnUpdate.Visible = False : imgbtnAdd.Visible = True
                If ddlParty.SelectedIndex > 0 Then
                    lstBoxDescription.Enabled = True
                Else
                    lstBoxDescription.Enabled = False
                End If
                GenerateOrderCodeAnddate()

                LoadExistingSampleNo()
                LoadCommodity()
                LoadModeOfCommunication()
                LoadParty()
                LoadMethodOfShiping()
                BindDescription(0)
                LoadIsuedBy()

                dgSampleSalesOrder.DataSource = Nothing
                dgSampleSalesOrder.DataBind()
                dgSampleSalesOrder.Visible = False

                Me.ddlCommodity.Attributes.Add("OnClick", "return ValidateMasterData()")
                Me.txtQuantity.Attributes.Add("onblur", "return ValidateQty()")
                Me.imgbtnAdd.Attributes.Add("OnClick", "return ValidateForm()")

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCodeAnddate()
        Try
            txtSampleNo.Text = objSampleSales.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            txtSampleDate.Text = objGen.FormatDtForRDBMS(System.DateTime.Now, "D")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingSampleNo()
        Dim dt As New DataTable
        Try
            dt = objSampleSales.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "SSOM_SampleOrderNo"
            ddlSearch.DataValueField = "SSOM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "--- Existing Order No ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadIsuedBy()
        Try
            ddlIssuedBy.DataSource = objSampleSales.LoadIssuedBy(sSession.AccessCode, sSession.AccessCodeID)
            ddlIssuedBy.DataTextField = "Usr_FullName"
            ddlIssuedBy.DataValueField = "Usr_id"
            ddlIssuedBy.DataBind()
            ddlIssuedBy.Items.Insert(0, "--- Select Inputed/Issued By ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadMethodOfShiping()
        Try
            ddlModeOfShipping.DataSource = objSampleSales.LoadMethodOfShiping(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfShipping.DataTextField = "Mas_desc"
            ddlModeOfShipping.DataValueField = "Mas_id"
            ddlModeOfShipping.DataBind()
            ddlModeOfShipping.Items.Insert(0, "--- Select Mode of Shipping ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadParty()
        Try
            ddlParty.DataSource = objSampleSales.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "BM_Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "--- Select Party ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadModeOfCommunication()
        Try
            ddlModeOfCommunication.DataSource = objSampleSales.BindModeOfCommunication(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfCommunication.DataTextField = "Mas_Desc"
            ddlModeOfCommunication.DataValueField = "Mas_ID"
            ddlModeOfCommunication.DataBind()
            ddlModeOfCommunication.Items.Insert(0, "--- Select Mode Of Communication ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objSampleSales.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "--- Select Commodity ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDescription(ByVal iCommodityID As Integer)
        Try
            lstBoxDescription.DataSource = objSampleSales.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID)
            lstBoxDescription.DataTextField = "INV_Code"
            lstBoxDescription.DataValueField = "INV_ID"
            lstBoxDescription.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindUnitOfMeassurement(ByVal iItemID As Integer, ByVal iHistoryID As Integer)
        Try
            ddlUnitOfMeassurement.DataSource = objSampleSales.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iItemID, iHistoryID)
            ddlUnitOfMeassurement.DataTextField = "Mas_Desc"
            ddlUnitOfMeassurement.DataValueField = "Mas_ID"
            ddlUnitOfMeassurement.DataBind()
            'ddlUnitOfMeassurement.Items.Insert(0, "----- Select Unit Of Meassurement -----")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim Arr() As String
        Dim dt As New DataTable
        Dim iMasterID As Integer
        Dim sStatus As String = ""
        Try
            lblErrorUp.Text = ""

            objSampleSales.SSOM_SampleOrderNo = txtSampleNo.Text
            objSampleSales.SSOM_SampleDate = Date.ParseExact(Trim(txtSampleDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSampleSales.SSOM_Party = ddlParty.SelectedValue
            objSampleSales.SSOM_PartyCode = txtPartyNo.Text
            objSampleSales.SSOM_ContantNo = txtContactNo.Text
            objSampleSales.SSOM_Address = txtAddress.Text
            If ddlModeOfShipping.SelectedIndex > 0 Then
                objSampleSales.SSOM_ModeOfShipping = ddlModeOfShipping.SelectedValue
            Else
                objSampleSales.SSOM_ModeOfShipping = 0
            End If
            If txtShippingDate.Text <> "" Then
                objSampleSales.SSOM_ShippingDate = Date.ParseExact(Trim(txtShippingDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If ddlModeOfCommunication.SelectedIndex > 0 Then
                objSampleSales.SSOM_Communication = ddlModeOfCommunication.SelectedValue
            Else
                objSampleSales.SSOM_Communication = 0
            End If
            If ddlIssuedBy.SelectedIndex > 0 Then
                objSampleSales.SSOM_IssuedBy = ddlIssuedBy.SelectedValue
            Else
                objSampleSales.SSOM_IssuedBy = 0
            End If
            objSampleSales.SSOM_IssuedOn = DateTime.Today

            objSampleSales.SSOM_CreatedBy = sSession.UserID
            objSampleSales.SSOM_CreatedOn = DateTime.Today
            objSampleSales.SSOM_DelFlag = "W"
            objSampleSales.SSOM_Status = "W"
            objSampleSales.SSOM_Operation = "C"
            objSampleSales.SSOM_IPAddress = sSession.IPAddress


            Arr = objSampleSales.SaveSampleSalesMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objSampleSales)
            iMasterID = Arr(1)

            objSampleSales.SSOD_MasterID = iMasterID

            If txtQuantity.Text <> "" Then
                objSampleSales.SSOD_CommodityId = ddlCommodity.SelectedValue
                objSampleSales.SSOD_ItemID = lstBoxDescription.SelectedValue
                objSampleSales.SSOD_Quantity = txtQuantity.Text
                objSampleSales.SSOD_UnitID = ddlUnitOfMeassurement.SelectedValue
                objSampleSales.SSOD_Historyid = objSampleSales.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                objSampleSales.SSOD_CompID = sSession.AccessCodeID
                objSampleSales.SSOD_DelFlag = "W"
                objSampleSales.SSOD_Status = "W"
                objSampleSales.SSOD_Operation = "C"
                objSampleSales.SSOD_IPAddress = sSession.IPAddress

                If objSampleSales.SSOM_PartyCode.StartsWith("P") Then
                    objSampleSales.SSOD_Amount = objSampleSales.GetRetailRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                ElseIf objSampleSales.SSOM_PartyCode.StartsWith("C") Then
                    objSampleSales.SSOD_Amount = objSampleSales.GetMRPRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                End If

                objSampleSales.SSOD_TotalAmount = (txtQuantity.Text * objSampleSales.SSOD_Amount)

                Arr = objSampleSales.SaveSampleSalesDetails(sSession.AccessCode, objSampleSales, sSession.YearID)

                If Arr(0) = "2" Then
                    lblErrorUp.Text = "Successfully Updated"
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    'imgbtnAdd.Visible = False : imgbtnUpdate.Visible = True
                ElseIf Arr(0) = "3" Then
                    lblErrorUp.Text = "Successfully Saved"
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    'imgbtnAdd.Visible = False : imgbtnUpdate.Visible = True
                End If

            End If

            sStatus = objSampleSales.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            If (sStatus = "W") Then
                lblStatus.Text = "Waiting for approval"
            ElseIf (sStatus = "A") Then
                lblStatus.Text = "Approved"
            ElseIf (sStatus = "R") Then
                lblStatus.Text = "Rejected By approver"
            End If

            LoadExistingSampleNo()
            ddlSearch.SelectedValue = iMasterID

            LoadExistingOrderGrid(iMasterID)
            txtOrderID.Text = iMasterID

            ClearDetails()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearDetails()
        Try
            For i = 0 To lstBoxDescription.Items.Count - 1
                lstBoxDescription.Items(i).Selected = False
            Next
            ddlUnitOfMeassurement.Items.Clear()
            txtQuantity.Text = "" : lblAvailableStock.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingOrderGrid(ByVal iMasterID As Integer)
        Dim dt As New DataTable
        Try
            dt = objSampleSales.BindExistingOrder(sSession.AccessCode, sSession.AccessCodeID, iMasterID)
            If dt.Rows.Count > 0 Then
                dgSampleSalesOrder.DataSource = dt
                dgSampleSalesOrder.DataBind()
                dgSampleSalesOrder.Visible = True
                Session("SampleSales") = dt
            Else
                dgSampleSalesOrder.DataSource = Nothing
                dgSampleSalesOrder.DataBind()
                lblErrorUp.Text = "No Orders Found."
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblErrorUp.Text = ""
            'ClearonPartySelection()
            GenerateOrderCodeAnddate()
            If ddlParty.SelectedIndex > 0 Then
                lstBoxDescription.Enabled = True
                dt = objSampleSales.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        txtPartyNo.Text = dt.Rows(i)("BM_Code")
                        txtAddress.Text = dt.Rows(i)("BM_Address")
                        txtContactNo.Text = dt.Rows(i)("BM_MobileNo")
                    Next
                End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCommodity.SelectedIndex > 0 Then
                BindDescription(ddlCommodity.SelectedValue)
            Else
                BindDescription(0)
            End If
        Catch ex As Exception
            lblErrorUp.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub lstBoxDescription_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxDescription.SelectedIndexChanged
        Dim iHistoryID As Integer
        Try
            txtQuantity.Text = ""
            If ddlParty.SelectedIndex > 0 Then
                If lstBoxDescription.SelectedIndex <> -1 Then
                    ddlCommodity.SelectedValue = objSampleSales.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)

                    iHistoryID = objSampleSales.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                    BindUnitOfMeassurement(lstBoxDescription.SelectedValue, iHistoryID)
                    lblAvailableStock.Text = objSampleSales.GetAvailableStock(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iHistoryID)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearch.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Dim bCheck As String = ""
        Dim sStatus As String = ""
        Try
            lblErrorUp.Text = ""
            'imgbtnAdd.Visible = False : imgbtnUpdate.Visible = True
            If ddlSearch.SelectedIndex > 0 Then
                lstBoxDescription.Enabled = True

                dtMaster = objSampleSales.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        txtSampleNo.Text = dtMaster.Rows(i)("SSOM_SampleOrderNo")
                        txtSampleDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SSOM_SampleDate"), "D")
                        ddlParty.SelectedValue = dtMaster.Rows(i)("SSOM_Party")
                        txtPartyNo.Text = dtMaster.Rows(i)("SSOM_PartyCode")
                        txtContactNo.Text = dtMaster.Rows(i)("SSOM_ContantNo")
                        txtAddress.Text = dtMaster.Rows(i)("SSOM_Address")

                        ddlModeOfShipping.SelectedValue = dtMaster.Rows(i)("SSOM_ModeOfShipping")
                        If IsDBNull(dtMaster.Rows(i)("SSOM_ShippingDate")) = False Then
                            If (dtMaster.Rows(i)("SSOM_ShippingDate")) <> "1899-12-30 00:00:00.000" Then
                                txtShippingDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SSOM_ShippingDate"), "D")
                            Else
                                txtShippingDate.Text = ""
                            End If
                        Else
                            txtShippingDate.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SSOM_Communication")) = False Then
                            If dtMaster.Rows(i)("SSOM_Communication") > 0 Then
                                ddlModeOfCommunication.SelectedValue = dtMaster.Rows(i)("SSOM_Communication")
                            Else
                                ddlModeOfCommunication.SelectedIndex = 0
                            End If
                        Else
                            ddlModeOfCommunication.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SSOM_IssuedBy")) = False Then
                            If dtMaster.Rows(i)("SSOM_IssuedBy") > 0 Then
                                ddlIssuedBy.SelectedValue = dtMaster.Rows(i)("SSOM_IssuedBy")
                            Else
                                ddlIssuedBy.SelectedIndex = 0
                            End If
                        Else
                            ddlIssuedBy.SelectedIndex = 0
                        End If

                    Next
                End If

                sStatus = objSampleSales.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If (sStatus = "W") Then
                    lblStatus.Text = "Waiting for approval"
                ElseIf (sStatus = "A") Then
                    lblStatus.Text = "Approved"
                ElseIf (sStatus = "X") Then
                    lblStatus.Text = "Deleted"
                End If

                If ddlCommodity.SelectedIndex > 0 Then
                    BindDescription(ddlCommodity.SelectedValue)
                Else
                    BindDescription(0)
                End If
                LoadExistingOrderGrid(ddlSearch.SelectedValue)

            Else
                ddlSearch.Visible = False
                Clear()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub Clear()
        Try
            If ddlSearch.SelectedValue <> "" Then
                ddlSearch.SelectedIndex = 0
            End If
            txtSampleNo.Text = "" : txtSampleDate.Text = "" : ddlModeOfShipping.SelectedIndex = 0 : txtShippingDate.Text = ""
            ddlModeOfCommunication.SelectedIndex = 0 : ddlCommodity.SelectedIndex = 0
            ddlParty.SelectedIndex = 0 : txtPartyNo.Text = "" : txtContactNo.Text = "" : txtAddress.Text = ""
            ddlIssuedBy.SelectedIndex = 0
            txtOrderID.Text = ""

            'For i = 0 To lstBoxDescription.Items.Count - 1
            '    lstBoxDescription.Items(i).Selected = False
            'Next
            lstBoxDescription.Items.Clear()
            ddlUnitOfMeassurement.Items.Clear()
            txtQuantity.Text = "" : lblAvailableStock.Text = ""

            dgSampleSalesOrder.DataSource = Nothing
            dgSampleSalesOrder.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgSampleSalesOrder_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgSampleSalesOrder.RowCommand
        Dim iItemID As Integer, iCommodityID As Integer, iOrderNo As Integer
        Dim sStatus As String = ""
        Dim iID As Integer

        Dim lblID, lblCommodityID, lblItemID As New Label
        Try
            lblErrorUp.Text = ""
            If ddlSearch.SelectedIndex > 0 Then
                iOrderNo = ddlSearch.SelectedValue
            Else
                iOrderNo = txtOrderID.Text
            End If
            If e.CommandName = "Cancel" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)

                iID = lblID.Text
                iCommodityID = lblCommodityID.Text
                iItemID = lblItemID.Text

                sStatus = objSampleSales.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo)
                If (sStatus = "A") Then
                    lblErrorUp.Text = "It is already Approved,items cannot be deleted."
                    lblCustomerValidationMsg.Text = lblErrorUp.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If

                objSampleSales.DeleteOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iID, iOrderNo, sSession.IPAddress)
                lblErrorUp.Text = "Canceled Successfully."
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
            LoadExistingOrderGrid(iOrderNo)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgSampleSalesOrder_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgSampleSalesOrder.RowDataBound
        Dim ibtnCancel As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                ibtnCancel = TryCast(e.Row.FindControl("ibtnCancel"), ImageButton)
                ibtnCancel.Attributes.Add("OnClick", "javascript:return Validate()")
            End If
        Catch ex As Exception
            lblErrorUp.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_ItemDataBound")
        End Try
    End Sub
    Private Sub imgbtnNew_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNew.Click
        Try
            lblStatus.Text = ""
            lblErrorUp.Text = ""
            Clear()
            GenerateOrderCodeAnddate()
            'imgbtnAdd.Visible = True : imgbtnUpdate.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnDelete_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDelete.Click
        Dim sStatus As String = ""
        Dim iOrderNo As Integer
        Dim iCommodityID, iItemID, iHistoryID, iQty As Integer
        Dim iID As Integer

        Dim lblID, lblCommodityID, lblItemID, lblHistoryID, lblQuantity As New Label
        Try
            lblErrorUp.Text = ""

            If ddlSearch.SelectedValue = "" And txtOrderID.Text = "" Then
                lblErrorUp.Text = "Create/select existing sample order to delete."
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlSearch.SelectedIndex > 0 Then
                iOrderNo = ddlSearch.SelectedValue
            Else
                iOrderNo = txtOrderID.Text
            End If

            If lblStatus.Text = "Approved" Then
                lblErrorUp.Text = "This order has been approved,it can not be deleted."
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf lblStatus.Text = "Deleted" Then
                lblErrorUp.Text = "It is already deleted."
                lblCustomerValidationMsg.Text = lblErrorUp.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            objSampleSales.DeleteMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, iOrderNo)
            If dgSampleSalesOrder.Rows.Count > 0 Then
                For i = 0 To dgSampleSalesOrder.Rows.Count - 1

                    lblID = dgSampleSalesOrder.Rows(i).FindControl("lblID")
                    lblCommodityID = dgSampleSalesOrder.Rows(i).FindControl("lblCommodityID")
                    lblItemID = dgSampleSalesOrder.Rows(i).FindControl("lblItemID")
                    lblHistoryID = dgSampleSalesOrder.Rows(i).FindControl("lblHistoryID")
                    lblQuantity = dgSampleSalesOrder.Rows(i).FindControl("lblQuantity")

                    iID = lblID.Text
                    iCommodityID = lblCommodityID.Text
                    iItemID = lblItemID.Text
                    iHistoryID = lblHistoryID.Text
                    iQty = lblQuantity.Text

                    objSampleSales.DeleteDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.YearID, iOrderNo, iCommodityID, iItemID, iHistoryID, iQty, sSession.IPAddress, iID)
                Next
            End If

            lblErrorUp.Text = "Deleted Successfully."
            lblCustomerValidationMsg.Text = lblErrorUp.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            sStatus = objSampleSales.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo)
            If sStatus = "X" Then
                lblStatus.Text = "Deleted"
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
