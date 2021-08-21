
Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports System.IO
Imports DatabaseLayer
'Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
'    MyBase.OnPreRender(e)
'    Dim strDisAbleBackButton As String
'    strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
'    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
'    imgbtnAdd.ImageUrl = "~/Images/Save24.png"
'    'imgbtnSave.ImageUrl = "~/Images/Save24.png"
'    'imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
'    imgbtnBack.ImageUrl = "~/Images/Backward24.png"
'    imgNAdd.ImageUrl = "~/Images/Add24.png"
'    'ImgbtnAddNew.ImageUrl = "~/Images/Add24.png"
'    imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
'End Sub
Partial Class Purchase_Approval
    Inherits System.Web.UI.Page
    Dim objPO As New clsPurchaseOrder
    Dim objGIN As New ClsGoodsInward
    Dim sSession As New AllSession
    Dim objFasGnrl As New clsFASGeneral
    Dim objGnrlFnctn As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails
    Dim objVerification As New clsPurchaseVrification
    Dim objApprvl As New ClsApproval
    Dim objDb As New DBHelper
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sFormName As String = "Purchase/InwardNote"
    Private dtTab As New DataTable
    Private Shared sIKBBackStatus As String
    Private Shared sCurrentMonthID As Integer = 0
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged

        Dim dtTab As New DataTable
            Dim dt1, dt2, dt3, dt4, dt5, dt As New DataTable
            Dim dTable1 As New DataTable
            Dim dtable2 As New DataTable
            Dim dtable3 As New DataTable
            Dim flag As Int32 = 0
            Dim dtable4 As New DataTable
            Try
                If (ddlorder.SelectedIndex = 0) Then
                    dgViewPR.DataSource = Nothing
                    dgViewPR.DataBind()
                    dgViewExcess.DataSource = Nothing
                    dgViewExcess.DataBind()
                    Exit Sub
                End If
                If (ddlCategory.SelectedIndex = 1) Then
                dt2 = objVerification.GetTransactionDetailsPR(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlinvoice.SelectedItem.Text, 0, ddlorder.SelectedValue)
                dtable2.Merge(dt2)
                    flag = 1
                ElseIf (ddlCategory.SelectedIndex = 2) Then
                dt3 = objVerification.GetTransactionDetailsExcess(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlinvoice.SelectedItem.Text, 0, ddlorder.SelectedValue)
                dtable3.Merge(dt3)
                    flag = 1
                ElseIf (ddlCategory.SelectedIndex = 3) Then
                dt3 = objVerification.GetTransactionDetailsExcess(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlinvoice.SelectedItem.Text, 0, ddlorder.SelectedValue)
                dtable3.Merge(dt3)
                    flag = 1
                ElseIf (ddlCategory.SelectedIndex = 3) Then

                End If
                If (flag = 1) Then
                    dgViewPR.DataSource = dtable2
                    dgViewExcess.DataSource = dtable3
                    dgViewExcess.DataBind()
                    dgViewPR.DataBind()
                End If
            Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCategory_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iOrderID As Integer = 0
        Dim ibillNo As Integer = 0
        Try
            sSession = Session("AllSession")
            ' lblErrorUp.Text = ""
            If IsPostBack = False Then
                LoadOrder()
                LoadInvoice()
                BindCategory()
                iOrderID = Request.QueryString("ExistingOrder")
                ibillNo = Request.QueryString("BillNo")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadOrder()
        Try
            ddlorder.DataSource = objVerification.Order(sSession.AccessCode, sSession.AccessCodeID)
            ddlorder.DataTextField = "POM_OrderNo"
            ddlorder.DataValueField = "POM_ID"
            ddlorder.DataBind()
            ddlorder.Items.Insert(0, New ListItem("--- Select Order No. ---", "0"))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub BindCategory()
        Try
            ddlCategory.Items.Add(New ListItem("Select Category", 0))
            ddlCategory.Items.Add(New ListItem("Reject Or Return", 1))
            ddlCategory.Items.Add(New ListItem("Excess", 2))
            ddlCategory.Items.Add(New ListItem("shortage", 3))
            ddlCategory.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LoadInvoice()
        Try
            ddlinvoice.DataSource = objVerification.LoadForApproval(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlorder.SelectedValue)
            ddlinvoice.DataTextField = "PGM_DocumentRefNo"
            ddlinvoice.DataValueField = "PGM_ID"
            ddlinvoice.DataBind()
            ddlinvoice.Items.Insert(0, "--- Select Document Ref NO ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlinvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlinvoice.SelectedIndexChanged
        Dim dt As New DataTable, dt2 As New DataTable, dt3 As New DataTable
        Dim flag As New Integer
        Dim dTable1 As New DataTable
        Dim dtable2 As New DataTable
        Dim dtable3 As New DataTable
        Dim dtable4 As New DataTable
        Try
            If (ddlinvoice.SelectedIndex > 0) Then

                If (objVerification.CheckRegisterNo(sSession.AccessCode, ddlinvoice.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlorder.SelectedValue)) Then
                    If (objVerification.CheckInwardNo(sSession.AccessCode, ddlinvoice.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlorder.SelectedValue)) Then
                        lblError.Text = ""
                        dgViewPR.DataSource = Nothing
                        dgViewPR.DataSource = Nothing
                        dgViewPR.DataBind()
                        'dgViewPI.DataBind()
                        If (ddlinvoice.SelectedValue <> "") Then
                            dt = objVerification.GetMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlinvoice.SelectedValue)
                        End If
                        If (ddlinvoice.SelectedIndex > 0) Then
                            ' objVerification.UpdateTransaction(sSession.AccessCode, sSession.AccessCodeID, ddlGoodsInwardNo.SelectedItem.Text, 0)
                            'dt1 = objVerification.GetTransactionDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlGoodsInwardNo.SelectedItem.Text, sSession.YearID, ddlExistingTransactions.SelectedValue)
                            dt2 = objVerification.GetTransactionDetailsPR(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlinvoice.SelectedItem.Text, 0, ddlorder.SelectedValue)
                            dt3 = objVerification.GetTransactionDetailsExcess(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlinvoice.SelectedItem.Text, 0, ddlorder.SelectedValue)
                            '  dt5 = objVerification.GetTransactionDetailsNewItemDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlinvoice.SelectedItem.Text, 0, ddlorder.SelectedValue)
                            '  dTable1.Merge(dt1)
                            dtable2.Merge(dt2)
                            dtable3.Merge(dt3)
                            'dtable4.Merge(dt4)
                            ' dtable3.Merge(dt5)
                            flag = 1
                        End If
                        If (flag = 1) Then
                            'dgViewPI.DataSource = dTable1
                            dgViewPR.DataSource = dtable2
                            dgViewExcess.DataSource = dtable3
                            dgViewExcess.DataBind()
                            dgViewPR.DataBind()
                            '  dgViewPI.DataBind()
                        End If
                    Else
                        lblError.Text = "Invoice not Received at Account Department"
                    End If
                Else
                    lblError.Text = "Goods not at Received"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlinvoice_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlorder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlorder.SelectedIndexChanged
        Try
            LoadInvoice()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlorder_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            'Dim sChk As String
            'Dim chkItem As CheckBoxList
            'Dim i As Integer
            'Dim blnCheck As Boolean
            'Dim lblItemID As Label
            'Dim IbChk As New CheckBox
            'Dim sSelIDs As String = String.Empty
            '' Dim objclsPermission As New clsPermission
            'Dim gin As Integer
            Dim cstid As Integer, vatid As Integer, exciseid As Integer, id As Integer = 1
            Dim Ssql As String
            'Try
            Dim dt As New DataTable

            dt = objDb.SQLExecuteDataTable(sSession.AccessCode, "select * from inventory_master_history order by Invh_ID")
            For i = 0 To dt.Rows.Count - 1
                cstid = objDb.SQLGetDescription(sSession.AccessCode, "select mas_id from acc_general_master where Mas_desc='" & dt.Rows(i)("invh_cst") & "' And Mas_master=15")
                vatid = objDb.SQLGetDescription(sSession.AccessCode, "select mas_id from acc_general_master where Mas_desc='" & dt.Rows(i)("invh_vat") & "' And Mas_master=14")
                exciseid = objDb.SQLGetDescription(sSession.AccessCode, "select mas_id from acc_general_master where Mas_desc='" & dt.Rows(i)("invh_excise") & "' And Mas_master=16")
                Ssql = "INSERT INTO inventory_master_taxdetails(IMT_ID, IMT_MasterID, IMT_VAT, IMT_CST, IMT_Excise,IMT_IPAddress,IMT_CompID,IMT_EffectiveVATFrom,IMT_EffectiveCSTFrom,IMT_EffectiveExciseFrom,IMT_Status,IMT_CreatedOn,IMT_CreatedBy,IMT_EffectiveExciseTo,IMT_EffectiveCSTTo,IMT_EffectiveVATTo)"
                Ssql = Ssql + " values(" & id & "," & dt.Rows(i)("InvH_ID") & "," & vatid & "," & cstid & "," & exciseid & ",'" & dt.Rows(i)("InvH_IPAddress") & "'," & dt.Rows(i)("InvH_CompID") & ",'2017-01-01 00:00:00.000','2017-01-01 00:00:00.000','2017-01-01 00:00:00.000','A','" & dt.Rows(i)("InvH_CreatedOn") & "'," & dt.Rows(i)("InvH_CreatedBy") & ",'1900-01-01 00:00:00.000','1900-01-01 00:00:00.000','1900-01-01 00:00:00.000')"
                objDb.SQLExecuteNonQuery(sSession.AccessCode, Ssql)
                id = id + 1
            Next


            '    gin = objDb.SQLGetDescription(sSession.AccessCode, "Select PGM_ID from Purchase_GIN_Master where PGM_DocumentRefNo='" & ddlinvoice.SelectedValue & "' ")
            '    For i = 0 To dgViewPR.Rows.Count - 1

            '        IbChk = dgViewPR.Rows(i).FindControl("chkSelect")
            '        lblItemID = dgViewPR.Rows(i).FindControl("lblItemID")
            '        If blnCheck = True Then
            '            objApprvl.ApproveReturnItem(sSession.AccessCode, sSession.AccessCodeID, ddlorder.SelectedValue, gin, lblItemID.Text)
            '            blnCheck = False
            '        End If
            '    Next
            'lblErrorUp.Text = "Successfully updated."
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnApprove_Click")
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
End Class
