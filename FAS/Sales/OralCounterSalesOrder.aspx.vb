Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Sales_OralCounterSalesOrder
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_SalesOrder"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Private objclsModulePermission As New clsModulePermission
    Dim objProForma As New clsPROFormaSalesOrder
    Dim objGenFun As New clsGeneralFunctions
    Dim objOral As New clsOralSalesOrder
    Private Shared sSession As AllSession
    Private Shared sOCSOSave As String
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Dim iDefaultBranch As Integer
    Dim objPO As New clsPurchaseOrder
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnNew.ImageUrl = "~/Images/Reresh24.png"
        'imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        'imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnDelete.ImageUrl = "~/Images/Trash24.png"
        imgbtnReport.ImageUrl = "~/Images/Download24.png"
        imgbtnCreateCustomer.ImageUrl = "~/Images/Add16.png"
        'imgbtnApprove.ImageUrl = "~/Images/CheckMark24.png"
        imgbtnBack.ImageUrl = "~/Images/BackWard24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iINVH_Unit As Integer
        Dim iPices As Integer
        Dim iCategoryID As Integer
        Dim dOrderDate As Date
        Dim sFormButtons As String = ""
        Dim dt As New DataTable
        Dim sDate As String = ""
        Dim iDefaultBranch As Integer
        Try

            RFVddlParty.InitialValue = "Select Party"
            RFVPaymentType.InitialValue = "Select Payment Type"
            RFVCommodity.InitialValue = "Select Commodity"
            RFVModeOfShipping.InitialValue = "Select Mode of Shipping"
            RFVddlChargeType.InitialValue = "Select Charge Type"

            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "CS")
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnNew.Visible = False : imgbtnDelete.Visible = False
                imgbtnCreateCustomer.Visible = False : imgbtnAddCharge.Visible = False : ibtnInsert.Visible = False : sOCSOSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnAdd.Visible = True
                        imgbtnDelete.Visible = True
                        imgbtnCreateCustomer.Visible = True
                        imgbtnAddCharge.Visible = True
                        ibtnInsert.Visible = True
                        sOCSOSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnNew.Visible = True
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If
                'imgbtnAdd.Visible = False : imgbtnDelete.Visible = False : imgbtnReport.Visible = False : imgbtnApprove.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasCaS", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnAdd.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Delete,") = True Then
                '        imgbtnDelete.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Approve,") = True Then
                '        imgbtnApprove.Visible = True
                '    End If
                'End If

                'VisibleTrueDispatchControls()
                'VisibleFalse()

                BindCompanyType()
                BindBranch()

                dt = objOral.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("CUST_COMM_Address")) = True Or IsDBNull(dt.Rows(0)("CUST_ProvisionalNo")) = True Or IsDBNull(dt.Rows(0)("CUST_INDTypeID")) = True Or IsDBNull(dt.Rows(0)("CUST_TAXPayableCategory")) = True Then
                        lblError.Text = "FIll the details in Company Master"
                        Exit Sub
                    End If
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")
                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TAXPayableCategory")

                    txtDeliveryFromAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtDeliveryFromGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")
                End If

                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)

                iDefaultBranch = objOral.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                'divcollapseCharges.Visible = False
                'divcollapseDispatchDetails.Visible = True
                divcollapseChequeDetails.Visible = False

                'imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False : imgbtnReport.Visible = False : imgbtnApprove.Visible = False

                sDate = objOral.GetApplicationStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If sDate <> "" Then
                    lblStartDate.Text = objGen.FormatDtForRDBMS(objOral.GetApplicationStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
                Else
                    lblError.Text = "Update the Application Copnfiguration."
                    Exit Sub
                End If

                pnlGrand.Visible = False
                If ddlParty.SelectedIndex > 0 Then
                    lstBoxDescription.Enabled = True
                Else
                    lstBoxDescription.Enabled = False
                End If
                ddlRate.Enabled = False

                GenerateOrderCodeAnddate()

                LoadExistingOrderNo()
                LoadCommodity()
                LoadPaymentType()
                LoadModeOfCommunication()
                LoadParty()
                LoadMethodOfShiping()
                LoadSalesMan()
                BindDescription(0)

                LoadDiscount()
                'LoadVAT()
                'LoadExcise()
                LoadCategory()
                LoadChargeType()

                Session("ChargesMaster") = Nothing

                dgExistingProFormaSalesOrder.DataSource = Nothing
                dgExistingProFormaSalesOrder.DataBind()
                dgExistingProFormaSalesOrder.Visible = False
                'ddlSearch.Visible = True

                RFVAccZone.InitialValue = "--- Select Zone ---" : RFVAccZone.ErrorMessage = "Select Zone."
                RFVAccRgn.InitialValue = "--- Select Region ---" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "--- Select Area ---" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "--- Select Branch ---" : RFVAccBrnch.ErrorMessage = "Select Branch."

                'Me.imgbtnApprove.Attributes.Add("OnClick", "return ValidateApprove()")

                Me.lstBoxDescription.Attributes.Add("OnClick", "return ValidateParty()")
                Me.ddlCommodity.Attributes.Add("OnClick", "return ValidateForm()")
                'Me.imgbtnAdd.Attributes.Add("OnClick", "return ValidateForm()")

                Me.txtQuantity.Attributes.Add("onblur", "return RateAmount()")
                Me.ddlDiscount.Attributes.Add("onChange", "return RateAmount()")
                Me.txtMRP.Attributes.Add("onblur", "return CalculateFinalAmount()")

                Me.txtGrandDiscount.Attributes.Add("onblur", "return CalculateGrandDiscount()")

                If ddlCategory.SelectedIndex > 0 Then
                    iCategoryID = ddlCategory.SelectedValue
                Else
                    iCategoryID = 0
                End If
                If txtOrderDate.Text <> "" Then
                    If ddlCommodity.SelectedIndex > 0 Then
                        If lstBoxDescription.SelectedIndex > 0 Then
                            If ddlParty.SelectedIndex > 0 Then
                                dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                                txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lstBoxDescription.SelectedValue, ddlParty.SelectedValue, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                iPices = objDBL.SQLExecuteScalarInt(sSession.AccessCode, "Select INVH_PerPieces From Inventory_master_History Where INVH_INV_ID =" & lstBoxDescription.SelectedValue & " And INVH_CompID=" & sSession.AccessCodeID & " and InvH_YearID = " & sSession.YearID & "")
                                iINVH_Unit = objOral.GetINVH_Unit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lstBoxDescription.SelectedValue)

                                Dim IHistoryID As Integer
                                'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, txtMRPFromTable.Text, "C")
                                IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "C", dOrderDate)
                                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)
                            End If
                        End If
                    End If
                End If

                Dim iSOID As String = ""
                iSOID = objGen.DecryptQueryString(Request.QueryString("SOID"))
                If iSOID <> "" Then
                    ddlSearch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("SOID"))
                    ddlSearch_SelectedIndexChanged(sender, e)
                End If

            End If

            'If ddlSalesType.SelectedIndex > 0 Then
            '    divcollapseDispatchDetails.Visible = True
            'Else
            '    divcollapseDispatchDetails.Visible = False
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindBranch()
        Try
            'ddlBranch.DataSource = objPO.LoadBranch(sSession.AccessCode, sSession.AccessCodeID)
            'ddlBranch.DataTextField = "CUSTB_Name"
            'ddlBranch.DataValueField = "CUSTB_Id"
            'ddlBranch.DataBind()
            'ddlBranch.Items.Insert(0, "Select Branch")

            ddlBranch.DataSource = objOral.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranch.DataTextField = "Org_Name"
            ddlBranch.DataValueField = "Org_Node"
            ddlBranch.DataBind()
            ddlBranch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindCompanyType()
        Try
            ddlCompanyType.DataSource = objOral.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyType.DataTextField = "Mas_Desc"
            ddlCompanyType.DataValueField = "Mas_Id"
            ddlCompanyType.DataBind()
            ddlCompanyType.Items.Insert(0, "Select Company Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindGSTNCategory(ByVal sCompanyType As String)
        Dim dt As New DataTable
        Try
            dt = objOral.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategory.DataSource = dt
            ddlGSTCategory.DataTextField = "GC_GSTCategory"
            ddlGSTCategory.DataValueField = "GC_Id"
            ddlGSTCategory.DataBind()
            ddlGSTCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindBankName(ByVal iID As Integer)
        Try
            ddlBankName.DataSource = objOral.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, iID)
            ddlBankName.DataTextField = "GlDesc"
            ddlBankName.DataValueField = "gl_Id"
            ddlBankName.DataBind()
            ddlBankName.Items.Insert(0, "Select Bank Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objOral.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingOrderNo()
        Dim dt As New DataTable
        Try
            dt = objOral.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "")
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "SPO_OrderCode"
            ddlSearch.DataValueField = "SPO_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Order No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDescription(ByVal iCommodityID As Integer)
        Try
            lstBoxDescription.DataSource = objOral.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID)
            lstBoxDescription.DataTextField = "INV_Code"
            lstBoxDescription.DataValueField = "INV_ID"
            lstBoxDescription.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCodeAnddate()
        Try
            txtOrderCode.Text = objOral.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            txtOrderDate.Text = objGen.FormatDtForRDBMS(System.DateTime.Now, "D")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCodeAnddate")
        End Try
    End Sub
    Private Sub LoadSalesMan()
        Try
            ddlSalesMan.DataSource = objOral.LoadSalesMan(sSession.AccessCode, sSession.AccessCodeID)
            ddlSalesMan.DataTextField = "username"
            ddlSalesMan.DataValueField = "Usr_id"
            ddlSalesMan.DataBind()
            ddlSalesMan.Items.Insert(0, "Select Sales Man")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadMethodOfShiping()
        Try
            ddlModeOfShipping.DataSource = objOral.LoadMethodOfShiping(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfShipping.DataTextField = "Mas_desc"
            ddlModeOfShipping.DataValueField = "Mas_id"
            ddlModeOfShipping.DataBind()
            ddlModeOfShipping.Items.Insert(0, "Select Mode of Shipping")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadParty()
        Try
            'ddlParty.DataSource = objOral.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            'ddlParty.DataTextField = "ACM_Name"
            'ddlParty.DataValueField = "ACM_ID"
            'ddlParty.DataBind()
            'ddlParty.Items.Insert(0, "--- Select Party ---")
            ddlParty.DataSource = objOral.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "BM_Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCategory()
        Try
            ddlCategory.DataSource = objOral.BindCategory(sSession.AccessCode, sSession.AccessCodeID)
            ddlCategory.DataTextField = "Mas_Desc"
            ddlCategory.DataValueField = "Mas_ID"
            ddlCategory.DataBind()
            ddlCategory.Items.Insert(0, "Select Category")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadModeOfCommunication()
        Try
            ddlModeOfCommunication.DataSource = objOral.BindModeOfCommunication(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfCommunication.DataTextField = "Mas_Desc"
            ddlModeOfCommunication.DataValueField = "Mas_ID"
            ddlModeOfCommunication.DataBind()
            ddlModeOfCommunication.Items.Insert(0, "Select Mode Of Communication")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadPaymentType()
        Try
            ddlPaymentType.DataSource = objOral.BindPaymentType(sSession.AccessCode, sSession.AccessCodeID)
            ddlPaymentType.DataTextField = "Mas_Desc"
            ddlPaymentType.DataValueField = "Mas_ID"
            ddlPaymentType.DataBind()
            ddlPaymentType.Items.Insert(0, "Select Payment Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objOral.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDiscount()
        Dim dt As New DataTable
        Try
            dt = objOral.LoadDiscounts(sSession.AccessCode, sSession.AccessCodeID)
            ddlDiscount.DataSource = dt
            ddlDiscount.DataTextField = "Mas_Desc"
            ddlDiscount.DataValueField = "Mas_Id"
            ddlDiscount.DataBind()
            ddlDiscount.Items.Insert(0, "Select Discount")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Private Sub LoadVAT(ByVal iHistoryID As Integer)
    '    Dim dt As New DataTable
    '    Dim sDate As Date
    '    Dim dOrderDate As Date
    '    Try
    '        ddlVAT.Items.Clear()
    '        If txtOrderDate.Text <> "" Then
    '            dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            dt = objOral.GetVATOnOrderDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dOrderDate)
    '            'dt = objOral.LoadVATAndCSTFromGeneralMaster(sSession.AccessCode, sSession.AccessCodeID, "VAT")
    '            'Else
    '            '    sDate = "01/01/1900"
    '            '    dt = objOral.GetVATOnOrderDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, sDate)
    '        End If
    '        If dt.Rows.Count > 0 Then
    '            ddlVAT.DataSource = dt
    '            ddlVAT.DataTextField = "Mas_Desc"
    '            ddlVAT.DataValueField = "Mas_Id"
    '            ddlVAT.DataBind()
    '            ddlVAT.Items.Insert(0, "Select VAT")
    '            If dt.Rows.Count = 1 Then
    '                ddlVAT.SelectedValue = dt.Rows(0)("MAS_ID")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Private Sub LoadExcise(ByVal iHistoryID As Integer)
    '    Dim dt As New DataTable
    '    Dim sDate As Date
    '    Dim dOrderDate As Date
    '    Try
    '        ddlExcise.Items.Clear()
    '        If txtOrderDate.Text <> "" Then
    '            dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            dt = objOral.GetExciseOnOrderDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dOrderDate)
    '            'Else
    '            '    sDate = "01/01/1900"
    '            '    dt = objOral.GetExciseOnOrderDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, sDate)
    '        End If
    '        If dt.Rows.Count > 0 Then
    '            'dt = objOral.LoadExciseFromGeneralMaster(sSession.AccessCode, sSession.AccessCodeID)
    '            ddlExcise.DataSource = dt
    '            ddlExcise.DataTextField = "Mas_Desc"
    '            ddlExcise.DataValueField = "Mas_Id"
    '            ddlExcise.DataBind()
    '            ddlExcise.Items.Insert(0, "Select Excise")
    '            If dt.Rows.Count = 1 Then
    '                ddlExcise.SelectedValue = dt.Rows(0)("Mas_ID")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub BindUnitOfMeassurement(ByVal iItemID As Integer, ByVal iHistoryID As Integer)
        Try
            ddlUnitOfMeassurement.DataSource = objOral.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iItemID, iHistoryID)
            ddlUnitOfMeassurement.DataTextField = "Mas_Desc"
            ddlUnitOfMeassurement.DataValueField = "Mas_ID"
            ddlUnitOfMeassurement.DataBind()
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Dim IHistoryID As Integer

        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer
        Dim dOrderDate As Date
        Try
            txtMRP.Enabled = False
            lblError.Text = ""
            ClearonPartySelection()
            BindDescription(0)
            If txtOrderDate.Text <> "" Then
                If ddlParty.SelectedIndex > 0 Then
                    lstBoxDescription.Enabled = True
                    dt = objOral.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            'txtPartyNo.Text = dt.Rows(i)("ACM_Code")
                            'txtAddress.Text = dt.Rows(i)("ACAD_Address")
                            'txtContactNo.Text = dt.Rows(i)("ACAD_MobileNo")

                            'If IsDBNull(dt.Rows(i)("ACM_GenCategory")) = False Then
                            '    ddlCategory.SelectedValue = dt.Rows(i)("ACM_GenCategory")
                            'Else
                            '    ddlCategory.SelectedIndex = 0
                            'End If
                            txtPartyNo.Text = dt.Rows(i)("BM_Code")
                            txtAddress.Text = dt.Rows(i)("BM_Address")
                            txtContactNo.Text = dt.Rows(i)("BM_MobileNo")

                            If IsDBNull(dt.Rows(i)("BM_GenCategory")) = False Then
                                If dt.Rows(i)("BM_GenCategory") > 0 Then
                                    ddlCategory.SelectedValue = dt.Rows(i)("BM_GenCategory")
                                Else
                                    ddlCategory.SelectedIndex = 0
                                End If
                            Else
                                ddlCategory.SelectedIndex = 0
                            End If

                            txtBillingAddress.Text = dt.Rows(i)("BM_Address")
                            txtBillingGSTNRegNo.Text = dt.Rows(i)("BM_GSTNRegNo")
                            'ddlCompanyType.SelectedValue = dt.Rows(i)("BM_CompanyType")
                            'If ddlCompanyType.SelectedIndex > 0 Then
                            '    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                            '    ddlGSTCategory.SelectedValue = dt.Rows(i)("BM_GSTNCategory")
                            'Else
                            '    ddlGSTCategory.Items.Clear()
                            'End If
                            Dim taxcategory As String

                            taxcategory = objOral.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("BM_GSTNCategory"))
                            If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                                txtDeliveryFromGSTNRegNo.Enabled = False
                            Else
                                txtDeliveryFromGSTNRegNo.Enabled = True
                            End If

                            txtDeleveryAddress.Text = dt.Rows(i)("BM_Address")
                            txtDeliveryGSTNRegNo.Text = dt.Rows(i)("BM_GSTNRegNo")

                        Next
                    End If

                    If ddlCategory.SelectedIndex > 0 Then
                        iCategoryID = ddlCategory.SelectedValue
                    Else
                        iCategoryID = 0
                    End If

                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    sCode = objOral.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                    If ddlCommodity.SelectedIndex > 0 Then
                        If lstBoxDescription.SelectedIndex <> -1 Then
                            sStockHistoryID = objOral.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                            If sCode.StartsWith("P") Then
                                'txtMRP.Enabled = True
                                txtCode.Value = "P"
                                lblPCRate.Text = "Retail Rate"
                                'If lstBoxDescription.SelectedIndex <> -1 Then
                                dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                                'If dt.Rows.Count = 0 Then
                                '    Exit Sub
                                'End If
                                If dt.Rows.Count > 1 Then
                                    ddlRate.Enabled = True
                                    ddlRate.DataSource = dt
                                    ddlRate.DataValueField = "INVH_ID"
                                    ddlRate.DataTextField = "INVH_Retail"
                                    ddlRate.DataBind()
                                    hfMRP.Value = ddlRate.SelectedItem.Text
                                    txtMRP.Text = ddlRate.SelectedItem.Text
                                    txtMRPFromTable.Text = ddlRate.SelectedItem.Text

                                    'IHistoryID = clsPROFormaSalesOrder.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, txtMRP.Text, "P")
                                    BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                                    'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                    'LoadVAT(ddlRate.SelectedValue)
                                    lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                                Else
                                    ddlRate.Enabled = False
                                    hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                    txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                    txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                                    'IHistoryID = clsPROFormaSalesOrder.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, txtMRP.Text, "P")
                                    'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
                                    IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "P", dOrderDate)
                                    BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                                    'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                                    'LoadVAT(IHistoryID)

                                    lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                                    If txtMRP.Text = 0 Then
                                        txtMRP.Text = ""
                                        lblEffectiveDates.Text = ""
                                    End If
                                End If
                                'End If
                            Else
                                'txtMRP.Enabled = False
                                txtCode.Value = "C"
                                lblPCRate.Text = "MRP Rate"
                                'If lstBoxDescription.SelectedIndex <> -1 Then
                                dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                                'If dt.Rows.Count = 0 Then
                                '    Exit Sub
                                'End If
                                If dt.Rows.Count > 1 Then
                                    ddlRate.Enabled = True
                                    ddlRate.DataSource = dt
                                    ddlRate.DataValueField = "INVH_ID"
                                    ddlRate.DataTextField = "INVH_MRP"
                                    ddlRate.DataBind()
                                    hfMRP.Value = ddlRate.SelectedItem.Text
                                    txtMRP.Text = ddlRate.SelectedItem.Text
                                    txtMRPFromTable.Text = ddlRate.SelectedItem.Text

                                    'IHistoryID = clsPROFormaSalesOrder.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, txtMRP.Text, "C")
                                    BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                                    'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                    'LoadVAT(ddlRate.SelectedValue)
                                    lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                                Else
                                    ddlRate.Enabled = False
                                    hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                    txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                    txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                                    'IHistoryID = clsPROFormaSalesOrder.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, txtMRP.Text, "C")
                                    'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
                                    IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "C", dOrderDate)
                                    BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                                    'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                                    'LoadVAT(IHistoryID)

                                    lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                                    If txtMRP.Text = 0 Then
                                        txtMRP.Text = ""
                                        lblEffectiveDates.Text = ""
                                    End If

                                End If
                                'End If

                            End If
                        End If
                    End If
                Else
                    txtPartyNo.Text = "" : txtAddress.Text = "" : txtContactNo.Text = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadExistingOrderGrid(ByVal iMasterID As Integer)
        Dim dt As New DataTable
        Dim iTotal As Double
        Dim iGrandTotal As Double
        Dim dTradeDisAmt As Double
        Try
            pnlGrand.Visible = True
            dt = objOral.BindExistingOrder(sSession.AccessCode, sSession.AccessCodeID, iMasterID)
            If dt.Rows.Count > 0 Then
                dgExistingProFormaSalesOrder.DataSource = dt
                dgExistingProFormaSalesOrder.DataBind()
                dgExistingProFormaSalesOrder.Visible = True
                Session("PROFORMA") = dt
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If IsDBNull(dt.Rows(i)("NetAmount")) = False Then
                            iTotal = dt.Rows(i)("NetAmount")
                            iGrandTotal = iGrandTotal + iTotal
                        End If
                    Next
                    txtGrandTotal.Text = iGrandTotal
                    txtGrandTotalAmt.Text = iGrandTotal

                    If txtGrandDiscount.Text <> "" Then
                        dTradeDisAmt = (txtGrandTotal.Text * txtGrandDiscount.Text) / 100
                        txtGrandDiscountAmt.Text = dTradeDisAmt
                        txtGrandTotalAmt.Text = (iGrandTotal - dTradeDisAmt)
                    End If
                End If
            Else
                dgExistingProFormaSalesOrder.DataSource = Nothing
                dgExistingProFormaSalesOrder.DataBind()

                lblError.Text = "No Orders Found."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingOrderGrid")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim Arr() As String
        Dim dt As New DataTable
        Dim iMasterID As Integer
        Dim dOrderDate As Date
        Dim sCode As String = "" : Dim sStr As String = ""
        Dim iOrderID As Integer
        Dim dDate, dSDate As Date : Dim m As Integer

        Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Dim sName As String = ""
        Dim iBaseID As Integer = 0
        Try
            lblError.Text = ""

            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            'Check Application Settings'

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146
            If iGL = 0 Then
                lblError.Text = "Set The Application Settings."
                Exit Sub
            End If

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146
            If iGL = 0 Then
                lblError.Text = "Set The Application Settings."
                Exit Sub
            End If

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            If iSubGroup = 0 Then
                lblError.Text = "Set The Application Settings."
                Exit Sub
            End If
            'Check Application Settings'

            If ddlSearch.SelectedIndex > 0 Then
                iOrderID = ddlSearch.SelectedValue
            Else
                If txtOrderID.Text <> "" Then
                    iOrderID = txtOrderID.Text
                Else
                    iOrderID = 0
                End If
            End If

            Dim sString As String = ""
            'Check Order has been already dispatched or not'
            sString = objOral.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            If sString = "True" Then
                lblError.Text = "For This Order No Invoice has been done."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            'Check Order has been already dispatched or not'

            'Save Master'
            objOral.SPO_OrderCode = objGen.SafeSQL(Trim(txtOrderCode.Text))
            If txtOrderDate.Text = "" Then
                lblError.Text = "Enter Order Date"
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtOrderDate.Focus()
                Exit Sub
            End If
            objOral.SPO_OrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Dim dDatel, dSDateo As Date
            'Cheque Date Comparision'
            dDatel = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDateo = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim f As Integer
            f = DateDiff(DateInterval.Day, dDatel, dSDateo)
            If f < 0 Then
                lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Application Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtOrderDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtDispatchDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            objOral.SPO_PartyCode = objGen.SafeSQL(Trim(txtPartyNo.Text))
            objOral.SPO_PartyName = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
            objOral.SPO_Address = objGen.SafeSQL(Trim(txtAddress.Text))
            objOral.SPO_ContantNo = objGen.SafeSQL(Trim(txtContactNo.Text))

            If ddlModeOfShipping.SelectedIndex > 0 Then
                objOral.SPO_ModeOfDispatch = objGen.SafeSQL(Trim(ddlModeOfShipping.SelectedValue))
            Else
                objOral.SPO_ModeOfDispatch = 0
            End If
            If txtShippingDate.Text <> "" Then
                objOral.SPO_ShippingDate = Date.ParseExact(objGen.SafeSQL(Trim(txtShippingDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtShippingDate.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtShippingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Shipping Date (" & txtShippingDate.Text & ") should be Greater than or equal to Order Date(" & txtOrderDate.Text & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtShippingDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            objOral.SPO_PaymentType = objGen.SafeSQL(Trim(ddlPaymentType.SelectedValue))

            If ddlModeOfCommunication.SelectedIndex > 0 Then
                objOral.SPO_ModeOfCommunication = objGen.SafeSQL(Trim(ddlModeOfCommunication.SelectedValue))
            Else
                objOral.SPO_ModeOfCommunication = 0
            End If
            If txtInputBy.Text <> "" Then
                objOral.SPO_InputBy = objGen.SafeSQL(Trim(txtInputBy.Text))
            Else
                objOral.SPO_InputBy = ""
            End If

            If ddlShippingCharges.SelectedIndex > 0 Then
                objOral.SPO_ShippingCharge = objGen.SafeSQL(Trim(ddlShippingCharges.SelectedValue))
            Else
                objOral.SPO_ShippingCharge = 0
            End If

            objOral.SPO_CreatedBy = sSession.UserID
            objOral.SPO_CreatedOn = DateTime.Today
            objOral.SPO_Status = "A"
            objOral.SPO_Operation = "C"
            objOral.SPO_IPAddress = sSession.IPAddress

            objOral.SPO_OrderType = "O"
            objOral.SPO_DispatchFlag = 0

            If ddlSalesMan.SelectedIndex > 0 Then
                objOral.SPO_SalesManID = objGen.SafeSQL(Trim(ddlSalesMan.SelectedValue))
            Else
                objOral.SPO_SalesManID = 0
            End If

            If txtBuyerPurOrderNo.Text <> "" Then
                objOral.SPO_BuyerOrderNo = objGen.SafeSQL(Trim(txtBuyerPurOrderNo.Text))
            Else
                objOral.SPO_BuyerOrderNo = "Oral"
            End If

            If ddlCategory.SelectedIndex > 0 Then
                objOral.SPO_Category = objGen.SafeSQL(Trim(ddlCategory.SelectedValue))
            Else
                objOral.SPO_Category = 0
            End If

            If txtRemarks.Text <> "" Then
                objOral.SPO_Remarks = objGen.SafeSQL(Trim(txtRemarks.Text))
            Else
                objOral.SPO_Remarks = ""
            End If

            If txtBuyerOrderDate.Text <> "" Then
                objOral.SPO_BuyerOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtBuyerOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            objOral.SPO_SalesType = 1
            objOral.SPO_OtherType = 0

            If txtChequeNo.Text <> "" Then
                objOral.SPO_ChequeNo = txtChequeNo.Text
            Else
                objOral.SPO_ChequeNo = ""
            End If
            If txtChequeDate.Text <> "" Then
                objOral.SPO_ChequeDate = Date.ParseExact(Trim(txtChequeDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtIFSCCode.Text <> "" Then
                objOral.SPO_IFSCCode = txtIFSCCode.Text
            Else
                objOral.SPO_IFSCCode = ""
            End If
            If ddlBankName.SelectedIndex > 0 Then
                objOral.SPO_BankName = ddlBankName.SelectedValue
            Else
                objOral.SPO_BankName = 0
            End If
            If txtBranch.Text <> "" Then
                objOral.SPO_Branch = txtBranch.Text
            Else
                objOral.SPO_Branch = ""
            End If
            objOral.SPO_GoThroughDispatch = 0

            If txtDispatchRefNo.Text <> "" Then
                objOral.SPO_DispatchRefNo = txtDispatchRefNo.Text
            Else
                objOral.SPO_DispatchRefNo = ""
            End If

            If txtESugamNo.Text <> "" Then
                objOral.SPO_ESugamNo = txtESugamNo.Text
            Else
                objOral.SPO_ESugamNo = ""
            End If

            If txtDispatchDate.Text <> "" Then
                objOral.SPO_DispatchDate = Date.ParseExact(Trim(txtDispatchDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtDispatchDate.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Dispatch Date (" & txtDispatchDate.Text & ") should be Greater than or equal to Order Date(" & txtOrderDate.Text & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtDispatchDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            objOral.SPO_ZoneID = ddlAccZone.SelectedValue
            objOral.SPO_RegionID = ddlAccRgn.SelectedValue
            objOral.SPO_AreaID = ddlAccArea.SelectedValue
            'objOral.SPO_BranchID = ddlAccBrnch.SelectedValue
            If ddlAccBrnch.SelectedIndex > 0 Then
                objOral.SPO_BranchID = ddlAccBrnch.SelectedValue
            Else
                objOral.SPO_BranchID = 0
            End If

            objOral.SPO_TrType = 10 'Cash Sales

            If txtCompanyAddress.Text <> "" Then
                objOral.SPO_CompanyAddress = txtCompanyAddress.Text
            Else
                objOral.SPO_CompanyAddress = ""
            End If

            If txtBillingAddress.Text <> "" Then
                objOral.SPO_BillingAddress = txtBillingAddress.Text
            Else
                objOral.SPO_BillingAddress = ""
            End If

            If txtDeliveryFromAddress.Text <> "" Then
                objOral.SPO_DeliveryFrom = txtDeliveryFromAddress.Text
            Else
                objOral.SPO_DeliveryFrom = ""
            End If

            If txtDeleveryAddress.Text <> "" Then
                objOral.SPO_DeliveryAddress = txtDeleveryAddress.Text
            Else
                objOral.SPO_DeliveryAddress = ""
            End If

            If txtCompanyGSTNRegNo.Text <> "" Then
                objOral.SPO_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
            Else
                objOral.SPO_CompanyGSTNRegNo = ""
            End If

            If txtBillingGSTNRegNo.Text <> "" Then
                objOral.SPO_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
            Else
                objOral.SPO_BillingGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" Then
                objOral.SPO_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
            Else
                objOral.SPO_DeliveryFromGSTNRegNo = ""
            End If

            If txtDeliveryGSTNRegNo.Text <> "" Then
                objOral.SPO_DeliveryGSTNRegNo = txtDeliveryGSTNRegNo.Text
            Else
                objOral.SPO_DeliveryGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objOral.SPO_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                objOral.SPO_DispatchStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objOral.SPO_DispatchStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                objOral.SPO_DispatchStatus = "Local"
            End If
            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
            '    objOral.SPO_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            'End If

            objOral.SPO_CompanyType = ddlCompanyType.SelectedValue
            objOral.SPO_GSTNCategory = ddlGSTCategory.SelectedValue

            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
            '    objOral.SPO_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            'End If
            If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objOral.SPO_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                objOral.SPO_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), (""))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objOral.SPO_State = CheckSourceDestinationState((""), Trim(txtDeliveryGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                Dim ibranch As Integer
                ibranch = objOral.getBranchFromPO(sSession.AccessCode, sSession.AccessCodeID, txtOrderCode.Text)
                If ibranch > 0 Then 'branch 
                    objOral.SPO_State = objOral.CheckDetailsofBranchState(sSession.AccessCode, sSession.AccessCodeID, txtOrderCode.Text)
                    If objOral.SPO_State = "" Then
                        lblError.Text = "Update state in branch master"
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in branch master.','', 'success');", True)
                        Exit Sub
                    End If
                Else 'Companyl
                    objOral.SPO_State = objOral.CheckDetailsofCompState(sSession.AccessCode, sSession.AccessCodeID)
                    If objOral.SPO_State = "" Then
                        lblError.Text = "Update state in company master"
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in company master.','', 'success');", True)
                        Exit Sub
                    End If
                End If
            End If

            'Chart Of Accounts'

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146

            'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
            sName = "Sale Of Product " & objOral.SPO_State
            txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
            Else
                iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
            End If
            'Chart Of Accounts'

            Dim dtGSTRates As New DataTable
            dtGSTRates = objOral.BindGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dtGSTRates.Rows.Count > 0 Then
                For x = 0 To dtGSTRates.Rows.Count - 1

                    sName = "Local GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objOral.SPO_State & " Sale Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                    sName = "Inter State GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objOral.SPO_State & " Sale Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                    sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")
                    iHead = sArray1(0) '1
                    iGroup = sArray1(1) '29
                    iSubGroup = sArray1(2) '31
                    iGL = sArray1(3) '146

                    sName = "OUTPUT SGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objOral.SPO_State & " Sale Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "OUTPUT CGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objOral.SPO_State & " Sale Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "OUTPUT IGST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objOral.SPO_State & " Sale Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If
                Next
            End If

            objOral.SPO_BatchNo = 0
            objOral.SPO_BaseName = 0

            Arr = objOral.SavePROFormaMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objOral)
            dt = Session("UpdateTab")
            iMasterID = Arr(1)

            objOral.SPOD_SOID = objGen.SafeSQL(Trim(iMasterID))

            If txtQuantity.Text <> "" Then

                If txtDetailIDTable.Text <> "" Then
                    objOral.SPOD_Id = txtDetailIDTable.Text
                Else
                    objOral.SPOD_Id = 0
                End If

                objOral.SPOD_CommodityID = objGen.SafeSQL(Trim(ddlCommodity.SelectedValue))
                objOral.SPOD_ItemID = objGen.SafeSQL(Trim(lstBoxDescription.SelectedValue))
                objOral.SPOD_Quantity = objGen.SafeSQL(Trim(txtQuantity.Text))

                If ddlDiscount.SelectedIndex > 0 Then
                    objOral.SPOD_Discount = objGen.SafeSQL(Trim(ddlDiscount.SelectedItem.Text))
                Else
                    objOral.SPOD_Discount = 0
                End If

                objOral.SPOD_UnitofMeasurement = objGen.SafeSQL(Trim(ddlUnitOfMeassurement.SelectedValue))

                If hfAmount.Value <> "" Then
                    objOral.SPOD_RateAmount = Request.Form(hfAmount.UniqueID)
                Else
                    objOral.SPOD_RateAmount = txtAmount.Text
                End If

                If hfDiscountAmount.Value <> "" Then
                    objOral.SPOD_DiscountRate = Request.Form(hfDiscountAmount.UniqueID)
                Else
                    objOral.SPOD_DiscountRate = 0
                End If

                If txtOrderDate.Text <> "" Then
                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If

                sCode = objOral.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                If sCode.StartsWith("P") Then
                    sStr = "P"
                Else
                    sStr = "C"
                End If

                If ddlRate.SelectedIndex <> -1 Then
                    objOral.SPOD_HistoryID = ddlRate.SelectedValue
                Else
                    objOral.SPOD_HistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, objOral.SPO_Category, sStr, dOrderDate)
                End If

                objOral.SPOD_CompiD = sSession.AccessCodeID
                objOral.SPOD_Status = "A"

                If txtMRP.Text <> "" Then
                    hfMRP.Value = txtMRP.Text
                End If
                If hfMRP.Value <> "" Then
                    objOral.SPOD_MRPRate = hfMRP.Value
                Else
                    objOral.SPOD_MRPRate = 0
                End If

                objOral.SPOD_VAT = 0
                objOral.SPOD_VATAmount = 0
                objOral.SPOD_Excise = 0
                objOral.SPOD_ExciseAmount = 0

                objOral.SPOD_Operation = "C"
                objOral.SPOD_IPAddress = sSession.IPAddress

                If ddlCategory.SelectedIndex > 0 Then
                    objOral.SPOD_Category = ddlCategory.SelectedValue
                Else
                    objOral.SPOD_Category = 0
                End If

                objOral.SPOD_CreatedBy = sSession.UserID
                objOral.SPOD_CreatedOn = DateTime.Today
                objOral.SPOD_UpdatedBy = sSession.UserID
                objOral.SPOD_UpdatedOn = DateTime.Today

                objOral.SPOD_GST_ID = txtGSTID.Text
                objOral.SPOD_GSTRate = txtGSTRate.Text
                objOral.SPOD_GSTAmount = txtGSTAmount.Text

                If objOral.SPO_DispatchStatus = "Local" Then
                    objOral.SPOD_SGST = objOral.SPOD_GSTRate / 2
                    objOral.SPOD_SGSTAmount = objOral.SPOD_GSTAmount / 2
                    objOral.SPOD_CGST = objOral.SPOD_GSTRate / 2
                    objOral.SPOD_CGSTAmount = objOral.SPOD_GSTAmount / 2
                    objOral.SPOD_IGST = 0
                    objOral.SPOD_IGSTAmount = 0
                ElseIf objOral.SPO_DispatchStatus = "Inter State" Then
                    objOral.SPOD_SGST = 0
                    objOral.SPOD_SGSTAmount = 0
                    objOral.SPOD_CGST = 0
                    objOral.SPOD_CGSTAmount = 0
                    objOral.SPOD_IGST = objOral.SPOD_GSTRate
                    objOral.SPOD_IGSTAmount = objOral.SPOD_GSTAmount
                End If

                'If UCase(ddlGSTCategory.SelectedItem.Text) = "UNRIGISTERED DEALER" Then
                '    Dim URD_GSTRate, URD_GSTAmt As Double

                '    URD_GSTRate = objOral.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                '    URD_GSTAmt = (((objOral.SPOD_RateAmount - objOral.SPOD_DiscountRate)) * URD_GSTRate) / 100

                '    objOral.SPOD_SGST = URD_GSTRate / 2
                '    objOral.SPOD_SGSTAmount = URD_GSTAmt / 2
                '    objOral.SPOD_CGST = URD_GSTRate / 2
                '    objOral.SPOD_CGSTAmount = URD_GSTAmt / 2
                '    objOral.SPOD_IGST = 0
                '    objOral.SPOD_IGSTAmount = 0
                'End If

                If hfNetAmount.Value <> "" Then
                    objOral.SPOD_TotalAmount = Request.Form(hfNetAmount.UniqueID)
                Else
                    objOral.SPOD_TotalAmount = 0
                End If

                iBaseID = objProForma.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
                If hfNetAmount.Value <> "" Then
                    objOral.dSPOD_FETotalAmt = objGen.SafeSQL(hfNetAmount.Value)
                    objOral.dSPOD_CurrencyAmt = 0
                    objOral.sSPOD_CurrencyTime = ""
                    objOral.iSPOD_Currency = iBaseID
                Else
                    objOral.dSPOD_FETotalAmt = 0
                    objOral.dSPOD_CurrencyAmt = 0
                    objOral.sSPOD_CurrencyTime = ""
                    objOral.iSPOD_Currency = iBaseID
                End If

                Arr = objOral.SavePROFormaMasterDetails(sSession.AccessCode, objOral, sSession.YearID)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnAdd.ImageUrl = "~/Images/Add24.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
            End If
            LoadExistingOrderNo()
            ddlSearch.SelectedValue = iMasterID
            LoadExistingOrderGrid(iMasterID)

            pnlGrand.Visible = True
            'imgbtnUpdate.Visible = True : imgbtnReport.Visible = True : imgbtnApprove.Visible = True

            'If btnSave.Text = "Update" Then
            '    objOral.UpdateOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, lstBoxDescription.SelectedValue, ddlCommodity.SelectedValue, iMasterID)
            '    btnSave.Text = "Add"
            'End If
            txtOrderID.Text = iMasterID

            'TradeDiscount updation'
            TradeDiscount()
            'TradeDiscount updation'

            ClearDetails()
            'imgbtnApprove.Visible = True

            'divcollapseCharges.Visible = True
            SaveCharges(iMasterID, 0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub SaveCharges(ByVal iOrderID As Integer, ByVal iDispatchID As Integer)
        Dim objDispatch As New ClsDispatchDetails
        Dim Arr() As String
        Try
            'Dim lblChargeID, lblChargeType, lblChargeAmount As New Label
            'Charges Saving'

            'Deleting charges Everytime & Saving'
            If ddlSearch.SelectedIndex > 0 Then
                iOrderID = ddlSearch.SelectedValue
            Else
                iOrderID = txtOrderID.Text
            End If
            objOral.DeleteOralCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, 0)
            'Deleting charges Everytime & Saving'

            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1

                    'lblChargeID = GvCharge.Rows(i).FindControl("lblChargeID")
                    'lblChargeType = GvCharge.Rows(i).FindControl("lblChargeType")
                    'lblChargeAmount = GvCharge.Rows(i).FindControl("lblChargeAmount")

                    objDispatch.C_ID = 0
                    objDispatch.C_OrderID = iOrderID
                    objDispatch.C_AllocatedID = 0
                    objDispatch.C_DispatchID = 0
                    objDispatch.C_OrderType = ""
                    objDispatch.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objDispatch.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objDispatch.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objDispatch.C_PSType = "S"
                    objDispatch.C_DelFlag = "W"
                    objDispatch.C_Status = "C"
                    objDispatch.C_CompID = sSession.AccessCodeID
                    objDispatch.C_YearID = sSession.YearID
                    objDispatch.C_CreatedBy = sSession.UserID
                    objDispatch.C_CreatedOn = System.DateTime.Now
                    objDispatch.C_Operation = "C"
                    objDispatch.C_IPAddress = sSession.IPAddress
                    objDispatch.C_SalesReturnID = 0
                    objDispatch.C_GoodsReturnID = 0

                    Arr = objDispatch.SaveCharges(sSession.AccessCode, objDispatch)
                Next
            End If
            'Charges Saving'
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveCharges")
        End Try
    End Sub
    Public Sub ClearonPartySelection()
        Try
            ddlModeOfShipping.SelectedIndex = 0 : txtShippingDate.Text = ""
            ddlPaymentType.SelectedIndex = 0 : ddlModeOfCommunication.SelectedIndex = 0 : ddlCommodity.SelectedIndex = 0
            txtInputBy.Text = "" : ddlShippingCharges.SelectedIndex = 0
            txtOrderID.Text = "" : txtMRPFromTable.Text = ""

            lstBoxDescription.Items.Clear()
            ddlUnitOfMeassurement.Items.Clear() : ddlRate.Items.Clear() : txtMRP.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = ""
            ddlDiscount.SelectedIndex = 0 : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            dgExistingProFormaSalesOrder.DataSource = Nothing
            dgExistingProFormaSalesOrder.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearonPartySelection")
        End Try
    End Sub
    Public Sub Clear()
        Try
            imgbtnAdd.ImageUrl = "~/Images/Add24.png"
            ddlSearch.SelectedIndex = 0
            txtOrderCode.Text = "" : txtOrderDate.Text = "" : ddlModeOfShipping.SelectedIndex = 0 : txtShippingDate.Text = ""
            ddlPaymentType.SelectedIndex = 0 : ddlModeOfCommunication.SelectedIndex = 0 : ddlCommodity.SelectedIndex = 0
            ddlParty.SelectedIndex = 0 : txtPartyNo.Text = "" : txtContactNo.Text = "" : txtAddress.Text = ""
            txtInputBy.Text = "" : ddlShippingCharges.SelectedIndex = 0
            txtOrderID.Text = "" : txtMRPFromTable.Text = ""

            lstBoxDescription.Items.Clear() : txtDetailIDTable.Text = ""
            ddlUnitOfMeassurement.Items.Clear() : ddlRate.Items.Clear() : txtMRP.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = ""
            ddlDiscount.SelectedIndex = 0 : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            dgExistingProFormaSalesOrder.DataSource = Nothing
            dgExistingProFormaSalesOrder.DataBind()

            txtBuyerPurOrderNo.Text = "" : txtBuyerOrderDate.Text = "" : txtRemarks.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Clear")
        End Try
    End Sub
    Public Sub ClearDetails()
        Try
            For i = 0 To lstBoxDescription.Items.Count - 1
                lstBoxDescription.Items(i).Selected = False
            Next
            ddlUnitOfMeassurement.Items.Clear() : ddlRate.Items.Clear() : txtMRP.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = ""
            ddlDiscount.SelectedIndex = 0 : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""
            txtGSTAmount.Text = "" : hfGSTAmount.Value = "" : txtGSTRate.Text = "" : txtGSTID.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearDetails")
        End Try
    End Sub
    Private Sub ddlSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearch.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Dim bCheck As String = ""
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            'divcollapseCharges.Visible = True
            If ddlSearch.SelectedIndex > 0 Then

                'imgbtnAdd.Visible = False : imgbtnUpdate.Visible = True : imgbtnDelete.Visible = True : imgbtnReport.Visible = True
                imgbtnAdd.ImageUrl = "~/Images/Update24.png"

                txtGrandTotal.Text = "" : txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotalAmt.Text = ""
                txtSearch.Visible = False : btnSearch.Visible = False
                lstBoxDescription.Enabled = True
                dtMaster = objOral.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("SPO_OrderCode")) = False Then
                            txtOrderCode.Text = dtMaster.Rows(i)("SPO_OrderCode")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_OrderDate")) = False Then
                            txtOrderDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_OrderDate"), "D")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_ModeOfDispatch")) = False Then
                            If dtMaster.Rows(i)("SPO_ModeOfDispatch") > 0 Then
                                ddlModeOfShipping.SelectedValue = dtMaster.Rows(i)("SPO_ModeOfDispatch")
                            Else
                                ddlModeOfShipping.SelectedIndex = 0
                            End If
                        Else
                            ddlModeOfShipping.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_SalesManID")) = False Then
                            If dtMaster.Rows(i)("SPO_SalesManID") > 0 Then
                                ddlSalesMan.SelectedValue = dtMaster.Rows(i)("SPO_SalesManID")
                            Else
                                ddlSalesMan.SelectedIndex = 0
                            End If
                        Else
                            ddlSalesMan.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_BuyerOrderNo")) = False Then
                            txtBuyerPurOrderNo.Text = dtMaster.Rows(i)("SPO_BuyerOrderNo")
                        Else
                            txtBuyerPurOrderNo.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_BuyerOrderDate")) = False Then
                            If (dtMaster.Rows(i)("SPO_BuyerOrderDate")) <> "1899-12-30 00:00:00.000" Then
                                txtBuyerOrderDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_BuyerOrderDate"), "D")
                            Else
                                txtBuyerOrderDate.Text = ""
                            End If
                        Else
                            txtBuyerOrderDate.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_Category")) = False Then
                            If dtMaster.Rows(i)("SPO_Category") > 0 Then
                                ddlCategory.SelectedValue = dtMaster.Rows(i)("SPO_Category")
                            Else
                                ddlCategory.SelectedIndex = 0
                            End If
                        Else
                            ddlCategory.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_Remarks")) = False Then
                            txtRemarks.Text = dtMaster.Rows(i)("SPO_Remarks")
                        Else
                            txtRemarks.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_ShippingDate")) = False Then
                            If (dtMaster.Rows(i)("SPO_ShippingDate")) <> "1899-12-30 00:00:00.000" Then
                                txtShippingDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_ShippingDate"), "D")
                            Else
                                txtShippingDate.Text = ""
                            End If
                        Else
                            txtShippingDate.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_PaymentType")) = False Then
                            ddlPaymentType.SelectedValue = dtMaster.Rows(i)("SPO_PaymentType")
                        End If
                        If UCase(ddlPaymentType.SelectedItem.Text) = UCase("Cheque") Then
                            'VisibleTrue()
                            divcollapseChequeDetails.Visible = True
                            If IsDBNull(dtMaster.Rows(i)("SPO_ChequeNo")) = False Then
                                txtChequeNo.Text = dtMaster.Rows(i)("SPO_ChequeNo")
                            Else
                                txtChequeNo.Text = ""
                            End If
                            If IsDBNull(dtMaster.Rows(i)("SPO_ChequeDate")) = False Then
                                txtChequeDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_ChequeDate"), "D")
                            Else
                                txtChequeDate.Text = ""
                            End If
                            If IsDBNull(dtMaster.Rows(i)("SPO_IFSCCode")) = False Then
                                txtIFSCCode.Text = dtMaster.Rows(i)("SPO_IFSCCode")
                            Else
                                txtIFSCCode.Text = ""
                            End If
                            If IsDBNull(dtMaster.Rows(i)("SPO_BankName")) = False Then
                                ddlBankName.SelectedValue = dtMaster.Rows(i)("SPO_BankName")
                            Else
                                ddlBankName.SelectedIndex = 0
                            End If
                            If IsDBNull(dtMaster.Rows(i)("SPO_Branch")) = False Then
                                txtBranch.Text = dtMaster.Rows(i)("SPO_Branch")
                            Else
                                txtBranch.Text = ""
                            End If
                        Else
                            'VisibleFalse()
                            divcollapseChequeDetails.Visible = False
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_PartyCode")) = False Then
                            txtPartyNo.Text = dtMaster.Rows(i)("SPO_PartyCode")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_PartyName")) = False Then
                            ddlParty.SelectedValue = dtMaster.Rows(i)("SPO_PartyName")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_ContantNo")) = False Then
                            txtContactNo.Text = dtMaster.Rows(i)("SPO_ContantNo")
                        Else
                            txtContactNo.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_Address")) = False Then
                            txtAddress.Text = dtMaster.Rows(i)("SPO_Address")
                        Else
                            txtAddress.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_ModeOfCommunication")) = False Then
                            If dtMaster.Rows(i)("SPO_ModeOfCommunication") > 0 Then
                                ddlModeOfCommunication.SelectedValue = dtMaster.Rows(i)("SPO_ModeOfCommunication")
                            Else
                                ddlModeOfCommunication.SelectedIndex = 0
                            End If
                        Else
                            ddlModeOfCommunication.SelectedIndex = 0
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_InputBy")) = False Then
                            txtInputBy.Text = dtMaster.Rows(i)("SPO_InputBy")
                        Else
                            txtInputBy.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_ShippingCharge")) = False Then
                            If dtMaster.Rows(i)("SPO_ShippingCharge") > 0 Then
                                ddlShippingCharges.SelectedValue = dtMaster.Rows(i)("SPO_ShippingCharge")
                            Else
                                ddlShippingCharges.SelectedIndex = 0
                            End If
                        Else
                            ddlShippingCharges.SelectedIndex = 0
                        End If
                        '----------------'
                        If IsDBNull(dtMaster.Rows(i)("SPO_DispatchRefNo")) = False Then
                            txtDispatchRefNo.Text = dtMaster.Rows(i)("SPO_DispatchRefNo")
                        Else
                            txtDispatchRefNo.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SPO_ESugamNo")) = False Then
                            txtESugamNo.Text = dtMaster.Rows(i)("SPO_ESugamNo")
                        Else
                            txtESugamNo.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_DispatchDate")) = False Then
                            If (dtMaster.Rows(i)("SPO_DispatchDate")) <> "1899-12-30 00:00:00.000" Then
                                txtDispatchDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SPO_DispatchDate"), "D")
                            Else
                                txtDispatchDate.Text = ""
                            End If
                        Else
                            txtDispatchDate.Text = ""
                        End If
                        BindCharges()
                        '----------------'

                        If IsDBNull(dtMaster.Rows(i)("SPO_GrandDiscount")) = False Then
                            txtGrandDiscount.Text = dtMaster.Rows(i)("SPO_GrandDiscount")
                        Else
                            txtGrandDiscount.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_GrandDiscountAmt")) = False Then
                            txtGrandDiscountAmt.Text = dtMaster.Rows(i)("SPO_GrandDiscountAmt")
                        Else
                            txtGrandDiscountAmt.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_GrandTotal")) = False Then
                            txtGrandTotal.Text = dtMaster.Rows(i)("SPO_GrandTotal")
                        Else
                            txtGrandTotal.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SPO_GrandTotalAmt")) = False Then
                            txtGrandTotalAmt.Text = dtMaster.Rows(i)("SPO_GrandTotalAmt")
                        Else
                            txtGrandTotalAmt.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(0)("SPO_ZoneID").ToString()) = False Then
                            If dtMaster.Rows(0)("SPO_ZoneID").ToString() = "" Then
                            Else
                                ddlAccZone.SelectedValue = dtMaster.Rows(0)("SPO_ZoneID").ToString()
                                LoadRegion(ddlAccZone.SelectedValue)
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(0)("SPO_RegionID").ToString()) = False Then
                            If dtMaster.Rows(0)("SPO_RegionID").ToString() = "" Then
                            Else
                                ddlAccRgn.SelectedValue = dtMaster.Rows(0)("SPO_RegionID").ToString()
                                LoadArea(ddlAccRgn.SelectedValue)
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(0)("SPO_AreaID").ToString()) = False Then
                            If dtMaster.Rows(0)("SPO_AreaID").ToString() = "" Then
                            Else
                                ddlAccArea.SelectedValue = dtMaster.Rows(0)("SPO_AreaID").ToString()
                                LoadAccBrnch(ddlAccArea.SelectedValue)
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(0)("SPO_BranchID").ToString()) = False Then
                            If dtMaster.Rows(0)("SPO_BranchID").ToString() = "" Then
                            Else
                                ddlAccBrnch.SelectedValue = dtMaster.Rows(0)("SPO_BranchID").ToString()
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(0)("SPO_CompanyAddress")) = False Then
                            txtCompanyAddress.Text = dtMaster.Rows(0)("SPO_CompanyAddress")
                        Else
                            txtCompanyAddress.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(0)("SPO_CompanyGSTNRegNo")) = False Then
                            txtCompanyGSTNRegNo.Text = dtMaster.Rows(0)("SPO_CompanyGSTNRegNo")
                        Else
                            txtCompanyGSTNRegNo.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(0)("SPO_BillingAddress")) = False Then
                            txtBillingAddress.Text = dtMaster.Rows(0)("SPO_BillingAddress")
                        Else
                            txtBillingAddress.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(0)("SPO_BillingGSTNRegNo")) = False Then
                            txtBillingGSTNRegNo.Text = dtMaster.Rows(0)("SPO_BillingGSTNRegNo")
                        Else
                            txtBillingGSTNRegNo.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(0)("SPO_DeliveryFrom")) = False Then
                            txtDeliveryFromAddress.Text = dtMaster.Rows(0)("SPO_DeliveryFrom")
                        Else
                            txtDeliveryFromAddress.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(0)("SPO_DeliveryFromGSTNRegNo")) = False Then
                            txtDeliveryFromGSTNRegNo.Text = dtMaster.Rows(0)("SPO_DeliveryFromGSTNRegNo")
                        Else
                            txtDeliveryFromGSTNRegNo.Text = ""
                        End If

                        If IsDBNull(dtMaster.Rows(0)("SPO_DeliveryAddress")) = False Then
                            txtDeleveryAddress.Text = dtMaster.Rows(0)("SPO_DeliveryAddress")
                        Else
                            txtDeleveryAddress.Text = ""
                        End If
                        If IsDBNull(dtMaster.Rows(0)("SPO_DeliveryGSTNRegNo")) = False Then
                            txtDeliveryGSTNRegNo.Text = dtMaster.Rows(0)("SPO_DeliveryGSTNRegNo")
                        Else
                            txtDeliveryGSTNRegNo.Text = ""
                        End If

                    Next
                End If

                lstBoxDescription.Items.Clear()
                LoadExistingOrderGrid(ddlSearch.SelectedValue)

                bCheck = objOral.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If bCheck = True Then
                    lblStatus.Text = "Dispatched"
                    lblError.Text = "For Selected Order No Invoice has been done, it can not be edit."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    'btnSave.Enabled = False
                    'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                    'imgbtnApprove.Visible = False

                    Dim dtDispatch, dtCharge As New DataTable
                    dtDispatch = objOral.GetDispatchedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                    If dtDispatch.Rows.Count > 0 Then
                        txtDispatchDate.Text = objGen.FormatDtForRDBMS(dtDispatch.Rows(0)("SDM_DispatchDate"), "D")
                        txtDispatchRefNo.Text = dtDispatch.Rows(0)("SDM_DispatchRefNo")
                        txtESugamNo.Text = dtDispatch.Rows(0)("SDM_ESugamNo")
                    End If
                    dtCharge = objOral.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                    GvCharge.DataSource = dtCharge
                    GvCharge.DataBind()

                    Exit Sub
                Else
                    lblStatus.Text = "Waiting For Dispatch"
                    sStatus = objOral.CheckOrderForAllocationApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                    If sStatus <> "" Then
                        If sStatus = "A" Then
                            lblError.Text = "Selected Order No has been Allocated & Approved, it can not be edit."
                            lblCustomerValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            'btnSave.Enabled = False
                            'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                            Exit Sub
                        End If
                    End If
                End If
                'imgbtnApprove.Visible = True
            Else
                'ddlSearch.Visible = False
                txtSearch.Text = ""
                'txtSearch.Visible = True : btnSearch.Visible = True
                Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSearch_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub txtPartyNo_TextChanged(sender As Object, e As EventArgs) Handles txtPartyNo.TextChanged
        Dim dt As New DataTable
        Dim iPartyID As Integer
        Try
            'iPartyID = DBHelper.SQLDBExecScalarInteger(sSession.AccessCode, "Select BM_ID From Sales_Buyers_Masters Where BM_Code='" & Trim(txtPartyNo.Text) & "' and BM_CompID =" & sSession.AccessCodeID & "")
            iPartyID = objOral.GetPartyID(sSession.AccessCode, sSession.AccessCodeID, txtPartyNo.Text)
            dt = objOral.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, iPartyID)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    'ddlParty.SelectedValue = dt.Rows(i)("ACM_ID")
                    'txtAddress.Text = dt.Rows(i)("ACAD_Address")
                    'txtContactNo.Text = dt.Rows(i)("ACAD_MobileNo")
                    ddlParty.SelectedValue = dt.Rows(i)("BM_ID")
                    txtAddress.Text = dt.Rows(i)("BM_Address")
                    txtContactNo.Text = dt.Rows(i)("BM_MobileNo")
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtPartyNo_TextChanged")
        End Try
    End Sub

    Private Sub ddlUnitOfMeassurement_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnitOfMeassurement.SelectedIndexChanged
        Dim iPices As Double
        Dim iINVH_Unit As Integer
        Dim sVATAmt As String = ""
        Dim sVATAmount As String = ""
        Dim dBasicAmount As Double
        Try
            lblError.Text = ""
            If lstBoxDescription.SelectedValue > 0 Then
                If txtMRP.Text <> "" Then
                    'iPices = DBHelper.SQLDBExecScalarInteger(sSession.AccessCode, "Select INVH_PerPieces From Inventory_master_History Where INVH_INV_ID =" & lstBoxDescription.SelectedValue & "  And INVH_CompID=" & sSession.AccessCodeID & " ")
                    iPices = objOral.GetPieceCount(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                    iINVH_Unit = objOral.GetINVH_Unit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lstBoxDescription.SelectedValue)
                    If txtQuantity.Text <> "" Then
                        If ddlUnitOfMeassurement.SelectedValue = iINVH_Unit Then
                            txtMRP.Text = txtQuantity.Text * iPices * txtMRPFromTable.Text
                            hfMRP.Value = txtQuantity.Text * iPices * txtMRPFromTable.Text

                            txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * iPices * txtMRPFromTable.Text))
                            hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * iPices * txtMRPFromTable.Text))
                            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * iPices * txtMRPFromTable.Text))
                            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * iPices * txtMRPFromTable.Text))
                        Else
                            txtMRP.Text = txtQuantity.Text * txtMRPFromTable.Text
                            hfMRP.Value = txtQuantity.Text * txtMRPFromTable.Text

                            txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * txtMRPFromTable.Text))
                            hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * txtMRPFromTable.Text))
                            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * txtMRPFromTable.Text))
                            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * txtMRPFromTable.Text))
                        End If
                    Else
                        If ddlUnitOfMeassurement.SelectedValue = iINVH_Unit Then
                            txtMRP.Text = iPices * txtMRPFromTable.Text
                            hfMRP.Value = iPices * txtMRPFromTable.Text
                        Else
                            txtMRP.Text = txtMRPFromTable.Text
                            hfMRP.Value = txtMRPFromTable.Text
                        End If
                    End If

                    txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
                    txtNetAmount.Text = "" : hfNetAmount.Value = ""

                    If txtQuantity.Text <> "" Then

                        If txtQuantity.Text <> "" And ddlDiscount.SelectedIndex = 0 Then 'Q
                            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                        End If

                        If txtQuantity.Text <> "" And ddlDiscount.SelectedIndex > 0 Then 'QD

                            If txtCode.Value = "C" Then
                                txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))
                                hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))

                                txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                                hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                            End If
                            If txtCode.Value = "P" Then
                                txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))
                                hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))

                                txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                                hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                            End If

                        End If

                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlUnitOfMeassurement_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRate.SelectedIndexChanged
        Dim sCode As String = ""
        Dim sVATAmt As String = ""
        Dim sVATAmount As String = ""

        Dim dBasicAmount As Double
        Try
            If ddlRate.SelectedIndex <> -1 Then
                hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(ddlRate.SelectedItem.Text))
                txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(ddlRate.SelectedItem.Text))
                txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(ddlRate.SelectedItem.Text))

                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)

                'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                'LoadVAT(ddlRate.SelectedValue)
                'LoadExcise(ddlRate.SelectedValue)

                If txtQuantity.Text <> "" Then
                    txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * txtMRP.Text))
                    hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * txtMRP.Text))

                    txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
                    txtNetAmount.Text = "" : hfNetAmount.Value = ""

                    If txtQuantity.Text <> "" And ddlDiscount.SelectedIndex = 0 Then 'Q
                        txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                        hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                    End If

                    If txtQuantity.Text <> "" And ddlDiscount.SelectedIndex > 0 Then 'QD

                        If txtCode.Value = "C" Then
                            txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))
                            hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))

                            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                        End If
                        If txtCode.Value = "P" Then
                            txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))
                            hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))

                            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                        End If

                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRate_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub lstBoxDescription_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxDescription.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Dim IHistoryID As Integer
        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer
        Dim dOrderDate As Date
        Dim iGSTRate As Integer
        Try
            hfAvailableQty.Value = ""
            lblError.Text = ""
            'btnSave.Text = "Add"
            txtQuantity.Text = "" : txtAmount.Text = "" : hfAmount.Value = ""
            txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""
            txtGSTAmount.Text = "" : txtGSTRate.Text = "" : txtGSTID.Text = ""
            hfGSTAmount.Value = ""

            'If ddlModeOfShipping.SelectedIndex > 0 Then
            'Else
            '    For i = 0 To lstBoxDescription.Items.Count - 1
            '        lstBoxDescription.Items(i).Selected = False
            '    Next
            '    lblError.Text = "Select Mode Of shipping"
            '    lblCustomerValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '    Exit Sub
            'End If

            If UCase(ddlPaymentType.SelectedItem.Text) = UCase("Cheque") Then
                If txtChequeNo.Text <> "" And txtChequeDate.Text <> "" Then
                Else
                    For i = 0 To lstBoxDescription.Items.Count - 1
                        lstBoxDescription.Items(i).Selected = False
                    Next
                    Exit Sub
                End If
            End If

            If txtOrderDate.Text <> "" Then
                If ddlParty.SelectedIndex > 0 Then
                    If lstBoxDescription.SelectedIndex <> -1 Then

                        LoadDiscount()
                        'LoadVAT()
                        'LoadExcise()
                        ddlCommodity.SelectedValue = objOral.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                        sStockHistoryID = objOral.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                        'Stock qty restriction for 0 & -ve'
                        hfAvailableQty.Value = objOral.GetAvailableStockOfThisItem(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                        'Stock qty restriction for 0 & -ve'


                        If ddlCategory.SelectedIndex > 0 Then
                            iCategoryID = ddlCategory.SelectedValue
                        Else
                            iCategoryID = 0
                        End If

                        dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                        sCode = objOral.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                        If sCode.StartsWith("P") Then
                            txtMRP.Enabled = True
                            txtCode.Value = "P"

                            dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                            'If dt.Rows.Count = 0 Then
                            '    Exit Sub
                            'End If
                            If dt.Rows.Count > 1 Then
                                ddlRate.Enabled = True
                                ddlRate.DataSource = dt
                                ddlRate.DataValueField = "INVH_ID"
                                ddlRate.DataTextField = "INVH_Retail"
                                ddlRate.DataBind()
                                hfMRP.Value = ddlRate.SelectedItem.Text
                                txtMRP.Text = ddlRate.SelectedItem.Text
                                txtMRPFromTable.Text = ddlRate.SelectedItem.Text

                                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                                'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                'If ddlVAT.SelectedItem.Text = 0 Then
                                '    ddlVAT.SelectedIndex = 0
                                'End If
                                'LoadVAT(ddlRate.SelectedValue)

                                'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                'If ddlExcise.SelectedItem.Text = 0 Then
                                '    txtExciseAmount.Text = 0
                                '    hfExciseAmount.Value = 0
                                'End If
                                'LoadExcise(ddlRate.SelectedValue)

                                'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                            Else
                                ddlRate.Items.Clear()
                                ddlRate.Enabled = False
                                hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                                'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
                                IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "P", dOrderDate)
                                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                                'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                                'If ddlVAT.SelectedItem.Text = 0 Then
                                '    ddlVAT.SelectedIndex = 0
                                'End If

                                'LoadVAT(IHistoryID)

                                'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                                'If ddlExcise.SelectedItem.Text = 0 Then
                                '    txtExciseAmount.Text = 0
                                '    hfExciseAmount.Value = 0
                                'End If
                                'LoadExcise(IHistoryID)

                                'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                                lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                                If txtMRP.Text = 0 Then
                                    txtMRP.Text = ""
                                    lblEffectiveDates.Text = ""
                                End If

                            End If

                        Else
                            txtMRP.Enabled = False
                            txtCode.Value = "C"
                            dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                            'If dt.Rows.Count = 0 Then
                            '    Exit Sub
                            'End If
                            If dt.Rows.Count > 1 Then
                                ddlRate.Enabled = True
                                ddlRate.DataSource = dt
                                ddlRate.DataValueField = "INVH_ID"
                                ddlRate.DataTextField = "INVH_MRP"
                                ddlRate.DataBind()
                                hfMRP.Value = ddlRate.SelectedItem.Text
                                txtMRP.Text = ddlRate.SelectedItem.Text
                                txtMRPFromTable.Text = ddlRate.SelectedItem.Text

                                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                                'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                'If ddlVAT.SelectedItem.Text = 0 Then
                                '    ddlVAT.SelectedIndex = 0
                                'End If

                                'LoadVAT(ddlRate.SelectedValue)

                                'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                'If ddlExcise.SelectedItem.Text = 0 Then
                                '    txtExciseAmount.Text = 0
                                '    hfExciseAmount.Value = 0
                                'End If

                                'LoadExcise(ddlRate.SelectedValue)

                                'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                            Else
                                ddlRate.Items.Clear()
                                ddlRate.Enabled = False

                                hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                                'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
                                IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "C", dOrderDate)
                                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                                'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                                'If ddlVAT.SelectedItem.Text = 0 Then
                                '    ddlVAT.SelectedIndex = 0
                                'End If

                                'LoadVAT(IHistoryID)

                                'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                                'If ddlExcise.SelectedItem.Text = 0 Then
                                '    txtExciseAmount.Text = 0
                                '    hfExciseAmount.Value = 0
                                'End If
                                'LoadExcise(IHistoryID)

                                'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                                lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                                If txtMRP.Text = 0 Then
                                    txtMRP.Text = ""
                                    lblEffectiveDates.Text = ""
                                End If

                            End If
                        End If

                        'GST'
                        iGSTRate = objOral.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                        If iGSTRate > 0 Then
                        Else
                            lblError.Text = "Enter the HSN Details in Inventory Master."
                            Exit Sub
                        End If

                        txtGSTID.Text = 0
                        txtGSTRate.Text = 0

                        Dim sGSTRate As String = ""
                        sGSTRate = objOral.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                        If sGSTRate <> "HSN" Then
                            txtGSTID.Text = 0
                            'txtGSTRate.Text = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                            txtGSTRate.Text = 0
                        Else
                            txtGSTID.Text = objOral.GetGSTID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                            txtGSTRate.Text = objOral.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                        End If
                        'GST'

                        'Category Code'
                        If UCase(ddlCategory.SelectedItem.Text) = "NA" Then
                            txtMRP.Enabled = True
                        ElseIf UCase(ddlCategory.SelectedItem.Text) = "NOT FOR SALE" Then
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                            txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))

                            hfMRP.Value = 0 : hfAmount.Value = 0 : hfDiscountAmount.Value = 0
                            hfNetAmount.Value = 0
                        Else
                            txtMRP.Enabled = False
                        End If
                        'Category Code'

                        'Sale Type Extra code'
                        If ddlRate.SelectedValue <> "" Then
                            If ddlRate.SelectedValue > 0 Then
                                IHistoryID = ddlRate.SelectedValue
                            Else
                                IHistoryID = IHistoryID
                            End If
                        Else
                            IHistoryID = IHistoryID
                        End If
                        'Sale Type Extra code'

                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstBoxDescription_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        Dim sCode As String = ""
        Dim dt As New DataTable
        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer
        Dim IHistoryID As Integer
        Dim dOrderDate As Date
        Try

            If ddlCategory.SelectedIndex > 0 Then
                iCategoryID = ddlCategory.SelectedValue
            Else
                iCategoryID = 0
            End If

            If txtOrderDate.Text <> "" Then
                If lstBoxDescription.SelectedIndex <> -1 Then
                    sStockHistoryID = objOral.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    sCode = objOral.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                    If sCode.StartsWith("P") Then
                        txtMRP.Enabled = True
                        txtCode.Value = "P"

                        dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                        'If dt.Rows.Count = 0 Then
                        '    Exit Sub
                        'End If
                        If dt.Rows.Count > 1 Then
                            ddlRate.Enabled = True
                            ddlRate.DataSource = dt
                            ddlRate.DataValueField = "INVH_ID"
                            ddlRate.DataTextField = "INVH_Retail"
                            ddlRate.DataBind()
                            hfMRP.Value = ddlRate.SelectedItem.Text
                            txtMRP.Text = ddlRate.SelectedItem.Text
                            txtMRPFromTable.Text = ddlRate.SelectedItem.Text

                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                        Else
                            ddlRate.Items.Clear()
                            ddlRate.Enabled = False
                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                            'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
                            IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "P", dOrderDate)
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                            If txtMRP.Text = 0 Then
                                txtMRP.Text = ""
                                lblEffectiveDates.Text = ""
                            End If
                        End If

                    Else
                        txtMRP.Enabled = False
                        txtCode.Value = "C"
                        dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                        'If dt.Rows.Count = 0 Then
                        '    Exit Sub
                        'End If
                        If dt.Rows.Count > 1 Then
                            ddlRate.Enabled = True
                            ddlRate.DataSource = dt
                            ddlRate.DataValueField = "INVH_ID"
                            ddlRate.DataTextField = "INVH_MRP"
                            ddlRate.DataBind()
                            hfMRP.Value = ddlRate.SelectedItem.Text
                            txtMRP.Text = ddlRate.SelectedItem.Text
                            txtMRPFromTable.Text = ddlRate.SelectedItem.Text

                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                        Else
                            ddlRate.Items.Clear()
                            ddlRate.Enabled = False

                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                            'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
                            IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "C", dOrderDate)
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                            If txtMRP.Text = 0 Then
                                txtMRP.Text = ""
                                lblEffectiveDates.Text = ""
                            End If
                        End If
                    End If

                End If
            End If

            If ddlCategory.SelectedIndex > 0 Then
                If UCase(ddlCategory.SelectedItem.Text) = "NA" Then
                    txtMRP.Enabled = True
                ElseIf UCase(ddlCategory.SelectedItem.Text) = "NOT FOR SALE" Then
                    txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                    txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                    txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))

                    hfMRP.Value = 0 : hfAmount.Value = 0 : hfDiscountAmount.Value = 0
                    hfNetAmount.Value = 0
                Else
                    txtMRP.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCategory_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        Try
            dt = objOral.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtSearch.Text))
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "SPO_OrderCode"
            ddlSearch.DataValueField = "SPO_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Order No")

            ddlSearch.Visible = True
            txtSearch.Visible = False : btnSearch.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSearch_Click")
        End Try
    End Sub
    Public Sub TradeDiscount()
        Dim iOrderID As Integer
        Dim dGrandDiscount, dGrandDiscountAmt, dGrandTotal, dGrandTotalAmt As Double
        Try
            If ddlSearch.SelectedIndex > 0 Then
                iOrderID = ddlSearch.SelectedValue
            Else
                iOrderID = txtOrderID.Text
            End If
            If iOrderID > 0 Then
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

                objOral.SaveGrandTotalToOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, iOrderID, dGrandDiscount, dGrandDiscountAmt, dGrandTotal, dGrandTotalAmt)
                'lblError.Text = "Saved Successfully."
                'lblCustomerValidationMsg.Text = lblError.Text
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "TradeDiscount")
        End Try
    End Sub
    Private Sub ibtnInsert_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnInsert.Click
        Try
            lblError.Text = ""
            TradeDiscount()
            lblError.Text = "Saved Successfully."
            lblCustomerValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ibtnInsert_Click")
        End Try
    End Sub
    Private Sub dgExistingProFormaSalesOrder_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgExistingProFormaSalesOrder.RowCommand
        Dim iItemID As Integer, iCommodityID As Integer, iOrderNo As Integer
        Dim dt As New DataTable
        Dim sCode As String = "", sStatus As String = ""
        Dim sStr As String = ""
        Dim dtRate As New DataTable
        Dim iHistoryID As Integer
        Dim bCheck As String = ""
        Dim iCategoryID As Integer

        Dim lblCommodityID As New Label
        Dim lblItemID As New Label
        Dim dOrderDate As Date
        Dim dVAT As New Double : Dim dExcise As New Double : Dim lblID As New Label
        Try
            lblError.Text = ""

            'Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            'lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
            'lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)

            'iCommodityID = lblCommodityID.Text
            'iItemID = lblItemID.Text

            If ddlSearch.SelectedIndex > 0 Then
                iOrderNo = ddlSearch.SelectedValue
            Else
                iOrderNo = txtOrderID.Text
            End If
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                txtDetailIDTable.Text = lblID.Text
                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)
                iCommodityID = lblCommodityID.Text
                iItemID = lblItemID.Text

                'btnSave.Text = "Update"
                'imgbtnUpdate.Visible = True
                imgbtnAdd.ImageUrl = "~/Images/Update24.png"

                ddlCommodity.SelectedValue = iCommodityID
                'objOral.GetCommodity(sSession.AccessCode, sSession.AccessCodeID, iOrderNo, iItemID)

                dt = objOral.BindExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iItemID, iOrderNo, lblID.Text)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If ddlCategory.SelectedIndex > 0 Then
                            iCategoryID = ddlCategory.SelectedValue
                        Else
                            iCategoryID = 0
                        End If

                        BindDescription(dt.Rows(i)("SPOD_CommodityID"))
                        lstBoxDescription.SelectedValue = dt.Rows(i)("SPOD_ItemID")

                        txtQuantity.Text = dt.Rows(i)("SPOD_Quantity")

                        txtMRP.Text = dt.Rows(i)("SPOD_MRPRate")
                        hfMRP.Value = dt.Rows(i)("SPOD_MRPRate")

                        txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))
                        hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))

                        txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))
                        hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))

                        LoadDiscount()
                        'LoadVAT()
                        'LoadExcise()
                        'LoadVAT(dt.Rows(i)("SPOD_HistoryID"))
                        'LoadExcise(dt.Rows(i)("SPOD_HistoryID"))

                        If dt.Rows(i)("SPOD_Discount") > 0 Then
                            ddlDiscount.SelectedValue = objOral.GetDiscountID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("SPOD_Discount"))
                        Else
                            ddlDiscount.SelectedIndex = 0
                        End If

                        txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_DiscountRate")))
                        hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_DiscountRate")))

                        txtGSTID.Text = dt.Rows(i)("SPOD_GST_ID")
                        txtGSTRate.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_GSTRate")))

                        txtGSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_GSTAmount")))
                        hfGSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_GSTAmount")))

                        If txtOrderDate.Text <> "" Then

                            dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            sCode = objOral.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                            If sCode.StartsWith("P") Then
                                txtCode.Value = "P"
                                sStr = "P"
                                dtRate = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(i)("SPOD_HistoryID"), sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                                If dtRate.Rows.Count > 1 Then
                                    ddlRate.Enabled = True
                                    ddlRate.DataSource = dtRate
                                    ddlRate.DataValueField = "INVH_ID"
                                    ddlRate.DataTextField = "INVH_Retail"
                                    ddlRate.DataBind()
                                    ddlRate.SelectedValue = dt.Rows(i)("SPOD_HistoryID")

                                    'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                                Else
                                    'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                                End If
                            Else
                                txtCode.Value = "C"
                                sStr = "C"
                                dtRate = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(i)("SPOD_HistoryID"), sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                                If dtRate.Rows.Count > 1 Then
                                    ddlRate.Enabled = True
                                    ddlRate.DataSource = dtRate
                                    ddlRate.DataValueField = "INVH_ID"
                                    ddlRate.DataTextField = "INVH_MRP"
                                    ddlRate.DataBind()
                                    ddlRate.SelectedValue = dt.Rows(i)("SPOD_HistoryID")

                                    'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                                Else
                                    'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                                End If
                            End If
                        End If
                        BindUnitOfMeassurement(lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                        ddlUnitOfMeassurement.SelectedValue = dt.Rows(i)("SPOD_UnitOfMeasurement")
                        txtMRPFromTable.Text = txtMRP.Text

                        lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("SPOD_HistoryID"))
                    Next
                End If
            ElseIf e.CommandName = "Cancel" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                txtDetailIDTable.Text = lblID.Text
                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)

                iCommodityID = lblCommodityID.Text
                iItemID = lblItemID.Text

                If ddlSearch.SelectedIndex > 0 Then
                    bCheck = objOral.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                    If bCheck = True Then
                        lblError.Text = "For Selected Order No Invoice has been done, items can not be canceled."
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                        Exit Sub
                    Else
                        sStatus = objOral.CheckOrderForAllocationApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                        If sStatus <> "" Then
                            If sStatus = "A" Then
                                lblError.Text = "Selected Order No has been Allocated & Approved, items can not be canceled."
                                lblCustomerValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                'btnSave.Enabled = False
                                'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                                Exit Sub
                            End If
                        End If
                    End If
                End If
                objOral.DeleteOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iItemID, iCommodityID, iOrderNo, sSession.IPAddress, lblID.Text)
                lblError.Text = "Canceled Successfully."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

                LoadExistingOrderGrid(iOrderNo)
                'TradeDiscount updation'
                TradeDiscount()
                'TradeDiscount updation'
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_RowCommand")
        End Try
    End Sub
    'Private Sub dgExistingProFormaSalesOrder_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgExistingProFormaSalesOrder.ItemDataBound
    '    Dim ibtnCancel As New ImageButton
    '    Try
    '        If (e.Item.ItemType <> ListItemType.Header) And (e.Item.ItemType <> ListItemType.Footer) Then
    '            ibtnCancel = e.Item.FindControl("ibtnCancel")
    '            ibtnCancel.Attributes.Add("OnClick", "javascript:return Validate()")
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_ItemDataBound")
    '    End Try
    'End Sub
    Private Sub dgExistingProFormaSalesOrder_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgExistingProFormaSalesOrder.RowDataBound
        Dim ibtnCancel As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                ibtnCancel = TryCast(e.Row.FindControl("ibtnCancel"), ImageButton)
                ibtnCancel.Attributes.Add("OnClick", "javascript:return Validate()")
            End If
            dgExistingProFormaSalesOrder.Columns(22).Visible = False
            If sOCSOSave = "YES" Then
                dgExistingProFormaSalesOrder.Columns(22).Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_RowDataBound")
        End Try
    End Sub
    Private Sub btnSearchParty_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearchParty.Click
        Dim dt As New DataTable
        Try
            If txtPartySearch.Text <> "" Then
                'dt = objOral.GetSearchPartyList(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtPartySearch.Text)
                'ddlParty.DataSource = dt
                'ddlParty.DataTextField = "ACM_Name"
                'ddlParty.DataValueField = "ACM_ID"
                'ddlParty.DataBind()
                'ddlParty.Items.Insert(0, "--- Select Party ---")
                dt = objOral.GetSearchPartyList(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtPartySearch.Text)
                ddlParty.DataSource = dt
                ddlParty.DataTextField = "BM_Name"
                ddlParty.DataValueField = "BM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Party")
            Else
                LoadParty()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSearchParty_Click")
        End Try
    End Sub
    Private Sub dgExistingProFormaSalesOrder_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles dgExistingProFormaSalesOrder.RowCancelingEdit

    End Sub
    Private Sub dgExistingProFormaSalesOrder_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgExistingProFormaSalesOrder.RowEditing

    End Sub
    Private Sub imgbtnNew_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNew.Click
        Try
            lblError.Text = "" : txtSearch.Text = "" : lblStatus.Text = ""
            pnlGrand.Visible = False : txtGrandTotal.Text = "" : txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotalAmt.Text = ""
            Clear()
            'ddlSearch.Visible = False
            'txtSearch.Visible = True : btnSearch.Visible = True
            GenerateOrderCodeAnddate()
            'imgbtnAdd.Enabled = True
            'imgbtnAdd.Visible = True : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False : imgbtnReport.Visible = False : imgbtnApprove.Visible = False

            ClearDispatchFields()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNew_Click")
        End Try
    End Sub
    Public Sub ClearDispatchFields()
        Try
            Session("ChargesMaster") = Nothing
            GvCharge.DataSource = Nothing
            GvCharge.DataBind()
            ddlChargeType.SelectedIndex = 0 : txtShippingRate.Text = ""
            txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFSCCode.Text = "" : ddlBankName.SelectedIndex = 0 : txtBranch.Text = ""
            txtDispatchRefNo.Text = "" : txtESugamNo.Text = "" : txtDispatchDate.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearDispatchFields")
        End Try
    End Sub
    'Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
    '    Dim Arr() As String
    '    Dim dt As New DataTable
    '    Dim iMasterID As Integer
    '    Dim dOrderDate As Date
    '    Dim sCode As String = "" : Dim sStr As String = ""

    '    Try
    '        lblError.Text = ""

    '        'Save Master'
    '        objOral.SPO_OrderCode = objGen.SafeSQL(Trim(txtOrderCode.Text))
    '        objOral.SPO_OrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '        objOral.SPO_PartyCode = objGen.SafeSQL(Trim(txtPartyNo.Text))
    '        objOral.SPO_PartyName = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
    '        objOral.SPO_Address = objGen.SafeSQL(Trim(txtAddress.Text))
    '        objOral.SPO_ContantNo = objGen.SafeSQL(Trim(txtContactNo.Text))

    '        If ddlModeOfShipping.SelectedIndex > 0 Then
    '            objOral.SPO_ModeOfDispatch = objGen.SafeSQL(Trim(ddlModeOfShipping.SelectedValue))
    '        Else
    '            objOral.SPO_ModeOfDispatch = 0
    '        End If
    '        If txtShippingDate.Text <> "" Then
    '            objOral.SPO_ShippingDate = Date.ParseExact(objGen.SafeSQL(Trim(txtShippingDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        End If
    '        objOral.SPO_PaymentType = ddlPaymentType.SelectedValue

    '        If ddlModeOfCommunication.SelectedIndex > 0 Then
    '            objOral.SPO_ModeOfCommunication = objGen.SafeSQL(Trim(ddlModeOfCommunication.SelectedValue))
    '        Else
    '            objOral.SPO_ModeOfCommunication = 0
    '        End If
    '        If txtInputBy.Text <> "" Then
    '            objOral.SPO_InputBy = objGen.SafeSQL(Trim(txtInputBy.Text))
    '        Else
    '            objOral.SPO_InputBy = ""
    '        End If

    '        If ddlShippingCharges.SelectedIndex > 0 Then
    '            objOral.SPO_ShippingCharge = objGen.SafeSQL(Trim(ddlShippingCharges.SelectedValue))
    '        Else
    '            objOral.SPO_ShippingCharge = 0
    '        End If

    '        objOral.SPO_CreatedBy = sSession.UserID
    '        objOral.SPO_CreatedOn = DateTime.Today
    '        objOral.SPO_Status = "A"
    '        objOral.SPO_Operation = "C"
    '        objOral.SPO_IPAddress = sSession.IPAddress

    '        objOral.SPO_OrderType = "O"
    '        objOral.SPO_DispatchFlag = 0

    '        If ddlSalesMan.SelectedIndex > 0 Then
    '            objOral.SPO_SalesManID = objGen.SafeSQL(Trim(ddlSalesMan.SelectedValue))
    '        Else
    '            objOral.SPO_SalesManID = 0
    '        End If

    '        If txtBuyerPurOrderNo.Text <> "" Then
    '            objOral.SPO_BuyerOrderNo = objGen.SafeSQL(Trim(txtBuyerPurOrderNo.Text))
    '        Else
    '            objOral.SPO_BuyerOrderNo = "Oral"
    '        End If

    '        If ddlCategory.SelectedIndex > 0 Then
    '            objOral.SPO_Category = objGen.SafeSQL(Trim(ddlCategory.SelectedValue))
    '        Else
    '            objOral.SPO_Category = 0
    '        End If

    '        If txtRemarks.Text <> "" Then
    '            objOral.SPO_Remarks = objGen.SafeSQL(Trim(txtRemarks.Text))
    '        Else
    '            objOral.SPO_Remarks = ""
    '        End If

    '        If txtBuyerOrderDate.Text <> "" Then
    '            objOral.SPO_BuyerOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtBuyerOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        End If
    '        objOral.SPO_SalesType = 1
    '        objOral.SPO_OtherType = 0

    '        If txtChequeNo.Text <> "" Then
    '            objOral.SPO_ChequeNo = txtChequeNo.Text
    '        Else
    '            objOral.SPO_ChequeNo = ""
    '        End If
    '        If txtChequeDate.Text <> "" Then
    '            objOral.SPO_ChequeDate = Date.ParseExact(Trim(txtChequeDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        End If
    '        If txtIFSCCode.Text <> "" Then
    '            objOral.SPO_IFSCCode = txtIFSCCode.Text
    '        Else
    '            objOral.SPO_IFSCCode = ""
    '        End If
    '        If ddlBankName.SelectedIndex > 0 Then
    '            objOral.SPO_BankName = ddlBankName.SelectedValue
    '        Else
    '            objOral.SPO_BankName = 0
    '        End If
    '        If txtBranch.Text <> "" Then
    '            objOral.SPO_Branch = txtBranch.Text
    '        Else
    '            objOral.SPO_Branch = ""
    '        End If
    '        objOral.SPO_GoThroughDispatch = 0

    '        If txtDispatchRefNo.Text <> "" Then
    '            objOral.SPO_DispatchRefNo = txtDispatchRefNo.Text
    '        Else
    '            objOral.SPO_DispatchRefNo = ""
    '        End If

    '        If txtESugamNo.Text <> "" Then
    '            objOral.SPO_ESugamNo = txtESugamNo.Text
    '        Else
    '            objOral.SPO_ESugamNo = ""
    '        End If

    '        If txtDispatchDate.Text <> "" Then
    '            objOral.SPO_DispatchDate = Date.ParseExact(Trim(txtDispatchDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        End If

    '        If txtDispatchDate.Text <> "" Then
    '            Dim dDate, dSDate As Date
    '            'Cheque Date Comparision'
    '            dDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            Dim m As Integer
    '            m = DateDiff(DateInterval.Day, dDate, dSDate)
    '            If m < 0 Then
    '                lblError.Text = "Dispatch Date (" & txtDispatchDate.Text & ") should be Greater than or equal to Order Date(" & txtOrderDate.Text & ")."
    '                lblCustomerValidationMsg.Text = lblError.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '                txtDispatchDate.Focus()
    '                Exit Sub
    '            End If
    '            'Cheque Date Comparision'
    '        End If

    '        Arr = objOral.SavePROFormaMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objOral)
    '        dt = Session("UpdateTab")
    '        iMasterID = Arr(1)

    '        objOral.SPOD_SOID = iMasterID

    '        If txtQuantity.Text <> "" Then

    '            objOral.SPOD_CommodityID = objGen.SafeSQL(Trim(ddlCommodity.SelectedValue))
    '            objOral.SPOD_ItemID = objGen.SafeSQL(Trim(lstBoxDescription.SelectedValue))
    '            objOral.SPOD_Quantity = objGen.SafeSQL(Trim(txtQuantity.Text))

    '            If ddlDiscount.SelectedIndex > 0 Then
    '                objOral.SPOD_Discount = objGen.SafeSQL(Trim(ddlDiscount.SelectedItem.Text))
    '            Else
    '                objOral.SPOD_Discount = 0
    '            End If

    '            objOral.SPOD_UnitofMeasurement = objGen.SafeSQL(Trim(ddlUnitOfMeassurement.SelectedValue))

    '            If hfAmount.Value <> "" Then
    '                objOral.SPOD_RateAmount = Request.Form(hfAmount.UniqueID)
    '            Else
    '                objOral.SPOD_RateAmount = txtAmount.Text
    '            End If

    '            If hfDiscountAmount.Value <> "" Then
    '                objOral.SPOD_DiscountRate = Request.Form(hfDiscountAmount.UniqueID)
    '            Else
    '                objOral.SPOD_DiscountRate = 0
    '            End If

    '            If txtOrderDate.Text <> "" Then
    '                dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            End If

    '            sCode = objOral.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
    '            If sCode.StartsWith("P") Then
    '                sStr = "P"
    '            Else
    '                sStr = "C"
    '            End If
    '            If ddlRate.SelectedIndex <> -1 Then
    '                objOral.SPOD_HistoryID = ddlRate.SelectedValue
    '            Else
    '                objOral.SPOD_HistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, objOral.SPO_Category, sStr, dOrderDate)
    '            End If

    '            objOral.SPOD_CompiD = sSession.AccessCodeID
    '            objOral.SPOD_Status = "A"

    '            If txtMRP.Text <> "" Then
    '                hfMRP.Value = txtMRP.Text
    '            End If
    '            If hfMRP.Value <> "" Then
    '                objOral.SPOD_MRPRate = hfMRP.Value
    '            Else
    '                objOral.SPOD_MRPRate = 0
    '            End If

    '            If hfNetAmount.Value <> "" Then
    '                objOral.SPOD_TotalAmount = Request.Form(hfNetAmount.UniqueID)
    '            Else
    '                objOral.SPOD_TotalAmount = 0
    '            End If

    '            If ddlVAT.SelectedIndex <> -1 Then
    '                If ddlVAT.SelectedIndex > 0 Then
    '                    objOral.SPOD_VAT = ddlVAT.SelectedValue
    '                Else
    '                    objOral.SPOD_VAT = objOral.GetZeroVAT(sSession.AccessCode, sSession.AccessCodeID)
    '                End If
    '            Else
    '                objOral.SPOD_VAT = 0
    '            End If

    '            If hfVATAmount.Value <> "" Then
    '                objOral.SPOD_VATAmount = Request.Form(hfVATAmount.UniqueID)
    '            Else
    '                objOral.SPOD_VATAmount = 0
    '            End If

    '            If ddlExcise.SelectedIndex <> -1 Then
    '                If ddlExcise.SelectedIndex > 0 Then
    '                    objOral.SPOD_Excise = ddlExcise.SelectedValue
    '                Else
    '                    objOral.SPOD_Excise = objOral.GetZeroExcise(sSession.AccessCode, sSession.AccessCodeID)
    '                End If
    '            Else
    '                objOral.SPOD_Excise = 0
    '            End If

    '            If hfExciseAmount.Value <> "" Then
    '                objOral.SPOD_ExciseAmount = Request.Form(hfExciseAmount.UniqueID)
    '            Else
    '                objOral.SPOD_ExciseAmount = 0
    '            End If

    '            objOral.SPOD_Operation = "C"
    '            objOral.SPOD_IPAddress = sSession.IPAddress

    '            If ddlCategory.SelectedIndex > 0 Then
    '                objOral.SPOD_Category = ddlCategory.SelectedValue
    '            Else
    '                objOral.SPOD_Category = 0
    '            End If

    '            Arr = objOral.SavePROFormaMasterDetails(sSession.AccessCode, objOral, sSession.YearID)

    '            If Arr(0) = "2" Then
    '                lblCustomerValidationMsg.Text = "Successfully Updated"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            ElseIf Arr(0) = "3" Then
    '                lblCustomerValidationMsg.Text = "Successfully Saved"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            End If
    '        End If

    '        LoadExistingOrderGrid(iMasterID)
    '        pnlGrand.Visible = True

    '        'If btnSave.Text = "Update" Then
    '        '    objOral.UpdateOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, lstBoxDescription.SelectedValue, ddlCommodity.SelectedValue, iMasterID)
    '        '    btnSave.Text = "Add"
    '        'End If
    '        objOral.UpdateOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, lstBoxDescription.SelectedValue, ddlCommodity.SelectedValue, iMasterID)
    '        imgbtnAdd.Visible = False : imgbtnUpdate.Visible = True : imgbtnApprove.Visible = True

    '        txtOrderID.Text = iMasterID

    '        'TradeDiscount updation'
    '        TradeDiscount()
    '        'TradeDiscount updation'

    '        ClearDetails()
    '        divcollapseCharges.Visible = True
    '        SaveCharges(iMasterID)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
    '    End Try
    'End Sub
    Private Sub imgbtnDelete_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDelete.Click
        Dim iOrderID As Integer
        Dim bCheck As Boolean
        Try
            If ddlSearch.SelectedIndex > 0 Then
                iOrderID = ddlSearch.SelectedValue
            Else
                If txtOrderID.Text <> "" Then
                    iOrderID = txtOrderID.Text
                Else
                    iOrderID = 0
                End If
            End If
            If iOrderID > 0 Then
                bCheck = objOral.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
                If bCheck = True Then
                    lblError.Text = "For Selected Order No Invoice has been done, it can not be delete."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                Else
                    objOral.DeleteWholeOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, iOrderID)
                    objOral.DeleteAllocationOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, iOrderID)

                    lblError.Text = "" : txtSearch.Text = ""
                    pnlGrand.Visible = False : txtGrandTotal.Text = "" : txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotalAmt.Text = ""
                    Clear()
                    'ddlSearch.Visible = False
                    'txtSearch.Visible = True : btnSearch.Visible = True
                    GenerateOrderCodeAnddate()

                    lblCustomerValidationMsg.Text = "Deleted Successfully."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    LoadExistingOrderNo()
                    ddlSearch.SelectedIndex = 0
                End If
            Else
                lblError.Text = "Select Existing Cash Sales Order/Create new Cash Sales Order to Delete."
                lblCustomerValidationMsg.Text = "Select Existing Cash Sales Order/Create new Cash Sales Order to Delete."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDelete_Click")
        End Try
    End Sub
    Private Sub dgExistingProFormaSalesOrder_PreRender(sender As Object, e As EventArgs) Handles dgExistingProFormaSalesOrder.PreRender
        Dim dt As New DataTable
        Try
            If dgExistingProFormaSalesOrder.Rows.Count > 0 Then
                dgExistingProFormaSalesOrder.UseAccessibleHeader = True
                dgExistingProFormaSalesOrder.HeaderRow.TableSection = TableRowSection.TableHeader
                dgExistingProFormaSalesOrder.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExistingProFormaSalesOrder_PreRender")
        End Try
    End Sub
    Private Sub imgbtnReport_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnReport.Click
        Dim dt As New DataTable
        Dim iSPOID As Integer

        Dim iReportType As Integer
        Try
            If (txtOrderID.Text <> "" Or ddlSearch.SelectedIndex > 0) Then
                lblError.Text = ""
                If ddlSearch.SelectedIndex > 0 Then
                    iSPOID = ddlSearch.SelectedValue
                Else
                    iSPOID = txtOrderID.Text
                End If

                iReportType = objOral.GetReportTypeFromPrintSettings(sSession.AccessCode, sSession.AccessCodeID)
                If iReportType > 0 Then
                    Response.Redirect("~/Reports/Viewer/OralSalesHR.aspx?ExistingOrder=" & iSPOID & "")
                Else
                    Response.Redirect("~/Reports/Viewer/OralSales.aspx?ExistingOrder=" & iSPOID)
                End If
            Else
                lblError.Text = "Save the new Order/Select the Existing Order."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnReport_Click")
        End Try
    End Sub
    Private Sub imgbtnCreateCustomer_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCreateCustomer.Click
        Try
            Response.Redirect(String.Format("~/Masters/CustomerMasterDetails.aspx?Status=SO"), False) 'Sales party Master
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnCreateCustomer_Click")
        End Try
    End Sub
    'Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
    '    Try
    '        If (txtOrderID.Text <> "" Or ddlSearch.SelectedIndex > 0) Then
    '            If GvCharge.Items.Count = 0 Then
    '                lblError.Text = "Enter Charges"
    '                Exit Sub
    '            End If
    '            SaveDispatchMasterAndDetails()
    '        Else
    '            lblError.Text = "Select Existing Cash Sales Order/Create new Cash Sales Order to Approve."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
    '    End Try
    'End Sub
    Public Sub SaveDispatchMasterAndDetails()
        Dim iOrderID As Integer
        Dim objDispatch As New ClsDispatchDetails
        Dim Arr() As String : Dim iMasterID As Integer

        Dim lnkGoods As New LinkButton
        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblUnit, lblMRP, lblOrderedQty, lblAmount As New Label
        Dim lblDiscount, lblDiscountAmount, lblVAT, lblVATAmount, lblExcise, lblExciseAmount, lblCST, lblCSTAmount, lblNetAmount As New Label
        Dim sGSTNCategory As String = ""
        Try
            objDispatch.SDM_Code = objDispatch.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            If ddlSearch.SelectedIndex > 0 Then
                iOrderID = ddlSearch.SelectedValue
            Else
                iOrderID = txtOrderID.Text
            End If

            Dim sString As String = ""
            'Check Order has been already dispatched or not'
            sString = objOral.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            If sString = "True" Then
                lblError.Text = "This order has been dispatched already."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            'Check Order has been already dispatched or not'

            If txtDispatchDate.Text <> "" Then
                Dim dDate, dSDate As Date
                'Check Date Comparision'
                dDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim m As Integer
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Invoice Date (" & txtDispatchDate.Text & ") should be Greater than or equal to Order Date(" & txtOrderDate.Text & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtDispatchDate.Focus()
                    Exit Sub
                End If
                'Check Date Comparision'
            End If

            'To Check Item Vat 0'
            'Dim dInvoiceDate As Date
            'Dim dVAT As Double : Dim dExcise As Double
            'dInvoiceDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'If dgExistingProFormaSalesOrder.Rows.Count > 0 Then
            '    For i = 0 To dgExistingProFormaSalesOrder.Rows.Count - 1
            '        lblCommodityID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCommodityID")
            '        lblItemID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblItemID")
            '        lblHistoryID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblHistoryID")
            '        lnkGoods = dgExistingProFormaSalesOrder.Rows(i).FindControl("lnkGoods")

            '        dVAT = objDispatch.GetItemVAT(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text, lblHistoryID.Text, dInvoiceDate)
            '        If dVAT > 0 Then
            '        Else
            '            lblError.Text = "There is no TAX Details for the item " & lnkGoods.Text & " >= to this Invoice Date" & objGen.FormatDtForRDBMS(txtDispatchDate.Text, "D") & " "
            '            Exit Sub
            '        End If
            '        dExcise = objDispatch.GetItemExcise(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text, lblHistoryID.Text, dInvoiceDate)
            '    Next
            'End If
            'To Check Item Vat 0'

            objDispatch.SDM_OrderID = objGen.SafeSQL(Trim(iOrderID))
            If txtOrderDate.Text <> "" Then
                objDispatch.SDM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            objDispatch.SDM_SupplierID = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
            objDispatch.SDM_ModeOfShipping = ddlModeOfShipping.SelectedValue
            If txtDispatchDate.Text <> "" Then
                objDispatch.SDM_DispatchDate = Date.ParseExact(Trim(txtDispatchDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            objDispatch.SDM_PaymentType = ddlPaymentType.SelectedValue
            objDispatch.SDM_ShippingRate = 0
            objDispatch.SDM_ExpectedDays = 0

            objDispatch.SDM_Status = "W"
            objDispatch.SDM_CompID = sSession.AccessCodeID
            objDispatch.SDM_YearID = sSession.YearID
            objDispatch.SDM_CreatedBy = sSession.UserID
            objDispatch.SDM_CreatedOn = System.DateTime.Now

            objDispatch.SDM_Operation = "C"
            objDispatch.SDM_IPAddress = sSession.IPAddress

            If txtChequeNo.Text <> "" Then
                objDispatch.SDM_ChequeNo = txtChequeNo.Text
            Else
                objDispatch.SDM_ChequeNo = ""
            End If
            If txtChequeDate.Text <> "" Then
                objDispatch.SDM_ChequeDate = Date.ParseExact(Trim(txtChequeDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtIFSCCode.Text <> "" Then
                objDispatch.SDM_IFSCCode = txtIFSCCode.Text
            Else
                objDispatch.SDM_IFSCCode = ""
            End If
            If ddlBankName.SelectedIndex > 0 Then
                objDispatch.SDM_BankName = ddlBankName.SelectedValue
            Else
                objDispatch.SDM_BankName = 0
            End If
            If txtBranch.Text <> "" Then
                objDispatch.SDM_Branch = txtBranch.Text
            Else
                objDispatch.SDM_Branch = ""
            End If

            'Dim sDate As Date
            'objDispatch.SDM_ChequeNo = ""
            'sDate = "01/01/1900"
            'objDispatch.SDM_ChequeDate = objGen.FormatDtForRDBMS(sDate, "D")
            'objDispatch.SDM_IFSCCode = ""
            'objDispatch.SDM_BankName = ""
            'objDispatch.SDM_Branch = ""

            If txtGrandDiscount.Text <> "" Then
                objDispatch.SDM_GrandDiscount = txtGrandDiscount.Text
            End If
            If txtGrandDiscountAmt.Text <> "" Then
                objDispatch.SDM_GrandDiscountAmt = txtGrandDiscountAmt.Text
            End If
            If txtGrandTotal.Text <> "" Then
                objDispatch.SDM_GrandTotal = txtGrandTotal.Text
            End If
            If txtGrandTotalAmt.Text <> "" Then
                objDispatch.SDM_GrandTotalAmt = txtGrandTotalAmt.Text
            End If

            If ddlSalesMan.SelectedIndex > 0 Then
                objDispatch.SDM_SalesManID = ddlSalesMan.SelectedValue
            Else
                objDispatch.SDM_SalesManID = 0
            End If

            If txtDispatchRefNo.Text <> "" Then
                objDispatch.SDM_DispatchRefNo = txtDispatchRefNo.Text
            Else
                objDispatch.SDM_DispatchRefNo = ""
            End If
            If txtESugamNo.Text <> "" Then
                objDispatch.SDM_ESugamNo = txtESugamNo.Text
            Else
                objDispatch.SDM_ESugamNo = ""
            End If

            objDispatch.SDM_Remarks = txtRemarks.Text

            objDispatch.SDM_SaleType = 0
            objDispatch.SDM_OtherType = 0
            objDispatch.SDM_AllocateID = 0

            objDispatch.SDM_TrType = 1
            objDispatch.SDM_CompanyAddress = ""
            objDispatch.SDM_CompanyGSTNRegNo = ""
            objDispatch.SDM_BillingAddress = ""
            objDispatch.SDM_BillingGSTNRegNo = ""
            objDispatch.SDM_DeliveryFrom = ""
            objDispatch.SDM_DeliveryFromGSTNRegNo = ""
            objDispatch.SDM_DeliveryAddress = ""
            objDispatch.SDM_DeliveryGSTNRegNo = ""
            objDispatch.SDM_DispatchStatus = "Local"

            objDispatch.SDM_DispatchID = 0
            objDispatch.SDM_GSTNCategory = objDispatch.GetGSTCategory(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
            sGSTNCategory = objDispatch.GSTNDesc(sSession.AccessCode, sSession.AccessCodeID, objDispatch.SDM_GSTNCategory)

            Arr = objDispatch.SaveDispatchMaster(sSession.AccessCode, objDispatch)
            iMasterID = Arr(1)

            SaveCharges(iOrderID, iMasterID)
            CalculateLocalBill(iMasterID, iOrderID, sGSTNCategory)

            'If dgExistingProFormaSalesOrder.Rows.Count > 0 Then
            '    For i = 0 To dgExistingProFormaSalesOrder.Rows.Count - 1
            '        lblCommodityID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCommodityID")
            '        lblItemID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblItemID")
            '        lblHistoryID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblHistoryID")
            '        lblUnitID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblUnitID")
            '        lnkGoods = dgExistingProFormaSalesOrder.Rows(i).FindControl("lnkGoods")
            '        lblUnit = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblUnit")
            '        lblOrderedQty = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblOrderedQty")
            '        lblMRP = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblMRP")
            '        lblAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblAmount")
            '        lblDiscount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblDiscount")
            '        lblDiscountAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblDiscountAmount")
            '        lblVAT = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblVAT")
            '        lblVATAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblVATAmount")
            '        lblCST = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCST")
            '        lblCSTAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCSTAmount")
            '        lblExcise = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblExcise")
            '        lblExciseAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblExciseAmount")
            '        lblNetAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblNetAmount")


            '        objDispatch.SDD_MasterID = iMasterID
            '        objDispatch.SDD_CommodityID = lblCommodityID.Text
            '        objDispatch.SDD_DescID = lblItemID.Text
            '        objDispatch.SDD_HistoryID = lblHistoryID.Text
            '        objDispatch.SDD_UnitID = lblUnitID.Text

            '        objDispatch.SDD_Rate = lblMRP.Text
            '        objDispatch.SDD_RateAmount = lblAmount.Text
            '        objDispatch.SDD_Quantity = lblOrderedQty.Text

            '        objDispatch.SDD_Discount = lblDiscount.Text
            '        objDispatch.SDD_DiscountAmount = lblDiscountAmount.Text

            '        objDispatch.SDD_VAT = lblVAT.Text
            '        objDispatch.SDD_VATAmount = lblVATAmount.Text

            '        objDispatch.SDD_CST = lblCST.Text
            '        objDispatch.SDD_CSTAmount = lblCSTAmount.Text

            '        objDispatch.SDD_Excise = lblExcise.Text
            '        objDispatch.SDD_ExciseAmount = lblExciseAmount.Text

            '        objDispatch.SDD_TotalAmount = lblNetAmount.Text

            '        objDispatch.SDD_Status = "W"
            '        objDispatch.SDD_CompID = sSession.AccessCodeID
            '        objDispatch.SDD_Operation = "C"
            '        objDispatch.SDD_IPAddress = sSession.IPAddress
            '        objDispatch.SDD_CreatedBy = sSession.UserID
            '        objDispatch.SDD_CreatedOn = System.DateTime.Now

            '        Arr = objDispatch.SaveDispatchDetails(sSession.AccessCode, objDispatch)
            '        objDispatch.UpdateStockLedgerClosingBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, objDispatch.SDD_CommodityID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, objDispatch.SDD_Quantity, sSession.IPAddress, iOrderID, iMasterID, 1)
            '    Next
            'End If


            'Update DispatchID'
            'objOral.UpdateCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, iMasterID)
            'Update DispatchID'


            lblError.Text = "Successfully Approved"
            lblCustomerValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveDispatchMasterAndDetails")
        End Try
    End Sub
    Public Sub CalculateLocalBill(ByVal iMasterID As Integer, ByVal iOrderID As Integer, ByVal sGSTNCategory As String)
        Dim Arr() As String, OrderNo As String = ""
        Dim sCode As String = ""

        Dim lnkGoods As New LinkButton
        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblUnit, lblMRP, lblOrderedQty, lblTotalAmount As New Label
        Dim lblDiscount, lblDiscountAmount, lblTax, lblTaxAmount, lblExciseDuty, lblExciseDutyAmount, lblCST, lblCSTAmount, lblTotal As New Label

        Dim iAllocateID As Integer
        Dim objDispatch As New ClsDispatchDetails

        Dim dInvoiceDate As Date
        Dim dVAT As Double : Dim dExcise As Double
        Dim dBasicAmt As Double
        Dim dItemChargeAmt, dAmountOnCalculate As Double
        Dim dChargeAmount, dItemsTotalFromDispatch As Double
        Try

            dChargeAmount = objDispatch.GetOralChargeAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, 0, 0)
            dItemsTotalFromDispatch = objDispatch.GetOralItemsTotal(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)

            sCode = objDispatch.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
            If dgExistingProFormaSalesOrder.Rows.Count > 0 Then
                For i = 0 To dgExistingProFormaSalesOrder.Rows.Count - 1

                    lblCommodityID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCommodityID")
                    lblItemID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblItemID")
                    lblHistoryID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblHistoryID")
                    lblUnitID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblUnitID")
                    'lblCommodity = grdDispatchDetails.Rows(i).FindControl("lblCommodity")
                    lnkGoods = dgExistingProFormaSalesOrder.Rows(i).FindControl("lnkGoods")
                    lblUnit = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblUnit")
                    lblMRP = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblMRP")
                    lblOrderedQty = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblOrderedQty")
                    lblTotalAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblAmount")
                    lblDiscount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblDiscount")
                    lblDiscountAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblDiscountAmount")
                    lblTax = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblVAT")
                    lblTaxAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblVATAmount")
                    lblExciseDuty = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblExcise")
                    lblExciseDutyAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblExciseAmount")
                    lblCST = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCST")
                    lblCSTAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCSTAmount")
                    lblTotal = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblNetAmount")

                    objDispatch.SDD_MasterID = iMasterID
                    objDispatch.SDD_CommodityID = lblCommodityID.Text
                    objDispatch.SDD_DescID = lblItemID.Text
                    objDispatch.SDD_HistoryID = lblHistoryID.Text
                    objDispatch.SDD_UnitID = lblUnitID.Text

                    objDispatch.SDD_Rate = lblMRP.Text
                    objDispatch.SDD_Quantity = lblOrderedQty.Text

                    objDispatch.SDD_CST = 0
                    objDispatch.SDD_CSTAmount = 0
                    objDispatch.SDD_Excise = 0
                    objDispatch.SDD_ExciseAmount = 0
                    objDispatch.SDD_VAT = 0
                    objDispatch.SDD_VATAmount = 0

                    objDispatch.SDD_Discount = lblDiscount.Text

                    If txtDispatchDate.Text <> "" Then
                        dInvoiceDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                        'Item VAT & Excise'
                        'dVAT = objDispatch.GetItemVAT(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text, lblHistoryID.Text, dInvoiceDate)
                        'dExcise = objDispatch.GetItemExcise(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text, lblHistoryID.Text, dInvoiceDate)
                        'Item VAT & Excise'

                        If (UCase(sGSTNCategory) = UCase("Aggregate turnover less than Rs.20 lakhs")) Or (UCase(sGSTNCategory) = UCase("Aggregate turnover more than Rs.20 lakhs and not availing Composition Scheme")) Then
                            objDispatch.SDD_GST_ID = 0
                            objDispatch.SDD_GSTRate = 0
                        Else
                            objDispatch.SDD_GST_ID = objDispatch.GetGSTID(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
                            objDispatch.SDD_GSTRate = objDispatch.GetGSTRate(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
                        End If

                        objDispatch.SDD_RateAmount = objDispatch.SDD_Rate * objDispatch.SDD_Quantity
                        objDispatch.SDD_Discount = 0
                        objDispatch.SDD_DiscountAmount = 0
                        objDispatch.SDD_TotalAmount = objDispatch.SDD_RateAmount

                        If dChargeAmount > 0 Then
                            If sCode = "C" Then
                                dBasicAmt = objDispatch.SDD_RateAmount
                                objDispatch.SDD_ChargesPeritem = String.Format("{0:0.00}", Convert.ToDecimal(((dBasicAmt) * dChargeAmount) / dItemsTotalFromDispatch))
                            End If
                            If sCode = "P" Then
                                objDispatch.SDD_ChargesPeritem = String.Format("{0:0.00}", Convert.ToDecimal(((objDispatch.SDD_RateAmount) * dChargeAmount) / dItemsTotalFromDispatch))
                            End If
                        Else
                            objDispatch.SDD_ChargesPeritem = 0
                        End If

                        If sCode = "C" Then
                            dBasicAmt = objDispatch.SDD_RateAmount
                            objDispatch.SDD_TotalAmount = dBasicAmt

                            If lblDiscount.Text <> "" Then
                                objDispatch.SDD_Discount = lblDiscount.Text
                                objDispatch.SDD_DiscountAmount = ((dBasicAmt + objDispatch.SDD_ChargesPeritem) * objDispatch.SDD_Discount) / 100
                            End If
                        ElseIf sCode = "P" Then

                            If lblDiscount.Text <> "" Then
                                objDispatch.SDD_Discount = lblDiscount.Text
                                objDispatch.SDD_DiscountAmount = ((objDispatch.SDD_RateAmount + objDispatch.SDD_ChargesPeritem) * objDispatch.SDD_Discount) / 100
                            End If
                        End If

                        dItemChargeAmt = objDispatch.SDD_ChargesPeritem
                        dAmountOnCalculate = String.Format("{0:0.00}", Convert.ToDecimal((objDispatch.SDD_RateAmount - objDispatch.SDD_DiscountAmount) + dItemChargeAmt))
                        objDispatch.SDD_Amount = dAmountOnCalculate

                        If sCode = "C" Then
                            objDispatch.SDD_GSTAmount = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * objDispatch.SDD_GSTRate) / 100))
                            objDispatch.SDD_TotalAmount = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + objDispatch.SDD_GSTAmount))
                        End If
                        If sCode = "P" Then
                            objDispatch.SDD_GSTAmount = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * objDispatch.SDD_GSTRate) / 100))
                            objDispatch.SDD_TotalAmount = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + objDispatch.SDD_GSTAmount))
                        End If

                    End If

                    objDispatch.SDD_Status = "W"
                    objDispatch.SDD_CompID = sSession.AccessCodeID
                    objDispatch.SDD_Operation = "C"
                    objDispatch.SDD_IPAddress = sSession.IPAddress
                    objDispatch.SDD_CreatedBy = sSession.UserID
                    objDispatch.SDD_CreatedOn = System.DateTime.Now

                    objDispatch.SDD_SGST = objDispatch.SDD_GSTRate / 2
                    objDispatch.SDD_SGSTAmount = objDispatch.SDD_GSTAmount / 2
                    objDispatch.SDD_CGST = objDispatch.SDD_GSTRate / 2
                    objDispatch.SDD_CGSTAmount = objDispatch.SDD_GSTAmount / 2
                    objDispatch.SDD_IGST = 0
                    objDispatch.SDD_IGSTAmount = 0

                    Arr = objDispatch.SaveDispatchDetails(sSession.AccessCode, objDispatch)
                    objDispatch.UpdateStockLedgerClosingBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, objDispatch.SDD_CommodityID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, objDispatch.SDD_Quantity, sSession.IPAddress, iOrderID, iMasterID, 1, iDefaultBranch)

                Next
                iAllocateID = 0
                objDispatch.UpdateDispatchFlag(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDM_OrderID, iAllocateID)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateLocalBill")
        End Try
    End Sub
    'Public Sub CalculateLocalBill(ByVal iMasterID As Integer, ByVal iOrderID As Integer, ByVal dVAT As Double, ByVal dExcise As Double)
    '    Dim Arr() As String, OrderNo As String = ""
    '    Dim sCode As String = ""

    '    Dim lnkGoods As New LinkButton
    '    Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblUnit, lblMRP, lblOrderedQty, lblTotalAmount As New Label
    '    Dim lblDiscount, lblDiscountAmount, lblTax, lblTaxAmount, lblExciseDuty, lblExciseDutyAmount, lblCST, lblCSTAmount, lblTotal As New Label

    '    Dim iAllocateID As Integer
    '    Dim objDispatch As New ClsDispatchDetails
    '    Try

    '        sCode = objDispatch.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
    '        If dgExistingProFormaSalesOrder.Rows.Count > 0 Then
    '            For i = 0 To dgExistingProFormaSalesOrder.Rows.Count - 1

    '                lblCommodityID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCommodityID")
    '                lblItemID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblItemID")
    '                lblHistoryID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblHistoryID")
    '                lblUnitID = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblUnitID")
    '                'lblCommodity = grdDispatchDetails.Rows(i).FindControl("lblCommodity")
    '                lnkGoods = dgExistingProFormaSalesOrder.Rows(i).FindControl("lnkGoods")
    '                lblUnit = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblUnit")
    '                lblMRP = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblMRP")
    '                lblOrderedQty = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblOrderedQty")
    '                lblTotalAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblAmount")
    '                lblDiscount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblDiscount")
    '                lblDiscountAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblDiscountAmount")
    '                lblTax = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblVAT")
    '                lblTaxAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblVATAmount")
    '                lblExciseDuty = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblExcise")
    '                lblExciseDutyAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblExciseAmount")
    '                lblCST = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCST")
    '                lblCSTAmount = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblCSTAmount")
    '                lblTotal = dgExistingProFormaSalesOrder.Rows(i).FindControl("lblNetAmount")


    '                objDispatch.SDD_MasterID = iMasterID
    '                objDispatch.SDD_CommodityID = lblCommodityID.Text
    '                objDispatch.SDD_DescID = lblItemID.Text
    '                objDispatch.SDD_HistoryID = lblHistoryID.Text
    '                objDispatch.SDD_UnitID = lblUnitID.Text

    '                objDispatch.SDD_Rate = lblMRP.Text
    '                objDispatch.SDD_Quantity = lblOrderedQty.Text

    '                objDispatch.SDD_Discount = lblDiscount.Text
    '                objDispatch.SDD_Excise = lblExciseDuty.Text

    '                If txtOrderDate.Text <> "" Then
    '                    If sCode = "C" Then

    '                        objDispatch.SDD_CST = 0
    '                        objDispatch.SDD_CSTAmount = 0

    '                        objDispatch.SDD_RateAmount = objDispatch.SDD_Rate * objDispatch.SDD_Quantity

    '                        Dim dVATAmt, dBasicAmt As Double

    '                        If objDispatch.SDD_Discount > 0 And dExcise = 0 Then

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            dVATAmt = (objDispatch.SDD_RateAmount * 100) / (dVAT + 100)

    '                            'dBasicAmt = (objDispatch.SDD_RateAmount - objDispatch.SDD_VATAmount)
    '                            objDispatch.SDD_VATAmount = (objDispatch.SDD_RateAmount - dVATAmt)
    '                            dBasicAmt = (objDispatch.SDD_RateAmount - objDispatch.SDD_VATAmount)

    '                            objDispatch.SDD_Discount = lblDiscount.Text
    '                            objDispatch.SDD_DiscountAmount = (dBasicAmt * objDispatch.SDD_Discount) / 100

    '                            objDispatch.SDD_Excise = 0
    '                            objDispatch.SDD_ExciseAmount = 0

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_VATAmount = (dBasicAmt * dVAT) / 100

    '                            objDispatch.SDD_TotalAmount = (dBasicAmt - objDispatch.SDD_DiscountAmount)
    '                        End If

    '                        If dExcise > 0 And objDispatch.SDD_Discount = 0 Then

    '                            objDispatch.SDD_Discount = 0
    '                            objDispatch.SDD_DiscountAmount = 0

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            dVATAmt = (objDispatch.SDD_RateAmount * 100) / (dVAT + 100)

    '                            objDispatch.SDD_VATAmount = (objDispatch.SDD_RateAmount - dVATAmt)
    '                            dBasicAmt = (objDispatch.SDD_RateAmount - objDispatch.SDD_VATAmount)

    '                            objDispatch.SDD_Excise = objDispatch.GetExcise(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_ExciseAmount = (dBasicAmt * dExcise) / 100

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_VATAmount = ((dBasicAmt + objDispatch.SDD_ExciseAmount) * dVAT) / 100

    '                            objDispatch.SDD_TotalAmount = (dBasicAmt + objDispatch.SDD_ExciseAmount)
    '                        End If

    '                        If objDispatch.SDD_Discount > 0 And dExcise > 0 Then

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            dVATAmt = (objDispatch.SDD_RateAmount * 100) / (dVAT + 100)

    '                            objDispatch.SDD_VATAmount = (objDispatch.SDD_RateAmount - dVATAmt)
    '                            dBasicAmt = (objDispatch.SDD_RateAmount - objDispatch.SDD_VATAmount)

    '                            objDispatch.SDD_Discount = lblDiscount.Text
    '                            objDispatch.SDD_DiscountAmount = (dBasicAmt * objDispatch.SDD_Discount) / 100

    '                            objDispatch.SDD_Excise = objDispatch.GetExcise(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_ExciseAmount = ((dBasicAmt - objDispatch.SDD_DiscountAmount) * dExcise) / 100

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_VATAmount = (((dBasicAmt - objDispatch.SDD_DiscountAmount) + objDispatch.SDD_ExciseAmount) * dVAT) / 100

    '                            objDispatch.SDD_TotalAmount = ((dBasicAmt - objDispatch.SDD_DiscountAmount) + objDispatch.SDD_ExciseAmount)
    '                        End If

    '                        If objDispatch.SDD_Discount = 0 And dExcise = 0 Then
    '                            objDispatch.SDD_Discount = 0
    '                            objDispatch.SDD_DiscountAmount = 0

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            dVATAmt = (objDispatch.SDD_RateAmount * 100) / (dVAT + 100)

    '                            objDispatch.SDD_VATAmount = (objDispatch.SDD_RateAmount - dVATAmt)
    '                            dBasicAmt = (objDispatch.SDD_RateAmount - objDispatch.SDD_VATAmount)

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_VATAmount = (dBasicAmt * dVAT) / 100

    '                            objDispatch.SDD_Excise = 0
    '                            objDispatch.SDD_ExciseAmount = 0

    '                            objDispatch.SDD_TotalAmount = dBasicAmt
    '                        End If

    '                    ElseIf sCode = "P" Then

    '                        objDispatch.SDD_CST = 0
    '                        objDispatch.SDD_CSTAmount = 0

    '                        objDispatch.SDD_RateAmount = objDispatch.SDD_Rate * objDispatch.SDD_Quantity

    '                        If objDispatch.SDD_Discount > 0 And dExcise = 0 Then
    '                            objDispatch.SDD_Discount = lblDiscount.Text
    '                            objDispatch.SDD_DiscountAmount = (objDispatch.SDD_RateAmount * objDispatch.SDD_Discount) / 100

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_VATAmount = ((objDispatch.SDD_RateAmount - objDispatch.SDD_DiscountAmount) * dVAT) / 100

    '                            objDispatch.SDD_Excise = 0
    '                            objDispatch.SDD_ExciseAmount = 0

    '                            objDispatch.SDD_TotalAmount = (objDispatch.SDD_RateAmount - objDispatch.SDD_DiscountAmount)
    '                        End If

    '                        If dExcise > 0 And objDispatch.SDD_Discount = 0 Then
    '                            objDispatch.SDD_Discount = 0
    '                            objDispatch.SDD_DiscountAmount = 0

    '                            objDispatch.SDD_Excise = objDispatch.GetExcise(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_ExciseAmount = (objDispatch.SDD_RateAmount * dExcise) / 100

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_VATAmount = ((objDispatch.SDD_RateAmount + objDispatch.SDD_ExciseAmount) * dVAT) / 100

    '                            objDispatch.SDD_TotalAmount = (objDispatch.SDD_RateAmount + objDispatch.SDD_ExciseAmount)
    '                        End If

    '                        If objDispatch.SDD_Discount > 0 And dExcise > 0 Then
    '                            objDispatch.SDD_Discount = lblDiscount.Text
    '                            objDispatch.SDD_DiscountAmount = (objDispatch.SDD_RateAmount * objDispatch.SDD_Discount) / 100

    '                            objDispatch.SDD_Excise = objDispatch.GetExcise(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_ExciseAmount = ((objDispatch.SDD_RateAmount - objDispatch.SDD_DiscountAmount) * dExcise) / 100

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_VATAmount = (((objDispatch.SDD_RateAmount - objDispatch.SDD_DiscountAmount) + objDispatch.SDD_ExciseAmount) * dVAT) / 100

    '                            objDispatch.SDD_TotalAmount = ((objDispatch.SDD_RateAmount - objDispatch.SDD_DiscountAmount) + objDispatch.SDD_ExciseAmount)
    '                        End If

    '                        If objDispatch.SDD_Discount = 0 And dExcise = 0 Then
    '                            objDispatch.SDD_Discount = 0
    '                            objDispatch.SDD_DiscountAmount = 0

    '                            objDispatch.SDD_VAT = objDispatch.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, txtOrderDate.Text)
    '                            objDispatch.SDD_VATAmount = (objDispatch.SDD_RateAmount * dVAT) / 100

    '                            objDispatch.SDD_Excise = 0
    '                            objDispatch.SDD_ExciseAmount = 0

    '                            objDispatch.SDD_TotalAmount = objDispatch.SDD_RateAmount
    '                        End If

    '                    End If
    '                End If
    '                objDispatch.SDD_Status = "W"
    '                objDispatch.SDD_CompID = sSession.AccessCodeID
    '                objDispatch.SDD_Operation = "C"
    '                objDispatch.SDD_IPAddress = sSession.IPAddress
    '                objDispatch.SDD_CreatedBy = sSession.UserID
    '                objDispatch.SDD_CreatedOn = System.DateTime.Now

    '                Arr = objDispatch.SaveDispatchDetails(sSession.AccessCode, objDispatch)
    '                objDispatch.UpdateStockLedgerClosingBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, objDispatch.SDD_CommodityID, objDispatch.SDD_DescID, objDispatch.SDD_HistoryID, objDispatch.SDD_Quantity, sSession.IPAddress, iOrderID, iMasterID, 1)

    '            Next
    '            iAllocateID = 0
    '            objDispatch.UpdateDispatchFlag(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDispatch.SDM_OrderID, iAllocateID)
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub ddlPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentType.SelectedIndexChanged
        Try
            If UCase(ddlPaymentType.SelectedItem.Text) = UCase("Cheque") Then
                divcollapseChequeDetails.Visible = True
                GetBankDDL()
            Else
                'VisibleFalse()
                divcollapseChequeDetails.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPaymentType_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub GetBankDDL()
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Try
            sPerm = objOral.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Bank", "Bank")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            BindBankName(sArray1(3))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetBankDDL")
        End Try
    End Sub
    Public Sub VisibleTrue()
        Try
            lblChequeNo.Visible = True : txtChequeNo.Visible = True : lblChequeDate.Visible = True : txtChequeDate.Visible = True
            lblIFSCCode.Visible = True : txtIFSCCode.Visible = True : lblBankName.Visible = True
            lblBranch.Visible = True : txtBranch.Visible = True

            lblChequeNo.Enabled = True : txtChequeNo.Enabled = True : lblChequeDate.Enabled = True : txtChequeDate.Enabled = True
            lblIFSCCode.Enabled = True : txtIFSCCode.Enabled = True : lblBankName.Enabled = True
            lblBranch.Enabled = True : txtBranch.Enabled = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub VisibleFalse()
        Try
            lblChequeNo.Visible = False : txtChequeNo.Visible = False : lblChequeDate.Visible = False : txtChequeDate.Visible = False
            lblIFSCCode.Visible = False : txtIFSCCode.Visible = False : lblBankName.Visible = False
            lblBranch.Visible = False : txtBranch.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/Sales/OralCounterMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
        Dim dt, dtTable As New DataTable
        Try
            If ddlChargeType.SelectedIndex > 0 Then
                If txtShippingRate.Text <> "" Then
                    dt = AddCharges()
                    dtTable = objOral.RemoveDublicate(dt)
                    GvCharge.DataSource = dtTable
                    GvCharge.DataBind()

                    ddlChargeType.SelectedIndex = 0 : txtShippingRate.Text = ""
                Else
                    lblError.Text = "Enter Amount charged."
                End If
            Else
                lblError.Text = "Select Charge Type."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddCharge_Click")
        End Try
    End Sub
    Public Function AddCharges() As DataTable
        Dim dr As DataRow
        Dim dt1 As New DataTable
        Try
            If IsNothing(Session("ChargesMaster")) = False Then
                dt1 = Session("ChargesMaster")
            Else
                dt1.Columns.Add("ChargeID")
                dt1.Columns.Add("SlNo")
                dt1.Columns.Add("ChargeType")
                dt1.Columns.Add("ChargeAmount")
            End If

            dr = dt1.NewRow
            dr("ChargeID") = ddlChargeType.SelectedValue
            dr("SlNo") = dt1.Rows.Count + 1
            dr("ChargeType") = Trim(ddlChargeType.SelectedItem.Text)
            dr("ChargeAmount") = Trim(txtShippingRate.Text)
            dt1.Rows.Add(dr)

            Session("ChargesMaster") = dt1
            Return dt1
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AddCharges")
        End Try
    End Function
    Public Sub VisibleTrueDispatchControls()
        Try
            lblChargeType.Visible = True : lblShippingRate.Visible = True
            ddlChargeType.Visible = True : txtShippingRate.Visible = True
            imgbtnAddCharge.Visible = True : lblDispatchRefNo.Visible = True : txtDispatchRefNo.Visible = True : lblESugamNo.Visible = True : txtESugamNo.Visible = True
            lblDispatchDate.Visible = True : txtDispatchDate.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub VisibleFalseDispatchControls()
        Try
            lblChargeType.Visible = False : lblShippingRate.Visible = False
            ddlChargeType.Visible = False : txtShippingRate.Visible = False
            imgbtnAddCharge.Visible = False : lblDispatchRefNo.Visible = False : txtDispatchRefNo.Visible = False : lblESugamNo.Visible = False : txtESugamNo.Visible = False
            lblDispatchDate.Visible = False : txtDispatchDate.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindCharges()
        Dim dt As New DataTable
        Dim iOrderID As Integer
        Try
            If ddlSearch.SelectedIndex > 0 Then
                iOrderID = ddlSearch.SelectedValue
            Else
                iOrderID = txtOrderID.Text
            End If
            dt = objOral.GetChargesGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            GvCharge.DataSource = dt
            GvCharge.DataBind()
            'Session("SavedCharges") = dt
            Session("ChargesMaster") = dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCharges")
        End Try
    End Sub
    'Private Sub txtDispatchDate_TextChanged(sender As Object, e As EventArgs) Handles txtDispatchDate.TextChanged
    '    Dim dt As New DataTable
    '    Dim sCode As String = ""
    '    Dim IHistoryID As Integer
    '    Dim sStockHistoryID As String = ""
    '    Dim iCategoryID As Integer
    '    Dim dOrderDate As Date
    '    Try
    '        If txtOrderDate.Text <> "" Then
    '            If ddlParty.SelectedIndex > 0 Then
    '                If lstBoxDescription.SelectedIndex <> -1 Then
    '                    LoadDiscount()
    '                    'LoadVAT()
    '                    'LoadExcise()
    '                    ddlCommodity.SelectedValue = objOral.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
    '                    sStockHistoryID = objOral.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

    '                    'Stock qty restriction for 0 & -ve'
    '                    hfAvailableQty.Value = objOral.GetAvailableStockOfThisItem(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
    '                    'Stock qty restriction for 0 & -ve'

    '                    If ddlCategory.SelectedIndex > 0 Then
    '                        iCategoryID = ddlCategory.SelectedValue
    '                    Else
    '                        iCategoryID = 0
    '                    End If

    '                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '                    sCode = objOral.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
    '                    If sCode.StartsWith("P") Then
    '                        txtMRP.Enabled = True
    '                        txtCode.Value = "P"

    '                        dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
    '                        'If dt.Rows.Count = 0 Then
    '                        '    Exit Sub
    '                        'End If
    '                        If dt.Rows.Count > 1 Then
    '                            ddlRate.Enabled = True
    '                            ddlRate.DataSource = dt
    '                            ddlRate.DataValueField = "INVH_ID"
    '                            ddlRate.DataTextField = "INVH_Retail"
    '                            ddlRate.DataBind()
    '                            hfMRP.Value = ddlRate.SelectedItem.Text
    '                            txtMRP.Text = ddlRate.SelectedItem.Text
    '                            txtMRPFromTable.Text = ddlRate.SelectedItem.Text

    '                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

    '                            LoadVAT(ddlRate.SelectedValue)
    '                            LoadExcise(ddlRate.SelectedValue)

    '                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
    '                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
    '                        Else
    '                            ddlRate.Items.Clear()
    '                            ddlRate.Enabled = False
    '                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
    '                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
    '                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

    '                            'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
    '                            IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "P", dOrderDate)
    '                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

    '                            LoadVAT(IHistoryID)
    '                            LoadExcise(IHistoryID)

    '                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
    '                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
    '                            If txtMRP.Text = 0 Then
    '                                txtMRP.Text = ""
    '                                lblEffectiveDates.Text = ""
    '                            End If

    '                        End If

    '                    Else
    '                        txtMRP.Enabled = False
    '                        txtCode.Value = "C"
    '                        dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
    '                        'If dt.Rows.Count = 0 Then
    '                        '    Exit Sub
    '                        'End If
    '                        If dt.Rows.Count > 1 Then
    '                            ddlRate.Enabled = True
    '                            ddlRate.DataSource = dt
    '                            ddlRate.DataValueField = "INVH_ID"
    '                            ddlRate.DataTextField = "INVH_MRP"
    '                            ddlRate.DataBind()
    '                            hfMRP.Value = ddlRate.SelectedItem.Text
    '                            txtMRP.Text = ddlRate.SelectedItem.Text
    '                            txtMRPFromTable.Text = ddlRate.SelectedItem.Text

    '                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

    '                            LoadVAT(ddlRate.SelectedValue)
    '                            LoadExcise(ddlRate.SelectedValue)

    '                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
    '                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
    '                        Else
    '                            ddlRate.Items.Clear()
    '                            ddlRate.Enabled = False

    '                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
    '                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
    '                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

    '                            'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
    '                            IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "C", dOrderDate)
    '                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

    '                            LoadVAT(IHistoryID)
    '                            LoadExcise(IHistoryID)

    '                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
    '                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
    '                            If txtMRP.Text = 0 Then
    '                                txtMRP.Text = ""
    '                                lblEffectiveDates.Text = ""
    '                            End If

    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub txtOrderDate_TextChanged(sender As Object, e As EventArgs) Handles txtOrderDate.TextChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Dim IHistoryID As Integer
        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer
        Dim dOrderDate As Date
        Try
            lblEffectiveDates.Text = ""
            hfAvailableQty.Value = ""
            lblError.Text = ""
            'btnSave.Text = "Add"
            txtQuantity.Text = "" : txtAmount.Text = "" : hfAmount.Value = ""
            txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""
            If ddlParty.SelectedIndex > 0 Then
                If lstBoxDescription.SelectedIndex <> -1 Then

                    LoadDiscount()
                    'LoadVAT()
                    'LoadExcise()
                    ddlCommodity.SelectedValue = objOral.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                    sStockHistoryID = objOral.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                    'Stock qty restriction for 0 & -ve'
                    hfAvailableQty.Value = objOral.GetAvailableStockOfThisItem(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                    'Stock qty restriction for 0 & -ve'

                    If ddlCategory.SelectedIndex > 0 Then
                        iCategoryID = ddlCategory.SelectedValue
                    Else
                        iCategoryID = 0
                    End If

                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    sCode = objOral.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                    If sCode.StartsWith("P") Then
                        txtMRP.Enabled = True
                        txtCode.Value = "P"

                        dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                        'If dt.Rows.Count = 0 Then
                        '    Exit Sub
                        'End If
                        If dt.Rows.Count > 1 Then
                            ddlRate.Enabled = True
                            ddlRate.DataSource = dt
                            ddlRate.DataValueField = "INVH_ID"
                            ddlRate.DataTextField = "INVH_Retail"
                            ddlRate.DataBind()
                            hfMRP.Value = ddlRate.SelectedItem.Text
                            txtMRP.Text = ddlRate.SelectedItem.Text
                            txtMRPFromTable.Text = ddlRate.SelectedItem.Text

                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                            'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            'If ddlVAT.SelectedItem.Text = 0 Then
                            '    ddlVAT.SelectedIndex = 0
                            'End If
                            'LoadVAT(ddlRate.SelectedValue)

                            'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            'If ddlExcise.SelectedItem.Text = 0 Then
                            '    txtExciseAmount.Text = 0
                            '    hfExciseAmount.Value = 0
                            'End If
                            'LoadExcise(ddlRate.SelectedValue)

                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                        Else
                            ddlRate.Items.Clear()
                            ddlRate.Enabled = False
                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                            'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
                            IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "P", dOrderDate)
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                            'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                            'If ddlVAT.SelectedItem.Text = 0 Then
                            '    ddlVAT.SelectedIndex = 0
                            'End If

                            'LoadVAT(IHistoryID)

                            'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                            'If ddlExcise.SelectedItem.Text = 0 Then
                            '    txtExciseAmount.Text = 0
                            '    hfExciseAmount.Value = 0
                            'End If
                            'LoadExcise(IHistoryID)

                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)

                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                            If txtMRP.Text = 0 Then
                                txtMRP.Text = ""
                                lblEffectiveDates.Text = ""
                            End If

                        End If

                    Else
                        txtMRP.Enabled = False
                        txtCode.Value = "C"
                        dt = objOral.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                        'If dt.Rows.Count = 0 Then
                        '    Exit Sub
                        'End If
                        If dt.Rows.Count > 1 Then
                            ddlRate.Enabled = True
                            ddlRate.DataSource = dt
                            ddlRate.DataValueField = "INVH_ID"
                            ddlRate.DataTextField = "INVH_MRP"
                            ddlRate.DataBind()
                            hfMRP.Value = ddlRate.SelectedItem.Text
                            txtMRP.Text = ddlRate.SelectedItem.Text
                            txtMRPFromTable.Text = ddlRate.SelectedItem.Text

                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                            'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            'If ddlVAT.SelectedItem.Text = 0 Then
                            '    ddlVAT.SelectedIndex = 0
                            'End If

                            'LoadVAT(ddlRate.SelectedValue)

                            'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            'If ddlExcise.SelectedItem.Text = 0 Then
                            '    txtExciseAmount.Text = 0
                            '    hfExciseAmount.Value = 0
                            'End If

                            'LoadExcise(ddlRate.SelectedValue)

                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)

                        Else
                            ddlRate.Items.Clear()
                            ddlRate.Enabled = False

                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objOral.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                            'IHistoryID = objOral.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
                            IHistoryID = objOral.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, iCategoryID, "C", dOrderDate)
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                            'ddlVAT.SelectedValue = objOral.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                            'If ddlVAT.SelectedItem.Text = 0 Then
                            '    ddlVAT.SelectedIndex = 0
                            'End If

                            'LoadVAT(IHistoryID)

                            'ddlExcise.SelectedValue = objOral.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                            'If ddlExcise.SelectedItem.Text = 0 Then
                            '    txtExciseAmount.Text = 0
                            '    hfExciseAmount.Value = 0
                            'End If
                            'LoadExcise(IHistoryID)

                            'hfItemVAT.Value = objOral.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)

                            lblEffectiveDates.Text = objOral.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                            If txtMRP.Text = 0 Then
                                txtMRP.Text = ""
                                lblEffectiveDates.Text = ""
                            End If

                        End If
                    End If

                    'Category Code'
                    If UCase(ddlCategory.SelectedItem.Text) = "NA" Then
                        txtMRP.Enabled = True
                    ElseIf UCase(ddlCategory.SelectedItem.Text) = "NOT FOR SALE" Then
                        txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                        txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                        txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))

                        hfMRP.Value = 0 : hfAmount.Value = 0 : hfDiscountAmount.Value = 0
                        hfNetAmount.Value = 0
                    Else
                        txtMRP.Enabled = False
                    End If
                    'Category Code'

                    'Sale Type Extra code'
                    If ddlRate.SelectedValue <> "" Then
                        If ddlRate.SelectedValue > 0 Then
                            IHistoryID = ddlRate.SelectedValue
                        Else
                            IHistoryID = IHistoryID
                        End If
                    Else
                        IHistoryID = IHistoryID
                    End If
                    'Sale Type Extra code'

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtOrderDate_TextChanged")
        End Try
    End Sub
    'Private Sub GvCharge_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvCharge.RowCommand
    '    Dim dt As New DataTable
    '    Dim lblSlNo As New Label
    '    Dim dataTable As New DataTable
    '    Try
    '        If e.CommandName = "Delete" Then
    '            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
    '            lblSlNo = DirectCast(clickedRow.FindControl("lblSlNo"), Label)

    '            If ddlSearch.SelectedIndex > 0 Or txtOrderID.Text <> "" Then
    '                dt = Session("SavedCharges")
    '                dt.Rows.RemoveAt(lblSlNo.Text - 1)
    '                dt.AcceptChanges()
    '            Else
    '                dt = Session("ChargesMaster")
    '                dt.Rows.RemoveAt(lblSlNo.Text - 1)
    '                dt.AcceptChanges()
    '            End If

    '            GetChargesTable(dt)
    '            'Dim dataView As New DataView(dt)
    '            'dataView.Sort = "SlNo ASC"
    '            'dataTable = dataView.ToTable

    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Sub GetChargesTable(ByVal dt As DataTable)
    '    Dim dt1 As New DataTable
    '    Dim dr As DataRow
    '    Try
    '        GvCharge.DataSource = Nothing
    '        GvCharge.DataBind()

    '        If dt.Rows.Count > 0 Then
    '            dt1.Columns.Add("ChargeID")
    '            dt1.Columns.Add("SlNo")
    '            dt1.Columns.Add("ChargeType")
    '            dt1.Columns.Add("ChargeAmount")
    '            For i = 0 To dt.Rows.Count - 1
    '                dr = dt1.NewRow
    '                dr("ChargeID") = dt.Rows(i)("ChargeID")
    '                dr("SlNo") = i + 1
    '                dr("ChargeType") = dt.Rows(i)("ChargeType")
    '                dr("ChargeAmount") = dt.Rows(i)("ChargeAmount")
    '                dt1.Rows.Add(dr)
    '            Next
    '        End If
    '        Session("ChargesMaster") = dt1
    '        divcollapseCharges.Visible = True
    '        'UpdatePanel2.Update()

    '        GvCharge.DataSource = dt1
    '        GvCharge.DataBind()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub GvCharge_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles GvCharge.ItemCommand
        Dim dt As New DataTable
        Try
            If e.CommandName = "Delete" Then
                dt = Session("ChargesMaster")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                Session("ChargesMaster") = dt
            End If
            GvCharge.DataSource = dt
            GvCharge.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objOral.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
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
            dt = objOral.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
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
            dt = objOral.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
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
            dt = objOral.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            txtCompanyAddress.Text = "" : txtCompanyGSTNRegNo.Text = "" : txtDeliveryFromAddress.Text = "" : txtDeliveryFromGSTNRegNo.Text = ""
            If ddlBranch.SelectedIndex > 0 Then
                dt = objOral.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, ddlBranch.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUSTB_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUSTB_GSTNRegNo")

                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUSTB_CompanyType")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUSTB_GSTNCategory")

                    txtDeliveryFromAddress.Text = txtCompanyAddress.Text
                    txtDeliveryFromGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
                End If
            Else
                dt = objOral.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TaxPayableCategory")

                    txtDeliveryFromAddress.Text = txtCompanyAddress.Text
                    txtDeliveryFromGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub chkboxFrom_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxFrom.CheckedChanged
        Try
            If chkboxFrom.Checked = True Then
                txtDeliveryFromAddress.Text = ""
                txtDeliveryFromGSTNRegNo.Text = ""
            Else
                txtDeliveryFromAddress.Text = txtCompanyAddress.Text
                txtDeliveryFromGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkboxFrom_CheckedChanged")
        End Try
    End Sub
    Private Sub chkboxTo_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxTo.CheckedChanged
        Try
            If chkboxTo.Checked = True Then
                txtDeleveryAddress.Text = ""
                txtDeliveryGSTNRegNo.Text = ""
            Else
                txtDeleveryAddress.Text = txtBillingAddress.Text
                txtDeliveryGSTNRegNo.Text = txtBillingGSTNRegNo.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkboxTo_CheckedChanged")
        End Try
    End Sub
    Public Function CheckSourceDestinationOfDispatch(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            sSource = sBillingAddress.Substring(0, 2)
            sDestination = sDeliveryAddress.Substring(0, 2)

            If sSource = sDestination Then
                CheckSourceDestinationOfDispatch = "Local"
            Else
                CheckSourceDestinationOfDispatch = "Inter State"
            End If
            Return CheckSourceDestinationOfDispatch
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationOfDispatch")
        End Try
    End Function
    Public Function CheckSourceDestinationState(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            'sSource = sBillingAddress.Substring(0, 2)
            'sDestination = sDeliveryAddress.Substring(0, 2)

            'If sSource = sDestination Then
            '    CheckSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
            'Else
            '    CheckSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
            'End If
            If sBillingAddress <> "" And sDeliveryAddress = "" Then
                sSource = sBillingAddress.Substring(0, 2)
                CheckSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sDeliveryAddress <> "" Then
                sDestination = sDeliveryAddress.Substring(0, 2)
                CheckSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sDeliveryAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sDeliveryAddress.Substring(0, 2)
                If sSource = sDestination Then
                    CheckSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    CheckSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return CheckSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationState")
        End Try
    End Function
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Acc_Head").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Head").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_Group").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Group").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGroup").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGroup").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_GL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_GL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                Return sPerm
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer, ByVal sStatus As String, ByVal sReason As String) As Integer
        Dim sRet As String = ""
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try
            objCOA.igl_id = 0
            objCOA.igl_head = iHead
            objCOA.igl_Parent = iParent
            objCOA.sgl_glcode = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, iAccHead, iParent)
            objCOA.sgl_Desc = objGen.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(sReason)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = iAccHead
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "A"
            objCOA.sgl_IPAddress = sSession.IPAddress

            'sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            If sStatus = "Save" Then
                sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            ElseIf sStatus = "Update" Then
                objCOA.igl_id = txtGLID.Text
                sRet = objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            End If

            sArray = sRet.Split(",")
            Return sArray(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            ddlBranch.SelectedValue = ddlAccBrnch.SelectedValue
            ddlBranch_SelectedIndexChanged(sender, e)

            If ddlAccBrnch.SelectedIndex > 0 Then
                iParent = objOral.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                ddlAccArea.SelectedValue = iParent
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iParent = objOral.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                ddlAccRgn.SelectedValue = iParent
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iParent = objOral.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                ddlAccZone.SelectedValue = iParent
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccBrnch_SelectedIndexChanged")
        End Try
    End Sub
End Class
