Imports System
Imports System.Data
Imports BusinesLayer
Partial Class FixedAsset_AssetTransactionDeletion
    Inherits System.Web.UI.Page

    Private sFormName As String = "FixedAsset_AssetTransactionDeletion"
    Private objerrorclass As New BusinesLayer.Components.ErrorClass
    Dim objAsstTrndel As New ClsAssetTransactionDeletion
    Private Shared sSession As AllSession
    Public dtFixedAssetTrndel As New DataTable
    Dim clsgeneral As New clsFASGeneral
    Dim iAssetTypeId As Integer
    Public dtFixedAssetTrn As New DataTable
    Private Shared iDocID As Integer
    Dim dt As New DataTable
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Private objIndex As New clsIndexing
    Dim objGnrlFnction As New clsGeneralFunctions
    Dim objGen As New clsFASGeneral
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnDelete.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnDADD.ImageUrl = "~/Images/Add24.png"
        imgbtnOtherCADD.ImageUrl = "~/Images/Add24.png"
        imgbtnDADD.ImageUrl = "~/Images/Add24.png"
        imgbtnOtherCADD.ImageUrl = "~/Images/Add24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"
        ImgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                lblDateDisplay.Text = objGnrlFnction.GetCurrentDate(sSession.AccessCode)
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                loadAssetDeletion()
                loadExistingTRnNo()
                BindHeadofAccounts() : LoadSubGL()
                '  loadExistingTRnNo()
                LoadPaymentType()
                'Me.imgbtnDelete.Attributes.Add("OnClick", "javascript:return Validate();")
                RFVAstNo.InitialValue = "Select ExistingTransactionNo" : RFVAstNo.ErrorMessage = "Select ExistingTransactionNo"
                RFVAstype.InitialValue = "Select AssetType" : RFVAstype.ErrorMessage = "Select AssetType"
                RFVDeletion.InitialValue = "Select Asset Deletion" : RFVDeletion.ErrorMessage = "Select Asset Deletion"
                REVDlnDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REVDlnDate.ErrorMessage = "Enter Date in Correct Formate."
                RFVDlnDate.ErrorMessage = "Enter Date"
                REVdeletionDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REVdeletionDate.ErrorMessage = "Enter Asset Deletion Date in Correct Formate."
                RFVdeletionDate.ErrorMessage = "Enter Asset Deletion Date."
                RFVESScrap.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVESScrap.ErrorMessage = "Enter Sale/Scrap Valve"
                REVESScrap.ErrorMessage = "Enter Sale/Scrap Valve"
                RFVdeldesc.ErrorMessage = "Enter Asset Deletion Description"
                loadAssetType()
                Dim sAssetRefNo As String = ""
                sAssetRefNo = Request.QueryString("MasterID")
                If sAssetRefNo <> "" Then
                    ddlTransNo.SelectedValue = clsgeneral.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                    sAssetRefNo = clsgeneral.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                    ddlTransNo_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "pageload")
        End Try
    End Sub
    Public Sub loadAssetDeletion()
        Dim dt As New DataTable
        Try
            'dt = objAsstTrndel.LoadAssetDeletion(sSession.AccessCode, sSession.AccessCodeID)
            'ddlDeletion.DataTextField = "Mas_Desc"
            'ddlDeletion.DataValueField = "Mas_Id"
            'ddlDeletion.DataSource = dt
            'ddlDeletion.DataBind()
            'ddlDeletion.Items.Insert(0, "Select Asset Deletion")
            ddlDeletion.Items.Insert(0, "--- Asset Deletion ---")
            ddlDeletion.Items.Insert(1, "Sold")
            ddlDeletion.Items.Insert(2, "Transfer")
            ddlDeletion.Items.Insert(3, "Stolen")
            ddlDeletion.Items.Insert(4, "Destroyed")
            ddlDeletion.Items.Insert(5, "Absolite")
            ddlDeletion.Items.Insert(6, "Repair")
            ddlDeletion.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetDeletion")
        End Try
    End Sub
    'Private Sub loadExistingTRnNo()
    '    Try
    '        drpAstNo.DataSource = objAsstTrndel.ExistingTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
    '        drpAstNo.DataTextField = "AFAA_AssetNo"
    '        drpAstNo.DataValueField = "AFAA_ID"
    '        drpAstNo.DataBind()
    '        drpAstNo.Items.Insert(0, "Select ExistingTransactionNo")
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingTRnNo")
    '    End Try
    'End Sub
    Private Sub imgbtnDelete_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDelete.Click
        Try
            If rboNew.Checked = False And rboOld.Checked = False Then
                lblError.Text = "Select Partialy Delete  or Fully Delete"
                Exit Sub
            End If
            lblAssetdeletionValidationMsg1.Text = "Are you Sure, you want to delete?"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType1').addClass('alert alert-success');$('#ModalDeletionValidation1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDelete_Click")
        End Try
    End Sub
    'Public Sub LoadAssetType1(ByVal iassetID As Integer)
    '    Dim AssetDesc As String
    '    Try
    '        AssetDesc = objAsstTrndel.LoadAssetType1(sSession.AccessCode, sSession.AccessCodeID, iassetID)
    '        LoadDesc(AssetDesc)
    '        drpAstype.SelectedValue = iAssetTypeId
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAssetType1")
    '    End Try
    'End Sub
    Public Sub LoadDesc(ByVal AssetDesc As String)
        Dim dt As New DataTable
        Try
            dt = objAsstTrndel.LoadDesc(sSession.AccessCode, sSession.AccessCodeID, AssetDesc)
            DDlAsstType.DataTextField = "GL_Desc"
            DDlAsstType.DataValueField = "GL_ID"
            DDlAsstType.DataSource = dt
            DDlAsstType.DataBind()
            DDlAsstType.Items.Insert(0, "Select Asset Type")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDesc")
        End Try
    End Sub
    'Private Sub drpAstNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpAstNo.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Try
    '        If drpAstNo.SelectedIndex > 0 Then
    '            dt = objAsstTrndel.showDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, drpAstNo.SelectedValue)
    '            If dt.Rows.Count > 0 Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    If IsDBNull(dt.Rows(i)("AFAA_AssetType").ToString()) = False Then
    '                        If dt.Rows(i)("AFAA_AssetType").ToString() = "" Then
    '                            drpAstype.SelectedIndex = 0
    '                        Else
    '                            iAssetTypeId = dt.Rows(i)("AFAA_AssetType").ToString()
    '                            LoadAssetType1(iAssetTypeId)

    '                        End If
    '                    End If
    '                    If dt.Rows(i)("AFAA_Delflag") = "W" Then
    '                        lblstatus.Text = "Waiting For Approval"
    '                    ElseIf dt.Rows(i)("AFAA_Delflag") = "A" Then
    '                        lblstatus.Text = "Approved"
    '                    ElseIf dt.Rows(i)("AFAA_Delflag") = "X" Then
    '                        lblstatus.Text = "Transaction Deleted"
    '                    ElseIf dt.Rows(i)("AFAA_Delflag") = "Y" Then
    '                        lblstatus.Text = "Recalled for Approval"
    '                    ElseIf dt.Rows(i)("AFAA_Delflag") = "D" Then
    '                        lblstatus.Text = "Transaction Deleted"
    '                    End If

    '                    If dt.Rows(i)("AFAA_Status").ToString() = "D" Then
    '                        ddlDeletion.SelectedIndex = dt.Rows(i)("AFAA_AssetDelID").ToString()
    '                        'loadAssetDeletion()
    '                        If dt.Rows(i)("AFAA_DelnType").ToString() = "PD" Then
    '                            rboNew.Checked = True
    '                        Else
    '                            rboOld.Checked = True
    '                        End If

    '                        If dt.Rows(i)("AFAA_Assetvalue").ToString() = "" Then
    '                                txtSScrap.Text = ""
    '                            Else
    '                                txtSScrap.Text = dt.Rows(i)("AFAA_Assetvalue").ToString()
    '                            End If
    '                            If dt.Rows(i)("AFAA_AssetDesc").ToString() = "" Then
    '                                txtdeldesc.Text = ""
    '                            Else
    '                                txtdeldesc.Text = dt.Rows(i)("AFAA_AssetDesc").ToString()
    '                            End If
    '                            If dt.Rows(i)("AFAA_AssetDelQuantity").ToString() = "" Then
    '                            txtQuantity.Text = ""
    '                        Else
    '                                txtQuantity.Text = dt.Rows(i)("AFAA_AssetDelQuantity").ToString()
    '                            End If
    '                            If dt.Rows(i)("AFAA_AssetDelDate").ToString() = "" Then
    '                                txtDlnDate.Text = ""
    '                            Else
    '                                txtDlnDate.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(i)("AFAA_AssetDelDate"), "D")
    '                            End If
    '                            If dt.Rows(i)("AFAA_AssetDeletionDate").ToString() = "" Then
    '                                txtdeletionDate.Text = ""
    '                            Else
    '                                txtdeletionDate.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(i)("AFAA_AssetDeletionDate"), "D")
    '                            End If
    '                            If dt.Rows(i)("AFAA_AssetDelID").ToString() = 2 Or dt.Rows(i)("AFAA_AssetDelID").ToString() = 6 Then
    '                                PnlPaymntType.Visible = False : ddlPaymnttype.Visible = False : lblPaymnttype.Visible = False
    '                                txtLocation.Visible = True : lbllocation.Visible = True
    '                                txtLocation.Text = dt.Rows(i)("AFAA_AssetDelLocation").ToString()
    '                            Else
    '                                txtLocation.Visible = False : lbllocation.Visible = False
    '                            End If
    '                            If dt.Rows(i)("AFAA_AssetDelID").ToString() = 1 Then
    '                                lblPaymnttype.Visible = True : ddlPaymnttype.Visible = True
    '                                ddlPaymnttype.SelectedIndex = dt.Rows(i)("AFAA_AssetDelPmntType").ToString()
    '                                If ddlPaymnttype.SelectedIndex = 1 Then
    '                                    PnlPaymntType.Visible = True
    '                                    txtChqNo.Text = dt.Rows(i)("AFAA_AssetDelChqeNo").ToString()
    '                                    txtChqRecvdDate.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(i)("AFAA_AssetDelChqeDate"), "D")
    '                                Else
    '                                    PnlPaymntType.Visible = False
    '                                End If
    '                            Else

    '                            End If
    '                        Else
    '                        ddlDeletion.SelectedIndex = 0
    '                        rboNew.Checked = False : rboOld.Checked = False
    '                        txtSScrap.Text = "" : txtdeldesc.Text = "" : txtQuantity.Text = "" : txtDlnDate.Text = ""
    '                        txtdeletionDate.Text = "" : PnlPaymntType.Visible = False : ddlPaymnttype.Visible = False : lblPaymnttype.Visible = False
    '                        txtLocation.Visible = False : lbllocation.Visible = False
    '                    End If
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "drpAstNo_SelectedIndexChanged")
    '    End Try
    'End Sub
    Private Sub BtnYES_Click(sender As Object, e As EventArgs) Handles BtnYES.Click
        Dim sDelLocation, sChequeNo As String
        Dim sChequeDate As Date
        Dim iPaymentType As Integer = 0
        Dim iAFAD_ID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0, dSum As Double = 0, dSDebit As Double = 0
        Dim dGridDebit As Double = 0 : Dim dGridCredit As Double = 0
        Dim sSts As String
        Try
            If ddlTransNo.SelectedIndex > 0 Then
                iAFAD_ID = ddlTransNo.SelectedValue
            Else
                iAFAD_ID = 0
            End If
            lblError.Text = ""
            If ddlDeletion.SelectedIndex = 1 Then
                iPaymentType = ddlPaymnttype.SelectedIndex
                If ddlPaymnttype.SelectedIndex = 1 Then
                    sChequeNo = txtChqNo.Text
                    '  sChequeDate = txtChqRecvdDate.Text
                Else
                    sChequeNo = ""
                    txtChqRecvdDate.Text = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
            Else
                iPaymentType = 0
                sChequeNo = ""
                txtChqRecvdDate.Text = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            'If ddlDeletion.SelectedIndex = 2 Or ddlDeletion.SelectedIndex = 6 Then
            '    sDelLocation = txtLocation.Text
            '    sSts = "TR"
            'Else
            '    sDelLocation = ""
            '    sSts = "W"
            'End If
            sSts = "W"
            If ddlDeletion.SelectedIndex = 0 Then 'For sold deletion type
                If dgPaymentDetails.Items.Count = 0 Then
                    lblError.Text = "Add Amount"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Amount','', 'success');", True)
                    Exit Sub
                End If

                For i = 0 To dgPaymentDetails.Items.Count - 1
                    dGridDebit = dGridDebit + Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                    dGridCredit = dGridCredit + Convert.ToDouble(dgPaymentDetails.Items(i).Cells(10).Text)
                Next
                If dGridDebit <> dGridCredit Then
                    lblError.Text = "Debit And Credit Amount not matching."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit And Credit Amount not matching','', 'success');", True)
                    Exit Sub
                End If
                For i = 0 To dgPaymentDetails.Items.Count - 1
                    dSDebit = Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                    dSum = dSum + dSDebit
                Next
            End If

            Dim iDelqty As Double = 0.0
            iDelqty = txtbxQty.Text - txtQuantity.Text
            Dim iUpdtedbyID As Integer = sSession.UserID
            iAFAD_ID = objAsstTrndel.DeleteTransaction(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iAFAD_ID, txtDelTransNo.Text, DDlAsstType.SelectedValue, ddlItemCode.SelectedValue, ddlDeletion.SelectedIndex, txtDlnDate.Text, txtdeletionDate.Text, txtSScrap.Text, txtdeldesc.Text, txtQuantity.Text, sDelLocation, iPaymentType, sChequeNo, txtChqRecvdDate.Text, txtbxDscrptn.Text, txtbxItmDecrtn.Text, iDelqty, txtbxDteofPurchase.Text, txtbxDteCmmunictn.Text, txtbxamount.Text, sSts)

            If dgPaymentDetails.Items.Count > 0 Then
                For i = 0 To dgPaymentDetails.Items.Count - 1

                    objAsstTrndel.iFXATD_TrType = 11
                    objAsstTrndel.iFXATD_BillId = iAFAD_ID
                    objAsstTrndel.iFXATD_PaymentType = 0
                    objAsstTrndel.iFXATD_DbOrCr = dgPaymentDetails.Items(i).Cells(12).Text
                    objAsstTrndel.iFXATD_Head = dgPaymentDetails.Items(i).Cells(1).Text
                    objAsstTrndel.iFXATD_GL = dgPaymentDetails.Items(i).Cells(2).Text
                    objAsstTrndel.iFXATD_SubGL = dgPaymentDetails.Items(i).Cells(3).Text
                    If objAsstTrndel.iFXATD_DbOrCr = 1 Then
                        dDebit = dgPaymentDetails.Items(i).Cells(9).Text
                        objAsstTrndel.dFXATD_Debit = dDebit
                        objAsstTrndel.dFXATD_Credit = 0.00
                    Else
                        dCredit = dgPaymentDetails.Items(i).Cells(10).Text
                        objAsstTrndel.dFXATD_Debit = 0.00
                        objAsstTrndel.dFXATD_Credit = dCredit
                    End If

                    objAsstTrndel.iFXATD_CreatedBy = sSession.UserID
                    objAsstTrndel.sFXATD_Status = "D"
                    objAsstTrndel.iFXATD_YearID = sSession.YearID
                    objAsstTrndel.sFXATD_Operation = "C"
                    objAsstTrndel.sFXATD_IPAddress = sSession.IPAddress
                    objAsstTrndel.iFXATD_CompID = sSession.AccessCodeID
                    objAsstTrndel.SaveFixedAssetTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, 0, objAsstTrndel)
                Next
            End If
            loadExistingTRnNo()
            ddlTransNo.SelectedValue = iAFAD_ID
            ddlTransNo_SelectedIndexChanged(sender, e)
            lblAssetdeletionValidationMsg.Text = "Waiting for approval"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType1').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BtnYES_Click")
        End Try
    End Sub

    Private Sub ddlDeletion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDeletion.SelectedIndexChanged
        Try

            If ddlDeletion.SelectedIndex = 2 Or ddlDeletion.SelectedIndex = 6 Then
                txtLocation.Visible = True
                lbllocation.Visible = True
            Else
                txtLocation.Visible = False
                lbllocation.Visible = False
            End If
            If ddlDeletion.SelectedIndex = 1 Then
                lblPaymnttype.Visible = True
                ddlPaymnttype.Visible = True
                PnlDebitCredit.Visible = True
                dgPaymentDetails.Visible = True
            Else
                lblPaymnttype.Visible = False
                ddlPaymnttype.Visible = False
                PnlPaymntType.Visible = False
                PnlDebitCredit.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadPaymentType()
        Dim dt As New DataTable
        Try
            ddlPaymnttype.Items.Insert(0, "--- Payment Types ---")
            ddlPaymnttype.Items.Insert(1, "Cheque")
            ddlPaymnttype.Items.Insert(2, "Cash")
            ddlPaymnttype.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPaymentType")
        End Try
    End Sub

    Private Sub ddlPaymnttype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymnttype.SelectedIndexChanged
        Try
            If ddlPaymnttype.SelectedIndex = 1 Then
                PnlPaymntType.Visible = True
            Else
                PnlPaymntType.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub loadAssetType()
        Dim dt As New DataTable
        Try
            dt = objAsstTrndel.LoadFxdAssetType(sSession.AccessCode, sSession.AccessCodeID)
            DDlAsstType.DataTextField = "GL_Desc"
            DDlAsstType.DataValueField = "GL_ID"
            DDlAsstType.DataSource = dt
            DDlAsstType.DataBind()
            DDlAsstType.Items.Insert(0, "Select AssetType")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType")
        End Try
    End Sub

    Private Sub DDlAsstType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDlAsstType.SelectedIndexChanged
        Dim iCount As Integer
        Dim AssetLen As String
        Dim ilen As Integer : Dim increment As Integer = 0
        Try
            If DDlAsstType.SelectedIndex > 0 Then
                Refresh()
                PnlDebitCredit.Visible = False
                dgPaymentDetails.Visible = False
                ddlTransNo.SelectedIndex = 0
                loadExistingItemCode()
                iCount = objAsstTrndel.GetGLID(sSession.AccessCode, sSession.AccessCodeID, DDlAsstType.SelectedValue)
                ' txtbxDscrptn.Text = "" : txtbxItmDecrtn.Text = "" : txtAstNOSup.Text = "" : txtbxQty.Text = "" : txtbxDteofPurchase.Text = "" : txtbxDteCmmunictn.Text = "" : txtbxamount.Text = ""
                If iCount > 0 Then
                    AssetLen = objAsstTrndel.GetAssetTypeNo(sSession.AccessCode, sSession.AccessCodeID, DDlAsstType.SelectedValue)
                    txtDelTransNo.Text = "D-" & AssetLen & iCount.ToString()
                Else
                    AssetLen = objAsstTrndel.LoadAssetNo(sSession.AccessCode, sSession.AccessCodeID, DDlAsstType.SelectedValue)
                    ilen = AssetLen.Length
                    If ilen = 9 Then
                        increment = increment + 1
                        txtDelTransNo.Text = "D-" & AssetLen & increment.ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DDlAsstType_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub loadExistingItemCode()
        Try
            ddlItemCode.DataSource = objAsstTrndel.ExistingItemCode(sSession.AccessCode, sSession.AccessCodeID, DDlAsstType.SelectedValue, sSession.YearID)
            ddlItemCode.DataTextField = "AFAM_ItemCode"
            ddlItemCode.DataValueField = "AFAM_ItemCode"
            ddlItemCode.DataBind()
            ddlItemCode.Items.Insert(0, "Select Itemcode")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingItemCode")
        End Try
    End Sub
    Private Sub loadExistingTRnNo()
        Try
            ddlTransNo.DataSource = objAsstTrndel.ExistingTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlTransNo.DataTextField = "AFAD_AssetTransNo"
            ddlTransNo.DataValueField = "AFAD_ID"
            ddlTransNo.DataBind()
            ddlTransNo.Items.Insert(0, "Select Existing TransactionNo")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingTRnNo")
        End Try
    End Sub

    Private Sub ddlTransNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransNo.SelectedIndexChanged
        Dim dt As New DataTable, dtTrans, dt1 As New DataTable
        Try
            If ddlTransNo.SelectedIndex > 0 Then
                PnlDebitCredit.Visible = False
                dgPaymentDetails.Visible = False
                dt = objAsstTrndel.showDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlTransNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If IsDBNull(dt.Rows(i)("AFAD_AssetType").ToString()) = False Then
                            If dt.Rows(i)("AFAD_AssetType").ToString() = "" Then
                                DDlAsstType.SelectedValue = 0
                            Else
                                DDlAsstType.SelectedValue = dt.Rows(i)("AFAD_AssetType").ToString()
                                'DDlAsstType_SelectedIndexChanged(sender, e)
                                loadExistingItemCode()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_AssetTransNo").ToString()) = False Then
                            If dt.Rows(i)("AFAD_AssetTransNo").ToString() = "" Then
                                txtDelTransNo.Text = ""
                            Else
                                txtDelTransNo.Text = dt.Rows(i)("AFAD_AssetTransNo").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_ItemCode").ToString()) = False Then
                            If dt.Rows(i)("AFAD_ItemCode").ToString() = "" Then
                                ddlItemCode.SelectedValue = ""
                            Else
                                ddlItemCode.SelectedValue = dt.Rows(i)("AFAD_ItemCode").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAD_Description").ToString()) = False Then
                            If dt.Rows(i)("AFAD_Description").ToString() = "" Then
                                txtbxDscrptn.Text = ""
                            Else
                                txtbxDscrptn.Text = dt.Rows(i)("AFAD_Description").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAD_ItemDescription").ToString()) = False Then
                            If dt.Rows(i)("AFAD_ItemDescription").ToString() = "" Then
                                txtbxItmDecrtn.Text = ""
                            Else
                                txtbxItmDecrtn.Text = dt.Rows(i)("AFAD_ItemDescription").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAD_Quantity").ToString()) = False Then
                            If dt.Rows(i)("AFAD_Quantity").ToString() = "" Then
                                txtbxQty.Text = ""
                            Else
                                txtbxQty.Text = dt.Rows(i)("AFAD_Quantity").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_CommissionDate").ToString()) = False Then
                            If dt.Rows(i)("AFAD_CommissionDate").ToString() = "" Then
                                txtbxDteCmmunictn.Text = ""
                            Else
                                txtbxDteCmmunictn.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(i)("AFAD_CommissionDate").ToString(), "D")
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_PurchaseDate").ToString()) = False Then
                            If dt.Rows(i)("AFAD_PurchaseDate").ToString() = "" Then
                                txtbxDteofPurchase.Text = ""
                            Else
                                txtbxDteofPurchase.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(i)("AFAD_PurchaseDate").ToString(), "D")
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_AssetAmount").ToString()) = False Then
                            If dt.Rows(i)("AFAD_AssetAmount").ToString() = "" Then
                                txtbxamount.Text = ""
                            Else
                                txtbxamount.Text = dt.Rows(i)("AFAD_AssetAmount").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_AssetDelID").ToString()) = False Then
                            If dt.Rows(i)("AFAD_AssetDelID").ToString() = "" Then
                                ddlDeletion.SelectedIndex = 0
                            Else
                                ddlDeletion.SelectedIndex = dt.Rows(i)("AFAD_AssetDelID").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAD_AssetDelDate").ToString()) = False Then
                            If dt.Rows(i)("AFAD_AssetDelDate").ToString() = "" Then
                                txtDlnDate.Text = ""
                            Else
                                txtDlnDate.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(i)("AFAD_AssetDelDate").ToString(), "D")
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_AssetDeletionDate").ToString()) = False Then
                            If dt.Rows(i)("AFAD_AssetDeletionDate").ToString() = "" Then
                                txtdeletionDate.Text = ""
                            Else
                                txtdeletionDate.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(i)("AFAD_AssetDeletionDate").ToString(), "D")
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_AssetDeltnAmount").ToString()) = False Then
                            If dt.Rows(i)("AFAD_AssetDeltnAmount").ToString() = "" Then
                                txtSScrap.Text = ""
                            Else
                                txtSScrap.Text = dt.Rows(i)("AFAD_AssetDeltnAmount").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_AssetDelQuantity").ToString()) = False Then
                            If dt.Rows(i)("AFAD_AssetDelQuantity").ToString() = "" Then
                                txtQuantity.Text = 0
                            Else
                                txtQuantity.Text = dt.Rows(i)("AFAD_AssetDelQuantity").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAD_DeltnItemDescription").ToString()) = False Then
                            If dt.Rows(i)("AFAD_DeltnItemDescription").ToString() = "" Then
                                txtdeldesc.Text = ""
                            Else
                                txtdeldesc.Text = dt.Rows(i)("AFAD_DeltnItemDescription").ToString()
                            End If
                        End If

                        If dt.Rows(i)("AFAD_AssetDelID").ToString() = 2 Or dt.Rows(i)("AFAD_AssetDelID").ToString() = 6 Then
                            PnlPaymntType.Visible = False : ddlPaymnttype.Visible = False : lblPaymnttype.Visible = False
                            txtLocation.Visible = True : lbllocation.Visible = True
                            txtLocation.Text = dt.Rows(i)("AFAD_AssetDelLocation").ToString()
                        Else
                            txtLocation.Visible = False : lbllocation.Visible = False
                        End If
                        If dt.Rows(i)("AFAD_AssetDelID").ToString() = 1 Then
                            lblPaymnttype.Visible = True : ddlPaymnttype.Visible = True
                            ddlPaymnttype.SelectedIndex = dt.Rows(i)("AFAD_AssetDelPmntType").ToString()
                            If ddlPaymnttype.SelectedIndex = 1 Then
                                PnlPaymntType.Visible = True
                                txtChqNo.Text = dt.Rows(i)("AFAD_AssetDelChqeNo").ToString()
                                txtChqRecvdDate.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(i)("AFAD_AssetDelChqeDate"), "D")
                            Else
                                PnlPaymntType.Visible = False
                            End If
                        Else

                        End If
                        If dt.Rows(i)("AFAD_Status") = "W" Then
                            lblstatus.Text = "Waiting For Approval"
                            imgbtnWaiting.Visible = True
                            ImgbtnActivate.Visible = False : imgbtnDelete.Visible = False
                        ElseIf dt.Rows(i)("AFAD_Status") = "D" Then
                            lblstatus.Text = "Deleted"
                            ImgbtnActivate.Visible = False
                            imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False
                        ElseIf dt.Rows(i)("AFAD_Status") = "TR" Then
                            lblstatus.Text = "Transfered/Repair"
                            imgbtnDelete.Visible = False
                            imgbtnWaiting.Visible = False : ImgbtnActivate.Visible = True
                            'imgbtnAttachment.Enabled = False : imgbtnAttach.Enabled = False
                            'imgbtnRefresh.Enabled = False
                            'imgbtnWaiting.Enabled = False
                        ElseIf dt.Rows(i)("AFAD_Status") = "RS" Then
                            lblstatus.Text = "Reactivated"
                            ImgbtnActivate.Visible = False : imgbtnDelete.Visible = False
                            imgbtnWaiting.Visible = False
                        End If


                    Next
                    If ddlDeletion.SelectedIndex = 1 Then
                        dtTrans = objAsstTrndel.LoadSavedTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlTransNo.SelectedValue)
                        If dtTrans.Rows.Count > 0 Then
                            PnlDebitCredit.Visible = True
                            dgPaymentDetails.Visible = True
                            dgPaymentDetails.DataSource = dtTrans
                            dgPaymentDetails.DataBind()
                            Session("dtAssetPayment") = dtTrans
                        Else
                            PnlDebitCredit.Visible = False
                            dgPaymentDetails.Visible = False
                        End If
                    End If
                    GetAttachFile(ddlTransNo.SelectedItem.Text)
                    lblBadgeCount.Text = Convert.ToString(objAsstTrndel.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, ddlTransNo.SelectedItem.Text))
                Else
                    lblAssetdeletionValidationMsg.Text = "No Data"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTransNo_SelectedIndexChanged")
        End Try

    End Sub
    Public Sub BindHeadofAccounts()
        Try
            ddlDrOtherHead.Items.Insert(0, "Select Head of Account")
            ddlDrOtherHead.Items.Insert(1, "Asset")
            ddlDrOtherHead.Items.Insert(2, "Income")
            ddlDrOtherHead.Items.Insert(3, "Expenditure")
            ddlDrOtherHead.Items.Insert(4, "Liabilities")
            ddlDrOtherHead.SelectedIndex = 0

            ddlCrOtherHead.Items.Insert(0, "Select Head of Account")
            ddlCrOtherHead.Items.Insert(1, "Asset")
            ddlCrOtherHead.Items.Insert(2, "Income")
            ddlCrOtherHead.Items.Insert(3, "Expenditure")
            ddlCrOtherHead.Items.Insert(4, "Liabilities")
            ddlCrOtherHead.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindHeadofAccounts")
        End Try
    End Sub
    Private Sub LoadSubGL()
        Try
            ddlCrOtherSubGL.DataSource = objAsstTrndel.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlCrOtherSubGL.DataTextField = "GlDesc"
            ddlCrOtherSubGL.DataValueField = "gl_Id"
            ddlCrOtherSubGL.DataBind()
            ddlCrOtherSubGL.Items.Insert(0, "Select SubGL Code")

            ddlDbOtherSubGL.DataSource = objAsstTrndel.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlDbOtherSubGL.DataTextField = "GlDesc"
            ddlDbOtherSubGL.DataValueField = "gl_Id"
            ddlDbOtherSubGL.DataBind()
            ddlDbOtherSubGL.Items.Insert(0, "Select SubGL Code")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSubGL")
        End Try
    End Sub
    Private Sub ddlCrOtherHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCrOtherHead.SelectedIndex > 0 Then
                ddlCrOtherGL.DataSource = objAsstTrndel.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherHead.SelectedIndex)
                ddlCrOtherGL.DataTextField = "GlDesc"
                ddlCrOtherGL.DataValueField = "gl_Id"
                ddlCrOtherGL.DataBind()
                ddlCrOtherGL.Items.Insert(0, "Select GL Code")
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrOtherHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlDrOtherHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDrOtherHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlDrOtherHead.SelectedIndex > 0 Then
                ddlDbOtherGL.DataSource = objAsstTrndel.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDrOtherHead.SelectedIndex)
                ddlDbOtherGL.DataTextField = "GlDesc"
                ddlDbOtherGL.DataValueField = "gl_Id"
                ddlDbOtherGL.DataBind()
                ddlDbOtherGL.Items.Insert(0, "Select GL Code")
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDrOtherHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnDADD_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDADD.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dDebit As Double = 0
        Dim dtDetails As New DataTable
        Try
            If ddlDbOtherSubGL.Items.Count > 1 Then
                PnlDebitCredit.Visible = True
                If ddlDbOtherSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Debit."
                    Exit Sub
                End If
            End If

            If IsNothing(Session("dtFixedAssetTrn")) Then
                dtFixedAssetTrn = dtDetails
            Else
                dtFixedAssetTrn = Session("dtFixedAssetTrn")
            End If

            dtCOA = objAsstTrndel.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

            'Debit
            If ddlDbOtherGL.SelectedIndex > 0 Then
                iGL = ddlDbOtherGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlDbOtherSubGL.SelectedIndex > 0 Then
                iSubGL = ddlDbOtherSubGL.SelectedValue
            Else
                iSubGL = 0
            End If

            If txtOtherDAmount.Text <> "" Then
                dDebit = txtOtherDAmount.Text
            Else
                dDebit = 0.00
            End If

            dtFixedAssetTrn = objAsstTrndel.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHead.SelectedIndex, iGL, iSubGL, dDebit, 1, dtFixedAssetTrn, dtCOA)
            Session("dtFixedAssetTrn") = dtFixedAssetTrn
            dgPaymentDetails.Visible = True
            dgPaymentDetails.DataSource = dtFixedAssetTrn
            dgPaymentDetails.DataBind()

            LoadSubGL()
            ddlDrOtherHead.SelectedIndex = 0 : ddlDbOtherGL.Items.Clear() : ddlDbOtherSubGL.SelectedIndex = 0 : txtOtherDAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDADD_Click")
        End Try
    End Sub
    Private Sub ddlDbOtherGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDbOtherGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlDbOtherGL.SelectedIndex > 0 Then
                ddlDbOtherSubGL.DataSource = objAsstTrndel.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDbOtherGL.SelectedValue)
                ddlDbOtherSubGL.DataTextField = "GlDesc"
                ddlDbOtherSubGL.DataValueField = "gl_Id"
                ddlDbOtherSubGL.DataBind()
                ddlDbOtherSubGL.Items.Insert(0, "Select SubGL Code")
            Else
                ddlDbOtherSubGL.DataSource = dt
                ddlDbOtherSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDbOtherGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnOtherCADD_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnOtherCADD.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dCredit As Double = 0
        Dim dtDetails As New DataTable
        Try

            If ddlCrOtherSubGL.Items.Count > 1 Then
                PnlDebitCredit.Visible = True
                If ddlCrOtherSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Credit."
                    Exit Sub
                End If
            End If

            If IsNothing(Session("dtFixedAssetTrn")) Then
                dtFixedAssetTrn = dtDetails
            Else

                dtFixedAssetTrn = Session("dtFixedAssetTrn")
            End If

            dtCOA = objAsstTrndel.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

            'Debit
            If ddlCrOtherGL.SelectedIndex > 0 Then
                iGL = ddlCrOtherGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlCrOtherSubGL.SelectedIndex > 0 Then
                iSubGL = ddlCrOtherSubGL.SelectedValue
            Else
                iSubGL = 0
            End If

            If txtOtherCAmount.Text <> "" Then
                dCredit = txtOtherCAmount.Text
            Else
                dCredit = 0.00
            End If
            dtFixedAssetTrn = objAsstTrndel.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrOtherHead.SelectedIndex, iGL, iSubGL, dCredit, 2, dtFixedAssetTrn, dtCOA)
            Session("dtAssetPayment") = dtFixedAssetTrn
            dgPaymentDetails.DataSource = dtFixedAssetTrn
            dgPaymentDetails.DataBind()

            LoadSubGL()
            ddlCrOtherHead.SelectedIndex = 0 : ddlCrOtherGL.Items.Clear() : ddlCrOtherSubGL.SelectedIndex = 0 : txtOtherCAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnOtherCADD_Click")
        End Try
    End Sub
    Protected Sub dgPaymentDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPaymentDetails.ItemCommand
        Dim dt As New DataTable
        Dim lblId As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "DELETE" Then

                If lblstatus.Text = "Activated" Then
                    lblError.Text = "This Payment has been Approved, you can not delete transactions."
                    Exit Sub
                End If

                dt = Session("dtFixedAssetTrn")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                If dt.Rows.Count > 0 Then
                    Session("dtFixedAssetTrn") = dt
                Else
                    Session("dtFixedAssetTrn") = Nothing
                End If
            End If

            dgPaymentDetails.DataSource = dt
            dgPaymentDetails.DataBind()

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPaymentDetails_ItemCommand")
        End Try
    End Sub
    Private Sub dgPaymentDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPaymentDetails.ItemDataBound
        Dim imgbtnDelete1 As New ImageButton, imgbtnEdit As New ImageButton
        Try
            lblError.Text = ""
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDelete1 = CType(e.Item.FindControl("imgbtnDelete1"), ImageButton)
                imgbtnDelete1.ImageUrl = "~/Images/Trash16.png"

                If lblstatus.Text = "Waiting for Approval" Then
                    imgbtnDelete1.Enabled = True
                Else
                    imgbtnDelete1.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPaymentDetails_ItemDataBound")
        End Try
    End Sub
    Public Sub Refresh()
        Try
            lblstatus.Text = "" : ddlDeletion.SelectedIndex = 0
            txtDlnDate.Text = "" : txtdeletionDate.Text = "" : txtSScrap.Text = "" : txtLocation.Text = ""
            ddlPaymnttype.SelectedIndex = 0 : txtChqNo.Text = "" : txtChqRecvdDate.Text = "" : txtdeldesc.Text = ""

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlItemCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlItemCode.SelectedIndexChanged
        Dim dt As New DataTable, dt1 As New DataTable
        Try
            If ddlItemCode.SelectedIndex <> 0 Then
                dt = objAsstTrndel.GetItemDescription(sSession.AccessCode, sSession.AccessCodeID, ddlItemCode.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("AFAM_Description").ToString()) = False Then
                        If dt.Rows(0)("AFAM_Description").ToString() = "" Then
                            txtbxDscrptn.Text = ""
                        Else
                            txtbxDscrptn.Text = dt.Rows(0)("AFAM_Description").ToString()
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("AFAM_ItemDescription").ToString()) = False Then
                        If dt.Rows(0)("AFAM_ItemDescription").ToString() = "" Then
                            txtbxItmDecrtn.Text = ""
                        Else
                            txtbxItmDecrtn.Text = dt.Rows(0)("AFAM_ItemDescription").ToString()
                        End If
                    End If
                    Dim delQty As Double = 0
                    delQty = objAsstTrndel.getDelQty(sSession.AccessCode, sSession.AccessCodeID, ddlItemCode.SelectedValue, DDlAsstType.SelectedValue)

                    If IsDBNull(dt.Rows(0)("AFAM_Quantity").ToString()) = False Then
                        If dt.Rows(0)("AFAM_Quantity").ToString() = "" Then
                            txtbxQty.Text = ""
                        Else
                            If delQty <> 0 Then
                                txtbxQty.Text = delQty
                            Else
                                txtbxQty.Text = dt.Rows(0)("AFAM_Quantity").ToString()
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("AFAM_Purchasedate").ToString()) = False Then
                        If dt.Rows(0)("AFAM_Purchasedate").ToString() = "" Then
                            txtbxDteofPurchase.Text = ""
                        Else
                            txtbxDteofPurchase.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(0)("AFAM_Purchasedate").ToString(), "D")
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("AFAM_CommissionDate").ToString()) = False Then
                        If dt.Rows(0)("AFAM_CommissionDate").ToString() = "" Then
                            txtbxDteCmmunictn.Text = ""
                        Else
                            txtbxDteCmmunictn.Text = clsgeneral.FormatDtForRDBMS(dt.Rows(0)("AFAM_CommissionDate").ToString(), "D")
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("AFAM_PurchaseAmount").ToString()) = False Then
                        If dt.Rows(0)("AFAM_PurchaseAmount").ToString() = "" Then
                            txtbxamount.Text = ""
                        Else
                            txtbxamount.Text = dt.Rows(0)("AFAM_PurchaseAmount").ToString()
                        End If
                    End If
                End If
                GetAttachFile(ddlTransNo.SelectedItem.Text)
                lblBadgeCount.Text = Convert.ToString(objAsstTrndel.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, ddlTransNo.SelectedItem.Text))

            Else
                ddlTransNo.SelectedIndex = 0
            End If
            Refresh()
            PnlDebitCredit.Visible = False
            dgPaymentDetails.Visible = False
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
        Dim objBatch As clsIndexing.BatchScan
        Dim Arr() As String
        Try
            If gvattach.Rows.Count > 0 Then
                AutomaticIndexing()
                GetAttachFile(ddlTransNo.SelectedItem.Text)
                gvattach.Visible = True
                '  gvattach.DataBind()
                lblBadgeCount.Text = Convert.ToString(objAsstTrndel.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, ddlTransNo.SelectedItem.Text))
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
            If ddlTransNo.SelectedIndex = 0 Then
                lblError.Text = "Select Transaction no."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlTransNo.Focus()
                Exit Sub
            Else
                icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlItemCode.SelectedItem.Text)
            End If

            iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Asset Deletion")

            If ddlTransNo.SelectedIndex = 0 Then
                lblError.Text = "Select Asset Item Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            ddlTransNo.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlTransNo.SelectedItem.Text)
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
                            objIndex.iPGEBASENAME = objGnrlFnction.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
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
                            objIndex.iPGEPAGENO = objGnrlFnction.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
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
                        lblError.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
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
                objGnrlFnction.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
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

            If ddlTransNo.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Transaction no"
                ddlTransNo.Focus()
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
                            dRow("CreatedOn") = objGnrlFnction.GetCurrentDate(sSession.AccessCode)
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
                            dRow("CreatedOn") = objGnrlFnction.GetCurrentDate(sSession.AccessCode)
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
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
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

            dt1 = objAsstTrndel.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
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
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim iCabinetID, iSubCabinetID, iFolderID As Integer
        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Dim dt As New DataTable
        Try
            If ddlTransNo.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlItemCode.SelectedItem.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Asset Deletion")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlTransNo.SelectedItem.Text)

                    dt = objAsstTrndel.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
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
                lblError.Text = "Select Existing Transaction No"
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim sSts As String
        Dim iDelqty As Double = 0.0
        Try
            If ddlTransNo.SelectedIndex > 0 Then
                If ddlDeletion.SelectedIndex = 2 Or ddlDeletion.SelectedIndex = 6 Then
                    sSts = "TR"
                Else
                    sSts = "D"
                End If
                ActivateStatus(ddlTransNo.SelectedValue, sSts, iDelqty)
                lblAssetdeletionValidationMsg.Text = "Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                lblstatus.Text = "Deleted"
                ddlTransNo_SelectedIndexChanged(sender, e)
            Else
                lblAssetdeletionValidationMsg.Text = "Select Transaction no"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub

    Private Sub ImgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles ImgbtnActivate.Click
        Try
            Dim iDelqty As Double = 0.0
            If ddlDeletion.SelectedIndex = 2 Or ddlDeletion.SelectedIndex = 6 Then
                iDelqty = Val(txtbxQty.Text) + Val(txtQuantity.Text)
            End If
            ActivateStatus(ddlTransNo.SelectedValue, "RS", iDelqty)
            lblAssetdeletionValidationMsg.Text = "Reactivated Sucessfully"
            lblstatus.Text = "Reactivated Sucessfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
            ImgbtnActivate.Visible = False
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgbtnActivate_Click")
        End Try
    End Sub
    Private Sub ActivateStatus(ByVal iTrnId As Integer, ByVal sStatus As String, ByVal iDelqty As Double)
        Try

            objAsstTrndel.UpdateDeletionStatus(sSession.AccessCode, sSession.AccessCodeID, iTrnId, sStatus, iDelqty)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ActivateStatus")
        End Try
    End Sub
End Class
