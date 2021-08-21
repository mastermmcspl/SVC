Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports DatabaseLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing
Partial Class Purchase_PurchaseVerification
    Inherits System.Web.UI.Page

    Private Shared sFormName As String = "Orders\PurchaseVerification.aspx"
    Dim sSession As New AllSession
    Dim objVerification As New clsPurchaseVrification
    Dim objDb As New DBHelper
    Dim objFasGnrl As clsFASGeneral
    Private Shared sIKBBackStatus As String
    Private Shared sCurrentMonthID As Integer = 0
    Dim objclsFASPermission As New clsFASPermission
    Dim objclsModulePermission As New clsModulePermission
    Dim objJE As New ClsPurchaseSalesJE
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objPO As New clsPurchaseOrder
    Dim iDefaultBranch As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnWaiting.ImageUrl = "~/Images/Save24.png"
        'imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PV")
                imgRefresh.Visible = False : imgbtnWaiting.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgRefresh.Visible = True
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnWaiting.Visible = True
                    End If
                End If
                Session("VeiwGrid") = Nothing
                iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                BindInvoiceNo(iDefaultBranch)
                LoadTransactions()
                LoadSuppliers()
                LoadExistGoodsInwardNo(0)
                'imgbtnWaiting.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasPV", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Approve,") = True Then
                '        imgbtnWaiting.Visible = True
                '    End If
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadSuppliers()
        Try
            ddlSupplier.DataSource = objVerification.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplier.DataTextField = "CSM_Name"
            ddlSupplier.DataValueField = "CSM_ID"
            ddlSupplier.DataBind()
            ddlSupplier.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindInvoiceNo(ByVal iBranch As Integer)
        Try
            ddlInvoiceNo.DataSource = objVerification.LoadInvoicesNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBranch)
            ddlInvoiceNo.DataTextField = "PIM_No"
            ddlInvoiceNo.DataValueField = "PIM_ID"
            ddlInvoiceNo.DataBind()
            ddlInvoiceNo.Items.Insert(0, "--- Select Transactions ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadTransactions()
        Try
            ddlExistingTransactions.DataSource = objVerification.LoadOurRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingTransactions.DataTextField = "POM_OrderNo"
            ddlExistingTransactions.DataValueField = "POM_ID"
            ddlExistingTransactions.DataBind()
            ddlExistingTransactions.Items.Insert(0, "--- Select Transactions ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub LoadMethodOfPayment()
    '    Try
    '        ddlPaymentMode.DataSource = objVerification.LoadMethodOfPayment(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlPaymentMode.DataTextField = "Mas_desc"
    '        ddlPaymentMode.DataValueField = "Mas_id"
    '        ddlPaymentMode.DataBind()
    '        ddlPaymentMode.Items.Insert(0, "--- Select Payment Mode ---")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub dgViewExcess_PreRender(sender As Object, e As EventArgs) Handles dgViewExcess.PreRender
        Dim dt As New DataTable
        Try
            If dgViewExcess.Rows.Count > 0 Then
                dgViewExcess.UseAccessibleHeader = True
                dgViewExcess.HeaderRow.TableSection = TableRowSection.TableHeader
                dgViewExcess.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgViewExcess_PreRender")
        End Try
    End Sub
    Private Sub LoadOrderDetails(ByVal iPONo As Integer)
        Dim dtable As New DataTable
        Dim objGIN As New ClsGoodsInward
        Dim Sdate As DateTime
        Try
            dtable = objGIN.OrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPONo)
            If (dtable.Rows.Count > 0) Then
                For i = 0 To dtable.Rows.Count - 1
                    Sdate = dtable.Rows(i)("POM_OrderDate")
                    txtOrderDate.Text = Date.ParseExact(Sdate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'txtOrderDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(0)("POM_OrderDate").ToString(), "D")
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

    Protected Sub dgViewPR_PreRender(sender As Object, e As EventArgs) Handles dgViewPR.PreRender
        Dim dt As New DataTable
        Try
            If dgViewPR.Rows.Count > 0 Then
                dgViewPR.UseAccessibleHeader = True
                dgViewPR.HeaderRow.TableSection = TableRowSection.TableHeader
                dgViewPR.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgViewPR_PreRender")
        End Try
    End Sub

    Protected Sub ddlExistingTransactions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingTransactions.SelectedIndexChanged
        Try
            lblError.Text = "" : txtOrderDate.Text = ""
            ddlPurchaseInvoice.Items.Clear()
            ddlGoodsInwardNo.Items.Clear()
            If (ddlExistingTransactions.SelectedIndex > 0) Then
                LoadOrderDetails(ddlExistingTransactions.SelectedValue)
                ddlSupplier.SelectedValue = objVerification.GetSupplier(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
                LoadExistGoodsInwardNo(ddlExistingTransactions.SelectedValue)
                LoadExistGoodsBillNo(ddlExistingTransactions.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingTransactions_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadExistGoodsInwardNo(ByVal iTransactionID As Integer)
        Try
            ddlGoodsInwardNo.DataSource = objVerification.LoadInwardNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransactionID)
            ddlGoodsInwardNo.DataValueField = "PRM_ID"
            ddlGoodsInwardNo.DataTextField = "PRM_DocumentRefNo"
            ddlGoodsInwardNo.DataBind()
            ddlGoodsInwardNo.Items.Insert(0, "--- Select Document Ref NO ---")
            ddlPurchaseInvoice.DataSource = objVerification.LoadPurchaseBill(sSession.AccessCode, sSession.AccessCodeID, iTransactionID)
            ddlPurchaseInvoice.DataTextField = "PV_BillNo"
            ddlPurchaseInvoice.DataValueField = "PV_ID"
            ddlPurchaseInvoice.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistGoodsInwardNo")
        End Try
    End Sub
    Private Sub LoadExistGoodsBillNo(ByVal iTransactionID As Integer)
        Try
            ddlPurchaseInvoice.DataSource = objVerification.LoadPurchaseBill(sSession.AccessCode, sSession.AccessCodeID, iTransactionID)
            ddlPurchaseInvoice.DataTextField = "PV_BillNo"
            ddlPurchaseInvoice.DataValueField = "PV_ID"
            ddlPurchaseInvoice.DataBind()
            ddlPurchaseInvoice.Items.Insert(0, "--- Select Purchase Invoice NO ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlGoodsInwardNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGoodsInwardNo.SelectedIndexChanged
        Dim dtTab As New DataTable
        Dim dt1, dt2, dt3, dt4, dt5, dt As New DataTable
        Dim dTable1 As New DataTable
        Dim dtable2 As New DataTable
        Dim dtable3 As New DataTable
        Dim dtable4 As New DataTable
        Dim flag As Integer = 0
        Dim count As Integer = 0
        Dim Checkdate As Date
        lblError.Text = ""
        Try
            If (ddlGoodsInwardNo.SelectedIndex = 0) Then
                dgViewPI.DataSource = Nothing
                dgViewPR.DataSource = Nothing
                dgViewPR.DataBind()
                dgViewPI.DataBind()
                lblStatus.Text = ""
                Exit Sub
            End If
            If (objVerification.CheckVerificationNo(sSession.AccessCode, ddlGoodsInwardNo.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue())) Then
                lblStatus.Text = "Approved"
                Exit Sub
            Else
                lblStatus.Text = "Waiting For APproval"
            End If
            Checkdate = objVerification.getDateFromInward(sSession.AccessCode, ddlGoodsInwardNo.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue)
            If (Checkdate = "01/01/1990") Or (Checkdate = "01-01-1990") Then
                txtReceiptDate.Text = ""
            Else
                txtReceiptDate.Text = Checkdate
                'Date.ParseExact(Checkdate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                lblStatus.Text = "Waiting For APproval"
            End If
            If (objVerification.CheckRegisterNo(sSession.AccessCode, ddlGoodsInwardNo.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue)) Then
                If (objVerification.CheckInwardNo(sSession.AccessCode, ddlGoodsInwardNo.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue)) Then
                    lblError.Text = ""
                    dgViewPI.DataSource = Nothing
                    dgViewPR.DataSource = Nothing
                    dgViewPR.DataBind()
                    dgViewPI.DataBind()
                    If (ddlGoodsInwardNo.SelectedValue <> "") Then
                        dt = objVerification.GetMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlGoodsInwardNo.SelectedValue)
                    End If
                    If (ddlGoodsInwardNo.SelectedIndex > 0) Then
                        dt1 = objVerification.GetTransactionDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlGoodsInwardNo.SelectedValue, sSession.YearID, ddlExistingTransactions.SelectedValue, ddlInvoiceNo.SelectedValue)
                        dt2 = objVerification.GetTransactionDetailsPR(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlGoodsInwardNo.SelectedItem.Text, 0, ddlExistingTransactions.SelectedValue)
                        dt3 = objVerification.GetTransactionDetailsExcess(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlGoodsInwardNo.SelectedItem.Text, 0, ddlExistingTransactions.SelectedValue)
                        dt5 = objVerification.GetTransactionDetailsNewItemDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlGoodsInwardNo.SelectedItem.Text, 0, ddlExistingTransactions.SelectedValue)
                        dTable1.Merge(dt1)
                        dtable2.Merge(dt2)
                        dtable3.Merge(dt3)
                        dtable4.Merge(dt4)
                        dtable3.Merge(dt5)
                        flag = 1
                    End If
                    If (flag = 1) Then
                        dgViewPI.DataSource = dTable1
                        dgViewPR.DataSource = dtable2
                        dgViewExcess.DataSource = dtable3
                        dgViewExcess.DataBind()
                        dgViewPR.DataBind()
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGoodsInwardNo_SelectedIndexChanged")
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
        Dim oStatus As Object
        Try
            lblError.Text = ""
            lblStatus.Text = ""
            oStatus = HttpUtility.UrlEncode(objFasGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/HomePages/Home.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim BillNo As String
        Dim dt As New DataTable, dTable1 As New DataTable
        'Dim objOrder As New clsCustomerOrder
        Dim s As Integer
        Dim InwrdNo As String = "", sCTime As String = ""
        Dim iPVID As Integer
        Dim iZoneID, iRegionID, iAreaID, iBranchID As Integer, iCurrency As Integer = 0, iBaseID As Integer = 0
        Dim dtPO As New DataTable
        Dim dCValue As Double = 0, dCRate As Double
        Try
            'btnGenerate.Visible = True
            'btnGenerate.Enabled = True
            iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)

            '*** Currency ***'
            GetPurchasedItemsGrid(ddlInvoiceNo.SelectedValue)

            iCurrency = objVerification.GetCurrency(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue)
            iBaseID = objVerification.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)

            If txtBillAmount.Text <> "" Then
                If iBaseID = iCurrency Then
                    dCValue = txtBillAmount.Text
                    dCRate = 0

                    sCTime = " "
                Else
                    dCValue = objVerification.GetFERates(sSession.AccessCode, sSession.AccessCodeID, iCurrency, txtBillAmount.Text)
                    If dCValue = 0 Then
                        lblError.Text = "Please set the exchange rates in Currency Master."
                        lblUserMasterDetailsValidationMsg.Text = "Please set the exchange rates in Currency Master."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                        Exit Sub
                    End If
                    dCRate = objVerification.GetFECRates(sSession.AccessCode, sSession.AccessCodeID, iCurrency)
                    sCTime = objVerification.GetFECTime(sSession.AccessCode, sSession.AccessCodeID, iCurrency)
                    If sCTime = "" Then
                        sCTime = " "
                    End If
                End If
            End If
            '*** Currency ***'

            BillNo = "PB" & "-" & sSession.YearID & "-" & "" & Date.Now.Month & "" & objVerification.GenerateBillNo(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue)
            If (ddlGoodsInwardNo.SelectedIndex > 0) Then
                iPVID = objVerification.SavePurchaseVerification(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlExistingTransactions.SelectedValue, ddlGoodsInwardNo.SelectedValue, sSession.YearID, BillNo, 1, ddlInvoiceNo.SelectedValue)
            End If
            If iPVID > 0 Then
                LoadExistGoodsBillNoFromApprove(ddlExistingTransactions.SelectedValue)
                If (ddlGoodsInwardNo.SelectedIndex > 0) Then
                    dt = objVerification.GetTransactionDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlGoodsInwardNo.SelectedValue, 0, ddlExistingTransactions.SelectedValue, ddlInvoiceNo.SelectedValue)
                    dTable1.Merge(dt)
                    InwrdNo = objVerification.GetGINID(sSession.AccessCode, ddlGoodsInwardNo.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue)

                    objVerification.SaveStockLedger(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, sSession.UserID, dTable1, ddlExistingTransactions.SelectedValue, InwrdNo, iDefaultBranch)
                End If

                Dim dtPurchase As New DataTable
                'dtPurchase = loadDetailsB(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, iPVID)

                'If dtPurchase.Rows.Count > 0 Then
                '    For j = 0 To dtPurchase.Rows.Count - 1
                '        If j = dtPurchase.Rows.Count - 1 Then
                '            txtBillAmount.Text = dtPurchase.Rows(j)("GrandTotal")
                '            GetDefaultGridPurchase(dtPurchase.Rows(j)("GrandTotal"), dtPurchase.Rows(j)("TotalVat"), dtPurchase.Rows(j)("CSTAmtTotal"), dtPurchase.Rows(j)("TotalExiseAmt"), dtPurchase.Rows(j)("SubTotal"))
                '        End If
                '    Next
                'End If

                'dtPurchase = loadDetailsBGST(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, iPVID)

                'If dtPurchase.Rows.Count > 0 Then
                '    For j = 0 To dtPurchase.Rows.Count - 1
                '        If j = dtPurchase.Rows.Count - 1 Then
                '            txtBillAmount.Text = dtPurchase.Rows(j)("GrandTotal")
                '            GetDefaultGridPurchase(dtPurchase.Rows(j)("GrandTotal"), dtPurchase.Rows(j)("SGSTAmountTotal"), dtPurchase.Rows(j)("CGSTAmountTotal"), dtPurchase.Rows(j)("IGSTAmountTotal"), dtPurchase.Rows(j)("SubTotal"))
                '        End If
                '    Next
                'End If

                dtPO = objVerification.GetZone(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPVID)
                If dtPO.Rows.Count > 0 Then
                    iZoneID = dtPO.Rows(0)("POM_ZoneID")
                    iRegionID = dtPO.Rows(0)("POM_RegionID")
                    iAreaID = dtPO.Rows(0)("POM_AreaID")
                    iBranchID = dtPO.Rows(0)("POM_BranchID")
                End If
                'GetPurchasedItemsGrid(ddlInvoiceNo.SelectedValue)

                objVerification.UpdatePurchaseVerification(sSession.AccessCode, sSession.AccessCodeID, dCValue, iCurrency, dCRate, sCTime, iPVID)
                SavePurchaseJE(iPVID, BillNo, iZoneID, iRegionID, iAreaID, iBranchID)
                lblError.Text = "Sucessfuly Approved"
                lblStatus.Text = "Approved"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub LoadExistGoodsBillNoFromApprove(ByVal iTransactionID As Integer)
        Try
            ddlPurchaseInvoice.DataSource = objVerification.LoadPurchaseBill(sSession.AccessCode, sSession.AccessCodeID, iTransactionID)
            ddlPurchaseInvoice.DataTextField = "PV_BillNo"
            ddlPurchaseInvoice.DataValueField = "PV_ID"
            ddlPurchaseInvoice.DataBind()
            ddlPurchaseInvoice.Items.Insert(0, "--- Select Purchase Invoice NO ---")
            ddlPurchaseInvoice.SelectedValue = objVerification.getVerificationID(sSession.AccessCode, ddlGoodsInwardNo.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlPurchaseInvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPurchaseInvoice.SelectedIndexChanged
        Dim dtTab As New DataTable
        Dim dt1, dt2, dt3, dt4, dt5, dt As New DataTable
        Dim dTable1 As New DataTable
        Dim dtable2 As New DataTable
        Dim dtable3 As New DataTable
        Dim dtable4 As New DataTable
        Dim flag As Integer = 0
        Dim DoCNo As String
        Try
            If (ddlPurchaseInvoice.SelectedIndex > 0) Then
                DoCNo = objDb.SQLGetDescription(sSession.AccessCode, "select PV_DocRefNo from purchase_verification where PV_BillNo='" & ddlPurchaseInvoice.SelectedItem.Text & "' and PV_CompID = " & sSession.AccessCodeID & "")
                If (objDb.SQLCheckForRecord(sSession.AccessCode, "select * from Purchase_Registry_master where PRM_DocumentRefNo='" & DoCNo & "' and PRM_CompID = " & sSession.AccessCodeID & "")) Then
                    If (objDb.SQLCheckForRecord(sSession.AccessCode, "select * from Purchase_GIN_Master where PGM_DocumentRefNo='" & DoCNo & "' and PGM_CompID = " & sSession.AccessCodeID & "")) Then
                        lblError.Text = ""
                        dgViewPI.DataSource = Nothing
                        dgViewPR.DataSource = Nothing
                        dgViewPR.DataBind()
                        dgViewPI.DataBind()
                        If (ddlGoodsInwardNo.SelectedValue <> "") Then
                            dt = objVerification.GetMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, DoCNo)
                        End If
                        If (ddlPurchaseInvoice.SelectedIndex > 0) Then
                            objVerification.UpdateTransaction(sSession.AccessCode, sSession.AccessCodeID, DoCNo, 0)
                            dt1 = objVerification.GetTransactionDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, DoCNo, 0, ddlExistingTransactions.SelectedValue, 0)
                            dt2 = objVerification.GetTransactionDetailsPR(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, DoCNo, 0, ddlExistingTransactions.SelectedValue)
                            dt3 = objVerification.GetTransactionDetailsExcess(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, DoCNo, 0, ddlExistingTransactions.SelectedValue)
                            dt4 = objVerification.GetTransactionDetailsDiffrence(sSession.AccessCode, sSession.AccessCodeID, DoCNo, 0)
                            dt5 = objVerification.GetTransactionDetailsNewItemDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, DoCNo, 0, ddlExistingTransactions.SelectedValue)
                            dTable1.Merge(dt1)
                            dtable2.Merge(dt2)
                            dtable3.Merge(dt3)
                            dtable4.Merge(dt4)
                            dtable3.Merge(dt5)
                            flag = 1
                        End If

                        If (flag = 1) Then
                            dgViewPI.DataSource = dTable1
                            dgViewPR.DataSource = dtable2
                            dgViewExcess.DataSource = dtable3

                            dgViewExcess.DataBind()
                            dgViewPR.DataBind()
                            dgViewPI.DataBind()
                        End If
                    Else
                        lblError.Text = "Invoice not Received at Account Department"
                    End If
                Else
                    lblError.Text = "Goods not at Received"

                End If
            Else
                dgViewPI.DataSource = Nothing
                dgViewPR.DataSource = Nothing
                dgViewExcess.DataSource = Nothing
                dgViewExcess.DataBind()
                dgViewPR.DataBind()
                dgViewPI.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPurchaseInvoice_SelectedIndexChanged")
        End Try
    End Sub

    'Public Function SaveInTransction(ByVal sNameSpace As String, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails)
    '    Dim iPaymentType As Integer = 0, iTransID As Integer = 0
    '    Dim dDebit As Double = 0, dCredit As Double = 0
    '    Dim iRet As Integer = 0
    '    Dim Arr() As String
    '    Try
    '        'If txtTransactionNo.Text <> "" Then
    '        '    If (txtTransactionNo.Text).StartsWith("P") Then

    '        '    ElseIf (txtTransactionNo.Text).StartsWith("S") Then

    '        '    End If
    '        'End If
    '        '  iRet = CheckDebitAndCredit()

    '        'If iRet = 1 Then
    '        '    lblCustomerValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '        '    Exit Sub
    '        'ElseIf iRet = 2 Then
    '        '    lblCustomerValidationMsg.Text = "Amount Not Matched with Advance Payment."
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '        '    Exit Sub
    '        'ElseIf iRet = 3 Then
    '        '    lblCustomerValidationMsg.Text = "Amount Not Matched with Payment."
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '        '    Exit Sub
    '        'ElseIf iRet = 4 Then
    '        '    lblCustomerValidationMsg.Text = "Total Debit Amount Not Matched with Invoice Total Bill Amount."
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '        '    Exit Sub
    '        'ElseIf iRet = 5 Then
    '        '    lblCustomerValidationMsg.Text = "Total Credit Amount Not Matched with Invoice Total Bill Amount."
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '        '    Exit Sub
    '        'End If

    '        'If (ddlPaymentType.SelectedIndex = 1) Or (ddlPaymentType.SelectedIndex = 2) Or (ddlPaymentType.SelectedIndex = 3) Then
    '        '    If ddldbHead.SelectedIndex = 0 Then
    '        '        lblCustomerValidationMsg.Text = "Enter Debit Details."
    '        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)
    '        '        Exit Sub
    '        '    End If

    '        '    If ddlCrHead.SelectedIndex = 0 Then
    '        '        lblCustomerValidationMsg.Text = "Enter Credit Details."
    '        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)
    '        '        Exit Sub
    '        '    End If
    '        'End If

    '        If ddlExistJE.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_ID = ddlExistJE.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_ID = 0
    '        End If

    '        objPSJEDetails.sAcc_JE_TransactionNo = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "P")

    '        If ddlParty.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_Party = ddlParty.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_Party = 0
    '        End If

    '        If ddlLocation.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_Location = ddlLocation.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_Location = 0
    '        End If

    '        If ddlBillType.SelectedIndex > 0 Then
    '            objPSJEDetails.iAcc_JE_BillType = ddlBillType.SelectedValue
    '        Else
    '            objPSJEDetails.iAcc_JE_BillType = 0
    '        End If
    '        objPSJEDetails.iAcc_JE_InvoiceID = ddlBillNo.SelectedValue
    '        objPSJEDetails.sAcc_JE_BillNo = ddlBillNo.SelectedItem.Text
    '        'txtBillNo.Text
    '        objPSJEDetails.dAcc_JE_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objPSJEDetails.dAcc_JE_BillAmount = txtBillAmount.Text
    '        objPSJEDetails.iAcc_JE_YearID = sSession.YearID
    '        objPSJEDetails.sAcc_JE_Status = "W"
    '        objPSJEDetails.iAcc_JE_CreatedBy = sSession.UserID
    '        objPSJEDetails.iAcc_JE_CreatedOn = DateTime.Today
    '        objPSJEDetails.sAcc_JE_Operation = "C"
    '        objPSJEDetails.sAcc_JE_IPAddress = sSession.IPAddress
    '        objPSJEDetails.dAcc_JE_BillCreatedDate = DateTime.Today

    '        If ddlPaymentType.SelectedIndex = 1 Then
    '            objPSJEDetails.dAcc_JE_AdvanceAmount = 0.00 : objPSJEDetails.dAcc_JE_BalanceAmount = 0.00
    '            objPSJEDetails.sAcc_JE_AdvanceNaration = ""
    '        ElseIf ddlPaymentType.SelectedIndex = 3 Then
    '            objPSJEDetails.dAcc_JE_NetAmount = 0.00
    '            objPSJEDetails.sAcc_JE_PaymentNarration = ""
    '        ElseIf ddlPaymentType.SelectedIndex = 4 Then
    '            objPSJEDetails.sAcc_JE_ChequeNo = ""
    '            objPSJEDetails.sAcc_JE_IFSCCode = "" : objPSJEDetails.sAcc_JE_BankName = "" : objPSJEDetails.sAcc_JE_BranchName = ""
    '        End If

    '        objPSJEDetails.sAcc_JE_AdvanceNaration = ""
    '        objPSJEDetails.sAcc_JE_PaymentNarration = ""
    '        objPSJEDetails.sAcc_JE_ChequeNo = ""
    '        objPSJEDetails.sAcc_JE_IFSCCode = ""
    '        objPSJEDetails.sAcc_JE_BankName = ""
    '        objPSJEDetails.sAcc_JE_BranchName = ""

    '        If ddlPaymentType.SelectedIndex = "1" Then   ' Advance 
    '            iPaymentType = 1
    '            If txtAdvancePayment.Text <> "" Then
    '                objPSJEDetails.dAcc_JE_AdvanceAmount = txtAdvancePayment.Text
    '            Else
    '                objPSJEDetails.dAcc_JE_AdvanceAmount = 0.00
    '            End If
    '            objPSJEDetails.dAcc_JE_BalanceAmount = txtBalanceAmount.Text
    '            objPSJEDetails.sAcc_JE_AdvanceNaration = txtNarration.Text

    '        ElseIf ddlPaymentType.SelectedIndex = "3" Then   'Payment
    '            iPaymentType = 3
    '            If txtNarration.Text <> "" Then
    '                objPSJEDetails.dAcc_JE_NetAmount = txtNarration.Text
    '            Else
    '                objPSJEDetails.dAcc_JE_NetAmount = 0.00
    '            End If
    '            objPSJEDetails.sAcc_JE_PaymentNarration = txtNarration.Text

    '        ElseIf ddlPaymentType.SelectedIndex = "4" Then   'Cheque Details
    '            iPaymentType = 4
    '            objPSJEDetails.sAcc_JE_ChequeNo = txtChequeNo.Text
    '            objPSJEDetails.dAcc_JE_ChequeDate = txtChequeDate.Text
    '            objPSJEDetails.sAcc_JE_IFSCCode = txtIFSC.Text
    '            objPSJEDetails.sAcc_JE_BankName = txtBankName.Text
    '            objPSJEDetails.sAcc_JE_BranchName = txtBranchName.Text
    '        End If

    '        objPSJEDetails.iAcc_JE_UpdatedBy = sSession.UserID
    '        objPSJEDetails.iAcc_JE_UpdatedOn = DateTime.Today
    '        objPSJEDetails.iAcc_JE_CompID = sSession.AccessCodeID


    '        If txtTransactionNo.Text <> "" Then
    '            If objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("P") Then
    '                Arr = objPSJEDetails.SavePurchaseJournalMaster(sSession.AccessCode, objPSJEDetails)
    '                iTransID = Arr(1)
    '            ElseIf objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("S") Then
    '                Arr = objPSJEDetails.SaveSalesJournalMaster(sSession.AccessCode, objPSJEDetails)
    '                iTransID = Arr(1)
    '            End If
    '        End If

    '        For i = 0 To dgJEDetails.Items.Count - 1

    '            If objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("P") Then
    '                objPSJEDetails.iATD_TrType = 5
    '            ElseIf objPSJEDetails.sAcc_JE_TransactionNo.StartsWith("S") Then
    '                objPSJEDetails.iATD_TrType = 6
    '            End If

    '            If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
    '                objPSJEDetails.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
    '            Else
    '                objPSJEDetails.iATD_ID = 0
    '            End If

    '            objPSJEDetails.dATD_TransactionDate = DateTime.Today

    '            objPSJEDetails.iATD_BillId = iTransID
    '            objPSJEDetails.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
    '            'iPaymentType

    '            If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
    '                objPSJEDetails.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
    '            Else
    '                objPSJEDetails.iATD_Head = 0
    '            End If

    '            If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
    '                objPSJEDetails.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
    '            Else
    '                objPSJEDetails.iATD_GL = 0
    '            End If
    '            If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
    '                objPSJEDetails.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
    '            Else
    '                objPSJEDetails.iATD_SubGL = 0
    '            End If
    '            If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
    '                objPSJEDetails.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
    '            Else
    '                objPSJEDetails.dATD_Debit = 0
    '            End If
    '            If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
    '                objPSJEDetails.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
    '            Else
    '                objPSJEDetails.dATD_Credit = 0
    '            End If
    '            If objPSJEDetails.dATD_Debit > 0 And objPSJEDetails.dATD_Credit = 0 Then
    '                objPSJEDetails.iATD_DbOrCr = 1 'Debit
    '            ElseIf objPSJEDetails.dATD_Debit = 0 And objPSJEDetails.dATD_Credit > 0 Then
    '                objPSJEDetails.iATD_DbOrCr = 2 'Credit
    '            End If
    '            objPSJEDetails.iATD_CreatedBy = sSession.UserID
    '            objPSJEDetails.dATD_CreatedOn = DateTime.Today
    '            objPSJEDetails.sATD_Status = "W"
    '            objPSJEDetails.iATD_YearID = sSession.YearID
    '            objPSJEDetails.sATD_Operation = "C"
    '            objPSJEDetails.sATD_IPAddress = sSession.IPAddress
    '            objPSJEDetails.iATD_UpdatedBy = sSession.UserID
    '            objPSJEDetails.dATD_UpdatedOn = DateTime.Today
    '            objPSJEDetails.iATD_CompID = sSession.AccessCodeID
    '            objPSJEDetails.SaveUpdateTransactionDetails(sSession.AccessCode, objPSJEDetails)
    '        Next

    '        'lblCustomerValidationMsg.Text = "Successfully Saved."
    '        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

    '        'dgJEDetails.DataSource = objPSJEDetails.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID, txtTransactionNo.Text)
    '        'dgJEDetails.DataBind()

    '        'LoadExistingJEs()
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPaymentType_SelectedIndexChanged")
    '    End Try
    'End Function


    'Public Function loadDetailsB(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dt1 As New DataTable
    '    Dim dt2 As New DataTable
    '    Dim dRow As DataRow
    '    Dim dr As DataRow
    '    Dim dtDetails As New DataTable
    '    Dim flag As String = ""
    '    Dim flag1 As String = ""
    '    Dim VAT As String = "", CST As String = "", Exise As String = ""
    '    Dim Cstval As String = ""
    '    Dim Total, TotalAmt, Totaltax, TotalVat As Double
    '    Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtExAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
    '    gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
    '    Dim flag3 As Integer = 0
    '    Try
    '        dt.Columns.Add("SlNo")
    '        dt.Columns.Add("Commodity")
    '        dt.Columns.Add("Description")
    '        dt.Columns.Add("TotalQty")
    '        dt.Columns.Add("Rate")
    '        dt.Columns.Add("VAT")
    '        dt.Columns.Add("VATAmt")
    '        dt.Columns.Add("CST")
    '        dt.Columns.Add("CSTAmt")
    '        dt.Columns.Add("CSTAmtTotal")
    '        dt.Columns.Add("Exise")
    '        dt.Columns.Add("ExiseAmt")
    '        dt.Columns.Add("TotalExiseAmt")
    '        dt.Columns.Add("Discount")
    '        dt.Columns.Add("DiscountAmt")
    '        dt.Columns.Add("TotalAmount")
    '        dt.Columns.Add("ItemCode")
    '        dt.Columns.Add("SubTotal")
    '        dt.Columns.Add("GrandTotal")
    '        dt.Columns.Add("TotalVat")
    '        dt.Columns.Add("UnitId")
    '        dt.Columns.Add("AltUnit")
    '        dt.Columns.Add("INVH_MRP")
    '        dt.Columns.Add("INVH_Edate")
    '        dt.Columns.Add("INVH_Mdate")
    '        dt.Columns.Add("BatchNumber")
    '        dt.Columns.Add("POM_OrderDate")
    '        dt.Columns.Add("PGM_DocumentRefNo")
    '        dt.Columns.Add("PGM_InvoiceDate")
    '        dt.Columns.Add("gtdiscountAmt")
    '        sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_ESugamNo,e.PGM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,"
    '        sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
    '        sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PGD_BatchNumber,h.PGD_ManufactureDate,h.PGD_ExpireDate,"
    '        sSql = sSql & " d.Inv_Description Commodity,"
    '        sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
    '        sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
    '        sSql = sSql & "  from Purchase_verification"
    '        sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
    '        sSql = sSql & " join Inventory_Master_history InvH On b.PIA_HistoryID=InvH.InvH_ID"
    '        sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
    '        sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
    '        sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
    '        sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
    '        sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID "
    '        sSql = sSql & "  join Purchase_GIN_Details h On b.PIA_GINID=h.PGD_MasterID And b.PIA_OrderID=h.PGD_OrderID and PIA_HistoryID=h.PGD_HistoryID where b.PIA_CompID=" & iCompID & ""
    '        If iorder <> 0 Then
    '            sSql = sSql & " And PV_OrderNo= " & iorder & " "
    '        End If
    '        If iInvoice <> 0 Then
    '            sSql = sSql & " And PV_ID= " & iInvoice & " "
    '        End If
    '        sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
    '        dtDetails = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        For i = 0 To dtDetails.Rows.Count - 1
    '            If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
    '                dRow = dt.NewRow()
    '                Total = 0
    '                TotalAmt = 0
    '                Totaltax = 0
    '                gtExAmt = 0
    '                If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
    '                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
    '                    dRow("SlNo") = i + 1
    '                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
    '                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
    '                    End If
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
    '                    dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
    '                    gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
    '                Else
    '                    dRow("TotalQty") = 0
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
    '                    dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
    '                    gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
    '                Else
    '                    dRow("Rate") = 0
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
    '                    dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
    '                    gtdiscount = gtdiscount + dRow("Discount")
    '                Else
    '                    dRow("Discount") = "0"
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
    '                    dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) * dtDetails.Rows(i)("POD_Discount")) / 100))
    '                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
    '                Else
    '                    dRow("DiscountAmt") = "0"
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
    '                    dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
    '                    gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
    '                Else
    '                    dRow("VAT") = 0
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
    '                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
    '                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
    '                    gtExAmt = dtDetails.Rows(i)("POD_ExciseAmount")
    '                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
    '                Else
    '                    dRow("ExiseAmt") = 0
    '                    gtExAmt = 0
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
    '                    dRow("VATAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_VAT") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
    '                    Totaltax = Totaltax + dRow("VATAmt")
    '                    gtVATAmt = gtVATAmt + dRow("VATAmt")
    '                    TotalVat = TotalVat + dRow("VATAmt")
    '                Else
    '                    dRow("VATAmt") = 0
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
    '                    dRow("CST") = dtDetails.Rows(i)("POD_CST")
    '                    gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
    '                Else
    '                    dRow("CST") = 0
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
    '                    dRow("CSTAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_CST") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
    '                    gtCSTAmt = gtCSTAmt + dRow("CSTAmt")
    '                    Totaltax = Totaltax + dRow("CSTAmt")
    '                Else
    '                    dRow("CSTAmt") = 0
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
    '                    dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
    '                    gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
    '                Else
    '                    dRow("Exise") = 0
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
    '                    dRow("UnitId") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
    '                    dRow("AltUnit") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
    '                Else
    '                    dRow("UnitId") = "0"
    '                    dRow("AltUnit") = "0"
    '                End If

    '                If (dtDetails.Rows(i)("PGM_InvoiceDate").ToString() <> "") Then
    '                    If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
    '                        dRow("PGM_InvoiceDate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate").ToString(), "D")
    '                    Else
    '                        dRow("PGM_InvoiceDate") = ""
    '                    End If
    '                Else
    '                    dRow("PGM_InvoiceDate") = ""
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("PGM_DocumentRefNo")) = False Then
    '                    dRow("PGM_DocumentRefNo") = dtDetails.Rows(i)("PGM_DocumentRefNo")
    '                End If

    '                If (dtDetails.Rows(i)("PGD_ManufactureDate").ToString() <> "") Then
    '                    If IsDBNull(dtDetails.Rows(i)("PGD_ManufactureDate")) = False Then
    '                        dRow("INVH_Mdate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ManufactureDate").ToString(), "D")
    '                    Else
    '                        dRow("INVH_Mdate") = ""
    '                    End If
    '                Else
    '                    dRow("INVH_Mdate") = ""
    '                End If

    '                If (dtDetails.Rows(i)("POM_OrderDate").ToString() <> "") Then
    '                    If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
    '                        dRow("POM_OrderDate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate").ToString(), "D")
    '                    Else
    '                        dRow("POM_OrderDate") = ""
    '                    End If
    '                Else
    '                    dRow("POM_OrderDate") = ""
    '                End If
    '                If (dtDetails.Rows(i)("PGD_ExpireDate").ToString() <> "") Then
    '                    If IsDBNull(dtDetails.Rows(i)("PGD_ExpireDate")) = False Then
    '                        dRow("INVH_Edate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ExpireDate").ToString(), "D")
    '                    Else
    '                        dRow("INVH_Edate") = ""
    '                    End If
    '                Else
    '                    dRow("INVH_Edate") = ""
    '                End If

    '                If (dRow("INVH_Edate").ToString() = "30/12/1899") Then
    '                    dRow("INVH_Edate") = ""
    '                End If
    '                If (dRow("INVH_Mdate").ToString() = "30/12/1899") Then
    '                    dRow("INVH_Mdate") = ""
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
    '                    dRow("INVH_MRP") = dtDetails.Rows(i)("INVH_MRP")
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("PGD_BatchNumber")) = False Then
    '                    dRow("BatchNumber") = dtDetails.Rows(i)("PGD_BatchNumber")
    '                End If
    '                subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
    '                TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
    '                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
    '                GrandTotal = GrandTotal + TotalAmt
    '                dRow("SubTotal") = String.Format("{0:0.00}", subTotal)
    '                dRow("TotalVat") = String.Format("{0:0.00}", TotalVat)
    '                dRow("CSTAmtTotal") = String.Format("{0:0.00}", gtCSTAmt)
    '                dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal + TotalVat + gtCSTAmt + gtExiseAmt))
    '                dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
    '                dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
    '                dt.Rows.Add(dRow)
    '            End If
    '        Next
    '        dtDetails.Clear()
    '        sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,"
    '        sSql = sSql & "b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,"
    '        sSql = sSql & " b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,"
    '        sSql = sSql & "c.Inv_Description,c.Inv_Color,c.Inv_Size,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,"
    '        sSql = sSql & "d.Inv_Description Commodity	"
    '        sSql = sSql & " From Purchase_verification"
    '        sSql = sSql & " Join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef"
    '        sSql = sSql & " Join Inventory_Master_history InvH On b.PIE_HistoryID=InvH.InvH_ID"
    '        sSql = sSql & "   Join Inventory_Master c on b.PIE_Description=c.Inv_ID"
    '        sSql = sSql & "  Join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID"
    '        sSql = sSql & " Join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID "
    '        sSql = sSql & " Join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"

    '        If iorder <> 0 Then
    '            sSql = sSql & " And PV_OrderNo = " & iorder & " "
    '        End If
    '        If iInvoice <> 0 Then
    '            sSql = sSql & " And PV_ID= " & iInvoice & " "
    '        End If
    '        sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
    '        dtDetails = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        For i = 0 To dtDetails.Rows.Count - 1
    '            If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
    '                dRow = dt.NewRow()
    '                Total = 0
    '                TotalAmt = 0
    '                Totaltax = 0
    '                If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
    '                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("Inv_Description")) = False Then
    '                    dRow("SlNo") = i + 1
    '                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
    '                    If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> "") Then
    '                        dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
    '                        gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
    '                    Else
    '                        dRow("TotalQty") = 0
    '                    End If
    '                Else
    '                    dRow("TotalQty") = 0
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
    '                    dRow("Rate") = dtDetails.Rows(i)("POD_Rate")
    '                    gtMRP = gtMRP + dtDetails.Rows(i)("POD_Rate")
    '                Else
    '                    dRow("Rate") = 0
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
    '                    dRow("VAT") = dtDetails.Rows(i)("POD_VATAmount")
    '                    gtVAT = gtVAT + dtDetails.Rows(i)("POD_VATAmount")
    '                Else
    '                    dRow("VAT") = 0
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("POD_Vat")) = False Then
    '                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_Vat")))
    '                    Totaltax = Totaltax + dRow("VATAmt")
    '                    gtVATAmt = gtVATAmt + dRow("VATAmt")
    '                    TotalVat = TotalVat + dRow("VATAmt")
    '                Else
    '                    dRow("VATAmt") = 0
    '                End If

    '                If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
    '                    dRow("CST") = dtDetails.Rows(i)("POD_CST")
    '                    gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
    '                Else
    '                    dRow("CST") = 0
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
    '                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
    '                    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
    '                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
    '                Else
    '                    dRow("CSTAmt") = 0
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
    '                    dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
    '                    gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
    '                Else
    '                    dRow("Exise") = 0
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
    '                    dRow("UnitId") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
    '                    dRow("AltUnit") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
    '                Else
    '                    dRow("UnitId") = "0"
    '                    dRow("AltUnit") = "0"
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False And dtDetails.Rows(i)("POD_ExciseAmount") <> "" Then
    '                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
    '                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
    '                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
    '                Else
    '                    dRow("ExiseAmt") = 0
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False And dtDetails.Rows(i)("POD_Discount") <> "" Then
    '                    dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
    '                    gtdiscount = gtdiscount + dRow("Discount")
    '                Else
    '                    dRow("Discount") = "0"
    '                End If
    '                If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
    '                    dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
    '                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
    '                Else
    '                    dRow("DiscountAmt") = "0"
    '                End If
    '                subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
    '                TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
    '                dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
    '                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
    '                GrandTotal = GrandTotal + TotalAmt
    '                dRow("SubTotal") = subTotal
    '                dRow("TotalVat") = TotalVat
    '                dRow("CSTAmtTotal") = gtCSTAmt
    '                dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal((subTotal + TotalVat + gtCSTAmt + gtExiseAmt) - gtdiscountAmt))
    '                dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
    '                dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
    '                dt.Rows.Add(dRow)
    '            End If
    '        Next

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function loadDetailsB(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dr As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim VAT As String = "", CST As String = "", Exise As String = ""
        Dim Cstval As String = ""
        Dim Total, TotalAmt, Totaltax, TotalVat As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtExAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("CSTAmtTotal")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("TotalExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("SubTotal")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalVat")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("AltUnit")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("INVH_Mdate")
            dt.Columns.Add("BatchNumber")
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("PGM_DocumentRefNo")
            dt.Columns.Add("PGM_InvoiceDate")
            dt.Columns.Add("gtdiscountAmt")
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_ESugamNo,e.PGM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,"
            sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
            sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PGD_BatchNumber,h.PGD_ManufactureDate,h.PGD_ExpireDate,"
            sSql = sSql & " d.Inv_Description Commodity,"
            sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
            sSql = sSql & "  from Purchase_verification"
            sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            sSql = sSql & " join Inventory_Master_history InvH On b.PIA_HistoryID=InvH.InvH_ID"
            sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID "
            sSql = sSql & "  join Purchase_GIN_Details h On b.PIA_GINID=h.PGD_MasterID And b.PIA_OrderID=h.PGD_OrderID and PIA_HistoryID=h.PGD_HistoryID where b.PIA_CompID=" & iCompID & ""
            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
            dtDetails = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    gtExAmt = 0
                    If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
                        gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
                    Else
                        dRow("Rate") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) * dtDetails.Rows(i)("POD_Discount")) / 100))
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                    Else
                        dRow("VAT") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        gtExAmt = dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                        gtExAmt = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_VAT") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_CST") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        gtCSTAmt = gtCSTAmt + dRow("CSTAmt")
                        Totaltax = Totaltax + dRow("CSTAmt")
                    Else
                        dRow("CSTAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If

                    If (dtDetails.Rows(i)("PGM_InvoiceDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
                            dRow("PGM_InvoiceDate") = dtDetails.Rows(i)("PGM_InvoiceDate")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate"), "D")
                        Else
                            dRow("PGM_InvoiceDate") = ""
                        End If
                    Else
                        dRow("PGM_InvoiceDate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PGM_DocumentRefNo")) = False Then
                        dRow("PGM_DocumentRefNo") = dtDetails.Rows(i)("PGM_DocumentRefNo")
                    End If

                    If (dtDetails.Rows(i)("PGD_ManufactureDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGD_ManufactureDate")) = False Then
                            dRow("INVH_Mdate") = dtDetails.Rows(i)("PGD_ManufactureDate")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ManufactureDate"), "D")
                        Else
                            dRow("INVH_Mdate") = ""
                        End If
                    Else
                        dRow("INVH_Mdate") = ""
                    End If

                    If (dtDetails.Rows(i)("POM_OrderDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                            dRow("POM_OrderDate") = dtDetails.Rows(i)("POM_OrderDate")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
                        Else
                            dRow("POM_OrderDate") = ""
                        End If
                    Else
                        dRow("POM_OrderDate") = ""
                    End If
                    If (dtDetails.Rows(i)("PGD_ExpireDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGD_ExpireDate")) = False Then
                            dRow("INVH_Edate") = dtDetails.Rows(i)("PGD_ExpireDate")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ExpireDate"), "D")
                        Else
                            dRow("INVH_Edate") = ""
                        End If
                    Else
                        dRow("INVH_Edate") = ""
                    End If

                    If (dRow("INVH_Edate").ToString() = "30/12/1899") Then
                        dRow("INVH_Edate") = ""
                    End If
                    If (dRow("INVH_Mdate").ToString() = "30/12/1899") Then
                        dRow("INVH_Mdate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                        dRow("INVH_MRP") = dtDetails.Rows(i)("INVH_MRP")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PGD_BatchNumber")) = False Then
                        dRow("BatchNumber") = dtDetails.Rows(i)("PGD_BatchNumber")
                    End If
                    subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = String.Format("{0:0.00}", subTotal)
                    dRow("TotalVat") = String.Format("{0:0.00}", TotalVat)
                    dRow("CSTAmtTotal") = String.Format("{0:0.00}", gtCSTAmt)
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal + TotalVat + gtCSTAmt + gtExiseAmt))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dt.Rows.Add(dRow)
                End If
            Next
            dtDetails.Clear()
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,"
            sSql = sSql & "b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,"
            sSql = sSql & " b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,"
            sSql = sSql & "c.Inv_Description,c.Inv_Color,c.Inv_Size,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,"
            sSql = sSql & "d.Inv_Description Commodity	"
            sSql = sSql & " From Purchase_verification"
            sSql = sSql & " Join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef"
            sSql = sSql & " Join Inventory_Master_history InvH On b.PIE_HistoryID=InvH.InvH_ID"
            sSql = sSql & "   Join Inventory_Master c on b.PIE_Description=c.Inv_ID"
            sSql = sSql & "  Join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID"
            sSql = sSql & " Join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID "
            sSql = sSql & " Join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"

            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo = " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
            dtDetails = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Description")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> "") Then
                            dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                            gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("TotalQty") = 0
                        End If
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("POD_Rate")
                        gtMRP = gtMRP + dtDetails.Rows(i)("POD_Rate")
                    Else
                        dRow("Rate") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VATAmount")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VATAmount")
                    Else
                        dRow("VAT") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Vat")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_Vat")))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                        gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                    Else
                        dRow("CSTAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False And dtDetails.Rows(i)("POD_ExciseAmount") <> "" Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False And dtDetails.Rows(i)("POD_Discount") <> "" Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If
                    subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = subTotal
                    dRow("TotalVat") = TotalVat
                    dRow("CSTAmtTotal") = gtCSTAmt
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal((subTotal + TotalVat + gtCSTAmt + gtExiseAmt) - gtdiscountAmt))
                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dt.Rows.Add(dRow)
                End If
            Next

            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadDetailsB")
        End Try
    End Function
    Public Function loadDetailsBGST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dr As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim VAT As String = "", CST As String = "", Exise As String = ""
        Dim Cstval As String = ""
        Dim Total, TotalAmt, Totaltax, TotalVat As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtExAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Dim gtFrieght, gtGSTRate, gtGSTAmount, gtSGST, gtSGSTAmount, gtCGST, gtCGSTAmount, gtIGST, gtIGSTAmount As Double
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("CSTAmtTotal")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("TotalExiseAmt")
            dt.Columns.Add("Frieght")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")

            dt.Columns.Add("Amount")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("GSTAmountTotal")
            dt.Columns.Add("SGST")
            dt.Columns.Add("SGSTAmount")
            dt.Columns.Add("SGSTAmountTotal")
            dt.Columns.Add("CGST")
            dt.Columns.Add("CGSTAmount")
            dt.Columns.Add("CGSTAmountTotal")
            dt.Columns.Add("IGST")
            dt.Columns.Add("IGSTAmount")
            dt.Columns.Add("IGSTAmountTotal")

            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("SubTotal")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalVat")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("AltUnit")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("INVH_Mdate")
            dt.Columns.Add("BatchNumber")
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("PGM_DocumentRefNo")
            dt.Columns.Add("PGM_InvoiceDate")
            dt.Columns.Add("gtdiscountAmt")


            'sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_ESugamNo,e.PGM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,"
            'sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
            'sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PGD_BatchNumber,h.PGD_ManufactureDate,h.PGD_ExpireDate,"
            'sSql = sSql & " d.Inv_Description Commodity,"
            'sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            'sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,g.POD_Frieght,g.POD_GSTRate,g.POD_GSTAmount,g.POD_SGST,g.POD_SGSTAmount,g.POD_CGST,g.POD_CGSTAmount,g.POD_IGST,g.POD_IGSTAmount,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
            'sSql = sSql & "  from Purchase_verification"
            'sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            'sSql = sSql & " join Inventory_Master_history InvH On b.PIA_HistoryID=InvH.InvH_ID"
            'sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            'sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            'sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            'sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            'sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID "
            'sSql = sSql & "  join Purchase_GIN_Details h On b.PIA_GINID=h.PGD_MasterID And b.PIA_OrderID=h.PGD_OrderID and PIA_HistoryID=h.PGD_HistoryID where b.PIA_CompID=" & iCompID & ""

            'sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_ESugamNo,e.PGM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,PIM_PRegesterID,b.PID_MasterID,b.PID_CommodityID,b.PID_DescID,b.PID_HistoryID,b.PID_UnitID,b.PID_Quantity,b.PID_Rate,b.PID_ChargePerItem,b.PID_Discount,b.PID_DiscountAmount,b.PID_GSTRate,b.PID_GSTAmount,b.PID_SGST,b.PID_SGSTAmount,b.PID_CGST,b.PID_CGSTAmount,b.PID_IGST,b.PID_IGSTAmount,b.PID_Status,b.PID_CompID,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PGD_BatchNumber,h.PGD_ManufactureDate,h.PGD_ExpireDate, d.Inv_Description Commodity, g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount, g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate  "
            'sSql = sSql & " From Purchase_verification  "
            'sSql = sSql & " Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID "
            'sSql = sSql & " Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID "
            'sSql = sSql & " Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID "
            'sSql = sSql & " Join Inventory_Master c On b.PID_DescID=c.Inv_ID "
            'sSql = sSql & " Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID "
            'sSql = sSql & " Left Join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            'sSql = sSql & " Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID "
            'sSql = sSql & " Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID "
            'sSql = sSql & " Left Join Purchase_GIN_Details h On PIM_PRegesterID=h.PGD_MasterID And PIM_OrderID=h.PGD_OrderID and b.PID_HistoryID=h.PGD_HistoryID "
            'sSql = sSql & " where b.PID_CompID=" & iCompID & " "

            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PRM_RegistryNo,e.PRM_DocumentRefNo,e.PRM_ESugamNo,e.PRM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,PIM_PRegesterID,b.PID_MasterID,b.PID_CommodityID,b.PID_DescID,b.PID_HistoryID,b.PID_UnitID,b.PID_Quantity,b.PID_Rate,b.PID_ChargePerItem,b.PID_Discount,b.PID_DiscountAmount,b.PID_GSTRate,b.PID_GSTAmount,b.PID_SGST,b.PID_SGSTAmount,b.PID_CGST,b.PID_CGSTAmount,b.PID_IGST,b.PID_IGSTAmount,b.PID_Status,b.PID_CompID,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PRD_BatchNumber,h.PRD_ManufactureDate,h.PRD_ExpireDate, d.Inv_Description Commodity, g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount, g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
            sSql = sSql & " From Purchase_verification"
            sSql = sSql & " Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID "
            sSql = sSql & " Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID  "
            sSql = sSql & " Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID  "
            sSql = sSql & " Join Inventory_Master c On b.PID_DescID=c.Inv_ID  "
            sSql = sSql & " Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID  "
            sSql = sSql & " Join Purchase_Registry_Master e On PV_GinNo=e.PRM_ID "
            sSql = sSql & " Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID "
            sSql = sSql & " Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID "
            sSql = sSql & " Join Purchase_Registry_Details h On PIM_PRegesterID=h.PRD_MasterID And PIM_OrderID=h.PRD_OrderNo and b.PID_HistoryID=h.PRD_HistoryID "
            sSql = sSql & " where b.PID_CompID=" & iCompID & " "

            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PID_ID,b.PID_DescID"
            dtDetails = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PID_Quantity") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    gtExAmt = 0
                    If IsDBNull(dtDetails.Rows(i)("PID_CommodityID")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_DescID")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("PID_Quantity")) = False Then
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PID_Quantity")) = False Then
                        dRow("TotalQty") = dtDetails.Rows(i)("PID_Quantity")
                        gtQty = gtQty + dtDetails.Rows(i)("PID_Quantity")
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PID_Rate")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("PID_Rate")
                        gtMRP = gtMRP + dtDetails.Rows(i)("PID_Rate")
                    Else
                        dRow("Rate") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_ChargePerItem")) = False Then
                        dRow("Frieght") = dtDetails.Rows(i)("PID_ChargePerItem")
                        gtFrieght = gtFrieght + dRow("Frieght")
                    Else
                        dRow("Frieght") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_Discount")) = False Then
                        dRow("Discount") = dtDetails.Rows(i)("PID_Discount")
                        'objDb.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dtDetails.Rows(i)("PID_Discount") & " And Mas_Master In(Select Mas_ID From Acc_Master_Type Where Mas_Type='Discount')")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_DiscountAmount")) = False Then
                        dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) * dRow("Discount")) / 100))
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If

                    '************'

                    If IsDBNull(dtDetails.Rows(i)("PID_GSTRate")) = False Then
                        dRow("GSTRate") = dtDetails.Rows(i)("PID_GSTRate")
                        gtGSTRate = gtGSTRate + dRow("GSTRate")
                    Else
                        dRow("GSTRate") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_GSTRate")) = False Then
                        dRow("GSTAmount") = dtDetails.Rows(i)("PID_GSTAmount")
                        gtGSTAmount = gtGSTAmount + dRow("GSTAmount")
                    Else
                        dRow("GSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_SGST")) = False Then
                        dRow("SGST") = dtDetails.Rows(i)("PID_SGST")
                        gtSGST = gtSGST + dRow("SGST")
                    Else
                        dRow("SGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_SGST")) = False Then
                        dRow("SGSTAmount") = dtDetails.Rows(i)("PID_SGSTAmount")
                        gtSGSTAmount = gtSGSTAmount + dRow("SGSTAmount")
                    Else
                        dRow("SGSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_CGST")) = False Then
                        dRow("CGST") = dtDetails.Rows(i)("PID_CGST")
                        gtCGST = gtCGST + dRow("CGST")
                    Else
                        dRow("CGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_CGST")) = False Then
                        dRow("CGSTAmount") = dtDetails.Rows(i)("PID_CGSTAmount")
                        gtCGSTAmount = gtCGSTAmount + dRow("CGSTAmount")
                    Else
                        dRow("CGSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_IGST")) = False Then
                        dRow("IGST") = dtDetails.Rows(i)("PID_IGST")
                        gtIGST = gtIGST + dRow("IGST")
                    Else
                        dRow("IGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_IGST")) = False Then
                        dRow("IGSTAmount") = dtDetails.Rows(i)("PID_IGSTAmount")
                        gtIGSTAmount = gtIGSTAmount + dRow("IGSTAmount")
                    Else
                        dRow("IGSTAmount") = "0"
                    End If

                    '************'


                    If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                    Else
                        dRow("VAT") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        gtExAmt = dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                        gtExAmt = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_VAT") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_CST") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        gtCSTAmt = gtCSTAmt + dRow("CSTAmt")
                        Totaltax = Totaltax + dRow("CSTAmt")
                    Else
                        dRow("CSTAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_HistoryID")) = False Then
                        dRow("UnitId") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PID_HistoryID") & "')")
                        dRow("AltUnit") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PID_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If

                    If (dtDetails.Rows(i)("PRM_InvoiceDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PRM_InvoiceDate")) = False Then
                            dRow("PGM_InvoiceDate") = dtDetails.Rows(i)("PRM_InvoiceDate")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate"), "D")
                        Else
                            dRow("PGM_InvoiceDate") = ""
                        End If
                    Else
                        dRow("PGM_InvoiceDate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PRM_DocumentRefNo")) = False Then
                        dRow("PGM_DocumentRefNo") = dtDetails.Rows(i)("PRM_DocumentRefNo")
                    End If

                    If (dtDetails.Rows(i)("PRD_ManufactureDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PRD_ManufactureDate")) = False Then
                            dRow("INVH_Mdate") = dtDetails.Rows(i)("PRD_ManufactureDate")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ManufactureDate"), "D")
                        Else
                            dRow("INVH_Mdate") = ""
                        End If
                    Else
                        dRow("INVH_Mdate") = ""
                    End If

                    If (dtDetails.Rows(i)("POM_OrderDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                            dRow("POM_OrderDate") = dtDetails.Rows(i)("POM_OrderDate")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
                        Else
                            dRow("POM_OrderDate") = ""
                        End If
                    Else
                        dRow("POM_OrderDate") = ""
                    End If
                    If (dtDetails.Rows(i)("PRD_ExpireDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PRD_ExpireDate")) = False Then
                            dRow("INVH_Edate") = dtDetails.Rows(i)("PRD_ExpireDate")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ExpireDate"), "D")
                        Else
                            dRow("INVH_Edate") = ""
                        End If
                    Else
                        dRow("INVH_Edate") = ""
                    End If

                    If (dRow("INVH_Edate").ToString() = "30/12/1899") Then
                        dRow("INVH_Edate") = ""
                    End If
                    If (dRow("INVH_Mdate").ToString() = "30/12/1899") Then
                        dRow("INVH_Mdate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                        dRow("INVH_MRP") = dtDetails.Rows(i)("INVH_MRP")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PRD_BatchNumber")) = False Then
                        dRow("BatchNumber") = dtDetails.Rows(i)("PRD_BatchNumber")
                    End If
                    subTotal = subTotal + String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")))
                    TotalAmt = Totaltax + (((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt"))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = String.Format("{0:0.00}", subTotal)
                    dRow("TotalVat") = String.Format("{0:0.00}", TotalVat)
                    dRow("CSTAmtTotal") = String.Format("{0:0.00}", gtCSTAmt)
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal + gtGSTAmount))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)

                    dRow("GSTAmountTotal") = String.Format("{0:0.00}", gtGSTAmount)
                    dRow("SGSTAmountTotal") = String.Format("{0:0.00}", gtSGSTAmount)
                    dRow("CGSTAmountTotal") = String.Format("{0:0.00}", gtCGSTAmount)
                    dRow("IGSTAmountTotal") = String.Format("{0:0.00}", gtIGSTAmount)

                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dt.Rows.Add(dRow)
                End If
            Next
            dtDetails.Clear()
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,"
            sSql = sSql & "b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,"
            sSql = sSql & " b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,"
            sSql = sSql & "c.Inv_Description,c.Inv_Color,c.Inv_Size,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,"
            sSql = sSql & "d.Inv_Description Commodity	"
            sSql = sSql & " From Purchase_verification"
            sSql = sSql & " Join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef"
            sSql = sSql & " Join Inventory_Master_history InvH On b.PIE_HistoryID=InvH.InvH_ID"
            sSql = sSql & "   Join Inventory_Master c on b.PIE_Description=c.Inv_ID"
            sSql = sSql & "  Join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID"
            sSql = sSql & " Join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID "
            sSql = sSql & " Join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"

            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo = " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
            dtDetails = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Description")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> "") Then
                            dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                            gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("TotalQty") = 0
                        End If
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("POD_Rate")
                        gtMRP = gtMRP + dtDetails.Rows(i)("POD_Rate")
                    Else
                        dRow("Rate") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VATAmount")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VATAmount")
                    Else
                        dRow("VAT") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Vat")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_Vat")))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                        gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                    Else
                        dRow("CSTAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDb.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False And dtDetails.Rows(i)("POD_ExciseAmount") <> "" Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Frieght")) = False And dtDetails.Rows(i)("POD_Frieght") <> "" Then
                        dRow("Frieght") = dtDetails.Rows(i)("POD_Frieght")
                        gtFrieght = gtFrieght + dRow("Frieght")
                    Else
                        dRow("Frieght") = "0"
                    End If


                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False And dtDetails.Rows(i)("POD_Discount") <> "" Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If

                    '************'

                    If IsDBNull(dtDetails.Rows(i)("POD_GSTRate")) = False Then
                        dRow("GSTRate") = dtDetails.Rows(i)("POD_GSTRate")
                        gtGSTRate = gtGSTRate + dRow("GSTRate")
                    Else
                        dRow("GSTRate") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_GSTRate")) = False Then
                        dRow("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")) * dtDetails.Rows(i)("POD_GSTRate")) / 100))
                        gtGSTAmount = gtGSTAmount + dRow("GSTAmount")
                    Else
                        dRow("GSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_SGST")) = False Then
                        dRow("SGST") = dtDetails.Rows(i)("POD_SGST")
                        gtSGST = gtSGST + dRow("SGST")
                    Else
                        dRow("SGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_SGST")) = False Then
                        dRow("SGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")) * dtDetails.Rows(i)("POD_SGST")) / 100))
                        gtSGSTAmount = gtSGSTAmount + dRow("SGSTAmount")
                    Else
                        dRow("SGSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CGST")) = False Then
                        dRow("CGST") = dtDetails.Rows(i)("POD_CGST")
                        gtCGST = gtCGST + dRow("CGST")
                    Else
                        dRow("CGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CGST")) = False Then
                        dRow("CGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")) * dtDetails.Rows(i)("POD_CGST")) / 100))
                        gtCGSTAmount = gtCGSTAmount + dRow("CGSTAmount")
                    Else
                        dRow("CGSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_IGST")) = False Then
                        dRow("IGST") = dtDetails.Rows(i)("POD_IGST")
                        gtIGST = gtIGST + dRow("IGST")
                    Else
                        dRow("IGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_IGST")) = False Then
                        dRow("IGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")) * dtDetails.Rows(i)("POD_IGST")) / 100))
                        gtIGSTAmount = gtIGSTAmount + dRow("IGSTAmount")
                    Else
                        dRow("IGSTAmount") = "0"
                    End If

                    '************'

                    subTotal = subTotal + (((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + (((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt"))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = subTotal
                    dRow("TotalVat") = TotalVat
                    dRow("CSTAmtTotal") = gtCSTAmt
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal + gtGSTAmount))
                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)

                    dRow("GSTAmountTotal") = String.Format("{0:0.00}", gtGSTAmount)
                    dRow("SGSTAmountTotal") = String.Format("{0:0.00}", gtSGSTAmount)
                    dRow("CGSTAmountTotal") = String.Format("{0:0.00}", gtCGSTAmount)
                    dRow("IGSTAmountTotal") = String.Format("{0:0.00}", gtIGSTAmount)
                    dt.Rows.Add(dRow)
                End If
            Next

            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadDetailsBGST")
        End Try
    End Function
    'Public Sub GetDefaultGridPurchase(ByVal dTotal As Double, ByVal dVATAmt As Double, ByVal dCSTAmt As Double, ByVal dExciseAmt As Double, ByVal dBasicPrice As Double)
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim sGL As String = "" : Dim sSubGL As String = ""
    '    Dim sArray As Array
    '    Dim iParty As Integer = 0
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("HeadID")
    '        dt.Columns.Add("GLID")
    '        dt.Columns.Add("SubGLID")
    '        dt.Columns.Add("PaymentID")
    '        dt.Columns.Add("SrNo")
    '        dt.Columns.Add("Type")
    '        dt.Columns.Add("GLCode")
    '        dt.Columns.Add("GLDescription")
    '        dt.Columns.Add("SubGL")
    '        dt.Columns.Add("SubGLDescription")
    '        dt.Columns.Add("OpeningBalance")
    '        dt.Columns.Add("Debit")
    '        dt.Columns.Add("Credit")
    '        dt.Columns.Add("Balance")

    '        iParty = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)

    '        dRow = dt.NewRow

    '        dRow("Id") = 0
    '        dRow("HeadID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_Head")
    '        dRow("GLID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_GL")
    '        dRow("SubGLID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_SubGL")
    '        dRow("PaymentID") = 5
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "Bill Amount"

    '        'sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        'If sGL <> "" Then
    '        '    sArray = sGL.Split("-")
    '        '    dRow("GLCode") = sArray(0)
    '        '    dRow("GLDescription") = sArray(1)
    '        'End If

    '        'sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        'If sSubGL <> "" Then
    '        '    sArray = sSubGL.Split("-")
    '        '    dRow("SubGL") = sArray(0)
    '        '    dRow("SubGLDescription") = sArray(1)
    '        'End If

    '        dRow("GLCode") = ""
    '        dRow("GLDescription") = ""

    '        dRow("Debit") = dBasicPrice
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'VAT

    '        dRow("Id") = 0
    '        dRow("HeadID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
    '        dRow("GLID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
    '        dRow("SubGLID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_SubGL")
    '        dRow("PaymentID") = 6
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "SGST"

    '        'sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        'If sGL <> "" Then
    '        '    sArray = sGL.Split("-")
    '        '    dRow("GLCode") = sArray(0)
    '        '    dRow("GLDescription") = sArray(1)
    '        'End If

    '        'sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        'If sSubGL <> "" Then
    '        '    sArray = sSubGL.Split("-")
    '        '    dRow("SubGL") = sArray(0)
    '        '    dRow("SubGLDescription") = sArray(1)
    '        'End If

    '        dRow("GLCode") = ""
    '        dRow("GLDescription") = ""

    '        dRow("Debit") = dVATAmt
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'CST

    '        dRow("Id") = 0
    '        dRow("HeadID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "CGST", "Acc_Head")
    '        dRow("GLID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "CGST", "Acc_GL")
    '        dRow("SubGLID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "CGST", "Acc_SubGL")
    '        dRow("PaymentID") = 7
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "CGST"

    '        'sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        'If sGL <> "" Then
    '        '    sArray = sGL.Split("-")
    '        '    dRow("GLCode") = sArray(0)
    '        '    dRow("GLDescription") = sArray(1)
    '        'End If

    '        'sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        'If sSubGL <> "" Then
    '        '    sArray = sSubGL.Split("-")
    '        '    dRow("SubGL") = sArray(0)
    '        '    dRow("SubGLDescription") = sArray(1)
    '        'End If

    '        dRow("SubGL") = ""
    '        dRow("SubGLDescription") = ""

    '        dRow("Debit") = dCSTAmt
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'Excise

    '        dRow("Id") = 0
    '        dRow("HeadID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "IGST", "Acc_Head")
    '        dRow("GLID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "IGST", "Acc_GL")
    '        dRow("SubGLID") = 0
    '        'objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "IGST", "Acc_SubGL")
    '        dRow("PaymentID") = 8
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "IGST"

    '        'sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        'If sGL <> "" Then
    '        '    sArray = sGL.Split("-")
    '        '    dRow("GLCode") = sArray(0)
    '        '    dRow("GLDescription") = sArray(1)
    '        'End If

    '        'sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '        'If sSubGL <> "" Then
    '        '    sArray = sSubGL.Split("-")
    '        '    dRow("SubGL") = sArray(0)
    '        '    dRow("SubGLDescription") = sArray(1)
    '        'End If

    '        dRow("SubGL") = ""
    '        dRow("SubGLDescription") = ""

    '        dRow("Debit") = dExciseAmt
    '        dRow("Credit") = 0

    '        dt.Rows.Add(dRow)

    '        dRow = dt.NewRow 'Party/Customer

    '        dRow("Id") = 0
    '        dRow("HeadID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
    '        dRow("GLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
    '        dRow("SubGLID") = objVerification.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
    '        dRow("PaymentID") = 9
    '        dRow("SrNo") = dt.Rows.Count + 1
    '        dRow("Type") = "Party/Customer"

    '        sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '        If sGL <> "" Then
    '            sArray = sGL.Split("-")
    '            dRow("GLCode") = sArray(0)
    '            dRow("GLDescription") = sArray(1)
    '        End If

    '        sSubGL = objVerification.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
    '        If sSubGL <> "" Then
    '            sArray = sSubGL.Split("-")
    '            dRow("SubGL") = sArray(0)
    '            dRow("SubGLDescription") = sArray(1)
    '        End If
    '        dRow("Debit") = 0
    '        dRow("Credit") = dTotal

    '        dt.Rows.Add(dRow)

    '        dgJEDetails.DataSource = dt
    '        dgJEDetails.DataBind()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetDefaultGridPurchase")
    '    End Try
    'End Sub
    Private Sub SavePurchaseJE(ByVal iPVID As Integer, ByVal sBillNo As String, ByVal iZoneID As Integer, ByVal iRegionID As Integer, ByVal iAreaID As Integer, ByVal iBranchID As Integer)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim objJE As New ClsPurchaseSalesJE
        Try
            objVerification.iAcc_JE_ID = 0
            objVerification.sAcc_JE_TransactionNo = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "P")
            objVerification.iAcc_JE_Party = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            objVerification.iAcc_JE_Location = objVerification.GetBranchID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPVID)
            objVerification.iAcc_JE_BillType = 0

            objVerification.iAcc_JE_InvoiceID = iPVID
            objVerification.sAcc_JE_BillNo = sBillNo

            objVerification.dAcc_JE_BillDate = txtReceiptDate.Text

            objVerification.dAcc_JE_BillAmount = txtBillAmount.Text
            objVerification.iAcc_JE_YearID = sSession.YearID
            objVerification.sAcc_JE_Status = "W"
            objVerification.iAcc_JE_CreatedBy = sSession.UserID
            objVerification.iAcc_JE_CreatedOn = DateTime.Today
            objVerification.sAcc_JE_Operation = "C"
            objVerification.sAcc_JE_IPAddress = sSession.IPAddress
            objVerification.dAcc_JE_BillCreatedDate = DateTime.Today
            objVerification.sAcc_JE_AdvanceNaration = ""
            objVerification.sAcc_JE_PaymentNarration = ""
            objVerification.sAcc_JE_ChequeNo = ""
            objVerification.sAcc_JE_IFSCCode = ""
            objVerification.sAcc_JE_BankName = ""
            objVerification.sAcc_JE_BranchName = ""

            objVerification.iAcc_JE_UpdatedBy = sSession.UserID
            objVerification.iAcc_JE_UpdatedOn = DateTime.Today
            objVerification.iAcc_JE_CompID = sSession.AccessCodeID

            objVerification.dAcc_JE_PendingAmount = txtBillAmount.Text
            objVerification.sAcc_PJE_Type = "PI"

            Arr = objVerification.SavePurchaseJournalMaster(sSession.AccessCode, objVerification)
            iTransID = Arr(1)

            For i = 0 To dgJEDetails.Items.Count - 1

                objVerification.iATD_TrType = 5

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objVerification.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objVerification.iATD_ID = 0
                End If

                objVerification.dATD_TransactionDate = DateTime.Today

                objVerification.iATD_BillId = iTransID
                objVerification.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
                'iPaymentType

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objVerification.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objVerification.iATD_Head = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objVerification.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objVerification.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objVerification.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objVerification.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objVerification.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objVerification.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objVerification.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objVerification.dATD_Credit = 0
                End If

                If objVerification.dATD_Debit > 0 And objVerification.dATD_Credit = 0 Then
                    objVerification.iATD_DbOrCr = 1 'Debit
                ElseIf objVerification.dATD_Debit = 0 And objVerification.dATD_Credit > 0 Then
                    objVerification.iATD_DbOrCr = 2 'Credit
                End If

                objVerification.iATD_CreatedBy = sSession.UserID
                objVerification.dATD_CreatedOn = DateTime.Today

                objVerification.sATD_Status = "W"
                objVerification.iATD_YearID = sSession.YearID
                objVerification.sATD_Operation = "C"
                objVerification.sATD_IPAddress = sSession.IPAddress

                objVerification.iATD_UpdatedBy = sSession.UserID
                objVerification.dATD_UpdatedOn = DateTime.Today

                objVerification.iATD_CompID = sSession.AccessCodeID

                objVerification.iATD_ZoneID = iZoneID
                objVerification.iATD_RegionID = iRegionID
                objVerification.iATD_AreaID = iAreaID
                objVerification.iATD_BranchID = iBranchID

                objVerification.dATD_OpenDebit = "0.00"
                objVerification.dATD_OpenCredit = "0.00"
                objVerification.dATD_ClosingDebit = "0.00"
                objVerification.dATD_ClosingCredit = "0.00"
                objVerification.iATD_SeqReferenceNum = 0

                objVerification.SaveUpdateTransactionDetails(sSession.AccessCode, objVerification)
            Next

            lblUserMasterDetailsValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            dgJEDetails.DataSource = objVerification.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID, objVerification.sAcc_JE_TransactionNo)
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SavePurchaseJE")
        End Try
    End Sub
    Private Sub ddlInvoiceNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInvoiceNo.SelectedIndexChanged
        Dim dt As New DataTable
        'Dim bCheck As Boolean
        Try
            'bCheck = objVerification.AllGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'If bCheck = True Then
            'Else
            '    lblError.Text = "Set The Application Settings for All GST Rates."
            '    Exit Sub
            'End If

            dt = objVerification.GetExistingDate(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedValue)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If IsDBNull(dt.Rows(i)("PIM_OrderID")) = False Then
                        ddlExistingTransactions.SelectedValue = dt.Rows(i)("PIM_OrderID")
                    Else
                        ddlExistingTransactions.SelectedIndex = 0
                    End If
                    If IsDBNull(dt.Rows(i)("PIM_OrderDate")) = False Then
                        txtOrderDate.Text = dt.Rows(i)("PIM_OrderDate")
                    Else
                        txtOrderDate.Text = ""
                    End If
                    If IsDBNull(dt.Rows(i)("PIM_PRegesterID")) = False Then
                        LoadExistGoodsInwardNo(ddlExistingTransactions.SelectedValue)
                        ddlGoodsInwardNo.SelectedValue = dt.Rows(i)("PIM_PRegesterID")
                    Else
                        ddlGoodsInwardNo.Items.Clear()
                    End If
                    If IsDBNull(dt.Rows(i)("PIM_InvoiceDate")) = False Then
                        txtReceiptDate.Text = dt.Rows(i)("PIM_InvoiceDate")
                    Else
                        txtReceiptDate.Text = ""
                    End If
                    If IsDBNull(dt.Rows(i)("PIM_SupplierID")) = False Then
                        ddlSupplier.SelectedValue = dt.Rows(i)("PIM_SupplierID")
                    Else
                        ddlSupplier.SelectedIndex = 0
                    End If
                Next
                ddlGoodsInwardNo_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInvoiceNo_SelectedIndexChanged")
        End Try
    End Sub
    'Public Sub GetPurchasedItemsGrid(ByVal iInvoiceID As Integer)
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim sGL As String = "" : Dim sSubGL As String = ""
    '    Dim sArray As Array
    '    Dim iParty As Integer = 0

    '    Dim dt1 As New DataTable
    '    Dim dPartyTotal As Double
    '    Dim dtGSTRates As New DataTable : Dim sSql As String = ""
    '    Dim dTotalAmt, dSGSTAmt, dCGSTAmt, dIGSTAmt As Double
    '    Dim SGST, CGST, IGST As Double
    '    Dim sTypeOfBill As String = ""
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("HeadID")
    '        dt.Columns.Add("GLID")
    '        dt.Columns.Add("SubGLID")
    '        dt.Columns.Add("PaymentID")
    '        dt.Columns.Add("SrNo")
    '        dt.Columns.Add("Type")
    '        dt.Columns.Add("GLCode")
    '        dt.Columns.Add("GLDescription")
    '        dt.Columns.Add("SubGL")
    '        dt.Columns.Add("SubGLDescription")
    '        dt.Columns.Add("OpeningBalance")
    '        dt.Columns.Add("Debit")
    '        dt.Columns.Add("Credit")
    '        dt.Columns.Add("Balance")

    '        'iParty = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
    '        iParty = ddlSupplier.SelectedValue

    '        sTypeOfBill = objDb.SQLGetDescription(sSession.AccessCode, "Select PIM_InvoiceStatus From Purchase_Invoice_Master Where PIM_ID=" & iInvoiceID & " And PIM_CompID=" & sSession.AccessCodeID & " And PIM_YearID=" & sSession.YearID & " ")

    '        sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & sSession.AccessCodeID & " And GST_YearID=" & sSession.YearID & " "
    '        dtGSTRates = objDb.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)


    '        If dtGSTRates.Rows.Count > 0 Then
    '            For k = 0 To dtGSTRates.Rows.Count - 1

    '                dt1 = objDb.SQLExecuteDataSet(sSession.AccessCode, "Select * From PI_Accepted_Details Where PID_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And PID_MasterID=" & iInvoiceID & " And PID_CompID=" & sSession.AccessCodeID & " ").Tables(0)
    '                If dt1.Rows.Count > 0 Then
    '                    For z = 0 To dt1.Rows.Count - 1
    '                        dTotalAmt = dTotalAmt + dt1.Rows(z)("PID_Amount")
    '                        dSGSTAmt = dSGSTAmt + dt1.Rows(z)("PID_SGSTAmount")
    '                        dCGSTAmt = dCGSTAmt + dt1.Rows(z)("PID_CGSTAmount")
    '                        dIGSTAmt = dIGSTAmt + dt1.Rows(z)("PID_IGSTAmount")
    '                        dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("PID_FinalTotal"))
    '                    Next

    '                    dRow = dt.NewRow 'Item Name
    '                    dRow("Id") = 0
    '                    dRow("HeadID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, dtGSTRates.Rows(k)("GST_GSTRate"), dtGSTRates.Rows(k)("GST_GSTRate"), "Acc_Head", sTypeOfBill)
    '                    dRow("GLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, dtGSTRates.Rows(k)("GST_GSTRate"), dtGSTRates.Rows(k)("GST_GSTRate"), "Acc_GL", sTypeOfBill)
    '                    dRow("SubGLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, dtGSTRates.Rows(k)("GST_GSTRate"), dtGSTRates.Rows(k)("GST_GSTRate"), "Acc_SubGL", sTypeOfBill)
    '                    dRow("PaymentID") = 5
    '                    dRow("SrNo") = dt.Rows.Count + 1
    '                    dRow("Type") = "Purchase Of Material"

    '                    sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '                    If sGL <> "" Then
    '                        sArray = sGL.Split("-")
    '                        dRow("GLCode") = sArray(0)
    '                        dRow("GLDescription") = sArray(1)
    '                    End If

    '                    sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '                    If sSubGL <> "" Then
    '                        sArray = sSubGL.Split("-")
    '                        dRow("SubGL") = sArray(0)
    '                        dRow("SubGLDescription") = sArray(1)
    '                    End If

    '                    dRow("Debit") = dTotalAmt
    '                    dRow("Credit") = 0
    '                    dt.Rows.Add(dRow)


    '                    SGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
    '                    CGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
    '                    IGST = dtGSTRates.Rows(k)("GST_GSTRate")

    '                    dRow = dt.NewRow 'SGST
    '                    dRow("Id") = 0
    '                    dRow("HeadID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "SGST", SGST, "Acc_Head", sTypeOfBill)
    '                    dRow("GLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "SGST", SGST, "Acc_GL", sTypeOfBill)
    '                    dRow("SubGLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "SGST", SGST, "Acc_SubGL", sTypeOfBill)
    '                    dRow("PaymentID") = 6
    '                    dRow("SrNo") = dt.Rows.Count + 1
    '                    dRow("Type") = "SGST"

    '                    sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '                    If sGL <> "" Then
    '                        sArray = sGL.Split("-")
    '                        dRow("GLCode") = sArray(0)
    '                        dRow("GLDescription") = sArray(1)
    '                    End If

    '                    sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '                    If sSubGL <> "" Then
    '                        sArray = sSubGL.Split("-")
    '                        dRow("SubGL") = sArray(0)
    '                        dRow("SubGLDescription") = sArray(1)
    '                    End If

    '                    dRow("Debit") = dSGSTAmt
    '                    dRow("Credit") = 0
    '                    dt.Rows.Add(dRow)

    '                    dRow = dt.NewRow 'CGST
    '                    dRow("Id") = 0
    '                    dRow("HeadID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "CGST", CGST, "Acc_Head", sTypeOfBill)
    '                    dRow("GLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "CGST", CGST, "Acc_GL", sTypeOfBill)
    '                    dRow("SubGLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "CGST", CGST, "Acc_SubGL", sTypeOfBill)
    '                    dRow("PaymentID") = 7
    '                    dRow("SrNo") = dt.Rows.Count + 1
    '                    dRow("Type") = "CGST"

    '                    sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '                    If sGL <> "" Then
    '                        sArray = sGL.Split("-")
    '                        dRow("GLCode") = sArray(0)
    '                        dRow("GLDescription") = sArray(1)
    '                    End If

    '                    sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '                    If sSubGL <> "" Then
    '                        sArray = sSubGL.Split("-")
    '                        dRow("SubGL") = sArray(0)
    '                        dRow("SubGLDescription") = sArray(1)
    '                    End If

    '                    dRow("Debit") = dCGSTAmt
    '                    dRow("Credit") = 0
    '                    dt.Rows.Add(dRow)

    '                    dRow = dt.NewRow 'IGST
    '                    dRow("Id") = 0
    '                    dRow("HeadID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "IGST", IGST, "Acc_Head", sTypeOfBill)
    '                    dRow("GLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "IGST", IGST, "Acc_GL", sTypeOfBill)
    '                    dRow("SubGLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "IGST", IGST, "Acc_SubGL", sTypeOfBill)
    '                    dRow("PaymentID") = 8
    '                    dRow("SrNo") = dt.Rows.Count + 1
    '                    dRow("Type") = "IGST"

    '                    sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '                    If sGL <> "" Then
    '                        sArray = sGL.Split("-")
    '                        dRow("GLCode") = sArray(0)
    '                        dRow("GLDescription") = sArray(1)
    '                    End If

    '                    sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
    '                    If sSubGL <> "" Then
    '                        sArray = sSubGL.Split("-")
    '                        dRow("SubGL") = sArray(0)
    '                        dRow("SubGLDescription") = sArray(1)
    '                    End If

    '                    dRow("Debit") = dIGSTAmt
    '                    dRow("Credit") = 0
    '                    dt.Rows.Add(dRow)

    '                    dTotalAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0 : dIGSTAmt = 0
    '                End If

    '            Next

    '            dRow = dt.NewRow 'Party/Customer
    '            dRow("Id") = 0
    '            dRow("HeadID") = objVerification.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
    '            dRow("GLID") = objVerification.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
    '            dRow("SubGLID") = objVerification.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
    '            dRow("PaymentID") = 9
    '            dRow("SrNo") = dt.Rows.Count + 1
    '            dRow("Type") = "Party/Customer"

    '            sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
    '            If sGL <> "" Then
    '                sArray = sGL.Split("-")
    '                dRow("GLCode") = sArray(0)
    '                dRow("GLDescription") = sArray(1)
    '            End If

    '            sSubGL = objVerification.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
    '            If sSubGL <> "" Then
    '                sArray = sSubGL.Split("-")
    '                dRow("SubGL") = sArray(0)
    '                dRow("SubGLDescription") = sArray(1)
    '            End If
    '            dRow("Debit") = 0
    '            dRow("Credit") = dPartyTotal

    '            txtBillAmount.Text = dPartyTotal

    '            dt.Rows.Add(dRow)

    '        End If

    '        dgJEDetails.DataSource = dt
    '        dgJEDetails.DataBind()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetDefaultGridPurchase")
    '    End Try
    'End Sub
    Public Sub GetPurchasedItemsGrid(ByVal iInvoiceID As Integer)
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
        Dim sGSTCategory As String = ""
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
            iParty = ddlSupplier.SelectedValue

            sTypeOfBill = objDb.SQLGetDescription(sSession.AccessCode, "Select PIM_InvoiceStatus From Purchase_Invoice_Master Where PIM_ID=" & iInvoiceID & " And PIM_CompID=" & sSession.AccessCodeID & " And PIM_YearID=" & sSession.YearID & " ")
            sState = objDb.SQLGetDescription(sSession.AccessCode, "Select PIM_State From Purchase_Invoice_Master Where PIM_ID=" & iInvoiceID & " And PIM_CompID=" & sSession.AccessCodeID & " And PIM_YearID=" & sSession.YearID & " ")

            sGSTCategory = objDb.SQLGetDescription(sSession.AccessCode, "Select GC_GSTCategory From GSTCategory_Table Where GC_ID in (Select PIM_GSTNCategory From Purchase_Invoice_Master Where PIM_ID=" & iInvoiceID & " And PIM_CompID=" & sSession.AccessCodeID & " And PIM_YearID=" & sSession.YearID & ")")

            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & sSession.AccessCodeID & " "
            dtGSTRates = objDb.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)

            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'

            If dtGSTRates.Rows.Count > 0 Then
                For k = 0 To dtGSTRates.Rows.Count - 1

                    dt1 = objDb.SQLExecuteDataSet(sSession.AccessCode, "Select * From PI_Accepted_Details Where PID_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And PID_MasterID=" & iInvoiceID & " And PID_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For z = 0 To dt1.Rows.Count - 1
                            dTotalAmt = dTotalAmt + dt1.Rows(z)("PID_Amount")
                            dSGSTAmt = dSGSTAmt + dt1.Rows(z)("PID_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dt1.Rows(z)("PID_CGSTAmount")
                            dIGSTAmt = dIGSTAmt + dt1.Rows(z)("PID_IGSTAmount")
                            dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("PID_FinalTotal"))
                        Next

                        If UCase(sGSTCategory) = "UNRIGISTERED DEALER" Or UCase(sGSTCategory) = "COMPOSITION DEALER" Then
                            dSGSTAmt = 0
                            dCGSTAmt = 0
                            dIGSTAmt = 0
                        End If

                        dRow = dt.NewRow 'Item Name
                        dRow("Id") = 0
                        dRow("HeadID") = objVerification.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Purchase Of Product " & sState)
                        dRow("GLID") = objVerification.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Purchase Of Product " & sState)
                        If sTypeOfBill = "Local" Then
                            dRow("SubGLID") = objVerification.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Local GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
                        ElseIf sTypeOfBill = "Inter State" Then
                            dRow("SubGLID") = objVerification.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Inter State GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
                        End If
                        dRow("PaymentID") = 5
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "Purchase Of Material"

                        sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dTotalAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)


                        SGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        CGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        IGST = dtGSTRates.Rows(k)("GST_GSTRate")

                        dRow = dt.NewRow 'SGST
                        dRow("Id") = 0
                        dRow("HeadID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objVerification.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input SGST " & SGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 6
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "SGST"

                        sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dSGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'CGST
                        dRow("Id") = 0
                        dRow("HeadID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objVerification.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input CGST " & CGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 7
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "CGST"

                        sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dCGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'IGST
                        dRow("Id") = 0
                        dRow("HeadID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objVerification.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objVerification.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input IGST " & IGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 8
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "IGST"

                        sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objVerification.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dIGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dTotalAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0 : dIGSTAmt = 0
                    End If

                Next

                dRow = dt.NewRow 'Party/Customer
                dRow("Id") = 0
                dRow("HeadID") = objVerification.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
                dRow("GLID") = objVerification.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
                dRow("SubGLID") = objVerification.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                dRow("PaymentID") = 9
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Party/Customer"

                sGL = objVerification.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objVerification.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    dRow("SubGLDescription") = sArray(1)
                End If
                dRow("Debit") = 0
                dRow("Credit") = dPartyTotal

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
End Class
