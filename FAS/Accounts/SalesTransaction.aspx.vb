Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Windows.Forms
Imports DatabaseLayer
Partial Class Accounts_SalesTransaction
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts_SalesTransaction"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objJE As New clsJournalEntry
    Private Shared sSession As AllSession
    Public dtMerge As New DataTable
    Private Shared iDbOrCr As Integer = 0
    Dim objPSJEDetails As New ClsPurchaseSalesJEDetails
    Dim objSales As New clsSalesTranscation
    Private objAccSetting As New clsAccountSetting
    Dim objDb As New DBHelper
    Private Shared sSTSave As String

    Private Shared sTypeName As String
    Private Shared sMasterName As String
    Private Shared iMasterID As Integer
    Private Shared iPKID As Integer
    Private Shared sTableName As String
    Private Shared sGMSave As String
    Private Shared sGMFlag As String
    Private Shared sGMBackStatus As String
    Private Shared dVATAmt As Double
    Private Shared dVATAmtResult As Double
    Private Shared dCSTAmt As Double
    Private Shared dExciseAmt As Double
    Private Shared dChangedAmt As Double
    Private Shared sUMBackStatus As String
    Private objclsModulePermission As New clsModulePermission
    Private Shared dtCOA As New DataTable
    Dim objPayment As New clsPayment
    Public dtSales As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        'imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
        imgbtnApprove.ImageUrl = "~/Images/CheckMark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterType As String = ""
        Dim sMasterID As String = ""
        Dim dt As New DataTable
        Dim taxcategory As String
        Dim sFormButtons As String = ""
        Dim iDefaultBranch As Integer
        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "ST")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnApprove.Visible = False
                imgbtnAddCharge.Visible = False : btnItemAdd.Visible = False : sSTSave = "NO"
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
                        imgbtnAddCharge.Visible = True : btnItemAdd.Visible = True
                        sSTSave = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnApprove.Visible = True
                    End If
                End If
                'If sSession.YearID > 0 Then
                '    sStr = sSession.YearName
                '    sArray = sStr.Split("-")
                '    iSYear = sArray(0)
                '    iEYear = sArray(1)

                'dStartDate = objGenFun.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'dEndDate = objGenFun.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                '    cclReceiptDate.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    cclReceiptDate.EndDate = New DateTime(iEYear, 3, dEndDate)

                '    cclBillDate.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    cclBillDate.EndDate = New DateTime(iEYear, 3, dEndDate)
                'End If

                'rgvtxtReceiptDate.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvtxtReceiptDate.MinimumValue = "" & dStartDate & ""
                'rgvtxtReceiptDate.MaximumValue = "" & dEndDate & ""

                'txtStartDate.Text = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtEndDate.Text = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                LoadCommodity()

                imgbtnSave.ImageUrl = "~/Images/Save24.png"
                imgbtnSave.ToolTip = "Save" : imgbtnSave.Enabled = True
                dgItems.DataSource = Nothing
                dgItems.DataBind()

                dgJEDetails.DataSource = Nothing
                dgJEDetails.DataBind()

                BindBranch() : BindCompanyType()

                dt = objSales.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("CUST_COMM_Address")) = True Or IsDBNull(dt.Rows(0)("CUST_ProvisionalNo")) = True Or IsDBNull(dt.Rows(0)("CUST_INDTypeID")) = True Or IsDBNull(dt.Rows(0)("CUST_TAXPayableCategory")) = True Then
                        lblError.Text = "Fill the details in Company Master"
                        Exit Sub
                    End If
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")
                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TAXPayableCategory")

                    txtDeliveryFromAddress.Text = txtCompanyAddress.Text
                    txtDeliveryFromGSTNRegNo.Text = txtCompanyGSTNRegNo.Text

                    If ddlGSTCategory.SelectedIndex > 0 Then
                        taxcategory = objSales.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                        If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                            txtDeliveryFromGSTNRegNo.Enabled = False
                        Else
                            txtDeliveryFromGSTNRegNo.Enabled = True
                        End If
                    End If

                End If

                dt = Nothing
                Session("dtSales") = Nothing : dtSales = Nothing : DivAccountsDetails.Visible = False

                lblStatus.Text = "Not Started."
                txtBillAmount.Text = "0" : txtPaidAmount.Text = "0" : txtTradeDiscountAmt.Text = "0" : txtOtherCharge.Text = "0" : txtTradeDis.Text = "0"
                dtCOA = objPayment.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)

                iDefaultBranch = objSales.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                BindTypeOfTaxes() : LoadParty()
                LoadExistingSalesVoucher()
                LoadChargeType()
                Session("ChargesMaster") = Nothing

                dgSales.DataSource = BuildTable()
                dgSales.DataBind()

                txtTransactionNo.Text = objSales.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)

                RFVParty.InitialValue = "Select Customer" : RFVParty.ErrorMessage = "Select Customer"
                RFVBillNo.ErrorMessage = "Enter Valid Bill Number."

                'REVReceiptDate.ValidationExpression = "^[0-3]?[0-9]\/[01]?[0-9]\/[12][90][0-9][0-9]$"
                'RFVReceiptDate.ErrorMessage = "Enter Valid Date Format."

                REFBillDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                RFVBillDate.ErrorMessage = "Enter Valid Date Format."

                'RFVEBillAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                'RFVEBillAmount.ErrorMessage = "Enter Valid Bill Amount."

                REVAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVAmount.ErrorMessage = "Enter Valid Amount."

                REVTradeDiscountAmt.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVTradeDiscountAmt.ErrorMessage = "Enter Valid Trade Discount."

                REVTradeDis.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVTradeDis.ErrorMessage = "Enter Valid Trade Discount."

                REVTotalAmt.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVTotalAmt.ErrorMessage = "Enter Valid Trade Discount."

                RFVTaxType.InitialValue = "Select Tax Type" : RFVTaxType.ErrorMessage = "Select Tax Type"

                RFVAccZone.InitialValue = "--- Select Zone ---" : RFVAccZone.ErrorMessage = "Select Zone."
                RFVAccRgn.InitialValue = "--- Select Region ---" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "--- Select Area ---" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "--- Select Branch ---" : RFVAccBrnch.ErrorMessage = "Select Branch."

                RFVddlCommodity.InitialValue = "Select Commodity" : RFVddlCommodity.ErrorMessage = "Select Commodity"
                'RFVddlBranch.InitialValue = "Select Branch" : RFVddlBranch.ErrorMessage = "Select Branch"

                Me.chkCategory.Attributes.Add("OnClick", "return ValidateList()")

                Me.txtRate.Attributes.Add("onblur", "return ValidateRate()")

                Me.txtQuantity.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtDiscount.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtGSTRate.Attributes.Add("onblur", "return CalculateQuantity()")

                'Me.txtAmount.Attributes.Add("onblur", "return BasicAmt()")
                'Me.txtTradeDis.Attributes.Add("onblur", "return CalculateGrandDiscount()")
                'Me.txtTradeDiscountAmt.Attributes.Add("onblur", "return Calculate()")

                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    ddlExistSales.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExistSales_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sUMBackStatus = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
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
            Throw
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
            Throw
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
    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objSales.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function BuildTable() As DataTable
        Dim dc As DataColumn
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("CommodityID", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("GoodsID", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitID", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HSNCode", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Commodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Goods", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Unit", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MRP", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Qty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RateAMt", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Discount", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("DiscountAmt", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Charge", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Total", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("GST", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("GSTAmt", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("NetAmount", GetType(String))
            dtTab.Columns.Add(dc)
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub BindTypeOfTaxes()
        Try
            ddlTaxType.Items.Insert(0, "Select Tax Type")
            ddlTaxType.Items.Insert(1, "SGST")
            ddlTaxType.Items.Insert(2, "CGST")
            ddlTaxType.Items.Insert(3, "IGST")
            ddlTaxType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingSalesVoucher()
        Try
            ddlExistSales.DataSource = objSales.LoadExistingSalesVocher(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            ddlExistSales.DataTextField = "Acc_Sales_TransactionNo"
            ddlExistSales.DataValueField = "Acc_Sales_ID"
            ddlExistSales.DataBind()
            ddlExistSales.Items.Insert(0, "Existing Sales Voucher")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadParty()
        Try
            ddlParty.DataSource = objSales.LoadBuyers(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExistSales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistSales.SelectedIndexChanged
        Try
            If ddlExistSales.SelectedIndex > 0 Then
                imgbtnSave.ImageUrl = "~/Images/Update24.png"
                imgbtnSave.ToolTip = "Update" : imgbtnSave.Enabled = True
                BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSales.SelectedValue)
                'BindExeVoucherDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSales.SelectedValue)
            Else
                imgbtnSave.ImageUrl = "~/Images/Save24.png"
                imgbtnSave.ToolTip = "Save" : imgbtnSave.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistSales_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindExeVoucherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSales As Integer)
        Dim dt As New DataTable
        Try
            dt = objSales.LoadSalesDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iSales)
            Session("dtSales") = dt
            dgSales.DataSource = dt
            dgSales.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPayment As Integer)
        Dim dtNew As New DataTable
        Dim dPaidAmount As Double
        Dim dtCharge As New DataTable
        Dim dtItems As New DataTable
        Try
            dtNew = objSales.GetSalesDetails(sNameSpace, iCompID, iPayment)
            If dtNew.Rows.Count > 0 Then
                If IsDBNull(dtNew.Rows(0)("Acc_Sales_TransactionNo").ToString()) = False Then
                    txtTransactionNo.Text = dtNew.Rows(0)("Acc_Sales_TransactionNo").ToString()
                Else
                    txtTransactionNo.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Sales_Party").ToString()) = False Then
                    ddlParty.SelectedValue = dtNew.Rows(0)("Acc_Sales_Party").ToString()
                Else
                    ddlParty.SelectedIndex = 0
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Sales_BillNo").ToString()) = False Then
                    txtBillNo.Text = dtNew.Rows(0)("Acc_Sales_BillNo").ToString()
                Else
                    txtBillNo.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Sales_BillDate").ToString()) = False Then
                    txtBillDate.Text = objGen.FormatDtForRDBMS(dtNew.Rows(0)("Acc_Sales_BillDate").ToString(), "D")
                Else
                    txtBillDate.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Sales_ReceiptDate").ToString()) = False Then
                    txtReceiptDate.Text = objGen.FormatDtForRDBMS(dtNew.Rows(0)("Acc_Sales_ReceiptDate").ToString(), "D")
                Else
                    txtReceiptDate.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Sales_BillAmount").ToString()) = False Then
                    txtBillAmount.Text = Convert.ToDecimal(dtNew.Rows(0)("Acc_Sales_BillAmount").ToString()).ToString("#,##0.00")
                Else
                    txtBillAmount.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Sales_OtherCharges").ToString()) = False Then
                    txtOtherCharge.Text = Convert.ToDecimal(dtNew.Rows(0)("Acc_Sales_OtherCharges").ToString()).ToString("#,##0.00")
                Else
                    txtOtherCharge.Text = 0
                End If

                'dPaidAmount = Convert.ToDecimal(objSales.GetPaidAmount(sSession.AccessCode, sSession.AccessCodeID, iPayment)).ToString("#,##0.00")
                'txtPaidAmount.Text = dPaidAmount + Convert.ToDouble(txtOtherCharge.Text)
                'txtPaidAmount.Text = dPaidAmount
                txtPaidAmount.Text = 0

                If dtNew.Rows(0)("Acc_Sales_DelFlag").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Submission"
                ElseIf dtNew.Rows(0)("Acc_Sales_DelFlag").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                ElseIf dtNew.Rows(0)("Acc_Sales_DelFlag").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                End If

                If dtNew.Rows(0)("Acc_Sales_Status").ToString() = "S" Then
                    lblStatus.Text = "Submitted"
                End If

                'If dtNew.Rows(0)("Acc_Sales_PaymentStatus").ToString() = "P" Then
                '    lblError.Text = "Cannot Update the Bill. Receipt Already Paid."
                'Else

                'End If

                If IsDBNull(dtNew.Rows(0)("ACC_Sales_ZoneID").ToString()) = False Then
                    If (dtNew.Rows(0)("ACC_Sales_ZoneID").ToString() = "") Then
                    Else
                        ddlAccZone.SelectedValue = dtNew.Rows(0)("ACC_Sales_ZoneID").ToString()
                        LoadRegion(ddlAccZone.SelectedValue)
                    End If
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_RegionID").ToString()) = False Then
                    If (dtNew.Rows(0)("ACC_Sales_RegionID").ToString() = "") Then
                    Else
                        ddlAccRgn.SelectedValue = dtNew.Rows(0)("ACC_Sales_RegionID").ToString()
                        LoadArea(ddlAccRgn.SelectedValue)
                    End If
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_AreaID").ToString()) = False Then
                    If (dtNew.Rows(0)("ACC_Sales_AreaID").ToString() = "") Then
                    Else
                        ddlAccArea.SelectedValue = dtNew.Rows(0)("ACC_Sales_AreaID").ToString()
                        LoadAccBrnch(ddlAccArea.SelectedValue)
                    End If
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_BranchID").ToString()) = False Then
                    If (dtNew.Rows(0)("ACC_Sales_BranchID").ToString() = "") Then
                    Else
                        ddlAccBrnch.SelectedValue = dtNew.Rows(0)("ACC_Sales_BranchID").ToString()
                    End If
                End If

                If IsDBNull(dtNew.Rows(0)("ACC_Sales_CompanyType")) = False Then
                    ddlCompanyType.SelectedValue = dtNew.Rows(0)("ACC_Sales_CompanyType")
                Else
                    ddlCompanyType.SelectedIndex = 0
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_GSTNCategory")) = False Then
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dtNew.Rows(0)("ACC_Sales_GSTNCategory")
                Else
                    ddlGSTCategory.SelectedIndex = 0
                End If

                If IsDBNull(dtNew.Rows(0)("ACC_Sales_CompanyAddress")) = False Then
                    txtCompanyAddress.Text = dtNew.Rows(0)("ACC_Sales_CompanyAddress")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_BillingAddress")) = False Then
                    txtBillingAddress.Text = dtNew.Rows(0)("ACC_Sales_BillingAddress")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_DeliveryFrom")) = False Then
                    txtDeliveryFromAddress.Text = dtNew.Rows(0)("ACC_Sales_DeliveryFrom")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_DeliveryAddress")) = False Then
                    txtDeleveryAddress.Text = dtNew.Rows(0)("ACC_Sales_DeliveryAddress")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_CompanyGSTNRegNo")) = False Then
                    txtCompanyGSTNRegNo.Text = dtNew.Rows(0)("ACC_Sales_CompanyGSTNRegNo")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_BillingGSTNRegNo")) = False Then
                    txtBillingGSTNRegNo.Text = dtNew.Rows(0)("ACC_Sales_BillingGSTNRegNo")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_DeliveryFromGSTNRegNo")) = False Then
                    txtDeliveryFromGSTNRegNo.Text = dtNew.Rows(0)("ACC_Sales_DeliveryFromGSTNRegNo")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Sales_DeliveryGSTNRegNo")) = False Then
                    txtDeliveryGSTNRegNo.Text = dtNew.Rows(0)("ACC_Sales_DeliveryGSTNRegNo")
                End If

                If dtNew.Rows(0)("Acc_Sales_PaymentStatus").ToString() = "P" Then
                    lblError.Text = "Cannot Update the Bill. Payment Already Paid."
                    Exit Sub
                End If

                Dim description As String
                description = objSales.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                    txtDeliveryGSTNRegNo.Enabled = False
                Else
                    txtDeliveryGSTNRegNo.Enabled = True
                End If

            End If

            DivAccountsDetails.Visible = True
            GvAccountDetails.DataSource = objSales.GetBillAccountsDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistSales.SelectedValue, dtCOA)
            GvAccountDetails.DataBind()

            dtCharge = objSales.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistSales.SelectedValue, 9)
            GvCharge.DataSource = dtCharge
            GvCharge.DataBind()
            Session("ChargesMaster") = dtCharge

            dtItems = objSales.BindItemsData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistSales.SelectedValue)
            Session("dtSales") = dtItems
            dgItems.DataSource = dtItems
            dgItems.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlTaxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTaxType.SelectedIndexChanged
        Try
            ddlTaxRate.SelectedIndex = -1
            txtTaxAmount.Text = "" : txtTotal.Text = ""

            lblError.Text = ""
            If txtOtherCharge.Text = "" Then
                lblError.Text = "Enter Other Charges."
                ddlTaxType.SelectedIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Other Charges.','', 'success');", True)
                Exit Sub
            End If

            If ddlTaxType.SelectedIndex = 1 Then
                ddlTaxRate.DataSource = objSales.LoadVCE(sSession.AccessCode, "SGST", sSession.AccessCodeID)
                ddlTaxRate.DataTextField = "gl_desc"
                ddlTaxRate.DataValueField = "GL_ID"
                ddlTaxRate.DataBind()
                ddlTaxRate.Items.Insert(0, "Select SGST(%)")
            ElseIf ddlTaxType.SelectedIndex = 2 Then
                ddlTaxRate.DataSource = objSales.LoadVCE(sSession.AccessCode, "CGST", sSession.AccessCodeID)
                ddlTaxRate.DataTextField = "gl_desc"
                ddlTaxRate.DataValueField = "GL_ID"
                ddlTaxRate.DataBind()
                ddlTaxRate.Items.Insert(0, "Select CGST(%)")
            ElseIf ddlTaxType.SelectedIndex = 3 Then
                ddlTaxRate.DataSource = objSales.LoadVCE(sSession.AccessCode, "IGST", sSession.AccessCodeID)
                ddlTaxRate.DataTextField = "gl_desc"
                ddlTaxRate.DataValueField = "GL_ID"
                ddlTaxRate.DataBind()
                ddlTaxRate.Items.Insert(0, "Select IGST(%)")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTaxType_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlTaxRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTaxRate.SelectedIndexChanged
        Dim dPerValue As Double, dCSTAmt As Double, dExciseAmt As Double
        Try
            lblError.Text = ""
            If txtOtherCharge.Text = "" Then
                lblError.Text = "Enter Other Charges."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Other Charges.','', 'success');", True)
                Exit Sub
            End If
            If ddlTaxType.SelectedIndex = 1 Then  'VAT
                dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")

            ElseIf ddlTaxType.SelectedIndex = 2 Then  'CST

                dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")

            ElseIf ddlTaxType.SelectedIndex = 3 Then  'Excise

                dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTaxRate_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub btnAddTax_Click(sender As Object, e As EventArgs) Handles btnAddTax.Click
        Dim dtDetails As New DataTable
        Dim dr As DataRow
        Dim dTotalAmt As Double
        Try
            lblError.Text = ""
            dtSales = BuildTable()

            If IsNothing(Session("dtSales")) = False Then
                dtSales = Session("dtSales")
            End If

            Dim dtDGL As New DataTable
            Dim DVGLCODE As New DataView(dtCOA)
            If ddlTaxRate.SelectedIndex > 0 Then
                DVGLCODE.RowFilter = "Gl_id=" & ddlTaxRate.SelectedValue
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Rate of Tax ','', 'info');", True)
                ddlTaxRate.Focus()
                Exit Sub
            End If
            dtDGL = DVGLCODE.ToTable

            dr = dtSales.NewRow
            dr("ID") = dgSales.Items.Count + 1
            dr("HeadID") = dtDGL.Rows(0)("gl_AccHead")
            dr("GLID") = dtDGL.Rows(0)("gl_Parent")
            dr("SubGLID") = ddlTaxRate.SelectedValue
            dr("TaxTypeID") = ddlTaxType.SelectedIndex
            dr("TaxRateID") = ddlTaxRate.SelectedValue
            dr("Amount") = Convert.ToDecimal(txtAmount.Text).ToString("#,##0.00")
            dr("TradeDis") = txtTradeDis.Text
            dr("TradeDisAmt") = txtTradeDiscountAmt.Text
            dr("TotalNetAmt") = txtTotalNetAmt.Text
            dr("TaxType") = ddlTaxType.SelectedValue
            dr("TaxRate") = ddlTaxRate.SelectedItem.Text
            dr("TaxAmount") = Convert.ToDecimal(txtTaxAmount.Text).ToString("#,##0.00")

            Dim dTotalAmount As Double = Math.Round(Convert.ToDecimal(txtTotal.Text))
            dr("Total") = Convert.ToDecimal(dTotalAmount).ToString("#,##0.00")
            dr("Roundof") = Convert.ToDecimal(dTotalAmount - Convert.ToDecimal(txtTotal.Text)).ToString("#,##0.00")

            dtSales.Rows.Add(dr)

            dgSales.DataSource = dtSales
            dgSales.DataBind()

            Session("dtSales") = dtSales

            'txtPaidAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmount) + Convert.ToDouble(txtPaidAmount.Text)).ToString("#,##0.00")
            'txtPaidAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmount) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")

            For i = 0 To dgSales.Items.Count - 1
                dTotalAmt = dTotalAmt + Convert.ToDouble(dgSales.Items(i).Cells(13).Text)
            Next
            txtPaidAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
            txtAmount.Text = "" : ddlTaxType.SelectedIndex = 0
            ddlTaxRate.DataSource = dtDetails
            ddlTaxRate.DataBind()
            txtTaxAmount.Text = "" : txtTotal.Text = "" : txtTradeDiscountAmt.Text = "0" : txtTradeDis.Text = "0"
            txtTotalNetAmt.Text = ""
            dTotalAmt = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddTax_Click")
        End Try
    End Sub

    Public Sub SaveOtherDetails(ByVal iBillId As Integer, ByVal dTaxAmount As Double)
        Dim Arr() As String
        Dim iSubGL As Integer = 0
        Try
            Dim dtDGL As New DataTable
            Dim DVGLCODE As New DataView(dtCOA)
            iSubGL = objSales.GetCustomerGLID(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
            DVGLCODE.RowFilter = "Gl_id=" & iSubGL
            dtDGL = DVGLCODE.ToTable

            'Bill Amount
            objSales.iATD_ID = 0
            objSales.dATD_TransactionDate = Date.Today
            objSales.iATD_TrType = 9  'Sales Voucher
            objSales.iATD_BillID = iBillId
            objSales.iATD_PaymentType = 0
            objSales.iATD_Head = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_Head")
            objSales.iATD_GL = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_GL")
            objSales.iATD_SubGL = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_SubGL")
            objSales.iATD_DbOrCr = 2
            objSales.dATD_Debit = 0
            objSales.dATD_Credit = Convert.ToDouble(txtBillAmount.Text) - dTaxAmount
            objSales.iATD_CreatedBy = sSession.UserID
            objSales.sATD_Status = "A"
            objSales.iATD_YearID = sSession.YearID
            objSales.iATD_CompID = sSession.AccessCodeID
            objSales.sATD_Operation = "C"
            objSales.sATD_IPAddress = sSession.IPAddress
            Arr = objSales.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objSales)


            'Customer
            objSales.iATD_ID = 0
            objSales.dATD_TransactionDate = Date.Today
            objSales.iATD_TrType = 9  'Sales Voucher
            objSales.iATD_BillID = iBillId
            objSales.iATD_PaymentType = 0
            objSales.iATD_Head = dtDGL.Rows(0)("gl_AccHead")
            objSales.iATD_GL = dtDGL.Rows(0)("gl_Parent")
            objSales.iATD_SubGL = iSubGL
            objSales.iATD_DbOrCr = 2
            objSales.dATD_Debit = txtBillAmount.Text
            objSales.dATD_Credit = 0
            objSales.iATD_CreatedBy = sSession.UserID
            objSales.sATD_Status = "A"
            objSales.iATD_YearID = sSession.YearID
            objSales.iATD_CompID = sSession.AccessCodeID
            objSales.sATD_Operation = "C"
            objSales.sATD_IPAddress = sSession.IPAddress
            Arr = objSales.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objSales)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub SaveOtherDetails(ByVal iBillId As Integer, ByVal dTaxAmount As Double)
    '    Dim dtSales As New DataTable, dtDetails As New DataTable, dtGL As New DataTable
    '    Dim dRow As DataRow
    '    Dim sGL As String = "" : Dim sSubGL As String = ""
    '    Dim Arr() As String
    '    Try
    '        dtSales.Columns.Add("ID")
    '        dtSales.Columns.Add("HeadID")
    '        dtSales.Columns.Add("GLID")
    '        dtSales.Columns.Add("SubGLID")
    '        dtSales.Columns.Add("PaymentID")
    '        dtSales.Columns.Add("Debit")
    '        dtSales.Columns.Add("Credit")

    '        dtDetails = objSales.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID)

    '        dRow = dtSales.NewRow 'BIll Amount

    '        Dim DVGLCODE As New DataView(dtDetails)
    '        DVGLCODE.RowFilter = "Acc_Types= 'Customer' AND Acc_LedgerType='Sales'"
    '        dtGL = DVGLCODE.ToTable

    '        If dtGL.Rows.Count > 0 Then
    '            dRow("HeadID") = dtGL.Rows(0)("Acc_Head")
    '            dRow("GLID") = dtGL.Rows(0)("Acc_GL")
    '            dRow("SubGLID") = dtGL.Rows(0)("Acc_SubGL")
    '        End If
    '        dRow("Id") = 0
    '        dRow("PaymentID") = 0
    '        dRow("Debit") = txtBillAmount.Text.Trim
    '        dRow("Credit") = 0

    '        dtSales.Rows.Add(dRow)

    '        objSales.iATD_ID = 0
    '        objSales.dATD_TransactionDate = Date.Today
    '        objSales.iATD_TrType = 9  'Sales Voucher
    '        objSales.iATD_BillID = iBillId
    '        objSales.iATD_PaymentType = dtSales.Rows(0).Item(4).ToString()
    '        objSales.iATD_Head = dtSales.Rows(0).Item(1).ToString()
    '        objSales.iATD_GL = dtSales.Rows(0).Item(2).ToString()
    '        objSales.iATD_SubGL = dtSales.Rows(0).Item(3).ToString()
    '        objSales.iATD_DbOrCr = 2
    '        objSales.dATD_Debit = Convert.ToDouble(dtSales.Rows(0).Item(5).ToString()) - dTaxAmount
    '        objSales.dATD_Credit = 0
    '        objSales.iATD_CreatedBy = sSession.UserID
    '        objSales.sATD_Status = "A"
    '        objSales.iATD_YearID = sSession.YearID
    '        objSales.iATD_CompID = sSession.AccessCodeID
    '        objSales.sATD_Operation = "C"
    '        objSales.sATD_IPAddress = sSession.IPAddress
    '        Arr = objSales.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objSales)



    '        'Customer
    '        dRow = dtSales.NewRow

    '        DVGLCODE.RowFilter = "Acc_Types= 'Customer' AND Acc_LedgerType='Customer'"
    '        dtGL = DVGLCODE.ToTable

    '        If dtGL.Rows.Count > 0 Then
    '            dRow("HeadID") = dtGL.Rows(0)("Acc_Head")
    '            dRow("GLID") = dtGL.Rows(0)("Acc_GL")
    '            dRow("SubGLID") = dtGL.Rows(0)("Acc_SubGL")
    '        End If
    '        dRow("Id") = 0
    '        dRow("PaymentID") = 0
    '        dRow("Debit") = 0.0
    '        dRow("Credit") = txtBillAmount.Text.Trim

    '        dtSales.Rows.Add(dRow)


    '        objSales.iATD_ID = 0
    '        objSales.dATD_TransactionDate = Date.Today
    '        objSales.iATD_TrType = 9  'Sales Voucher
    '        objSales.iATD_BillID = iBillId
    '        objSales.iATD_PaymentType = dtSales.Rows(1).Item(4).ToString()
    '        objSales.iATD_Head = dtSales.Rows(1).Item(1).ToString()
    '        objSales.iATD_GL = dtSales.Rows(1).Item(2).ToString()
    '        objSales.iATD_SubGL = dtSales.Rows(1).Item(3).ToString()
    '        objSales.iATD_DbOrCr = 2
    '        objSales.dATD_Debit = 0
    '        objSales.dATD_Credit = dtSales.Rows(1).Item(6).ToString()
    '        objSales.iATD_CreatedBy = sSession.UserID
    '        objSales.sATD_Status = "A"
    '        objSales.iATD_YearID = sSession.YearID
    '        objSales.iATD_CompID = sSession.AccessCodeID
    '        objSales.sATD_Operation = "C"
    '        objSales.sATD_IPAddress = sSession.IPAddress
    '        Arr = objSales.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objSales)

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    'Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
    '    Dim Arr() As String
    '    Dim i As Integer = 0, iRet As Integer = 0, iBillID As Integer = 0
    '    Dim dTotalAmount As Double = 0 : Dim iFlag As Integer = 0
    '    Dim dTaxAmount As Double = 0
    '    Dim dDate, dSDate As Date : Dim m As Integer

    '    Try
    '        'Dim result As New DialogResult
    '        lblError.Text = ""
    '        If ddlAccBrnch.SelectedIndex = 0 Then
    '            lblError.Text = "Select Branch."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
    '            Exit Sub
    '        End If

    '        'Cheque Date Comparision'
    '        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m < 0 Then
    '            lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
    '            lblPaymentValidataionMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtBillDate.Focus()
    '            Exit Sub
    '        End If

    '        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m > 0 Then
    '            lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
    '            lblPaymentValidataionMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtBillDate.Focus()
    '            Exit Sub
    '        End If
    '        'Cheque Date Comparision'

    '        If dgSales.Items.Count = 0 Then
    '            lblError.Text = "Add amount"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add amount','', 'success');", True)
    '            Exit Sub
    '        End If

    '        For i = 0 To dgSales.Items.Count - 1
    '            dTotalAmount = dTotalAmount + Convert.ToDouble(dgSales.Items(i).Cells(13).Text)
    '        Next

    '        If txtBillAmount.Text = "0" Or txtBillAmount.Text = "" Then
    '            lblError.Text = "Enter Bill Amount"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Bill Amount','', 'success');", True)
    '            Exit Sub
    '        End If

    '        If Math.Round(Convert.ToDouble(txtBillAmount.Text)) = Math.Round(Convert.ToDouble(dTotalAmount)) = False Then
    '            ' lblError.Text = "Amount not matched with bill amount"
    '            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not matched with bill amount','', 'success');", True)
    '            'Exit Sub
    '            iFlag = 1
    '            'result = MessageBox.Show("Amount not matched with bill amount.Do you want to Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    '            'If result = DialogResult.Yes Then
    '            '    GoTo SaveResult
    '            'ElseIf result = DialogResult.No Then
    '            '    Exit Sub
    '            'End If
    '        End If

    '        'SaveResult:
    '        objSales.iAcc_Sales_ID = 0
    '        objSales.sAcc_Sales_TransactionNo = txtTransactionNo.Text
    '        objSales.iAcc_Sales_Party = ddlParty.SelectedValue
    '        objSales.sAcc_Sales_BillNo = txtBillNo.Text
    '        objSales.dAcc_Sales_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        'objSales.dAcc_Sales_ReceiptDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objSales.dAcc_Sales_ReceiptDate = "01/01/1900"
    '        objSales.dAcc_Sales_BillAmount = txtBillAmount.Text
    '        objSales.iAcc_Sales_CreatedBy = sSession.UserID
    '        objSales.iAcc_Sales_Year = sSession.YearID
    '        objSales.iAcc_Sales_CompID = sSession.AccessCodeID
    '        objSales.sAcc_Sales_Status = "C"
    '        objSales.sAcc_Sales_DelFlag = "W"
    '        objSales.sAcc_Sales_Operation = "C"
    '        objSales.sAcc_Sales_IPAddress = sSession.IPAddress
    '        objSales.iAcc_Sales_MisMatchFlag = iFlag
    '        objSales.sAcc_Sales_PaymentStatus = "N"  'Nil
    '        If txtOtherCharge.Text = "" Then
    '            objSales.dAcc_Sales_OtherCharges = "0"
    '        Else
    '            objSales.dAcc_Sales_OtherCharges = txtOtherCharge.Text
    '        End If

    '        objSales.iACC_Sales_ZoneID = ddlAccZone.SelectedValue
    '        objSales.iACC_Sales_RegionID = ddlAccRgn.SelectedValue
    '        objSales.iACC_Sales_AreaID = ddlAccArea.SelectedValue
    '        objSales.iACC_Sales_BranchID = ddlAccBrnch.SelectedValue

    '        Arr = objSales.SaveSalesVoucher(sSession.AccessCode, sSession.AccessCodeID, objSales)
    '        iBillID = Arr(1)

    '        For i = 0 To dgSales.Items.Count - 1
    '            objSales.iATD_ID = 0
    '            objSales.dATD_TransactionDate = Date.Today
    '            objSales.iATD_TrType = 9  'Sales Voucher
    '            objSales.iATD_BillID = iBillID
    '            objSales.iATD_PaymentType = dgSales.Items(i).Cells(4).Text
    '            objSales.iATD_Head = dgSales.Items(i).Cells(1).Text
    '            objSales.iATD_GL = dgSales.Items(i).Cells(2).Text
    '            objSales.iATD_SubGL = dgSales.Items(i).Cells(3).Text
    '            objSales.iATD_DbOrCr = 2

    '            If dgSales.Items(i).Cells(12).Text <> "" Then
    '                dTaxAmount = dTaxAmount + dgSales.Items(i).Cells(12).Text
    '            End If

    '            objSales.dATD_Debit = 0
    '            objSales.dATD_Credit = dgSales.Items(i).Cells(12).Text
    '            objSales.iATD_CreatedBy = sSession.UserID
    '            objSales.sATD_Status = "A"
    '            objSales.iATD_YearID = sSession.YearID
    '            objSales.iATD_CompID = sSession.AccessCodeID
    '            objSales.sATD_Operation = "C"
    '            objSales.sATD_IPAddress = sSession.IPAddress
    '            Arr = objSales.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objSales)
    '        Next

    '        SaveSalesDetails(iBillID)
    '        SaveOtherDetails(iBillID, dTaxAmount)

    '        SaveCharges(iBillID)

    '        LoadExistingSalesVoucher()
    '        ddlExistSales.SelectedValue = iBillID
    '        ddlExistSales_SelectedIndexChanged(sender, e)
    '        imgbtnSave.Visible = False : imgbtnUpdate.Visible = True

    '        If Arr(0) = "2" Then
    '            lblError.Text = "Successfully Updated."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
    '        ElseIf Arr(0) = "3" Then
    '            lblError.Text = "Successfully Saved & Waiting for Approval."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
    '        End If
    '        lblStatus.Text = "Waiting for Approval"
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
    '    End Try
    'End Sub
    Private Sub SaveSalesDetails(ByVal iBillID As Integer)
        Dim Arr() As String
        Try
            For i = 0 To dgSales.Items.Count - 1
                objSales.iAcc_SMD_ID = 0
                objSales.iAcc_SMD_MasterID = iBillID
                objSales.iAcc_SMD_Head = dgSales.Items(i).Cells(1).Text
                objSales.iAcc_SMD_GL = dgSales.Items(i).Cells(2).Text
                objSales.iAcc_SMD_SubGL = dgSales.Items(i).Cells(3).Text
                objSales.dAcc_SMD_Amount = dgSales.Items(i).Cells(6).Text
                objSales.iAcc_SMD_TaxType = dgSales.Items(i).Cells(4).Text
                objSales.iAcc_SMD_TaxRate = dgSales.Items(i).Cells(5).Text
                objSales.dAcc_SMD_TaxAmount = dgSales.Items(i).Cells(12).Text
                objSales.dAcc_SMD_TotalAmount = dgSales.Items(i).Cells(13).Text
                objSales.dAcc_SMD_PendingAmount = dgSales.Items(i).Cells(13).Text
                objSales.sAcc_SMD_RoundOff = dgSales.Items(i).Cells(14).Text
                objSales.sAcc_SMD_Status = "A"
                objSales.iAcc_SMD_CompID = sSession.AccessCodeID
                objSales.iAcc_SMD_YearID = sSession.YearID

                objSales.dAcc_SMD_TradeDis = dgSales.Items(i).Cells(7).Text
                objSales.dAcc_SMD_TradeDisAmt = dgSales.Items(i).Cells(8).Text
                objSales.dAcc_SMD_NetAmount = dgSales.Items(i).Cells(9).Text

                Arr = objSales.SaveSalesDetails(sSession.AccessCode, sSession.AccessCodeID, objSales)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object
        Try
            lblError.Text = ""
            If lblStatus.Text = "Waiting for Submission" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf lblStatus.Text = "De-Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf lblStatus.Text = "Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Not Started." Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Submitted" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            End If
            Response.Redirect(String.Format("~/Accounts/SalesTransactionDashboard.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblStatus.Text = ""
            Session("dtSales") = Nothing
            txtBillAmount.Text = "0" : txtPaidAmount.Text = "0" : txtOtherCharge.Text = "0"
            txtTransactionNo.Text = objSales.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlExistSales.SelectedIndex = 0 : lblStatus.Text = "Not Started"
            ddlParty.SelectedIndex = 0 : txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = ""

            Session("ChargesMaster") = Nothing
            GvCharge.DataSource = Nothing
            GvCharge.DataBind()

            ClearAll()
            imgbtnSave.ImageUrl = "~/Images/Save24.png"
            imgbtnSave.ToolTip = "Save" : imgbtnSave.Enabled = True
            txtTransactionNo.Text = objSales.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub ClearAll()
        Try
            ddlAccZone.SelectedIndex = 0 : ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            txtTransactionNo.Text = "" : txtBillDate.Text = "" : ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.SelectedIndex = 0
            chkboxFrom.Checked = False : chkboxTo.Checked = False
            txtBillingAddress.Text = "" : txtDeliveryFromAddress.Text = "" : txtCompanyAddress.Text = "" : txtDeleveryAddress.Text = ""
            txtBillingGSTNRegNo.Text = "" : txtDeliveryFromGSTNRegNo.Text = "" : txtCompanyGSTNRegNo.Text = "" : txtDeliveryGSTNRegNo.Text = ""
            ddlBranch.SelectedIndex = 0 : txtBillDate.Text = "" : txtReceiptDate.Text = ""

            txtHSNCode.Text = "" : chkCategory.ClearSelection() : ddlUnit.Items.Clear() : txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtTotalAmount.Text = ""

            dgItems.DataSource = Nothing
            dgItems.DataBind()

            dgJEDetails.DataSource = Nothing
            dgJEDetails.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dgSales_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgSales.ItemDataBound
        Dim imgbtnDelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDelete = CType(e.Item.FindControl("imgbtnDelete"), ImageButton)
                imgbtnDelete.ImageUrl = "~/Images/Trash16.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSales_ItemDataBound")
        End Try
    End Sub
    Private Sub dgSales_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgSales.ItemCommand
        Dim dt As New DataTable
        Dim dTotalAmt As Double
        Try
            If e.CommandName = "Delete" Then
                dt = Session("dtSales")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                Session("dtSales") = dt
            End If
            dgSales.DataSource = dt
            dgSales.DataBind()

            For i = 0 To dgSales.Items.Count - 1
                dTotalAmt = dTotalAmt + Convert.ToDouble(dgSales.Items(i).Cells(13).Text)
            Next
            txtPaidAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSales_ItemCommand")
        End Try
    End Sub
    Private Sub txtOtherCharge_TextChanged(sender As Object, e As EventArgs) Handles txtOtherCharge.TextChanged
        Dim dAmount As Double = 0
        Try
            If txtPaidAmount.Text = "" Then
                dAmount = 0
            Else
                dAmount = Convert.ToDouble(txtPaidAmount.Text)
            End If

            If IsNumeric(txtOtherCharge.Text) = True Then
                dAmount = dAmount + Convert.ToDouble(txtOtherCharge.Text)
            End If

            txtPaidAmount.Text = Convert.ToDecimal(dAmount).ToString("#,##0.00")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtOtherCharge_TextChanged")
        End Try
    End Sub
    'Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Try
    '        If ddlParty.SelectedIndex > 0 Then
    '            DivAccountsDetails.Visible = True
    '            GvAccountDetails.DataSource = objSales.GetAccountsDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue, dtCOA)
    '            GvAccountDetails.DataBind()
    '        Else
    '            DivAccountsDetails.Visible = False
    '            GvAccountDetails.DataSource = dt
    '            GvAccountDetails.DataBind()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
    '    End Try
    'End Sub
    Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
        Dim dt, dtTable As New DataTable
        Dim dTotalCharges As Double
        Try
            lblError.Text = ""
            If lblStatus.Text = "Submitted" Then
                lblError.Text = "This transaction has been submitted, Charges can not be add."
                Exit Sub
            ElseIf lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved, Charges can not be add."
                Exit Sub
            End If

            If ddlChargeType.SelectedIndex > 0 Then
                If txtShippingRate.Text <> "" Then
                    dt = AddCharges()
                    dtTable = objSales.RemoveDublicate(dt)
                    GvCharge.DataSource = dtTable
                    GvCharge.DataBind()

                    ddlChargeType.SelectedIndex = 0 : txtShippingRate.Text = ""

                    'grdDispatchDetails.DataSource = objDispatch.BindAllocatedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
                    'grdDispatchDetails.DataBind()
                Else
                    lblError.Text = "Enter Amount charged."
                End If
            Else
                lblError.Text = "Select Charge Type."
            End If

            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1
                    dTotalCharges = dTotalCharges + Convert.ToDouble(GvCharge.Items(i).Cells(2).Text)
                Next
            End If
            txtOtherCharge.Text = dTotalCharges
            'txtPaidAmount.Text = dTotalCharges
            dTotalCharges = 0

            If ddlTaxRate.SelectedIndex > 0 Then
                ddlTaxRate_SelectedIndexChanged(sender, e)
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
                dt1.Columns.Add("ChargeType")
                dt1.Columns.Add("ChargeAmount")
            End If

            dr = dt1.NewRow
            dr("ChargeID") = ddlChargeType.SelectedValue
            dr("ChargeType") = Trim(ddlChargeType.SelectedItem.Text)
            dr("ChargeAmount") = Trim(txtShippingRate.Text)
            dt1.Rows.Add(dr)

            Session("ChargesMaster") = dt1
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveCharges(ByVal iTRID As Integer)
        Dim Arr() As String
        Try
            'Deleting charges Everytime & Saving'
            objSales.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTRID, 9)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1
                    objSales.C_ID = 0
                    objSales.C_TRID = iTRID
                    objSales.C_TRType = 9   'Sales Voucher
                    objSales.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objSales.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objSales.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objSales.C_DelFlag = "W"
                    objSales.C_Status = "C"
                    objSales.C_CompID = sSession.AccessCodeID
                    objSales.C_YearID = sSession.YearID
                    objSales.C_CreatedBy = sSession.UserID
                    objSales.C_CreatedOn = System.DateTime.Now
                    objSales.C_Operation = "C"
                    objSales.C_IPAddress = sSession.IPAddress

                    Arr = objSales.SaveAccCharges(sSession.AccessCode, objSales)
                Next
            End If
            'Charges Saving'
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GvCharge_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles GvCharge.ItemCommand
        Dim dt As New DataTable
        Dim dTotalCharges As Double
        Try
            If e.CommandName = "Delete" Then
                dt = Session("ChargesMaster")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                Session("ChargesMaster") = dt
            End If
            GvCharge.DataSource = dt
            GvCharge.DataBind()

            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1
                    dTotalCharges = dTotalCharges + Convert.ToDouble(GvCharge.Items(i).Cells(2).Text)
                Next
            End If
            txtOtherCharge.Text = dTotalCharges
            'txtPaidAmount.Text = dTotalCharges
            dTotalCharges = 0

            If ddlTaxRate.SelectedIndex > 0 Then
                ddlTaxRate_SelectedIndexChanged(source, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub
    Private Sub txtTradeDiscount_TextChanged(sender As Object, e As EventArgs) Handles txtTradeDiscountAmt.TextChanged
        Dim dPerValue As Double
        Try
            If ddlTaxRate.SelectedIndex > 0 Then
                If ddlTaxType.SelectedIndex = 1 Then  'VAT
                    dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                    dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                    txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                    txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")

                ElseIf ddlTaxType.SelectedIndex = 2 Then  'CST

                    dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                    dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                    txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                    txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")

                ElseIf ddlTaxType.SelectedIndex = 3 Then  'Excise

                    dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                    dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                    txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                    txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtTradeDiscount_TextChanged")
        End Try
    End Sub
    Private Sub txtTradeDis_TextChanged(sender As Object, e As EventArgs) Handles txtTradeDis.TextChanged
        Dim dPerValue As Double
        Try
            If ddlTaxRate.SelectedIndex > 0 Then
                If ddlTaxType.SelectedIndex = 1 Then  'VAT
                    dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                    dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                    txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                    txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")

                ElseIf ddlTaxType.SelectedIndex = 2 Then  'CST

                    dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                    dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                    txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                    txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")

                ElseIf ddlTaxType.SelectedIndex = 3 Then  'Excise

                    dPerValue = objSales.GetPerValue(sSession.AccessCode, sSession.AccessCodeID, ddlTaxRate.SelectedValue)
                    dVATAmt = (((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)) * (dPerValue + 100)) / 100
                    txtTaxAmount.Text = Convert.ToDecimal((dVATAmt - ((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)))).ToString("#,##0.00")
                    txtTotal.Text = Convert.ToDecimal(Convert.ToDouble(txtTaxAmount.Text) + (Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(txtTradeDiscountAmt.Text)) + Convert.ToDouble(txtOtherCharge.Text)).ToString("#,##0.00")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtTradeDis_TextChanged")
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objSales.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindBranch()
        Try
            ddlBranch.DataSource = objSales.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlCompanyType.DataSource = objSales.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
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
            dt = objSales.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategory.DataSource = dt
            ddlGSTCategory.DataTextField = "GC_GSTCategory"
            ddlGSTCategory.DataValueField = "GC_Id"
            ddlGSTCategory.DataBind()
            ddlGSTCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
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
    Private Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Dim dt As New DataTable
        Dim description As String
        Try
            txtCompanyAddress.Text = "" : txtCompanyGSTNRegNo.Text = "" : txtDeliveryFromAddress.Text = "" : txtDeliveryFromGSTNRegNo.Text = ""
            If ddlBranch.SelectedIndex > 0 Then
                dt = objSales.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, ddlBranch.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUSTB_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUSTB_GSTNRegNo")

                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUSTB_CompanyType")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUSTB_GSTNCategory")

                    description = objSales.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryFromGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNo.Enabled = True
                    End If

                    txtDeliveryFromAddress.Text = txtCompanyAddress.Text
                    txtDeliveryFromGSTNRegNo.Text = txtCompanyGSTNRegNo.Text

                End If
            Else
                dt = objSales.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TaxPayableCategory")
                    description = objSales.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryFromGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNo.Enabled = True
                    End If
                    txtDeliveryFromAddress.Text = txtCompanyAddress.Text
                    txtDeliveryFromGSTNRegNo.Text = txtCompanyGSTNRegNo.Text

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged")
        End Try
    End Sub
    Public Function CheckSourceDestinationOfDispatch(ByVal sBillingAddress As String, ByVal sReceiveAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            sSource = sBillingAddress.Substring(0, 2)
            sDestination = sReceiveAddress.Substring(0, 2)

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
    Public Function GetSourceDestinationState(ByVal sBillingAddress As String, ByVal sReceiveAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            'sSource = sBillingAddress.Substring(0, 2)
            'sDestination = sReceiveAddress.Substring(0, 2)

            'If sSource = sDestination Then
            '    GetSourceDestinationState = objInvoiceForm.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
            'Else
            '    GetSourceDestinationState = objInvoiceForm.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
            'End If
            'Return GetSourceDestinationState
            If sBillingAddress <> "" And sReceiveAddress = "" Then
                sSource = sBillingAddress.Substring(0, 2)
                GetSourceDestinationState = objSales.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sReceiveAddress <> "" Then
                sDestination = sReceiveAddress.Substring(0, 2)
                GetSourceDestinationState = objSales.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sReceiveAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sReceiveAddress.Substring(0, 2)
                If sSource = sDestination Then
                    GetSourceDestinationState = objSales.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    GetSourceDestinationState = objSales.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return GetSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSourceDestinationState")
        End Try
    End Function
    Private Sub dgItems_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgItems.ItemCommand
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Dim iMasterID As Integer
        Try
            lblError.Text = ""
            If e.CommandName = "Delete" Then
                If ddlExistSales.SelectedIndex > 0 Then
                    iMasterID = ddlExistSales.SelectedValue
                Else
                    iMasterID = 0
                End If

                If iMasterID > 0 Then
                    sStatus = objSales.GetStatus(sSession.AccessCode, sSession.AccessCodeID, iMasterID, sSession.YearID)
                    If sStatus = "S" Then
                        lblError.Text = "This is submitted,goods can not be deleted."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This is submitted,goods can not be deleted.','', 'success');", True)
                        Exit Sub
                    ElseIf sStatus = "A" Then
                        lblError.Text = "This is Approved,goods can not be deleted."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This is Approved,goods can not be deleted.','', 'success');", True)
                        Exit Sub
                    End If
                    dt = Session("dtSales")
                    dt.Rows.Item(e.Item.ItemIndex).Delete()
                    Session("dtSales") = dt
                Else
                    dt = Session("dtSales")
                    dt.Rows.Item(e.Item.ItemIndex).Delete()
                    Session("dtSales") = dt
                End If
            End If

            dgItems.DataSource = dt
            dgItems.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgItems_ItemCommand")
        End Try
    End Sub
    Private Sub dgItems_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgItems.ItemDataBound
        Dim dMRP, dQty, dRateAmt, dDis, dDisAmt, dCharge, dTotal, dGST, dGSTAmt, dNetAmt, dItemTotal As Double
        Dim dtPurchase As New DataTable
        Dim sStatus As String = ""
        Dim dtData As New DataTable
        Dim iCommodity, iGoodsID, iUnitID As Integer
        Try
            lblError.Text = ""
            dtPurchase = Session("dtSales")
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                If ddlExistSales.SelectedIndex > 0 Then
                    iCommodity = e.Item.Cells(0).Text
                    iGoodsID = e.Item.Cells(1).Text
                    iUnitID = e.Item.Cells(2).Text

                    dtData = objSales.BindSavedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistSales.SelectedValue, iCommodity, iGoodsID)
                    If dtData.Rows.Count > 0 Then
                        For m = 0 To dtData.Rows.Count - 1
                            e.Item.Cells(3).Text = dtData.Rows(m)("PD_HSNCode")
                            e.Item.Cells(4).Text = objDb.SQLGetDescription(sSession.AccessCode, "Select INV_Description From Inventory_master Where INV_ID=" & iCommodity & " And INV_Parent=0 And INV_CompID=" & sSession.AccessCodeID & " ")
                            e.Item.Cells(5).Text = objDb.SQLGetDescription(sSession.AccessCode, "Select INV_Description From Inventory_master Where INV_ID=" & iGoodsID & " And INV_Parent=" & iCommodity & " And INV_CompID=" & sSession.AccessCodeID & " ")
                            e.Item.Cells(6).Text = objDb.SQLGetDescription(sSession.AccessCode, "Select Mas_Desc From Acc_General_master Where Mas_ID=" & iUnitID & " And MAS_CompID=" & sSession.AccessCodeID & " And MAS_DelFlag='A' And MAS_Master in(Select MAS_ID From Acc_Master_Type where Mas_Type='Unit of Measurement') ")
                            e.Item.Cells(7).Text = dtData.Rows(m)("PD_Rate")
                            e.Item.Cells(8).Text = dtData.Rows(m)("PD_Quantity")
                            e.Item.Cells(9).Text = dtData.Rows(m)("PD_RateAmount")
                            e.Item.Cells(10).Text = dtData.Rows(m)("PD_Discount")
                            e.Item.Cells(11).Text = dtData.Rows(m)("PD_DiscountAmount")
                            e.Item.Cells(12).Text = dtData.Rows(m)("PD_ChargePerItem")
                            e.Item.Cells(13).Text = dtData.Rows(m)("PD_Amount")
                            e.Item.Cells(14).Text = dtData.Rows(m)("PD_GSTRate")
                            e.Item.Cells(15).Text = dtData.Rows(m)("PD_GSTAmount")
                            e.Item.Cells(16).Text = dtData.Rows(m)("PD_FinalTotal")
                        Next
                    End If

                    sStatus = objSales.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistSales.SelectedValue, sSession.YearID)
                    If sStatus = "S" Or sStatus = "A" Then
                        Dim imgbtnDelete As ImageButton = CType(e.Item.FindControl("imgbtnDelete"), ImageButton)
                        imgbtnDelete.Enabled = False
                    Else
                        If txtOtherCharge.Text <> "" Then
                            If dtData.Rows.Count > 0 Then
                                For i = 0 To dtData.Rows.Count - 1
                                    dItemTotal = dItemTotal + dtData.Rows(i)("PD_RateAmount")
                                Next
                                dCharge = (e.Item.Cells(9).Text * txtOtherCharge.Text) / (dItemTotal)
                                e.Item.Cells(12).Text = String.Format("{0:0.00}", Convert.ToDecimal(dCharge))

                                e.Item.Cells(13).Text = String.Format("{0:0.00}", Convert.ToDecimal(((e.Item.Cells(9).Text - e.Item.Cells(11).Text) + dCharge)))
                                e.Item.Cells(15).Text = String.Format("{0:0.00}", Convert.ToDecimal(((e.Item.Cells(13).Text) * e.Item.Cells(14).Text) / 100))
                                e.Item.Cells(16).Text = String.Format("{0:0.00}", Convert.ToDecimal(Convert.ToDecimal(e.Item.Cells(13).Text) + Convert.ToDecimal(e.Item.Cells(15).Text)))
                            End If
                        End If

                    End If

                Else

                    dMRP = e.Item.Cells(7).Text
                    dQty = e.Item.Cells(8).Text
                    dRateAmt = e.Item.Cells(9).Text
                    dDis = e.Item.Cells(10).Text
                    dDisAmt = e.Item.Cells(11).Text
                    'dCharge = e.Item.Cells(9).Text
                    'dTotal = e.Item.Cells(10).Text
                    dGST = e.Item.Cells(14).Text
                    'dGSTAmt = e.Item.Cells(12).Text
                    'dNetAmt = e.Item.Cells(13).Text

                    If txtOtherCharge.Text <> "" Then
                        If dtPurchase.Rows.Count > 0 Then
                            For i = 0 To dtPurchase.Rows.Count - 1
                                dItemTotal = dItemTotal + dtPurchase.Rows(i)("RateAMt")
                            Next
                            dCharge = (dRateAmt * txtOtherCharge.Text) / (dItemTotal)
                            e.Item.Cells(12).Text = String.Format("{0:0.00}", Convert.ToDecimal(dCharge))

                            e.Item.Cells(13).Text = String.Format("{0:0.00}", Convert.ToDecimal(((dRateAmt - dDisAmt) + dCharge)))
                            e.Item.Cells(15).Text = String.Format("{0:0.00}", Convert.ToDecimal(((e.Item.Cells(13).Text) * dGST) / 100))
                            e.Item.Cells(16).Text = String.Format("{0:0.00}", Convert.ToDecimal(Convert.ToDecimal(e.Item.Cells(13).Text) + Convert.ToDecimal(e.Item.Cells(15).Text)))
                        End If
                    End If
                End If

                dgItems.Columns(17).Visible = False
                If sSTSave = "YES" Then
                    dgItems.Columns(17).Visible = True
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgItems_ItemDataBound")
        End Try
    End Sub
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim dttab As New DataTable
        Dim dTotalAmt As Double
        Try
            lblError.Text = ""
            If lblStatus.Text = "Submitted" Then
                lblError.Text = "This transaction has been submitted, Charges can not be add."
                Exit Sub
            ElseIf lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved, Charges can not be add."
                Exit Sub
            End If

            dttab = Session("dtSales")
            dgItems.DataSource = dttab
            dgItems.DataBind()

            For i = 0 To dgItems.Items.Count - 1
                dTotalAmt = dTotalAmt + Convert.ToDouble(dgItems.Items(i).Cells(16).Text)
            Next
            txtBillAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCalculate_Click")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim i As Integer = 0, iRet As Integer = 0, iBillID As Integer = 0
        Dim dTotalAmount As Double = 0 : Dim iFlag As Integer = 0
        Dim dTaxAmount As Double = 0
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim objSale As clsSalesTranscation.Sales

        Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Dim sName As String = ""
        Try

            If lblStatus.Text = "Submitted" Then
                lblError.Text = "This transaction has been submitted, it can not be Updated again."
                Exit Sub
            ElseIf lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved, it can not be Updated again."
                Exit Sub
            End If

            'Dim result As New DialogResult
            lblError.Text = ""
            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
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
            dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblPaymentValidataionMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtBillDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblPaymentValidataionMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtBillDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            For i = 0 To dgItems.Items.Count - 1
                dTotalAmount = dTotalAmount + Convert.ToDouble(dgItems.Items(i).Cells(16).Text)
            Next

            If txtBillAmount.Text = "0" Or txtBillAmount.Text = "" Then
                lblError.Text = "Enter Bill Amount"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Bill Amount','', 'success');", True)
                Exit Sub
            End If

            If Math.Round(Convert.ToDouble(txtBillAmount.Text)) = Math.Round(Convert.ToDouble(dTotalAmount)) = False Then
                ' lblError.Text = "Amount not matched with bill amount"
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not matched with bill amount','', 'success');", True)
                'Exit Sub
                iFlag = 1
                'result = MessageBox.Show("Amount not matched with bill amount.Do you want to Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                'If result = DialogResult.Yes Then
                '    GoTo SaveResult
                'ElseIf result = DialogResult.No Then
                '    Exit Sub
                'End If
            End If

            If dgItems.Items.Count > 0 Then
            Else
                lblError.Text = "Add the goods details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add the goods details','', 'success');", True)
                Exit Sub
            End If

            'SaveResult:
            If ddlExistSales.SelectedIndex > 0 Then
                objSales.iAcc_Sales_ID = ddlExistSales.SelectedValue
            Else
                objSales.iAcc_Sales_ID = 0
            End If
            objSales.sAcc_Sales_TransactionNo = txtTransactionNo.Text
            objSales.iAcc_Sales_Party = ddlParty.SelectedValue
            objSales.sAcc_Sales_BillNo = txtBillNo.Text
            objSales.dAcc_Sales_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'objSales.dAcc_Sales_ReceiptDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSales.dAcc_Sales_ReceiptDate = "01/01/1900"
            objSales.dAcc_Sales_BillAmount = txtBillAmount.Text
            objSales.iAcc_Sales_CreatedBy = sSession.UserID
            objSales.iAcc_Sales_Year = sSession.YearID
            objSales.iAcc_Sales_CompID = sSession.AccessCodeID
            objSales.sAcc_Sales_Status = "C"
            objSales.sAcc_Sales_DelFlag = "W"
            objSales.sAcc_Sales_Operation = "C"
            objSales.sAcc_Sales_IPAddress = sSession.IPAddress
            objSales.iAcc_Sales_MisMatchFlag = iFlag
            objSales.sAcc_Sales_PaymentStatus = "N"  'Nil
            If txtOtherCharge.Text = "" Then
                objSales.dAcc_Sales_OtherCharges = "0"
            Else
                objSales.dAcc_Sales_OtherCharges = txtOtherCharge.Text
            End If

            objSales.iACC_Sales_ZoneID = ddlAccZone.SelectedValue
            objSales.iACC_Sales_RegionID = ddlAccRgn.SelectedValue
            objSales.iACC_Sales_AreaID = ddlAccArea.SelectedValue
            'objSales.iACC_Sales_BranchID = ddlAccBrnch.SelectedValue
            If ddlAccBrnch.SelectedIndex > 0 Then
                objSales.iACC_Sales_BranchID = ddlAccBrnch.SelectedValue
            Else
                objSales.iACC_Sales_BranchID = 0
            End If

            If txtCompanyAddress.Text <> "" Then
                objSales.Acc_Sales_CompanyAddress = txtCompanyAddress.Text
            Else
                objSales.Acc_Sales_CompanyAddress = ""
            End If

            If txtBillingAddress.Text <> "" Then
                objSales.Acc_Sales_BillingAddress = txtBillingAddress.Text
            Else
                objSales.Acc_Sales_BillingAddress = ""
            End If

            If txtDeliveryFromAddress.Text <> "" Then
                objSales.Acc_Sales_DeliveryFrom = txtDeliveryFromAddress.Text
            Else
                objSales.Acc_Sales_DeliveryFrom = ""
            End If

            If txtDeleveryAddress.Text <> "" Then
                objSales.Acc_Sales_DeliveryAddress = txtDeleveryAddress.Text
            Else
                objSales.Acc_Sales_DeliveryAddress = ""
            End If

            If txtCompanyGSTNRegNo.Text <> "" Then
                objSales.Acc_Sales_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
            Else
                objSales.Acc_Sales_CompanyGSTNRegNo = ""
            End If

            If txtBillingGSTNRegNo.Text <> "" Then
                objSales.Acc_Sales_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
            Else
                objSales.Acc_Sales_BillingGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" Then
                objSales.Acc_Sales_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
            Else
                objSales.Acc_Sales_DeliveryFromGSTNRegNo = ""
            End If

            If txtDeliveryGSTNRegNo.Text <> "" Then
                objSales.Acc_Sales_DeliveryGSTNRegNo = txtDeliveryGSTNRegNo.Text
            Else
                objSales.Acc_Sales_DeliveryGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objSales.Acc_Sales_InvoiceStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                objSales.Acc_Sales_InvoiceStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objSales.Acc_Sales_InvoiceStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                objSales.Acc_Sales_InvoiceStatus = "Local"
            End If
            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
            '    objSales.Acc_Sales_InvoiceStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            'End If

            objSales.Acc_Sales_CompanyType = ddlCompanyType.SelectedValue
            objSales.Acc_Sales_GSTNCategory = ddlGSTCategory.SelectedValue

            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
            '    objSales.Acc_Sales_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            'End If
            If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objSales.Acc_Sales_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                objSales.Acc_Sales_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), (""))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objSales.Acc_Sales_State = GetSourceDestinationState((""), Trim(txtDeliveryGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                Dim ibranch As Integer
                ibranch = objSales.getBranchFromPO(sSession.AccessCode, sSession.AccessCodeID, txtTransactionNo.Text)
                If ibranch > 0 Then 'branch 
                    objSales.Acc_Sales_State = objSales.CheckDetailsofBranchState(sSession.AccessCode, sSession.AccessCodeID, txtTransactionNo.Text)
                    If objSales.Acc_Sales_State = "" Then
                        lblError.Text = "Update state in branch master"
                        lblPaymentValidataionMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in branch master.','', 'success');", True)
                        Exit Sub
                    End If
                Else 'Company
                    objSales.Acc_Sales_State = objSales.CheckDetailsofCompState(sSession.AccessCode, sSession.AccessCodeID)
                    If objSales.Acc_Sales_State = "" Then
                        lblError.Text = "Update state in company master"
                        lblPaymentValidataionMsg.Text = lblError.Text
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
            sName = "Sale Of Product " & objSales.Acc_Sales_State
            txtGLID.Text = objSales.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
            Else
                iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
            End If
            'Chart Of Accounts'

            Dim dtGSTRates As New DataTable
            dtGSTRates = objSales.BindGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'
            If dtGSTRates.Rows.Count > 0 Then
                For x = 0 To dtGSTRates.Rows.Count - 1

                    sName = "Local GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objSales.Acc_Sales_State & " Sale Account"
                    txtGLID.Text = objSales.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                    sName = "Inter State GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objSales.Acc_Sales_State & " Sale Account"
                    txtGLID.Text = objSales.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
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

                    sName = "OUTPUT SGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objSales.Acc_Sales_State & " Sale Account"
                    txtGLID.Text = objSales.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "OUTPUT CGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objSales.Acc_Sales_State & " Sale Account"
                    txtGLID.Text = objSales.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "OUTPUT IGST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objSales.Acc_Sales_State & " Sale Account"
                    txtGLID.Text = objSales.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                Next
            End If

            Arr = objSales.SaveSalesVoucher(sSession.AccessCode, sSession.AccessCodeID, objSales)
            iBillID = Arr(1)

            objSales.DeleteDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBillID)

            For i = 0 To dgItems.Items.Count - 1
                objSale.PD_MasterID = iBillID

                objSale.PD_Commodity = dgItems.Items(i).Cells(0).Text
                objSale.PD_Goods = dgItems.Items(i).Cells(1).Text
                objSale.PD_Unit = dgItems.Items(i).Cells(2).Text
                objSale.PD_HSNCode = dgItems.Items(i).Cells(3).Text

                objSale.PD_Rate = dgItems.Items(i).Cells(7).Text
                objSale.PD_Quantity = dgItems.Items(i).Cells(8).Text

                objSale.PD_RateAmount = dgItems.Items(i).Cells(9).Text

                If dgItems.Items(i).Cells(10).Text <> "" Then
                    objSale.PD_Discount = dgItems.Items(i).Cells(10).Text
                Else
                    objSale.PD_Discount = 0
                End If

                If dgItems.Items(i).Cells(11).Text <> "" Then
                    objSale.PD_DiscountAmount = dgItems.Items(i).Cells(11).Text
                Else
                    objSale.PD_DiscountAmount = 0
                End If

                objSale.PD_ChargePerItem = dgItems.Items(i).Cells(12).Text

                objSale.PD_Amount = dgItems.Items(i).Cells(13).Text

                objSale.PD_GSTRate = dgItems.Items(i).Cells(14).Text

                If dgItems.Items(i).Cells(15).Text <> "" Then
                    objSale.PD_GSTAmount = dgItems.Items(i).Cells(15).Text
                Else
                    objSale.PD_GSTAmount = 0
                End If

                If objSales.Acc_Sales_InvoiceStatus = "Local" Then
                    objSale.PD_SGST = objSale.PD_GSTRate / 2
                    objSale.PD_SGSTAmount = objSale.PD_GSTAmount / 2
                    objSale.PD_CGST = objSale.PD_GSTRate / 2
                    objSale.PD_CGSTAmount = objSale.PD_GSTAmount / 2
                    objSale.PD_IGST = 0
                    objSale.PD_IGSTAmount = 0
                ElseIf objSales.Acc_Sales_InvoiceStatus = "Inter State" Then
                    objSale.PD_SGST = 0
                    objSale.PD_SGSTAmount = 0
                    objSale.PD_CGST = 0
                    objSale.PD_CGSTAmount = 0
                    objSale.PD_IGST = objSale.PD_GSTRate
                    objSale.PD_IGSTAmount = objSale.PD_GSTAmount
                End If

                'If UCase(ddlGSTCategory.SelectedItem.Text) = "UNRIGISTERED DEALER" Then
                '    Dim URD_GSTRate, URD_GSTAmt As Double

                '    'URD_GSTRate = txtGST.Text
                '    URD_GSTRate = objSales.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, objSale.PD_Commodity, objSale.PD_Goods)
                '    URD_GSTAmt = (((objSale.PD_RateAmount - objSale.PD_DiscountAmount) + objSale.PD_ChargePerItem) * URD_GSTRate) / 100

                '    objSale.PD_SGST = URD_GSTRate / 2
                '    objSale.PD_SGSTAmount = URD_GSTAmt / 2
                '    objSale.PD_CGST = URD_GSTRate / 2
                '    objSale.PD_CGSTAmount = URD_GSTAmt / 2
                '    objSale.PD_IGST = 0
                '    objSale.PD_IGSTAmount = 0
                'End If

                If dgItems.Items(i).Cells(16).Text <> "" Then
                    objSale.PD_FinalTotal = dgItems.Items(i).Cells(16).Text
                Else
                    objSale.PD_FinalTotal = 0
                End If

                objSale.PD_Status = "W"
                objSale.PD_CompID = sSession.AccessCodeID
                objSale.PD_CreatedBy = sSession.UserID
                objSale.PD_CreatedOn = System.DateTime.Now
                objSale.PD_Operation = "C"
                objSale.PD_IPAddress = sSession.IPAddress
                objSale.PD_YearID = sSession.YearID

                Arr = objSales.SaveAcceptedDetails(sSession.AccessCode, objSale)
            Next

            SaveCharges(iBillID)

            LoadExistingSalesVoucher()
            ddlExistSales.SelectedValue = iBillID
            ddlExistSales_SelectedIndexChanged(sender, e)

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Submission."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Submission.','', 'success');", True)
            End If
            lblStatus.Text = "Waiting for Submission"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
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
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dtCustomer As New DataTable
        Dim description As String
        Try
            If ddlParty.SelectedIndex > 0 Then
                dtCustomer = objSales.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                If dtCustomer.Rows.Count > 0 Then
                    txtBillingAddress.Text = dtCustomer.Rows(0)("BM_Address")
                    txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("BM_GSTNRegNo")

                    txtDeleveryAddress.Text = dtCustomer.Rows(0)("BM_Address")
                    txtDeliveryGSTNRegNo.Text = dtCustomer.Rows(0)("BM_GSTNRegNo")

                    description = objSales.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, dtCustomer.Rows(0)("BM_GSTNCategory"))
                    If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryGSTNRegNo.Enabled = True
                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim iMasterID As Integer
        Dim dt As New DataTable
        Try
            If lblStatus.Text = "Submitted" Then
                lblError.Text = "This transaction has been submitted, it can not be submitted again."
                Exit Sub
            ElseIf lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved, it can not be submitted again."
                Exit Sub
            End If
            If ddlExistSales.SelectedIndex > 0 Then
                iMasterID = ddlExistSales.SelectedValue
            Else
                iMasterID = 0
            End If

            Dim lblCommodityID, lblItemID, lblHistoryID, lblOrderedQty As New Label
            If iMasterID > 0 Then
                objSales.ApproveTransaction(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
                dt = objSales.GetItemDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        lblCommodityID.Text = dt.Rows(i)("PD_Commodity")
                        lblItemID.Text = dt.Rows(i)("PD_Goods")
                        lblHistoryID.Text = 0
                        lblOrderedQty.Text = dt.Rows(i)("PD_Quantity")
                        objSales.UpdateStockLedgerClosingBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, lblCommodityID.Text, lblItemID.Text, lblHistoryID.Text, lblOrderedQty.Text, sSession.IPAddress, 0, iMasterID, 1, ddlAccBrnch.SelectedValue)
                    Next
                End If

                GetSaleItemsGrid(iMasterID)
                SaveSalesJE(iMasterID)

                lblStatus.Text = "Submitted"
                lblError.Text = "Submitted Successfully."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Submitted Successfully','', 'success');", True)
            Else
                lblError.Text = "Select Existing Purchase Transaction/Create new transaction to Submit."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    Private Sub SaveSalesJE(ByVal iMasterID As Integer)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim objJE As New ClsPurchaseSalesJE
        Try
            'objVerification.iAcc_JE_ID = 0
            'objVerification.sAcc_JE_TransactionNo = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "P")
            'objVerification.iAcc_JE_Party = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            'objVerification.iAcc_JE_Location = 0
            'objVerification.iAcc_JE_BillType = 0

            'objVerification.iAcc_JE_InvoiceID = iPVID
            'objVerification.sAcc_JE_BillNo = sBillNo

            'objVerification.dAcc_JE_BillDate = txtReceiptDate.Text

            'objVerification.dAcc_JE_BillAmount = txtBillAmount.Text
            'objVerification.iAcc_JE_YearID = sSession.YearID
            'objVerification.sAcc_JE_Status = "W"
            'objVerification.iAcc_JE_CreatedBy = sSession.UserID
            'objVerification.iAcc_JE_CreatedOn = DateTime.Today
            'objVerification.sAcc_JE_Operation = "C"
            'objVerification.sAcc_JE_IPAddress = sSession.IPAddress
            'objVerification.dAcc_JE_BillCreatedDate = DateTime.Today
            'objVerification.sAcc_JE_AdvanceNaration = ""
            'objVerification.sAcc_JE_PaymentNarration = ""
            'objVerification.sAcc_JE_ChequeNo = ""
            'objVerification.sAcc_JE_IFSCCode = ""
            'objVerification.sAcc_JE_BankName = ""
            'objVerification.sAcc_JE_BranchName = ""

            'objVerification.iAcc_JE_UpdatedBy = sSession.UserID
            'objVerification.iAcc_JE_UpdatedOn = DateTime.Today
            'objVerification.iAcc_JE_CompID = sSession.AccessCodeID
            'Arr = objVerification.SavePurchaseJournalMaster(sSession.AccessCode, objVerification)
            'iTransID = Arr(1)

            For i = 0 To dgJEDetails.Items.Count - 1

                objSales.iATD_TrType = 9 'Sales voucher

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objSales.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objSales.iATD_ID = 0
                End If

                objSales.dATD_TransactionDate = DateTime.Today

                objSales.iATD_BillID = iMasterID
                objSales.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
                'iPaymentType

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objSales.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objSales.iATD_Head = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objSales.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objSales.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objSales.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objSales.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objSales.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objSales.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objSales.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objSales.dATD_Credit = 0
                End If

                If objSales.dATD_Debit > 0 And objSales.dATD_Credit = 0 Then
                    objSales.iATD_DbOrCr = 1 'Debit
                ElseIf objSales.dATD_Debit = 0 And objSales.dATD_Credit > 0 Then
                    objSales.iATD_DbOrCr = 2 'Credit
                End If

                objSales.iATD_CreatedBy = sSession.UserID
                objSales.dATD_CreatedOn = DateTime.Today

                objSales.sATD_Status = "A"
                objSales.iATD_YearID = sSession.YearID
                objSales.sATD_Operation = "C"
                objSales.sATD_IPAddress = sSession.IPAddress

                objSales.iATD_UpdatedBy = sSession.UserID
                objSales.dATD_UpdatedOn = DateTime.Today

                objSales.iATD_CompID = sSession.AccessCodeID

                objSales.iATD_ZoneID = ddlAccZone.SelectedValue
                objSales.iATD_RegionID = ddlAccRgn.SelectedValue
                objSales.iATD_AreaID = ddlAccArea.SelectedValue
                objSales.iATD_BranchID = ddlAccBrnch.SelectedValue

                objSales.dATD_OpenDebit = "0.00"
                objSales.dATD_OpenCredit = "0.00"
                objSales.dATD_ClosingDebit = "0.00"
                objSales.dATD_ClosingCredit = "0.00"
                objSales.iATD_SeqReferenceNum = 0

                objSales.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objSales)

            Next

            lblError.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            dgJEDetails.DataSource = objSales.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID, txtTransactionNo.Text)
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SavePurchaseJE")
        End Try
    End Sub
    Public Sub GetSaleItemsGrid(ByVal iMasterID As Integer)
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
        Dim iParty As Integer = 0

        Dim dt1 As New DataTable
        Dim dPartyTotal As Double
        Dim dtGSTRates As New DataTable : Dim sSql As String = ""
        Dim dTotalAmt, dSGSTAmt, dCGSTAmt, dIGSTAmt As Double
        Dim SGST, CGST, IGST As Double
        Dim sTypeOfBill As String = "" : Dim sState As String = ""
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("PaymentID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Type")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")

            'iParty = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            iParty = ddlParty.SelectedValue

            sTypeOfBill = objDb.SQLGetDescription(sSession.AccessCode, "Select Acc_Sales_InvoiceStatus From Acc_Sales_Masters Where Acc_Sales_ID=" & iMasterID & " And Acc_Sales_CompID=" & sSession.AccessCodeID & " And Acc_Sales_Year=" & sSession.YearID & " ")
            sState = objDb.SQLGetDescription(sSession.AccessCode, "Select Acc_Sales_State From Acc_Sales_Masters Where Acc_Sales_ID=" & iMasterID & " And Acc_Sales_CompID=" & sSession.AccessCodeID & " And Acc_Sales_Year=" & sSession.YearID & " ")

            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & sSession.AccessCodeID & " "
            dtGSTRates = objDb.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)
            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'

            If dtGSTRates.Rows.Count > 0 Then
                For k = 0 To dtGSTRates.Rows.Count - 1

                    dt1 = objDb.SQLExecuteDataSet(sSession.AccessCode, "Select * From ACC_Sales_Details Where PD_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And PD_MasterID=" & iMasterID & " And PD_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For z = 0 To dt1.Rows.Count - 1
                            dTotalAmt = dTotalAmt + dt1.Rows(z)("PD_Amount")
                            dSGSTAmt = dSGSTAmt + dt1.Rows(z)("PD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dt1.Rows(z)("PD_CGSTAmount")
                            dIGSTAmt = dIGSTAmt + dt1.Rows(z)("PD_IGSTAmount")
                            dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("PD_FinalTotal"))
                        Next

                        dRow = dt.NewRow 'Item Name
                        dRow("Id") = 0
                        dRow("HeadID") = objSales.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Sale Of Product " & sState)
                        dRow("GLID") = objSales.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Sale Of Product " & sState)
                        If sTypeOfBill = "Local" Then
                            dRow("SubGLID") = objSales.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Local GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Sale Account")
                        ElseIf sTypeOfBill = "Inter State" Then
                            dRow("SubGLID") = objSales.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Inter State GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Sale Account")
                        End If
                        dRow("PaymentID") = 5
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "Sale Of Material"

                        sGL = objSales.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objSales.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = 0
                        dRow("Credit") = dTotalAmt
                        dt.Rows.Add(dRow)


                        SGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        CGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        IGST = dtGSTRates.Rows(k)("GST_GSTRate")

                        dRow = dt.NewRow 'SGST
                        dRow("Id") = 0
                        dRow("HeadID") = objSales.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objSales.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objSales.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output SGST " & SGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 6
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "SGST"

                        sGL = objSales.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objSales.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = 0
                        dRow("Credit") = dSGSTAmt
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'CGST
                        dRow("Id") = 0
                        dRow("HeadID") = objSales.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objSales.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objSales.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output CGST " & CGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 7
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "CGST"

                        sGL = objSales.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objSales.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = 0
                        dRow("Credit") = dCGSTAmt
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'IGST
                        dRow("Id") = 0
                        dRow("HeadID") = objSales.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objSales.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objSales.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output IGST " & IGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 8
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "IGST"

                        sGL = objSales.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objSales.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = 0
                        dRow("Credit") = dIGSTAmt
                        dt.Rows.Add(dRow)

                        dTotalAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0 : dIGSTAmt = 0
                    End If

                Next

                dRow = dt.NewRow 'Party/Customer
                dRow("Id") = 0
                dRow("HeadID") = objSales.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_Head")
                dRow("GLID") = objSales.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_GL")
                dRow("SubGLID") = objSales.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "C")
                dRow("PaymentID") = 9
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Party/Customer"

                sGL = objSales.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objSales.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    dRow("SubGLDescription") = sArray(1)
                End If
                dRow("Debit") = dPartyTotal
                dRow("Credit") = 0

                txtBillAmount.Text = dPartyTotal

                dt.Rows.Add(dRow)

            End If

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetDefaultGridPurchase")
        End Try
    End Sub
    Private Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            txtRate.Enabled = True
            If ddlCommodity.SelectedIndex > 0 Then
                chkCategory.DataSource = objSales.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue)
                chkCategory.DataTextField = "Inv_Code"
                chkCategory.DataValueField = "Inv_ID"
                chkCategory.DataBind()
            Else
                loadDescitionStart()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub loadDescitionStart()
        Try
            chkCategory.DataSource = objSales.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            chkCategory.DataTextField = "Inv_Code"
            chkCategory.DataValueField = "Inv_ID"
            chkCategory.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
        Dim altPices As Integer
        Try
            lblError.Text = ""
            txtFreight.Text = 0 : txtFreightAmount.Text = 0
            If (chkCategory.SelectedValue > 0) Then
                ddlCommodity.SelectedValue = objSales.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                'If (ddlTypeOfSale.SelectedIndex > 0) Then
                '    If (ddlTypeOfSale.SelectedIndex = 1) Then
                '        ddlCommodity.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                '    ElseIf (ddlTypeOfSale.SelectedIndex = 2) Then
                '        If (ddlCstCtgry.SelectedIndex > 0) Then
                '            ddlCommodity.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                '        Else
                '            Exit Sub
                '        End If
                '    End If
                'Else
                '    Exit Sub
                'End If
            End If
            If ddlParty.SelectedIndex > 0 Then
                lblDescID.Text = chkCategory.SelectedValue
                LoadDesciptionDetails()
                altPices = objSales.GetAlterNatePiceValue(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                txtPices.Text = altPices
                ddlUnit.SelectedValue = objSales.GetUnitsValue(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                hfTotalPieces.Value = txtPices.Text
                txtHSNCode.Text = objSales.GetGSTRatesDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
            Else
                For i = 0 To chkCategory.Items.Count - 1
                    chkCategory.Items(i).Selected = False
                Next
                ddlUnit.Items.Clear() : txtHSNCode.Text = ""
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadDesciptionDetails()
        Dim dt As New DataTable
        Dim sArray As Array
        Dim dOrderDate As Date
        Dim iGSTRate As Integer
        Try
            txtRate.Text = "" : txtRateAmount.Text = ""
            txtQuantity.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtGSTRate.Text = "" : txtGSTAmount.Text = ""
            txtTotalAmount.Text = ""

            If lblDescID.Text <> "0" Then
                iGSTRate = objSales.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                If iGSTRate > 0 Then
                Else
                    lblError.Text = "Enter the HSN Details in Inventory Master."
                    Exit Sub
                End If

                'If txtOrderDate.Text <> "" Then
                'dOrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                '    dt = objPO.CheckDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text, dOrderDate)
                '    If dt.Rows.Count = 0 Then
                '        lblError.Text = "Enter Details in Inventory Master Details"
                '        Exit Sub
                '    End If
                'If dt.Rows.Count > 1 Then
                '        ddlRate.DataSource = dt
                '        ddlRate.DataTextField = "INVH_PreDeterminedPrice"
                '        ddlRate.DataValueField = "InvH_ID"
                '        ddlRate.DataBind()
                '        ddlRate.Enabled = True : txtRate.Enabled = False
                '        txtHistoryID.Text = ddlRate.SelectedValue
                '        sArray = ddlRate.SelectedItem.Text.Split("-")
                '        txtRate.Text = sArray(0)
                '        'LoadExciseUsingDate()

                '        GetOtherDetails(txtHistoryID.Text)
                '    Else
                '        sArray = dt.Rows(0)(1).ToString().Split("-")
                '        txtRate.Text = sArray(0)
                '        txtHistoryID.Text = dt.Rows(0)(0).ToString()

                '        'LoadExciseUsingDate()
                '        ddlRate.Enabled = False : txtRate.Enabled = True
                '        If txtHistoryID.Text <> "" Then
                '            ' GetPurchaseDetails(txtHistoryID.Text)
                '            GetOtherDetails(txtHistoryID.Text)
                '        End If
                '    End If

                ddlUnit.DataSource = objSales.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                ddlUnit.DataTextField = "Mas_Desc"
                ddlUnit.DataValueField = "Mas_ID"
                ddlUnit.DataBind()
                ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")

                txtGSTID.Text = 0
                txtGSTRate.Text = 0

                Dim sGSTRate As String = ""
                sGSTRate = objSales.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                If sGSTRate <> "HSN" Then
                    txtGSTID.Text = 0
                    'txtGSTRate.Text = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                    txtGSTRate.Text = 0
                Else
                    txtGSTID.Text = objSales.GetGSTID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                    txtGSTRate.Text = objSales.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                End If
                'End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDesciptionDetails")
        End Try
    End Sub
    Private Sub lnkbtnCreateCommodity_Click(sender As Object, e As EventArgs) Handles lnkbtnCreateCommodity.Click
        Try
            Response.Redirect(String.Format("~/Masters/InventoryMaster.aspx?"), False)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnItemAdd_Click(sender As Object, e As EventArgs) Handles btnItemAdd.Click
        Dim dtDetails As New DataTable
        Dim dr As DataRow
        Dim dRateAmt, dDisAmt, dTotal, dGSTAmt, dNetAmt, dCharge, dItemTotal, dTotalAmt As Double
        Try
            lblError.Text = ""
            dtSales = BuildTable()

            If IsNothing(Session("dtSales")) = False Then
                dtSales = Session("dtSales")
            End If

            dr = dtSales.NewRow
            dr("CommodityID") = ddlCommodity.SelectedValue
            dr("GoodsID") = chkCategory.SelectedValue
            dr("UnitID") = ddlUnit.SelectedValue

            dr("HSNCode") = txtHSNCode.Text
            dr("Commodity") = ddlCommodity.SelectedItem.Text
            dr("Goods") = chkCategory.SelectedItem.Text
            dr("Unit") = ddlUnit.SelectedItem.Text
            dr("MRP") = txtRate.Text
            dr("Qty") = txtQuantity.Text
            dr("RateAMt") = txtRateAmount.Text
            If txtDiscount.Text <> "" Then
                dr("Discount") = txtDiscount.Text
                dr("DiscountAmt") = txtDiscountAmount.Text
            Else
                dr("Discount") = 0
                dr("DiscountAmt") = 0
                txtDiscount.Text = 0
                txtDiscountAmount.Text = 0
            End If


            'If txtOtherCharge.Text <> "" Then
            '    If dtPurchase.Rows.Count > 0 Then
            '        For i = 0 To dtPurchase.Rows.Count - 1
            '            dItemTotal = dItemTotal + dtPurchase.Rows(i)("RateAMt")
            '        Next
            '        dCharge = (txtTotalAmt.Text * txtOtherCharge.Text) / (dItemTotal + txtTotalAmt.Text)
            '        dr("Charge") = dCharge
            '    Else
            '        dCharge = (txtTotalAmt.Text * txtOtherCharge.Text) / (txtTotalAmt.Text)
            '        dr("Charge") = dCharge
            '    End If
            'Else
            dCharge = 0
            dr("Charge") = 0
            'End If
            dr("Total") = txtRateAmount.Text - txtDiscountAmount.Text
            dr("GST") = txtGSTRate.Text
            dr("GSTAmt") = txtGSTAmount.Text
            dr("NetAmount") = txtTotalAmount.Text
            dtSales.Rows.Add(dr)

            dgItems.DataSource = dtSales
            dgItems.DataBind()

            Session("dtSales") = dtSales

            For i = 0 To dgItems.Items.Count - 1
                dTotalAmt = dTotalAmt + Convert.ToDouble(dgItems.Items(i).Cells(16).Text)
            Next
            txtBillAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")

            txtHSNCode.Text = "" : chkCategory.ClearSelection() : ddlUnit.Items.Clear() : txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtTotalAmount.Text = ""

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnItemAdd_Click")
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try

            If ddlAccBrnch.SelectedIndex > 0 Then
                ddlBranch.SelectedValue = ddlAccBrnch.SelectedValue
                ddlBranch_SelectedIndexChanged(sender, e)

                If ddlAccBrnch.SelectedIndex > 0 Then
                    iParent = objSales.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                    ddlAccArea.SelectedValue = iParent
                End If
                If ddlAccArea.SelectedIndex > 0 Then
                    iParent = objSales.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                    ddlAccRgn.SelectedValue = iParent
                End If
                If ddlAccRgn.SelectedIndex > 0 Then
                    iParent = objSales.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                    ddlAccZone.SelectedValue = iParent
                End If

            Else
                ddlBranch.SelectedIndex = 0
                ddlBranch_SelectedIndexChanged(sender, e)

                ddlAccArea.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccZone.SelectedIndex = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccBrnch_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub GvCharge_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles GvCharge.ItemDataBound
        Try
            GvCharge.Columns(3).Visible = False
            If sSTSave = "YES" Then
                GvCharge.Columns(3).Visible = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
