Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.Object
Partial Class Sales_SalesOrder
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_SalesOrder"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objProForma As New clsPROFormaSalesOrder
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Private objclsModulePermission As New clsModulePermission
    Private objAccSetting As New clsAccountSetting
    Private objSettings As New clsApplicationSettings

    Private Shared iDocID As Integer
    Private objIndex As New clsIndexing
    Dim dt As New DataTable
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Dim IHistoryID As Integer
    Dim objPO As New clsPurchaseOrder
    Dim iDefaultBranch As Integer
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
        imgbtnBack.ImageUrl = "~/Images/BackWard24.png"
        imgbtnCreateCustomer.ImageUrl = "~/Images/Add16.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iINVH_Unit As Integer
        Dim iPices As Integer
        Dim iCategoryID As Integer
        Dim sFormButtons As String = ""
        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Dim sDate As String = ""
        Try

            RFVddlParty.InitialValue = "Select Customer"
            RFVPaymentType.InitialValue = "Select Payment Type"
            RFVCommodity.InitialValue = "Select Commodity"

            sSession = Session("AllSession")
            If IsPostBack = False Then

                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                txtHistoryID.Text = ""

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SO")
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnNew.Visible = False : imgbtnDelete.Visible = False : ibtnInsert.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnNew.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnAdd.Visible = True
                        imgbtnDelete.Visible = True
                        ibtnInsert.Visible = True
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If
                'If sSession.YearID > 0 Then
                '    sStr = sSession.YearName
                '    sArray = sStr.Split("-")
                '    iSYear = sArray(0)
                '    iEYear = sArray(1)

                'dStartDate = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'dEndDate = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                '    txtOrderDate_CalendarExtender.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    txtOrderDate_CalendarExtender.EndDate = New DateTime(iEYear, 3, dEndDate)

                'End If

                'rgvtxtOrderDate.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvtxtOrderDate.MinimumValue = "" & dStartDate & ""
                'rgvtxtOrderDate.MaximumValue = "" & dEndDate & ""

                'imgbtnAdd.Visible = False : imgbtnDelete.Visible = False : imgbtnReport.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasSO", 1)
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
                'End If

                'imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False : imgbtnReport.Visible = False

                'txtStartDate.Text = objGenFun.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtEndDate.Text = objGenFun.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                sDate = objProForma.GetApplicationStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If sDate <> "" Then
                    lblStartDate.Text = objGen.FormatDtForRDBMS(objProForma.GetApplicationStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
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

                LoadZone() : LoadCurrencyType() : GetAppSettings()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)

                iDefaultBranch = objProForma.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                LoadExistingOrderNo()
                LoadCommodity()
                LoadPaymentType()
                LoadModeOfCommunication()
                LoadParty()
                LoadMethodOfShiping()
                LoadSalesMan()
                BindDescription(0, iDefaultBranch)

                'LoadDiscount()
                LoadCategory()

                dgExistingProFormaSalesOrder.DataSource = Nothing
                dgExistingProFormaSalesOrder.DataBind()
                dgExistingProFormaSalesOrder.Visible = False

                RFVAccZone.InitialValue = "--- Select Zone ---" : RFVAccZone.ErrorMessage = "Select Zone."
                RFVAccRgn.InitialValue = "--- Select Region ---" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "--- Select Area ---" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "--- Select Branch ---" : RFVAccBrnch.ErrorMessage = "Select Branch."

                Me.lstBoxDescription.Attributes.Add("OnClick", "return ValidateParty()")
                Me.ddlCommodity.Attributes.Add("OnClick", "return ValidateMasterData()")
                'Me.imgbtnAdd.Attributes.Add("OnClick", "return ValidateForm()")

                Me.txtQuantity.Attributes.Add("onblur", "return RateAmount()")
                'Me.ddlDiscount.Attributes.Add("onChange", "return RateAmount()")
                Me.txtMRP.Attributes.Add("onblur", "return RateAmount()")

                Me.txtGrandDiscount.Attributes.Add("onblur", "return CalculateGrandDiscount()")

                If ddlCategory.SelectedIndex > 0 Then
                    iCategoryID = ddlCategory.SelectedValue
                Else
                    iCategoryID = 0
                End If

                Dim dOrderDate As Date
                If txtOrderDate.Text <> "" Then
                    If lstBoxDescription.SelectedIndex > 0 Then
                        If ddlParty.SelectedIndex > 0 Then
                            If txtOrderDate.Text <> "" Then
                                dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            End If

                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lstBoxDescription.SelectedValue, ddlParty.SelectedValue, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            iPices = objDBL.SQLExecuteScalarInt(sSession.AccessCode, "Select INVH_PerPieces From Inventory_master_History Where INVH_INV_ID =" & lstBoxDescription.SelectedValue & " And INVH_CompID=" & sSession.AccessCodeID & " and InvH_YearID = " & sSession.YearID & "")
                            iINVH_Unit = objProForma.GetINVH_Unit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lstBoxDescription.SelectedValue)

                            IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, txtMRPFromTable.Text, "C")
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)
                        End If
                    End If
                End If

                If ddlRate.SelectedIndex > 0 Then
                    lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedIndex)
                End If

                Dim iSOID As String = ""
                iSOID = objGen.DecryptQueryString(Request.QueryString("SOID"))
                If iSOID <> "" Then
                    ddlSearch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("SOID"))
                    ddlSearch_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadCurrencyType()
        Dim dt As New DataTable
        Try
            dt = objSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCurrency.DataSource = dt
            ddlCurrency.DataTextField = "CUR_CODE"
            ddlCurrency.DataValueField = "CUR_ID"
            ddlCurrency.DataBind()
            ddlCurrency.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetAppSettings()
        Dim dt As New DataTable
        Dim i As Integer
        Try
            dt = objSettings.GetApllicationSettingsDetails(sSession.AccessCode, sSession.AccessCodeID)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("sad_Config_Key") = "Currency" Then    'Currency
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        ddlCurrency.SelectedValue = dt.Rows(i)("sad_Config_Value")
                    End If
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
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
            dt = objAccSetting.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
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
            dt = objAccSetting.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
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
            dt = objAccSetting.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingOrderNo()
        Dim dt As New DataTable
        Try
            dt = objProForma.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "")
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "SPO_OrderCode"
            ddlSearch.DataValueField = "SPO_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Order No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDescription(ByVal iCommodityID As Integer, ByVal iDefaultBranch As Integer)
        Try
            lstBoxDescription.DataSource = objProForma.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID, iDefaultBranch)
            lstBoxDescription.DataTextField = "INV_Code"
            lstBoxDescription.DataValueField = "INV_ID"
            lstBoxDescription.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCodeAnddate()
        Try
            txtOrderCode.Text = objProForma.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            txtOrderDate.Text = objGen.FormatDtForRDBMS(System.DateTime.Now, "D")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCodeAnddate")
        End Try
    End Sub
    Private Sub LoadSalesMan()
        Try
            ddlSalesMan.DataSource = objProForma.LoadSalesMan(sSession.AccessCode, sSession.AccessCodeID)
            ddlSalesMan.DataTextField = "username"
            ddlSalesMan.DataValueField = "Usr_id"
            ddlSalesMan.DataBind()
            ddlSalesMan.Items.Insert(0, "Select Sales Person")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadMethodOfShiping()
        Try
            ddlModeOfShipping.DataSource = objProForma.LoadMethodOfShiping(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlParty.DataSource = objProForma.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "BM_Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCategory()
        Try
            ddlCategory.DataSource = objProForma.BindCategory(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlModeOfCommunication.DataSource = objProForma.BindModeOfCommunication(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlPaymentType.DataSource = objProForma.BindPaymentType(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlCommodity.DataSource = objProForma.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Private Sub LoadDiscount()
    '    Dim dt As New DataTable
    '    Try
    '        dt = objProForma.LoadDiscounts(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlDiscount.DataSource = dt
    '        ddlDiscount.DataTextField = "Mas_Desc"
    '        ddlDiscount.DataValueField = "Mas_Id"
    '        ddlDiscount.DataBind()
    '        ddlDiscount.Items.Insert(0, "Select Discount")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub BindUnitOfMeassurement(ByVal iItemID As Integer, ByVal iHistoryID As Integer)
        Try
            ddlUnitOfMeassurement.DataSource = objProForma.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iItemID, iHistoryID)
            ddlUnitOfMeassurement.DataTextField = "Mas_Desc"
            ddlUnitOfMeassurement.DataValueField = "Mas_ID"
            ddlUnitOfMeassurement.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Dim dt As New DataTable
        Dim iBranch As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranch = ddlAccBrnch.SelectedValue
            Else
                iBranch = 0
            End If
            If ddlCommodity.SelectedIndex > 0 Then
                BindDescription(ddlCommodity.SelectedValue, iBranch)
            Else
                BindDescription(0, iBranch)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""

        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer
        Dim dOrderDate As Date
        Dim iBranch As Integer
        Try
            'txtMRP.Enabled = False
            lblError.Text = ""
            ClearonPartySelection()

            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranch = ddlAccBrnch.SelectedValue
            Else
                iBranch = 0
            End If
            BindDescription(0, iBranch)
            If ddlParty.SelectedIndex > 0 Then
                If txtOrderDate.Text <> "" Then
                    lstBoxDescription.Enabled = True
                    dt = objProForma.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
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
                        Next
                    End If

                    If ddlCategory.SelectedIndex > 0 Then
                        iCategoryID = ddlCategory.SelectedValue
                    Else
                        iCategoryID = 0
                    End If

                    If txtOrderDate.Text <> "" Then
                        dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    sCode = objProForma.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                    If ddlCommodity.SelectedIndex > 0 Then
                        If lstBoxDescription.SelectedIndex <> -1 Then
                            sStockHistoryID = objProForma.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                            If sCode.StartsWith("P") Then

                                txtCode.Value = "P"
                                lblPCRate.Text = "Retail Rate"

                                dt = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
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
                                    lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                                Else
                                    ddlRate.Enabled = False
                                    hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                    txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                    txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                                    IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
                                    txtHistoryID.Text = IHistoryID
                                    BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                                    lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                                    If txtMRP.Text = 0 Then
                                        lblEffectiveDates.Text = ""
                                        txtMRP.Text = ""
                                    End If

                                End If
                            Else

                                txtCode.Value = "C"
                                lblPCRate.Text = "MRP Rate"

                                dt = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
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
                                    lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                                Else
                                    ddlRate.Enabled = False
                                    hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                    txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                    txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                                    IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
                                    txtHistoryID.Text = IHistoryID
                                    BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                                    lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                                    If txtMRP.Text = 0 Then
                                        lblEffectiveDates.Text = ""
                                        txtMRP.Text = ""
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If
            Else
                txtPartyNo.Text = "" : txtAddress.Text = "" : txtContactNo.Text = ""
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
            dt = objProForma.BindExistingOrder(sSession.AccessCode, sSession.AccessCodeID, iMasterID)
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
        Dim bCheck As String = "" : Dim sStatus As String = ""
        Dim dDate, dSDate As Date : Dim m As Integer, iBaseID As Integer = 0

        Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
        Dim sPerm As String = ""
        Dim sArray1 As Array
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

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
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
                txtOrderDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            If ddlSearch.SelectedIndex > 0 Then
                iOrderID = ddlSearch.SelectedValue
            Else
                If txtOrderID.Text <> "" Then
                    iOrderID = txtOrderID.Text
                Else
                    iOrderID = 0
                End If
            End If
            bCheck = objProForma.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            If bCheck = True Then
                lblError.Text = "Selected Order No has been dispatched already."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                Exit Sub
            Else
                sStatus = objProForma.CheckOrderForAllocationApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
                If sStatus <> "" Then
                    If sStatus = "True" Then
                        lblError.Text = "Selected Order No has been Allocated already."
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                        Exit Sub
                    End If
                End If
            End If

            'Save Master'
            objProForma.SPO_OrderCode = objGen.SafeSQL(Trim(txtOrderCode.Text))
            If txtOrderDate.Text = "" Then
                lblError.Text = "Enter Order Date"
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtOrderDate.Focus()
                Exit Sub
            End If
            objProForma.SPO_OrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Dim dDatel, dSDateo As Date
            'Cheque Date Comparision'
            dDatel = Date.ParseExact(lblStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDateo = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim f As Integer
            f = DateDiff(DateInterval.Day, dDatel, dSDateo)
            If f < 0 Then
                lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Application Start Date(" & lblStartDate.Text & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtOrderDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            objProForma.SPO_PartyCode = objGen.SafeSQL(Trim(txtPartyNo.Text))
            objProForma.SPO_PartyName = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
            objProForma.SPO_Address = objGen.SafeSQL(Trim(txtAddress.Text))
            objProForma.SPO_ContantNo = objGen.SafeSQL(Trim(txtContactNo.Text))

            If ddlModeOfShipping.SelectedIndex > 0 Then
                objProForma.SPO_ModeOfDispatch = objGen.SafeSQL(Trim(ddlModeOfShipping.SelectedValue))
            Else
                objProForma.SPO_ModeOfDispatch = 0
            End If
            If txtShippingDate.Text <> "" Then
                objProForma.SPO_ShippingDate = Date.ParseExact(objGen.SafeSQL(Trim(txtShippingDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtShippingDate.Text <> "" Then
                'Dim dDate, dSDate As Date
                'Cheque Date Comparision'
                dDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtShippingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'Dim m As Integer
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

            objProForma.SPO_PaymentType = objGen.SafeSQL(Trim(ddlPaymentType.SelectedValue))

            If ddlModeOfCommunication.SelectedIndex > 0 Then
                objProForma.SPO_ModeOfCommunication = objGen.SafeSQL(Trim(ddlModeOfCommunication.SelectedValue))
            Else
                objProForma.SPO_ModeOfCommunication = 0
            End If
            If txtInputBy.Text <> "" Then
                objProForma.SPO_InputBy = objGen.SafeSQL(Trim(txtInputBy.Text))
            Else
                objProForma.SPO_InputBy = ""
            End If

            If ddlShippingCharges.SelectedIndex > 0 Then
                objProForma.SPO_ShippingCharge = objGen.SafeSQL(Trim(ddlShippingCharges.SelectedValue))
            Else
                objProForma.SPO_ShippingCharge = 0
            End If

            objProForma.SPO_CreatedBy = sSession.UserID
            objProForma.SPO_CreatedOn = DateTime.Today
            objProForma.SPO_Status = "A"
            objProForma.SPO_Operation = "C"
            objProForma.SPO_IPAddress = sSession.IPAddress

            objProForma.SPO_OrderType = "S"
            objProForma.SPO_DispatchFlag = 0

            If ddlSalesMan.SelectedIndex > 0 Then
                objProForma.SPO_SalesManID = objGen.SafeSQL(Trim(ddlSalesMan.SelectedValue))
            Else
                objProForma.SPO_SalesManID = 0
            End If

            If txtBuyerPurOrderNo.Text <> "" Then
                objProForma.SPO_BuyerOrderNo = objGen.SafeSQL(Trim(txtBuyerPurOrderNo.Text))
            Else
                objProForma.SPO_BuyerOrderNo = "Oral"
            End If

            If ddlCategory.SelectedIndex > 0 Then
                objProForma.SPO_Category = objGen.SafeSQL(Trim(ddlCategory.SelectedValue))
            Else
                objProForma.SPO_Category = 0
            End If

            If txtRemarks.Text <> "" Then
                objProForma.SPO_Remarks = objGen.SafeSQL(Trim(txtRemarks.Text))
            Else
                objProForma.SPO_Remarks = ""
            End If

            If txtBuyerOrderDate.Text <> "" Then
                objProForma.SPO_BuyerOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtBuyerOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            objProForma.SPO_SalesType = 0
            objProForma.SPO_OtherType = 0

            Dim dChequeDate As Date
            dChequeDate = "01/01/1900"

            objProForma.SPO_ChequeNo = ""
            objProForma.SPO_ChequeDate = dChequeDate
            objProForma.SPO_IFSCCode = ""
            objProForma.SPO_BankName = ""
            objProForma.SPO_Branch = ""

            objProForma.SPO_GoThroughDispatch = 0
            objProForma.SPO_DispatchRefNo = ""
            objProForma.SPO_ESugamNo = ""

            Dim dDispatchDate As Date
            dDispatchDate = "01/01/1900"
            objProForma.SPO_DispatchDate = dDispatchDate

            '***********Preethika*************************
            objProForma.SPO_ZoneID = ddlAccZone.SelectedValue
            objProForma.SPO_RegionID = ddlAccRgn.SelectedValue
            objProForma.SPO_AreaID = ddlAccArea.SelectedValue
            objProForma.SPO_BranchID = ddlAccBrnch.SelectedValue

            objProForma.SPO_CompanyAddress = ""
            objProForma.SPO_BillingAddress = ""
            objProForma.SPO_DeliveryFrom = ""
            objProForma.SPO_DeliveryAddress = ""
            objProForma.SPO_CompanyGSTNRegNo = ""
            objProForma.SPO_BillingGSTNRegNo = ""
            objProForma.SPO_DeliveryFromGSTNRegNo = ""
            objProForma.SPO_DeliveryGSTNRegNo = ""
            objProForma.SPO_DispatchStatus = ""
            objProForma.SPO_CompanyType = 0
            objProForma.SPO_GSTNCategory = 0
            objProForma.SPO_State = ""

            objProForma.SPO_BatchNo = 0
            objProForma.SPO_BaseName = 0

            Arr = objProForma.SavePROFormaMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objProForma)
            dt = Session("UpdateTab")
            iMasterID = Arr(1)

            objProForma.SPOD_SOID = iMasterID

            If txtQuantity.Text <> "" Then

                If txtDetailID.Text <> "" Then
                    objProForma.SPOD_Id = txtDetailID.Text
                Else
                    objProForma.SPOD_Id = 0
                End If

                objProForma.SPOD_CommodityID = objGen.SafeSQL(Trim(ddlCommodity.SelectedValue))
                objProForma.SPOD_ItemID = objGen.SafeSQL(Trim(lstBoxDescription.SelectedValue))
                objProForma.SPOD_Quantity = objGen.SafeSQL(Trim(txtQuantity.Text))

                'If ddlDiscount.SelectedIndex > 0 Then
                '    objProForma.SPOD_Discount = objGen.SafeSQL(Trim(ddlDiscount.SelectedItem.Text))
                'Else
                '    objProForma.SPOD_Discount = 0
                'End If

                objProForma.SPOD_UnitofMeasurement = objGen.SafeSQL(Trim(ddlUnitOfMeassurement.SelectedValue))

                If hfAmount.Value <> "" Then
                    objProForma.SPOD_RateAmount = Request.Form(hfAmount.UniqueID)
                Else
                    objProForma.SPOD_RateAmount = txtAmount.Text
                End If

                'If hfDiscountAmount.Value <> "" Then
                '    objProForma.SPOD_DiscountRate = Request.Form(hfDiscountAmount.UniqueID)
                'Else
                '    objProForma.SPOD_DiscountRate = 0
                'End If

                If txtOrderDate.Text <> "" Then
                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                sCode = objProForma.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                If sCode.StartsWith("P") Then
                    sStr = "P"
                Else
                    sStr = "C"
                End If

                If ddlRate.SelectedIndex <> -1 Then
                    objProForma.SPOD_HistoryID = ddlRate.SelectedValue
                Else
                    If txtHistoryID.Text <> "" Then
                        objProForma.SPOD_HistoryID = txtHistoryID.Text
                    Else
                        objProForma.SPOD_HistoryID = 0
                    End If
                    'Working'
                    'objProForma.SPOD_HistoryID = objProForma.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, objProForma.SPO_Category, sStr, dOrderDate)
                    'Working'
                End If

                objProForma.SPOD_CompiD = sSession.AccessCodeID
                objProForma.SPOD_Status = "A"

                If txtMRP.Text <> "" Then
                    hfMRP.Value = txtMRP.Text
                End If
                If hfMRP.Value <> "" Then
                    objProForma.SPOD_MRPRate = hfMRP.Value
                Else
                    objProForma.SPOD_MRPRate = 0
                End If

                If hfNetAmount.Value <> "" Then
                    objProForma.SPOD_TotalAmount = Request.Form(hfNetAmount.UniqueID)
                Else
                    objProForma.SPOD_TotalAmount = 0
                End If
                iBaseID = objProForma.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
                If hfNetAmount.Value <> "" Then
                    If ddlCurrency.SelectedValue = iBaseID Then
                        objProForma.SPOD_FETotalAmt = objGen.SafeSQL(hfNetAmount.Value)
                        objProForma.SPOD_CurrencyAmt = 0
                        objProForma.SPOD_CurrencyTime = ""
                    Else
                        objProForma.SPOD_FETotalAmt = objProForma.GetFERates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue, objGen.SafeSQL(hfNetAmount.Value))
                        objProForma.SPOD_CurrencyAmt = objProForma.GetFECRates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                        objProForma.SPOD_CurrencyTime = objProForma.GetFECTime(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                    End If
                Else
                    objProForma.SPOD_FETotalAmt = 0
                    objProForma.SPOD_CurrencyAmt = 0
                    objProForma.SPOD_CurrencyTime = ""
                End If
                If ddlCurrency.SelectedIndex > 0 Then
                    objProForma.SPOD_Currency = ddlCurrency.SelectedValue
                Else
                    objProForma.SPOD_Currency = 0
                End If
                objProForma.SPOD_Operation = "C"
                objProForma.SPOD_IPAddress = sSession.IPAddress

                If ddlCategory.SelectedIndex > 0 Then
                    objProForma.SPOD_Category = ddlCategory.SelectedValue
                Else
                    objProForma.SPOD_Category = 0
                End If

                objProForma.SPOD_CreatedBy = sSession.UserID
                objProForma.SPOD_CreatedOn = DateTime.Today
                objProForma.SPOD_UpdatedBy = sSession.UserID
                objProForma.SPOD_UpdatedOn = DateTime.Today

                objProForma.SPOD_GST_ID = 0
                objProForma.SPOD_GSTRate = 0
                objProForma.SPOD_GSTAmount = 0
                objProForma.SPOD_SGST = 0
                objProForma.SPOD_SGSTAmount = 0
                objProForma.SPOD_CGST = 0
                objProForma.SPOD_CGSTAmount = 0
                objProForma.SPOD_IGST = 0
                objProForma.SPOD_IGSTAmount = 0

                Arr = objProForma.SavePROFormaMasterDetails(sSession.AccessCode, objProForma, sSession.YearID)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    imgbtnAdd.ImageUrl = "~/Images/Add24.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                End If
            End If

            LoadExistingOrderNo()
            ddlSearch.SelectedValue = iMasterID
            ddlSearch_SelectedIndexChanged(sender, e)
            LoadExistingOrderGrid(ddlSearch.SelectedValue)
            pnlGrand.Visible = True
            'imgbtnUpdate.Visible = True : imgbtnReport.Visible = True

            'If btnSave.Text = "Update" Then
            '    objProForma.UpdateOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, lstBoxDescription.SelectedValue, ddlCommodity.SelectedValue, iMasterID)
            '    btnSave.Text = "Add"
            'End If
            txtOrderID.Text = iMasterID
            'TradeDiscount updation'
            TradeDiscount()
            'TradeDiscount updation'
            ClearDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
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
            'ddlDiscount.SelectedIndex = 0 : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
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

            lstBoxDescription.Items.Clear() : txtDetailID.Text = ""
            ddlUnitOfMeassurement.Items.Clear() : ddlRate.Items.Clear() : txtMRP.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = ""
            'ddlDiscount.SelectedIndex = 0 : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
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
            'ddlDiscount.SelectedIndex = 0 : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearDetails")
        End Try
    End Sub
    Private Sub ddlSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearch.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Dim bCheck As String = ""
        Dim sStatus As String = ""
        Dim iCurrnecy As Integer = 0
        Try
            lblError.Text = ""
            If ddlSearch.SelectedIndex > 0 Then
                'imgbtnAdd.Visible = False : imgbtnUpdate.Visible = True : imgbtnDelete.Visible = True : imgbtnReport.Visible = True
                imgbtnAdd.ImageUrl = "~/Images/Update24.png"
                ddlCurrency.Enabled = True
                txtGrandTotal.Text = "" : txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotalAmt.Text = ""
                txtSearch.Visible = False : btnSearch.Visible = False
                lstBoxDescription.Enabled = True
                dtMaster = objProForma.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
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

                    Next
                    iCurrnecy = objProForma.GetCurrency(sSession.AccessCode, sSession.AccessCodeID, ddlSearch.SelectedValue)
                    If iCurrnecy > 0 Then
                        ddlCurrency.Enabled = False
                        ddlCurrency.SelectedValue = iCurrnecy
                    End If
                End If

                lstBoxDescription.Items.Clear()
                LoadExistingOrderGrid(ddlSearch.SelectedValue)

                GetAttachFile(ddlSearch.SelectedItem.Text)

                bCheck = objProForma.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If bCheck = True Then
                    lblError.Text = "Selected Order No has been dispatched, it can not be edited."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                    Exit Sub
                Else
                    sStatus = objProForma.CheckOrderForAllocationApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                    If sStatus <> "" Then
                        If sStatus = "True" Then
                            lblError.Text = "Selected Order No has been Allocated, it can not be edited."
                            lblCustomerValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                            Exit Sub
                        End If
                    End If
                End If
            Else
                txtSearch.Text = ""
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
            iPartyID = objProForma.GetPartyID(sSession.AccessCode, sSession.AccessCodeID, txtPartyNo.Text)
            dt = objProForma.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, iPartyID)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
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
                    iPices = objProForma.GetPieceCount(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                    iINVH_Unit = objProForma.GetINVH_Unit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lstBoxDescription.SelectedValue)
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

                    'txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
                    'txtNetAmount.Text = "" : hfNetAmount.Value = ""

                    'If txtQuantity.Text <> "" Then

                    '    If txtQuantity.Text <> "" And ddlDiscount.SelectedIndex = 0 Then
                    '        txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                    '        hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                    '    End If

                    '    If txtQuantity.Text <> "" And ddlDiscount.SelectedIndex > 0 Then
                    '        If txtCode.Value = "C" Then
                    '            txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))
                    '            hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))

                    '            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                    '            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                    '        End If
                    '        If txtCode.Value = "P" Then
                    '            txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))
                    '            hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))

                    '            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                    '            hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                    '        End If
                    '    End If

                    'End If
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

                lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)

                If txtQuantity.Text <> "" Then
                    txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * txtMRP.Text))
                    hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtQuantity.Text * txtMRP.Text))

                    txtNetAmount.Text = "" : hfNetAmount.Value = ""

                    txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                    hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))

                    'txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
                    'txtNetAmount.Text = "" : hfNetAmount.Value = ""

                    'If txtQuantity.Text <> "" And ddlDiscount.SelectedIndex = 0 Then
                    '    txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                    '    hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text))
                    'End If

                    'If txtQuantity.Text <> "" And ddlDiscount.SelectedIndex > 0 Then
                    '    If txtCode.Value = "C" Then
                    '        txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))
                    '        hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))

                    '        txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                    '        hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                    '    End If
                    '    If txtCode.Value = "P" Then
                    '        txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))
                    '        hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((txtAmount.Text) * ddlDiscount.SelectedItem.Text) / 100))

                    '        txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                    '        hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtAmount.Text - txtDiscountAmount.Text)))
                    '    End If
                    'End If
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

        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer

        Dim dOrderDate As Date
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            hfAvailableQty.Value = ""
            lblError.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = "" : hfAmount.Value = ""
            'txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            If txtOrderDate.Text <> "" Then
                If ddlParty.SelectedIndex > 0 Then
                    If lstBoxDescription.SelectedIndex <> -1 Then

                        If txtOrderDate.Text <> "" Then
                            dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            'Cheque Date Comparision'
                            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            dSDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            m = DateDiff(DateInterval.Day, dDate, dSDate)
                            If m < 0 Then
                                lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
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
                                txtOrderDate.Focus()
                                Exit Sub
                            End If
                            'Cheque Date Comparision'
                        End If

                        'LoadDiscount()
                        ddlCommodity.SelectedValue = objProForma.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                        sStockHistoryID = objProForma.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                        'Stock qty restriction for 0 & -ve'
                        hfAvailableQty.Value = objProForma.GetAvailableStockOfThisItem(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                        'Stock qty restriction for 0 & -ve'

                        If ddlCategory.SelectedIndex > 0 Then
                            iCategoryID = ddlCategory.SelectedValue
                        Else
                            iCategoryID = 0
                        End If

                        sCode = objProForma.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                        If sCode.StartsWith("P") Then
                            'txtMRP.Enabled = True
                            txtCode.Value = "P"

                            dt = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
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
                                'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                                lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                            Else
                                ddlRate.Items.Clear()
                                ddlRate.Enabled = False
                                hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                                IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
                                txtHistoryID.Text = IHistoryID
                                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)
                                lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)

                                If txtMRP.Text = 0 Then
                                    txtMRP.Text = ""
                                    lblEffectiveDates.Text = ""
                                End If
                                'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                            End If

                        Else
                            'txtMRP.Enabled = False

                            txtCode.Value = "C"
                            dt = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
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
                                'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                                lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                            Else
                                ddlRate.Items.Clear()
                                ddlRate.Enabled = False

                                hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                                txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                                IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
                                txtHistoryID.Text = IHistoryID
                                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)
                                lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)

                                If txtMRP.Text = 0 Then
                                    txtMRP.Text = ""
                                    lblEffectiveDates.Text = ""
                                End If
                                'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                            End If
                        End If

                        'Category Code'
                        If UCase(ddlCategory.SelectedItem.Text) = "NA" Then
                            'txtMRP.Enabled = True
                        ElseIf UCase(ddlCategory.SelectedItem.Text) = "NOT FOR SALE" Then
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                            'txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                            txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))

                            hfMRP.Value = 0 : hfAmount.Value = 0  'hfDiscountAmount.Value = 0
                            hfNetAmount.Value = 0
                        Else
                            'txtMRP.Enabled = False
                        End If
                        'Category Code'

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

        Dim dOrderDate As Date
        Try

            If ddlCategory.SelectedIndex > 0 Then
                iCategoryID = ddlCategory.SelectedValue
            Else
                iCategoryID = 0
            End If
            If txtOrderDate.Text <> "" Then
                If lstBoxDescription.SelectedIndex <> -1 Then
                    If txtOrderDate.Text <> "" Then
                        dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    sStockHistoryID = objProForma.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                    sCode = objProForma.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                    If sCode.StartsWith("P") Then
                        'txtMRP.Enabled = True
                        txtCode.Value = "P"

                        dt = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
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
                            lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                            'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                        Else
                            ddlRate.Items.Clear()
                            ddlRate.Enabled = False
                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                            IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
                            txtHistoryID.Text = IHistoryID
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                            lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                            If txtMRP.Text = 0 Then
                                lblEffectiveDates.Text = ""
                                txtMRP.Text = ""
                            End If
                            'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                        End If
                    Else
                        'txtMRP.Enabled = False
                        txtCode.Value = "C"
                        dt = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
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
                            lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                            'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                        Else
                            ddlRate.Items.Clear()
                            ddlRate.Enabled = False

                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                            IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
                            txtHistoryID.Text = IHistoryID
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                            lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)
                            If txtMRP.Text = 0 Then
                                lblEffectiveDates.Text = ""
                                txtMRP.Text = ""
                            End If
                            'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                        End If
                    End If
                End If
            End If

            If ddlCategory.SelectedIndex > 0 Then
                If UCase(ddlCategory.SelectedItem.Text) = "NA" Then
                    'txtMRP.Enabled = True
                ElseIf UCase(ddlCategory.SelectedItem.Text) = "NOT FOR SALE" Then
                    txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                    'txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                    txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))

                    hfMRP.Value = 0 : hfAmount.Value = 0  'hfDiscountAmount.Value = 0
                    hfNetAmount.Value = 0
                Else
                    'txtMRP.Enabled = False
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
            dt = objProForma.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtSearch.Text))
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

                objProForma.SaveGrandTotalToOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, iOrderID, dGrandDiscount, dGrandDiscountAmt, dGrandTotal, dGrandTotalAmt)
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

        Dim bCheck As String = ""
        Dim iCategoryID As Integer

        Dim lblCommodityID As New Label
        Dim lblItemID As New Label
        Dim iBranch As Integer : Dim lblID As New Label
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
                txtDetailID.Text = lblID.Text
                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)
                iCommodityID = lblCommodityID.Text
                iItemID = lblItemID.Text

                'imgbtnAdd.Visible = False
                'imgbtnUpdate.Visible = True
                imgbtnAdd.ImageUrl = "~/Images/Update24.png"

                ddlCommodity.SelectedValue = objProForma.GetCommodity(sSession.AccessCode, sSession.AccessCodeID, iOrderNo, iItemID, lblID.Text)

                dt = objProForma.BindExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iItemID, iOrderNo, lblID.Text)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If ddlCategory.SelectedIndex > 0 Then
                            iCategoryID = ddlCategory.SelectedValue
                        Else
                            iCategoryID = 0
                        End If
                        If ddlAccBrnch.SelectedIndex > 0 Then
                            iBranch = ddlAccBrnch.SelectedValue
                        Else
                            iBranch = 0
                        End If

                        BindDescription(dt.Rows(i)("SPOD_CommodityID"), iBranch)
                        lstBoxDescription.SelectedValue = dt.Rows(i)("SPOD_ItemID")

                        txtQuantity.Text = dt.Rows(i)("SPOD_Quantity")

                        txtMRP.Text = dt.Rows(i)("SPOD_MRPRate")
                        hfMRP.Value = dt.Rows(i)("SPOD_MRPRate")

                        txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))
                        hfAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))

                        txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))
                        hfNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))

                        'LoadDiscount()

                        'If dt.Rows(i)("SPOD_Discount") > 0 Then
                        '    ddlDiscount.SelectedValue = objProForma.GetDiscountID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("SPOD_Discount"))
                        'Else
                        '    ddlDiscount.SelectedIndex = 0
                        'End If

                        'txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_DiscountRate")))
                        'hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_DiscountRate")))
                        Dim dOrderDate As Date
                        If txtOrderDate.Text <> "" Then
                            If txtOrderDate.Text <> "" Then
                                dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            End If

                            sCode = objProForma.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                            If sCode.StartsWith("P") Then
                                txtCode.Value = "P"
                                sStr = "P"
                                dtRate = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(i)("SPOD_HistoryID"), sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                                If dtRate.Rows.Count > 1 Then
                                    ddlRate.Enabled = True
                                    ddlRate.DataSource = dtRate
                                    ddlRate.DataValueField = "INVH_ID"
                                    ddlRate.DataTextField = "INVH_Retail"
                                    ddlRate.DataBind()
                                    ddlRate.SelectedValue = dt.Rows(i)("SPOD_HistoryID")

                                    'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                                Else
                                    'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                                End If
                            Else
                                txtCode.Value = "C"
                                sStr = "C"
                                dtRate = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(i)("SPOD_HistoryID"), sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
                                If dtRate.Rows.Count > 1 Then
                                    ddlRate.Enabled = True
                                    ddlRate.DataSource = dtRate
                                    ddlRate.DataValueField = "INVH_ID"
                                    ddlRate.DataTextField = "INVH_MRP"
                                    ddlRate.DataBind()
                                    ddlRate.SelectedValue = dt.Rows(i)("SPOD_HistoryID")

                                    'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                                Else
                                    'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                                End If
                            End If
                        End If

                        BindUnitOfMeassurement(lstBoxDescription.SelectedValue, dt.Rows(i)("SPOD_HistoryID"))
                        ddlUnitOfMeassurement.SelectedValue = dt.Rows(i)("SPOD_UnitOfMeasurement")
                        txtMRPFromTable.Text = txtMRP.Text

                        lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("SPOD_HistoryID"))
                    Next
                End If
            ElseIf e.CommandName = "Cancel" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                txtDetailID.Text = lblID.Text
                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)

                iCommodityID = lblCommodityID.Text
                iItemID = lblItemID.Text

                If ddlSearch.SelectedIndex > 0 Then
                    bCheck = objProForma.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                    If bCheck = True Then
                        lblError.Text = "Selected Order No has been dispatched, items can not be canceled."
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                        Exit Sub
                    Else
                        sStatus = objProForma.CheckOrderForAllocationApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                        If sStatus <> "" Then
                            If sStatus = "A" Then
                                lblError.Text = "Selected Order No has been Allocated & Approved, items can not be canceled."
                                lblCustomerValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                'imgbtnAdd.Enabled = False : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                                Exit Sub
                            End If
                        End If
                    End If
                End If
                objProForma.DeleteOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iItemID, iCommodityID, iOrderNo, sSession.IPAddress, lblID.Text)
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
    Private Sub dgExistingProFormaSalesOrder_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgExistingProFormaSalesOrder.RowDataBound
        Dim ibtnCancel As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                ibtnCancel = TryCast(e.Row.FindControl("ibtnCancel"), ImageButton)
                ibtnCancel.Attributes.Add("OnClick", "javascript:return Validate()")
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
                dt = objProForma.GetSearchPartyList(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtPartySearch.Text)
                ddlParty.DataSource = dt
                ddlParty.DataTextField = "BM_Name"
                ddlParty.DataValueField = "BM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer")
            Else
                LoadParty()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgExistingProFormaSalesOrder_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles dgExistingProFormaSalesOrder.RowCancelingEdit

    End Sub
    Private Sub dgExistingProFormaSalesOrder_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgExistingProFormaSalesOrder.RowEditing

    End Sub
    Private Sub imgbtnNew_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNew.Click
        Try
            lblError.Text = "" : txtSearch.Text = ""
            pnlGrand.Visible = False : txtGrandTotal.Text = "" : txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotalAmt.Text = ""
            Clear()
            GenerateOrderCodeAnddate()
            'imgbtnAdd.Visible = True : imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False : imgbtnReport.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNew_Click")
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
    '        objProForma.SPO_OrderCode = objGen.SafeSQL(Trim(txtOrderCode.Text))
    '        objProForma.SPO_OrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '        objProForma.SPO_PartyCode = objGen.SafeSQL(Trim(txtPartyNo.Text))
    '        objProForma.SPO_PartyName = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
    '        objProForma.SPO_Address = objGen.SafeSQL(Trim(txtAddress.Text))
    '        objProForma.SPO_ContantNo = objGen.SafeSQL(Trim(txtContactNo.Text))

    '        If ddlModeOfShipping.SelectedIndex > 0 Then
    '            objProForma.SPO_ModeOfDispatch = objGen.SafeSQL(Trim(ddlModeOfShipping.SelectedValue))
    '        Else
    '            objProForma.SPO_ModeOfDispatch = 0
    '        End If
    '        If txtShippingDate.Text <> "" Then
    '            objProForma.SPO_ShippingDate = Date.ParseExact(objGen.SafeSQL(Trim(txtShippingDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        End If
    '        objProForma.SPO_PaymentType = objGen.SafeSQL(Trim(ddlPaymentType.SelectedValue))

    '        If ddlModeOfCommunication.SelectedIndex > 0 Then
    '            objProForma.SPO_ModeOfCommunication = objGen.SafeSQL(Trim(ddlModeOfCommunication.SelectedValue))
    '        Else
    '            objProForma.SPO_ModeOfCommunication = 0
    '        End If
    '        If txtInputBy.Text <> "" Then
    '            objProForma.SPO_InputBy = objGen.SafeSQL(Trim(txtInputBy.Text))
    '        Else
    '            objProForma.SPO_InputBy = ""
    '        End If

    '        If ddlShippingCharges.SelectedIndex > 0 Then
    '            objProForma.SPO_ShippingCharge = objGen.SafeSQL(Trim(ddlShippingCharges.SelectedValue))
    '        Else
    '            objProForma.SPO_ShippingCharge = 0
    '        End If

    '        objProForma.SPO_CreatedBy = sSession.UserID
    '        objProForma.SPO_CreatedOn = DateTime.Today
    '        objProForma.SPO_Status = "A"
    '        objProForma.SPO_Operation = "C"
    '        objProForma.SPO_IPAddress = sSession.IPAddress

    '        objProForma.SPO_OrderType = "S"
    '        objProForma.SPO_DispatchFlag = 0

    '        If ddlSalesMan.SelectedIndex > 0 Then
    '            objProForma.SPO_SalesManID = objGen.SafeSQL(Trim(ddlSalesMan.SelectedValue))
    '        Else
    '            objProForma.SPO_SalesManID = 0
    '        End If

    '        If txtBuyerPurOrderNo.Text <> "" Then
    '            objProForma.SPO_BuyerOrderNo = objGen.SafeSQL(Trim(txtBuyerPurOrderNo.Text))
    '        Else
    '            objProForma.SPO_BuyerOrderNo = "Oral"
    '        End If

    '        If ddlCategory.SelectedIndex > 0 Then
    '            objProForma.SPO_Category = objGen.SafeSQL(Trim(ddlCategory.SelectedValue))
    '        Else
    '            objProForma.SPO_Category = 0
    '        End If

    '        If txtRemarks.Text <> "" Then
    '            objProForma.SPO_Remarks = objGen.SafeSQL(Trim(txtRemarks.Text))
    '        Else
    '            objProForma.SPO_Remarks = ""
    '        End If

    '        If txtBuyerOrderDate.Text <> "" Then
    '            objProForma.SPO_BuyerOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtBuyerOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        End If

    '        objProForma.SPO_SalesType = 0
    '        objProForma.SPO_OtherType = 0

    '        Dim dChequeDate As Date
    '        dChequeDate = "01/01/1900"

    '        objProForma.SPO_ChequeNo = ""
    '        objProForma.SPO_ChequeDate = dChequeDate
    '        objProForma.SPO_IFSCCode = ""
    '        objProForma.SPO_BankName = ""
    '        objProForma.SPO_Branch = ""

    '        objProForma.SPO_GoThroughDispatch = 0
    '        objProForma.SPO_DispatchRefNo = ""
    '        objProForma.SPO_ESugamNo = ""

    '        Dim dDispatchDate As Date
    '        dDispatchDate = "01/01/1900"
    '        objProForma.SPO_DispatchDate = dDispatchDate

    '        Arr = objProForma.SavePROFormaMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objProForma)
    '        dt = Session("UpdateTab")
    '        iMasterID = Arr(1)

    '        objProForma.SPOD_SOID = iMasterID

    '        If txtQuantity.Text <> "" Then

    '            objProForma.SPOD_CommodityID = objGen.SafeSQL(Trim(ddlCommodity.SelectedValue))
    '            objProForma.SPOD_ItemID = objGen.SafeSQL(Trim(lstBoxDescription.SelectedValue))
    '            objProForma.SPOD_Quantity = objGen.SafeSQL(Trim(txtQuantity.Text))

    '            'If ddlDiscount.SelectedIndex > 0 Then
    '            '    objProForma.SPOD_Discount = objGen.SafeSQL(Trim(ddlDiscount.SelectedItem.Text))
    '            'Else
    '            '    objProForma.SPOD_Discount = 0
    '            'End If

    '            objProForma.SPOD_UnitofMeasurement = objGen.SafeSQL(Trim(ddlUnitOfMeassurement.SelectedValue))

    '            If hfAmount.Value <> "" Then
    '                objProForma.SPOD_RateAmount = Request.Form(hfAmount.UniqueID)
    '            Else
    '                objProForma.SPOD_RateAmount = txtAmount.Text
    '            End If

    '            'If hfDiscountAmount.Value <> "" Then
    '            '    objProForma.SPOD_DiscountRate = Request.Form(hfDiscountAmount.UniqueID)
    '            'Else
    '            '    objProForma.SPOD_DiscountRate = 0
    '            'End If

    '            If txtOrderDate.Text <> "" Then
    '                dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            End If
    '            sCode = objProForma.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
    '            If sCode.StartsWith("P") Then
    '                sStr = "P"
    '            Else
    '                sStr = "C"
    '            End If

    '            If ddlRate.SelectedIndex <> -1 Then
    '                objProForma.SPOD_HistoryID = ddlRate.SelectedValue
    '            Else
    '                objProForma.SPOD_HistoryID = objProForma.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, objProForma.SPO_Category, sStr, dOrderDate)
    '            End If

    '            objProForma.SPOD_CompiD = sSession.AccessCodeID
    '            objProForma.SPOD_Status = "A"

    '            If txtMRP.Text <> "" Then
    '                hfMRP.Value = txtMRP.Text
    '            End If
    '            If hfMRP.Value <> "" Then
    '                objProForma.SPOD_MRPRate = hfMRP.Value
    '            Else
    '                objProForma.SPOD_MRPRate = 0
    '            End If

    '            If hfNetAmount.Value <> "" Then
    '                objProForma.SPOD_TotalAmount = Request.Form(hfNetAmount.UniqueID)
    '            Else
    '                objProForma.SPOD_TotalAmount = 0
    '            End If

    '            objProForma.SPOD_Operation = "C"
    '            objProForma.SPOD_IPAddress = sSession.IPAddress

    '            If ddlCategory.SelectedIndex > 0 Then
    '                objProForma.SPOD_Category = ddlCategory.SelectedValue
    '            Else
    '                objProForma.SPOD_Category = 0
    '            End If

    '            Arr = objProForma.SavePROFormaMasterDetails(sSession.AccessCode, objProForma, sSession.YearID)

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
    '        '    objProForma.UpdateOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, lstBoxDescription.SelectedValue, ddlCommodity.SelectedValue, iMasterID)
    '        '    btnSave.Text = "Add"
    '        'End If
    '        objProForma.UpdateOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, lstBoxDescription.SelectedValue, ddlCommodity.SelectedValue, iMasterID)
    '        imgbtnAdd.Visible = False : imgbtnUpdate.Visible = True

    '        txtOrderID.Text = iMasterID

    '        'TradeDiscount updation'
    '        TradeDiscount()
    '        'TradeDiscount updation'

    '        ClearDetails()

    '        imgbtnUpdate.Visible = False : imgbtnAdd.Visible = True
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
                bCheck = objProForma.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
                If bCheck = True Then
                    lblError.Text = "Selected Order No has been dispatched, it can not be delete."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                Else
                    objProForma.DeleteWholeOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, iOrderID)
                    objProForma.DeleteAllocationOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, iOrderID)

                    lblError.Text = "" : txtSearch.Text = ""
                    pnlGrand.Visible = False : txtGrandTotal.Text = "" : txtGrandDiscount.Text = "" : txtGrandDiscountAmt.Text = "" : txtGrandTotalAmt.Text = ""
                    Clear()
                    GenerateOrderCodeAnddate()
                    lblCustomerValidationMsg.Text = "Deleted Successfully."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    LoadExistingOrderNo()
                    ddlSearch.SelectedIndex = 0
                End If
            Else
                lblError.Text = "Select Existing Sales order/Create New Sales Order to Delete."
                lblCustomerValidationMsg.Text = "Select Existing Sales order/Create New Sales Order to Delete."
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
                iReportType = objProForma.GetReportTypeFromPrintSettings(sSession.AccessCode, sSession.AccessCodeID)
                If iReportType > 0 Then
                    Response.Redirect("~/Reports/Viewer/PROFormaHR.aspx?ExistingOrder=" & iSPOID)
                Else
                    Response.Redirect("~/Reports/Viewer/PROFormaReport.aspx?ExistingOrder=" & iSPOID)
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
            Response.Redirect(String.Format("~/Masters/CustomerMasterDetails.aspx?Status=SO"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnCreateCustomer_Click")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/Sales/SalesOrderMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub txtOrderDate_TextChanged(sender As Object, e As EventArgs) Handles txtOrderDate.TextChanged
        Dim dt As New DataTable
        Dim sCode As String = ""

        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer
        Dim dOrderDate As Date
        Dim dDate, dSDate As Date : Dim m As Integer

        Try
            hfAvailableQty.Value = ""
            lblError.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = "" : hfAmount.Value = ""
            'txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            If ddlParty.SelectedIndex > 0 Then
                If lstBoxDescription.SelectedIndex <> -1 Then

                    If txtOrderDate.Text <> "" Then
                        dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                        'Cheque Date Comparision'
                        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        dSDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        m = DateDiff(DateInterval.Day, dDate, dSDate)
                        If m < 0 Then
                            lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
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
                            txtOrderDate.Focus()
                            Exit Sub
                        End If
                        'Cheque Date Comparision'

                    End If

                    'LoadDiscount()
                    ddlCommodity.SelectedValue = objProForma.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                    sStockHistoryID = objProForma.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                    'Stock qty restriction for 0 & -ve'
                    hfAvailableQty.Value = objProForma.GetAvailableStockOfThisItem(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                    'Stock qty restriction for 0 & -ve'

                    If ddlCategory.SelectedIndex > 0 Then
                        iCategoryID = ddlCategory.SelectedValue
                    Else
                        iCategoryID = 0
                    End If

                    sCode = objProForma.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                    If sCode.StartsWith("P") Then
                        'txtMRP.Enabled = True
                        txtCode.Value = "P"

                        dt = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
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
                            'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)
                            lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                        Else
                            ddlRate.Items.Clear()
                            ddlRate.Enabled = False
                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                            IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "P")
                            txtHistoryID.Text = IHistoryID
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)
                            lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)

                            If txtMRP.Text = 0 Then
                                txtMRP.Text = ""
                                lblEffectiveDates.Text = ""
                            End If
                            'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                        End If

                    Else
                        'txtMRP.Enabled = False
                        txtCode.Value = "C"
                        dt = objProForma.BindMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)
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
                            'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, ddlRate.SelectedValue)

                            lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, ddlRate.SelectedValue)
                        Else
                            ddlRate.Items.Clear()
                            ddlRate.Enabled = False

                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(objProForma.GetMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sStockHistoryID, sCode, iCategoryID, lstBoxDescription.SelectedValue, dOrderDate)))

                            IHistoryID = objProForma.GetINVHID(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, iCategoryID, "C")
                            txtHistoryID.Text = IHistoryID
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)
                            lblEffectiveDates.Text = objProForma.GetEffectiveDates(sSession.AccessCode, sSession.AccessCodeID, IHistoryID)

                            If txtMRP.Text = 0 Then
                                txtMRP.Text = ""
                                lblEffectiveDates.Text = ""
                            End If
                            'hfItemVAT.Value = objProForma.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, IHistoryID)
                        End If
                    End If

                    'Category Code'
                    If UCase(ddlCategory.SelectedItem.Text) = "NA" Then
                        'txtMRP.Enabled = True
                    ElseIf UCase(ddlCategory.SelectedItem.Text) = "NOT FOR SALE" Then
                        txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                        'txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                        txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))

                        hfMRP.Value = 0 : hfAmount.Value = 0  'hfDiscountAmount.Value = 0
                        hfNetAmount.Value = 0
                    Else
                        'txtMRP.Enabled = False
                    End If
                    'Category Code'

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtOrderDate_TextChanged")
        End Try
    End Sub
    Private Sub dgExistingProFormaSalesOrder_RowUpdated(sender As Object, e As GridViewUpdatedEventArgs) Handles dgExistingProFormaSalesOrder.RowUpdated

    End Sub
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
    Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
        Dim objBatch As clsIndexing.BatchScan
        Dim Arr() As String
        Try
            If gvattach.Rows.Count > 0 Then
                AutomaticIndexing()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
            Else
                lblError.Text = "Add the files before index"
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub AutomaticIndexing()
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
        Dim Arr() As String
        Dim dDate As Date
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        'Dim iCabinet As Integer
        'Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable
        Dim bCheckCabinet As Boolean

        Try
            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlAccBrnch.Focus()
                Exit Sub
            Else
                icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
            End If

            iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Payment Voucher")

            If ddlSearch.SelectedIndex = 0 Then
                lblError.Text = "Select Existing Sales Order No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlSearch.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlSearch.SelectedItem.Text)
            End If

            iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

            'If ddlType.SelectedIndex = 0 Then
            '    lblModelError.Text = "Select Type."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            '    ddlType.Focus()
            '    Exit Sub
            'Else
            '    iType = ddlType.SelectedValue
            'End If

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                If gvattach.Rows.Count > 0 Then
                    For i = 0 To gvattach.Rows.Count - 1
                        iPageDetailsid = 0
                        chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                        lblPath = gvattach.Rows(i).FindControl("lblPath")
                        If chkSelect.Checked = True Then
                            sPageExt = UCase(gvattach.Rows(i).Cells(3).Text)
                            sFilePath = lblPath.Text
                            sFileName = gvattach.Rows(i).Cells(2).Text
                            objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objGen.SafeSQL(txtTitle.Text.Trim)
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt
                            If gvKeywords.Rows.Count > 0 Then

                                For k = 0 To gvKeywords.Rows.Count - 1
                                    txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
                                    If txtKeywords.Text <> "" Then
                                        sKeywords = sKeywords & "," & txtKeywords.Text
                                    End If
                                Next
                            End If
                            If sKeywords.StartsWith(",") = True Then
                                sKeywords = sKeywords.Remove(0, 1)
                            End If
                            If sKeywords.EndsWith(",") = True Then
                                sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
                            End If
                            objIndex.sPGEKeyWORD = objGen.SafeSQL(sKeywords)
                            objIndex.sPGEOCRText = ""
                            objIndex.iPGESIZE = 0
                            objIndex.iPGECURRENT_VER = 0
                            Select Case UCase(sPageExt)
                                Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                                    objIndex.sPGEOBJECT = "IMAGE"
                                Case Else
                                    objIndex.sPGEOBJECT = "OLE"
                            End Select
                            objIndex.sPGESTATUS = "A"
                            objIndex.iPGESubCabinet = iSubCabinet
                            objIndex.iPgeUpdatedBy = sSession.UserID

                            objIndex.spgeDelflag = "A"
                            objIndex.iPGEQCUsrGrpId = 0
                            objIndex.sPGEFTPStatus = "F"
                            objIndex.iPGEbatchname = objIndex.iPGEBASENAME
                            objIndex.spgeOrignalFileName = objGen.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
                            objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)

                            If gvDocumentType.Rows.Count > 0 Then
                                For j = 0 To gvDocumentType.Rows.Count - 1
                                    lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                                    txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                                    If objIndex.iPGEBASENAME = iPageDetailsid Then
                                        objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If Arr(0) = "3" Then
                        lblCustomerValidationMsg.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AutomaticIndexing")
        End Try
    End Sub
    Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            If sFileInDB = "FALSE" Then
                sImagePath = objIndex.GetImagePath(sSession.AccessCode)
                sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
                sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                If System.IO.File.Exists(sImagePath) = False Then
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub btnAttch_Click(sender As Object, e As EventArgs) Handles btnAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Try
            lblError.Text = "" : iDocID = 0

            If ddlSearch.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Existing Payment No."
                ddlSearch.Focus()
                Exit Sub
            End If

            Dim hfc As HttpFileCollection = Request.Files

            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dt.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        dt = Session("Attachment")
                        If dt.Rows.Count = 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)

                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        ElseIf dt.Rows.Count > 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        End If
                    End If
                Next
            End If

            If dt.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            End If

            Session("Attachment") = dt
            gvattach.DataSource = dt
            gvattach.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub gvattach_PreRender(sender As Object, e As EventArgs) Handles gvattach.PreRender
        Try
            If gvattach.Rows.Count > 0 Then
                gvattach.UseAccessibleHeader = True
                gvattach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvattach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach_PreRender")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Public Sub GetAttachFile(ByVal sTrNo As String)
        Dim dRow As DataRow
        Dim dt, dt1 As New DataTable
        Try
            dt.Columns.Add("FilePath")
            dt.Columns.Add("FileName")
            dt.Columns.Add("Extension")
            dt.Columns.Add("CreatedOn")

            dt1 = objProForma.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("FilePath") = ""
                    dRow("FileName") = dt1.Rows(i)("pge_Orignalfilename")
                    dRow("Extension") = dt1.Rows(i)("pge_ext")
                    dRow("CreatedOn") = objGen.FormatDtForRDBMS(dt1.Rows(i)("pge_createdon"), "D")
                    dt.Rows.Add(dRow)
                Next
            End If

            gvattach.DataSource = dt
            gvattach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub txtMRP_TextChanged(sender As Object, e As EventArgs) Handles txtMRP.TextChanged
        Dim dOrderDate As Date
        Dim sCode As String = "" : Dim sStr As String = ""
        Try
            If txtMRP.Text <> "" Then
                If txtOrderDate.Text <> "" Then
                    dOrderDate = Date.ParseExact(objGen.SafeSQL(Trim(txtOrderDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                sCode = objProForma.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                If sCode.StartsWith("P") Then
                    sStr = "P"
                Else
                    sStr = "C"
                End If
                txtHistoryID.Text = objProForma.GetCheckHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, objProForma.SPO_Category, sStr, dOrderDate, Trim(txtMRP.Text))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtMRP_TextChanged")
        End Try
    End Sub
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim iCabinetID, iSubCabinetID, iFolderID As Integer
        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Dim dt As New DataTable
        Try
            If ddlSearch.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Payment Voucher")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlSearch.SelectedItem.Text)

                    dt = objProForma.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            sSelectedChecksIDs = sSelectedChecksIDs & "," & dt.Rows(i)("PGE_BASENAME")
                        Next
                    End If

                    If sSelectedChecksIDs.StartsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                    End If
                    If sSelectedChecksIDs.EndsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(Len(sSelectedChecksIDs) - 1, 1)
                    End If

                    oSelectedCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iCabinetID))
                    oSelectedSubCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iSubCabinetID))
                    oSelectedFolID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iFolderID))
                    oSelectedChecksIDs = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedChecksIDs))
                    oSelectedIndexID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(0))

                    Response.Redirect(String.Format("~/Viewer/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={7}&SelectedDescID={8}&SelectedFrmtID={9}&SelectedCrByID={10}&SelectedIndexID={11}", "", "", oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, "", "", "", "", "", oSelectedIndexID), False)
                Else
                    lblError.Text = "No Attachments to view"
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Existing Payment No"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnView_Click")
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Dim iCommodity As Integer
        Dim iBranch As Integer
        Try
            If ddlCommodity.SelectedIndex > 0 Then
                iCommodity = ddlCommodity.SelectedValue
            Else
                iCommodity = 0
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranch = ddlAccBrnch.SelectedValue
            Else
                iBranch = 0
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iParent = objProForma.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                ddlAccArea.SelectedValue = iParent
                BindDescription(iCommodity, iBranch)
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iParent = objProForma.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                ddlAccRgn.SelectedValue = iParent
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iParent = objProForma.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                ddlAccZone.SelectedValue = iParent
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCurrency.SelectedIndexChanged
        Dim iBaseID As Integer, iCurrID As Integer
        Try
            lblError.Text = ""
            If ddlCurrency.SelectedIndex > 0 Then
                iBaseID = objProForma.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
                If ddlCurrency.SelectedValue = iBaseID Then
                Else
                    iCurrID = objProForma.GetFEID(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                    If iCurrID = 0 Then
                        lblError.Text = "Please set the exchange rates in Currency Master."
                        lblCustomerValidationMsg.Text = "Please set the exchange rates in Currency Master."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCurrency_SelectedIndexChanged")
        End Try
    End Sub
End Class
