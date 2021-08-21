Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports DatabaseLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing
Partial Class Purchase_InvoiceForm
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Purchase\InvoiceForm.aspx"
    Dim sSession As New AllSession
    Dim objInvoiceForm As New ClsInvoiceForm
    Dim objDb As New DBHelper
    Dim objFasGnrl As New clsFASGeneral
    Private Shared sIKBBackStatus As String
    Private Shared sCurrentMonthID As Integer = 0
    Dim objclsFASPermission As New clsFASPermission
    Dim objclsModulePermission As New clsModulePermission
    Dim objJE As New ClsPurchaseSalesJE
    Dim objGnrlFnctn As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objPO As New clsPurchaseOrder
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnNew.ImageUrl = "~/Images/Reresh24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
        imgbtnApprove.ImageUrl = "~/Images/CheckMark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Dim dt As New DataTable
        Dim iDefaultBranch As Integer
        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PI")
                imgbtnSave.Visible = False : imgbtnApprove.Visible = False : imgbtnNew.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnApprove.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnNew.Visible = True
                    End If
                End If

                txtGLID.Text = 0
                Session("VeiwGrid") = Nothing

                txtBreakup.Text = ""
                BindExistingInvoice()
                GenerateOrderCode()
                iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                LoadTransactions(iDefaultBranch)
                BindCompanyType()
                'BindGSTNCategory()
                LoadSuppliers()
                LoadChargeType()

                Session("ChargesMaster") = Nothing
                'dt = objInvoiceForm.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                'If dt.Rows.Count > 0 Then
                '    txtCompanyAddress.Text = dt.Rows(0)("CUST_Address")
                '    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")
                'End If
                Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return Validate();")

                Dim iDisID As String = ""
                iDisID = Request.QueryString("IFID")
                If iDisID <> "" Then
                    ddlExistingInvoiceNo.SelectedValue = Request.QueryString("IFID")
                    ddlExistingInvoiceNo_SelectedIndexChanged(sender, e)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub DashBoardLoadExistGoodsInwardNo(ByVal iTransactionID As Integer)
        Try
            ddlPurchaseRegister.DataSource = objInvoiceForm.DashBoardLoadInwardNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransactionID)
            ddlPurchaseRegister.DataValueField = "PRM_ID"
            ddlPurchaseRegister.DataTextField = "PRM_DocumentRefNo"
            ddlPurchaseRegister.DataBind()
            ddlPurchaseRegister.Items.Insert(0, "--- Select Document Ref NO ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCode()
        Try
            txtInvoiceNo.Text = objInvoiceForm.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCode")
        End Try
    End Sub
    Private Sub BindExistingInvoice()
        Dim dt As New DataTable
        Try
            dt = objInvoiceForm.LoadExistingInvoice(sSession.AccessCode, sSession.AccessCodeID)
            ddlExistingInvoiceNo.DataSource = dt
            ddlExistingInvoiceNo.DataTextField = "PIM_No"
            ddlExistingInvoiceNo.DataValueField = "PIM_ID"
            ddlExistingInvoiceNo.DataBind()
            ddlExistingInvoiceNo.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindCompanyType()
        Dim dt As New DataTable
        Try
            dt = objInvoiceForm.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyType.DataSource = dt
            ddlCompanyType.DataTextField = "Mas_Desc"
            ddlCompanyType.DataValueField = "Mas_Id"
            ddlCompanyType.DataBind()
            ddlCompanyType.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindGSTNCategory(ByVal sCompanyType As String)
        Dim dt As New DataTable
        Try
            dt = objInvoiceForm.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategory.DataSource = dt
            ddlGSTCategory.DataTextField = "GC_GSTCategory"
            ddlGSTCategory.DataValueField = "GC_Id"
            ddlGSTCategory.DataBind()
            ddlGSTCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSuppliers()
        Try
            ddlSupplier.DataSource = objInvoiceForm.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplier.DataTextField = "CSM_Name"
            ddlSupplier.DataValueField = "CSM_ID"
            ddlSupplier.DataBind()
            ddlSupplier.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objInvoiceForm.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadTransactions(ByVal iBranch As Integer)
        Try
            ddlPurchaseOrder.DataSource = objInvoiceForm.LoadOurRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBranch)
            ddlPurchaseOrder.DataTextField = "POM_OrderNo"
            ddlPurchaseOrder.DataValueField = "POM_ID"
            ddlPurchaseOrder.DataBind()
            ddlPurchaseOrder.Items.Insert(0, "--- Select Transactions ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadOrderDetails(ByVal iPONo As Integer)
        Dim dtable As New DataTable
        Dim objGIN As New ClsGoodsInward
        Dim Sdate As DateTime
        Try
            dtable = objInvoiceForm.OrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPONo)
            If dtable.Rows.Count > 0 Then
                For i = 0 To dtable.Rows.Count - 1
                    'Sdate = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("POM_OrderDate"), "D")
                    If IsDBNull(dtable.Rows(i)("POM_OrderDate")) = False Then
                        txtOrderDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("POM_OrderDate"), "D")
                    Else
                        txtOrderDate.Text = ""
                    End If

                    ddlSupplier.SelectedValue = dtable.Rows(i)("POM_Supplier")

                    'Extra'
                    ddlCompanyType.SelectedValue = dtable.Rows(i)("POM_CompanyType")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dtable.Rows(i)("POM_GSTNCategory")
                    'Extra'

                    txtCompanyAddress.Text = dtable.Rows(i)("POM_CompanyAddress")
                    txtCompanyGSTNRegNo.Text = dtable.Rows(i)("POM_CompanyGSTNRegNo")
                    txtBillingAddress.Text = dtable.Rows(i)("POM_BillingAddress")
                    txtBillingGSTNRegNo.Text = dtable.Rows(i)("POM_BillingGSTNRegNo")
                    txtDeliveryFromAddress.Text = dtable.Rows(i)("POM_DeliveryFrom")
                    txtDeliveryFromGSTNRegNo.Text = dtable.Rows(i)("POM_DeliveryFromGSTNRegNo")
                    txtReceiveAddress.Text = dtable.Rows(i)("POM_DeliveryAddress")
                    txtReceiveGSTNRegNo.Text = dtable.Rows(i)("POM_DeliveryGSTNRegNo")

                    Dim dtCharge As New DataTable
                    dtCharge = objInvoiceForm.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseOrder.SelectedValue, 0, 0)
                    GvCharge.DataSource = dtCharge
                    GvCharge.DataBind()
                    Session("ChargesMaster") = dtCharge
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOrderDetails")
        End Try
    End Sub
    Protected Sub dgViewPI_PreRender(sender As Object, e As EventArgs) Handles dgViewPI.PreRender
        Dim dt As New DataTable
        Try
            If dgViewPI.Rows.Count > 0 Then
                dgViewPI.UseAccessibleHeader = True
                dgViewPI.HeaderRow.TableSection = TableRowSection.TableHeader
                dgViewPI.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgViewPI_PreRender")
        End Try
    End Sub
    Protected Sub ddlPurchaseOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPurchaseOrder.SelectedIndexChanged
        Dim dtSupplier As New DataTable
        Try
            lblError.Text = "" : txtOrderDate.Text = ""
            ddlPurchaseRegister.Items.Clear()
            If (ddlPurchaseOrder.SelectedIndex > 0) Then
                LoadOrderDetails(ddlPurchaseOrder.SelectedValue)
                'ddlSupplier.SelectedValue = objInvoiceForm.GetSupplier(sSession.AccessCode, sSession.AccessCodeID, ddlPurchaseOrder.SelectedValue, sSession.YearID)
                'dtSupplier = objInvoiceForm.GetSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
                'If dtSupplier.Rows.Count > 0 Then
                '    txtBillingAddress.Text = dtSupplier.Rows(0)("CSM_Address")
                '    txtBillingGSTNRegNo.Text = dtSupplier.Rows(0)("CSM_GSTNRegNo")
                'End If
                LoadExistGoodsInwardNo(ddlPurchaseOrder.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPurchaseOrder_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadExistGoodsInwardNo(ByVal iTransactionID As Integer)
        Try
            ddlPurchaseRegister.DataSource = objInvoiceForm.LoadInwardNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransactionID)
            ddlPurchaseRegister.DataValueField = "PRM_ID"
            ddlPurchaseRegister.DataTextField = "PRM_DocumentRefNo"
            ddlPurchaseRegister.DataBind()
            ddlPurchaseRegister.Items.Insert(0, "--- Select Document Ref NO ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlPurchaseRegister_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPurchaseRegister.SelectedIndexChanged
        Dim dtTab As New DataTable
        Dim dt1, dt4, dt5, dt As New DataTable
        Dim dTable1 As New DataTable
        Dim dtable2 As New DataTable
        Dim dtable3 As New DataTable
        Dim dtable4 As New DataTable
        Dim flag As Integer = 0
        Dim count As Integer = 0
        Dim Checkdate As Date
        lblError.Text = ""
        Try
            If (ddlPurchaseRegister.SelectedIndex = 0) Then
                dgViewPI.DataSource = Nothing
                dgViewPI.DataBind()
                lblStatus.Text = ""
                Exit Sub
            End If
            If (objInvoiceForm.CheckVerificationNo(sSession.AccessCode, ddlPurchaseRegister.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlPurchaseOrder.SelectedValue())) Then
                lblStatus.Text = "Approved"
                Exit Sub
            Else
                lblStatus.Text = "Waiting For APproval"
            End If
            Checkdate = objInvoiceForm.getDateFromInward(sSession.AccessCode, ddlPurchaseRegister.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlPurchaseOrder.SelectedValue)
            If (Checkdate = "01/01/1990") Or Checkdate = "01-01-1990" Then
                txtInvoiceDate.Text = ""
            Else
                txtInvoiceDate.Text = objFasGnrl.FormatDtForRDBMS(Checkdate, "D")
                'Date.ParseExact(Checkdate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                lblStatus.Text = "Waiting For APproval"
            End If
            If (objInvoiceForm.CheckRegisterNo(sSession.AccessCode, ddlPurchaseRegister.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlPurchaseOrder.SelectedValue)) Then
                If (objInvoiceForm.CheckInwardNo(sSession.AccessCode, ddlPurchaseRegister.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlPurchaseOrder.SelectedValue)) Then
                    lblError.Text = ""
                    dgViewPI.DataSource = Nothing
                    dgViewPI.DataBind()
                    If (ddlPurchaseRegister.SelectedValue <> "") Then
                        dt = objInvoiceForm.GetMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseRegister.SelectedValue)
                    End If
                    If (ddlPurchaseRegister.SelectedIndex > 0) Then
                        ddlSupplier.SelectedValue = objInvoiceForm.GetSupplier(sSession.AccessCode, sSession.AccessCodeID, ddlPurchaseOrder.SelectedValue, sSession.YearID)
                        dt1 = objInvoiceForm.GetTransactionDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseRegister.SelectedItem.Text, sSession.YearID, ddlPurchaseOrder.SelectedValue)
                        dt5 = objInvoiceForm.GetTransactionDetailsNewItemDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseRegister.SelectedItem.Text, 0, ddlPurchaseOrder.SelectedValue)
                        dTable1.Merge(dt1)
                        dtable4.Merge(dt4)
                        dtable3.Merge(dt5)
                        flag = 1
                    End If
                    If (flag = 1) Then
                        dgViewPI.DataSource = dTable1
                        dgViewPI.DataBind()
                    End If
                Else
                    lblError.Text = "Invoice not Received at Account Department"
                End If
            Else
                lblError.Text = "Goods not at Received"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPurchaseRegister_SelectedIndexChanged")
        End Try
    End Sub
    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedOn", OleDb.OleDbType.Date)
            'ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dATD_CreatedOn)
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            'ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_UpdatedBy)
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_UpdatedOn", OleDb.OleDbType.Date)
            'ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dATD_UpdatedOn)
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spAcc_Transactions_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveUpdateTransactionDetails")
        End Try
    End Function
    Public Function SavePurchaseJournalMaster(ByVal sNameSpace As String, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(30) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_TransactionNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_Party)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_Location)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_BillType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_BillNo))
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dAcc_JE_BillDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dAcc_JE_BillAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dAcc_JE_AdvanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceNaration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_AdvanceNaration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BalanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dAcc_JE_BalanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_NetAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dAcc_JE_NetAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PaymentNarration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_PaymentNarration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeNo", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_ChequeNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dAcc_JE_ChequeDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IFSCCode", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_IFSCCode)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BankName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_BankName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BranchName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_BranchName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_CreatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.sAcc_JE_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillCreatedDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.dAcc_JE_BillCreatedDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_UpdatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_UpdatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_InvoiceID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objPSJEDetails.iAcc_JE_InvoiceID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spAcc_Purchase_JE_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SavePurchaseJournalMaster")
        End Try
    End Function
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblError.Text = ""
            lblStatus.Text = ""
            Response.Redirect(String.Format("~/Purchase/InvoiceMasterForm.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SavePurchaseJournalMaster")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim iMasterID As Integer = 0
        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblGoods, lblUnit, lblExpectedDate, lblQuantity, lblRate, lblCharges, lblRateAmount, lblTotalAmount, lblDiscountAmount, lblGSTID, lblGSTRate, lblGSTAmount, lblRemarks As New Label
        Dim lblSGST, lblSGSTAmount, lblCGST, lblCGSTAmount, lblIGST, lblIGSTAmount, lblFinalTotal As New Label
        'Dim ddlDiscount As New DropDownList
        Dim HFGSTAmount, HFDiscountAmount, HFTotalAmount, HFFinalTotal As HiddenField
        Dim bCheck As Boolean
        Dim txtDiscount As TextBox
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim ibranch As Integer
        Try
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

            bCheck = objInvoiceForm.CheckInvoice(sSession.AccessCode, sSession.AccessCodeID, txtInvoiceNo.Text)
            If bCheck = True Then
                lblError.Text = "This is already saved."
                Exit Sub
            End If

            'Check Source & Destination State Code'
            Dim sSStr As String = "" : Dim sDStr As String = ""
            If txtDeliveryFromGSTNRegNo.Text <> "" Then
                sSStr = objInvoiceForm.CheckStateCode(sSession.AccessCode, sSession.AccessCodeID, Trim(txtDeliveryFromGSTNRegNo.Text))
                If sSStr = False Then
                    lblError.Text = "Delivery From GSTN Reg.No Does Not Exists."
                    Exit Sub
                End If
            End If

            If txtReceiveGSTNRegNo.Text <> "" Then
                sDStr = objInvoiceForm.CheckStateCode(sSession.AccessCode, sSession.AccessCodeID, Trim(txtReceiveGSTNRegNo.Text))
                If sDStr = False Then
                    lblError.Text = "Receiving GSTN Reg.No Does Not Exists."
                    Exit Sub
                End If
            End If

            'Check Source & Destination State Code'

            objInvoiceForm.PIM_No = txtInvoiceNo.Text
            objInvoiceForm.PIM_OrderID = ddlPurchaseOrder.SelectedValue
            objInvoiceForm.PIM_PRegesterID = ddlPurchaseRegister.SelectedValue
            If txtOrderDate.Text <> "" Then
                objInvoiceForm.PIM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            objInvoiceForm.PIM_SupplierID = ddlSupplier.SelectedValue
            If txtInvoiceDate.Text <> "" Then
                objInvoiceForm.PIM_InvoiceDate = Date.ParseExact(Trim(txtInvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            objInvoiceForm.PIM_CreatedBy = sSession.UserID
            objInvoiceForm.PIM_CreatedOn = System.DateTime.Now
            objInvoiceForm.PIM_Status = "W"
            objInvoiceForm.PIM_YearID = sSession.YearID
            objInvoiceForm.PIM_CompID = sSession.AccessCodeID

            objInvoiceForm.PIM_TrType = 4

            If txtCompanyAddress.Text <> "" Then
                objInvoiceForm.PIM_CompanyAddress = txtCompanyAddress.Text
            Else
                objInvoiceForm.PIM_CompanyAddress = ""
            End If

            If txtBillingAddress.Text <> "" Then
                objInvoiceForm.PIM_BillingAddress = txtBillingAddress.Text
            Else
                objInvoiceForm.PIM_BillingAddress = ""
            End If

            If txtDeliveryFromAddress.Text <> "" Then
                objInvoiceForm.PIM_DeliveryFrom = txtDeliveryFromAddress.Text
            Else
                objInvoiceForm.PIM_DeliveryFrom = ""
            End If

            If txtReceiveAddress.Text <> "" Then
                objInvoiceForm.PIM_ReceiveAddress = txtReceiveAddress.Text
            Else
                objInvoiceForm.PIM_ReceiveAddress = ""
            End If

            If txtCompanyGSTNRegNo.Text <> "" Then
                objInvoiceForm.PIM_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
            Else
                objInvoiceForm.PIM_CompanyGSTNRegNo = ""
            End If

            If txtBillingGSTNRegNo.Text <> "" Then
                objInvoiceForm.PIM_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
            Else
                objInvoiceForm.PIM_BillingGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" Then
                objInvoiceForm.PIM_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
            Else
                objInvoiceForm.PIM_DeliveryFromGSTNRegNo = ""
            End If

            If txtReceiveGSTNRegNo.Text <> "" Then
                objInvoiceForm.PIM_ReceiveGSTNRegNo = txtReceiveGSTNRegNo.Text
            Else
                objInvoiceForm.PIM_ReceiveGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
                objInvoiceForm.PIM_InvoiceStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text = "" Then
                objInvoiceForm.PIM_InvoiceStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtReceiveGSTNRegNo.Text <> "" Then
                objInvoiceForm.PIM_InvoiceStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtReceiveGSTNRegNo.Text = "" Then
                objInvoiceForm.PIM_InvoiceStatus = "Local"
            End If
            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
            '    objInvoiceForm.PIM_InvoiceStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
            'End If

            objInvoiceForm.PIM_CompanyType = ddlCompanyType.SelectedValue
            objInvoiceForm.PIM_GSTNCategory = ddlGSTCategory.SelectedValue

            If txtManualBillAmt.Text <> "" Then
                objInvoiceForm.PIM_ManualBillAmount = txtManualBillAmt.Text
            Else
                objInvoiceForm.PIM_ManualBillAmount = 0
            End If

            If txtManualGST.Text <> "" Then
                objInvoiceForm.PIM_ManualGST = txtManualGST.Text
            Else
                objInvoiceForm.PIM_ManualGST = 0
            End If

            Dim dTotalInvoiceAmt As Double
            If dgViewPI.Rows.Count > 0 Then
                For j = 0 To dgViewPI.Rows.Count - 1
                    HFFinalTotal = dgViewPI.Rows(j).FindControl("HFFinalTotal")
                    dTotalInvoiceAmt = dTotalInvoiceAmt + HFFinalTotal.Value
                Next
            End If

            If objInvoiceForm.PIM_ManualBillAmount <> dTotalInvoiceAmt Then
                objInvoiceForm.PIM_BillDifferenceStatus = "Invoice Not Matching."
            Else
                objInvoiceForm.PIM_BillDifferenceStatus = "Invoice Matching."
            End If

            objInvoiceForm.PIM_Operation = "C"
            objInvoiceForm.PIM_IPADdress = sSession.IPAddress

            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
            '    objInvoiceForm.PIM_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
            'End If
            If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
                objInvoiceForm.PIM_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text = "" Then
                objInvoiceForm.PIM_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), (""))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtReceiveGSTNRegNo.Text <> "" Then
                objInvoiceForm.PIM_State = GetSourceDestinationState((""), Trim(txtReceiveGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtReceiveGSTNRegNo.Text = "" Then

                ibranch = objInvoiceForm.getBranchFromPO(sSession.AccessCode, sSession.AccessCodeID, ddlPurchaseOrder.SelectedValue)

                If ibranch > 0 Then 'branch 
                    objInvoiceForm.PIM_State = objInvoiceForm.CheckDetailsofBranchState(sSession.AccessCode, sSession.AccessCodeID, ddlPurchaseOrder.SelectedValue)
                    If objInvoiceForm.PIM_State = "" Then
                        lblError.Text = "Update state in branch master"
                        lblUserMasterDetailsValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in branch master.','', 'success');", True)
                        Exit Sub
                    End If
                Else 'Company
                    objInvoiceForm.PIM_State = objInvoiceForm.CheckDetailsofCompState(sSession.AccessCode, sSession.AccessCodeID)
                    If objInvoiceForm.PIM_State = "" Then
                        lblError.Text = "Update state in company master"
                        lblUserMasterDetailsValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in company master.','', 'success');", True)
                        Exit Sub
                    End If
                End If
            End If

            'Chart Of Accounts'
            Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
            Dim sPerm As String = ""
            Dim sArray1 As Array
            Dim sName As String = ""

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146

            'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
            sName = "Purchase Of Product " & objInvoiceForm.PIM_State
            txtGLID.Text = objInvoiceForm.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
            Else
                iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
            End If
            'Chart Of Accounts'

            Dim dtGSTRates As New DataTable
            dtGSTRates = objInvoiceForm.BindGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'
            If dtGSTRates.Rows.Count > 0 Then
                For x = 0 To dtGSTRates.Rows.Count - 1

                    sName = "Local GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objInvoiceForm.PIM_State & " Purchase Account"
                    txtGLID.Text = objInvoiceForm.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                    sName = "Inter State GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objInvoiceForm.PIM_State & " Purchase Account"
                    txtGLID.Text = objInvoiceForm.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                    sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")
                    iHead = sArray1(0) '1
                    iGroup = sArray1(1) '29
                    iSubGroup = sArray1(2) '31
                    iGL = sArray1(3) '146

                    sName = "INPUT SGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objInvoiceForm.PIM_State & " Purchase Account"
                    txtGLID.Text = objInvoiceForm.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "INPUT CGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objInvoiceForm.PIM_State & " Purchase Account"
                    txtGLID.Text = objInvoiceForm.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "INPUT IGST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objInvoiceForm.PIM_State & " Purchase Account"
                    txtGLID.Text = objInvoiceForm.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                Next
            End If

            Arr = objInvoiceForm.SaveInvoiceMaster(sSession.AccessCode, objInvoiceForm)
            iMasterID = Arr(1)
            txtMasterID.Text = iMasterID

            If dgViewPI.Rows.Count > 0 Then
                For i = 0 To dgViewPI.Rows.Count - 1

                    lblCommodityID = dgViewPI.Rows(i).FindControl("lblCommodityID")
                    lblItemID = dgViewPI.Rows(i).FindControl("lblItemID")
                    lblHistoryID = dgViewPI.Rows(i).FindControl("lblHistoryID")
                    lblUnitID = dgViewPI.Rows(i).FindControl("lblUnitID")
                    lblCommodity = dgViewPI.Rows(i).FindControl("lblCommodity")
                    lblGoods = dgViewPI.Rows(i).FindControl("lblGoods")
                    lblUnit = dgViewPI.Rows(i).FindControl("lblUnit")
                    lblRemarks = dgViewPI.Rows(i).FindControl("lblRemarks")
                    lblQuantity = dgViewPI.Rows(i).FindControl("lblQuantity")
                    lblRate = dgViewPI.Rows(i).FindControl("lblRate")
                    lblCharges = dgViewPI.Rows(i).FindControl("lblCharges")
                    lblRateAmount = dgViewPI.Rows(i).FindControl("lblRateAmount")

                    txtDiscount = dgViewPI.Rows(i).FindControl("txtDiscount")
                    lblDiscountAmount = dgViewPI.Rows(i).FindControl("lblDiscountAmount")
                    lblTotalAmount = dgViewPI.Rows(i).FindControl("lblTotalAmount")

                    lblGSTID = dgViewPI.Rows(i).FindControl("lblGSTID")
                    lblGSTRate = dgViewPI.Rows(i).FindControl("lblGSTRate")
                    lblGSTAmount = dgViewPI.Rows(i).FindControl("lblGSTAmount")

                    lblSGST = dgViewPI.Rows(i).FindControl("lblSGST")
                    lblSGSTAmount = dgViewPI.Rows(i).FindControl("lblSGSTAmount")
                    lblCGST = dgViewPI.Rows(i).FindControl("lblCGST")
                    lblCGSTAmount = dgViewPI.Rows(i).FindControl("lblCGSTAmount")
                    lblIGST = dgViewPI.Rows(i).FindControl("lblIGST")
                    lblIGSTAmount = dgViewPI.Rows(i).FindControl("lblIGSTAmount")

                    lblFinalTotal = dgViewPI.Rows(i).FindControl("lblFinalTotal")

                    HFGSTAmount = dgViewPI.Rows(i).FindControl("HFGSTAmount")
                    HFDiscountAmount = dgViewPI.Rows(i).FindControl("HFDiscountAmount")
                    HFTotalAmount = dgViewPI.Rows(i).FindControl("HFTotalAmount")
                    HFFinalTotal = dgViewPI.Rows(i).FindControl("HFFinalTotal")

                    objInvoiceForm.PID_MasterID = iMasterID
                    objInvoiceForm.PID_CommodityID = lblCommodityID.Text
                    objInvoiceForm.PID_DescID = lblItemID.Text
                    If lblHistoryID.Text <> "" Then
                        objInvoiceForm.PID_HistoryID = lblHistoryID.Text
                    Else
                        objInvoiceForm.PID_HistoryID = 0
                    End If
                    objInvoiceForm.PID_UnitID = lblUnitID.Text
                    objInvoiceForm.PID_Remarks = lblRemarks.Text
                    objInvoiceForm.PID_Rate = lblRate.Text
                    objInvoiceForm.PID_Quantity = lblQuantity.Text
                    objInvoiceForm.PID_ChargePerItem = lblCharges.Text
                    objInvoiceForm.PID_RateAmount = lblRateAmount.Text

                    'If ddlDiscount.SelectedIndex > 0 Then
                    '    objInvoiceForm.PID_Discount = ddlDiscount.SelectedValue
                    'Else
                    '    objInvoiceForm.PID_Discount = 0
                    'End If
                    If txtDiscount.Text <> "" Then
                        objInvoiceForm.PID_Discount = txtDiscount.Text
                    Else
                        objInvoiceForm.PID_Discount = 0
                    End If

                    If HFDiscountAmount.Value <> "" Then
                        objInvoiceForm.PID_DiscountAmount = HFDiscountAmount.Value
                    Else
                        objInvoiceForm.PID_DiscountAmount = 0
                    End If

                    objInvoiceForm.PID_Amount = HFTotalAmount.Value

                    objInvoiceForm.PID_GSTID = lblGSTID.Text
                    objInvoiceForm.PID_GSTRate = lblGSTRate.Text
                    If HFGSTAmount.Value <> "" Then
                        objInvoiceForm.PID_GSTAmount = HFGSTAmount.Value
                    Else
                        objInvoiceForm.PID_GSTAmount = 0
                    End If

                    If objInvoiceForm.PIM_InvoiceStatus = "Local" Then
                        objInvoiceForm.PID_SGST = objInvoiceForm.PID_GSTRate / 2
                        objInvoiceForm.PID_SGSTAmount = objInvoiceForm.PID_GSTAmount / 2
                        objInvoiceForm.PID_CGST = objInvoiceForm.PID_GSTRate / 2
                        objInvoiceForm.PID_CGSTAmount = objInvoiceForm.PID_GSTAmount / 2
                        objInvoiceForm.PID_IGST = 0
                        objInvoiceForm.PID_IGSTAmount = 0
                    ElseIf objInvoiceForm.PIM_InvoiceStatus = "Inter State" Then
                        objInvoiceForm.PID_SGST = 0
                        objInvoiceForm.PID_SGSTAmount = 0
                        objInvoiceForm.PID_CGST = 0
                        objInvoiceForm.PID_CGSTAmount = 0
                        objInvoiceForm.PID_IGST = objInvoiceForm.PID_GSTRate
                        objInvoiceForm.PID_IGSTAmount = objInvoiceForm.PID_GSTAmount
                    End If

                    If UCase(ddlGSTCategory.SelectedItem.Text) = "UNRIGISTERED DEALER" Then
                        Dim URD_GSTRate, URD_GSTAmt As Double

                        URD_GSTRate = objInvoiceForm.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
                        URD_GSTAmt = (((objInvoiceForm.PID_RateAmount - objInvoiceForm.PID_DiscountAmount) + objInvoiceForm.PID_ChargePerItem) * URD_GSTRate) / 100

                        objInvoiceForm.PID_SGST = URD_GSTRate / 2
                        objInvoiceForm.PID_SGSTAmount = URD_GSTAmt / 2
                        objInvoiceForm.PID_CGST = URD_GSTRate / 2
                        objInvoiceForm.PID_CGSTAmount = URD_GSTAmt / 2
                        objInvoiceForm.PID_IGST = 0
                        objInvoiceForm.PID_IGSTAmount = 0
                    End If

                    If HFFinalTotal.Value <> "" Then
                        objInvoiceForm.PID_FinalTotal = HFFinalTotal.Value
                    Else
                        objInvoiceForm.PID_FinalTotal = 0
                    End If

                    objInvoiceForm.PID_ItemStatus = "A"
                    objInvoiceForm.PID_Status = "W"
                    objInvoiceForm.PID_CompID = sSession.AccessCodeID
                    objInvoiceForm.PID_CreatedBy = sSession.UserID
                    objInvoiceForm.PID_CreatedOn = System.DateTime.Now
                    objInvoiceForm.PID_Operation = "C"
                    objInvoiceForm.PID_IPAddress = sSession.IPAddress

                    Arr = objInvoiceForm.SaveAcceptedDetails(sSession.AccessCode, objInvoiceForm)

                Next
            End If

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                lblUserMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
                lblUserMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
            lblStatus.Text = "Waiting For Approval"

            'SaveCharges(iMasterID)

            clearAll()
            BindExistingInvoice()
            ddlExistingInvoiceNo.SelectedValue = iMasterID
            ddlExistingInvoiceNo_SelectedIndexChanged(sender, e)

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
            objCOA.sgl_Desc = objFasGnrl.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objFasGnrl.SafeSQL(sReason)
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
                GetSourceDestinationState = objInvoiceForm.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sReceiveAddress <> "" Then
                sDestination = sReceiveAddress.Substring(0, 2)
                GetSourceDestinationState = objInvoiceForm.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sReceiveAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sReceiveAddress.Substring(0, 2)
                If sSource = sDestination Then
                    GetSourceDestinationState = objInvoiceForm.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    GetSourceDestinationState = objInvoiceForm.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return GetSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSourceDestinationState")
        End Try
    End Function
    Public Sub SaveCharges(ByVal iMasterID As Integer)
        Dim Arr() As String
        Try
            'Deleting charges Everytime & Saving'
            objInvoiceForm.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseOrder.SelectedValue, iMasterID)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1

                    objInvoiceForm.C_POrderID = ddlPurchaseOrder.SelectedValue
                    If ddlPurchaseRegister.SelectedIndex > 0 Then
                        objInvoiceForm.C_PGinID = ddlPurchaseRegister.SelectedValue
                    Else
                        objInvoiceForm.C_PGinID = 0
                    End If
                    objInvoiceForm.C_PInvoiceDocRef = iMasterID
                    objInvoiceForm.C_OrderType = ""
                    objInvoiceForm.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objInvoiceForm.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objInvoiceForm.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objInvoiceForm.C_PSType = "P"
                    objInvoiceForm.C_DelFlag = "W"
                    objInvoiceForm.C_Status = "C"
                    objInvoiceForm.C_CompID = sSession.AccessCodeID
                    objInvoiceForm.C_YearID = sSession.YearID
                    objInvoiceForm.C_CreatedBy = sSession.UserID
                    objInvoiceForm.C_CreatedOn = System.DateTime.Now
                    objInvoiceForm.C_Operation = "C"
                    objInvoiceForm.C_IPAddress = sSession.IPAddress

                    Arr = objInvoiceForm.SaveCharges(sSession.AccessCode, objInvoiceForm)
                Next
            End If
            'Charges Saving'
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveCharges")
        End Try
    End Sub
    Private Sub dgViewPI_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgViewPI.RowDataBound
        Dim dtDiscount As New DataTable
        Dim ddlDiscount As New DropDownList
        'Dim iDiscountID As Integer
        Dim lblCommodityID, lblItemID, lblHistoryID, lblQuantity, lblRate, lblCharges, lblRateAmount, lblDiscountAmount, lblTotalAmount, lblGSTID, lblGSTRate, lblGSTAmount, lblFinalTotal As New Label
        Dim HFDiscountAmount, HFCharges, HFTotalAmount, HFGSTAmount, HFFinalTotal As HiddenField
        Dim dChargeAmount, dItemsTotalFromDispatch As Double
        Dim dtInvoiceDetails, dtCharge As New DataTable
        Dim lblGoods As New Label
        Dim iDiscountID As Integer

        Dim txtDiscount As New TextBox
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                lblCommodityID = e.Row.FindControl("lblCommodityID")
                lblItemID = e.Row.FindControl("lblItemID")
                lblHistoryID = e.Row.FindControl("lblHistoryID")

                'ddlDiscount = e.Row.FindControl("ddlDiscount")
                'dtDiscount = objInvoiceForm.BindDiscount(sSession.AccessCode, sSession.AccessCodeID)
                'ddlDiscount.DataSource = dtDiscount
                'ddlDiscount.DataTextField = "MAS_DESC"
                'ddlDiscount.DataValueField = "MAS_ID"
                'ddlDiscount.DataBind()
                'ddlDiscount.Items.Insert(0, "Select")

                txtDiscount = e.Row.FindControl("txtDiscount")

                If ddlExistingInvoiceNo.SelectedIndex > 0 Then
                    iDiscountID = objInvoiceForm.GetDiscountFromInvoice(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseOrder.SelectedValue, ddlPurchaseRegister.SelectedValue, ddlExistingInvoiceNo.SelectedValue, lblCommodityID.Text, lblItemID.Text)
                    If iDiscountID > 0 Then
                        txtDiscount.Text = iDiscountID
                    Else
                        txtDiscount.Text = 0
                    End If
                Else
                    iDiscountID = objInvoiceForm.GetDiscountFromPOD(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseOrder.SelectedValue, lblCommodityID.Text, lblItemID.Text)
                    If iDiscountID > 0 Then
                        txtDiscount.Text = iDiscountID
                    Else
                        txtDiscount.Text = 0
                    End If
                End If

                lblQuantity = e.Row.FindControl("lblQuantity")
                lblRate = e.Row.FindControl("lblRate")
                lblCharges = e.Row.FindControl("lblCharges")
                lblRateAmount = e.Row.FindControl("lblRateAmount")
                lblDiscountAmount = e.Row.FindControl("lblDiscountAmount")
                lblTotalAmount = e.Row.FindControl("lblTotalAmount")

                lblGSTID = e.Row.FindControl("lblGSTID")
                lblGSTRate = e.Row.FindControl("lblGSTRate")
                lblGSTAmount = e.Row.FindControl("lblGSTAmount")
                lblFinalTotal = e.Row.FindControl("lblFinalTotal")

                HFDiscountAmount = e.Row.FindControl("HFDiscountAmount")
                HFCharges = e.Row.FindControl("HFCharges")
                HFTotalAmount = e.Row.FindControl("HFTotalAmount")
                HFGSTAmount = e.Row.FindControl("HFGSTAmount")
                HFFinalTotal = e.Row.FindControl("HFFinalTotal")

                lblGoods = e.Row.FindControl("lblGoods")

                Dim sGSTRate As String = ""
                sGSTRate = objInvoiceForm.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                If sGSTRate <> "HSN" Then
                    'lblGSTID.Text = 0
                    lblGSTID.Text = objInvoiceForm.GetGSTIDFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
                    lblGSTRate.Text = 0
                    'objInvoiceForm.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                Else
                    lblGSTID.Text = objInvoiceForm.GetGSTIDFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
                    lblGSTRate.Text = objInvoiceForm.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
                End If

                If GvCharge.Items.Count > 0 Then
                    For i = 0 To GvCharge.Items.Count - 1
                        dChargeAmount = dChargeAmount + GvCharge.Items(i).Cells(2).Text
                    Next
                End If
                dItemsTotalFromDispatch = objInvoiceForm.GetItemsTotalFromPurchaseRegister(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseRegister.SelectedValue)

                If txtBreakup.Text = "BreakUp" Then
                    If lblGoods.Text = "Total" Then
                        HFDiscountAmount.Value = 0 : HFCharges.Value = 0 : HFTotalAmount.Value = 0 : HFGSTAmount.Value = 0 : HFFinalTotal.Value = 0
                        lblFinalTotal.Text = txtManualBillAmt.Text
                        lblGSTRate.Text = txtManualGST.Text
                        CalculateBreakUpGST(lblQuantity, lblRate, lblRateAmount, txtDiscount, lblDiscountAmount, lblCharges, lblTotalAmount, lblGSTRate, lblGSTAmount, lblFinalTotal, HFDiscountAmount, HFCharges, HFTotalAmount, HFGSTAmount, HFFinalTotal, dChargeAmount, dItemsTotalFromDispatch)
                    End If
                Else
                    If ddlExistingInvoiceNo.SelectedIndex > 0 Then
                        '*** Commented bcz for HistoryID 0 items getting repeated  ***'
                        'dtInvoiceDetails = objInvoiceForm.BindInvoiceROWData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseOrder.SelectedValue, ddlExistingInvoiceNo.SelectedValue, ddlPurchaseRegister.SelectedValue, lblCommodityID.Text, lblItemID.Text, lblHistoryID.Text)
                        'If dtInvoiceDetails.Rows.Count > 0 Then
                        '    For m = 0 To dtInvoiceDetails.Rows.Count - 1
                        '        lblRate.Text = dtInvoiceDetails.Rows(m)("PID_Rate")
                        '        lblQuantity.Text = dtInvoiceDetails.Rows(m)("PID_Quantity")
                        '        lblRateAmount.Text = dtInvoiceDetails.Rows(m)("PID_RateAmount")
                        '        lblDiscountAmount.Text = dtInvoiceDetails.Rows(m)("PID_DiscountAmount")
                        '        lblCharges.Text = dtInvoiceDetails.Rows(m)("PID_ChargePeritem")
                        '        lblTotalAmount.Text = dtInvoiceDetails.Rows(m)("PID_Amount")
                        '        lblGSTRate.Text = dtInvoiceDetails.Rows(m)("PID_GSTRate")
                        '        lblGSTAmount.Text = dtInvoiceDetails.Rows(m)("PID_GSTAmount")
                        '        lblFinalTotal.Text = dtInvoiceDetails.Rows(m)("PID_FinalTotal")
                        '    Next
                        'End If
                        '*** Commented bcz for HistoryID 0 items getting repeated  ***'
                    Else
                        HFDiscountAmount.Value = 0 : HFCharges.Value = 0 : HFTotalAmount.Value = 0 : HFGSTAmount.Value = 0 : HFFinalTotal.Value = 0
                        CalculateGST(lblQuantity, lblRate, lblRateAmount, txtDiscount, lblDiscountAmount, lblCharges, lblTotalAmount, lblGSTRate, lblGSTAmount, lblFinalTotal, HFDiscountAmount, HFCharges, HFTotalAmount, HFGSTAmount, HFFinalTotal, dChargeAmount, dItemsTotalFromDispatch)
                    End If
                End If
                'ddlDiscount.Attributes.Add("onChange", "javascript:return CalculateGST('" & lblQuantity.ClientID & "','" & lblRate.ClientID & "','" & lblRateAmount.ClientID & "','" & txtDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblCharges.ClientID & "','" & lblTotalAmount.ClientID & "','" & lblGSTRate.ClientID & "','" & lblGSTAmount.ClientID & "','" & lblFinalTotal.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFCharges.ClientID & "','" & HFTotalAmount.ClientID & "','" & HFGSTAmount.ClientID & "','" & HFFinalTotal.ClientID & "'," & dChargeAmount & "," & dItemsTotalFromDispatch & ")")
                txtDiscount.Attributes.Add("Onblur", "javascript:return CalculateGST('" & lblQuantity.ClientID & "','" & lblRate.ClientID & "','" & lblRateAmount.ClientID & "','" & txtDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblCharges.ClientID & "','" & lblTotalAmount.ClientID & "','" & lblGSTRate.ClientID & "','" & lblGSTAmount.ClientID & "','" & lblFinalTotal.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFCharges.ClientID & "','" & HFTotalAmount.ClientID & "','" & HFGSTAmount.ClientID & "','" & HFFinalTotal.ClientID & "'," & dChargeAmount & "," & dItemsTotalFromDispatch & ")")

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgViewPI_RowDataBound")
        End Try
    End Sub
    Public Sub CalculateGST(ByVal lblQuantity As Label, ByVal lblRate As Label, ByVal lblRateAmount As Label, ByVal txtDiscount As TextBox, ByVal lblDiscountAmount As Label, ByVal lblCharges As Label, ByVal lblTotalAmount As Label, ByVal lblGSTRate As Label, ByVal lblGSTAmount As Label, ByVal lblFinalTotal As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFCharges As HiddenField, ByVal HFTotalAmount As HiddenField, ByVal HFGSTAmount As HiddenField, ByVal HFFinalTotal As HiddenField, ByVal dChargeAmount As Double, ByVal dItemsTotalFromDispatch As Double)
        Dim sBasicAmount As Double
        Dim sTotal As Double : Dim dDiscount As Double
        Dim dAmountOnCalculate As Double
        Try
            lblRateAmount.Text = lblQuantity.Text * lblRate.Text
            lblFinalTotal.Text = lblQuantity.Text * lblRate.Text

            lblDiscountAmount.Text = 0
            HFDiscountAmount.Value = 0

            If lblQuantity.Text <> "" And txtDiscount.Text > 0 Then
                lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblRateAmount.Text * txtDiscount.Text) / 100))
                HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblRateAmount.Text * txtDiscount.Text) / 100))
            End If

            If dChargeAmount > 0 Then
                sTotal = lblRateAmount.Text
                dDiscount = lblDiscountAmount.Text

                lblCharges.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal) * dChargeAmount) / dItemsTotalFromDispatch))
                HFCharges.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal) * dChargeAmount) / dItemsTotalFromDispatch))
            Else
                lblCharges.Text = 0
                HFCharges.Value = 0
            End If

            Dim dItemChargeAmt As Double
            sTotal = lblRateAmount.Text
            dDiscount = lblDiscountAmount.Text
            dItemChargeAmt = lblCharges.Text

            dAmountOnCalculate = String.Format("{0:0.00}", Convert.ToDecimal((sTotal - dDiscount) + dItemChargeAmt))
            lblTotalAmount.Text = dAmountOnCalculate
            HFTotalAmount.Value = dAmountOnCalculate

            If lblGSTRate.Text <> "" Then

                lblGSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))
                HFGSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))

                lblFinalTotal.Text = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
                HFFinalTotal.Value = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))

            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateGST")
        End Try
    End Sub
    Public Sub CalculateBreakUpGST(ByVal lblQuantity As Label, ByVal lblRate As Label, ByVal lblRateAmount As Label, ByVal txtDiscount As TextBox, ByVal lblDiscountAmount As Label, ByVal lblCharges As Label, ByVal lblTotalAmount As Label, ByVal lblGSTRate As Label, ByVal lblGSTAmount As Label, ByVal lblFinalTotal As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFCharges As HiddenField, ByVal HFTotalAmount As HiddenField, ByVal HFGSTAmount As HiddenField, ByVal HFFinalTotal As HiddenField, ByVal dChargeAmount As Double, ByVal dItemsTotalFromDispatch As Double)
        Dim sBasicAmount As Double
        Dim sTotal As Double : Dim dDiscount As Double
        Dim dAmountOnCalculate As Double
        Try
            'lblRateAmount.Text = lblQuantity.Text * lblRate.Text
            'lblFinalTotal.Text = lblQuantity.Text * lblRate.Text

            'lblDiscountAmount.Text = 0
            'HFDiscountAmount.Value = 0

            'If lblQuantity.Text <> "" And ddlDiscount.SelectedIndex > 0 Then
            '    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblRateAmount.Text * ddlDiscount.SelectedItem.Text) / 100))
            '    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblRateAmount.Text * ddlDiscount.SelectedItem.Text) / 100))
            'End If

            'If dChargeAmount > 0 Then
            '    sTotal = lblRateAmount.Text
            '    dDiscount = lblDiscountAmount.Text

            '    lblCharges.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal - dDiscount) * dChargeAmount) / dItemsTotalFromDispatch))
            '    HFCharges.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal - dDiscount) * dChargeAmount) / dItemsTotalFromDispatch))
            'Else
            '    lblCharges.Text = 0
            '    HFCharges.Value = 0
            'End If

            'Dim dItemChargeAmt As Double
            'sTotal = lblRateAmount.Text
            'dDiscount = lblDiscountAmount.Text
            'dItemChargeAmt = lblCharges.Text

            If lblGSTRate.Text <> "" Then

                Dim dGSTAMt As Double
                dGSTAMt = String.Format("{0:0.00}", Convert.ToDecimal(((lblFinalTotal.Text) * lblGSTRate.Text) / (lblGSTRate.Text + 100)))

                lblGSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dGSTAMt))
                HFGSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dGSTAMt))

                HFFinalTotal.Value = lblFinalTotal.Text
            End If

            'dAmountOnCalculate = String.Format("{0:0.00}", Convert.ToDecimal((sTotal - dDiscount) + dItemChargeAmt))
            lblTotalAmount.Text = lblFinalTotal.Text - lblGSTAmount.Text
            HFTotalAmount.Value = lblFinalTotal.Text - lblGSTAmount.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateBreakUpGST")
        End Try
    End Sub
    Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
        Dim dt, dtTable As New DataTable
        Try
            If ddlChargeType.SelectedIndex > 0 Then
                If txtShippingRate.Text <> "" Then
                    dt = AddCharges()
                    dtTable = objInvoiceForm.RemoveDublicate(dt)
                    GvCharge.DataSource = dtTable
                    GvCharge.DataBind()

                    dgViewPI.DataSource = objInvoiceForm.GetTransactionDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseRegister.SelectedItem.Text, sSession.YearID, ddlPurchaseOrder.SelectedValue)
                    dgViewPI.DataBind()
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AddCharges")
        End Try
    End Function
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

            dgViewPI.DataSource = objInvoiceForm.GetTransactionDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseRegister.SelectedItem.Text, sSession.YearID, ddlPurchaseOrder.SelectedValue)
            dgViewPI.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub
    Private Sub imgbtnNew_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNew.Click
        Try
            clearAll()
            lblError.Text = ""
            GenerateOrderCode()
            GvCharge.DataSource = Nothing
            GvCharge.DataBind()
            txtGLID.Text = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNew_Click")
        End Try
    End Sub
    Public Sub clearAll()
        Try
            txtBreakup.Text = "" : lblStatus.Text = ""
            ddlExistingInvoiceNo.SelectedIndex = 0 : txtInvoiceNo.Text = "" : txtInvoiceDate.Text = "" : ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.SelectedIndex = 0
            ddlPurchaseOrder.SelectedIndex = 0 : ddlPurchaseRegister.SelectedIndex = 0 : txtOrderDate.Text = "" : ddlSupplier.SelectedIndex = 0 : txtBillingAddress.Text = "" : txtBillingGSTNRegNo.Text = ""
            txtDeliveryFromAddress.Text = "" : txtDeliveryFromGSTNRegNo.Text = "" : txtReceiveAddress.Text = "" : txtReceiveGSTNRegNo.Text = ""
            ddlChargeType.SelectedIndex = 0 : txtShippingRate.Text = "" : txtManualBillAmt.Text = "" : txtManualGST.Text = ""

            dgViewPI.DataSource = Nothing
            dgViewPI.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "clearAll")
        End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim bCheck As String
        Try
            If ddlExistingInvoiceNo.SelectedIndex > 0 Then
                bCheck = objInvoiceForm.CheckApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInvoiceNo.SelectedValue)
                If bCheck = "A" Then
                    lblError.Text = "This InvoiceNo already approved."
                    Exit Sub
                End If
                objInvoiceForm.ApproveInvoice(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInvoiceNo.SelectedValue)
                lblError.Text = "Approved Successfully."
                lblStatus.Text = "Approved"
            Else
                lblError.Text = "Select Existing InvoiceNo to Approve."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    Private Sub ddlExistingInvoiceNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingInvoiceNo.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlExistingInvoiceNo.SelectedIndex > 0 Then
                dt = objInvoiceForm.GetExistingDate(sSession.AccessCode, sSession.AccessCodeID, ddlExistingInvoiceNo.SelectedValue)

                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1

                        If IsDBNull(dt.Rows(i)("PIM_CompanyType")) = False Then
                            ddlCompanyType.SelectedValue = dt.Rows(i)("PIM_CompanyType")
                        Else
                            ddlCompanyType.SelectedIndex = 0
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_GSTNCategory")) = False Then
                            BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                            ddlGSTCategory.SelectedValue = dt.Rows(i)("PIM_GSTNCategory")
                        Else
                            ddlGSTCategory.SelectedIndex = 0
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_OrderID")) = False Then
                            ddlPurchaseOrder.SelectedValue = dt.Rows(i)("PIM_OrderID")
                        Else
                            ddlPurchaseOrder.SelectedIndex = 0
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_OrderDate")) = False Then
                            txtOrderDate.Text = dt.Rows(i)("PIM_OrderDate")
                        Else
                            txtOrderDate.Text = ""
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_No")) = False Then
                            txtInvoiceNo.Text = dt.Rows(i)("PIM_No")
                        Else
                            txtInvoiceNo.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("PIM_PRegesterID")) = False Then
                            'LoadExistGoodsInwardNo(ddlPurchaseOrder.SelectedValue)
                            DashBoardLoadExistGoodsInwardNo(ddlPurchaseOrder.SelectedValue)
                            ddlPurchaseRegister.SelectedValue = dt.Rows(i)("PIM_PRegesterID")
                        Else
                            ddlPurchaseRegister.Items.Clear()
                        End If

                        If IsDBNull(dt.Rows(i)("PIM_InvoiceDate")) = False Then
                            txtInvoiceDate.Text = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PIM_InvoiceDate"), "D")
                        Else
                            txtInvoiceDate.Text = ""
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_SupplierID")) = False Then
                            ddlSupplier.SelectedValue = dt.Rows(i)("PIM_SupplierID")
                        Else
                            ddlSupplier.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("PIM_CompanyAddress")) = False Then
                            txtCompanyAddress.Text = dt.Rows(i)("PIM_CompanyAddress")
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_BillingAddress")) = False Then
                            txtBillingAddress.Text = dt.Rows(i)("PIM_BillingAddress")
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_DeliveryFrom")) = False Then
                            txtDeliveryFromAddress.Text = dt.Rows(i)("PIM_DeliveryFrom")
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_ReceiveAddress")) = False Then
                            txtReceiveAddress.Text = dt.Rows(i)("PIM_ReceiveAddress")
                        End If

                        If IsDBNull(dt.Rows(i)("PIM_CompanyGSTNRegNo")) = False Then
                            txtCompanyGSTNRegNo.Text = dt.Rows(i)("PIM_CompanyGSTNRegNo")
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_BillingGSTNRegNo")) = False Then
                            txtBillingGSTNRegNo.Text = dt.Rows(i)("PIM_BillingGSTNRegNo")
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_DeliveryFromGSTNRegNo")) = False Then
                            txtDeliveryFromGSTNRegNo.Text = dt.Rows(i)("PIM_DeliveryFromGSTNRegNo")
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_ReceiveGSTNRegNo")) = False Then
                            txtReceiveGSTNRegNo.Text = dt.Rows(i)("PIM_ReceiveGSTNRegNo")
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_ManualBillAmount")) = False Then
                            txtManualBillAmt.Text = dt.Rows(i)("PIM_ManualBillAmount")
                        End If
                        If IsDBNull(dt.Rows(i)("PIM_ManualGST")) = False Then
                            txtManualGST.Text = dt.Rows(i)("PIM_ManualGST")
                        End If

                        If IsDBNull(dt.Rows(i)("PIM_Status")) = False Then
                            If dt.Rows(i)("PIM_Status") = "W" Then
                                lblStatus.Text = "Waiting For Approval"
                            ElseIf dt.Rows(i)("PIM_Status") = "A" Then
                                lblStatus.Text = "Approved"
                            End If
                        End If

                        Dim dtCharge As New DataTable
                        dtCharge = objInvoiceForm.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseOrder.SelectedValue, ddlExistingInvoiceNo.SelectedValue, ddlPurchaseRegister.SelectedValue)
                        GvCharge.DataSource = dtCharge
                        GvCharge.DataBind()
                        Session("ChargesMaster") = dtCharge

                        dgViewPI.DataSource = objInvoiceForm.BindInvoiceDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingInvoiceNo.SelectedValue)
                        dgViewPI.DataBind()

                    Next
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingInvoiceNo_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub btnBreakUp_Click(sender As Object, e As EventArgs) Handles btnBreakUp.Click
        Dim dt1 As New DataTable
        Try
            txtBreakup.Text = "BreakUp"

            dt1 = objInvoiceForm.GetBreakUpDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPurchaseRegister.SelectedItem.Text, sSession.YearID, ddlPurchaseOrder.SelectedValue)
            dgViewPI.DataSource = dt1
            dgViewPI.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnBreakUp_Click")
        End Try
    End Sub
    Private Sub ddlCompanyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompanyType.SelectedIndexChanged
        Try
            If ddlCompanyType.SelectedIndex > 0 Then
                BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
            Else
                ddlGSTCategory.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCompanyType_SelectedIndexChanged")
        End Try
    End Sub
End Class
